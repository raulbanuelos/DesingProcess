using System;
using System.Collections.Generic;
using System.Data;
using DataAccess.SQLServer;

namespace DataAccess.ServiceObjects
{
    public class SO_Material
    {
        #region Propiedades
        string StrinDeConexion = string.Empty;
        #endregion

        #region Constructores
        public SO_Material(string stringDeConexion)
        {
            StrinDeConexion = stringDeConexion;
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

        #endregion
    }
}
