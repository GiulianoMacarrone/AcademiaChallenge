using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Exceptions
{
    public class ClienteNoEncontradoException : FacturaException
    {
        public ClienteNoEncontradoException(string razonSocial) : base($"Cliente con ID {razonSocial} no encontrado.")
        {
        }
    }
}
