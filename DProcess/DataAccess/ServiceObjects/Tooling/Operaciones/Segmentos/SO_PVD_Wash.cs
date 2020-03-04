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
        /// Función que retorna las posibles opciones de mesa de acuerdo al rango técnicamente posible.
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

        #region Manga PVD Wash

        /// <summary>
        /// Método que retorna la manga dependiendo de la dim A
        /// </summary>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public IList GetManga(double dimA)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var lista = (from a in Conexion.TBL_MANGA_PVD_WASH
                                 join m in Conexion.MaestroHerramentales on a.CODIGO equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where a.DIM_A == dimA
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
                                     DIM_A = a.DIM_A
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
        /// Consulta para traer los datos de un registro Manga PVD Wash por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoMangaPVDWash(string codigo)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Realizamos la consulta
                    var Lista = (from a in Conexion.TBL_MANGA_PVD_WASH
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     a.ID_MANGA_PVD_WASH,
                                     a.CODIGO,
                                     a.DIM_A,
                                     a.DIM_D,
                                     b.Descripcion,
                                     b.Activo
                                 }).ToList();

                    // Retornamos la lista
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
        /// Inserción de registros Manga PVD Wash
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dim_a"></param>
        /// <param name="dim_d"></param>
        /// <returns></returns>
        public int InsertMangaPVDWash(string codigo, double dim_a, double dim_d)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_MANGA_PVD_WASH manga_wash = new TBL_MANGA_PVD_WASH();

                    // Asignamos los valores
                    manga_wash.CODIGO = codigo;
                    manga_wash.DIM_A = dim_a;
                    manga_wash.DIM_D = dim_d;

                    // Insertamos el objeto
                    Conexion.TBL_MANGA_PVD_WASH.Add(manga_wash);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID del objeto insertado
                    return manga_wash.ID_MANGA_PVD_WASH;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registros Manga PVD Wash
        /// </summary>
        /// <param name="id_manga_wash"></param>
        /// <param name="codigo"></param>
        /// <param name="dim_a"></param>
        /// <param name="dim_d"></param>
        /// <returns></returns>
        public int UpdateMangaPVDWash(int id_manga_wash, string codigo, double dim_a, double dim_d)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_MANGA_PVD_WASH manga_wash = Conexion.TBL_MANGA_PVD_WASH.Where(x => x.ID_MANGA_PVD_WASH == id_manga_wash).FirstOrDefault();

                    // Asignamos valores
                    manga_wash.ID_MANGA_PVD_WASH = id_manga_wash;
                    manga_wash.CODIGO = codigo;
                    manga_wash.DIM_A = dim_a;
                    manga_wash.DIM_D = dim_d;

                    // Modificamos el registro
                    Conexion.Entry(manga_wash).State = System.Data.Entity.EntityState.Modified;

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
        ///  Delete de registros Manga PVD Wash
        /// </summary>
        /// <param name="id_manga_wash"></param>
        /// <returns></returns>
        public int DeleteMangaPVDWash(int id_manga_wash)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_MANGA_PVD_WASH manga_wash = Conexion.TBL_MANGA_PVD_WASH.Where(x => x.ID_MANGA_PVD_WASH == id_manga_wash).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(manga_wash).State = System.Data.Entity.EntityState.Deleted;

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
        
        #endregion
    }
}