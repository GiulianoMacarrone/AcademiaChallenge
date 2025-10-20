using AcademiaChallenge;
using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Model;

namespace AcademiaChallengeTest
{
    [TestClass]
    public class NegocioTest
    {
        #region valores para testear
        private const string codigoCliente1 = "codigoCliente1";
        private const string razonSocialCliente1 = "razonSocialCliente1";
        private const double porcentajeImpuestoCliente1 = 0.1;
        private const string codigoArticulo1 = "codigoArticulo1";
        private const string descripcionArticulo1 = "descripcionArticulo1";
        private const double cantidadPedida1 = 3;
        private const double precioUnitario1 = 5;
        private const double precioTotal1 = 15;
        private const double importeRecibo1 = 16.5;
        private readonly DateTime fecha1 = new(1);
        private readonly DateTime fecha2 = new(2);
        private readonly DateTime fecha3 = new(3);
        #endregion

        #region tests circuito
        [TestMethod]
        public void Facturar_UnCliente_UnPedido_UnArticulo()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);
            negocio.AgregarPedido(1, fecha1, codigoCliente1,
                [(1, codigoArticulo1, cantidadPedida1)]);
            negocio.AgregarRecibo(1, fecha2, codigoCliente1, importeRecibo1);
            int numeroFactura = negocio.Facturar(1, 1, fecha3);
            Factura factura = negocio.ObtenerFactura(numeroFactura);
            Assert.AreEqual(1, numeroFactura);
            Assert.AreEqual(numeroFactura, factura.Numero);
            Assert.AreEqual(fecha3, factura.Fecha);
            Assert.AreEqual(codigoCliente1, factura.Cliente.CodigoCliente);
            Assert.AreEqual(razonSocialCliente1, factura.Cliente.RazonSocialCliente);
            Assert.AreEqual(1, factura.Renglones.Count);
            Assert.AreEqual(1, factura.Renglones.Single().Numero);
            Assert.AreEqual(codigoArticulo1, factura.Renglones.Single().Articulo.CodigoArticulo);
            Assert.AreEqual(descripcionArticulo1, factura.Renglones.Single().Articulo.DescripcionArticulo);
            Assert.AreEqual(cantidadPedida1, factura.Renglones.Single().Cantidad);
            Assert.AreEqual(precioUnitario1, factura.Renglones.Single().Articulo.PrecioUnitario);
            Assert.AreEqual(precioTotal1, factura.Renglones.Single().PrecioTotal);
            Assert.AreEqual(precioTotal1, factura.TotalSinImpuestos);
            Assert.AreEqual(1.5, factura.TotalImpuestos);
            Assert.AreEqual(importeRecibo1, factura.TotalConImpuestos);
        }
        #endregion

        #region validaciones simples
        [TestMethod] // No se puede agregar el cliente mas de una vez
        public void AgregarDosVecesMismoClienteTiraErrorDuplicado()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            Assert.ThrowsException<AgregarCienteDuplicadoException>(() => negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1));
        }

        [TestMethod] // No se pueden tener dos clientes con el mismo código.
        public void AgregarDosClientesMismoCodigoTiraErrorDuplicado()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            Assert.ThrowsException<AgregarCienteDuplicadoException>(() => negocio.AgregarCliente(codigoCliente1, "otraRazonSocial", 0.2));
        }

        [TestMethod] // No se pueden tener dos clientes con la misma descripción
        public void AgregarClienteConDescripcionIgualTiraErrorDuplicado()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            Assert.ThrowsException<AgregarCienteDuplicadoException>(() => negocio.AgregarCliente("otroCodigo", razonSocialCliente1, porcentajeImpuestoCliente1));
        }

        [TestMethod] // No se puede agregar el artículo mas de una vez
        public void AgregarDosVecesMismoArticuloTiraErrorDuplicado()
        {
            Negocio negocio = new();
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);
            Assert.ThrowsException<AgregarArticuloDuplicadoException>(() => negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1));
        }

        [TestMethod] // No se pueden tener dos artículos con la misma descripción.
        public void AgregarDosArticulosMismaDescripcionTiraErrorDuplicado()
        {
            Negocio negocio = new();
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);
            Assert.ThrowsException<AgregarArticuloDuplicadoException>(() => negocio.AgregarArticulo("otroCodigo", descripcionArticulo1, precioUnitario1));
        }

        [TestMethod] // No se pueden tener dos artículos con el mismo código.
        public void AgregarArticuloConCodigoIgualTiraErrorDuplicado()
        {
            Negocio negocio = new();
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);
            Assert.ThrowsException<AgregarArticuloDuplicadoException>(() => negocio.AgregarArticulo(codigoArticulo1, "otraDescripcion", precioUnitario1));
        }

        [TestMethod] // La numeración de los pedidos comienza en 1 y es correlativa, sin saltearse números 
        public void AgregarPedidoConNumeroNoCorrelativoTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);
            negocio.AgregarPedido(1, fecha1, codigoCliente1,
                [(1, codigoArticulo1, cantidadPedida1)]);
            Assert.ThrowsException<AgregarPedidoNoCorrelativoException>(() => negocio.AgregarPedido(3, fecha1, codigoCliente1,
                [(1, codigoArticulo1, cantidadPedida1)]));
        }

        [TestMethod]
        public void AgregarPedidoConClienteInexistenteTiraError()
        {
            Negocio negocio = new();
            Assert.ThrowsException<ClienteNoEncontradoException>(() => negocio.AgregarPedido(1, fecha1, codigoCliente1, [(1, codigoArticulo1, cantidadPedida1)]));
        }

        [TestMethod]
        public void AgregarPedidoConArticuloInexistenteTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            Assert.ThrowsException<ArticuloNoEncontradoException>(() => negocio.AgregarPedido(1, fecha1, codigoCliente1,
                [(1, codigoArticulo1, cantidadPedida1)]));
        }

        [TestMethod]
        public void AgregarReciboConClienteInexistenteTiraError()
        {
            Negocio negocio = new();
            Assert.ThrowsException<ClienteNoEncontradoException>(() => negocio.AgregarRecibo(1, fecha1, codigoCliente1, importeRecibo1));
        }

        [TestMethod] // Los renglones no pueden tener artículos repetidos.
        public void AgregarPedidoConRenglonesRepetidosTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);

            var renglones = new List<(int, string, double)>
            {
                (1, codigoArticulo1, 2),
                (1, codigoArticulo1, 3)
            };

            Assert.ThrowsException<RenglonesRepetidosException>(() => negocio.AgregarPedido(1, fecha1, codigoCliente1, renglones));
        }

        [TestMethod] // Las cantidades pedidas de cada renglón de pedido tienen que tener sentido
        public void AgregarPedidoConCantidadInvalidaTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);

            var renglones = new List<(int, string, double)>
            {
                (1, codigoArticulo1, -5)
            };

            Assert.ThrowsException<CantidadInvalidaException>(() => negocio.AgregarPedido(1, fecha1, codigoCliente1, renglones));
        }

        [TestMethod] //Los artículos se identifican de forma unívoca según su codigo, validar que un mismo artículo siempre tenga mismo código, descripción y precio unitario
        public void AgregarArticuloConMismoCodigoDiferentePrecioTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);
            Assert.ThrowsException<AgregarArticuloDuplicadoException>(() => negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, 999));
        }

        [TestMethod]
        public void AgregarArticuloConMismoCodigoDiferenteDescripcionTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);

            Assert.ThrowsException<AgregarArticuloDuplicadoException>(() => negocio.AgregarArticulo(codigoArticulo1, "otraDescripcion", precioUnitario1));
        }

        [TestMethod]
        public void AgregarPedidoConArticulosDuplicadosTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);

            var renglones = new List<(int, string, double)>
            {
                (1, codigoArticulo1, 2),
                (2, codigoArticulo1, 3)
            };

            Assert.ThrowsException<AgregarArticuloDuplicadoException>(() => negocio.AgregarPedido(1, fecha1, codigoCliente1, renglones));
        }

        #endregion

        #region validaciones de circuito
        [TestMethod]
        public void Facturar_UnCliente_UnPedido_UnArticulo_SiNoCoincidenImportesTiraError()  //no paso
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);
            negocio.AgregarPedido(1, fecha1, codigoCliente1,
                [(1, codigoArticulo1, cantidadPedida1)]);
            negocio.AgregarRecibo(1, fecha2, codigoCliente1, 100);
            Assert.ThrowsException<NoCoincideimportePedidoyReciboException>(() => negocio.Facturar(1, 1, fecha3));
        }

        [TestMethod]
        public void Facturar_UnPedido_SiNoExisteTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            negocio.AgregarRecibo(1, fecha1, codigoCliente1, importeRecibo1);

            Assert.ThrowsException<PedidoNoEncontradoException>(() => negocio.Facturar(99, 1, fecha3));
        }

        [TestMethod]
        public void Facturar_UnRecibo_SiNoExisteTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);
            negocio.AgregarPedido(1, fecha1, codigoCliente1,
                [(1, codigoArticulo1, cantidadPedida1)]);

            Assert.ThrowsException<ReciboNoEncontradoException>(() => negocio.Facturar(1, 99, fecha3));
        }

        [TestMethod]
        public void Facturar_SiNoCoincideClientePedidoYReciboTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            negocio.AgregarCliente("codigoCliente2", "razonSocialCliente2", porcentajeImpuestoCliente1);
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);
            negocio.AgregarPedido(1, fecha1, codigoCliente1,
                [(1, codigoArticulo1, cantidadPedida1)]);
            negocio.AgregarRecibo(1, fecha2, "codigoCliente2", importeRecibo1);
            Assert.ThrowsException<NoCoincideClientePedidoyReciboException>(() => negocio.Facturar(1, 1, fecha3));
        }

        [TestMethod]
        public void ObtenerFactura_SiNoExisteTiraError()
        {
            Negocio negocio = new();
            Assert.ThrowsException<FacturaNoEncontradaException>(() => negocio.ObtenerFactura(99));
        }


        [TestMethod]
        public void Facturar_ClienteInexistenteTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarCliente("0", razonSocialCliente1, porcentajeImpuestoCliente1);
            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);
            negocio.AgregarPedido(1, fecha1, "0",
                [(1, codigoArticulo1, cantidadPedida1)]);
            negocio.AgregarRecibo(1, fecha2, "0", importeRecibo1);

            //se implementa la eliminación del cliente para simular que no existe al momento de facturar
            negocio.EliminarCliente("0");

            Assert.ThrowsException<ClienteNoEncontradoException>(() => negocio.Facturar(1, 1, fecha3));
        }

        [TestMethod]
        public void Facturar_SiClientesNoCoincidenTiraError()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);
            negocio.AgregarCliente("otroCliente", "otraRazonSocial", 0.1);

            negocio.AgregarArticulo(codigoArticulo1, descripcionArticulo1, precioUnitario1);
            negocio.AgregarPedido(1, fecha1, codigoCliente1,
                [(1, codigoArticulo1, cantidadPedida1)]);
            negocio.AgregarRecibo(1, fecha2, "otroCliente", importeRecibo1);

            Assert.ThrowsException<NoCoincideClientePedidoyReciboException>(() => negocio.Facturar(1, 1, fecha3));
        }

        [TestMethod]
        public void FacturarPedidoConMultiplesRenglones_CalculaTotalesCorrectamente()
        {
            Negocio negocio = new();
            negocio.AgregarCliente(codigoCliente1, razonSocialCliente1, porcentajeImpuestoCliente1);

            negocio.AgregarArticulo("A1", "Articulo 1", 10);
            negocio.AgregarArticulo("A2", "Articulo 2", 20);

            var renglones = new List<(int, string, double)>
            {
                (1, "A1", 2),
                (2, "A2", 1)
            };

            negocio.AgregarPedido(1, fecha1, codigoCliente1, renglones);

            double totalPedido = 40;
            double totalImpuestos = totalPedido * porcentajeImpuestoCliente1;
            double totalConImpuestos = totalPedido + totalImpuestos;

            negocio.AgregarRecibo(1, fecha2, codigoCliente1, totalConImpuestos);

            int numeroFactura = negocio.Facturar(1, 1, fecha3);
            Factura factura = negocio.ObtenerFactura(numeroFactura);

            Assert.AreEqual(totalPedido, factura.TotalSinImpuestos);
            Assert.AreEqual(totalImpuestos, factura.TotalImpuestos);
            Assert.AreEqual(totalConImpuestos, factura.TotalConImpuestos);
            Assert.AreEqual(2, factura.Renglones.Count);
        }

        [TestMethod]
        public void FacturarVariosClientesYPedidos_SeFacturaCorrectamente()
        {
            Negocio negocio = new();
            negocio.AgregarCliente("C1", "Cliente 1", 0.1);
            negocio.AgregarCliente("C2", "Cliente 2", 0.2);

            negocio.AgregarArticulo("X", "Articulo X", 10);
            negocio.AgregarArticulo("Y", "Articulo Y", 20);

            negocio.AgregarPedido(1, fecha1, "C1", new List<(int, string, double)> { (1, "X", 1) });
            negocio.AgregarRecibo(1, fecha2, "C1", 11);

            negocio.AgregarPedido(2, fecha1, "C2", new List<(int, string, double)> { (1, "Y", 2) });
            negocio.AgregarRecibo(2, fecha2, "C2", 48);

            int facturaC1 = negocio.Facturar(1, 1, fecha3);
            int facturaC2 = negocio.Facturar(2, 2, fecha3);

            Assert.AreEqual(11, negocio.ObtenerFactura(facturaC1).TotalConImpuestos);
            Assert.AreEqual(48, negocio.ObtenerFactura(facturaC2).TotalConImpuestos);
        }

        #endregion
    }
}