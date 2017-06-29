using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_Archivo
    {
        /// <summary>
        /// Método para obtener los registros de la tabla TBL_archivos.
        /// </summary>
        /// <returns>Retorna nulo si existe algún error.</returns>
        public IList GetArchivo()
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from a in Conexion.TBL_ARCHIVO
                                 join v in Conexion.TBL_VERSION on a.ID_VERSION equals v.ID_VERSION
                                 select new
                                 {
                                     a.ID_ARCHIVO,
                                     ID_VERSION = v.ID_VERSION,
                                     a.ARCHIVO,
                                     a.EXT,
                                     a.NOMBRE_ARCHIVO
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
        /// Método par insertar un registro en la tabla TBL_archivo.
        /// </summary>
        /// <param name="id_archivo"></param>
        /// <param name="id_version"></param>
        /// <param name="archivo"></param>
        /// <param name="ext"></param>
        /// <returns>Retorna cero si hay error.</returns>
        public int UpdateArchivo(int id_archivo, int id_version, byte[] archivo, string ext,string nombre)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_ARCHIVO obj = Conexion.TBL_ARCHIVO.Where(x => x.ID_ARCHIVO == id_archivo).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    obj.ID_VERSION = id_version;
                    obj.ARCHIVO = archivo;
                    obj.EXT = ext;
                    obj.NOMBRE_ARCHIVO = nombre;

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
        /// <param name="id_archivo"></param>
        /// <returns>Si hay algún error, retorna cero.</returns>
        public int DeleteArchivo(int id_archivo)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TBL_ARCHIVO archivo = Conexion.TBL_ARCHIVO.Where(x => x.ID_ARCHIVO == id_archivo).FirstOrDefault();

                    //Se estable el estado de registro a eliminado.
                    Conexion.Entry(archivo).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error, regresa 0.
                return 0;
            }
        }

        /// <summary>
        /// Método para buscar el archivo de acuerdo a la extensión
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList SearchArchivo(string keyword)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se obtiene la lista de la búsqueda,donde la extensión debe contener la palabra recibida.
                    var Lista = Conexion.TBL_ARCHIVO.Where(a => a.EXT.Contains(keyword)).ToList();

                    //Regresa el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception e)
            {
                //Si hay error, retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método para obtener el archivo y la extensión del id recibido.
        /// </summary>
        /// <param name="id_archivo"></param>
        /// <returns></returns>
        public IList GetByte(int id_archivo)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //obtenemos los valores del id que se recibió como parámetro.
                    var lista = (from a in Conexion.TBL_ARCHIVO
                                 where a.ID_ARCHIVO == id_archivo
                                 select new
                                 {
                                     a.ARCHIVO,
                                     a.EXT
                                 }).ToList();
                    //retornamos la lista.
                    return lista;
                }
            }
            catch (Exception er)
            {
                //Si hay algún error, retornamos nulo.
                return null;
            }
        }
        
        /// <summary>
        /// Método para insertar un registro a la tabla archivo.
        /// </summary>
        /// <param name="version"></param>
        /// <param name="archivo"></param>
        /// <param name="ext"></param>
        /// <returns></returns>
        public Task<int> SetArchivo(int version, byte[] archivo, string ext,string nombre)
        {
            return Task.Run(() =>
            {
                try
                {
                    DataSet datos = null;
                    //Se crea conexion a la BD.
                    Desing_SQL conexion = new Desing_SQL();

                    //Se inicializa un dictionario que contiene propiedades de tipo string y un objeto.
                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    //se agregan el nombre y el objeto de los parámetros.
                    parametros.Add("version", version);
                    parametros.Add("archivo", archivo);
                    parametros.Add("ext", ext);
                    parametros.Add("nombre", nombre);

                    //se ejecuta el procedimiento y se mandan los parámetros añadidos anteriormente.
                    datos = conexion.EjecutarStoredProcedure("Set_Archivo", parametros);

                    //Retorna el número de elementos en la tabla.
                    return datos.Tables.Count;
                }
                catch (Exception e)
                {
                    return 0;
                }
            });
        }

        /// <summary>
        /// Método para validar si existe el archivo
        /// </summary>
        /// <param name="id_archivo"></param>
        /// <returns></returns>
        public int ValidateArchivo(int id_archivo)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var archivo = (from v in Conexion.TBL_ARCHIVO
                                   where v.ID_ARCHIVO == id_archivo
                                   select v.ID_ARCHIVO).ToList().FirstOrDefault();

                    return archivo;
                }
            }
            catch (Exception)
            {
                //Si hay algún error, retornamos nulo.
                return 0;
            }
        }
    }
}