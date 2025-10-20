using AcademiaChallenge.Exceptions;

namespace AcademiaChallenge.Exceptions
{
    public class AgregarCienteDuplicadoException : FacturaException
    {
        public AgregarCienteDuplicadoException(string message) : base(message)
        {
        }
    }
}
