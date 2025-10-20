using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Negocios
{
    internal class NegocioCliente
    {
        public List<Cliente> Clientes { get; set; }
        public NegocioCliente()
        {
            Clientes = new List<Cliente>();
        }

        private void ValidarNuevoCliente(string codigoCliente, string razonSocialCliente)
        {
            if (Clientes.Any(c => c.CodigoCliente == codigoCliente))
                throw new AgregarCienteDuplicadoException("Error: el código del cliente ya existe.");

            if (Clientes.Any(c => c.RazonSocialCliente == razonSocialCliente))
                throw new AgregarCienteDuplicadoException("Error: la razón social del cliente ya existe.");
        }


        public void AgregarCliente(string codigoCliente, string razonSocialCliente, double porcentajeImpuestos)
        {
            ValidarNuevoCliente(codigoCliente, razonSocialCliente);
            Clientes.Add(
                new Cliente()
                {
                    CodigoCliente = codigoCliente,
                    RazonSocialCliente = razonSocialCliente,
                    PorcentajeImpuestos = porcentajeImpuestos
                }
            );
        }

        public void EliminarCliente(string codigoCliente)
        {
            var cliente = ObtenerCliente(codigoCliente);
            if (cliente != null)
                Clientes.Remove(cliente);
            else
                throw new ClienteNoEncontradoException("Error: el cliente no existe.");
        }

        public Cliente? ObtenerCliente(string codigoCliente)
        {
            return Clientes.FirstOrDefault(c => c.CodigoCliente == codigoCliente);
        }

    }
}
