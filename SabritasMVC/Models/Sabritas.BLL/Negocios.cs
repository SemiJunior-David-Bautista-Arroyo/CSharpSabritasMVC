using SabritasMVC.Models.Entities;
using SabritasMVC.Models.Sabritas.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SabritasMVC.Models.Sabritas.BLL
{
    public class Negocios
    {
        ProductosDAL Sw;
        AdminDAL Ad;
        UsuariosDAL Lg;
        public Task<List<Productos>> ObtenerProductos()
        {
            Sw = new ProductosDAL();
            return Sw.ObtenerProductos();
        }
        public Task<Productos> ObtenerProductoDetallado(int id)
        {
            Sw = new ProductosDAL();
            return Sw.ObtenerProductoDetallado(id);
        }

        public async Task AgregarCarrito(string nom, double cantidad, string descrip, double precio, string imagen, int userid)
        {
            Sw = new ProductosDAL();
            await Sw.AgregarCarrito( nom, cantidad, descrip, precio, imagen,  userid);
        }

        public Task<List<Carrito>> VerCarrito(int idusr)
        {
            Sw = new ProductosDAL();
            return Sw.VerCarrito(idusr);
        }

        public Task Borrar(int id)
        {
            Sw = new ProductosDAL();
            return Sw.BorrarCarrito(id);
        }
        public Task Agregar(int id, double total, int usuarioid, string producto)
        {
            Sw = new ProductosDAL();
            return Sw.AgregarCompras(id, total, usuarioid,producto);
        }

        public Task<List<Compras>> VerCompras(int idcarrito)
        {
            Sw = new ProductosDAL();
            return Sw.VerCompras(idcarrito);
        }

        //////////////////AQUÍ EMPIEZA LA PARTE DEL ADMIN////////////////////////////////////////////////////////////
        
        public async Task AltaProducto(string nombre, double Precio, string imagen, string Descripcion)
        {
            Ad = new AdminDAL();
            await Ad.AltaProducto(nombre, Precio, imagen, Descripcion);
            
        }
        public async Task EditarProducto(int productoId ,string nombre, double Precio, string imagen, string Descripcion)
        {
            Ad = new AdminDAL();
            await Ad.EditarProd(productoId,nombre, Precio, imagen, Descripcion);
            
        }

        public async Task EliminarProducto(int productoid)
        {
            Ad = new AdminDAL();
            await Ad.EliminarProd(productoid);
        }

        public async Task<List<Usuarios>> ObtenerUsuarios()
        {
            Ad = new AdminDAL();
            return await Ad.ObtenerUsuarios();
        }

        public async Task<List<Compras>> Comprados(int idusr)
        {
            Ad = new AdminDAL();
            return await Ad.VerComprados(idusr);
        }

        public async Task<List<Usuarios>> ProductosUsuarios(string producto)
        {
            Ad = new AdminDAL();
            return await Ad.UsuariosProductos(producto);
        }

        ////////////////////////LOGUIN//////////////////////////////////
        
        public async Task<Usuarios> VerificarLoguin(string correo, string passwd)
        {
            Lg = new UsuariosDAL();
            return await Lg.VerificarLogin(correo, passwd);
        }

    }
}