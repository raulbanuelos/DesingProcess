using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{

    public class SO_Documento
    {
        /// <summary>
        /// Método para obtener todos los registro de la tabla.
        /// </summary>
        /// <returns></returns>
        public IList GetDocumento()
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join u in Conexion.Usuarios on d.ID_USUARIO_REVISO equals u.Usuario
                                 select new
                                 {
                                     d.ID_DOCUMENTO,d.ID_USUARIO_REVISO,d.NOMBRE,d.DESCRIPCION,d.VERSION_ACTUAL,
                                     d.FECHA_EMISION,d.FECHA_CREACION,d.FECHA_ACTUALIZACION                                     
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
        /// Método para insertar un registro en la tabla TBL_Documento.
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="id_usuario"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="version_actual"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <param name="fecha_emision"></param>
        /// <returns></returns>
        public int SetDocumento(int id_documento,string id_usuario,string nombre,string descripcion,string version_actual,DateTime fecha_creacion,DateTime fecha_actualizacion,DateTime fecha_emision)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    TBL_DOCUMENTO obj = new TBL_DOCUMENTO();

                    //Se asiganan los valores.
                    obj.ID_DOCUMENTO = id_documento;
                    obj.ID_USUARIO_REVISO = id_usuario;
                    obj.NOMBRE = nombre;
                    obj.DESCRIPCION = descripcion;
                    obj.VERSION_ACTUAL = version_actual;
                    obj.FECHA_CREACION = fecha_creacion;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    obj.FECHA_EMISION = fecha_emision;

                    //Agrega el objeto a la tabla.
                    Conexion.TBL_DOCUMENTO.Add(obj);
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del usuario insertado
                    return obj.ID_DOCUMENTO;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        /// <summary>
        /// Método para actualizar un registro de la tabla TBL_documento
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="id_usuario"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="version_actual"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <param name="fecha_emision"></param>
        /// <returns></returns>
        public int UpdateDocumento(int id_documento, string id_usuario, string nombre, string descripcion, string version_actual, DateTime fecha_creacion, DateTime fecha_actualizacion, DateTime fecha_emision)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_DOCUMENTO obj = Conexion.TBL_DOCUMENTO.Where(x => x.ID_DOCUMENTO == id_documento).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    obj.ID_USUARIO_REVISO = id_usuario;
                    obj.NOMBRE = nombre;
                    obj.DESCRIPCION = descripcion;
                    obj.VERSION_ACTUAL = version_actual;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    obj.FECHA_CREACION = fecha_creacion;
                    obj.FECHA_EMISION = fecha_emision;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si encuentra error devuelve cero.
                return 0;
            }
        }
        
        /// <summary>
        /// Método para eliminar un registro de la tabla TBL_documento.
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public int DeleteDocumento(int id_documento)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TBL_DOCUMENTO obj = Conexion.TBL_DOCUMENTO.Where(x => x.ID_DOCUMENTO == id_documento).FirstOrDefault();

                    //Se estable el estado de registro a eliminado.
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error, se regresa 0.
                return 0;
            }
        }
    }
}
