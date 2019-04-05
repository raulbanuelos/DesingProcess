using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_TipoError
    {
        // Consulta que traiga todos los campos
        public IList GetAllTipoError()
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var tipo = (from a in Conexion.TBL_NOTIFICACION_ERROR select a).ToList();

                    return tipo;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

    }
}
