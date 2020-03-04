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
        /// Consulta para traer los datos de un registro UpperRoll por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoUpperRoll(string codigo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Realizamos la consulta y el resultado lo asignamos a una variable anónima
                    var Lista = (from a in Conexion.TBL_BOBINADO_UPPER_ROLL
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     a.ID_BOBINADO_UPPER_ROLL,
                                     a.CODIGO,
                                     a.WIRE_WIDTH_MIN,
                                     a.WIRE_WIDTH_MAX,
                                     a.DIA_MIN,
                                     a.DIA_MAX,
                                     a.DETALLE_ENGRANE,
                                     a.MEDIDA,
                                     b.Descripcion,
                                     b.Activo
                                 }).ToList();

                    // Retornamos el resultado de la consulta
                    return Lista;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos nulo
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
            catch (Exception er)
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
            catch (Exception er)
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
            catch (Exception er)
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
        /// Consulta para traer los datos de un registro LowerRoll por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoLowerRoll(string codigo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Realizamos la consulta y el resultado lo asignamos a una variable anónima
                    var Lista = (from a in Conexion.TBL_BOBINADO_LOWER_ROLL
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     a.ID_BOBINADO_LOWER_ROLL,
                                     a.CODIGO,
                                     a.DETALLE_RODILLO,
                                     a.DETALLE_ENGRANE,
                                     a.WIRE_WIDTH_MIN,
                                     a.WIRE_WIDTH_MAX,
                                     a.DIA_MIN,
                                     a.DIA_MAX,
                                     a.SIDE_PLATE_DIA,
                                     b.Descripcion,
                                     b.Activo
                                 }).ToList();

                    // Retornamos el resultado de la consulta
                    return Lista;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos null
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
            catch (Exception er)
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
            catch (Exception er)
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
            catch (Exception er)
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
        /// Consulta para traernos los datos de un registro TargetRoll por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoTargetRoll(string codigo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Realizamos la consulta y el resultado lo asignamos a una variable anónima
                    var Lista = (from a in Conexion.TBL_BOBINADO_TARGET_ROLL
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     a.ID_BOBINADO_TARGET_ROLL,
                                     a.CODIGO,
                                     a.A,
                                     a.B,
                                     b.Descripcion,
                                     b.Activo
                                 }).ToList();

                    // Retornamos el resultado de la consulta
                    return Lista;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos nulo
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
        public int InsertBobinadoTargetRoll(string codigo, double a, double b)
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
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualizacion de registros TarjetRoll
        /// </summary>
        /// <param name="id_bobinado_target_roll"></param>
        /// <param name="codigo"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public int UpdateBobinadoTargetRoll(int id_bobinado_target_roll, string codigo, double a, double b)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BOBINADO_TARGET_ROLL tarjet_roll = Conexion.TBL_BOBINADO_TARGET_ROLL.Where(x => x.ID_BOBINADO_TARGET_ROLL == id_bobinado_target_roll).FirstOrDefault();

                    // Establecemos valores
                    tarjet_roll.ID_BOBINADO_TARGET_ROLL = id_bobinado_target_roll;
                    tarjet_roll.CODIGO = codigo;
                    tarjet_roll.A = a;
                    tarjet_roll.B = b;

                    // Actualizamos el registro
                    Conexion.Entry(tarjet_roll).State = System.Data.Entity.EntityState.Modified;

                    // Retornamos registros afectados
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Eliminar registro TarjetLower
        /// </summary>
        /// <param name="id_bobinado_target_roll"></param>
        /// <returns></returns>
        public int DeletedBobinadoTargetRoll(int id_bobinado_target_roll)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BOBINADO_TARGET_ROLL tarjet_roll = Conexion.TBL_BOBINADO_TARGET_ROLL.Where(x => x.ID_BOBINADO_TARGET_ROLL == id_bobinado_target_roll).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(tarjet_roll).State = System.Data.Entity.EntityState.Deleted;

                    // Retornamos los registros afectados
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Consulta para traer todos los registros CenterWafer
        /// </summary>
        /// <param name="h1"></param>
        /// <param name="d1"></param>
        /// <param name="centerwaferh1min"></param>
        /// <param name="centerwaferh1max"></param>
        /// <returns></returns>
        public IList GetCenterWafer(double h1, double d1, double centerwaferh1min, double centerwaferh1max)
        {
            double h1Min, h1Max;

            h1Min = h1 - centerwaferh1min;
            h1Max = h1 + centerwaferh1max;
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaHerramentales = (from a in Conexion.TBL_BOBINADO_CENTER_WAFER
                                              join m in Conexion.MaestroHerramentales on a.CODIGO equals m.Codigo
                                              join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                              where a.WIRE_WIDTH >= h1Min && a.WIRE_WIDTH <= h1Max && d1 >= a.DIM_A_MIN && d1 <= a.DIM_A_MAX
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

        /// <summary>
        /// Consulta para traernos los datos de un registro CenterWafer por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoCenterWafer(string codigo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Realizamos la consulta y el rsultado lo asignamos a una variable anónima
                    var Lista = (from a in Conexion.TBL_BOBINADO_CENTER_WAFER
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     a.ID_BOBONADO_CENTER_WAFER,
                                     a.CODIGO,
                                     a.DIM_A_MIN,
                                     a.DIM_A_MAX,
                                     a.WIRE_WIDTH,
                                     a.DETALLE,
                                     a.DIA_B,
                                     a.F_WIDTH,
                                     b.Descripcion,
                                     b.Activo
                                 }).ToList();

                    // Retornamos el resultado de la consulta
                    return Lista;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos nulo
                return null;
            }
        }

        /// <summary>
        /// Inserción de registros CenterWafer
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dim_a_min"></param>
        /// <param name="dim_a_max"></param>
        /// <param name="wire_width"></param>
        /// <param name="detalle"></param>
        /// <param name="dia_b"></param>
        /// <param name="f_width"></param>
        /// <returns></returns>
        public int InsertBobinadoCenterWafer(string codigo, double dim_a_min, double dim_a_max, double wire_width, string detalle, double dia_b, double f_width)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos la lista del objeto
                    TBL_BOBINADO_CENTER_WAFER center_wafer = new TBL_BOBINADO_CENTER_WAFER();

                    // Asignamos valores
                    center_wafer.CODIGO = codigo;
                    center_wafer.DIM_A_MIN = dim_a_min;
                    center_wafer.DIM_A_MAX = dim_a_max;
                    center_wafer.WIRE_WIDTH = wire_width;
                    center_wafer.DETALLE = detalle;
                    center_wafer.DIA_B = dia_b;
                    center_wafer.F_WIDTH = f_width;

                    // Insertamos el registro
                    Conexion.TBL_BOBINADO_CENTER_WAFER.Add(center_wafer);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID
                    return center_wafer.ID_BOBONADO_CENTER_WAFER;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registros CenterWafer
        /// </summary>
        /// <param name="id_bobinado_center_wafer"></param>
        /// <param name="codigo"></param>
        /// <param name="dim_a_min"></param>
        /// <param name="dim_a_max"></param>
        /// <param name="wire_width"></param>
        /// <param name="detalle"></param>
        /// <param name="dia_b"></param>
        /// <param name="f_width"></param>
        /// <returns></returns>
        public int UpdateBobinadoCenterWafer(int id_bobinado_center_wafer, string codigo, double dim_a_min, double dim_a_max, double wire_width, string detalle, double dia_b, double f_width)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BOBINADO_CENTER_WAFER center_wafer = Conexion.TBL_BOBINADO_CENTER_WAFER.Where(x => x.ID_BOBONADO_CENTER_WAFER == id_bobinado_center_wafer).FirstOrDefault();

                    // Asignamos valores
                    center_wafer.ID_BOBONADO_CENTER_WAFER = id_bobinado_center_wafer;
                    center_wafer.CODIGO = codigo;
                    center_wafer.DIM_A_MIN = dim_a_min;
                    center_wafer.DIM_A_MAX = dim_a_max;
                    center_wafer.WIRE_WIDTH = wire_width;
                    center_wafer.DETALLE = detalle;
                    center_wafer.DIA_B = dia_b;
                    center_wafer.F_WIDTH = f_width;

                    // Actualizamos el registro
                    Conexion.Entry(center_wafer).State = System.Data.Entity.EntityState.Modified;

                    // Retornamos datos afectados
                    return Conexion.SaveChanges();                    
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Eliminar registros CenterWafer
        /// </summary>
        /// <param name="id_bobinado_center_wafer"></param>
        /// <returns></returns>
        public int DeleteBobinadoCenterWafer(int id_bobinado_center_wafer)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BOBINADO_CENTER_WAFER center_wafer = Conexion.TBL_BOBINADO_CENTER_WAFER.Where(x => x.ID_BOBONADO_CENTER_WAFER == id_bobinado_center_wafer).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(center_wafer).State = System.Data.Entity.EntityState.Deleted;

                    // Retornamos registros afectados
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;            
            }
        }
    }
}