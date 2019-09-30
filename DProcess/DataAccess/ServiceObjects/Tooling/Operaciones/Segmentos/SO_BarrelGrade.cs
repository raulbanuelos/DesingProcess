using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Segmentos
{
    public class SO_BarrelGrade
    {
        public IList GetPusher(double d1, double barrelPucherD1Min, double barrelPusherD1Max)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaHerramentales = (from a in Conexion.TBL_BARREL_GRADE_PUSHER
                                              join m in Conexion.MaestroHerramentales on a.CODIGO equals m.Codigo
                                              join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                              where a.DIM_F >= (d1 - barrelPucherD1Min) && a.DIM_F <= (d1 + barrelPusherD1Max)
                                              select new
                                              {
                                                  Codigo = a.CODIGO,
                                                  m.Descripcion,
                                                  m.Activo,
                                                  Clasificacion = c.Descripcion,
                                                  c.UnidadMedida,
                                                  c.Costo,
                                                  c.CantidadUtilizar,
                                                  c.VidaUtil,
                                                  c.idClasificacion,
                                                  c.ListaCotasRevisar,
                                                  c.VerificacionAnual,
                                                  DIM_F = a.DIM_F
                                              }).ToList();

                    return listaHerramentales;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList GetBushing(double d1, double barrelBushingD1Min, double barrelBishingD1Max)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaHerramentales = (from a in Conexion.TBL_BARREL_GRADE_BUSHING
                                              join m in Conexion.MaestroHerramentales on a.CODIGO equals m.Codigo
                                              join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                              where a.DIM_D >= (d1 - barrelBushingD1Min) && a.DIM_D <= (d1 + barrelBishingD1Max)
                                              select new
                                              {
                                                  Codigo = a.CODIGO,
                                                  m.Descripcion,
                                                  m.Activo,
                                                  Clasificacion = c.Descripcion,
                                                  c.UnidadMedida,
                                                  c.Costo,
                                                  c.CantidadUtilizar,
                                                  c.VidaUtil,
                                                  c.idClasificacion,
                                                  c.ListaCotasRevisar,
                                                  c.VerificacionAnual,
                                                  DIM_D = a.DIM_D
                                              }).ToList();

                    return listaHerramentales;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
