using System;
using System.Collections;
using System.Linq;

namespace DataAccess.ServiceObjects.Unidades
{
    public class SO_Unidades
    {
        /// <summary>
        /// Método que obtiene todas las unidades de tipo Distancia.
        /// </summary>
        /// <returns>Retorna una lista anónima con la información. Si se presenta algún error retorna un nulo.</returns>
        public IList GetUnidadesDistancia()
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Contexto = new EntitiesUnidades())
                {
                    //Realizamos la consulta. El resultado lo asignamos a una lista anónima.
                    var Lista = (from a in Contexto.UnidadDistancia
                                 select new {
                                     ID = a.ID_UNIDAD_DISTANCIA,
                                     VALOR = a.ValorInches,
                                     UNIDAD = a.Nombre, 
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si se presenta algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el valor de una unidad.
        /// </summary>
        /// <param name="nombreUnidad">Cadena que representa el nombre de la unidad requerida.</param>
        /// <returns></returns>
        public double GetValueInchUnidadDistance(string nombreUnidad)
        {
            try
            {
                //Inicializamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesUnidades())
                {
                    //Realizamos la consulta y el resultado lo guardamos en una variable.
                    double valor = (from a in Conexion.UnidadDistancia
                                    where a.Nombre == nombreUnidad
                                    select a.ValorInches).FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return valor;
                }
            }
            catch (Exception er)
            {
                //Retornamos un cero si ocurre algún error.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todas las unidades de tipo Presion.
        /// </summary>
        /// <returns>Retorna una lista anónima con la información. Si se presenta algún error retorna un nulo.</returns>
        public IList GetUnidadesPresion()
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesUnidades())
                {
                    //Realizamos la consulta. El resultado lo asignamos a una lista anónima.
                    var Lista = (from a in Conexion.UnidadPresion
                                 select new {
                                     ID = a.ID_UNIDAD_PRESION,
                                     VALOR = a.ValorPSI,
                                     UNIDAD = a.Nombre
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si se presenta algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el valor de una unidad.
        /// </summary>
        /// <param name="nombreUnidad">Cadena que representa el nombre de la unidad requerida.</param>
        /// <returns></returns>
        public double GetValuePSIUnidadPresion(string nombreUnidad)
        {
            try
            {
                //Inicializamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesUnidades())
                {
                    //Realizamos la consulta y el resultado lo guardamos en una variable.
                    var valor = (from a in Conexion.UnidadPresion
                                 where a.Nombre == nombreUnidad
                                 select a.ValorPSI).FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return valor;
                }
            }
            catch (Exception)
            {
                //Retornamos un cero si ocurre algún error.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todas las unidades de tipo Angle.
        /// </summary>
        /// <returns>Retorna una lista anónima con la información. Si se presenta algún error retorna un nulo.</returns>
        public IList GetUnidadesAngle()
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Contexto = new EntitiesUnidades())
                {
                    //Realizamos la consulta. El resultado lo asignamos a una lista anónima.
                    var Lista = (from a in Contexto.UnidadAngle
                                 select new {
                                     ID = a.ID_UNIDAD_ANGLE,
                                     VALOR = a.ValorGrados,
                                     UNIDAD = a.Nombre
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si se presenta algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el valor de una unidad.
        /// </summary>
        /// <param name="nombreUnidad">Cadena que representa el nombre de la unidad requerida.</param>
        /// <returns></returns>
        public double GetValueGradosUnidadAngle(string nombreUnidad)
        {
            try
            {
                //Inicializamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesUnidades())
                {
                    //Realizamos la consulta y el resultado lo guardamos en una variable.
                    var valor = (from a in Conexion.UnidadAngle
                                 where a.Nombre == nombreUnidad
                                 select a.ValorGrados).FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return valor;
                }
            }
            catch (Exception)
            {
                //Retornamos un cero si ocurre algún error.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todas las unidades de tipo Cantidad.
        /// </summary>
        /// <returns>Retorna una lista anónima con la información. Si se presenta algún error retorna un nulo.</returns>
        public IList GetUnidadesCantidad()
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Contexto = new EntitiesUnidades())
                {
                    //Realizamos la consulta. El resultado lo asignamos a una lista anónima.
                    var Lista = (from a in Contexto.UnidadCantidad
                                 select new {
                                     ID = a.ID_UNIDAD_CANTIDAD,
                                     VALOR = a.ValorUnidad,
                                     UNIDAD = a.Nombre
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si se presenta algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el valor de una unidad.
        /// </summary>
        /// <param name="nombreUnidad">Cadena que representa el nombre de la unidad requerida.</param>
        /// <returns></returns>
        public double GetValueUnidadUnidadCantidad(string nombreUnidad)
        {
            try
            {
                //Inicializamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesUnidades())
                {
                    //Realizamos la consulta y el resultado lo guardamos en una variable.
                    var valor = (from a in Conexion.UnidadCantidad
                                 where a.Nombre == nombreUnidad
                                 select a.ValorUnidad).FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return valor;
                }
            }
            catch (Exception)
            {
                //Retornamos un cero si ocurre algún error.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todas las unidades de tipo Force.
        /// </summary>
        /// <returns>Retorna una lista anónima con la información. Si se presenta algún error retorna un nulo.</returns>
        public IList GetUnidadesForce()
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Contexto = new EntitiesUnidades())
                {
                    //Realizamos la consulta. El resultado lo asignamos a una lista anónima.
                    var Lista = (from a in Contexto.UnidadForce
                                 select new {
                                     ID = a.ID_UNIDAD_FORCE,
                                     VALOR = a.ValorLBS,
                                     UNIDAD = a.Nombre
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si se presenta algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el valor de una unidad.
        /// </summary>
        /// <param name="nombreUnidad">Cadena que representa el nombre de la unidad requerida.</param>
        /// <returns></returns>
        public double GetValueLBSUnidadForce(string nombreUnidad)
        {
            try
            {
                //Inicializamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesUnidades())
                {
                    //Realizamos la consulta y el resultado lo guardamos en una variable.
                    var valor = (from a in Conexion.UnidadForce
                                 where a.Nombre == nombreUnidad
                                 select a.ValorLBS).FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return valor;
                }
            }
            catch (Exception)
            {
                //Retornamos un cero si ocurre algún error.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todas las unidades de tipo Mass.
        /// </summary>
        /// <returns>Retorna una lista anónima con la información. Si se presenta algún error retorna un nulo.</returns>
        public IList GetUnidadesMas()
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Contexto = new EntitiesUnidades())
                {
                    //Realizamos la consulta. El resultado lo asignamos a una lista anónima.
                    var Lista = (from a in Contexto.UnidadMass
                                 select new {
                                     ID = a.ID_UNIDAD_MASS,
                                     VALOR = a.ValorGram,
                                     UNIDAD = a.Nombre
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si se presenta algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el valor de una unidad.
        /// </summary>
        /// <param name="nombreUnidad">Cadena que representa el nombre de la unidad requerida.</param>
        /// <returns></returns>
        public double GetValueGramUnidadMass(string nombreUnidad)
        {
            try
            {
                //Inicializamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesUnidades())
                {
                    //Realizamos la consulta y el resultado lo guardamos en una variable.
                    var valor = (from a in Conexion.UnidadMass
                                 where a.Nombre == nombreUnidad
                                 select a.ValorGram).FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return valor;
                }
            }
            catch (Exception)
            {
                //Retornamos un cero si ocurre algún error.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todas las unidades de tipo Tiempo.
        /// </summary>
        /// <returns>Retorna una lista anónima con la información. Si se presenta algún error retorna un nulo.</returns>
        public IList GetUnidadesTiempo()
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Contexto = new EntitiesUnidades())
                {
                    //Realizamos la consulta. El resultado lo asignamos a una lista anónima.
                    var Lista = (from a in Contexto.UnidadTiempo
                                 select new {
                                     ID = a.ID_UNIDAD_TIEMPO,
                                     VALOR = a.ValorSeg,
                                     UNIDAD = a.Nombre
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si se presenta algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el valor de una unidad.
        /// </summary>
        /// <param name="nombreUnidad">Cadena que representa el nombre de la unidad requerida.</param>
        /// <returns></returns>
        public double GetValueSegUnidadTiempo(string nombreUnidad)
        {
            try
            {
                //Inicializamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesUnidades())
                {
                    //Realizamos la consulta y el resultado lo guardamos en una variable.
                    var valor = (from a in Conexion.UnidadTiempo
                                 where a.Nombre == nombreUnidad
                                 select a.ValorSeg).FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return valor;
                }
            }
            catch (Exception)
            {
                //Retornamos un cero si ocurre algún error.
                return 0;
            }
        }
    }
}
