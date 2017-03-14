using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.SQLServer;

namespace DataAccess.ServiceObjects
{
    public class SO_Usuario
    {
        public SO_Usuario()
        {
        }

        /// <summary>
        /// Método que ejecuta el procedimiento que realiza el LogIn del sistema.
        /// </summary>
        /// <param name="usuario">Usuario</param>
        /// <param name="contrasena">Contraseña</param>
        /// <returns>Si los datos corresponden a un usuario registrado retorna los datos completos de la persona, sino retorna un nulo.</returns>
        public DataSet GetLogin(string usuario, string contrasena)
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
                parametros.Add("usuario", usuario);
                parametros.Add("contrasena", contrasena);

                //LLamamos al método para ejecutar el procedimiento, el resultado lo guardamos 
                datos = conexion.EjecutarStoredProcedure("SP_RGP_GetLogin", parametros);
                return datos;
            }
            catch (Exception er)
            {
                return null;
            }

        }
    }
}
