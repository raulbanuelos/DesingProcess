using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
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
                parametros.Add("EspecificacionMaterial", EspecificacionMaterial);

                //LLamamos al método para ejecutar el procedimiento, el resultado lo asignamos a la variable local.
                datos = conexion.EjecutarStoredProcedure("SP_RGP_GetCycleTimeSplitterCasting", parametros);

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
        /// Método el cual obtiene el herramental spacer ideal de la tabla CutterSpacerSplitter
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
        /// Obtiene la información de un herramental de CutterSpacer a partit del código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoSpacer(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.CutterSpacerSplitter
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 where a.Codigo.Equals(codigo) 
                                 select new
                                 {
                                     a.Codigo,
                                     a.A,
                                     a.B,
                                     m.Descripcion,
                                     m.Activo,
                                     a.ID_SPACER_SPLITTER
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
        /// <summary>
        /// Método que da de alta un registro a la tabla CutterSpacerSplitter
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int SetCutterSpacerS(string codigo, double a, double b, string plano)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla
                    CutterSpacerSplitter obj = new CutterSpacerSplitter();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.A = a;
                    obj.B = b;
                    obj.Plano = plano;

                    //Guardamos los cambios
                    Conexion.CutterSpacerSplitter.Add(obj);
                    Conexion.SaveChanges();
                    //Retornamos el id
                    return obj.ID_SPACER_SPLITTER;
                }
            }
            catch (Exception)
            {
                //Si hubo error, retornamos cero
                return 0;
            }
        }

        /// <summary>
        /// Método que actualiza un registro de la tabla  CutterSpacerSplitter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int UpdateCutterSpacerS(int id,string codigo,double a,double b, string plano)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var conexion= new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    CutterSpacerSplitter obj = conexion.CutterSpacerSplitter.Where(x => x.ID_SPACER_SPLITTER == id).FirstOrDefault();

                    //Asiganmos los valores
                    obj.A = a;
                    obj.B = b;
                    obj.Plano = plano;

                    //Se cambia el estado de registro a modificado.
                    conexion.Entry(obj).State = EntityState.Modified;
                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {
                //Si encuentra error devuelve cero.
                return 0; ;
            }
        }

        /// <summary>
        /// Método que elimina un registro de la tabla CutterSpacerSplitter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCutterSpacerS(int id)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion= new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    CutterSpacerSplitter obj = Conexion.CutterSpacerSplitter.Where(x => x.ID_SPACER_SPLITTER == id).FirstOrDefault();

                    //eliminamos el registro
                    Conexion.Entry(obj).State = EntityState.Deleted;
                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros de Cutter Spacer Splitter
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllCutterSpacerS(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.CutterSpacerSplitter
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo  
                                 where a.Codigo.Contains(texto) || m.Descripcion.Contains(texto)                           
                                 select new
                                 {
                                     a.Codigo,
                                     a.A,
                                     a.B,
                                     m.Descripcion,
                                     m.Activo,

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
        /// Método que obtiene el herramental cutter de la operación.
        /// </summary>
        /// <param name="medida"></param>
        /// <returns></returns>
        public IList GetCutter(double medida)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.CutterSplitter
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where a.Diametro == medida
                                 select new
                                 {
                                     a.Codigo,
                                     Diametro = a.Diametro,
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
        /// Obtiene la información de un herramental de Cutter Splitter partit del código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoCutter(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.CutterSplitter
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 where a.Codigo.Equals(codigo)
                                 select new
                                 {
                                     a.Codigo,
                                     a.Diametro,
                                     m.Descripcion,
                                     m.Activo,
                                     a.ID_CUTTER_SPLITTER
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
        ///  Método que da de alta un registro a la tabla CutterSplitter
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="diam"></param>
        /// <returns></returns>
        public int SetCutter(string codigo, double diam)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla
                    CutterSplitter obj = new CutterSplitter();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.Diametro = diam;
                    //Guardamos los cambios
                    Conexion.CutterSplitter.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.ID_CUTTER_SPLITTER;
                }
            }
            catch (Exception)
            {
                //Si hay error, retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que actualiza un registro en la tabla CutterSplitter
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="diam"></param>
        /// <returns></returns>
        public int UpdateCutter(int id, string codigo,double diam)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion= new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    CutterSplitter obj = Conexion.CutterSplitter.Where(x => x.ID_CUTTER_SPLITTER == id).FirstOrDefault();
                   
                    //Asiganmos los valores
                   
                    obj.Diametro = diam;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;
                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();

                }
            }
            catch (Exception)
            {
                //Si encuentra error devuelve cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro de la tabla CutterSplitter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCutter(int id)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion= new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    CutterSplitter obj = Conexion.CutterSplitter.Where(x => x.ID_CUTTER_SPLITTER == id).FirstOrDefault();

                    //eliminamos el registro
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla Cutter Splitter
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllCutter(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.CutterSplitter
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 where a.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     a.Codigo,
                                     a.Diametro,
                                     m.Descripcion,
                                     m.Activo,

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
        /// Método que obtiene el herramental Chuck de la operación.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetChuck(double id)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.ChuckSplitter
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where id >= a.DiaMin && id <= a.DiaMax
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
                                     TipoEnsamble = a.TipoEnsamble
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

        /// <summary>
        /// Método que obtiene todos los herramentales chuck de acuerdo al texto
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllChuck(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    //Realizamos la consulta
                    var Lista = (from c in Conexion.ChuckSplitter
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DiaMin,
                                     c.DiaMax,
                                     c.TipoEnsamble,
                                     m.Descripcion,
                                     m.Activo
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


        /// <summary>
        /// Obtiene la información de un herramental de Chuck Splitter partir del código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoChuckS(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.ChuckSplitter
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 where a.Codigo.Equals(codigo)
                                 select new
                                 {
                                     a.Codigo,
                                     a.DiaMin,
                                     a.DiaMax,
                                     a.TipoEnsamble,
                                     m.Descripcion,
                                     m.Activo,
                                     a.Id_Chuck
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
        /// Método que da de alta un registro a la tabla Chuck
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="diaMin"></param>
        /// <param name="diaMax"></param>
        /// <param name="ensamble"></param>
        /// <returns></returns>
        public int  SetChuck(string codigo, double diaMin, double diaMax,string ensamble )
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    ChuckSplitter obj = new ChuckSplitter();
                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.DiaMin = diaMin;
                    obj.DiaMax = diaMax;
                    obj.TipoEnsamble = ensamble;
                    //Se guardan los cambios
                    Conexion.ChuckSplitter.Add(obj);
                    Conexion.SaveChanges();
                    //Retorna el código
                    return obj.Id_Chuck;
                }
            }
            catch (Exception)
            {
                //si hay error, retorna nulo
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de la tabla ChuckSplitter
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dimMin"></param>
        /// <param name="dimMax"></param>
        /// <param name="ensamble"></param>
        /// <returns></returns>
        public int UpdateChuck(int id,string codigo, double dimMin,double dimMax,string ensamble)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Obtiene el objeto que se va a modificar
                    ChuckSplitter obj = Conexion.ChuckSplitter.Where(x => x.Id_Chuck == id).FirstOrDefault();

                    //Asignamos los valores
                    obj.DiaMin = dimMin;
                    obj.DiaMax = dimMax;
                    obj.TipoEnsamble = ensamble;

                    //Guardamos los cambios, y retornamos los registros modificados
                    Conexion.Entry(obj).State = EntityState.Modified;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //si hay error , retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro de la tabla ChuckSplitter
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int DeleteChuck(int id)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Obtiene el objeto que se va a modificar
                    ChuckSplitter obj = Conexion.ChuckSplitter.Where(x => x.Id_Chuck == id).FirstOrDefault();
                    //Guardamos los cambios, y retornamos los registros modificados
                    Conexion.Entry(obj).State = EntityState.Deleted;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //si hay error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que indica si un componente (dependiendo de id de splitter) debe de llevar el herramental uretano.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetHasUretano(double id)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la conexíon a través de EntityFramework.
                    var Lista = (from a in Conexion.UretanoSplitter
                                 where id >= a.DiaMin && id <= a.DiaMax
                                 select a).ToList();
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el herramental Uretano.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetUretano(double id)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.UretanoSplitter
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where id >= a.DiaMin && id <= a.DiaMax
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
                                     Detalle = a.Detalle,
                                     Color = a.Color
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
        /// Obtiene todos los registros de Uretano Splitter
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllUretano(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from u in Conexion.UretanoSplitter
                                 join m in Conexion.MaestroHerramentales on u.Codigo equals m.Codigo
                                 where u.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     u.Codigo, 
                                     m.Descripcion,
                                     m.Activo,
                                     u.Color,
                                     u.Detalle,
                                     u.DiaMin,
                                     u.DiaMax,
                                     u.Medidas
                                 }).ToList();
                    //retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error, retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Obtiene la información de un herramental de Uretano Splitter partir del código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoUretanoS(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.UretanoSplitter
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 where a.Codigo.Equals(codigo)
                                 select new
                                 {
                                     a.Codigo,
                                     a.DiaMin,
                                     a.DiaMax,
                                     a.Medidas,                                     
                                     a.Color,
                                     m.Descripcion,
                                     m.Activo,
                                     a.ID_URETANO_SPLITTER,
                                     a.Detalle
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
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="medidas"></param>
        /// <param name="color"></param>
        /// <param name="diaMin"></param>
        /// <param name="diaMax"></param>
        /// <param name="detalle"></param>
        /// <returns></returns>
        public int SetUretano(string codigo,string medidas,string color,double diaMin, double diaMax,string detalle)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    UretanoSplitter uretano = new UretanoSplitter();

                    //Asignamos los valores
                    uretano.Codigo = codigo;
                    uretano.Medidas = medidas;
                    uretano.Color = color;
                    uretano.DiaMin = diaMin;
                    uretano.DiaMax = diaMax;
                    uretano.Detalle = detalle;

                    //Se guardan los cambios
                    Conexion.UretanoSplitter.Add(uretano);
                    Conexion.SaveChanges();

                    //Retorna el id
                    return uretano.ID_URETANO_SPLITTER;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa cero
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="medidas"></param>
        /// <param name="color"></param>
        /// <param name="diaMin"></param>
        /// <param name="diaMax"></param>
        /// <param name="detalle"></param>
        /// <returns></returns>
        public int UpdateUretano(int id,string codigo,string medidas,string color,double diaMin,double diaMax,string detalle)
        {
            try
            {
                using (var Conexion= new EntitiesTooling())
                {
                    UretanoSplitter obj = Conexion.UretanoSplitter.Where(x => x.ID_URETANO_SPLITTER == id).FirstOrDefault();

              
                    obj.Medidas = medidas;
                    obj.Color = color;
                    obj.DiaMin = diaMin;
                    obj.DiaMax = diaMax;
                    obj.Detalle = detalle;

                    //Guardamos los cambios, y retornamos los registros modificados
                    Conexion.Entry(obj).State = EntityState.Modified;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteUretano(int id)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Obtiene el objeto que se va a modificar
                    UretanoSplitter obj = Conexion.UretanoSplitter.Where(x => x.ID_URETANO_SPLITTER == id).FirstOrDefault();
                 
                    //Guardamos los cambios, y retornamos los registros modificados
                    Conexion.Entry(obj).State = EntityState.Deleted;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        #endregion
    }
}
