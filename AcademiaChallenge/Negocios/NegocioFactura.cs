using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Mapper;
using AcademiaChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Negocios
{
    internal class NegocioFactura
    {
        private readonly List<Factura> Facturas = new List<Factura>();

        private readonly NegocioCliente negocioCliente;
        private readonly NegocioPedido negocioPedido;
        private readonly NegocioRecibo negocioRecibo;

        public NegocioFactura(NegocioCliente oNegocioCliente, NegocioPedido oNegocioPedido, NegocioRecibo oNegocioRecibo)
        {
            negocioCliente = oNegocioCliente;
            negocioPedido = oNegocioPedido;
            negocioRecibo = oNegocioRecibo;
        }

        private void ValidarBasicos(Pedido pedido, Recibo recibo, Cliente cliente)
        {
            if (pedido == null)
            { throw new PedidoNoEncontradoException(-1); }
            if (recibo == null)
            { throw new ReciboNoEncontradoException(-1); }
            if (cliente == null)
                throw new ClienteNoEncontradoException(pedido?.Cliente?.CodigoCliente ?? "N/A");
            if (pedido.Cliente.CodigoCliente != recibo.Cliente.CodigoCliente)
            { throw new NoCoincideClientePedidoyReciboException(); }
        }

        /* 
            * NOTA: para validar que el importe del recibo coincide con el total del pedido se uso Math
            * La idea es evitar problemas de redondeo con números decimales ya que el tipo double no es exacto
            * Con esto generamos un margen de error aceptable.
        */
        private void ValidarFactura(Recibo recibo, double totalConImpuestos)
        {
            if (Math.Abs(recibo.Importe - totalConImpuestos) > 0.01)
            {
                throw new NoCoincideimportePedidoyReciboException("Error: no coincide el importe del pedido y el recibo.");
            }
        }

        public int Facturar(int numeroPedido, int numeroRecibo, DateTime fecha)
        {
            var pedido = negocioPedido.ObtenerPedido(numeroPedido);
            var recibo = negocioRecibo.ObtenerRecibo(numeroRecibo);
            var cliente = negocioCliente.ObtenerCliente(pedido?.Cliente?.CodigoCliente ?? "");

            ValidarBasicos(pedido, recibo, cliente);

            var totalSinImpuestos = pedido.Renglones.Sum(r => r.PrecioTotal);
            var totalImpuestos = totalSinImpuestos * cliente.PorcentajeImpuestos;
            var totalConImpuestos = totalSinImpuestos + totalImpuestos;

            ValidarFactura(recibo, totalConImpuestos);

            int numeroFactura = Facturas.Count + 1;
            Facturas.Add(new Factura()
            {
                Numero = numeroFactura,
                Fecha = fecha,
                Cliente = ClienteMapper.ToClienteFacturaDTO(cliente),

                Renglones = pedido.Renglones.Select(r => new RenglonFactura
                {
                    Numero = r.Numero,
                    Articulo = ArticuloMapper.ToArticuloFacturaDTO(r.Articulo),
                    Cantidad = r.CantidadPedida,
                    PrecioTotal = r.PrecioTotal
                }).ToList(),
                TotalSinImpuestos = totalSinImpuestos,
                TotalImpuestos = totalImpuestos,
                TotalConImpuestos = totalConImpuestos
            });
            return numeroFactura;
        }

        public Factura ObtenerFactura(int numeroFactura)
        {
            return Facturas.SingleOrDefault(f => f.Numero == numeroFactura) ?? throw new FacturaNoEncontradaException(numeroFactura);
        }
    }
}
