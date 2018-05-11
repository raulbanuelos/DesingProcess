using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Notificaciones
{
    public class SO_Notificaciones
    {
        public int InsertNotificacion(string userSend, string userReceiver, string title, string msg, int type)
        {
            try
            {
                using (var Conexion = new EntitesNotificaciones())
                {
                    TBL_NOTIFICACIONES notificacion = new TBL_NOTIFICACIONES();

                    notificacion.ID_USUARIO_RECEIVER = userReceiver;
                    notificacion.ID_USUARIO_SEND = userSend;
                    notificacion.MSG = msg;
                    notificacion.TITLE = title;
                    notificacion.TYPE_NOTIFICATION = type;

                    Conexion.TBL_NOTIFICACIONES.Add(notificacion);

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
