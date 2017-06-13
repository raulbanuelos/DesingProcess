using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_Propiedad
    {
        public IList GetAllPropiedades()
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.CAT_PROPIEDAD
                                 select a).ToList();
                    return lista;
                }
            }
            catch (Exception er)
            {
                return null;
            }
        }

        public IList GetPropiedadesByPerfil(int idPerfil)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.CAT_PROPIEDAD
                                 join b in Conexion.TR_PROPIEDAD_PERFIL on a.ID_PROPIEDAD equals b.ID_PROPIEDAD
                                 join c in Conexion.CAT_PERFIL on b.ID_PERFIL equals c.ID_PERFIL
                                 where c.ID_PERFIL == idPerfil
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
