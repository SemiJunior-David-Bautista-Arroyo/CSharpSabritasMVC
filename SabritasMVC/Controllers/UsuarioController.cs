using SabritasMVC.Models.Entities;
using SabritasMVC.Models.Sabritas.BLL;
using SabritasMVC.Models.Sabritas.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SabritasMVC.Controllers
{
    public class UsuarioController : Controller
    {
        Negocios bll;
        // GET: Usuario
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Error()
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
        public async Task<ActionResult> Detalles(int id)
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

        public async Task<ActionResult> VerCarrito()
        {
            int idusr = 1;
            bll = new Negocios();
            List<Carrito> listcar = await bll.VerCarrito(idusr);
            if (listcar != null)
            {
                return View(listcar);
            }
            else
            {
                return View("Error");
            }
        }


        public async Task<ActionResult> Agregar(string nombre, double cantidad, string descripcion, double precio, string imagen)
        {
            
            int idusr = 1;
            bll = new Negocios();
            await bll.AgregarCarrito( nombre, cantidad, descripcion, precio,imagen, idusr);
            return RedirectToAction("VerCarrito");
        }


        public async Task<ActionResult> Borrar(int id)
        {
            bll = new Negocios();
            await bll.Borrar(id);
            return RedirectToAction("VerCarrito");

        }
        public async Task<ActionResult> Comprar(int id, double total, string producto)
        {
            int usuarioid = 1;
            bll = new Negocios();
            int idCarrito = id;
            await bll.Agregar(id, total, usuarioid, producto);
            return RedirectToAction("ComprasHechas", new {idcarrito = idCarrito});

        }
        public async Task<ActionResult> ComprasHechas(int idcarrito)
        {
            bll = new Negocios();
            List<Compras> lc = await bll.VerCompras(idcarrito);
            return View(lc);

        }

        public ActionResult Salir()
        {

            Session.Remove("UsuarioId");//Remover las variables de ssesion
            return RedirectToAction("Loguin", "Acceso");
        }





    }
}