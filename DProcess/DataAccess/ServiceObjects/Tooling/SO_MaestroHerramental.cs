using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_MaestroHerramental
    {
        /// <summary>
        /// Método que retorna todos los registro de el maestro de herramentales.
        /// </summary>
        /// <returns></returns>
        public DataSet GetMaestroHerramentales(string busqueda)
        {
            try
            {
                //Declaramos un objeto de tipo DataSet que será el que guarde los resultados de la consulta.
                DataSet datos = null;

                //Declaramos un objeto con el cual nos permitira conectarnos hacia la base de datos.
                Desing_SQL conexion = new Desing_SQL();

                //Declaramos un diccionario en el cual guardaremos los parámetros que requiere el procedimiento.
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                //Agregamos los parámertros necesarios del procedimiento.
                parametros.Add("CampoBusqueda", busqueda);

                //LLamamos al método para ejecutar el procedimiento, el resultado lo guardamos 
                datos = conexion.EjecutarStoredProcedure("SP_RGP_GetMaestroHerramentales", parametros);
                return datos;
            }
            catch (Exception er)
            {
                return null;
            }
        }

        /// <summary>
        /// Método para insertar un registro a la tabla MAESTROHERRAMENTALES
        /// </summary>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_cambio"></param>
        /// <param name="usuario_creacion"></param>
        /// <param name="usuario_cambio"></param>
        /// <param name="activo"></param>
        /// <param name="id_clasificacion"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public string SetMaestroHerramentales(string descripcion,string fecha_creacion,string fecha_cambio,string usuario_creacion,string usuario_cambio,bool activo,int id_clasificacion,int plano)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    //Se  crea un objeto, el cual se va agregar a la tabla 
                    MaestroHerramentales obj = new MaestroHerramentales();
                    //Se asiganan los valores.
                    obj.Descripcion = descripcion;
                    obj.FechaCreacion = fecha_creacion;
                    obj.FechaCambio = fecha_cambio;
                    obj.UsuarioCreacion = usuario_creacion;
                    obj.UsuarioCambio = usuario_cambio;
                    obj.Activo = activo;
                    obj.idClasificacionHerramental = id_clasificacion;
                    obj.idPlano = plano;
                    //Agrega el objeto a la tabla.
                    Conexion.MaestroHerramentales.Add(obj);
                    //Guardamos los cambios
                    Conexion.SaveChanges();
                    //Retorna el codigo del registro insertado
                    return obj.Codigo;

                }
            }
            catch (Exception)
            {
                //retorna nulo si hay error
                return null;
            }
        }

        /// <summary>
        /// Método para modificar un registro de la tabla MAESTROHERRAMENTALES
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_cambio"></param>
        /// <param name="usuario_creacion"></param>
        /// <param name="usuario_cambio"></param>
        /// <param name="activo"></param>
        /// <param name="id_clasificacion"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int UpdateMaestroHerramentales(string codigo,string descripcion,string fecha_creacion, string fecha_cambio, string usuario_creacion, string usuario_cambio, bool activo, int id_clasificacion, int plano)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    MaestroHerramentales obj = Conexion.MaestroHerramentales.Where(x => x.Codigo.Equals(codigo)).FirstOrDefault();
                    //Se asiganan los valores.
                    obj.Descripcion = descripcion;
                    obj.FechaCreacion = fecha_creacion;
                    obj.FechaCambio = fecha_cambio;
                    obj.UsuarioCreacion = usuario_creacion;
                    obj.UsuarioCambio = usuario_cambio;
                    obj.Activo = activo;
                    obj.idClasificacionHerramental = id_clasificacion;
                    obj.idPlano = plano;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;
                    
                    //Guardamos los cambios
                   return Conexion.SaveChanges();

                }
            }
            catch (Exception)
            {
                //retorna nulo si hay error
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla MAESTROHERRAMENTALES
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int DeleteMaestroHerramentales(string codigo)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    MaestroHerramentales obj = Conexion.MaestroHerramentales.Where(x => x.Codigo.Equals(codigo)).FirstOrDefault();

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Guardamos los cambios y retornamos el resultado
                    return Conexion.SaveChanges();

                }
            }
            catch (Exception)
            {
                //retorna nulo si hay error
                return 0;
            }
        }
    }
}
