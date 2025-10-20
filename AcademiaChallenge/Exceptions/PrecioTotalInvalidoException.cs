using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Exceptions
{
    public class PrecioTotalInvalidoException : FacturaException
    {
        public PrecioTotalInvalidoException(double precioTotal) : base($"Error: el precio total {precioTotal} es inválido.")
        {
        }
    }
}
