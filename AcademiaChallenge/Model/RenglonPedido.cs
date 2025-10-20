namespace AcademiaChallenge.Model
{
    internal class RenglonPedido
    {
        public int Numero { get; set; }
        //public required string CodigoArticulo { get; set; }
        //public required string DescripcionArticulo { get; set; }
        //public double PrecioUnitario { get; set; }
        public required Articulo Articulo { get; set; }
        public double CantidadPedida { get; set; }
        public double PrecioTotal { get; set; }
    }
}