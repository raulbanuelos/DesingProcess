using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_Propiedad
    {
        /// <summary>
        /// Método que retorna todas las propiedades.
        /// </summary>
        /// <returns></returns>
        public IList GetAllPropiedades()
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework
                using (var Conexion = new EntitiesPerfiles())
                {
                    //Realizamos la consulta y el resultado lo guardamos en una lista anónima.
                    var lista = (from a in Conexion.CAT_PROPIEDAD
                                 select a).ToList();

                    //Retornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error, retornamos un null.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene las propiedades a partir de un perfil.
        /// </summary>
        /// <param name="idPerfil">Entero que representa el id del perfil que se requiere.</param>
        /// <returns></returns>
        public IList GetPropiedadesByPerfil(int idPerfil)
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework
                using (var Conexion = new EntitiesPerfiles())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var lista = (from a in Conexion.CAT_PROPIEDAD
                                 join b in Conexion.TR_PROPIEDAD_PERFIL on a.ID_PROPIEDAD equals b.ID_PROPIEDAD
                                 join c in Conexion.CAT_PERFIL on b.ID_PERFIL equals c.ID_PERFIL
                                 where c.ID_PERFIL == idPerfil
                                 select a).ToList();

                    //Retornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }
    }
}
