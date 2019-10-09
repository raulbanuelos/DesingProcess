using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_DrillEngrave
    {
        public IList GetBroca(string tipoBroca)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaHerramental = (from a in Conexion.DrillEngrave_
                                            join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                            where tipoBroca == a.DESCRIPCION && b.Activo == true
                                            select new {
                                                a.ID_DRILL_ENGRAVE,
                                                b.Codigo,
                                                b.Descripcion,
                                                a.DIMENCION,
                                            }).ToList();

                    return listaHerramental;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
