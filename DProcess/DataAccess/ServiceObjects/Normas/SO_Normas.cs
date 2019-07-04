using System;
using System.Collections;
using System.Linq;

namespace DataAccess.ServiceObjects.Normas
{
    public class SO_Normas
    {
        public IList GetAll()
        {
            try
            {
                using (var Conexion = new EntitiesNormas())
                {
                    var lista = (from a in Conexion.TBL_NORMAS
                                 select a).ToList();

                    return lista;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
