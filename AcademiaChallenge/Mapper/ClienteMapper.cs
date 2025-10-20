using AcademiaChallenge.Model;
using AcademiaChallenge.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Mapper
{
    internal static class ClienteMapper
    {
        public static DTOClienteFactura ToClienteFacturaDTO(Cliente cliente) => new DTOClienteFactura
        {
            CodigoCliente = cliente.CodigoCliente,
            RazonSocialCliente = cliente.RazonSocialCliente
        };
    }

}
