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
        /// 
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
                                     v.FECHA_CREACION
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
        /// 
        /// </summary>
        /// <param name="id_tipoDocumento"></param>
        /// <returns></returns>
        public IList GetValidacion(int id_tipoDocumento)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from v in Conexion.TBL_VALIDACION_DOCUMENTO
                                 join tr in Conexion.TR_VALIDACION_TIPO_DOCUMENTO on v.ID_VALIDACION_DOCUMENTO equals tr.ID_VALIDACION_DOCUMENTO
                                 where tr.ID_TIPO_DOCUMENTO != id_tipoDocumento
                                 select new
                                 {
                                     v.ID_VALIDACION_DOCUMENTO,
                                     v.VALIDACION_DOCUMENTO,
                                     v.VALIDACION_DESCRIPCION,
                                     v.FECHA_CREACION
                                 }).Distinct().ToList();
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
        /// 
        /// </summary>
        /// <returns></returns>
        public IList GetValidaciones()
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var Lista = (from v in Conexion.TBL_VALIDACION_DOCUMENTO
                                 select new
                                 {
                                     v.ID_VALIDACION_DOCUMENTO,
                                     v.VALIDACION_DESCRIPCION,
                                     v.VALIDACION_DOCUMENTO,
                                     v.FECHA_CREACION
                                 }).ToList();

                    return Lista;
                }
            }
            catch (Exception e)
            {

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="validacion_documento"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public int SetValidacion(string validacion_documento,string descripcion)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TBL_VALIDACION_DOCUMENTO obj = new TBL_VALIDACION_DOCUMENTO();

                    obj.VALIDACION_DOCUMENTO = validacion_documento;
                    obj.VALIDACION_DESCRIPCION = descripcion;
                    obj.FECHA_ACTUALIZACION = DateTime.Now;
                    obj.FECHA_CREACION = DateTime.Now;

                    Conexion.TBL_VALIDACION_DOCUMENTO.Add(obj);

                    Conexion.SaveChanges();

                    return obj.ID_VALIDACION_DOCUMENTO;

                }
            }
            catch (Exception er)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <param name="id_validacion"></param>
        /// <returns></returns>
        public int SetRelacion(int id_tipo, int id_validacion)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TR_VALIDACION_TIPO_DOCUMENTO obj = new TR_VALIDACION_TIPO_DOCUMENTO();

                    obj.ID_VALIDACION_DOCUMENTO = id_validacion;
                    obj.ID_TIPO_DOCUMENTO = id_tipo;

                    Conexion.TR_VALIDACION_TIPO_DOCUMENTO.Add(obj);
                    Conexion.SaveChanges();

                    return obj.ID_VALIDACION_TIPO_DOCUMENTO;
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="id_validacion"></param>
       /// <returns></returns>
        public int DeleteValidacion(int id_validacion)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TBL_VALIDACION_DOCUMENTO obj = Conexion.TBL_VALIDACION_DOCUMENTO.Where(x => x.ID_VALIDACION_DOCUMENTO == id_validacion).FirstOrDefault();

                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception e)
            {

                return 0;
            }
        }

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_validacion"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public int SearchValidacion(int id_validacion, int id_tipo)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var id_validacion_doc = (from tr in Conexion.TR_VALIDACION_TIPO_DOCUMENTO
                                             where tr.ID_VALIDACION_DOCUMENTO == id_validacion && tr.ID_TIPO_DOCUMENTO == id_tipo
                                             select tr.ID_VALIDACION_TIPO_DOCUMENTO).FirstOrDefault();

                    return id_validacion_doc;
                }

            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public int GetID_Validacion(string validacion)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var id = (from v in Conexion.TBL_VALIDACION_DOCUMENTO
                              where v.VALIDACION_DOCUMENTO == validacion
                              select v.ID_VALIDACION_DOCUMENTO).FirstOrDefault();

                    return id;
                }
            }
            catch (Exception e)
            {

                return 0;
            }
        }
    }
}
