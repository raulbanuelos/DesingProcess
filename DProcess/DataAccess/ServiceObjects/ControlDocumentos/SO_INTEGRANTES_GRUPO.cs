using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_INTEGRANTES_GRUPO
    {
        public IList GetAll(int idGrupoSeleccionado)
        {
            try
            {
                // Realizamos la  conexion
                using(var Conexion = new EntitiesControlDocumentos())
                {
                    var lista = (from a in Conexion.TR_INTEGRANTES_GRUPO
                                 join u in Conexion.Usuarios on a.ID_USUARIO_INTEGRANTE equals u.Usuario
                                 where a.ID_GRUPO == idGrupoSeleccionado
                                 select new
                                 {
                                     a.ID_GRUPO,
                                     a.ID_USUARIO_INTEGRANTE,
                                     ID_INTEGRANTES_GRUPO = a.ID_INTEGRANTES_GRUPO,
                                     nombrecompleto = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                 }
                                 ).ToList();                    
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int SetIntegrantesGrupos(int id_grupo, string id_usuario_integrante)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using(var Conexion = new EntitiesControlDocumentos())
                {
                    // Declaramos el objeto de la tabla
                    TR_INTEGRANTES_GRUPO obj = new TR_INTEGRANTES_GRUPO();

                    // Asignamos valores
                    obj.ID_GRUPO = id_grupo;
                    obj.ID_USUARIO_INTEGRANTE = id_usuario_integrante;

                    // Agregamos el objeto a la tabla
                    Conexion.TR_INTEGRANTES_GRUPO.Add(obj);

                    // Guardamos cambios
                    Conexion.SaveChanges();

                    // Retornamos el id
                    return obj.ID_INTEGRANTES_GRUPO;
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        public int DeleteIntegrantesGrupos(int id_grupo, string id_integrante)
        {
            try
            {
                // Realizamos la conexion
                using(var Conexion = new EntitiesControlDocumentos())
                {
                    // Declaramos el objeto de la tabla
                    TR_INTEGRANTES_GRUPO obj = Conexion.TR_INTEGRANTES_GRUPO.Where(x => x.ID_GRUPO == id_grupo && x.ID_USUARIO_INTEGRANTE == id_integrante).FirstOrDefault();

                    // Guardamos los cambios
                    Conexion.Entry(obj).State = System.Data.Entity.EntityState.Deleted;
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
