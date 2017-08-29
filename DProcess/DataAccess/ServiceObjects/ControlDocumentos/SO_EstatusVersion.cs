using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_EstatusVersion
    {
        public String GetEstatusVersion(int idEstatusVersion)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var version = (from a in Conexion.TBL_ESTATUS_VERSION
                                   where a.ID_ESTATUS_VERSION == idEstatusVersion
                                   select a.ESTATUS_VERSION).FirstOrDefault();

                    return version;
                }
            }
            catch (Exception er)
            {
                return string.Empty;
            }
        }
    }
}
