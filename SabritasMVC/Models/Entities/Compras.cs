using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SabritasMVC.Models.Entities
{
    public class Compras
    {
        public int ComprasId { get; set; }
        public int CarritoId { get; set; }
        public double Total { get; set; }
        public int UsuarioId { get; set; }
        public string Producto { get; set; }
    }
}