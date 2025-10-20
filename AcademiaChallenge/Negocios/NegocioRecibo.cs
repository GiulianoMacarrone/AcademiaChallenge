using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Negocios
{
    internal class NegocioRecibo
    {
        public List<Recibo> Recibos { get; set; }
        private readonly NegocioCliente negocioCliente;
        public NegocioRecibo(NegocioCliente oNegocioCliente)
        {
            negocioCliente = oNegocioCliente;
            Recibos = new List<Recibo>();
        }

        public void AgregarRecibo(DateTime fecha, string codigoCliente, double importe)
        {
            var cliente = negocioCliente.ObtenerCliente(codigoCliente);
            if (cliente == null)
            {
                throw new ClienteNoEncontradoException($"Cliente {codigoCliente} no encontrado.");
            }

            int proximoNumero = Recibos.Any() ? Recibos.Max(r => r.Numero) + 1 : 1;

            Recibos.Add(new()
            {
                Numero = proximoNumero,
                Fecha = fecha,
                Cliente = cliente,
                Importe = importe
            });
        }

        public Recibo? ObtenerRecibo(int numero)
        {
            return Recibos.FirstOrDefault(r => r.Numero == numero);
        }
    }
}
