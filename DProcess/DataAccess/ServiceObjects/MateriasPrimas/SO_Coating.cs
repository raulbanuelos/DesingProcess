using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_Coating
    {
        public IList GetAllCoating()
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    var Lista = (from a in Conexion.Coating
                                 select a).OrderBy(x => x.NombreCoating).ToList();

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
