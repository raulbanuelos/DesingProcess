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
                                          }).ToList();

                    // Retornamos la lista
                    return ListaSuscritos;
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos la lista nula
                return null;
            }
        }

        /// <summary>
        /// Consulta para traer los documentos a los que está suscrito cada usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public IList GetDocSuscripcion(string usuario)
        {
            try
            {
                // Establecemos conexión a través de EntityFramework
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var ListaDocSuscripcion = (from d in Conexion.TBL_DOCUMENTO
                                               join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                               join a in Conexion.TBL_ARCHIVO on v.ID_VERSION equals a.ID_VERSION
                                               join dp in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals dp.ID_DEPARTAMENTO
                                               join u in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals u.Usuario
                                               join uu in Conexion.Usuarios on v.ID_USUARIO_AUTORIZO equals uu.Usuario
                                               join s in Conexion.TBL_SUSCRIPCION_DOCUMENTO on d.ID_DOCUMENTO equals s.ID_DOCUMENTO
                                               where s.ID_USUARIO_SUSCRITO == usuario && v.ID_ESTATUS_VERSION == 1 && d.ID_ESTATUS_DOCUMENTO == 5
                                               select new
                                               {
                                                   d.ID_DOCUMENTO,
                                                   d.NOMBRE,
                                                   d.ID_TIPO_DOCUMENTO,
                                                   FECHA_ACTUALIZACION = v.FECHA_VERSION,
                                                   d.ID_DEPARTAMENTO,
                                                   v.No_VERSION,
                                                   v.ID_VERSION,
                                                   v.NO_COPIAS,
                                                   DESCRIPCION = v.DESCRIPCION,
                                                   dp.NOMBRE_DEPARTAMENTO,
                                                   d.FECHA_EMISION,
                                                   USUARIO_ELABORO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                                   USUARIO_AUTORIZO = uu.Nombre + " " + uu.APaterno + " " + uu.AMaterno

                                               }).ToList();

                    // Retornamos la lista
                    return ListaDocSuscripcion;
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos nulo
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
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var ListaRegistros = (from a in Conexion.TBL_SUSCRIPCION_DOCUMENTO
                                          where a.ID_USUARIO_SUSCRITO == usuario_suscrito
                                          && a.ID_DOCUMENTO == id_documento
                                          select a).ToList().Count;

                    // Retornamos la lista con el número de registros
                    return ListaRegistros;
                }
            }
            catch (Exception)
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
            catch (Exception)
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
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

    }
}
