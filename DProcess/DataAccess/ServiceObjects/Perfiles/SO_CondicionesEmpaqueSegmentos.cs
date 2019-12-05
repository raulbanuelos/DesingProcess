using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_CondicionesEmpaqueSegmentos
    {
        /// <summary>
        /// Método que obtiene el número de piezas por rollo de segmentos.
        /// </summary>
        /// <param name="h1">Width del segmento.</param>
        /// <returns></returns>
        public IList GetPzasRollo(double h1)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.TBL_PZA_ROLLO_SEGMENTO
                                 where a.WIDTH_SEGMENTO == h1
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
        /// Método que obtiene el número de rollos por caja y el número de caja de segmentos.
        /// </summary>
        /// <param name="d1">Diámetro del anillo.</param>
        /// <returns></returns>
        public IList GetRollosCaja(double d1)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.TBL_ROLLOS_CAJA_SEGMENTOS
                                 join b in Conexion.TBL_NO_CAJA on a.ID_NO_CAJA equals b.ID_NO_CAJA
                                 where a.DIAMETRO_MIN <= d1 && a.DIAMETRO_MAX >= d1
                                 select new
                                 {
                                     a.ROLLOS_CAJA,
                                     b.NO_CAJA
                                 }).ToList();

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
