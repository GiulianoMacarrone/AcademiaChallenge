using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Exceptions
{
    public class NoCoincideClientePedidoyReciboException : FacturaException
    {
        public NoCoincideClientePedidoyReciboException() : base("Error: el cliente del pedido y el recibo no coinciden.")
        {
        }
    }
}
