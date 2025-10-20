using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AcademiaChallenge.Model
{
    /// <summary>
    /// Representa un artículo de inventario con sus datos maestros (código, descripción y precio)
    /// utilizado en Pedidos y Renglones de Factura.
    /// </summary>
    internal class Articulo
    {
        public required string CodigoArticulo { get; set; }
        public required string DescripcionArticulo { get; set; }
        public double PrecioUnitario { get; set; }
    }
}
