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
        /// <summary>
        /// Consulta para traer todos los registros UpperRoll
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="d1"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Inserción de registros para UpperRoll
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="wire_width_min"></param>
        /// <param name="wire_width_max"></param>
        /// <param name="dia_min"></param>
        /// <param name="dia_max"></param>
        /// <param name="detalle_engrane"></param>
        /// <param name="medida"></param>
        /// <returns></returns>
        public int InsertBobinadoUpperRoll(string codigo, double wire_width_min, double wire_width_max, double dia_min, double dia_max, string detalle_engrane, double medida)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BOBINADO_UPPER_ROLL upper_roll = new TBL_BOBINADO_UPPER_ROLL();

                    // Asignamos valores
                    upper_roll.CODIGO = codigo;
                    upper_roll.WIRE_WIDTH_MIN = wire_width_min;
                    upper_roll.WIRE_WIDTH_MAX = wire_width_max;
                    upper_roll.DIA_MIN = dia_min;
                    upper_roll.DIA_MAX = dia_max;
                    upper_roll.DETALLE_ENGRANE = detalle_engrane;
                    upper_roll.MEDIDA = medida;

                    // Agregamos el objeto a la tabla
                    Conexion.TBL_BOBINADO_UPPER_ROLL.Add(upper_roll);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID
                    return upper_roll.ID_BOBINADO_UPPER_ROLL;
                }
            }
            catch (Exception)
            {
                // Si hay un error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registros UpperRoll
        /// </summary>
        /// <param name="id_bobinado_upper_roll"></param>
        /// <param name="codigo"></param>
        /// <param name="wire_width_min"></param>
        /// <param name="wire_width_max"></param>
        /// <param name="dia_min"></param>
        /// <param name="dia_max"></param>
        /// <param name="detalle_engrane"></param>
        /// <param name="medida"></param>
        /// <returns></returns>
        public int UpdateBobinadoUpperRoll(int id_bobinado_upper_roll, string codigo, double wire_width_min, double wire_width_max, double dia_min, double dia_max, string detalle_engrane, double medida)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la lista
                    TBL_BOBINADO_UPPER_ROLL upper_roll = Conexion.TBL_BOBINADO_UPPER_ROLL.Where(X => X.ID_BOBINADO_UPPER_ROLL == id_bobinado_upper_roll).FirstOrDefault();

                    // Asignamos valores
                    upper_roll.ID_BOBINADO_UPPER_ROLL = id_bobinado_upper_roll;
                    upper_roll.CODIGO = codigo;
                    upper_roll.WIRE_WIDTH_MIN = wire_width_min;
                    upper_roll.WIRE_WIDTH_MAX = wire_width_max;
                    upper_roll.DIA_MIN = dia_min;
                    upper_roll.DIA_MAX = dia_max;
                    upper_roll.DETALLE_ENGRANE = detalle_engrane;
                    upper_roll.MEDIDA = medida;

                    // Modificamos el registro
                    Conexion.Entry(upper_roll).State = System.Data.Entity.EntityState.Modified;

                    // Guardamos los cambios
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
        /// Delete de registros UpperRoll
        /// </summary>
        /// <param name="id_bobinado_upper_roll"></param>
        /// <returns></returns>
        public int DeleteBobinadoUpperRoll(int id_bobinado_upper_roll)
        {
            try
            {
                // Establecemos conexión a través de EntitFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BOBINADO_UPPER_ROLL upper_roll = Conexion.TBL_BOBINADO_UPPER_ROLL.Where(x => x.ID_BOBINADO_UPPER_ROLL == id_bobinado_upper_roll).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(upper_roll).State = System.Data.Entity.EntityState.Deleted;

                    // Guardamos cambios
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
        /// Consulta para traer todos los registros LowerRoll
        /// </summary>
        /// <param name="a1"></param>
        /// <param name="d1"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Inserción de registros para LowerRoll
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="detalle_rodillo"></param>
        /// <param name="detalle_engrane"></param>
        /// <param name="wire_width_min"></param>
        /// <param name="wire_width_max"></param>
        /// <param name="dia_min"></param>
        /// <param name="dia_max"></param>
        /// <param name="side_plate_dia"></param>
        /// <returns></returns>
        public int InsertBobinadoLowerRoll(string codigo, string detalle_rodillo, string detalle_engrane, double wire_width_min, double wire_width_max, double dia_min, double dia_max, double side_plate_dia)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BOBINADO_LOWER_ROLL lower_roll = new TBL_BOBINADO_LOWER_ROLL();

                    // Asignamos valores
                    lower_roll.CODIGO = codigo;
                    lower_roll.DETALLE_RODILLO = detalle_rodillo;
                    lower_roll.DETALLE_ENGRANE = detalle_engrane;
                    lower_roll.WIRE_WIDTH_MIN = wire_width_min;
                    lower_roll.WIRE_WIDTH_MAX = wire_width_max;
                    lower_roll.DIA_MIN = dia_min;
                    lower_roll.DIA_MAX = dia_max;
                    lower_roll.SIDE_PLATE_DIA = side_plate_dia;

                    // Agregamos el objeto en la lista
                    Conexion.TBL_BOBINADO_LOWER_ROLL.Add(lower_roll);

                    // Guardamos cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID
                    return lower_roll.ID_BOBINADO_LOWER_ROLL;
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registros LowerRoll
        /// </summary>
        /// <param name="id_bobinado_lower_roll"></param>
        /// <param name="codigo"></param>
        /// <param name="detalle_rodillo"></param>
        /// <param name="detalle_engrane"></param>
        /// <param name="wire_width_min"></param>
        /// <param name="wire_width_max"></param>
        /// <param name="dia_min"></param>
        /// <param name="dia_max"></param>
        /// <param name="side_plate_dia"></param>
        /// <returns></returns>
        public int UpdateBobinadoLowerRoll(int id_bobinado_lower_roll, string codigo, string detalle_rodillo, string detalle_engrane, double wire_width_min, double wire_width_max, double dia_min, double dia_max, double side_plate_dia)
        {
            try
            {
                // Establecemos conexión a través de EnrityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BOBINADO_LOWER_ROLL lower_roll = Conexion.TBL_BOBINADO_LOWER_ROLL.Where(X => X.ID_BOBINADO_LOWER_ROLL == id_bobinado_lower_roll).FirstOrDefault();

                    // Asignamos valores
                    lower_roll.ID_BOBINADO_LOWER_ROLL = id_bobinado_lower_roll;
                    lower_roll.CODIGO = codigo;
                    lower_roll.DETALLE_RODILLO = detalle_rodillo;
                    lower_roll.DETALLE_ENGRANE = detalle_engrane;
                    lower_roll.WIRE_WIDTH_MIN = wire_width_min;
                    lower_roll.WIRE_WIDTH_MAX = wire_width_max;
                    lower_roll.DIA_MIN = dia_min;
                    lower_roll.DIA_MAX = dia_max;
                    lower_roll.SIDE_PLATE_DIA = side_plate_dia;

                    // Modificamos el registro
                    Conexion.Entry(lower_roll).State = System.Data.Entity.EntityState.Modified;

                    // Guardamos cambios
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
        /// Eliminar registro Lower_Roll
        /// </summary>
        /// <param name="id_bobinado_lower_roll"></param>
        /// <returns></returns>
        public int DeleteBobinadoLowerRoll(int id_bobinado_lower_roll)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BOBINADO_LOWER_ROLL lower_roll = Conexion.TBL_BOBINADO_LOWER_ROLL.Where(x => x.ID_BOBINADO_LOWER_ROLL == id_bobinado_lower_roll).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(lower_roll).State = System.Data.Entity.EntityState.Deleted;

                    // Guardamos cambios
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                // Si hay errror retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Consulta para traer todos los registros TargetRoll
        /// </summary>
        /// <param name="medidaA"></param>
        /// <param name="medidaB"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Inserción de registros TarjetRoll
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int InsertBoninadoTarjetRoll(string codigo, double a, double b)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos la lista del objeto
                    TBL_BOBINADO_TARGET_ROLL tarjet_roll = new TBL_BOBINADO_TARGET_ROLL();

                    // Asignamos los valores
                    tarjet_roll.CODIGO = codigo;
                    tarjet_roll.A = a;
                    tarjet_roll.B = b;

                    // Agregamos el objeto a la tabla
                    Conexion.TBL_BOBINADO_TARGET_ROLL.Add(tarjet_roll);

                    // Guardamos cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID
                    return tarjet_roll.ID_BOBINADO_TARGET_ROLL;
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualizacion de registros TarjetRoll
        /// </summary>
        /// <param name="id_bobinado_tarjet_roll"></param>
        /// <param name="codigo"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int UpdateBobinadoTarjetRoll(int id_bobinado_tarjet_roll, string codigo, double a, double b)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BOBINADO_TARGET_ROLL tarjet_roll = Conexion.TBL_BOBINADO_TARGET_ROLL.Where(x => x.ID_BOBINADO_TARGET_ROLL == id_bobinado_tarjet_roll).FirstOrDefault();

                    // Establecemos valores
                    tarjet_roll.ID_BOBINADO_TARGET_ROLL = id_bobinado_tarjet_roll;
                    tarjet_roll.CODIGO = codigo;
                    tarjet_roll.A = a;
                    tarjet_roll.B = b;

                    // Actualizamos el registro
                    Conexion.Entry(tarjet_roll).State = System.Data.Entity.EntityState.Modified;

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
        /// Eliminar registro TarjetLower
        /// </summary>
        /// <param name="id_bobinado_tarjet_roll"></param>
        /// <returns></returns>
        public int DeletedBobinadoTarjetRoll(int id_bobinado_tarjet_roll)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BOBINADO_LOWER_ROLL tarjet_roll = Conexion.TBL_BOBINADO_LOWER_ROLL.Where(x => x.ID_BOBINADO_LOWER_ROLL == id_bobinado_tarjet_roll).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(tarjet_roll).State = System.Data.Entity.EntityState.Deleted;

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