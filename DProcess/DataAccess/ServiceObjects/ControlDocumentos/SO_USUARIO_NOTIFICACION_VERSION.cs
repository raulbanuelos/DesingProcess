using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_USUARIO_NOTIFICACION_VERSION
    {
        public IList GetAll(int id_version)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var lista = (from a in Conexion.TR_USUARIO_NOTIFICACION_VERSION
                                 where a.ID_VERSION == id_version
                                 select a).ToList();

                    return lista;
                }
            }
            catch (Exception er)
            {
                return null;                
            }
        }

        public int SetUserNotifyVersion(string id_usuario, int id_version)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TR_USUARIO_NOTIFICACION_VERSION obj = new TR_USUARIO_NOTIFICACION_VERSION();

                    obj.ID_USUARIO = id_usuario;
                    obj.ID_VERSION = id_version;

                    Conexion.TR_USUARIO_NOTIFICACION_VERSION.Add(obj);

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
