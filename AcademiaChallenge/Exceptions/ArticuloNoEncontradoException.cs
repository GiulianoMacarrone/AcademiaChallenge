using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Exceptions
{
    public class ArticuloNoEncontradoException : FacturaException
    {
        public ArticuloNoEncontradoException(string codigoArticulo) : base($"Artículo {codigoArticulo} no encontrado.")
        {
        }
    }
}
