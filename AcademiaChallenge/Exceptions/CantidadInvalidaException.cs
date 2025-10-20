using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Exceptions
{
    public class CantidadInvalidaException : FacturaException
    {
        public CantidadInvalidaException(double cantidad) : base($"Cantidad inválida: {cantidad}. Debe ser mayor que cero.")
        {
        }
    }
}
