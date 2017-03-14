using System;
using System.Linq;
namespace DataAccess.ServiceObjects.Operaciones.Premaquinado
{
    public class SO_SplitterCasting
    {
        #region Propiedades
        #endregion

        #region Constructores
        public SO_SplitterCasting()
        {

        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que obtiene cual es el width de la operación Splitter cuando es un casting.
        /// </summary>
        /// <param name="H1">Double que representa el width nominal del anillo.</param>
        /// <param name="Proceso">Proceso por el cual el usuario eligió se procesará.</param>
        /// <returns>Double que representa el width en la operación splitter cuando el material base es un casting.</returns>
        public double GetWidthSplitterCastings(double H1, string Proceso)
        {

            //Declaramos una variable double que será la que retornemos en el método.
            double widthOperacion = 0;

            //Realizar la consulta con Entity Framework. Tomar como referencia la consutla que
            //se encuentra en el método getWidthSplitterCastings ubicado en la clase DataStore.

            using (var Contexto = new EntitiesPreMaquinado())
            {
                if (Proceso == "Doble")
                {
                    var width = (from a in Contexto.SplitterSpacerChart
                                 where a.Nominal_split == H1
                                 select a.Split_width).FirstOrDefault();

                    widthOperacion = Convert.ToDouble(width);
                }
                else
                {
                    var width = (from a in Contexto.SPlitterSpacerChart2
                                 where a.RingWidth == H1
                                 select a.SplitWidth).FirstOrDefault();

                    widthOperacion = Convert.ToDouble(width);
                }
            }

            //Retornamos el valor obtenido.
            return widthOperacion;
        }

        #endregion
    }
}
