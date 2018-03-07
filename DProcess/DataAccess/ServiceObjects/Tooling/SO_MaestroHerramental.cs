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
                //Si hay error, retorna nulo
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
        public string SetMaestroHerramentales(string descripcion,string fecha_creacion,string fecha_cambio,string usuario_creacion,string usuario_cambio,bool activo,int id_clasificacion,int plano, string codigo)
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
                    obj.idPlano = null;
                    obj.Codigo = codigo;

                    //Agrega el objeto a la tabla.
                    Conexion.MaestroHerramentales.Add(obj);
                    //Guardamos los cambios
                    Conexion.SaveChanges();
                    //Retorna el codigo del registro insertado
                    return obj.Codigo;

                }
            }
            catch (Exception er)
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
        public int UpdateMaestroHerramentales(string codigo,string descripcion, string fecha_cambio, string usuario_cambio, bool activo, int id_clasificacion, int plano)
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
                    obj.FechaCambio = fecha_cambio;
                    obj.UsuarioCambio = usuario_cambio;
                    obj.Activo = activo;
                    obj.idClasificacionHerramental = id_clasificacion;
                    obj.idPlano = null;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;
                    
                    //Guardamos los cambios
                   return Conexion.SaveChanges();

                }
            }
            catch (Exception er)
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

        /// <summary>
        /// Método que comprueba si existe un código igual
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public string GetCodigoMaestro(string codigo)
        {
            try
            { //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se ejecuta el comando para verificar si el código existe
                    var id = (from m in Conexion.MaestroHerramentales
                              where m.Codigo.Equals(codigo)
                              select m.Codigo).FirstOrDefault();
                    return id;
                }

            }
            catch (Exception er)
            {
                //Si hay error regresa nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene toda la información de un maestro herramental 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetPropiedadesHerramental(string codigo)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se ejecuta el comando para obtener la información del herramental
                    var Lista = (from m in Conexion.MaestroHerramentales
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where m.Codigo.Equals(codigo)
                                 select new
                                 {
                                     m.Descripcion,
                                     m.Activo,
                                     m.idClasificacionHerramental,
                                     m.idPlano,
                                     c.ObjetoXML
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //si hay error, retorna nulo
                return null;
            }
        }

        public IList GetHerramental(string codigo)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var Lista = (from m in Conexion.MaestroHerramentales
                                 where m.Codigo == codigo
                                 select m).ToList();
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
