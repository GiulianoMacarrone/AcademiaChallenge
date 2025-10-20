using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Negocios
{
    internal class NegocioPedido
    {
        public List<Pedido> Pedidos { get; set; }
        private readonly NegocioCliente negocioCliente;
        private readonly NegocioArticulo negocioArticulo;

        public NegocioPedido(NegocioCliente oNegocioCliente, NegocioArticulo oNegocioArticulo)
        {
            negocioCliente = oNegocioCliente;
            negocioArticulo = oNegocioArticulo;
            Pedidos = new List<Pedido>();
        }

        public void AgregarPedido(int numero, DateTime fecha, string codigoCliente,
           List<(int numeroRenglon, string codigoArticulo, double cantidadPedida)> renglones)
        {
            var cliente = negocioCliente.ObtenerCliente(codigoCliente);
            if (cliente == null)
            {
                throw new ClienteNoEncontradoException(codigoCliente);
            }

            var numerosRenglones = renglones.Select(r => r.numeroRenglon).ToList();
            if (numerosRenglones.Count != numerosRenglones.Distinct().Count())
            {
                var numeroRepetido = renglones
                    .GroupBy(r => r.numeroRenglon)
                    .FirstOrDefault(g => g.Count() > 1)?.Key;
                throw new RenglonesRepetidosException();
            }

            var codigosArticulos = renglones.Select(r => r.codigoArticulo).ToList();
            if (codigosArticulos.Count != codigosArticulos.Distinct().Count())
            {
                var codigoRepetido = renglones.GroupBy(r => r.codigoArticulo)
                        .FirstOrDefault(g => g.Count() > 1)?.Key;
                if (codigoRepetido != null) throw new AgregarArticuloDuplicadoException(codigoRepetido);
            }

            foreach (var r in renglones)
            {
                if (r.cantidadPedida <= 0) throw new CantidadInvalidaException(r.numeroRenglon);
            }

            if (Pedidos.Any() && numero != Pedidos.Max(p => p.Numero) + 1)
            {
                throw new AgregarPedidoNoCorrelativoException(numero);
            }

            //Tambien se puede forzar la correlatividad:
            //int proximoNumero = Pedidos.Any() ? Pedidos.Max(r => r.Numero) + 1 : 1;

            Pedidos.Add(new()
            {
                Numero = numero,
                Fecha = fecha,
                Cliente = cliente,
                Renglones = renglones.Select(r =>
                {
                    var articulo = negocioArticulo.ObtenerArticulo(r.codigoArticulo);
                    if (articulo == null)
                    {
                        throw new ArticuloNoEncontradoException(r.codigoArticulo);
                    }

                    return new RenglonPedido
                    {
                        Numero = r.numeroRenglon,
                        Articulo = articulo,
                        CantidadPedida = r.cantidadPedida,
                        PrecioTotal = articulo.PrecioUnitario * r.cantidadPedida
                    };
                }).ToList()
            });
        }

        public Pedido? ObtenerPedido(int numero)
        {
            return Pedidos.FirstOrDefault(p => p.Numero == numero);
        }
    }
}
