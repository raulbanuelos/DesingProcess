using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_RecursoTipoDocumento
    { 
        /// <summary>
        /// Método que obtiene todos los documentos (recursos) de un tipo de documento.
        /// </summary>
        /// <param name="idTipoDocumento"></param>
        /// <returns></returns>
        public IList GetArchivos(int idTipoDocumento)
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y le resultado lo guardamos en una lista anónima.
                    var Lista = (from a in Conexion.TBL_RECURSO_TIPO_DOCUMENTO
                                 where a.ID_TIPO_DOCUMENTO == idTipoDocumento
                                 select a).ToList();

                    //Retornamos el resultado de la lista.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que inserta un registro en la tabla Recurso tipo documento.
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="descripcion"></param>
        /// <param name="ext"></param>
        /// <param name="nombreArchivo"></param>
        /// <param name="idTipoDocumento"></param>
        /// <returns></returns>
        public int Insert(byte[] archivo, string descripcion, string ext, string nombreArchivo, int idTipoDocumento)
        {
            try
            {
                //Establecemos la conexión con Entity Framework.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Declaramos un objeto el cual será el que insertemos en la tabla.
                    TBL_RECURSO_TIPO_DOCUMENTO recurso = new TBL_RECURSO_TIPO_DOCUMENTO();

                    //Mapeamos los valores recibidos a las propiedades del objeto.
                    recurso.ARCHIVO = archivo;
                    recurso.DESCRIPCION = descripcion;
                    recurso.EXT = ext;
                    recurso.NOMBRE_ARCHVO = nombreArchivo;
                    recurso.ID_TIPO_DOCUMENTO = idTipoDocumento;

                    //Agregamos el objeto.
                    Conexion.TBL_RECURSO_TIPO_DOCUMENTO.Add(recurso);

                    //Guardamos los cambios y retornamos el no. de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro de la tabla recurso tipo documento.
        /// </summary>
        /// <param name="idRecursoDocumento"></param>
        /// <returns></returns>
        public int Delete(int idRecursoDocumento)
        {
            try
            {
                //Establacemos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Obtenemos el registro que se requiere eliminar.
                    TBL_RECURSO_TIPO_DOCUMENTO recurso = Conexion.TBL_RECURSO_TIPO_DOCUMENTO.Where(x => x.ID_RECURSO_TIPO_DOCUMENTO == idRecursoDocumento).FirstOrDefault();

                    //Se establece el estado de registro a eliminado.
                    Conexion.Entry(recurso).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos un cero.
                return 0;
            }
        }
    }
}
