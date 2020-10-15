using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_CriteriosAnillos
    {
        public IList GetCriterio(string NombreCriterio)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var Lista = (from a in Conexion.CriteriosAnillos
                                 select a).ToList();

                    return Lista;
                }
            }
            catch (Exception er)
            {
                return null;
            }
        }
    }
}
