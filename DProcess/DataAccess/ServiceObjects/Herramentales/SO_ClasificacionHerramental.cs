using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.ServiceObjects.Herramentales
{
    public class SO_ClasificacionHerramental
    {
        #region Métodos

        /// <summary>
        /// Método que inserta una clasificacion en la base de datos.
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="unidadMedida"></param>
        /// <param name="costo"></param>
        /// <param name="cantidadUtilizar"></param>
        /// <param name="vidaUtil"></param>
        /// <param name="verificacionAnual"></param>
        /// <param name="cotasRevisar"></param>
        /// <param name="objetoXML"></param>
        /// <param name="tablaDetalles"></param>
        /// <param name="fechaModificacion"></param>
        /// <returns>ID de la clasificación que se insertó, si se registro algún problema retorna un 0</returns>
        public int SetClasificacionHerramental(string descripcion,string unidadMedida,double costo,int cantidadUtilizar,int vidaUtil,bool verificacionAnual,string cotasRevisar,string objetoXML,string tablaDetalles,DateTime fechaModificacion)
        {
            try
            {
                //Incializamos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesHerramentales())
                {
                    //Declaramos un objeto de tipo ClasificacionHerramental que será el que contenga la información para insertarlo.
                    ClasificacionHerramental obj = new ClasificacionHerramental();

                    //Mapeamos los valores de los parámetros en cada una de las propiedades correspondientes del objeto.
                    obj.CantidadUtilizar = cantidadUtilizar;
                    obj.Costo = costo;
                    obj.Descripcion = descripcion;
                    obj.FechaModificacion = fechaModificacion;
                    obj.ListaCotasRevisar = cotasRevisar;
                    obj.ObjetoXML = objetoXML;
                    obj.TablaDetalles = tablaDetalles;
                    obj.UnidadMedida = unidadMedida;
                    obj.VerificacionAnual = verificacionAnual;
                    obj.VidaUtil = vidaUtil;

                    //Insertamos el objeto a la base de datos.
                    Conexion.ClasificacionHerramental.Add(obj);

                    //Guardamos los cambios.
                    Conexion.SaveChanges();

                    //Retornamos el id que se acaba de insertar.
                    return obj.idClasificacion;
                }
            }
            catch (Exception)
            {
                //Si se registró algún error, retornamos un 0.
                return 0;
            }
        }

        /// <summary>
        /// Método que actualiza los valores de un registro en la tabla ClasificacionHerramental.
        /// </summary>
        /// <param name="descripcion"></param>
        /// <param name="unidadMedida"></param>
        /// <param name="costo"></param>
        /// <param name="cantidadUtilizar"></param>
        /// <param name="vidaUtil"></param>
        /// <param name="verificacionAnual"></param>
        /// <param name="cotasRevisar"></param>
        /// <param name="objetoXML"></param>
        /// <param name="tablaDetalles"></param>
        /// <param name="fechaModificacion"></param>
        /// <param name="idClasificacion">Id de la clasificación que se requiere actualizar.</param>
        /// <returns>Número de registros que se afectaron en la actualización. Retorna un 0 si no se afectó ninguno o hubo algún error.</returns>
        public int UpdateClasificacionHerramental(string descripcion, string unidadMedida, double costo, int cantidadUtilizar, int vidaUtil, bool verificacionAnual, string cotasRevisar, string objetoXML, string tablaDetalles, DateTime fechaModificacion, int idClasificacion)
        {
            try
            {
                //Incializamos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesHerramentales())
                {
                    //Obtenemos el objeto que se requiere modificar.
                    ClasificacionHerramental clasificacion = Conexion.ClasificacionHerramental.Where(x => x.idClasificacion == idClasificacion).FirstOrDefault();

                    //Mapeamos los valores de los parámetros recibidos en las propiedades correspondientes del objeto.
                    clasificacion.ListaCotasRevisar = cotasRevisar;
                    clasificacion.CantidadUtilizar = cantidadUtilizar;
                    clasificacion.Costo = costo;
                    clasificacion.Descripcion = descripcion;
                    clasificacion.FechaModificacion = fechaModificacion;
                    clasificacion.ObjetoXML = objetoXML;
                    clasificacion.TablaDetalles = tablaDetalles;
                    clasificacion.UnidadMedida = unidadMedida;
                    clasificacion.VerificacionAnual = verificacionAnual;
                    clasificacion.VidaUtil = vidaUtil;

                    //Establecemos el estado del registro a Modificado.
                    Conexion.Entry(clasificacion).State = EntityState.Modified;

                    //Guardamos los cambios y retornamos el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si se registró algún error, retornamos un 0.
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro de la tabla ClasificacionHerramental.
        /// </summary>
        /// <param name="idClaficacionHerramental">Entero que representa el id del registro que se requiere eliminar.</param>
        /// <returns>Número de registros que se eliminaron. Retorna un 0 si no se afectó ninguno o hubo algún error.</returns>
        public int DeleteClasificacionHerramental(int idClaficacionHerramental)
        {
            try
            {
                //Incializamos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesHerramentales())
                {
                    //Obtenemos el objeto que se requiere eliminar.
                    ClasificacionHerramental clasificacion = Conexion.ClasificacionHerramental.Where(x => x.idClasificacion == idClaficacionHerramental).FirstOrDefault();

                    //Establecemos el estado del registro a Eliminado.
                    Conexion.Entry(clasificacion).State = EntityState.Deleted;

                    //Guardamos los cambios y retornamos el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si se registró algún error, retornamos un 0.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todas las clasificaciones de herramental.
        /// </summary>
        /// <returns>Lista anónima que contiene la información de la tabla ClasificacionHerramental, si se genera algún error retornamos un nulo.</returns>
        public IList GetClasificacionHerramental()
        {
            try
            {
                //Incializamos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesHerramentales())
                {
                    //Realizamos la consulta para obtener todos los registros, los ordenamos por la descripcion.
                    var lista = (from h in Conexion.ClasificacionHerramental
                                 select h).OrderBy(x => x.Descripcion).ToList();

                    //Renornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception er)
            {
                //Si se generó algún error, retornamos un nulo.
                return null;
            }
        }
        #endregion
    }
}
