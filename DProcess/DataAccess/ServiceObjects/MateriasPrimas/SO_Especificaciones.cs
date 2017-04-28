using System;
using System.Collections;
using System.Linq;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_Especificaciones
    {
        /// <summary>
        /// Método que obtiene los registros de la base de datos de Especificaciones de materia prima.
        /// </summary>
        /// <returns></returns>
        public IList GetAllEspecificaciones()
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Relizamos la consulta. El resultado lo asignamos a un objeto anónimo.
                    var Lista = (from a in Conexion.Esp_MP_Anillos
                                 select a).ToList();

                    //Retornamos el resuiltado.
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
