using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_GRUPOS
    {
        public IList GetAll(string usuario)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    var lista = (from a in Conexion.TBL_GRUPOS
                                 where a.ID_USUARIO_DUENO == usuario
                                 select a).ToList();

                    return lista;
                }
            }
            catch (Exception er)
            {
                return null;
            }
        }

        public int SetGrupo(string nombre, string id_usuario_dueno)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    // Declaramos el objeto de la tabla
                    TBL_GRUPOS obj = new TBL_GRUPOS();

                    // Asignamos los valores
                    obj.NOMBRE = nombre;
                    obj.ID_USUARIO_DUENO = id_usuario_dueno;

                    // Agregamos el objeto a la tabla
                    Conexion.TBL_GRUPOS.Add(obj);

                    // Guardamos los cambios
                    Conexion.SaveChanges();

                    // Retornamos el id
                    return obj.ID_GRUPO;
                }
            }
            catch (Exception er)
            {
                // Si hay un error retoramos un 0
                return 0;
            }
        }

        //public int UpdateGrupo(int id_grupo, string nombre, string id_usuario_dueno)
        //{
        //    try
        //    {
        //        // Realizamos al conexión a través de EntityFramework
        //        using(var Conexion = new EntitiesControlDocumentos())
        //        {
        //            // Declaramos el objeto de la lista
        //            TBL_GRUPOS obj = Conexion.TBL_GRUPOS.Where(x => x.ID_GRUPO == id_grupo).FirstOrDefault();

        //            // Asignamos los valores
        //            obj.NOMBRE = nombre;

        //            // Guardamos los cambios
        //            Conexion.Entry(obj).State = System.Data.Entity.EntityState.Modified;
        //            return Conexion.SaveChanges();
        //            }
        //    }
        //    catch (Exception)
        //    {
        //        // Si hay error retornamos 0
        //        return 0;
        //    }
        //}

        public int DeleteGrupo(int id_grupo)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using(var Conexion = new EntitiesControlDocumentos())
                {
                    // Declaramos el objeto de la lista
                    TBL_GRUPOS obj = Conexion.TBL_GRUPOS.Where(x => x.ID_GRUPO == id_grupo).FirstOrDefault();

                    // Guardamos los cambios
                    Conexion.Entry(obj).State = System.Data.Entity.EntityState.Deleted;

                    // Gardamos los cambios
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
