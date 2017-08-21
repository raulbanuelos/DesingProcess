using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_Validacion
    {
        /// <summary>
        ///  Método que obtiene las relaciones de la tabla TR_Validacion_tipo_documento dependiento del tipo de documento
        /// </summary>
        /// <param name="id_tipoDocumento"></param>
        /// <returns></returns>
        public IList GetTR_Validacion(int id_tipoDocumento)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from v in Conexion.TBL_VALIDACION_DOCUMENTO
                                 join tr in Conexion.TR_VALIDACION_TIPO_DOCUMENTO on v.ID_VALIDACION_DOCUMENTO equals tr.ID_VALIDACION_DOCUMENTO
                                 where tr.ID_TIPO_DOCUMENTO == id_tipoDocumento
                                 select new
                                 {
                                     v.ID_VALIDACION_DOCUMENTO,
                                     v.VALIDACION_DOCUMENTO,
                                     v.VALIDACION_DESCRIPCION,
                                     v.FECHA_CREACION,
                                     tr.ID_VALIDACION_TIPO_DOCUMENTO
                                 }).ToList();
                    //se retorna la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay algún error, se retorna un nulo.
                return null;
            }
        }      

        /// <summary>
        /// Método que obtiene todos los registros de la tabla TBL_VAlidacion_documento
        /// </summary>
        /// <returns></returns>
        public IList GetValidaciones()
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from v in Conexion.TBL_VALIDACION_DOCUMENTO
                                 orderby v.ID_VALIDACION_DOCUMENTO ascending
                                 select new
                                 {
                                     v.ID_VALIDACION_DOCUMENTO,
                                     v.VALIDACION_DESCRIPCION,
                                     v.VALIDACION_DOCUMENTO,
                                     v.FECHA_CREACION
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception e)
            {
                //Si hay error regresa nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene las relaciones de una validación de documento
        /// </summary>
        /// <param name="id_val"></param>
        /// <returns></returns>
        public IList GetR_Val_Tipo(int id_val)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from v in Conexion.TBL_VALIDACION_DOCUMENTO
                                 join tr in Conexion.TR_VALIDACION_TIPO_DOCUMENTO on v.ID_VALIDACION_DOCUMENTO equals tr.ID_VALIDACION_DOCUMENTO
                                 where tr.ID_VALIDACION_DOCUMENTO == id_val
                                 select new
                                 {
                                     tr.ID_VALIDACION_TIPO_DOCUMENTO
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa nulo
                return null;
            }
        }

        /// <summary>
        /// Método que agrega un registro a la tabla de validaciones
        /// </summary>
        /// <param name="validacion_documento"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public int SetValidacion(string validacion_documento,string descripcion)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TBL_VALIDACION_DOCUMENTO obj = new TBL_VALIDACION_DOCUMENTO();
                    //Asigamos los valores
                    obj.VALIDACION_DOCUMENTO = validacion_documento;
                    obj.VALIDACION_DESCRIPCION = descripcion;
                    obj.FECHA_ACTUALIZACION = DateTime.Now;
                    obj.FECHA_CREACION = DateTime.Now;

                    //Añadimos el objeto 
                    Conexion.TBL_VALIDACION_DOCUMENTO.Add(obj);
                    //Guardamos los cambios
                    Conexion.SaveChanges();
                    //Retornamos el id del objeto agregado
                    return obj.ID_VALIDACION_DOCUMENTO;

                }
            }
            catch (Exception er)
            {
                //Si hay error regresamos cero
                return 0;
            }
        }

        /// <summary>
        /// Método que agrega un registro a la tabla de relaciones
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <param name="id_validacion"></param>
        /// <returns></returns>
        public int SetRelacion(int id_tipo, int id_validacion)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TR_VALIDACION_TIPO_DOCUMENTO obj = new TR_VALIDACION_TIPO_DOCUMENTO();
                    //Asigamos los valores
                    obj.ID_VALIDACION_DOCUMENTO = id_validacion;
                    obj.ID_TIPO_DOCUMENTO = id_tipo;

                    //Añadimos el objeto 
                    Conexion.TR_VALIDACION_TIPO_DOCUMENTO.Add(obj);
                    //Guardamos los cambios
                    Conexion.SaveChanges();

                    //Retornamos el id del objeto agregado
                    return obj.ID_VALIDACION_TIPO_DOCUMENTO;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa cero
                return 0;
            }
        }

       /// <summary>
       /// Método que elimina un registro de la tabla de validaciones
       /// </summary>
       /// <param name="id_validacion"></param>
       /// <returns></returns>
        public int DeleteValidacion(int id_validacion)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TBL_VALIDACION_DOCUMENTO obj = Conexion.TBL_VALIDACION_DOCUMENTO.Where(x => x.ID_VALIDACION_DOCUMENTO == id_validacion).FirstOrDefault();

                    //Guaramos el estado
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //Si hay error regresamos cero
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro de la tabla TR_VALIDACION_TIPO_DOCUMENTO
        /// </summary>
        /// <param name="id_val_tipo"></param>
        /// <returns></returns>
        public int DeleteRelacion_Validacion(int id_val_tipo)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TR_VALIDACION_TIPO_DOCUMENTO obj = Conexion.TR_VALIDACION_TIPO_DOCUMENTO.Where(x => x.ID_VALIDACION_TIPO_DOCUMENTO == id_val_tipo).FirstOrDefault();

                    //Eliminamos el registro
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error regresamos cero
                return 0;
            }
        }

        
        /// <summary>
        /// Método que busca si existe un registro de validacion de acuerdo al tipo
        /// </summary>
        /// <param name="id_validacion"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public int SearchValidacion(int id_validacion, int id_tipo)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una VARIABLE, para retornar el resultado.
                    var id_validacion_doc = (from tr in Conexion.TR_VALIDACION_TIPO_DOCUMENTO
                                             where tr.ID_VALIDACION_DOCUMENTO == id_validacion && tr.ID_TIPO_DOCUMENTO == id_tipo
                                             select tr.ID_VALIDACION_TIPO_DOCUMENTO).FirstOrDefault();
                    //Retornamos el id
                    return id_validacion_doc;
                }

            }
            catch (Exception)
            {
                //Si hay error regresamos cero
                return 0;
            }
        }

        /// <summary>
        /// Método que busca si existe un registro de validacion de acuerdo al tipo
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public int GetID_Validacion(string validacion)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una VARIABLE, para retornar el resultado.
                    var id = (from v in Conexion.TBL_VALIDACION_DOCUMENTO
                              where v.VALIDACION_DOCUMENTO.Equals(validacion)
                              select v.ID_VALIDACION_DOCUMENTO).FirstOrDefault();

                    //Retornamos el id
                    return id;
                }
            }
            catch (Exception e)
            {
                //Si hay error regresamos cero
                return 0;
            }
        }
    }
}
