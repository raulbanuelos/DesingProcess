using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_ColoresAnillos
    {
        public IList GetAll()
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.ColoresAnillos
                                 select a).ToList();

                    return lista;
                }
            }
            catch (Exception er)
            {

                throw;
            }
        }
    }
}
