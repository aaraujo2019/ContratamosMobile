using Contratamos.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Contratamos.Droid.Conexion
{
    public static class BaseDatos
    {
        private static string cadenaConexion = @"data source=190.8.176.202;initial catalog=decasa_empleos;user id=decasa_empleos;password=Empl30s2019*.";
        private static Dictionary<int, string> retorno;

        public static Usuarios ValidarUsuarioConexionAcceso(string pUsuario, string pClave)
        {
            bool estadoSesion = false;
            Usuarios usuarios = new Usuarios();
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
                    byte[] archivoByte = new byte[datosFiltros.Tables[0].Rows[0][7].ToString() == string.Empty ? 0 : datosFiltros.Tables[0].Rows[0][7].ToString().Length];
                    usuarios.Usuario = datosFiltros.Tables[0].Rows[0][3].ToString();
                    usuarios.Nombre = datosFiltros.Tables[0].Rows[0][1].ToString();
                    usuarios.Apellido = datosFiltros.Tables[0].Rows[0][2].ToString();
                    usuarios.IdUsuario = Convert.ToInt32(datosFiltros.Tables[0].Rows[0][0].ToString());
                    usuarios.Email = datosFiltros.Tables[0].Rows[0][5].ToString();
                    usuarios.IdTipoUsuario = Convert.ToInt32(datosFiltros.Tables[0].Rows[0][6].ToString());
                    usuarios.ArchivoCv = archivoByte;
                    usuarios.Contraseña = datosFiltros.Tables[0].Rows[0][4].ToString();
                    usuarios.Celular = datosFiltros.Tables[0].Rows[0][8].ToString();

                    return usuarios;
                }
                else
                {
                    retorno.Add(0, string.Concat("El usuario ", pUsuario, " no iste."));
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static SqlConnection getConnection()
        {
            try
            {
                return new SqlConnection(cadenaConexion);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                if (command.Connection.State == ConnectionState.Open)
                {
                    command.Connection.Close();
                }

                command.Parameters.Clear();
                command.Dispose();
                ds.Dispose();
            }
        }


        public static Usuarios ConsultarusuarioPorID(int pIdUsuario)
        {
            Usuarios usuarios = new Usuarios();
            try
            {
                SqlParameter[] param =
                {
                    new SqlParameter("@pIdUsuario", SqlDbType.VarChar, 10) 
                };

                param[0].Value = pIdUsuario;

                DataSet datosFiltros = new DataSet();
                datosFiltros = ValidarUsuarioLogin("decasa_admin.ConsultarusuarioPorID", param);

                if (datosFiltros.Tables[0].Rows.Count > 0)
                {
                    byte[] archivoByte = new byte[datosFiltros.Tables[0].Rows[0][7].ToString() == string.Empty ? 0 : datosFiltros.Tables[0].Rows[0][7].ToString().Length];
                    usuarios.Usuario = datosFiltros.Tables[0].Rows[0][3].ToString();
                    usuarios.Nombre = datosFiltros.Tables[0].Rows[0][1].ToString();
                    usuarios.Apellido = datosFiltros.Tables[0].Rows[0][2].ToString();
                    usuarios.IdUsuario = Convert.ToInt32(datosFiltros.Tables[0].Rows[0][0].ToString());
                    usuarios.Email = datosFiltros.Tables[0].Rows[0][5].ToString();
                    usuarios.IdTipoUsuario = Convert.ToInt32(datosFiltros.Tables[0].Rows[0][6].ToString());
                    usuarios.ArchivoCv = archivoByte;
                    usuarios.Contraseña = datosFiltros.Tables[0].Rows[0][4].ToString();
                    usuarios.Celular = datosFiltros.Tables[0].Rows[0][8].ToString();
                    usuarios.Observaciones = datosFiltros.Tables[0].Rows[0][9].ToString();

                    return usuarios;
                }
                else
                {
                    retorno.Add(0, string.Concat("El usuario ", pIdUsuario, " no existe."));
                    return null;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public static List<Profesiones> CargarProfesiones()
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                List<Profesiones> listaProfesiones = new List<Profesiones>();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("select * from [decasa_admin].[Profesiones]", cn))
                {
                    using (SqlDataReader reader = cmdConsulta.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Profesiones profesion = new Profesiones()
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
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Ciudades> CargarCiudades()
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                List<Ciudades> listaCiudades = new List<Ciudades>();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.CargarCiudades", cn))
                {
                    using (SqlDataReader reader = cmdConsulta.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Ciudades ciudad = new Ciudades()
                            {
                                IdCiudad = reader.GetInt32(0),
                                NombreCiudad = reader.GetString(1)
                            };

                            listaCiudades.Add(ciudad);
                        }
                    }
                }
                return listaCiudades;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<Aplicaciones> CargarAplcaciones()
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                List<Aplicaciones> listaAplcaciones = new List<Aplicaciones>();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("[decasa_admin].[AplicacionesUsuarios]", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(datosFiltros);

                    foreach (DataRow dr in datosFiltros.Tables[0].Rows)
                    {
                        listaAplcaciones.Add(new Aplicaciones
                        {
                            Titulo = dr["Titulo"].ToString(),
                            NombreUsuario = dr["NombreUsuario"].ToString(),
                            FechaAplicación = Convert.ToDateTime(dr["FechaAplicacion"]).ToShortDateString(),
                            IdUsuario = Convert.ToInt32(dr["IdUsuario"])
                        });
                    }
                }

                return listaAplcaciones;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<TipoUsuario> CargarTipoUsuario()
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                List<TipoUsuario> listaTipoUsuarios = new List<TipoUsuario>();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("select * from [decasa_admin].[TipoUsuario] where not Descripcion = 'Administrador'", cn))
                {
                    using (SqlDataReader reader = cmdConsulta.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            TipoUsuario tipoUsuario = new TipoUsuario()
                            {
                                IdTipoUsuario = reader.GetInt32(0),
                                Descripcion = reader.GetString(1)
                            };

                            listaTipoUsuarios.Add(tipoUsuario);
                        }
                    }
                }
                return listaTipoUsuarios;
            }
            catch (Exception)
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
            catch (Exception)
            {
                return null;
            }
        }


        public static DataSet ConsultarTipoUsuarioId(int idTipoUsuario)
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.ConsultarTipoUsuarioId", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pIdTipoUsuario", idTipoUsuario);
                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(datosFiltros);

                    return datosFiltros;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void GuardarUsuario(Usuarios usuario)
        {
            try
            {
                byte[] archivoByte = new byte[usuario.ArchivoCv == null ? 0 : usuario.ArchivoCv.Length];
                SqlConnection cn = getConnection();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.InsertarUsuario", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pNombre", usuario.Nombre);
                    cmdConsulta.Parameters.AddWithValue("@pApellido", usuario.Apellido);
                    cmdConsulta.Parameters.AddWithValue("@pUsuario", usuario.Usuario);
                    cmdConsulta.Parameters.AddWithValue("@pContraseña", usuario.Contraseña);
                    cmdConsulta.Parameters.AddWithValue("@pEmail", usuario.Email);
                    cmdConsulta.Parameters.AddWithValue("@pIdTipoUsuario", usuario.IdTipoUsuario);
                    cmdConsulta.Parameters.AddWithValue("@pArchivoCv", archivoByte);
                    cmdConsulta.Parameters.AddWithValue("@pCelular", usuario.Celular);
                    cmdConsulta.Parameters.AddWithValue("@pObservaciones", usuario.Observaciones);
                    cmdConsulta.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void ActualizarUsuario(Usuarios usuario)
        {
            try
            {
                byte[] archivoByte = new byte[usuario.ArchivoCv == null ? 0 : usuario.ArchivoCv.Length];
                SqlConnection cn = getConnection();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.ActualizarUsuario", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pNombre", usuario.Nombre);
                    cmdConsulta.Parameters.AddWithValue("@pApellido", usuario.Apellido);
                    cmdConsulta.Parameters.AddWithValue("@pUsuario", usuario.Usuario);
                    cmdConsulta.Parameters.AddWithValue("@pContraseña", usuario.Contraseña);
                    cmdConsulta.Parameters.AddWithValue("@pEmail", usuario.Email);
                    cmdConsulta.Parameters.AddWithValue("@pIdTipoUsuario", usuario.IdTipoUsuario);
                    cmdConsulta.Parameters.AddWithValue("@pArchivoCv", archivoByte);
                    cmdConsulta.Parameters.AddWithValue("@pIdUsuario", usuario.IdUsuario);
                    cmdConsulta.Parameters.AddWithValue("@pCelular", usuario.Celular);
                    cmdConsulta.Parameters.AddWithValue("@pObservaciones", usuario.Observaciones);
                    cmdConsulta.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static int GuardarOfertaEmpleo(Ofertas oferta)
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.InsertarOfertas", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pTitulo", oferta.Titulo);
                    cmdConsulta.Parameters.AddWithValue("@pDescripcionOferta", oferta.DescripcionOferta);
                    cmdConsulta.Parameters.AddWithValue("@pSalario", oferta.Salario);
                    cmdConsulta.Parameters.AddWithValue("@pOfertaDesde", oferta.OfertaDesde);
                    cmdConsulta.Parameters.AddWithValue("@pOfertaHasta", oferta.OfertaHasta);
                    cmdConsulta.Parameters.AddWithValue("@pIdProfesion", oferta.IdProfesion);
                    cmdConsulta.Parameters.AddWithValue("@pIdUsuario", oferta.IdUsuario);
                    cmdConsulta.Parameters.AddWithValue("@pIdEstado", oferta.IdEstado);
                    cmdConsulta.Parameters.AddWithValue("@pIdDispositivo", oferta.IdDispositivo);
                    cmdConsulta.Parameters.AddWithValue("@pIdCiudad", oferta.IdCiudad);
                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(datosFiltros);

                    return Convert.ToInt32(datosFiltros.Tables[0].Rows[0][0]);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataSet BuscarOfertasPorId(int idOferta)
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.BuscarOfertasPorId", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pIdOferta", idOferta);
                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(datosFiltros);

                    return datosFiltros;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void ActualizarOferta(Ofertas oferta)
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.ActualizarOferta", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pTitulo", oferta.Titulo);
                    cmdConsulta.Parameters.AddWithValue("@pDescripcionOferta", oferta.DescripcionOferta);
                    cmdConsulta.Parameters.AddWithValue("@pSalario", oferta.Salario);
                    cmdConsulta.Parameters.AddWithValue("@pOfertaDesde", oferta.OfertaDesde);
                    cmdConsulta.Parameters.AddWithValue("@pOfertaHasta", oferta.OfertaHasta);
                    cmdConsulta.Parameters.AddWithValue("@pIdProfesion", oferta.IdProfesion);
                    cmdConsulta.Parameters.AddWithValue("@pIdUsuario", oferta.IdUsuario);
                    cmdConsulta.Parameters.AddWithValue("@pIdEstado", oferta.IdEstado);
                    cmdConsulta.Parameters.AddWithValue("@pIdDispositivo", oferta.IdDispositivo);
                    cmdConsulta.Parameters.AddWithValue("@pIdOferta", oferta.IdOferta);
                    cmdConsulta.Parameters.AddWithValue("@pIdCiudad", oferta.IdCiudad);
                    cmdConsulta.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static void GuardarAplicacion(int idOferta, int idUsuario, string IdDispositivo)
        {
            try
            {
                SqlConnection cn = getConnection();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.InsertarAplicacion", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pIdOferta", idOferta);
                    cmdConsulta.Parameters.AddWithValue("@pIdUsuario", idUsuario);
                    cmdConsulta.Parameters.AddWithValue("@pIdDispositivo", IdDispositivo);
                    cmdConsulta.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public static DataSet BuscarProfesionPorId(int idProfesion)
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.BuscarProfesionPorId", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pIdProfesion", idProfesion);
                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(datosFiltros);

                    return datosFiltros;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int GuardarProfesion(string Descripcion)
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.InsertarProfesion", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pDescripcion", Descripcion);
                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(datosFiltros);

                    return Convert.ToInt32(datosFiltros.Tables[0].Rows[0][0]);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static void ActualizarProfesion(Profesiones profesiones)
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.ActualizarProfesion", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pDescripcion", profesiones.Descripcion);
                    cmdConsulta.Parameters.AddWithValue("@pIdProfesion", profesiones.IdProfesion);
                    cmdConsulta.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static DataSet FiltrarOferta(int opcion, string texto)
        {
            try
            {
                SqlConnection cn = getConnection();
                DataSet datosFiltros = new DataSet();
                cn.Open();

                using (SqlCommand cmdConsulta = new SqlCommand("decasa_admin.FiltrarOfertas", cn))
                {
                    cmdConsulta.CommandType = CommandType.StoredProcedure;
                    cmdConsulta.Parameters.Clear();
                    cmdConsulta.Parameters.AddWithValue("@pOpcion", opcion);
                    cmdConsulta.Parameters.AddWithValue("@pTextoBusqueda", texto);
                    SqlDataAdapter daConsulta = new SqlDataAdapter(cmdConsulta);
                    daConsulta.Fill(datosFiltros);

                    return datosFiltros;
                }
            }
            catch (System.Exception)
            {
                return null;
            }
        }

    }
}