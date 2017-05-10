using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
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
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.ID_TIPO_DOCUMENTO,
                                     d.NOMBRE,
                                     d.DESCRIPCION,
                                     d.VERSION_ACTUAL,
                                     d.FECHA_EMISION,
                                     d.FECHA_CREACION,
                                     d.FECHA_ACTUALIZACION
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
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="version_actual"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <param name="fecha_emision"></param>
        /// <returns></returns>
        public int SetDocumento(int id_documento, int id_tipo_documento,int id_dep, string nombre, string descripcion, string version_actual, DateTime fecha_creacion, DateTime fecha_actualizacion, DateTime fecha_emision)
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
                    obj.ID_TIPO_DOCUMENTO = id_tipo_documento;
                    obj.ID_DEPARTAMENTO = id_dep;
                    obj.NOMBRE = nombre;
                    obj.DESCRIPCION = descripcion;
                    obj.VERSION_ACTUAL = version_actual;
                    obj.FECHA_CREACION = DateTime.Now;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    obj.FECHA_EMISION = fecha_emision;

                    //Agrega el objeto a la tabla.
                    Conexion.TBL_DOCUMENTO.Add(obj);
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del documento insertado
                    return obj.ID_DOCUMENTO;
                }
            }
            catch (Exception er)
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
        public int UpdateDocumento(int id_documento, int id_tipo_documento,int id_dep, string nombre, string descripcion, string version_actual, DateTime fecha_creacion, DateTime fecha_actualizacion, DateTime fecha_emision)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_DOCUMENTO obj = Conexion.TBL_DOCUMENTO.Where(x => x.ID_DOCUMENTO == id_documento).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    obj.NOMBRE = nombre;
                    obj.ID_TIPO_DOCUMENTO = id_tipo_documento;
                    obj.ID_DEPARTAMENTO = id_dep;
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

        public int UpdateVersion(int id_documento,string version_actual)
        {

            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion= new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_DOCUMENTO obj = Conexion.TBL_DOCUMENTO.Where(x => x.ID_DOCUMENTO == id_documento).FirstOrDefault();

                    //Se modifica el id de la version con la original
                    obj.VERSION_ACTUAL = version_actual;

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
            catch (Exception e)
            {
                //Si hay error, se regresa 0.
                return 0;
            }
        }

        /// <summary>
        /// Método que busca el documento de acuerdo al nombre o descripcón 
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList SearchDocumento(string keyword)
        {
            try
            {
                //Incializamos la conexión a través de EntityControlDocumentos.

                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta para obtener todos los registros,donde el nombre o la descripción  del documento debe de contener la palabra recibida.
                    var Lista = Conexion.TBL_DOCUMENTO.Where(d => d.NOMBRE.Contains(keyword) || d.DESCRIPCION.Contains(keyword)).ToList();

                    //Renornamos el resultado de la consulta.
                    return Lista;

                }
            }
            catch (Exception er)
            {
                //Si se generó algún error, retornamos un nulo.
                return null;
            }

        }

        /// <summary>
        /// Método para obtener los registros para el llenado de la tabla ControlDocumento.
        /// </summary>
        /// <returns></returns>
        public IList GetDataGrid(int idTipoDocumento,string textoBusqueda)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    if (string.IsNullOrEmpty(textoBusqueda))
                    {
                        //Realizamos la consulta, para llenar la tabla de control documento
                        var lista = (from d in Conexion.TBL_DOCUMENTO
                                     join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                     join a in Conexion.TBL_ARCHIVO on v.ID_VERSION equals a.ID_VERSION
                                     join b in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals b.ID_DEPARTAMENTO
                                     where d.ID_TIPO_DOCUMENTO == idTipoDocumento && d.VERSION_ACTUAL.Contains(v.ID_VERSION.ToString())
                                     select new
                                     {
                                         d.ID_DOCUMENTO,
                                         d.NOMBRE,
                                         FECHA_ACTUALIZACION = v.FECHA_VERSION,
                                         d.ID_DEPARTAMENTO,
                                         v.No_VERSION,
                                         v.ID_VERSION,
                                         v.NO_COPIAS,
                                         DESCRIPCION = d.DESCRIPCION,
                                         b.NOMBRE_DEPARTAMENTO,
                                         d.FECHA_EMISION
                                     }).OrderBy(x => x.ID_DOCUMENTO).Distinct().ToList();
                        return lista;
                    }
                    else
                    {
                        //Realizamos la consulta, para llenar la tabla de control documento
                        var lista = (from d in Conexion.TBL_DOCUMENTO
                                     join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                     join a in Conexion.TBL_ARCHIVO on v.ID_VERSION equals a.ID_VERSION
                                     join b in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals b.ID_DEPARTAMENTO
                                     where d.ID_TIPO_DOCUMENTO == idTipoDocumento && d.VERSION_ACTUAL.Contains(v.ID_VERSION.ToString()) && (d.NOMBRE.Contains(textoBusqueda) || d.DESCRIPCION.Contains(textoBusqueda))
                                     select new
                                     {
                                         d.ID_DOCUMENTO,
                                         d.NOMBRE,
                                         d.FECHA_ACTUALIZACION,
                                         d.ID_DEPARTAMENTO,
                                         v.No_VERSION,
                                         v.ID_VERSION,
                                         v.NO_COPIAS,
                                         DESCRIPCION = d.DESCRIPCION,
                                         b.NOMBRE_DEPARTAMENTO,
                                         d.FECHA_EMISION
                                     }).OrderBy(x => x.ID_DOCUMENTO).Distinct().ToList();
                        return lista;
                    }
                }
            }
            catch (Exception er)
            {
                //Si existe algún error, se regresa nulo.
                return null;
            }
        }

        /// <summary>
        /// Métdo para obtener el tipo de documento y el archivo.
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public IList GetTipo(int id_documento,int id_version)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta para obtener el archivo y el tipo de acuerdo al documento requerido.
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 join a in Conexion.TBL_ARCHIVO on v.ID_VERSION equals a.ID_VERSION
                                 where d.ID_DOCUMENTO == id_documento && v.ID_VERSION == id_version
                                 select new
                                 {

                                     d.ID_TIPO_DOCUMENTO,
                                     a.NOMBRE_ARCHIVO,
                                     t.TIPO_DOCUMENTO,
                                     a.ID_ARCHIVO,
                                     a.ARCHIVO,
                                     a.EXT
                                 }).ToList();
                    //se retorna la lista
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //si hay error, retorna nulo.
                return null;
            }
        }

    
      
    }
}
