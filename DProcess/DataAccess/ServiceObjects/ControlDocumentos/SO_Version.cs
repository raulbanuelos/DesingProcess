using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
   public  class SO_Version
    {
        /// <summary>
        /// Método para obetener todos los registros de la tabla TBL_Version
        /// </summary>
        /// <returns>Retorna null, si hay error.</returns>
        public IList GetVersion()
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from v in Conexion.TBL_VERSION
                                 join d in Conexion.TBL_DOCUMENTO on v.ID_DOCUMENTO equals d.ID_DOCUMENTO
                                 join u in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals u.Usuario
                                 select new
                                 {
                                     v.ID_VERSION,
                                     ID_USUARIO_ELABORO = u.Usuario,
                                     ID_DOCUMENTO = d.ID_DOCUMENTO,
                                     v.No_VERSION,
                                     v.FECHA_VERSION,
                                     v.NO_COPIAS
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
        /// Método para insertar un registro a la tabla TBL_Version
        /// </summary>
        /// <param name="id_version"></param>
        /// <param name="id_usuario"></param>
        /// <param name="id_documento"></param>
        /// <param name="no_version"></param>
        /// <param name="fecha"></param>
        /// <param name="no_copias"></param>
        /// <returns>Si hay algún error, retorna cero.</returns>
        public int SetVersion(int id_version,string id_usuario,int id_documento,string no_version,DateTime fecha,int no_copias)
        {

            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    TBL_VERSION obj = new TBL_VERSION();

                    //Se asiganan los valores.
                   obj.ID_VERSION = id_version;
                    obj.ID_USUARIO_ELABORO = id_usuario;
                    obj.ID_DOCUMENTO = id_documento;
                    obj.No_VERSION = no_version;
                    obj.NO_COPIAS = no_copias;
                    obj.FECHA_VERSION = fecha;

                    //Agrega el objeto a la tabla.
                    Conexion.TBL_VERSION.Add(obj);
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del usuario insertado
                    return obj.ID_VERSION;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar un registro en la tabla TBL_Version.
        /// </summary>
        /// <param name="id_version"></param>
        /// <param name="id_usuario"></param>
        /// <param name="id_documento"></param>
        /// <param name="no_version"></param>
        /// <param name="fecha"></param>
        /// <param name="no_copias"></param>
        /// <returns></returns>
        public int UpdateVersion(int id_version, string id_usuario, int id_documento, string no_version, DateTime fecha, int no_copias)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_VERSION obj = Conexion.TBL_VERSION.Where(x => x.ID_VERSION == id_version).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    obj.ID_USUARIO_ELABORO = id_usuario;
                    obj.ID_DOCUMENTO = id_documento;
                    obj.No_VERSION = no_version;
                    obj.FECHA_VERSION = fecha;
                    obj.NO_COPIAS = no_copias;

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
        /// Método para eliminar un registro de la tabla.
        /// </summary>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public int DeleteVersion(int id_version)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TBL_VERSION archivo = Conexion.TBL_VERSION.Where(x => x.ID_VERSION == id_version).FirstOrDefault();

                    //Se estable el estado de registro a eliminado.
                    Conexion.Entry(archivo).State = EntityState.Deleted;

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

        public string GetUsuario(int id_version)
        {
            try
            {
                //Relizamos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y e resultado lo guardamos en una variable local.
                    string usuario = (from v in Conexion.TBL_VERSION
                                           where v.ID_VERSION == id_version
                                           select v.ID_USUARIO_ELABORO).ToList().FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return usuario;
                }
            }
            catch (Exception)
            {
                //Si se genera un error retornamos un cero.
                return null;
            }
        }
    }
}
