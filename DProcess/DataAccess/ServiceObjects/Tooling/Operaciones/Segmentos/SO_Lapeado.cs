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
                                                  MEDIDA_MANGA = a.MEDIDA
                                              }).Take(1).ToList();

                    return listaHerramentales;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Consulta para traer los datos de un registro MangaLapeado por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoMangaLapeado(string codigo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Realizamos la consulta y el resultado lo asignamos a una variable anónima
                    var Lista = (from a in Conexion.TBL_MANGA_LAPEADO_SEGMENTOS
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     a.ID_MANGA_LAPEADO_SEGMENTOS,
                                     a.CODIGO,
                                     a.MEDIDA,
                                     a.MEDIDA_DECIMAL,
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
        /// Inserción de registros Manga Lapeado
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="medida"></param>
        /// <param name="medida_decimal"></param>
        /// <returns></returns>
        public int InsertMangaLapeado(string codigo, string medida, double medida_decimal)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_MANGA_LAPEADO_SEGMENTOS manga_lapeado = new TBL_MANGA_LAPEADO_SEGMENTOS();

                    // Asignamos los valores
                    manga_lapeado.CODIGO = codigo;
                    manga_lapeado.MEDIDA = medida;
                    manga_lapeado.MEDIDA_DECIMAL = medida_decimal;

                    // Agregamos el objeto a la tabla
                    Conexion.TBL_MANGA_LAPEADO_SEGMENTOS.Add(manga_lapeado);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID del objeto
                    return manga_lapeado.ID_MANGA_LAPEADO_SEGMENTOS;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualización de registros Manga Lapeado
        /// </summary>
        /// <param name="id_manga_lapeado_segmentos"></param>
        /// <param name="codigo"></param>
        /// <param name="medida"></param>
        /// <param name="medida_decimal"></param>
        /// <returns></returns>
        public int UpdateMangaLapeado(int id_manga_lapeado_segmentos, string codigo, string medida, double medida_decimal)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_MANGA_LAPEADO_SEGMENTOS manga_lapeado = Conexion.TBL_MANGA_LAPEADO_SEGMENTOS.Where(X => X.ID_MANGA_LAPEADO_SEGMENTOS == id_manga_lapeado_segmentos).FirstOrDefault();

                    // Asignamos valores
                    manga_lapeado.ID_MANGA_LAPEADO_SEGMENTOS = id_manga_lapeado_segmentos;
                    manga_lapeado.CODIGO = codigo;
                    manga_lapeado.MEDIDA = medida;
                    manga_lapeado.MEDIDA_DECIMAL = medida_decimal;

                    // Modificamos el registro
                    Conexion.Entry(manga_lapeado).State = System.Data.Entity.EntityState.Modified;

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
        /// Eliminación de registros Manga Lapeado
        /// </summary>
        /// <param name="id_manga_lapeado_segmentos"></param>
        /// <returns></returns>
        public int DeleteMangaLapeado(int id_manga_lapeado_segmentos)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_MANGA_LAPEADO_SEGMENTOS manga_lapeado = Conexion.TBL_MANGA_LAPEADO_SEGMENTOS.Where(x => x.ID_MANGA_LAPEADO_SEGMENTOS == id_manga_lapeado_segmentos).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(manga_lapeado).State = System.Data.Entity.EntityState.Deleted;

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

        /// <summary>
        /// Consulta para trear los datos de un registro RubberLapeado por su código
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoRubberLapeado(string codigo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Realizamos la consulta y el resultado lo asignamos a una variable anónima
                    var Lista = (from a in Conexion.TBL_RUBBER_LAPEADO_SEGMENTOS
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     a.ID_RUBBER_LAPEADO_SEGMENTOS,
                                     a.CODIGO,
                                     a.DIM_A,
                                     a.PLANO,
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
        /// Inserción de registros Rubber Lapeado
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dim_a"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int InsertRubberLapeado(string codigo, double dim_a, string plano)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Delcaramos el objeto de la lista
                    TBL_RUBBER_LAPEADO_SEGMENTOS Rubber_Lapeado = new TBL_RUBBER_LAPEADO_SEGMENTOS();

                    // Asignamos los valores
                    Rubber_Lapeado.CODIGO = codigo;
                    Rubber_Lapeado.DIM_A = dim_a;
                    Rubber_Lapeado.PLANO = plano;

                    // Agregamos el objeto a la tabla
                    Conexion.TBL_RUBBER_LAPEADO_SEGMENTOS.Add(Rubber_Lapeado);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID del objeto
                    return Rubber_Lapeado.ID_RUBBER_LAPEADO_SEGMENTOS;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Actualizar registros Rubber Lapeado
        /// </summary>
        /// <param name="id_rubber_lapeado_segmentos"></param>
        /// <param name="codigo"></param>
        /// <param name="dim_a"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int UpdateRubberLapeado(int id_rubber_lapeado_segmentos, string codigo, double dim_a, string plano)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_RUBBER_LAPEADO_SEGMENTOS Rubber_Lapeado = Conexion.TBL_RUBBER_LAPEADO_SEGMENTOS.Where(x => x.ID_RUBBER_LAPEADO_SEGMENTOS == id_rubber_lapeado_segmentos).FirstOrDefault();

                    // Asignamos valores
                    Rubber_Lapeado.ID_RUBBER_LAPEADO_SEGMENTOS = id_rubber_lapeado_segmentos;
                    Rubber_Lapeado.CODIGO = codigo;
                    Rubber_Lapeado.DIM_A = dim_a;
                    Rubber_Lapeado.PLANO = plano;

                    // Modificamos el registro
                    Conexion.Entry(Rubber_Lapeado).State = System.Data.Entity.EntityState.Modified;

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
        /// Eliminar registros Rubber Lapeado
        /// </summary>
        /// <param name="id_rubber_lapeado_segmentos"></param>
        /// <returns></returns>
        public int DeleteRubberLapeado(int id_rubber_lapeado_segmentos)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la lista
                    TBL_RUBBER_LAPEADO_SEGMENTOS Rubber_Lapeado = Conexion.TBL_RUBBER_LAPEADO_SEGMENTOS.Where(x => x.ID_RUBBER_LAPEADO_SEGMENTOS == id_rubber_lapeado_segmentos).FirstOrDefault();

                    // Eliminamos el registro
                    Conexion.Entry(Rubber_Lapeado).State = System.Data.Entity.EntityState.Deleted;

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
