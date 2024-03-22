using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SabritasMVC.Models.Entities
{
    public class Carrito
    {
        public int CarritoId { get; set; }
        public string Nombre { get; set; }
        public int Cantidad{ get; set; }
        public string Descripcion { get; set; }
        public double Precio { get; set; }
        public string Imagen { get; set; }
        public int UsuarioId { get; set; }
    }
}