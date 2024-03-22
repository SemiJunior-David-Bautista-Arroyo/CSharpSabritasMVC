using SabritasMVC.Models.Entities;
using SabritasMVC.Models.Sabritas.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SabritasMVC.Controllers
{
    public class AdminController : Controller
    {
        Negocios bll;
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> Listar()
        {
            bll = new Negocios();
            List<Productos> productos = await bll.ObtenerProductos();
            if (productos != null)
            {
                return View(productos);
            }
            else
            {
                return View("Error");
            }
        }
        public ActionResult NuevoProd()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> AltaProducto(string Nombre, double Precio, string Imagen, string Descripcion)
        {
            if (ModelState.IsValid)
            {
                bll = new Negocios();
                await bll.AltaProducto(Nombre, Precio, Imagen, Descripcion);
                return RedirectToAction("Listar");
            }
            else
            {
                // Si el modelo no es válido, vuelve a mostrar la vista de formulario
                return View();
            }
        }

        public async Task<ActionResult> Detalle(int id)
        {
            bll = new Negocios();
            Productos producto = await bll.ObtenerProductoDetallado(id);
            if (producto != null)
            {
                return View(producto);
            }
            else
            {
                return View("Error");
            }
        }

        public async Task<ActionResult> EditarProd(int id)
        {
            bll = new Negocios();
            Productos producto = await bll.ObtenerProductoDetallado(id);
            return View(producto);
        }
        [HttpPost]
        public async Task<ActionResult> EditarProducto(int ProductoId,string Nombre, double Precio, string Imagen, string Descripcion)
        {
            
                bll = new Negocios();
                await bll.EditarProducto(ProductoId, Nombre, Precio, Imagen, Descripcion);
                return RedirectToAction("Listar");
        }

        public async Task<ActionResult> Borrar(int id)
        {
            bll = new Negocios();
            await bll.EliminarProducto(id);
            return RedirectToAction("Listar");
        }

        public async Task<ActionResult> ListarUsuarios()
        {
            bll = new Negocios();
            List<Usuarios> productos = await bll.ObtenerUsuarios();
            return View(productos);
            
        }

        public async Task<ActionResult> Comprados(int id)
        {
            bll = new Negocios();
            List<Compras> Comprado = await bll.Comprados(id);
            return View(Comprado);
        }

        public async Task<ActionResult> ProductosUsuarios(string producto)
        {
            bll = new Negocios();
            List<Usuarios> usuarios = await bll.ProductosUsuarios(producto);
            return View(usuarios);
        }

        public ActionResult Salir()
        {

            Session.Remove("UsuarioId");//Remover las variables de ssesion
            return RedirectToAction("Loguin", "Acceso");
        }


    }
}