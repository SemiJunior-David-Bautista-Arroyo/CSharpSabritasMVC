using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using SabritasMVC.Models.Entities;

namespace SabritasMVC.Models.Sabritas.DAL
{
    public class ProductosDAL
    {
        string dbconexion;
        public ProductosDAL() {
            dbconexion = ConfigurationManager.ConnectionStrings["ConectaProductos"].ConnectionString;
        }

        public async Task<List<Productos>> ObtenerProductos()
        {
            List<Productos> ListaP = new List<Productos>();
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("ObtenerProductos", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {//Mientras sdr pueda leer filas
                        while (sdr.Read())
                        {//Se agregan los productos obtenidos a la lista
                            ListaP.Add(new Productos
                            {
                                ProductoId = Convert.ToInt16(sdr["ProductoId"]),
                                Nombre = sdr["Nombre"].ToString(),
                                Descripcion = sdr["Descripcion"].ToString(),
                                Precio = Convert.ToDouble(sdr["Precio"]),
                                Imagen = sdr["Imagen"].ToString()
                            });
                        }
                        con.Close(); //Cierre de conexion
                    }
                    else
                    { //Si no se obtuvo nada se retorna la lista vacía
                        ListaP = null;
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
                return ListaP; //Se retorna la lista con o sin valores

            }
        }
        public async Task<Productos> ObtenerProductoDetallado(int id)
        {
            Productos Pr = null;
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("ProductoDetallado", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@ProdID", id);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {//Mientras sdr pueda leer filas
                        while (sdr.Read())
                        {//Se agregan los productos obtenidos a la lista
                            Pr= new Productos
                            {
                                ProductoId = Convert.ToInt16(sdr["ProductoId"]),
                                Nombre = sdr["Nombre"].ToString(),
                                Descripcion = sdr["Descripcion"].ToString(),
                                Precio = Convert.ToDouble(sdr["Precio"]),
                                Imagen = sdr["Imagen"].ToString()
                                
                            };
                        }
                        con.Close(); //Cierre de conexion
                    }
                    else
                    { //Si no se obtuvo nada se retorna la lista vacía
                        Pr = null;
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
                return Pr; 

            }
        }

        public async Task<List<Carrito>> VerCarrito(int idusr)
        {
            List<Carrito> car = new List<Carrito>();
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("VerCarrito", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@UserId", idusr);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {//Mientras sdr pueda leer filas
                        while (sdr.Read())
                        {//Se agregan los productos obtenidos a la lista
                            car.Add(new Carrito
                            {
                                CarritoId = Convert.ToInt16(sdr["CarritoId"]),
                                Nombre = sdr["Nombre"].ToString(),
                                Cantidad = Convert.ToInt16(sdr["Cantidad"]),
                                Descripcion = sdr["Descripcion"].ToString(),
                                Precio = Convert.ToDouble(sdr["Precio"]),
                                Imagen = sdr["Imagen"].ToString()

                            });
                        }
                        con.Close(); //Cierre de conexion
                    }
                    else
                    { //Si no se obtuvo nada se retorna la lista vacía
                        car = null;
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
                return car;

            }
        }
        public async Task AgregarCarrito(string nom, double cantidad,string descrip, double precio,string imagen,int userid)
        {
            
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("AgregarCarrito", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@Nombre", nom);
                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                cmd.Parameters.AddWithValue("@Descripcion", descrip);
                cmd.Parameters.AddWithValue("@Precio", precio);
                cmd.Parameters.AddWithValue("@Imagen", imagen);
                cmd.Parameters.AddWithValue("@UserId", userid);
                try
                {
                    await con.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            

            }
        }
        
        public async Task BorrarCarrito(int carritoid)
        {
            
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("Borrar", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@CarritoId", carritoid);
                
                try
                {
                    await con.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
        }
        public async Task AgregarCompras(int carritoid, double total, int usurioid, string producto)
        {
            
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("AgregarCompras", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@CarritoId", carritoid);
                cmd.Parameters.AddWithValue("@Total", total);
                cmd.Parameters.AddWithValue("@UsuarioId", usurioid);
                cmd.Parameters.AddWithValue("@Producto", producto);
                
                try
                {
                    await con.OpenAsync();
                    await cmd.ExecuteReaderAsync();
                    con.Close();
                }
                catch (Exception)
                {
                    con.Close();
                }
            }
        }
        public async Task<List<Compras>> VerCompras(int idcarrito)
        {
            List<Compras> com = new List<Compras>();
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("VerCompras", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@CarritoId", idcarrito);
                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {//Mientras sdr pueda leer filas
                        while (sdr.Read())
                        {//Se agregan los productos obtenidos a la lista
                            com.Add(new Compras
                            {
                                ComprasId = Convert.ToInt16(sdr["ComprasId"]),
                                CarritoId = Convert.ToInt16(sdr["CarritoId"]),
                                Total = Convert.ToDouble(sdr["Total"]),
                                UsuarioId = Convert.ToInt32(sdr["UsuarioId"]),
                                Producto = sdr["Producto"].ToString()

                            });
                        }
                        con.Close(); //Cierre de conexion
                    }
                    else
                    { //Si no se obtuvo nada se retorna la lista vacía
                        com = null;
                    }
                }
                catch (Exception)
                {
                    con.Close();
                }
                return com;

            }
        }






    }
}