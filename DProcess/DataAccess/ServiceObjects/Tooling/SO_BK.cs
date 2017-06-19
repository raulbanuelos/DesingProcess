using System;
using System.Collections;
using System.Linq;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_BK
    {
        /// <summary>
        /// Método que obtiene el collarin a partir de los valores mínimos y máximos.
        /// </summary>
        /// <param name="maxA"></param>
        /// <param name="minB"></param>
        /// <returns></returns>
        public IList GetCollar(double maxA, double minB)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var Lista = (from a in Conexion.MaestroHerramentales
                                 join b in Conexion.CollarBK on a.Codigo equals b.Codigo
                                 where b.DimA <= maxA && b.DimB >= minB
                                 select new
                                 {
                                     CODIGO = a.Codigo,
                                     DESCRIPCION = a.Descripcion,
                                     DIMA = b.DimA,
                                     DIMB = b.DimB,
                                     PARTE = b.Parte,
                                     PAREDCOLLARIN = b.DimA - b.DimB
                                 }
                                 ).OrderBy(o => o.PAREDCOLLARIN).ToList();

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
        /// Método que obtiene el collarin con las medidas específicas, y que sea diferente en su campo parte.
        /// </summary>
        /// <param name="maxA"></param>
        /// <param name="minB"></param>
        /// <param name="parte"></param>
        /// <returns></returns>
        public IList GetCollar(double maxA, double minB, string parte)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var Lista = (from a in Conexion.MaestroHerramentales
                                 join b in Conexion.CollarBK on a.Codigo equals b.Codigo
                                 where b.DimA == maxA && b.DimB == minB && b.Parte != parte
                                 select new
                                 {
                                     CODIGO = a.Codigo,
                                     DESCRIPCION = a.Descripcion,
                                     PARTE = b.Parte
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
    }
}
