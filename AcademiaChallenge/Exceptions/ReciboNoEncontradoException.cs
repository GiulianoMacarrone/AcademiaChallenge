using AcademiaChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Exceptions
{
    public class ReciboNoEncontradoException : FacturaException
    {
        public ReciboNoEncontradoException(int numeroRecibo) : base($"Recibo {numeroRecibo} no encontrado.")
        {
        }
    }
}
