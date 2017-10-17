using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Maquinado
{
    public class SO_CamTurn
    {
        private string SP_RGP_GET_TIMECAMTURN = "SP_RGP_GET_TIMECAMTURN";

        public DataSet GetTimeCamTurn(string v, string especMaterial)
        {
            try
            {
                DataSet datos = null;

                Desing_SQL conexion = new Desing_SQL();

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("especMaterial",especMaterial);
                parametros.Add("CamTurn",v);

                datos = conexion.EjecutarStoredProcedure(SP_RGP_GET_TIMECAMTURN, parametros);

                return datos;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
