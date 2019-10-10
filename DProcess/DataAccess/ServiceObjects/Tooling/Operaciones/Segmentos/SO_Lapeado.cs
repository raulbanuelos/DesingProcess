using System;
using System.Collections;
using System.Linq;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Segmentos
{
    public class SO_Lapeado
    {
        #region Manga
        public IList GetManga(double d1)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaHerramentales = (from a in Conexion.TBL_MANGA_LAPEADO_SEGMENTOS
                                              join m in Conexion.MaestroHerramentales on a.CODIGO equals m.Codigo
                                              join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                              where a.MEDIDA_DECIMAL < d1 && m.Activo == true
                                              orderby a.MEDIDA_DECIMAL descending
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
                                                  MEDIDA_MANGA = a.MEDIDA,
                                                  a.ID_MANGA_LAPEADO_SEGMENTOS
                                              }).Take(1).ToList();

                    return listaHerramentales;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Rubber
        public IList GetRubber(double rubberMin, double rubberMax)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaHerramentales = (from a in Conexion.TBL_RUBBER_LAPEADO_SEGMENTOS
                                              join m in Conexion.MaestroHerramentales on a.CODIGO equals m.Codigo
                                              join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                              where a.DIM_A >= rubberMin && a.DIM_A <= rubberMax && m.Activo == true
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
                                                  DIM_A_RUBBER = a.DIM_A
                                              }).ToList();

                    return listaHerramentales;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Collar

        /// <summary>
        /// 
        /// </summary>
        /// <param name="espesorRadialMP">Espesor radial de la materia prima seleccionada.</param>
        /// <param name="d1">Diámetro del anillo (Inch)</param>
        /// <returns></returns>
        public IList GetDimACollarThickess(double espesorRadialMP, double d1)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    if (espesorRadialMP > 0.100)
                    {
                        var lista = (from a in Conexion.TBL_CONF_DIMA_COLLAR_LAPEADO_SEGMENTOS
                                     where a.DIAMETRO_MIN > d1
                                     select new {
                                         a.THICKNESS_UP_100
                                     }).ToList();
                        return lista;
                    }
                    else
                    {
                        var lista = (from a in Conexion.TBL_CONF_DIMA_COLLAR_LAPEADO_SEGMENTOS
                                     where a.DIAMETRO_MIN > d1
                                     select new
                                     {
                                         a.THICKNESS_DOWN_100
                                     }).ToList();
                        return lista;
                    }
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1">Diámetro del anillo (Inch)</param>
        /// <returns></returns>
        public IList GetSTDCollar(double d1)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var lista = (from a in Conexion.TBL_CONF_STD_COLLAR_LAPEADO_SEGMENTOS
                                 where a.MINIMO <= d1 && a.MAXIMO >= d1
                                 select new
                                 {
                                     a.DETALLE
                                 }).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1">Diámetro del anillo (Inch)</param>
        /// <returns></returns>
        public IList GetPlusCollar(double d1)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var lista = (from a in Conexion.TBL_CONF_PLUS_COLLAR_LAPEADO_SEGMENTOS
                                 where a.MINIMO <= d1 && a.MAXIMO >= d1
                                 select new
                                 {
                                     a.DETALLE
                                 }).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}
