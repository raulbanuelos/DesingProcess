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
    }
}
