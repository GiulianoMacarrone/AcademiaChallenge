using AcademiaChallenge.DTOs;
using AcademiaChallenge.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Mapper
{
    internal static class ArticuloMapper
    {
        public static DTOArticuloFactura ToArticuloFacturaDTO(Articulo articulo) => new DTOArticuloFactura
        {
            CodigoArticulo = articulo.CodigoArticulo,
            DescripcionArticulo = articulo.DescripcionArticulo,
            PrecioUnitario = articulo.PrecioUnitario
        };
    }
}
