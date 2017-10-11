using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Premaquinado
{
    public class SO_FinishGrind
    {
        #region Constructors
        /// <summary>
        /// Constructor por default.
        /// </summary>
        public SO_FinishGrind()
        {

        }
        #endregion

        #region Methods
        /// <summary>
        /// Método que busca la barra guia 
        /// </summary>
        /// <param name="widthOperation"></param>
        /// <returns></returns>
        public IList GetGuideBar(double widthOperation)
        {
            try
            {
                //Realizamos la conexíon a la Base de datos a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.GuideBarFinGrind
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where widthOperation >= a.WidthMinProceso && widthOperation <= a.WidthMaxProceso && m.Activo == true
                                 select new
                                 {
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
                                     c.VerificacionAnual,
                                     EspesorBarraGuia = a.EspesorBarraGuia
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        } 
        #endregion
    }
}
