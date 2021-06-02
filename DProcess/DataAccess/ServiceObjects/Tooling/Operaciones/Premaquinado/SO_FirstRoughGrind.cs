using System;
using System.Collections;
using System.Linq;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Premaquinado
{
    public class SO_FirstRoughGrind
    {
        #region Propiedades
        #endregion

        #region Constructores
        public SO_FirstRoughGrind()
        {

        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que obtiene cual es el width de la operación de First Rough Grind cuando es el primer paso.
        /// </summary>
        /// <param name="Proceso">Cadena que representa cual es el proceso que eligió el usuario. (Doble, Sencillo, Cuádruple).</param>
        /// <param name="H1">Double que representa el width nominal del anillo.</param>
        /// <returns>Double que representa el width que será en la operación de First Rough Grind.</returns>
        public double? GetWidthOperacion(string Proceso, double H1)
        {
            double? widthOperacion = 0;

            //Realizar la consulta con Entity Framework. Tomar como referencia la consulta que
            //se encuentra en el método getWidthFirstRoughGrind ubicado en la clase DataStore.

            try
            {
                //Si el Proceso es Doble, la consulta la realizamos en la tabla SplitterSpacerChart
                if (Proceso == "Doble")
                {
                    //Realizamos la consulta a través de EntityFramework.
                    using (var Conexion = new EntitiesTooling())
                    {
                        //Realizamos la consulta y el resultado lo guardamos en la variable local.
                        widthOperacion = (from tabla in Conexion.SplitterSpacerChart
                                            where tabla.Nominal_split == H1 && tabla.Proceso == Proceso
                                            select tabla.Grind_width).First();

                        //Retornamos el resultado de la consulta.
                        return widthOperacion;
                    }
                }
                else
                {
                    //Realizamos la consulta a través de EntityFramework.
                    using (var Conexion = new EntitiesTooling())
                    {
                        //Realizamos la consulta y el resultado lo guardamos en la variable local.
                        widthOperacion = (from tabla in Conexion.SPlitterSpacerChart2
                                            where tabla.RingWidth == H1 && tabla.Proceso == Proceso
                                             select tabla.GrindWidth).First();

                        //Retornamos el resultado de la consulta.
                        return widthOperacion;
                    }
                }
            }
            catch (System.Exception)
            {
                //Si ocurre algún error retornamos un cero.
                return 0;
            }
        }

        public IList GetGuideBarFirstRoughGrind(double A)
        {
            //Realizar la consulta con EntityFramework basando la consulta con el siguiente query
            //SELECT H.Codigo, H.A, M.Descripcion,M.Activo,C.Descripcion AS Clasificacion,C.UnidadMedida,C.Costo,C.CantidadUtilizar,C.VidaUtil,C.idClasificacion, C.ListaCotasRevisar,C.VerificacionAnual
            //--, HE.ID_PLANO,HE.NO_PLANO
            //FROM GuideBarFirstRoughGrind AS H
            //INNER JOIN DBO.MaestroHerramentales AS M ON H.Codigo = M.Codigo
            //INNER JOIN dbo.ClasificacionHerramental AS C ON C.idClasificacion = M.idClasificacionHerramental
            //--LEFT JOIN dbo.PLANO_HERRAMENTAL AS HE ON M.idPlano = HE.ID_PLANO
            //WHERE H.A = .125
            try
            {
                //Realizamos la consulta a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from h in Conexion.GuideBarFirstRoughGrind
                                 join m in Conexion.MaestroHerramentales on h.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where h.A == A && m.Activo == true
                                 select new
                                 {
                                     h.Codigo,
                                     DimA = h.A,
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
            catch (System.Exception)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que inserta un resitro a la tabla GuideBarFirstRoughGrind
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public int SetFirstRG(string codigo, double dimA)
        {
            try
            {
                //Realizamos la consulta a través de EntityFramework.
                using (var conexion = new EntitiesTooling())
                {
                    GuideBarFirstRoughGrind obj = new GuideBarFirstRoughGrind();

                    obj.Codigo = codigo;
                    obj.A = dimA; 

                    //Guardamos los cambios 
                    conexion.GuideBarFirstRoughGrind.Add(obj);
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

        #endregion
    }
}
