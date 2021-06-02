using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Usuario
{
    public class SO_SolicitudCorreo
    {
        public int Insert(string title, string body, string recipients, string origen, int idArchivo)
        {
            try
            {
                using (var Conexion = new EntitiesUsuario())
                {
                    TBL_SOLICITUD_CORREO tblSolicitudCorreo = new TBL_SOLICITUD_CORREO();

                    tblSolicitudCorreo.FECHA_SOLICITUD = DateTime.Now;
                    tblSolicitudCorreo.TITLE = title;
                    tblSolicitudCorreo.BODY = body;
                    tblSolicitudCorreo.RECIPIENTS = recipients;
                    tblSolicitudCorreo.BAN_EJECUTADA = false;
                    tblSolicitudCorreo.ORIGEN = origen;
                    tblSolicitudCorreo.ID_ARCHIVO = idArchivo;

                    Conexion.TBL_SOLICITUD_CORREO.Add(tblSolicitudCorreo);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int SetEjecutada(int idSolicitud)
        {
            try
            {
                using (var Conexion = new EntitiesUsuario())
                {
                    TBL_SOLICITUD_CORREO tblSolicitudCorreo = Conexion.TBL_SOLICITUD_CORREO.Where(x => x.ID_SOLICITUD_CORREO == idSolicitud).FirstOrDefault();

                    tblSolicitudCorreo.BAN_EJECUTADA = true;
                    tblSolicitudCorreo.FECHA_EJECUTADA = DateTime.Now;

                    Conexion.Entry(tblSolicitudCorreo).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList GetNoEjecutadas()
        {
            try
            {
                using (var Conexion = new EntitiesUsuario())
                {
                    var lista = (from a in Conexion.TBL_SOLICITUD_CORREO
                                 where a.BAN_EJECUTADA == false
                                 orderby a.FECHA_SOLICITUD descending
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
