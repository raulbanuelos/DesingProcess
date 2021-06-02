using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Operaciones
{
    public class SO_Operaciones
    {
        public IList GetAllOperaciones()
        {
            try
            {
                using (var Conexion = new EntityOperaciones())
                {
                    var Lista = (from a in Conexion.OperacionesRouter
                                 select a).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
