using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.ServiceObjects.Usuario
{
    public class SO_Usuario
    {

        #region Métodos

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

        /// <summary>
        /// Método que obtiene el perfil de un usuario en específico.
        /// </summary>
        /// <param name="idUsuario">Cadena que representa el usuario al que se requiere obtener el perfil.</param>
        /// <returns>Lista anónima con la información del perfil del usuario. Retorna un nulo si se generó elgún error.</returns>
        public IList GetPerfilUsuario(string idUsuario)
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesUsuario())
                {

                    //Realizamos la consulta.
                    var listaPermisos = (from p in Conexion.PerfilUsuario
                                         where p.ID_USUARIO == idUsuario
                                         select p).ToList();

                    //Retornamos el resultado de la consulta.
                    return listaPermisos;
                }
            }
            catch (Exception)
            {
                //Si se generó algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene los privilegios de un usuario en específico.
        /// </summary>
        /// <param name="idUsuario">Cadena que representa el usuario al que se requiere obtener los privilegios.</param>
        /// <returns>Lista anónima con la información de los privilegios del usuario. Retorna un nulo si se generó elgún error.</returns>
        public IList GetPrivilegioUsuario(string idUsuario)
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesUsuario())
                {

                    //Realizamos la consulta.
                    var listaPrivilegios = (from p in Conexion.PrivilegioUsuario
                                            where p.ID_USUARIO == idUsuario
                                            select p).ToList();

                    //Retornamos el resultado de la consulta.
                    return listaPrivilegios;
                }
            }
            catch (Exception)
            {
                //Si se generó algún error, retornamos un nulo.
                return null;
            }
        }

        public int Privilegio_Usuario(string id_usuario, bool rgp, bool tooling, bool raw_material, bool standar_time, bool quotes, bool cit,
            bool data, bool user_profile, bool help)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesUsuario())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    PrivilegioUsuario Puser = new PrivilegioUsuario();

                    //Se asiganan los valores.
                    Puser.ID_USUARIO = id_usuario;
                    Puser.RGP = rgp;
                    Puser.TOOLING = tooling;
                    Puser.RAW_MATERIAL = raw_material;
                    Puser.STANDAR_TIME = standar_time;
                    Puser.QUOTES = quotes;
                    Puser.CIT = cit;
                    Puser.DATA = data;
                    Puser.USER_PROFILE = user_profile;
                    Puser.HELP = help;

                    //Agrega el objeto a la tabla.
                    Conexion.PrivilegioUsuario.Add(Puser);
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del usuario insertado
                    return Puser.ID_PRIVILEGIO_USUARIO;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        public int Perfil_Usuario(string id_usuario, bool rgp, bool tooling, bool raw_material, bool standar_time, bool quotes, bool cit,
           bool data, bool user_profile, bool help)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesUsuario())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    PerfilUsuario Puser = new PerfilUsuario();

                    //Se asiganan los valores.
                    Puser.ID_USUARIO = id_usuario;
                    Puser.RGP = rgp;
                    Puser.TOOLING = tooling;
                    Puser.RAW_MATERIAL = raw_material;
                    Puser.STANDAR_TIME = standar_time;
                    Puser.QUOTES = quotes;
                    Puser.CIT = cit;
                    Puser.DATA = data;
                    Puser.USER_PROFILE = user_profile;
                    Puser.HELP = help;

                    //Agrega el objeto a la tabla.
                    Conexion.PerfilUsuario.Add(Puser);
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del usuario insertado
                    return Puser.ID_PERFIL_USUARIO;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        #endregion
    }
}
