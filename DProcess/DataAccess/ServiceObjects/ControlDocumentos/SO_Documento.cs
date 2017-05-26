using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
                                 join e in Conexion.TBL_ESTATUS_DOCUMENTO on d.ID_ESTATUS_DOCUMENTO equals e.ID_ESTATUS_DOCUMENTO
                                 join u in Conexion.Usuarios on d.ID_USUARIO equals u.Usuario
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     t.ID_TIPO_DOCUMENTO,
                                     d.NOMBRE,
                                     d.DESCRIPCION,
                                     //d.VERSION_ACTUAL,
                                     d.FECHA_EMISION,
                                     d.FECHA_CREACION,
                                     d.FECHA_ACTUALIZACION,
                                     e.ID_ESTATUS_DOCUMENTO,
                                     u.Usuario
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
        public int SetDocumento(int id_documento, int id_tipo_documento,int id_dep, string nombre, string descripcion, DateTime fecha_creacion, DateTime fecha_actualizacion, DateTime fecha_emision,
                                 int id_estatus, string id_usuario)
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
                    //obj.DESCRIPCION = descripcion;
                    //obj.VERSION_ACTUAL = version_actual;
                    obj.FECHA_CREACION = DateTime.Now;
                    //obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    //obj.FECHA_EMISION = fecha_emision;
                    obj.ID_ESTATUS_DOCUMENTO = id_estatus;
                    obj.ID_USUARIO = id_usuario;

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
        public int UpdateDocumento(int id_documento, int id_tipo_documento,int id_dep, string nombre, string descripcion, DateTime fecha_actualizacion,int id_estatus,DateTime fecha_emision)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_DOCUMENTO obj = Conexion.TBL_DOCUMENTO.Where(x => x.ID_DOCUMENTO == id_documento).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    //obj.NOMBRE = nombre;
                    obj.ID_TIPO_DOCUMENTO = id_tipo_documento;
                    obj.ID_DEPARTAMENTO = id_dep;
                    obj.DESCRIPCION = descripcion;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    obj.FECHA_EMISION = fecha_emision;
                    obj.ID_ESTATUS_DOCUMENTO = id_estatus;

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
        /// 
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="version_actual"></param>
        /// <returns></returns>
        public int UpdateVersion(int id_documento)
        {

            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion= new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_DOCUMENTO obj = Conexion.TBL_DOCUMENTO.Where(x => x.ID_DOCUMENTO == id_documento).FirstOrDefault();

                    //Se modifica el id de la version con la original
                    //obj.VERSION_ACTUAL = version_actual;

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
                                     where d.ID_TIPO_DOCUMENTO == idTipoDocumento && d.ID_ESTATUS_DOCUMENTO == 5 && v.ID_ESTATUS_VERSION == 1
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
                                     where d.ID_TIPO_DOCUMENTO == idTipoDocumento && d.ID_ESTATUS_DOCUMENTO == 5 && v.ID_ESTATUS_VERSION == 1  && (d.NOMBRE.Contains(textoBusqueda) || d.DESCRIPCION.Contains(textoBusqueda))
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

        /// <summary>
        /// Método para generar número al documento.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public string GetNumero(string nombre)
        {
            string lastNumber;
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta para obetener el último número de un documento.
                    string numero = (from d in Conexion.TBL_DOCUMENTO
                                     where d.NOMBRE.Contains(nombre)
                                     orderby d.ID_DOCUMENTO descending
                                     select d.NOMBRE).ToList().FirstOrDefault();

                    //se asigna a la variable lastNumber
                    lastNumber = numero;

                    //si no encontró ningun registro, se crea un nuevo número
                    if (string.IsNullOrEmpty(lastNumber))
                    {
                        //se le asigna al nombre el primer número.
                        lastNumber = string.Concat(nombre, "-0001");
                    }
                    else
                    {
                       //si encontro algún número
                       //Extraemos el número del registro que se obtuvo
                       string resultString = Regex.Match(lastNumber, @"\d+").Value;

                        //Sumamos uno al número 
                        int number = Int32.Parse(resultString);
                       number++;

                        //Concatenamos el nombre con el nuevo número
                        lastNumber = string.Concat(nombre,"-",number.ToString("D4"));
                    }

                }
            }
            catch (Exception)
            {
                //Si hay algún error, se retorna un nulo.
                return null;
            }
            //retornamos el número generado.
            return lastNumber;
        }

        /// <summary>
        /// Método para obtener los documentos pendientes de un usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        public IList GetDocumento_Version(string id_usuario)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 join dep in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals dep.ID_DEPARTAMENTO
                                 where d.ID_ESTATUS_DOCUMENTO == 1 & d.ID_USUARIO== id_usuario
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.ID_TIPO_DOCUMENTO,
                                     d.ID_DEPARTAMENTO,
                                     d.NOMBRE,
                                     t.TIPO_DOCUMENTO,
                                     dep.NOMBRE_DEPARTAMENTO,
                                     d.ID_ESTATUS_DOCUMENTO
                                 }).OrderBy(x => x.ID_DOCUMENTO).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {

                return null;
            }
           
        }

        /// <summary>
        /// Método que obtiene el nombre de un documento
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public IList GetNombre(int id_documento)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta para obtener el archivo y el tipo de acuerdo al documento requerido.
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 where d.ID_DOCUMENTO == id_documento 
                                 select new
                                 {
                                     d.NOMBRE
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


        public IList GetDocumentosValidar(string nombreUsuario)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 join u in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals u.Usuario
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 where v.ID_ESTATUS_VERSION == 3
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.NOMBRE,
                                     NOMBRE_USUARIO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                     t.TIPO_DOCUMENTO
                                 }).ToList();
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
