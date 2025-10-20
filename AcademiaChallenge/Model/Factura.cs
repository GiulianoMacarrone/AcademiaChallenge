using AcademiaChallenge.DTOs;

namespace AcademiaChallenge.Model
{
    public class Factura
    {
        public int Numero { get; init; }
        public DateTime Fecha { get; init; }
        //public required string CodigoCliente { get; set; }
        //public required string RazonSocialCliente { get; set; }
        public required DTOClienteFactura Cliente { get; init; }
        public required List<RenglonFactura> Renglones { get; init; }
        public double TotalSinImpuestos { get; init; }
        public double TotalImpuestos { get; init; }
        public double TotalConImpuestos { get; init; }
    }
}
