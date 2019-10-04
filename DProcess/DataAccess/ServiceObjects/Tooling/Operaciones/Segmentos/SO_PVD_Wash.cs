using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Segmentos
{
    public class SO_PVD_Wash
    {
        /// <summary>
        /// Función que retorna las posibles opciones de mesa de acuerdo al rango ideal.
        /// </summary>
        /// <param name="d1">Diámetro del anillo en mm</param>
        /// <returns></returns>
        public IList GetMesaFirstOption(double d1)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaMesas = (from a in Conexion.CAT_MESA_PVD_WASH
                                      where a.MIN_IDEAL_AVAILABLE <= d1 && a.MAX_IDEAL_AVAILABLE >= d1
                                      select a).ToList();

                    return listaMesas;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Función que retorna las posibles opciones de mesa de acuerdo al rango tecnicamente posible..
        /// </summary>
        /// <param name="d1">Diámetro del anillo en mm</param>
        /// <returns></returns>
        public IList GetMesaSecondOption(double d1)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaMesas = (from a in Conexion.CAT_MESA_PVD_WASH
                                      where a.MIN_TECH_AVAILABLE >= d1 && a.MAX_TECH_AVAILABLE <= d1
                                      select a).ToList();

                    return listaMesas;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
