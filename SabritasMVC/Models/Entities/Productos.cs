using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SabritasMVC.Models.Entities
{
    public class Productos
    {
        public int ProductoId { get; set; }
        public string Nombre { get; set; }
        public double Precio { get; set; }
        public string Imagen { get; set; }
        public string Descripcion { get; set; }
        
        
    }
}