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
            catch (Exception)
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

        // Consulta que elimina registros de la TR_USUARIO_NOTIFICACION_VERSION por ID_VERSION, una vez que el documento ha sido liberado
        public int DeleteRegistroVersion(int id_version)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    // Declaramos la lista
                    List <TR_USUARIO_NOTIFICACION_VERSION> List = Conexion.TR_USUARIO_NOTIFICACION_VERSION.Where(x => x.ID_VERSION == id_version).ToList();

                    foreach (var Registro in List)
                    {
                        // Eliminamos los registros
                        Conexion.Entry(Registro).State = System.Data.Entity.EntityState.Deleted;
                    }

                    // Guardamos los cambios
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            } 
        }
    }
}
