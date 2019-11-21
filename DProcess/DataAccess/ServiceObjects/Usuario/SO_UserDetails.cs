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
    }
}
