using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
namespace DataAccess.ServiceObjects.Tooling.Operaciones.Premaquinado
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

            using (var Contexto = new EntitiesTooling())
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

        /// <summary>
        /// Método 
        /// </summary>
        /// <param name="EspecificacionMaterial"></param>
        /// <returns></returns>
        public DataSet GetCycleTime(string EspecificacionMaterial)
        {
            try
            {
                //Declaramos un objeto de tipo DataSet que será el que guarde los resultados de la consulta.
                DataSet datos = null;

                //Declaramos un objeto con el cual nos permitira conectarnos hacia la base de datos.
                Desing_SQL conexion = new Desing_SQL();

                //Declaramos un diccionario en el cual guardaremos los parámetros que requiere el procedimiento.
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                //Agregamos los parámertros necesarios del procedimiento.
                parametros.Add("EspecificacionMaterial",EspecificacionMaterial);

                //LLamamos al método para ejecutar el procedimiento, el resultado lo asignamos a la variable local.
                datos = conexion.EjecutarStoredProcedure("SP_RGP_GetCycleTimeSplitterCasting",parametros);

                //Retornamos el resultado de la consulta.
                return datos;

            }
            catch (Exception)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }
        
        /// <summary>
        /// Método el cual obtiene el herramental spacer ideal.
        /// </summary>
        /// <param name="proceso"></param>
        /// <param name="spacerMin"></param>
        /// <param name="spacerMax"></param>
        /// <returns></returns>
        public IList GetSpacer(double spacerMin, double spacerMax)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.CutterSpacerSplitter
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where a.B >= spacerMin && a.B <= spacerMax && m.Activo == true
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
                                     c.VerificacionAnual
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la medida ideal del spacer.
        /// </summary>
        /// <param name="proceso"></param>
        /// <param name="h1"></param>
        /// <returns></returns>
        public IList GetMedidaSpacer(string proceso, double h1)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Comparamos si el proceso es Doble la consulta la realizamos en la tabla SplitterSpacerChart
                    if (proceso == "Doble")
                    {
                        //Realizamos la consulta, el resultado lo asignamos a una variable anónima.
                        var Lista = (from a in Conexion.SplitterSpacerChart
                                     where a.Nominal_split == h1 && a.Proceso == proceso
                                     select new
                                     {
                                         Cutter_Spacer = a.Cutter_spacer
                                     }).ToList();

                        //Retornamos el resultado de la consulta.
                        return Lista;
                    }
                    else
                    {
                        //Realizamos la consulta, el resultado lo asignamos a una variable anónima.
                        var Lista = (from a in Conexion.SPlitterSpacerChart2
                                     where a.RingWidth == h1 && a.Proceso == proceso
                                     select new
                                     {
                                         Cutter_Spacer = a.CutterSpacer1
                                     }).ToList();

                        //Retornamos el resultado de la consulta.
                        return Lista;
                    }
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la cantidad de espaciadores utilizados para cuando el proceso es Doble.
        /// </summary>
        /// <param name="proceso"></param>
        /// <param name="h1"></param>
        /// <returns></returns>
        public IList GetCantidadSpacerDoble(string proceso, double h1)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.SplitterSpacerChart
                                 where a.Nominal_split == h1 && a.Proceso == proceso
                                 select new
                                 {
                                     CantidadSpacer = a.Castings_per_chuck

                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la medida ideal del spacer cuando el proceso es distinto a Doble.
        /// </summary>
        /// <param name="proceso"></param>
        /// <param name="h1"></param>
        /// <returns></returns>
        public IList GetMedidaSpacer2(string proceso, double h1)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.SPlitterSpacerChart2
                                 where a.RingWidth == h1 && a.Proceso == proceso
                                 select new
                                 {
                                     Cutter_Spacer = a.CutterSpacer2
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la cantidad de espaciadores utilizados para cuando el proceso es distinto de Doble.
        /// </summary>
        /// <param name="proceso"></param>
        /// <param name="h1"></param>
        /// <returns></returns>
        public IList GetCantidadSpacer(string proceso, double h1)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.SPlitterSpacerChart2
                                 where a.RingWidth == h1 && a.Proceso == proceso
                                 select new
                                 {
                                     CantidadSpacer = a.CantidadSpacer1
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }
        #endregion
    }
}
