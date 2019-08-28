using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_Perfil
    {
        /// <summary>
        /// Método que retorna todos los registros de Perfiles
        /// </summary>
        /// <returns></returns>
        public IList GetAllPerfiles()
        {
            try
            {
                //Establecemos la conexión a la base de datos a través de Entity Framework.
                using (var Conexion = new EntitiesPerfiles())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var lista = (from a in Conexion.CAT_PERFIL
                                 join b in Conexion.CAT_TIPO_PERFIL on a.ID_TIPO_PERFIL equals b.ID_TIPO_PERFIL
                                 select new
                                 {
                                     a.ID_PERFIL,
                                     a.NOMBRE,
                                     a.DESCRIPCION,
                                     a.IMAGEN,
                                     b.PERFIL,
                                     a.FECHA_ACTUALIZACION,
                                     a.FECHA_CREACION,
                                     a.ID_USUARIO_CREACION,
                                     a.ID_USUARIO_ACTUALIZACION,
                                     a.ID_TIPO_PERFIL
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception er)
            {
                //Si se genero algún error, retornamos un null.
                return null;
            }
        }

        public IList GetAllPerfiles(string tipoPerfil)
        {
            try
            {
                //Establecemos la conexión a la base de datos a través de Entity Framework.
                using (var Conexion = new EntitiesPerfiles())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var lista = (from a in Conexion.CAT_PERFIL
                                 join b in Conexion.CAT_TIPO_PERFIL on a.ID_TIPO_PERFIL equals b.ID_TIPO_PERFIL
                                 where b.PERFIL == tipoPerfil
                                 select new {
                                     a.ID_PERFIL,
                                     a.NOMBRE,
                                     a.DESCRIPCION,
                                     a.IMAGEN,
                                     b.PERFIL
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception er)
            {
                //Si se genero algún error, retornamos un null.
                return null;
            }
        }

        public int SetPerfil(int idTipoPerfil, string Nombre, string Descripcion, byte[] imagen, int idUsuarioCreacion, DateTime fecha)
        {
            try
            {
                //DataSet datos = null;

                //Desing_SQL conexion = new Desing_SQL();

                //Dictionary<string, object> parametros = new Dictionary<string, object>();

                //parametros.Add("idTipoPerfil", idTipoPerfil);
                //parametros.Add("Nombre", Nombre);
                //parametros.Add("Descripcion", Descripcion);
                //parametros.Add("Imagen", imagen);
                //parametros.Add("idUsuarioCreacion", idUsuarioCreacion);

                //datos = conexion.EjecutarStoredProcedure("SP_RGP_INSERT_PERFIL", parametros);

                ////Retorna el número de elementos en la tabla.
                //return datos.Tables.Count;

                using (var Conexion = new EntitiesPerfiles())
                {
                    CAT_PERFIL perfil = new CAT_PERFIL();

                    perfil.ID_TIPO_PERFIL = idTipoPerfil;
                    perfil.NOMBRE = Nombre;
                    perfil.DESCRIPCION = Descripcion;
                    perfil.IMAGEN = imagen;
                    perfil.FECHA_ACTUALIZACION = fecha;
                    perfil.FECHA_CREACION = fecha;
                    perfil.ID_USUARIO_ACTUALIZACION = idUsuarioCreacion;
                    perfil.ID_USUARIO_CREACION = idUsuarioCreacion;

                    Conexion.CAT_PERFIL.Add(perfil);

                    Conexion.SaveChanges();

                    return perfil.ID_PERFIL;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que actualiza un registro de la tabla CAT_PERFIL.
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <param name="idTipoPerfil"></param>
        /// <param name="Nombre"></param>
        /// <param name="Descripcion"></param>
        /// <param name="imagen"></param>
        /// <param name="fechaActualizacion"></param>
        /// <returns></returns>
        public int UpdatePerfil(int idPerfil, int idTipoPerfil, string Nombre, string Descripcion, byte[] imagen,DateTime fechaActualizacion)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    CAT_PERFIL perfil = Conexion.CAT_PERFIL.Where(x => x.ID_PERFIL == idPerfil).FirstOrDefault();

                    perfil.ID_TIPO_PERFIL = idTipoPerfil;
                    perfil.NOMBRE = Nombre;
                    perfil.DESCRIPCION = Descripcion;
                    perfil.IMAGEN = imagen;
                    perfil.FECHA_ACTUALIZACION = fechaActualizacion;

                    Conexion.Entry(perfil).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public int SetPerfilArquetipo(string codigo, int idPerfil)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TR_PERFIL_ARQUETIPO tr = new TR_PERFIL_ARQUETIPO();

                    tr.CODIGO = codigo;
                    tr.ID_PERFIL = idPerfil;

                    Conexion.TR_PERFIL_ARQUETIPO.Add(tr);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene el perfil.
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <returns></returns>
        public IList GetPerfilByID(int idPerfil)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var Lista = (from a in Conexion.CAT_PERFIL
                                 join b in Conexion.CAT_TIPO_PERFIL on a.ID_TIPO_PERFIL equals b.ID_TIPO_PERFIL
                                 where a.ID_PERFIL == idPerfil
                                 select new {
                                     a.ID_PERFIL,
                                     a.NOMBRE,
                                     a.DESCRIPCION,
                                     a.IMAGEN,
                                     b.PERFIL,
                                     b.ID_TIPO_PERFIL
                                 }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene los perfiles que tiene guardados un componente.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetPerfilesComponente(string codigo)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.TR_PERFIL_ARQUETIPO
                                 where a.CODIGO == codigo
                                 select a).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int InsertNormasArquetipo(string codigo, int idNorma)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TR_NORMAS_ARQUETIPO normaCodigo = new TR_NORMAS_ARQUETIPO();

                    normaCodigo.CODIGO = codigo;
                    normaCodigo.ID_NORMA = idNorma;

                    Conexion.TR_NORMAS_ARQUETIPO.Add(normaCodigo);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int DeleteNormasArquetipo(string Codigo, int idNorma)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TR_NORMAS_ARQUETIPO normaCodigo = Conexion.TR_NORMAS_ARQUETIPO.Where(x => x.CODIGO == Codigo && x.ID_NORMA == idNorma).FirstOrDefault();

                    Conexion.Entry(normaCodigo).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList GetNormasByArquetipo(string codigo)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var Lista = (from a in Conexion.TBL_NORMAS
                                 join b in Conexion.TR_NORMAS_ARQUETIPO on a.ID_NORMA equals b.ID_NORMA
                                 where b.CODIGO == codigo
                                 select a).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}