using DataAccess.ServiceObjects.ControlDocumentos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_TipoDocumento
    {
        /// <summary>
        /// Método para obtener los registros de la tabla.
        /// </summary>
        /// <returns>Si encuentra un error, retorna cero.</returns>
        public IList GetTipo()
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from t in Conexion.TBL_TIPO_DOCUMENTO
                                 join d in Conexion.TBL_DOCUMENTO on t.ID_TIPO_DOCUMENTO equals d.ID_TIPO_DOCUMENTO
                                 select new
                                 {
                                  t.ID_TIPO_DOCUMENTO,
                                  t.TIPO_DOCUMENTO,
                                  t.ABREBIATURA,
                                  t.FECHA_CREACION,
                                  t.FECHA_ACTUALIZACION
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
        /// Método para insertar un registro a la tabla tipo.
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <param name="tipo_documento"></param>
        /// <param name="abreviatura"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <returns>Si encuentra un error, retorna cero.</returns>
        public int SetTipo(int id_tipo,string tipo_documento,string abreviatura,DateTime fecha_creacion,DateTime fecha_actualizacion)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    TBL_TIPO_DOCUMENTO  obj = new TBL_TIPO_DOCUMENTO();

                    //Se asiganan los valores.
                    obj.ID_TIPO_DOCUMENTO = id_tipo;
                    obj.TIPO_DOCUMENTO = tipo_documento;
                    obj.ABREBIATURA = abreviatura;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    obj.FECHA_CREACION = fecha_creacion;
                    //Agrega el objeto a la tabla.
                    Conexion.TBL_TIPO_DOCUMENTO.Add(obj);
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del usuario insertado
                    return obj.ID_TIPO_DOCUMENTO;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar un registro de la tabla TBL_tipo
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <param name="tipo_documento"></param>
        /// <param name="abreviatura"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <returns>si hay error, retorna cero,</returns>
        public int UpdateTipo(int id_tipo, string tipo_documento, string abreviatura, DateTime fecha_creacion, DateTime fecha_actualizacion)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    TBL_TIPO_DOCUMENTO obj = Conexion.TBL_TIPO_DOCUMENTO.Where(x => x.ID_TIPO_DOCUMENTO == id_tipo).FirstOrDefault();

                    //Se asiganan los valores.
                 
                    obj.TIPO_DOCUMENTO = tipo_documento;
                    obj.ABREBIATURA = abreviatura;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    obj.FECHA_CREACION = fecha_creacion;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges(); ;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla TBL_Tipo
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public int DeleteTipo(int id_tipo)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TBL_TIPO_DOCUMENTO obj = Conexion.TBL_TIPO_DOCUMENTO.Where(x => x.ID_TIPO_DOCUMENTO == id_tipo).FirstOrDefault();

                    //Se estable el estado de registro a eliminado.
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //Si hay error, se regresa 0.
                return 0;
            }
        }
    }
}
