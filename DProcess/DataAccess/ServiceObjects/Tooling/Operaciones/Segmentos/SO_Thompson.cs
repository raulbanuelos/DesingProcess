using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Segmentos
{
    public class SO_Thompson
    {
        #region CLAMP PLATE

        /// <summary>
        /// Consulta para traer los datos de un registro ClampPlate por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoClampPlate(string codigo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    //  Realizamos la consulta y el resultado lo asignamos a una variable anónima
                    var Lista = (from a in Conexion.TBL_CLAMP_PLATE_THOMPSON_SEGMENTOS
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     a.ID_CLAMP_PLATE,
                                     a.CODIGO,
                                     a.DIM_B,
                                     a.PARTE,
                                     b.Descripcion,
                                     b.Activo
                                 }).ToList();

                    //  Retornamos el resultado de la consulta
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
        /// Inserción de registros para ClampPlate
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dim_b"></param>
        /// <param name="parte"></param>
        /// <returns></returns>
        public int InsertClampPlate(string codigo, double dim_b, string parte)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_CLAMP_PLATE_THOMPSON_SEGMENTOS Clamp_Plate = new TBL_CLAMP_PLATE_THOMPSON_SEGMENTOS();

                    // Asignamos valores
                    Clamp_Plate.CODIGO = codigo;
                    Clamp_Plate.DIM_B = dim_b;
                    Clamp_Plate.PARTE = parte;

                    // Agregamos el objeto a la tabla
                    Conexion.TBL_CLAMP_PLATE_THOMPSON_SEGMENTOS.Add(Clamp_Plate);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID
                    return Clamp_Plate.ID_CLAMP_PLATE;
                }
            }
            catch (Exception er)
            {
                // Si hay un error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registros ClampPlate
        /// </summary>
        /// <param name="id_clamp_plate"></param>
        /// <param name="codigo"></param>
        /// <param name="dim_b"></param>
        /// <param name="parte"></param>
        /// <returns></returns>
        public int UpdateClampPlate(int id_clamp_plate, string codigo, double dim_b, string parte)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_CLAMP_PLATE_THOMPSON_SEGMENTOS Clamp_Plate = Conexion.TBL_CLAMP_PLATE_THOMPSON_SEGMENTOS.Where(x => x.ID_CLAMP_PLATE == id_clamp_plate).FirstOrDefault();

                    // Asignamos valores
                    Clamp_Plate.ID_CLAMP_PLATE = id_clamp_plate;
                    Clamp_Plate.CODIGO = codigo;
                    Clamp_Plate.DIM_B = dim_b;
                    Clamp_Plate.PARTE = parte;

                    // Modificamos el registro
                    Conexion.Entry(Clamp_Plate).State = System.Data.Entity.EntityState.Modified;

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
        /// Delete de registros ClampPlate
        /// </summary>
        /// <param name="id_clamp_plate"></param>
        /// <returns></returns>
        public int DeleteClampPlate(int id_clamp_plate)
        {
            try
            {
                // Establecemos conexión a través de EntitFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_CLAMP_PLATE_THOMPSON_SEGMENTOS Clamp_Plate = Conexion.TBL_CLAMP_PLATE_THOMPSON_SEGMENTOS.Where(x => x.ID_CLAMP_PLATE == id_clamp_plate).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(Clamp_Plate).State = System.Data.Entity.EntityState.Deleted;

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

        #endregion

        #region BACKUP RING

        /// <summary>
        /// Consulta para traer los datos de un registro BackUPRing por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoBackUPRing(string codigo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Realizamos la consulta y el resultado lo asignamos a una variable anónima
                    var Lista = (from a in Conexion.TBL_BACKUP_RING_THOMPSON_SEGMENTOS
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     a.ID_BACKUP_RING,
                                     a.CODIGO,
                                     a.DIM_A,
                                     a.PARTE,
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
        ///  Inserción de registros para BackUPRing
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dim_a"></param>
        /// <param name="parte"></param>
        /// <returns></returns>
        public int InsertBackUPRing(string codigo, double dim_a, string parte)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BACKUP_RING_THOMPSON_SEGMENTOS BackUP_Ring = new TBL_BACKUP_RING_THOMPSON_SEGMENTOS();

                    // Asignamos valores
                    BackUP_Ring.CODIGO = codigo;
                    BackUP_Ring.DIM_A = dim_a;
                    BackUP_Ring.PARTE = parte;

                    // Agregamos el objeto a la tabla
                    Conexion.TBL_BACKUP_RING_THOMPSON_SEGMENTOS.Add(BackUP_Ring);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID
                    return BackUP_Ring.ID_BACKUP_RING;
                }
            }
            catch (Exception er)
            {
                // Si hay un error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registros BackUPRing
        /// </summary>
        /// <param name="id_backup_ring"></param>
        /// <param name="codigo"></param>
        /// <param name="dim_a"></param>
        /// <param name="parte"></param>
        /// <returns></returns>
        public int UpdateBackUPRing(int id_backup_ring, string codigo, double dim_a, string parte)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BACKUP_RING_THOMPSON_SEGMENTOS BackUP_Ring = Conexion.TBL_BACKUP_RING_THOMPSON_SEGMENTOS.Where(x => x.ID_BACKUP_RING == id_backup_ring).FirstOrDefault();

                    // Asignamos valores
                    BackUP_Ring.ID_BACKUP_RING = id_backup_ring;
                    BackUP_Ring.CODIGO = codigo;
                    BackUP_Ring.DIM_A = dim_a;
                    BackUP_Ring.PARTE = parte;

                    // Modificamos el registro
                    Conexion.Entry(BackUP_Ring).State = System.Data.Entity.EntityState.Modified;

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
        /// Delete de registros BackUPRing
        /// </summary>
        /// <param name="id_backup_ring"></param>
        /// <returns></returns>
        public int DeleteBackUPRing(int id_backup_ring)
        {
            try
            {
                // Establecemos conexión a través de EntitFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_BACKUP_RING_THOMPSON_SEGMENTOS BackUP_Ring = Conexion.TBL_BACKUP_RING_THOMPSON_SEGMENTOS.Where(x => x.ID_BACKUP_RING == id_backup_ring).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(BackUP_Ring).State = System.Data.Entity.EntityState.Deleted;

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

        #endregion

        #region PLATO EMPUJADOR

        /// <summary>
        /// Consulta para traer los datos de un registro PlatoEmpujador por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoPlatoEmpujador(string codigo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Realizamos la consulta y el resultado lo asignamos a una variable anónima
                    var Lista = (from a in Conexion.TBL_PLATO_EMPUJADOR_THOMPSON_SEGMENTOS
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     a.ID_PLATO_EMPUJADOR,
                                     a.CODIGO,
                                     a.DIM_A,
                                     a.PARTE,
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
        /// Inserción de registros para PlatoEmpujador
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dim_a"></param>
        /// <param name="parte"></param>
        /// <returns></returns>
        public int InsertPlatoEmpujador(string codigo, double dim_a, string parte)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_PLATO_EMPUJADOR_THOMPSON_SEGMENTOS Plato_Empujador = new TBL_PLATO_EMPUJADOR_THOMPSON_SEGMENTOS();

                    // Asignamos valores
                    Plato_Empujador.CODIGO = codigo;
                    Plato_Empujador.DIM_A = dim_a;
                    Plato_Empujador.PARTE = parte;

                    // Agregamos el objeto a la tabla
                    Conexion.TBL_PLATO_EMPUJADOR_THOMPSON_SEGMENTOS.Add(Plato_Empujador);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID
                    return Plato_Empujador.ID_PLATO_EMPUJADOR;              
                }
            }
            catch (Exception er)
            {
                //  Si hay un error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registros PlatoEmpujador
        /// </summary>
        /// <param name="id_plato_empujador"></param>
        /// <param name="codigo"></param>
        /// <param name="dim_a"></param>
        /// <param name="parte"></param>
        /// <returns></returns>
        public int UpdatePlatoEmpujador(int id_plato_empujador, string codigo, double dim_a, string parte)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_PLATO_EMPUJADOR_THOMPSON_SEGMENTOS Plato_Empujador = Conexion.TBL_PLATO_EMPUJADOR_THOMPSON_SEGMENTOS.Where(x => x.ID_PLATO_EMPUJADOR == id_plato_empujador).FirstOrDefault();

                    // Asignamos valores
                    Plato_Empujador.ID_PLATO_EMPUJADOR = id_plato_empujador;
                    Plato_Empujador.CODIGO = codigo;
                    Plato_Empujador.DIM_A = dim_a;
                    Plato_Empujador.PARTE = parte;

                    // Modificamos el registro
                    Conexion.Entry(Plato_Empujador).State = System.Data.Entity.EntityState.Modified;

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
        /// Delete de registros PlatoEmpujador
        /// </summary>
        /// <param name="id_plato_empujador"></param>
        /// <returns></returns>
        public int DeletePlatoEmpujador(int id_plato_empujador)
        {
            try
            {
                // Establecemos conexión a través de EntitFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_PLATO_EMPUJADOR_THOMPSON_SEGMENTOS Plato_Empujador = Conexion.TBL_PLATO_EMPUJADOR_THOMPSON_SEGMENTOS.Where(x => x.ID_PLATO_EMPUJADOR == id_plato_empujador).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(Plato_Empujador).State = System.Data.Entity.EntityState.Deleted;

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

        #endregion

        #region TUBO ENROLLADOR

        /// <summary>
        /// Consulta para traer los datos de un registro TuboEnrollador por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoTuboEnrollador(string codigo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Realizamos la consulta y el resultado lo asignamos a una variable anónima
                    var Lista = (from a in Conexion.TBL_TUBO_ENROLLADOR_THOMPSON_SEGMENTOS
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     a.ID_TUBO_ENROLLADOR,
                                     a.CODIGO,
                                     a.DIM_A,
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
        /// Inserción de registros para TuboEnrollador
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dim_a"></param>
        /// <returns></returns>
        public int InsertTuboEnrollador(string codigo, double dim_a)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_TUBO_ENROLLADOR_THOMPSON_SEGMENTOS Tubo_Enrollador = new TBL_TUBO_ENROLLADOR_THOMPSON_SEGMENTOS();

                    // Asignamos valores
                    Tubo_Enrollador.CODIGO = codigo;
                    Tubo_Enrollador.DIM_A = dim_a;

                    // Agregamos el objeto a la tabla
                    Conexion.TBL_TUBO_ENROLLADOR_THOMPSON_SEGMENTOS.Add(Tubo_Enrollador);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID
                    return Tubo_Enrollador.ID_TUBO_ENROLLADOR;
                }
            }
            catch (Exception er)
            {
                // Si hay un error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registros TuboEnrollador
        /// </summary>
        /// <param name="id_tubo_enrollador"></param>
        /// <param name="codigo"></param>
        /// <param name="dim_a"></param>
        /// <returns></returns>
        public int UpdateTuboEnrollador(int id_tubo_enrollador, string codigo, double dim_a)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_TUBO_ENROLLADOR_THOMPSON_SEGMENTOS Tubo_Enrollador = Conexion.TBL_TUBO_ENROLLADOR_THOMPSON_SEGMENTOS.Where(x => x.ID_TUBO_ENROLLADOR == id_tubo_enrollador).FirstOrDefault();

                    // Asignamos valores
                    Tubo_Enrollador.ID_TUBO_ENROLLADOR = id_tubo_enrollador;
                    Tubo_Enrollador.CODIGO = codigo;
                    Tubo_Enrollador.DIM_A = dim_a;

                    // Modificamos el registro
                    Conexion.Entry(Tubo_Enrollador).State = System.Data.Entity.EntityState.Modified;

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
        /// Delete de registros TuboEnrollador
        /// </summary>
        /// <param name="id_tubo_enrollador"></param>
        /// <returns></returns>
        public int DeleteTuboEnrollador(int id_tubo_enrollador)
        {
            try
            {
                // Establecemos conexión a través de EntitFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_TUBO_ENROLLADOR_THOMPSON_SEGMENTOS Tubo_Enrollador = Conexion.TBL_TUBO_ENROLLADOR_THOMPSON_SEGMENTOS.Where(x => x.ID_TUBO_ENROLLADOR == id_tubo_enrollador).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(Tubo_Enrollador).State = System.Data.Entity.EntityState.Deleted;

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

        #endregion
    }
}
