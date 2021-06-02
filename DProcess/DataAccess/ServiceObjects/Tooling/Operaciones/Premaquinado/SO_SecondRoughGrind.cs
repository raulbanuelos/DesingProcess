using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Premaquinado
{
    public class SO_SecondRoughGrind
    {
        /// <summary>
        /// Método el cual obtiene el herramental GuideBar.
        /// </summary>
        /// <param name="widthOperacion"></param>
        /// <returns></returns>
        public IList GetGuideBar(double widthOperacion)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.GuideBarSecondRoughGrind
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where widthOperacion >= a.WidthMinProceso && widthOperacion <= a.WidthMaxProceso && m.Activo == true
                                 select new
                                 {
                                     a.EspesorBarraGuia,
                                     a.Codigo,
                                     m.Descripcion,
                                     m.Activo,
                                     Clasificacion = c.Descripcion,
                                     c.UnidadMedida,
                                     c.Costo,
                                     c.CantidadUtilizar,
                                     c.VidaUtil,
                                     c.idClasificacion,
                                     c.ListaCotasRevisar,
                                     c.VerificacionAnual
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }
        /// <summary>
        /// Método que inserta un resitro a la tabla GuideBarSecondRoughGrind
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public int SetSecondRG(string codigo, double min,double max, double espesor)
        {
            try
            {
                //Realizamos la consulta a través de EntityFramework.
                using (var conexion = new EntitiesTooling())
                {
                    GuideBarSecondRoughGrind obj = new GuideBarSecondRoughGrind();

                    obj.Codigo = codigo;
                    obj.WidthMinProceso = min;
                    obj.WidthMaxProceso = max;
                    obj.EspesorBarraGuia = espesor;

                    //Guardamos los cambios 
                    conexion.GuideBarSecondRoughGrind.Add(obj);
                    conexion.SaveChanges();

                    return obj.ID_GUIDE_BAR;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos 0
                return 0;
            }
        }
    }
}
