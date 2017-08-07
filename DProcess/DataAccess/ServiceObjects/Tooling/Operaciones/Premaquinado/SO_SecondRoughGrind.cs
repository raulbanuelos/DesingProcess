using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Premaquinado
{
    public class SO_SecondRoughGrind
    {
        /// <summary>
        /// Método el cual obtiene el herramental GuideBar.
        /// </summary>
        /// <param name="widthOperacion"></param>
        /// <returns></returns>
        public IList GetGuideBar(double widthOperacion)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.GuideBarSecondRoughGrind
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where widthOperacion >= a.WidthMinProceso && widthOperacion <= a.WidthMaxProceso && m.Activo == true
                                 select new
                                 {
                                     a.EspesorBarraGuia,
                                     a.Codigo,
                                     m.Descripcion,
                                     m.Activo,
                                     Clasificacion = c.Descripcion,
                                     c.UnidadMedida,
                                     c.Costo,
                                     c.CantidadUtilizar,
                                     c.VidaUtil,
                                     c.idClasificacion,
                                     c.ListaCotasRevisar,
                                     c.VerificacionAnual
                                 }).ToList();

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
