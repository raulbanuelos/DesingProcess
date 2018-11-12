using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DataAccess.ServiceObjects.Tooling.Operaciones
{
    public class SO_Diskus
    {
        #region Attributes
        private string SP_RGP_GET_LOSS_FACTOR_DISKUS = "SP_RGP_GET_LOSS_FACTOR_DISKUS";
        private string SP_RGP_GET_DETALLE1_DISCO_DISKUS = "SP_RGP_GET_DETALLE1_DISCO_DISKUS";
        private string SP_RGP_GET_DETALLE2_DISCO_DISKUS = "SP_RGP_GET_DETALLE2_DISCO_DISKUS";
        private string SP_RGP_GET_DETALLE3_DISCO_DISKUS = "SP_RGP_GET_DETALLE3_DISCO_DISKUS";
        #endregion

        #region Methods

        /// <summary>
        /// Método que retorna la información del loss factor, utilizado para buscar el herramental disco en la operación Diskus.
        /// </summary>
        /// <param name="especMaterial"></param>
        /// <returns></returns>
        public DataSet GetLossFactor(string especMaterial)
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
                parametros.Add("material", especMaterial);

                //LLamamos al método para ejecutar el procedimiento, el resultado lo guardamos.
                datos = conexion.EjecutarStoredProcedure(SP_RGP_GET_LOSS_FACTOR_DISKUS, parametros);

                //Retornamos los datos.
                return datos;
            }
            catch (Exception)
            {
                //Si hay algún error, retornamos un nulo.
                return null;
            }
        } 

        /// <summary>
        /// Método que retorna el 1er detalle para obtener el disco de la operación Diskus.
        /// </summary>
        /// <param name="valorCalculado"></param>
        /// <param name="diskus1Min"></param>
        /// <param name="diskus1Max"></param>
        /// <returns></returns>
        public DataSet GetDetalle1Diskus(double valorCalculado, double diskus1Min, double diskus1Max)
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
                parametros.Add("valorCalculado", valorCalculado);
                parametros.Add("diskus1Min", diskus1Min);
                parametros.Add("diskus1Max", diskus1Max);

                //LLamamos al método para ejecutar el procedimiento, el resultado lo guardamos.
                datos = conexion.EjecutarStoredProcedure(SP_RGP_GET_DETALLE1_DISCO_DISKUS, parametros);

                //Retornamos los datos.
                return datos;
            }
            catch (Exception)
            {
                //Si hay algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que retorna el 2do detalle para obtener el disco de la operación Diskus.
        /// </summary>
        /// <param name="h1"></param>
        /// <param name="diskus2Min"></param>
        /// <param name="diskus2Max"></param>
        /// <returns></returns>
        public DataSet GetDetalle2Diskus(double h1,double diskus2Min, double diskus2Max)
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
                parametros.Add("widthNominal", h1);
                parametros.Add("diskus2Min", diskus2Min);
                parametros.Add("diskus2Max", diskus2Max);

                //LLamamos al método para ejecutar el procedimiento, el resultado lo guardamos.
                datos = conexion.EjecutarStoredProcedure(SP_RGP_GET_DETALLE2_DISCO_DISKUS, parametros);

                //Retornamos los datos.
                return datos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que retorna el 3er detalle para obtener el disco de la operación Diskus.
        /// </summary>
        /// <param name="freeGap"></param>
        /// <returns></returns>
        public DataSet GetDetalle3Diskus(double freeGap)
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
                parametros.Add("freeGap", freeGap);

                //LLamamos al método para ejecutar el procedimiento, el resultado lo guardamos.
                datos = conexion.EjecutarStoredProcedure(SP_RGP_GET_DETALLE3_DISCO_DISKUS, parametros);

                //Retornamos los datos.
                return datos;
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que busca el disco.
        /// </summary>
        /// <param name="detalle"></param>
        /// <returns></returns>
        public IList GetDisco(string detalle)
        {
            try
            {
                //Realizamos la conexíon a la Base de datos a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.DiscoDiskus_
                                 join m in Conexion.MaestroHerramentales on a.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where a.Detalle == detalle && m.Activo == true
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
                                     a.Detalle
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
