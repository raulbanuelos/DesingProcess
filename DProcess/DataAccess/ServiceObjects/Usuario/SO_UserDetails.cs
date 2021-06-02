using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Usuario
{
    public class SO_UserDetails
    {
        /// <summary>
        /// Obtiene los detalles de un usuario en específico.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public IList GetUserDetails(string idUsuario)
        {
            try
            {
                using (var Conexion = new EntitiesUsuario())
                {
                    var lista = (from a in Conexion.TBL_USERS_DETAILS
                                 where a.ID_USUARIO == idUsuario
                                 select a).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método para actulizar el campo de Is Available Email de la tabla TBL_USERS_DETAILS.
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <param name="isAvailableEmail"></param>
        /// <returns></returns>
        public int UpdateIsAvailableEmail(string idUsuario, bool isAvailableEmail)
        {
            try
            {
                using (var Conexion = new EntitiesUsuario())
                {
                    TBL_USERS_DETAILS user = Conexion.TBL_USERS_DETAILS.Where(x => x.ID_USUARIO == idUsuario).FirstOrDefault();

                    user.IS_AVAILABLE_EMAIL = isAvailableEmail;

                    Conexion.Entry(user).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Consulta para actualizar el valor de el campo TEMPORAL_PASSWORD en la tabla TBL_USER_DETAIL
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="temporalpassword"></param>
        /// <returns></returns>
        public int UpdateTemporalPassword(string usuario, bool temporalpassword)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesUsuario())
                {
                    // Declaramos el objeto de la lista
                    TBL_USERS_DETAILS objdetail = Conexion.TBL_USERS_DETAILS.Where(x => x.ID_USUARIO == usuario).FirstOrDefault();

                    // Asignamos valores
                    objdetail.TEMPORAL_PASSWORD = temporalpassword;

                    // Guardamos los cambios
                    Conexion.Entry(objdetail).State = EntityState.Modified;

                    // Cambios
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Método para insertar un registro en la tabla TBL_USER_DETAILS
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="url_foto"></param>
        /// <param name="is_available_email"></param>
        /// <returns></returns>
        public int InsertUserDetail(string id_usuario, string url_foto, bool is_available_email, bool temporal_password)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesUsuario())
                {
                    // Declaramos el objeto de la tabla
                    TBL_USERS_DETAILS objdetail = new TBL_USERS_DETAILS();

                    // Asignamos valores
                    objdetail.ID_USUARIO = id_usuario;
                    objdetail.URL_PHOTO = url_foto;
                    objdetail.IS_AVAILABLE_EMAIL = is_available_email;
                    objdetail.TEMPORAL_PASSWORD = temporal_password;

                    // Agregamos el objeto a la tabla
                    Conexion.TBL_USERS_DETAILS.Add(objdetail);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID del objeto
                    return objdetail.ID_USERS_DETAILS;
                }
            }
            catch (Exception)
            {
                // Si hay un error, retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla TBL_USER_DETAILS
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public int DeleteUserDetail(string id_usuario)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using(var Conexion = new EntitiesUsuario())
                {
                    // Declaramos el objeto de la tabla
                    TBL_USERS_DETAILS objDetail = Conexion.TBL_USERS_DETAILS.Where(x => x.ID_USUARIO == id_usuario).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(objDetail).State = EntityState.Deleted;

                    // Guardamos los cambios
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }
    }
}
