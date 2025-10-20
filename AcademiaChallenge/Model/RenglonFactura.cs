using AcademiaChallenge.DTOs;

namespace AcademiaChallenge.Model
{
    public class RenglonFactura
    {
        public int Numero { get; set; }
        //public required string CodigoArticulo { get; set; }
        //public required string DescripcionArticulo { get; set; }
        //public double PrecioUnitario { get; set; }
        public required DTOArticuloFactura Articulo { get; set; }
        public double Cantidad { get; set; }
        public double PrecioTotal { get; set; }
    }
}
