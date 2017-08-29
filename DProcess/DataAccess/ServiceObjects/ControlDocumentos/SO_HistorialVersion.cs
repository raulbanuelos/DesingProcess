using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_HistorialVersion
    {
        public int Insert(int idVersion, DateTime fecha, string descripcion, string nombreUsuario)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TBL_HISTORIAL_VERSION registro = new TBL_HISTORIAL_VERSION();

                    registro.ID_VERSION = idVersion;
                    registro.FECHA = fecha;
                    registro.DESCRIPCION = descripcion;
                    registro.NOMBRE_USUARIO = nombreUsuario;

                    Conexion.TBL_HISTORIAL_VERSION.Add(registro);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {
                return 0;
            }
        }
    }
}
