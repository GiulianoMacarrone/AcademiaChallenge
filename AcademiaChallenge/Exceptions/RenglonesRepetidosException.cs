using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Exceptions
{
    public class RenglonesRepetidosException : FacturaException
    {
        public RenglonesRepetidosException() : base("Error: el pedido contiene renglones repetidos.")
        {
        }
    }
}
