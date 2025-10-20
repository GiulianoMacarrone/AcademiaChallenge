using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Exceptions
{
    public class PedidoNoEncontradoException : FacturaException
    {
        public PedidoNoEncontradoException(int numeroPedido) : base($"Pedido {numeroPedido} no encontrado.")
        {
        }
    }
}
