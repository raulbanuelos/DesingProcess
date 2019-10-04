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

        /// <summary>
        /// Inserción de registro BarrelGradeBushing
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dim_d"></param>
        /// <returns></returns>
        public int InsertBarrelGradeBushing(string codigo, double dim_d)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using ( var Conexion = new EntitiesTooling())
                {
                    // Decalramos el objeto de la lista
                    TBL_BARREL_GRADE_BUSHING grade_bushing = new TBL_BARREL_GRADE_BUSHING();

                    // Asignamos los valores
                    grade_bushing.CODIGO = codigo;
                    grade_bushing.DIM_D = dim_d;

                    // Insertamos el objeto a la tabla
                    Conexion.TBL_BARREL_GRADE_BUSHING.Add(grade_bushing);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID
                    return grade_bushing.ID_BARREL_GRADE_BUSHING;
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registro BarrelGradeBushing
        /// </summary>
        /// <param name="id_barrel_grade_bushing"></param>
        /// <param name="codigo"></param>
        /// <param name="dim_d"></param>
        /// <returns></returns>
        public int UpdateBarrelGradeBushing(int id_barrel_grade_bushing, string codigo, double dim_d)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BARREL_GRADE_BUSHING grade_bushing = Conexion.TBL_BARREL_GRADE_BUSHING.Where(x => x.ID_BARREL_GRADE_BUSHING == id_barrel_grade_bushing).FirstOrDefault();

                    // Asignamos valores
                    grade_bushing.ID_BARREL_GRADE_BUSHING = id_barrel_grade_bushing;
                    grade_bushing.CODIGO = codigo;
                    grade_bushing.DIM_D = dim_d;

                    // Actualizamos el registro
                    Conexion.Entry(grade_bushing).State = System.Data.Entity.EntityState.Modified;

                    // Retornamos elementos afectados
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Eliminar registro BarrelGradeBushing
        /// </summary>
        /// <param name="id_barrel_grade_bushing"></param>
        /// <returns></returns>
        public int DeletedBarrelGradeBushing(int id_barrel_grade_bushing)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BARREL_GRADE_BUSHING grade_bushing = Conexion.TBL_BARREL_GRADE_BUSHING.Where(x => x.ID_BARREL_GRADE_BUSHING == id_barrel_grade_bushing).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(grade_bushing).State = System.Data.Entity.EntityState.Deleted;

                    // Retornamos registros afectados
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Inserción de registro BarrelGradePusher
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dim_f"></param>
        /// <returns></returns>
        public int InsertBarrelGradePusher(string codigo, double dim_f)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BARREL_GRADE_PUSHER grade_pusher = new TBL_BARREL_GRADE_PUSHER();

                    // Asignamos los valores
                    grade_pusher.CODIGO = codigo;
                    grade_pusher.DIM_F = dim_f;

                    // Insertamos el objeto a la tabla
                    Conexion.TBL_BARREL_GRADE_PUSHER.Add(grade_pusher);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID
                    return grade_pusher.ID_BARREL_GRADE_PUSHER;
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registro BarrelGradePusher
        /// </summary>
        /// <param name="id_barrel_grade_pusher"></param>
        /// <param name="codigo"></param>
        /// <param name="dim_f"></param>
        /// <returns></returns>
        public int UpdateBarrelGradePusher(int id_barrel_grade_pusher, string codigo, double dim_f)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BARREL_GRADE_PUSHER grade_pusher = Conexion.TBL_BARREL_GRADE_PUSHER.Where(x => x.ID_BARREL_GRADE_PUSHER == id_barrel_grade_pusher).FirstOrDefault();

                    // Asignamos valores
                    grade_pusher.ID_BARREL_GRADE_PUSHER = id_barrel_grade_pusher;
                    grade_pusher.CODIGO = codigo;
                    grade_pusher.DIM_F = dim_f;

                    // Actualizamos el registro
                    Conexion.Entry(grade_pusher).State = System.Data.Entity.EntityState.Modified;

                    // Retornamos los registros afectados
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Delete registros BarrelGradePusher
        /// </summary>
        /// <param name="id_barrel_grade_pusher"></param>
        /// <returns></returns>
        public int DeleteBarrelGradePusher(int id_barrel_grade_pusher)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BARREL_GRADE_PUSHER grade_pusher = Conexion.TBL_BARREL_GRADE_PUSHER.Where(x => x.ID_BARREL_GRADE_PUSHER == id_barrel_grade_pusher).FirstOrDefault();

                    // Eliminamos el registro de la tabla
                    Conexion.Entry(grade_pusher).State = System.Data.Entity.EntityState.Deleted;

                    // Retornamos registros afectados
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }
    }
}
