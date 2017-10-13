using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_CutterAngle
    {
        /// <summary>
        /// Método que obtiene los valores de CutterAngle
        /// </summary>
        /// <param name="cutterAngle"></param>
        /// <returns></returns>
        public IList GetCutterAngle(double cutterAngle)
        {
            try
            {
                //Establecemos la conexión a la base de datos a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y lo guardamos en una lista anónima.
                    var Lista = (from a in Conexion.cutter_angle
                                 where a.dec == cutterAngle
                                 select new
                                 {
                                     a.GRADOS,
                                     a.MINUTOS
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }
    }
}
