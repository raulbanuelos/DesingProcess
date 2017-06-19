using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_MaestroHerramental
    {
        /// <summary>
        /// Método que retorna todos los registro de el maestro de herramentales.
        /// </summary>
        /// <returns></returns>
        public IList GetMaestroHerramentales()
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var Lista = (from a in Conexion.MaestroHerramentales
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
