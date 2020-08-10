using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_SolicitudControlDocumento
    {

        /// <summary>
        /// Método que establece como terminada una solicitud.
        /// </summary>
        /// <param name="idSolicitud"></param>
        /// <returns></returns>
        public int setDoneSolicitud(int idSolicitud)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TBL_SOLICITUD_CONTROL_DOCUMENTO tblSolicitud = Conexion.TBL_SOLICITUD_CONTROL_DOCUMENTO.Where(a => a.ID_SOLICITUD_CONTROL_DOCUMENTO == idSolicitud).FirstOrDefault();

                    tblSolicitud.FECHA_EJECUCION = DateTime.Now;
                    tblSolicitud.BAN_EJECUTADA = true;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(tblSolicitud).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int delete(int idSolicitud)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TBL_SOLICITUD_CONTROL_DOCUMENTO tblSolicitud = Conexion.TBL_SOLICITUD_CONTROL_DOCUMENTO.Where(a => a.ID_SOLICITUD_CONTROL_DOCUMENTO == idSolicitud).FirstOrDefault();
                    

                    //Se cambia el estado de registro a Eliminado
                    Conexion.Entry(tblSolicitud).State = EntityState.Deleted;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
