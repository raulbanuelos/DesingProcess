using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
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
    }
}
