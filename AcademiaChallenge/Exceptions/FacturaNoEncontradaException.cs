using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Exceptions
{
    public class FacturaNoEncontradaException : FacturaException
    {
        public FacturaNoEncontradaException(int numeroFactura) : base($"Factura {numeroFactura} no encontrada.")
        {
        }
    }
}
