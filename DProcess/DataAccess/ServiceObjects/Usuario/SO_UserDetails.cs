using System;
using System.Collections;
using System.Collections.Generic;
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
    }
}
