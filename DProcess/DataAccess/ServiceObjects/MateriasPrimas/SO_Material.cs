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
        #region Atributtes
        private string SP_RGP_GET_ALEANTES_MATERIAPRIMA = "SP_RGP_GET_ALEANTES_MATERIAPRIMA"; 
        #endregion

        #region Propiedades
        //Cadena de conexión
        string StrinDeConexion = string.Empty;
        #endregion

        #region Constructores

        //Constructor por default.
        public SO_Material()
        {
            //Obtenemos la cadena de conexión.
            StrinDeConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"];
        }

        #endregion

        #region Métodos
        /// <summary>
        /// Método el cual obtiene el tipo de material de una especificación de material.
        /// </summary>
        /// <param name="EspecificacionMaterial"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Método el cual obtiene el id de material prima de una especificación.
        /// </summary>
        /// <param name="material"></param>
        /// <returns></returns>
        public IList GetIdEspecficacionMaterial(string material)
        {
            try
            {
                //Estabecemos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta, el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.Esp_MP_Anillos
                                 where a.id_material == material || a.Odl_Mahle == material || a.Ref1 == material || a.Ref2 == material || a.Ref3 == material || a.Ref4 == material || a.Ref5 == material || a.Ref6 == material || a.Ref7 == material || a.Ref8 == material || a.Ref9 == material || a.Ref10 == material || a.Ref11 == material || a.Ref12 == material
                                 select new
                                 {
                                     IdMaterial = a.id_material
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método el cual obtiene el valor de camTurnConstant.
        /// </summary>
        /// <param name="especMaterial"></param>
        /// <param name="tipoAnillo"></param>
        /// <param name="ringShape"></param>
        /// <returns></returns>
        public DataSet GetCamTurnConstant(string especMaterial, string tipoAnillo, string ringShape)
        {
            //Declaramos un DataSet el cual será el que retornemos en el método.
            DataSet informacionBD = new DataSet();
            try
            {
                //Verificamos si la cadena de conexión es diferente de vacío.
                if (StrinDeConexion != string.Empty)
                {
                    //Declaramos un objeto el cual será el que ejecute el procedimiento.
                    Desing_SQL ConexionSQL = new Desing_SQL();

                    //Declaramos un objeto Dictionary el cual contendrá los parámetros.
                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    //Asignamos los parámetros y sus valores.
                    parametros.Add("espec_material", especMaterial);
                    parametros.Add("tipo_anillo", tipoAnillo);
                    parametros.Add("ring_shape", ringShape);

                    //Ejecutamos el método para ejecutar el procedimiento almacenado.
                    return informacionBD = ConexionSQL.EjecutarStoredProcedure("SP_RGP_GetCamTurnConstant", parametros);
                }
                else
                {
                    //Si la cadena de conexión no fué encontrada, retornamos el DataSet vacío.
                    return informacionBD;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos el DataSet vacío.
                return informacionBD;
            }
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

        /// <summary>
        /// Método que obtiene la lista de  todo el material.
        /// </summary>
        /// <returns></returns>
        public IList GetMaterial()
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta. El resultado lo guardamos en una variable anónima.
                    var lista = (from m in Conexion.material
                                 select new
                                 {
                                     m.id,
                                     m.descripcion,
                                     m.Recomendado
                                 }).OrderBy(x => x.id).ToList();

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
        /// Método que obtiene la lista de aleantes a partir de una especificación de materia prima.
        /// </summary>
        /// <param name="especMaterial"></param>
        /// <returns></returns>
        public DataSet GetAleanteEspecificacionMaterial(string especMaterial)
        {
            //Delcaramos un objeto DataSet que será el que reciba la información de la consulta de la base de datos.
            DataSet informacionBD = new DataSet();

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
                    parametros.Add("EspecMateriaPrima", especMaterial);

                    //Ejecutamos el método enviando el nombre del procedimiento almacenado y los parámetros.
                    informacionBD = ConexionSQL.EjecutarStoredProcedure(SP_RGP_GET_ALEANTES_MATERIAPRIMA, parametros);
                }

                //Retornamos los resultados obtenidos.
                return informacionBD;
            }
            catch (Exception)
            {
                //Registrar el error.
                return informacionBD;
            }
        }

        /// <summary>
        /// Método que obtiene todos los tipo de material. (GASOLINA, SPR-212, SUPER DUTY)
        /// </summary>
        /// <returns></returns>
        public IList GetAllTipoMaterial()
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta y el resultado lo guardamos en una lista.
                    var lista = (from c in Conexion.Tipo_Materia_Prima
                                 select c).ToList();

                    //Retornamos el resultado de la lista.
                    return lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre un error, retornamos un nulo.
                return null;
            }
        }
        #endregion
    }
}
