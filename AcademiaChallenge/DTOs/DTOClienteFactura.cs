using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.DTOs
{
    public class DTOClienteFactura
    {
        public required string CodigoCliente { get; set; }
        public required string RazonSocialCliente { get; set; }
    }
}
