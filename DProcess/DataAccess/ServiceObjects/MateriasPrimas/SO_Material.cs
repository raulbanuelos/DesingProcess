using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.SQLServer;
using DataAccess.ServiceObjects.MateriasPrimas;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_Material
    {
        #region Propiedades
        string StrinDeConexion = @"data source=MXAGSQLSRV01\SQLINTERTEL12;initial catalog=RGP2-PBA;user id=shruser;password=sOHR2011";
        #endregion

        #region Constructores
        public SO_Material(string stringDeConexion)
        {
            StrinDeConexion = stringDeConexion;
        }

        public SO_Material()
        {
        }

        #endregion

        #region Métodos
        public DataSet GetTipoMaterial(string EspecificacionMaterial)
        {
            //Delcaramos un objeto DataSet que será el que reciba la información de la consulta de la base de datos.
            DataSet InformacionBD = new DataSet();

            try
            {

                //Verificamos si la cadena de conexión fue inicializada.
                if (StrinDeConexion != string.Empty)
                {
                    //Declaramos un objeto de tipo Desing_SQL enviando la cadena de conexión.
                    Desing_SQL ConexionSQL = new Desing_SQL();

                    //Declaramos un diccionario para guardar los parámetros necesarios del procedimiento.
                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    //Agregamos los paramentros al diccionario.
                    parametros.Add("espec", EspecificacionMaterial);

                    //Ejecutamos el método enviando el nombre del procedimiento almacenado y los parámetros.
                    InformacionBD = ConexionSQL.EjecutarStoredProcedure("SP_RGP_GetTipoMaterial", parametros);
                }
            }
            catch (Exception)
            {
                //Registrar el error.
                return InformacionBD;
            }
            return InformacionBD;
        }

        public DataSet GetCamTurnConstant(string especMaterial, string tipoAnillo, string ringShape)
        {
            DataSet informacionBD = new DataSet();
            try
            {
                if (StrinDeConexion != string.Empty)
                {
                    Desing_SQL ConexionSQL = new Desing_SQL();

                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    parametros.Add("espec_material", especMaterial);
                    parametros.Add("tipo_anillo",tipoAnillo);
                    parametros.Add("ring_shape", ringShape);

                    informacionBD = ConexionSQL.EjecutarStoredProcedure("SP_RGP_GetCamTurnConstant",parametros);
                }
            }
            catch (Exception)
            {
                return informacionBD;
            }
            
            return informacionBD;
        }

        /// <summary>
        /// Método que obtiene la lista de materias primas que pertenecen a un grupo determinado.
        /// </summary>
        /// <param name="desc"></param>
        /// <returns></returns>
        public IList GetEspecByDescripcion(string desc)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework, el resultado lo asignamos a una variable anónima.
                using (var Conexion = new EntitiesMateriaPrima())
                {

                    var Lista = (from a in Conexion.material
                                 where a.descripcion == desc
                                 select a).OrderBy(x => x.descripcion).ToList();

                    //Retornamos el resultado de consulta.
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
        /// Método que obtiene los valores de CSTG_SM_OD y RISE de una placa modelo.
        /// </summary>
        /// <param name="CodigoPlacaModelo"></param>
        /// <returns></returns>
        public IList GetCSTGSMOD(string CodigoPlacaModelo)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta. El resultado lo guardamos en una variable anónima.
                    var lista = (from p in Conexion.Pattern2
                                 where p.codigo == CodigoPlacaModelo
                                 select new {
                                     p.CSTG_SM_OD,
                                     p.RISE
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el valor de PATT_SM_ID
        /// </summary>
        /// <param name="CodigoPlacaModelo"></param>
        /// <returns></returns>
        public IList GetPATTSMID(string CodigoPlacaModelo)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta. El resultado lo guardamos en una variable anónima.
                    var lista = (from p in Conexion.Pattern2
                                 where p.codigo == CodigoPlacaModelo
                                 select new
                                 {
                                     p.PATT_SM_ID
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }
        #endregion
    }
}
