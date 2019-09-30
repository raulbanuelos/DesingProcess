using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Segmentos
{
    public class SO_Bobinado
    {
        public IList GetUpperRoll(double a1, double d1)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaHerramentales = (from a in Conexion.TBL_BOBINADO_UPPER_ROLL
                                              join m in Conexion.MaestroHerramentales on a.CODIGO equals m.Codigo
                                              join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                              where a1 <= a.WIRE_WIDTH_MAX && a1 >= a.WIRE_WIDTH_MIN && d1 <= a.DIA_MAX && d1 >= a.DIA_MIN && m.Activo == true
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
                                                  DETALLE_ENGRANE = a.DETALLE_ENGRANE,
                                                  a.MEDIDA
                                              }).ToList();

                    return listaHerramentales;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList GetLowerRoll(double a1, double d1)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaHerramentales = (from a in Conexion.TBL_BOBINADO_LOWER_ROLL
                                              join m in Conexion.MaestroHerramentales on a.CODIGO equals m.Codigo
                                              join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                              where a1 <= a.WIRE_WIDTH_MAX && a1 >= a.WIRE_WIDTH_MIN && d1 <= a.DIA_MAX && d1 >= a.DIA_MIN && m.Activo == true
                                              select new {
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
                                                  DETALLE_ENGRANE = a.DETALLE_ENGRANE,
                                                  DETALLE_RODILLO = a.DETALLE_RODILLO,
                                                  SIDE_PLATE_DIA = a.SIDE_PLATE_DIA
                                              }).ToList();

                    return listaHerramentales;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList GetTargetRoll(double medidaA, double medidaB)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaHerramentales = (from a in Conexion.TBL_BOBINADO_TARGET_ROLL
                                              join m in Conexion.MaestroHerramentales on a.CODIGO equals m.Codigo
                                              join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                              where a.A == medidaA && a.B == medidaB
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
                                                  A = a.A,
                                                  B = a.B
                                              }).ToList();

                    return listaHerramentales;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList GetCenterWafer(double h1, double d1, double centerwaferh1min, double centerwaferh1max)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaHerramentales = (from a in Conexion.TBL_BOBINADO_CENTER_WAFER
                                              join m in Conexion.MaestroHerramentales on a.CODIGO equals m.Codigo
                                              join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                              where a.WIRE_WIDTH >= h1 - centerwaferh1min && a.WIRE_WIDTH <= h1 + centerwaferh1max && d1 >= a.DIM_A_MIN && d1 <= a.DIM_A_MAX
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
                                                  DETALLE = a.DETALLE,
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
