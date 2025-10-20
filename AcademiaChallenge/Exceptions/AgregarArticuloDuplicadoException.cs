using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Exceptions
{
    public class AgregarArticuloDuplicadoException : FacturaException
    {
        public AgregarArticuloDuplicadoException(string codigoArticulo) : base($"Artículo {codigoArticulo} ya existe.")
        {
        }
    }
}
