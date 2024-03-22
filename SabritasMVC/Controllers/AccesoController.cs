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
    public class AccesoController : Controller
    {
        Negocios bll;
        // GET: Acceso
        public ActionResult Index()
        {
            return View();
        }

        public  ActionResult Loguin()
        {
            return View();
        }
        [HttpPost]
        public async Task<ActionResult> VerificarLoguin(string correo, string passwd)
        {
            bll = new Negocios();
            Usuarios user = await bll.VerificarLoguin(correo, passwd);
            if (user != null)
            {
                Session["UsuarioId"] = user.UsuarioId;

                if (user.RolId == 1)
                {
                    return RedirectToAction("Listar", "Admin");
                }
                else if(user.RolId == 2)
                {
                    return RedirectToAction("Listar", "Usuario");
                }
                else
                {
                    // Manejar el caso en que el usuario no sea encontrado
                    return RedirectToAction("Loguin");
                }
            }
            else
            {
                // Manejar el caso en que el usuario no sea encontrado
                return RedirectToAction("Loguin");
            }
        }



    }
}