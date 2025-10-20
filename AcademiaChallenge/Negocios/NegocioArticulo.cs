using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AcademiaChallenge.Exceptions;
using AcademiaChallenge.Model;

namespace AcademiaChallenge.Negocios
{
    internal class NegocioArticulo
    {
        public List<Articulo> Articulos { get; set; }
        public NegocioArticulo()
        {
            Articulos = new List<Articulo>();
        }
        public void ValidarNuevoArticulo(string codigoArticulo, string descripcionArticulo)
        {
            if (Articulos.Any(a => a.CodigoArticulo == codigoArticulo))
            { throw new AgregarArticuloDuplicadoException("Error: el código del artículo ya existe."); }

            if (Articulos.Any(a => a.DescripcionArticulo == descripcionArticulo))
            { throw new AgregarArticuloDuplicadoException("Error: la descripción del artículo ya existe."); }
        }
        public void AgregarArticulo(string codigoArticulo, string descripcionArticulo, double precioUnitario)
        {
            ValidarNuevoArticulo(codigoArticulo, descripcionArticulo);
            Articulos.Add(new()
            {
                CodigoArticulo = codigoArticulo,
                DescripcionArticulo = descripcionArticulo,
                PrecioUnitario = precioUnitario
            });
        }
        public Articulo? ObtenerArticulo(string codigoArticulo)
        {
            return Articulos.FirstOrDefault(a => a.CodigoArticulo == codigoArticulo);
        }
    }
}
