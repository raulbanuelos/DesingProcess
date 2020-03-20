using System;
using System.Collections;
using System.Linq;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_DocumentoFirmado
    {
        public IList Get(int idVersion)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var lista = (from a in Conexion.TBL_DOCUMENTO_FIRMADO
                                 where a.ID_VERSION == idVersion
                                 select a).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int Insert(int idVersion, byte[] archivo, string nombreArchivo, string ext)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    TBL_DOCUMENTO_FIRMADO documentoFirmado = new TBL_DOCUMENTO_FIRMADO();

                    documentoFirmado.ID_VERSION = idVersion;
                    documentoFirmado.ARCHIVO = archivo;
                    documentoFirmado.NOMBRE_ARCHIVO = nombreArchivo;
                    documentoFirmado.EXT = ext;

                    Conexion.TBL_DOCUMENTO_FIRMADO.Add(documentoFirmado);

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
