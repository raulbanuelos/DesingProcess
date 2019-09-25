using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_CriteriosSegmentos_
    {
        /// <summary>
        /// Método para insertar un registro a la tabla CriteriosSegmentos_
        /// </summary>
        /// <returns></returns>
        public int SetCriteriosSegmentos_(double mpaxialWidthMinPVD, double mpaxialWidthMaxPVD, double mpradialThickMinPVD, double mpradialThickMaxPVD, double mpaxialWidthMin, double mpaxialWidthMax, double mpradialThickMin, double mpradialThickMax, double mpradialCromoMin,
                                            double mpradialCromoMax, double discoMin, double discoMax, double cromoServicio, double cromoEO, double cromoFreeGapMin, double cromoFreeGapMax, double nitruFreeGapMin, double nitruFreeGapMax, double nitru2FreeGapMin, double nitru2FreeGapMax, double freeGapSinCromoMin,
                                            double freeGapSinCromoMax, double centerWaferH1Min, double centerWaferH1Max, double cromoCollarinMin, double cromoCollarinMax, double mangaNormMin, double mangaNormMax, double mangaNormAntesMin, double mangaNormAntesMax, double thompsonGapMin, double thompsonGapMax,
                                            double thompClampMin, double thompClampMax, double thompBackUpMin, double thompBackUpMax, double thompPlatoMin, double thompPlatoMax, double vulcanFrontCollarMin, double vulcanFrontCollarMax, double vulcanBackCollarMin, double vulcanBackCollarMax, double vulcanPlungerMin,
                                            double vulcanPlungerMax, double lapRubberSleeveMin, double lapRubberSleeveMax, double scotchMangaCMin, double scotchMangaCMax, double scotchMangaDMin, double scotchMangaDMax, double scotchMangaFMin, double scotchMangaFMax, double scotchMangaAMin, double scotchMangaAMax,
                                            double scotchMangaBMin, double scotchMangaBMax, double barrelBushingD1Min, double barrelBushingD1Max, double barrelPusherD1Min, double barrelPusherD1Max, double nitruLayerMin, double nitruLayerMax, double anilloPatronMin, double anilloPatronMax, double cromoIntCollarMin, double cromoIntCollarMax)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    // Declaramos el objeto de la tabla
                    CriteriosSegmentos_ obj = new CriteriosSegmentos_();

                    //Asignamos los valores
                    obj.MPAxialWidthMinPVD = mpaxialWidthMinPVD;
                    obj.MPAxialWidthMaxPVD = mpaxialWidthMaxPVD;
                    obj.MPRadialThickMinPVD = mpradialThickMinPVD;
                    obj.MPRadialThickMaxPVD = mpradialThickMaxPVD;
                    obj.MPAxialWidthMin = mpaxialWidthMin;
                    obj.MPAxialWidthMax = mpaxialWidthMax;
                    obj.MPRadialThickMin = mpradialThickMin;
                    obj.MPRadialThickMax = mpradialThickMax;
                    obj.MPRadialCromoMin = mpradialCromoMin;
                    obj.MPRadialCromoMax = mpradialCromoMax;
                    obj.DiscoMin = discoMin;
                    obj.DiscoMax = discoMax;
                    obj.CromoServicio = cromoServicio;
                    obj.CromoEO = cromoEO;
                    obj.CromoFreeGapMin = cromoFreeGapMin;
                    obj.CromoFreeGapMax = cromoFreeGapMax;
                    obj.NitruFreeGapMin = nitruFreeGapMin;
                    obj.NitruFreeGapMax = nitruFreeGapMax;
                    obj.Nitru2FreeGapMin = nitru2FreeGapMin;
                    obj.Nitru2FreeGapMax = nitru2FreeGapMax;
                    obj.FreeGapSinCromoMin = freeGapSinCromoMin;
                    obj.FreeGapSinCromoMax = freeGapSinCromoMax;
                    obj.CenterWaferH1Min = centerWaferH1Min;
                    obj.CenterWaferH1Max = centerWaferH1Max;
                    obj.CromoCollarinMin = cromoCollarinMin;
                    obj.CromoCollarinMax = cromoCollarinMax;
                    obj.MangaNormMin = mangaNormMin;
                    obj.MangaNormMax = mangaNormMax;
                    obj.MangaNormAntesMin = mangaNormAntesMin;
                    obj.MangaNormAntesMax = mangaNormAntesMax;
                    obj.ThompsonGapMin = thompsonGapMin;
                    obj.ThompsonGapMax = thompsonGapMax;
                    obj.ThompClampMin = thompClampMin;
                    obj.ThompClampMax = thompClampMax;
                    obj.ThompBackUpMin = thompBackUpMin;
                    obj.ThompBackUpMax = thompBackUpMax;
                    obj.ThompPlatoMin = thompPlatoMin;
                    obj.ThompPlatoMax = thompPlatoMax;
                    obj.VulcanFrontCollarMin = vulcanFrontCollarMin;
                    obj.VulcanPlungerMax = vulcanFrontCollarMax;
                    obj.VulcanBackCollarMin = vulcanBackCollarMin;
                    obj.VulcanBackCollarMax = vulcanBackCollarMax;
                    obj.VulcanPlungerMin = vulcanPlungerMin;
                    obj.VulcanPlungerMax = vulcanPlungerMax;
                    obj.LapRubberSleeveMin = lapRubberSleeveMin;
                    obj.LapRubberSleeveMax = lapRubberSleeveMax;
                    obj.ScotchMangaCMin = scotchMangaCMin;
                    obj.ScotchMangaCMax = scotchMangaCMax;
                    obj.ScotchMangaDMin = scotchMangaDMin;
                    obj.ScotchMangaDMax = scotchMangaDMax;
                    obj.ScotchMangaFMin = scotchMangaFMin;
                    obj.ScotchMangaFMax = scotchMangaFMax;
                    obj.ScotchMangaAMin = scotchMangaAMin;
                    obj.ScotchMangaAMax = scotchMangaAMax;
                    obj.ScotchMangaBMin = scotchMangaBMin;
                    obj.ScotchMangaBMax = scotchMangaBMax;
                    obj.BarrelBushingD1Min = barrelBushingD1Min;
                    obj.BarrelBushingD1Max = barrelBushingD1Max;
                    obj.BarrelPusherD1Min = barrelPusherD1Min;
                    obj.BarrelPusherD1Max = barrelPusherD1Max;
                    obj.NitruLayerMin = nitruLayerMin;
                    obj.NitruLayerMax = nitruLayerMax;
                    obj.AnilloPatronMax = anilloPatronMax;
                    obj.AnilloPatronMin = anilloPatronMin;
                    obj.CromoIntCollarMin = cromoIntCollarMin;
                    obj.CromoIntCollarMax = cromoIntCollarMax;

                    //Agregar el objeto a la tabla
                    Conexion.CriteriosSegmentos_.Add(obj);
                    //Guardamos los cambios
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.ID_CRITERIO_SEGMENTO;
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar un registro de la tabla CriteriosSegmentos_
        /// </summary>
        /// <returns></returns>
        public int UpdateCriteriosSegmentos(int id_criterio_segmento, double mpaxialWidthMinPVD, double mpaxialWidthMaxPVD, double mpradialThickMinPVD, double mpradialThickMaxPVD, double mpaxialWidthMin, double mpaxialWidthMax, double mpradialThickMin, double mpradialThickMax, double mpradialCromoMin,
                                            double mpradialCromoMax, double discoMin, double discoMax, double cromoServicio, double cromoEO, double cromoFreeGapMin, double cromoFreeGapMax, double nitruFreeGapMin, double nitruFreeGapMax, double nitru2FreeGapMin, double nitru2FreeGapMax, double freeGapSinCromoMin,
                                            double freeGapSinCromoMax, double centerWaferH1Min, double centerWaferH1Max, double cromoCollarinMin, double cromoCollarinMax, double mangaNormMin, double mangaNormMax, double mangaNormAntesMin, double mangaNormAntesMax, double thompsonGapMin, double thompsonGapMax,
                                            double thompClampMin, double thompClampMax, double thompBackUpMin, double thompBackUpMax, double thompPlatoMin, double thompPlatoMax, double vulcanFrontCollarMin, double vulcanFrontCollarMax, double vulcanBackCollarMin, double vulcanBackCollarMax, double vulcanPlungerMin,
                                            double vulcanPlungerMax, double lapRubberSleeveMin, double lapRubberSleeveMax, double scotchMangaCMin, double scotchMangaCMax, double scotchMangaDMin, double scotchMangaDMax, double scotchMangaFMin, double scotchMangaFMax, double scotchMangaAMin, double scotchMangaAMax,
                                            double scotchMangaBMin, double scotchMangaBMax, double barrelBushingD1Min, double barrelBushingD1Max, double barrelPusherD1Min, double barrelPusherD1Max, double nitruLayerMin, double nitruLayerMax, double anilloPatronMin, double anilloPatronMax, double cromoIntCollarMin, double cromoIntCollarMax)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Declaramos el objeto de la tabla.
                    CriteriosSegmentos_ obj = Conexion.CriteriosSegmentos_.Where(x => x.ID_CRITERIO_SEGMENTO == id_criterio_segmento).FirstOrDefault();

                    //Asignamos los valores
                    obj.ID_CRITERIO_SEGMENTO = id_criterio_segmento;
                    obj.MPAxialWidthMinPVD = mpaxialWidthMinPVD;
                    obj.MPAxialWidthMaxPVD = mpaxialWidthMaxPVD;
                    obj.MPRadialThickMinPVD = mpradialThickMinPVD;
                    obj.MPRadialThickMaxPVD = mpradialThickMaxPVD;
                    obj.MPAxialWidthMin = mpaxialWidthMin;
                    obj.MPAxialWidthMax = mpaxialWidthMax;
                    obj.MPRadialThickMin = mpradialThickMin;
                    obj.MPRadialThickMax = mpradialThickMax;
                    obj.MPRadialCromoMin = mpradialCromoMin;
                    obj.MPRadialCromoMax = mpradialCromoMax;
                    obj.DiscoMin = discoMin;
                    obj.DiscoMax = discoMax;
                    obj.CromoServicio = cromoServicio;
                    obj.CromoEO = cromoEO;
                    obj.CromoFreeGapMin = cromoFreeGapMin;
                    obj.CromoFreeGapMax = cromoFreeGapMax;
                    obj.NitruFreeGapMin = nitruFreeGapMin;
                    obj.NitruFreeGapMax = nitruFreeGapMax;
                    obj.Nitru2FreeGapMin = nitru2FreeGapMin;
                    obj.Nitru2FreeGapMax = nitru2FreeGapMax;
                    obj.FreeGapSinCromoMin = freeGapSinCromoMin;
                    obj.FreeGapSinCromoMax = freeGapSinCromoMax;
                    obj.CenterWaferH1Min = centerWaferH1Min;
                    obj.CenterWaferH1Max = centerWaferH1Max;
                    obj.CromoCollarinMin = cromoCollarinMin;
                    obj.CromoCollarinMax = cromoCollarinMax;
                    obj.MangaNormMin = mangaNormMin;
                    obj.MangaNormMax = mangaNormMax;
                    obj.MangaNormAntesMin = mangaNormAntesMin;
                    obj.MangaNormAntesMax = mangaNormAntesMax;
                    obj.ThompsonGapMin = thompsonGapMin;
                    obj.ThompsonGapMax = thompsonGapMax;
                    obj.ThompClampMin = thompClampMin;
                    obj.ThompClampMax = thompClampMax;
                    obj.ThompBackUpMin = thompBackUpMin;
                    obj.ThompBackUpMax = thompBackUpMax;
                    obj.ThompPlatoMin = thompPlatoMin;
                    obj.ThompPlatoMax = thompPlatoMax;
                    obj.VulcanFrontCollarMin = vulcanFrontCollarMin;
                    obj.VulcanPlungerMax = vulcanFrontCollarMax;
                    obj.VulcanBackCollarMin = vulcanBackCollarMin;
                    obj.VulcanBackCollarMax = vulcanBackCollarMax;
                    obj.VulcanPlungerMin = vulcanPlungerMin;
                    obj.VulcanPlungerMax = vulcanPlungerMax;
                    obj.LapRubberSleeveMin = lapRubberSleeveMin;
                    obj.LapRubberSleeveMax = lapRubberSleeveMax;
                    obj.ScotchMangaCMin = scotchMangaCMin;
                    obj.ScotchMangaCMax = scotchMangaCMax;
                    obj.ScotchMangaDMin = scotchMangaDMin;
                    obj.ScotchMangaDMax = scotchMangaDMax;
                    obj.ScotchMangaFMin = scotchMangaFMin;
                    obj.ScotchMangaFMax = scotchMangaFMax;
                    obj.ScotchMangaAMin = scotchMangaAMin;
                    obj.ScotchMangaAMax = scotchMangaAMax;
                    obj.ScotchMangaBMin = scotchMangaBMin;
                    obj.ScotchMangaBMax = scotchMangaBMax;
                    obj.BarrelBushingD1Min = barrelBushingD1Min;
                    obj.BarrelBushingD1Max = barrelBushingD1Max;
                    obj.BarrelPusherD1Min = barrelPusherD1Min;
                    obj.BarrelPusherD1Max = barrelPusherD1Max;
                    obj.NitruLayerMin = nitruLayerMin;
                    obj.NitruLayerMax = nitruLayerMax;
                    obj.AnilloPatronMax = anilloPatronMax;
                    obj.AnilloPatronMin = anilloPatronMin;
                    obj.CromoIntCollarMin = cromoIntCollarMin;
                    obj.CromoIntCollarMax = cromoIntCollarMax;

                    //Guardamos los cambios
                    Conexion.Entry(obj).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla CriteriosSegmentos_
        /// </summary>
        /// <returns></returns>
        public int DeleteCriteriosSegmentos_(int id_criterio_segmento)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    CriteriosSegmentos_ obj = Conexion.CriteriosSegmentos_.Where(x => x.ID_CRITERIO_SEGMENTO == id_criterio_segmento).FirstOrDefault();

                    Conexion.Entry(obj).State = EntityState.Deleted;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList GetCriteriosSegmentos()
        {
            try
            {
                //Establecemos la conexíon a través de Entity Framework
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Se realiza la consulta
                    var Lista = (from c in Conexion.CriteriosSegmentos_
                                 select new
                                 {
                                     c.ID_CRITERIO_SEGMENTO,
                                     c.MPAxialWidthMinPVD,
                                     c.MPAxialWidthMaxPVD,
                                     c.MPRadialThickMinPVD,
                                     c.MPRadialThickMaxPVD,
                                     c.MPAxialWidthMin,
                                     c.MPAxialWidthMax,
                                     c.MPRadialThickMin,
                                     c.MPRadialThickMax,
                                     c.MPRadialCromoMin,
                                     c.MPRadialCromoMax,
                                     c.DiscoMin,
                                     c.DiscoMax,
                                     c.CromoServicio,
                                     c.CromoEO,
                                     c.CromoFreeGapMin,
                                     c.CromoFreeGapMax,
                                     c.NitruFreeGapMin,
                                     c.NitruFreeGapMax,
                                     c.Nitru2FreeGapMin,
                                     c.Nitru2FreeGapMax,
                                     c.FreeGapSinCromoMin,
                                     c.FreeGapSinCromoMax,
                                     c.CenterWaferH1Min,
                                     c.CenterWaferH1Max,
                                     c.CromoCollarinMin,
                                     c.CromoCollarinMax,
                                     c.MangaNormMin,
                                     c.MangaNormMax,
                                     c.MangaNormAntesMin,
                                     c.MangaNormAntesMax,
                                     c.ThompsonGapMin,
                                     c.ThompsonGapMax,
                                     c.ThompClampMin,
                                     c.ThompClampMax,
                                     c.ThompBackUpMin,
                                     c.ThompBackUpMax,
                                     c.ThompPlatoMin,
                                     c.ThompPlatoMax,
                                     c.VulcanFrontCollarMin,
                                     c.VulcanFrontCollarMax,
                                     c.VulcanBackCollarMin,
                                     c.VulcanBackCollarMax,
                                     c.VulcanPlungerMin,
                                     c.VulcanPlungerMax,
                                     c.LapRubberSleeveMin,
                                     c.LapRubberSleeveMax,
                                     c.ScotchMangaCMin,
                                     c.ScotchMangaCMax,
                                     c.ScotchMangaDMin,
                                     c.ScotchMangaDMax,
                                     c.ScotchMangaFMin,
                                     c.ScotchMangaFMax,
                                     c.ScotchMangaAMin,
                                     c.ScotchMangaAMax,
                                     c.ScotchMangaBMin,
                                     c.ScotchMangaBMax,
                                     c.BarrelBushingD1Min,
                                     c.BarrelBushingD1Max,
                                     c.BarrelPusherD1Min,
                                     c.BarrelPusherD1Max,
                                     c.NitruLayerMin,
                                     c.NitruLayerMax,
                                     c.AnilloPatronMin,
                                     c.AnilloPatronMax,
                                     c.CromoIntCollarMin,
                                     c.CromoIntCollarMax,

                                 }).ToList();

                    //Retornamos el resultado de la consulta
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay algún error, se retorna nulo.
                return null;
            }
        }
    }
}
