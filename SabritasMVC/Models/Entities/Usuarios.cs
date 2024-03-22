using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SabritasMVC.Models.Entities
{
    public class Usuarios
    {
        public int UsuarioId {  get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public int RolId { get; set; }
        public string Passwd { get; set; }
    }
}