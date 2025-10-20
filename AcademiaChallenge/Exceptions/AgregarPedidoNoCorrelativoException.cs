using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Exceptions
{
    public class AgregarPedidoNoCorrelativoException : FacturaException
    {
        public AgregarPedidoNoCorrelativoException(int numero) : base($"Error: el número de pedido {numero} no es correlativo.")
        {
        }
    }
}
