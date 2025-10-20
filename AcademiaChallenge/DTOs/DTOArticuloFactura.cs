using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.DTOs
{
    public class DTOArticuloFactura
    {
        public required string CodigoArticulo { get; set; }
        public required string DescripcionArticulo { get; set; }
        public double PrecioUnitario { get; set; }
    }
}
