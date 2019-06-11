using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Contratamos.Droid.Conexion
{
    public static class BaseDatos
    {
        private static string cadenaConexion = @"data source=190.8.176.202;initial catalog=decasa_empleos;user id=decasa_empleos;password=Empl30s2019*.";
        private static Dictionary<int, string> retorno;

        public static Models.Usuarios ValidarUsuarioConexionAcceso(string pUsuario, string pClave)
        {
            bool estadoSesion = false;
            Models.Usuarios usuarios = new Models.Usuarios();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@pUsuario", SqlDbType.VarChar, 10) ,
                    new SqlParameter("@pClave ", SqlDbType.VarChar, 100)
                };

                param[0].Value = pUsuario;
                param[1].Value = pClave;

                DataSet datosFiltros = new DataSet();
                datosFiltros = ValidarUsuarioLogin("ValidarUsuario", param);

                if (datosFiltros.Tables[0].Rows.Count > 0)
                {
                    estadoSesion = true;
                    usuarios.Usuario = datosFiltros.Tables[0].Rows[0][3].ToString();
                    usuarios.Nombre = datosFiltros.Tables[0].Rows[0][1].ToString();
                    usuarios.Apellido = string.Concat(datosFiltros.Tables[0].Rows[0][1].ToString(), " ", datosFiltros.Tables[0].Rows[0][2].ToString());
                    usuarios.IdUsuario = System.Convert.ToInt32(datosFiltros.Tables[0].Rows[0][0].ToString());
                    usuarios.IdTipoUsuario = System.Convert.ToInt32(datosFiltros.Tables[0].Rows[0][5].ToString());
       
                    return usuarios;
                }
                else
                {
                    retorno.Add(0, string.Concat("El usuario ", pUsuario, " no existe."));
                    return null;
                }
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }

        public static SqlConnection getConnection()
        {
            try
            {
                return new SqlConnection(cadenaConexion);
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
        }


        public static DataSet ValidarUsuarioLogin(string sp, SqlParameter[] param)
        {
            DataSet ds = new DataSet();
            SqlCommand command = new SqlCommand();
            try
            {
                command.Connection = getConnection();
                command.CommandText = sp;
                command.Connection.Open();

                foreach (SqlParameter par in param)
                {
                    command.Parameters.Add(par);
                }
                
                command.CommandType = CommandType.StoredProcedure;
                new SqlDataAdapter(command).Fill(ds);
                return ds;
            }
            catch (System.Exception ex)
            {
                throw new System.Exception(ex.Message);
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open) command.Connection.Close();
                command.Parameters.Clear();
                command.Dispose();
                ds.Dispose();
            }
        }


        public static List<Models.Profesiones> CargarProfesiones()
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                List<Models.Profesiones> listaProfesiones = new List<Models.Profesiones>();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("select * from [decasa_admin].[Profesiones]", cn))
                {
                    using (SqlDataReader reader = cmdConsulta.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Models.Profesiones profesion = new Models.Profesiones()
                            {
                                IdProfesion = reader.GetInt32(0),
                                Descripcion = reader.GetString(1)
                            };

                            listaProfesiones.Add(profesion);
                        }
                    }
                }
                return listaProfesiones;
            }
            catch (System.Exception)
            {
                return null;
            }
        }

        public static DataSet CargarOfertas()
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.CargarOfertas", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(datosFiltros);

                    return datosFiltros;
                }
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }

    }
}