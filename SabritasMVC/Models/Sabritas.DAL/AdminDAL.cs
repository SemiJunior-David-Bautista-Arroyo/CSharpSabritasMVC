using SabritasMVC.Models.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace SabritasMVC.Models.Sabritas.DAL
{
    public class AdminDAL
    {
        String dbconexion;
        public AdminDAL() {
            dbconexion = ConfigurationManager.ConnectionStrings["ConectaProductos"].ConnectionString;
        }

        public async Task AltaProducto(string nombre, double Precio, string imagen, string Descripcion)
        {
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("AltaProducto", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Precio", Precio);
                cmd.Parameters.AddWithValue("@Imagen", imagen);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
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

        public async Task EditarProd(int productoId,string nombre, double Precio, string imagen, string Descripcion)
        {
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("Editar", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@ProductoId", productoId);
                cmd.Parameters.AddWithValue("@Nombre", nombre);
                cmd.Parameters.AddWithValue("@Precio", Precio);
                cmd.Parameters.AddWithValue("@Imagen", imagen);
                cmd.Parameters.AddWithValue("@Descripcion", Descripcion);
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
        public async Task EliminarProd(int productoId)
        {
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("EliminarProducto", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@ProductoId", productoId);
                
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
        public async Task<List<Usuarios>> ObtenerUsuarios()
        {
            List<Usuarios> ListaP = new List<Usuarios>();
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("ListarUsuarios", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {//Mientras sdr pueda leer filas
                        while (sdr.Read())
                        {//Se agregan los productos obtenidos a la lista
                            ListaP.Add(new Usuarios
                            {
                                UsuarioId = Convert.ToInt16(sdr["UsuarioId"]),
                                Nombre = sdr["Nombre"].ToString(),
                                Apellido = sdr["Apellido"].ToString(),
                                Correo = sdr["Correo"].ToString(),
                                Passwd = sdr["Password"].ToString(),
                                RolId = Convert.ToInt32(sdr["RolId"])
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
        public async Task<List<Compras>> VerComprados(int idusr)
        {
            List<Compras> ListaP = new List<Compras>();
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("VerComprados", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@UsuarioId", idusr);

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {//Mientras sdr pueda leer filas
                        while (sdr.Read())
                        {//Se agregan los productos obtenidos a la lista
                            ListaP.Add(new Compras
                            {
                                ComprasId = Convert.ToInt16(sdr["ComprasId"]),
                                CarritoId = Convert.ToInt16(sdr["CarritoId"]),
                                Total = Convert.ToDouble(sdr["Total"]),
                                UsuarioId = Convert.ToInt16(sdr["UsuarioId"]),
                                Producto = sdr["Producto"].ToString()
                                
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
        public async Task<List<Usuarios>> UsuariosProductos(string producto)
        {
            List<Usuarios> ListaP = new List<Usuarios>();
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("UsuariosProducto", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@Producto", producto);

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {//Mientras sdr pueda leer filas
                        while (sdr.Read())
                        {//Se agregan los productos obtenidos a la lista
                            ListaP.Add(new Usuarios
                            {
                                UsuarioId = Convert.ToInt16(sdr["UsuarioId"]),
                                Nombre = sdr["Nombre"].ToString(),
                                Apellido = sdr["Apellido"].ToString(),
                                Correo = sdr["Correo"].ToString(),
                                Passwd = sdr["Password"].ToString(),
                                RolId = Convert.ToInt32(sdr["RolId"])

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


        






    }
}