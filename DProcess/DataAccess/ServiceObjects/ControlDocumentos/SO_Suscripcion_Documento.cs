using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_Suscripcion_Documento
    {
        /// <summary>
        /// Consulta para traer los usuarios suscritos por ID_DOCUMENTO
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public IList GetUserSuscripDoc(int id_documento)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var ListaSuscritos = (from a in Conexion.TBL_SUSCRIPCION_DOCUMENTO
                                          join b in Conexion.Usuarios on a.ID_USUARIO_SUSCRITO equals b.Usuario
                                          join c in Conexion.TBL_DOCUMENTO on a.ID_DOCUMENTO equals c.ID_DOCUMENTO
                                          where c.ID_DOCUMENTO == id_documento
                                          select new
                                          {
                                              a.ID_SUSCRIPCION_DOC,
                                              a.ID_USUARIO_SUSCRITO,
                                              a.ID_DOCUMENTO
                                              //b.Usuario,
                                              //c.ID_DOCUMENTO
                                          }).ToList();

                    // Retornamos la lista
                    return ListaSuscritos;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos la lista nula
                return null;
            }
        }

        /// <summary>
        /// Consulta para saber si hay usuarios suscritos
        /// </summary>
        /// <param name="usuario_suscrito"></param>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public int GetRegistrosSuscritos(string usuario_suscrito, int id_documento)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using(var Conexion = new EntitiesControlDocumentos())
                {
                    var ListaRegistros = (from a in Conexion.TBL_SUSCRIPCION_DOCUMENTO
                                          where a.ID_USUARIO_SUSCRITO == usuario_suscrito
                                          && a.ID_DOCUMENTO == id_documento
                                          select a).ToList().Count;

                    // Retornamos la lista con el número de registros
                    return ListaRegistros;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Método para insertar un registro Suscripción Documento
        /// </summary>
        /// <param name="usuario_suscrito"></param>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public int InsertSuscriptorDoc(string usuario_suscrito, int id_documento)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    // Declaramos el objeto de la lista
                    TBL_SUSCRIPCION_DOCUMENTO suscrip_doc = new TBL_SUSCRIPCION_DOCUMENTO();

                    // Asignamos valores
                    suscrip_doc.ID_USUARIO_SUSCRITO = usuario_suscrito;
                    suscrip_doc.ID_DOCUMENTO = id_documento;

                    // Insertamos el objeto a la tabla
                    Conexion.TBL_SUSCRIPCION_DOCUMENTO.Add(suscrip_doc);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el ID del objeto
                    return suscrip_doc.ID_SUSCRIPCION_DOC;
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro Suscripción Documento
        /// </summary>
        /// <param name="id_suscrpcion_doc"></param>
        /// <returns></returns>
        public int DeleteSuscriptorDoc(string usuario_suscrito, int id_documento)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    // Declaramos el objeto de la lista
                    TBL_SUSCRIPCION_DOCUMENTO suscrip_user = Conexion.TBL_SUSCRIPCION_DOCUMENTO.Where(x => x.ID_DOCUMENTO == id_documento && x.ID_USUARIO_SUSCRITO == usuario_suscrito).FirstOrDefault();

                    // Eliminamos el objeto de la tabla
                    Conexion.Entry(suscrip_user).State = System.Data.Entity.EntityState.Deleted;

                    // Guardamos los cambios
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception er)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

    }
}
