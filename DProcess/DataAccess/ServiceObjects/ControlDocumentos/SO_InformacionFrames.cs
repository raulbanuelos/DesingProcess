using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_InformacionFrames
    {
        public IList GetAll()
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var list = (from a in Conexion.TBL_INFORMACION_FRAMES
                                select a).ToList();

                    return list;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
