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
                                 join us in Conexion.Usuarios on v.ID_USUARIO_AUTORIZO equals us.Usuario
                                 select new
                                 {
                                     v.ID_VERSION,
                                     ID_USUARIO_ELABORO = u.Usuario,
                                     ID_USUARIO_AUTORIZO=us.Usuario,
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
        public int SetVersion(string id_usuario,string id_usuario_autorizo,int id_documento,string no_version,DateTime fecha,
                                int no_copias,int id_estatus,string descripcion)
        {

            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    TBL_VERSION obj = new TBL_VERSION();

                    //Se asiganan los valores.
                    obj.ID_USUARIO_ELABORO = id_usuario;
                    obj.ID_DOCUMENTO = id_documento;
                    obj.No_VERSION = no_version;
                    obj.NO_COPIAS = no_copias;
                    obj.FECHA_VERSION = fecha;
                    obj.ID_USUARIO_AUTORIZO = id_usuario_autorizo;
                    obj.ID_ESTATUS_VERSION = id_estatus;
                    obj.DESCRIPCION = descripcion;

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
        public int UpdateVersion(int id_version, string id_usuario,string id_usuario_autorizo, int id_documento, 
                                 string no_version, DateTime fecha, int no_copias,int id_estatus, string descripcion)
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
                    obj.ID_USUARIO_AUTORIZO = id_usuario_autorizo;
                    obj.ID_DOCUMENTO = id_documento;
                    obj.No_VERSION = no_version;
                    obj.FECHA_VERSION = fecha;
                    obj.NO_COPIAS = no_copias;
                    obj.ID_ESTATUS_VERSION = id_estatus;
                    obj.DESCRIPCION = descripcion;

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

        /// <summary>
        /// Método para obetener el id del usuario del id que elaboró y autorizó de la version.
        /// </summary>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public IList GetUsuario(int id_version)
        {
            try
            {
                //Relizamos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y e resultado lo guardamos en una variable local.
                    var usuario = (from v in Conexion.TBL_VERSION
                                           join u in Conexion.Usuarios on v.ID_USUARIO_AUTORIZO equals u.Usuario 
                                           join us in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals us.Usuario
                                           where v.ID_VERSION == id_version
                                           select new {
                                               v.ID_USUARIO_ELABORO,
                                               v.ID_USUARIO_AUTORIZO,
                                               USUARIO_AUTORIZO = u.Nombre + " "  +u.APaterno,
                                               USUARIO_ELABORO = us.Nombre + " " + us.APaterno
                                           }).ToList();

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

        /// <summary>
        /// Método para obtener el último número de la versión de un documento.
        /// </summary>
        /// <returns></returns>
        public string GetLastVersion(int id_documento)
        {
            //Declaramos una variable, que retornara el último código agregado
            string version;

            try
            {
                //Se establece la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se ordena de mayor a menor el código para obtener el primer valor,
                    //en este caso la última versión del documento correspondiente.
                    var last = (from v in Conexion.TBL_VERSION
                                join d in Conexion.TBL_DOCUMENTO on v.ID_DOCUMENTO equals d.ID_DOCUMENTO
                                where v.ID_DOCUMENTO== id_documento
                                orderby v.No_VERSION descending
                                select v.No_VERSION).First();

                    //Asignamos el resultado obtenido a la variable local.
                    version = last;
                }
            }
            catch (Exception)
            {
                //Si hubo algún error retornamos una cadena vacía.
                return string.Empty;
            }
            //Retornamos el valor.
            return version;

        }

        /// <summary>
        /// Método para obtener el id de la versión anterior de la la versión actual
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public int GetLastVersion_Id(int id_documento,int idVersion)
        {
            //Declaramos una variable, que retornara el último código agregado
            int id_version;

            try
            {
                //Se establece la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se ordena de mayor a menor el código para obtener el primer valor,
                    //en este caso la última versión del documento correspondiente.
                    var last = (from v in Conexion.TBL_VERSION
                                join d in Conexion.TBL_DOCUMENTO on v.ID_DOCUMENTO equals d.ID_DOCUMENTO
                                where v.ID_DOCUMENTO == id_documento & v.ID_VERSION != idVersion
                                orderby v.ID_VERSION descending
                                select v.ID_VERSION).First();

                    //Asignamos el resultado obtenido a la variable local.
                    id_version = last;

                }
            }
            catch (Exception er)
            {
                //Si hubo algún error retornamos una cadena vacía.
                return 0;
            }
            //Retornamos el valor.
            return id_version;

        }

        /// <summary>
        /// Método que obtiene el número de versión con el id de la versión
        /// </summary>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public string GetNum_Version(int id_version)
        {
            try
            {
                //Se establece la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se obtiene el número de la versión
                    var numero = (from v in Conexion.TBL_VERSION
                                  where v.ID_VERSION == id_version
                                  select v.No_VERSION).First();
                    //Retornamos el valor.
                    return numero;
                }
            }
            catch (Exception)
            {
                //Si hubo algún error retornamos una cadena vacía.
                return null;
            }
        }

        /// <summary>
        /// Método para validar si la versión recibida existe.
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="no_version"></param>
        /// <returns></returns>
        public int  ValidateVersion(int id_documento,string no_version)
        {
            try
            {
                //Se establece la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {

                    //Obtenemos el id de la versión.
                    var version = (from v in Conexion.TBL_VERSION
                                   where v.ID_DOCUMENTO == id_documento & v.No_VERSION == no_version
                                   select v.ID_VERSION).ToList().FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return version;
                }
            }
            catch (Exception er)
            {

                return 0;
            }
        }

        /// <summary>
        /// Método que retorna el id de todas las versiones de un documento.
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public IList Versiones(int id_documento)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from v in Conexion.TBL_VERSION
                                 where v.ID_DOCUMENTO== id_documento
                                 select new
                                 {
                                     v.ID_VERSION,
                                     v.No_VERSION
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
        /// Método para obtener los archivos de una versión
        /// </summary>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public IList GetArchivos(int id_version)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from a in Conexion.TBL_ARCHIVO
                                 where a.ID_VERSION== id_version
                                 select new
                                 {
                                     a.ID_ARCHIVO,
                                     a.NOMBRE_ARCHIVO,
                                     a.EXT,
                                     a.ARCHIVO
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
        /// M+etodo para obtener la versión de un documento que no esté liberado u obsoleto
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public IList GetStatus(int id_documento)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta, el resultado se guarda en una lista
                    var version = (from v in Conexion.TBL_VERSION
                                   join e in Conexion.TBL_ESTATUS_VERSION on v.ID_ESTATUS_VERSION equals e.ID_ESTATUS_VERSION
                                   where v.ID_DOCUMENTO == id_documento & v.ID_ESTATUS_VERSION != 1 & v.ID_ESTATUS_VERSION != 2
                                   select new
                                   {
                                       v.ID_VERSION,
                                       v.No_VERSION,
                                       e.ESTATUS_VERSION
                                   }).ToList();
                    //Retornamos la lista
                    return version;
                }
            }
            catch (Exception er)
            {
                //Si hay error, se regresa nulo
                return null;
            }
        }

        /// <summary>
        /// Método para actualizar el estatus de una versión.
        /// </summary>
        /// <param name="id_version"></param>
        /// <param name="id_estatus"></param>
        /// <returns></returns>
        public int UpdateEstatus_Version(int id_version, int id_estatus)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_VERSION obj = Conexion.TBL_VERSION.Where(x => x.ID_VERSION== id_version).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    obj.ID_ESTATUS_VERSION = id_estatus;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception e)
            {
                //Si encuentra error devuelve cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene una lista de las versiones liberadas de un documento correspondiente
        /// </summary>
        /// <param name="id_doc"></param>
        /// <returns></returns>
        public IList GetVersiones(int id_doc)
        {
            try
            {
                //Se establece la conexion 
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta, el resultado se guarda en una lista
                    var ListaVersion = (from d in Conexion.TBL_DOCUMENTO
                                        join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                        join u in Conexion.Usuarios on v.ID_USUARIO_AUTORIZO equals u.Usuario
                                        join us in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals us.Usuario
                                        join a in Conexion.TBL_ARCHIVO on v.ID_VERSION equals a.ID_VERSION
                                        where d.ID_DOCUMENTO == id_doc && (v.ID_ESTATUS_VERSION==1 | v.ID_ESTATUS_VERSION==2)
                                        select new
                                        {
                                            v.ID_VERSION,
                                            v.No_VERSION,
                                            v.FECHA_VERSION,
                                            v.NO_COPIAS,
                                            v.DESCRIPCION,
                                            USUARIO_AUTORIZO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                            USUARIO_ELABORO = us.Nombre + " " + us.APaterno + " " + us.AMaterno,
                                        }).OrderBy(x => x.ID_VERSION).Distinct().ToList();

                    //Retornamos la lista
                    return ListaVersion;
                }

            }
            catch (Exception er)
            {
                //Si hay error, se regresa nulo
                return null;
            }
        }

    }
}
