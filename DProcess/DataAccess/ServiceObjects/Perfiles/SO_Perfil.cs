using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_Perfil
    {
        /// <summary>
        /// Método que retorna todos los registros de Perfiles
        /// </summary>
        /// <returns></returns>
        public IList GetAllPerfiles()
        {
            try
            {
                //Establecemos la conexión a la base de datos a través de Entity Framework.
                using (var Conexion = new EntitiesPerfiles())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var lista = (from a in Conexion.CAT_PERFIL
                                 select a).ToList();

                    //Retornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception er)
            {
                //Si se genero algún error, retornamos un null.
                return null;
            }
        }
    }
}
