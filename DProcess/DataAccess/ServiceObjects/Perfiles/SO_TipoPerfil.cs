using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_TipoPerfil
    {
        /// <summary>
        /// Método que retorna todos los registro de tipo de perfil.
        /// </summary>
        /// <returns></returns>
        public IList GetAllTipoPerfil()
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework
                using (var Conexion = new EntitiesPerfiles())
                {
                    //Realizamos la consulta para obtener todos los registros, los ordenamos y el resultado lo guardamos en una variable anónima.
                    var Lista = (from a in Conexion.CAT_TIPO_PERFIL
                                 select a).OrderByDescending(v => v.PERFIL).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }
    }
}
