namespace AcademiaChallenge.Model
{
    internal class Recibo
    {
        public int Numero { get; set; }
        public DateTime Fecha { get; set; }
        //public required string CodigoCliente { get; set; }
        //public required string RazonSocialCliente { get; set; }
        public required Cliente Cliente { get; set; }
        public double Importe { get; set; }
    }
}
