using System;
using System.Collections;
using System.Linq;

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
                                            join p in Conexion.PLANO_HERRAMENTAL on b.idPlano equals p.ID_PLANO
                                            join c in Conexion.ClasificacionHerramental on b.idClasificacionHerramental equals c.idClasificacion
                                            where tipoBroca == a.DESCRIPCION && b.Activo == true
                                            select new {
                                                a.ID_DRILL_ENGRAVE,
                                                b.Codigo,
                                                b.Descripcion,
                                                a.DIMENCION,
                                                Clasificacion = c.Descripcion,
                                                c.UnidadMedida,
                                                c.Costo,
                                                c.CantidadUtilizar,
                                                c.VidaUtil,
                                                c.idClasificacion,
                                                c.ListaCotasRevisar,
                                                c.VerificacionAnual,
                                                b.Activo,
                                                p.NO_PLANO
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
