using Microsoft.AspNetCore.Mvc.Rendering;

namespace TercerInventario.Models.ViewModel
{
    public class ProductoMV
    {
        public Producto oProducto { get; set; }

        public List<SelectListItem> oListaCategoria { get; set; }


    }
}
