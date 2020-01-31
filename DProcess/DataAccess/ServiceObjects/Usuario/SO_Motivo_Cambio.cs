using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Usuario
{
    public class SO_Motivo_Cambio
    {
        public IList Get()
        {
            try
            {
                using (var Conexion = new EntitiesUsuario())
                {
                    var lista = (from a in Conexion.CAT_MOTIVO_CAMBIO
                                 orderby a.MOTIVO_CAMBIO
                                 select a).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int InsertTRMotivoCambioLeccion(int idLeccion, int idMotivo)
        {
            try
            {
                using (var Conexion = new EntitiesUsuario())
                {
                    TR_LECCIONES_APRENDIDAS_MOTIVO_CAMBIO tr = new TR_LECCIONES_APRENDIDAS_MOTIVO_CAMBIO();

                    tr.ID_LECCION_APRENDIDA = idLeccion;
                    tr.ID_MOTIVO_CAMBIO = idMotivo;

                    Conexion.TR_LECCIONES_APRENDIDAS_MOTIVO_CAMBIO.Add(tr);

                    return Conexion.SaveChanges();
                }


            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList Get(int idLeccion)
        {
            try
            {
                using (var Conexion = new EntitiesUsuario())
                {
                    var Lista = (from a in Conexion.TR_LECCIONES_APRENDIDAS_MOTIVO_CAMBIO
                                 join b in Conexion.CAT_MOTIVO_CAMBIO on a.ID_MOTIVO_CAMBIO equals b.ID_MOTIVO_CAMBIO
                                 where a.ID_LECCION_APRENDIDA == idLeccion
                                 orderby b.MOTIVO_CAMBIO
                                 select new
                                 {
                                     b.MOTIVO_CAMBIO
                                 }).ToList();

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
