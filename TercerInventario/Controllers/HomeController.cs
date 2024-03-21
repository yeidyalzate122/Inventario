///
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
//
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using TercerInventario.Models;
using TercerInventario.Models.ViewModel;
namespace TercerInventario.Controllers
{
    public class HomeController : Controller
    {                                       //me da la coneccion de la bd directamente  
        private readonly InventarioContext _DBContext;

        public HomeController(InventarioContext context)
        {
            _DBContext = context;
        }

        //Listar
        public IActionResult Index()
        {                                                       //me incluye la fK
            List<Producto> lista = _DBContext.Productos.Include(F => F.IdCategoriaNavigation).ToList();
            return View(lista);
        }

        //Listar categoria
        [HttpGet]
        public IActionResult CategoriaProducto(int idProducto)
        {
            ProductoMV oProductoMV = new ProductoMV()
            {
                oProducto = new Producto(),

                oListaCategoria = _DBContext.CategoriaProductos.Select(cate => new SelectListItem()
                {
                    Text = cate.Nombre,
                    Value = cate.Id.ToString()

                }).ToList()

            };


            if (idProducto != 0)
            {
                oProductoMV.oProducto = _DBContext.Productos.Find(idProducto);


            }

            return View(oProductoMV);
        }


   

        [HttpPost]
        public IActionResult GuardarProducto(ProductoMV oProductoMV)
        {
            try
            {
                if (oProductoMV.oProducto.Id == 0)
                {
                    _DBContext.Productos.Add(oProductoMV.oProducto);
                    TempData["SuccessMessage"] = "¡El producto se ha guardado correctamente!";
                }
                else
                {
                    _DBContext.Productos.Update(oProductoMV.oProducto);
                    TempData["SuccessMessage"] = "¡El producto se ha actualizado correctamente!";

                }
                //Guardar los cambios
                _DBContext.SaveChanges();

                // Mostrar mensaje de éxito
               

                return RedirectToAction("Index", "Home");
            }
            catch (DbUpdateException ex)
            {
                // Manejar la excepción y mostrar detalles del error interno
                if (ex.InnerException != null)
                {
                    Console.WriteLine("Detalles del error interno:");
                    Console.WriteLine(ex.InnerException.Message);
                }
                else
                {
                    Console.WriteLine("No se pudo obtener más información sobre el error interno.");
                }

         
                return View("Error");
            }
        }
    


    [HttpGet]
        public IActionResult EiliminarProducto(int idProducto)
        {
            Producto producto = _DBContext.Productos.Include(o => o.IdCategoriaNavigation).Where(e => e.Id == idProducto).FirstOrDefault();

            return View(producto);
        }

        [HttpPost]
        public IActionResult EiliminarProducto(Producto oProducto, CategoriaProducto categoria)
        {
            _DBContext.Productos.Remove(oProducto);
            _DBContext.SaveChanges();
            // Mostrar mensaje de éxito
            TempData["SuccessMessage"] = "¡El producto se ha eliminado correctamente!";
            return RedirectToAction("Index", "Home");
        }
    }
}
