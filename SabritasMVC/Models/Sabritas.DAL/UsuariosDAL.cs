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
    public class UsuariosDAL
    {
        string dbconexion;

        public UsuariosDAL() { 
            dbconexion = ConfigurationManager.ConnectionStrings["ConectaProductos"].ConnectionString;
        }
        public async Task<Usuarios> VerificarLogin(string correo, string passwd)
        {
            Usuarios ListaP = null;
            using (SqlConnection con = new SqlConnection(dbconexion))//Conexion que vamos a usar
            {
                SqlCommand cmd = new SqlCommand("VerificarLogin", con);//Se llama al procedure
                cmd.CommandType = CommandType.StoredProcedure;// se elige comando de tipo Procedure
                cmd.Parameters.AddWithValue("@correo", correo);
                cmd.Parameters.AddWithValue("@password", passwd);

                try
                {
                    await con.OpenAsync();
                    SqlDataReader sdr = await cmd.ExecuteReaderAsync();
                    if (sdr.HasRows)
                    {//Mientras sdr pueda leer filas
                        while (sdr.Read())
                        {//Se agregan los productos obtenidos a la lista
                            ListaP = new Usuarios
                            {
                                UsuarioId = Convert.ToInt16(sdr["UsuarioId"]),
                                Nombre = sdr["Nombre"].ToString(),
                                Apellido = sdr["Apellido"].ToString(),
                                Correo = sdr["Correo"].ToString(),
                                RolId = Convert.ToInt16(sdr["RolId"]),
                                Passwd = sdr["Passwd"].ToString()
                            };
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