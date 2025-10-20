using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Model;
using AcademiaChallenge.Negocios;
using System;

namespace AcademiaChallenge
{
    public class Negocio
    {
        private readonly NegocioCliente negocioCliente;
        private readonly NegocioArticulo negocioArticulo;
        private readonly NegocioPedido negocioPedido;
        private readonly NegocioRecibo negocioRecibo;
        private readonly NegocioFactura negocioFactura;

        public Negocio()
        {
            negocioCliente = new NegocioCliente();
            negocioArticulo = new NegocioArticulo();
            negocioPedido = new NegocioPedido(negocioCliente, negocioArticulo);
            negocioRecibo = new NegocioRecibo(negocioCliente);
            negocioFactura = new NegocioFactura(negocioCliente, negocioPedido, negocioRecibo);
        }

        #region public
        /// <summary>
        /// Agrega en el registro de clientes del negocio un cliente con los datos enviados por parámetro.
        /// </summary>
        /// <param name="codigoCliente"></param>
        /// <param name="razonSocialCliente"></param>
        /// <param name="porcentajeImpuestos"></param>
        public void AgregarCliente(string codigoCliente, string razonSocialCliente, double porcentajeImpuestos)
        {
            negocioCliente.AgregarCliente(codigoCliente, razonSocialCliente, porcentajeImpuestos);
        }

        /// <summary>
        /// Agrega en el registro de clientes del negocio un cliente con los datos enviados por parámetro.
        /// </summary>
        /// <param name="codigoArticulo"></param>
        /// <param name="descripcionArticulo"></param>
        /// <param name="precioUnitario"></param>
        public void AgregarArticulo(string codigoArticulo, string descripcionArticulo, double precioUnitario)
        {
            negocioArticulo.AgregarArticulo(codigoArticulo, descripcionArticulo, precioUnitario);
        }

        /// <summary>
        /// Carga un pedido con los datos enviados por parámetro
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="fecha"></param>
        /// <param name="codigoCliente"></param>
        /// <param name="renglones"></param>
        public void AgregarPedido(int numero, DateTime fecha, string codigoCliente,
            List<(int numeroRenglon, string codigoArticulo, double cantidadPedida)> renglones)
        {
            negocioPedido.AgregarPedido(numero, fecha, codigoCliente, renglones);
        }

        /// <summary>
        /// Carga un recibo con los datos enviados por parámetro
        /// </summary>
        /// <param name="numero"></param>
        /// <param name="fecha"></param>
        /// <param name="codigoCliente"></param>
        /// <param name="importe"></param>
        public void AgregarRecibo(int numero, DateTime fecha, string codigoCliente, double importe)
        {
            negocioRecibo.AgregarRecibo(fecha, codigoCliente, importe);
        }

        /// <summary>
        /// Genera una factura en referencia al pedido y recibo enviados por parámetro
        /// </summary>
        /// <param name="numeroPedido"></param>
        /// <param name="numeroRecibo"></param>
        /// <param name="fecha"></param>
        /// <returns>número de la factura generada</returns>
        public int Facturar(int numeroPedido, int numeroRecibo, DateTime fecha)
        {
            return negocioFactura.Facturar(numeroPedido, numeroRecibo, fecha);
        }

        /// <summary>
        /// Busca y retorna una factura específica utilizando su número identificador.
        /// </summary>
        /// <param name="numeroFactura">El número identificador de la factura.</param>
        /// <returns>La instancia de la Factura si es encontrada.</returns>
        public Factura ObtenerFactura(int numeroFactura)
        {
            return negocioFactura.ObtenerFactura(numeroFactura);
        }

        /// <summary>
        /// Elimina un cliente de la colección de clientes maestros.
        /// </summary>
        /// <param name="codigoCliente">El código único del cliente a eliminar.</param>
        public void EliminarCliente(string codigoCliente)
        {
            negocioCliente.EliminarCliente(codigoCliente);
        }
        #endregion
    }
}
