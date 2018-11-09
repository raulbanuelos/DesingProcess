using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Usuario
{
    public class SO_LeccionesCentroTrabajo
    {
        #region Métodos

        /// <summary>
        /// Método para obtener todos los registros
        /// </summary>
        /// <returns></returns>
        public IList GetLeccionesCentroTrabajo(int Id_leccion)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    var Lista = (from a in Conexion.CentroTrabajo
                                 join b in Conexion.TR_LECCIONES_CENTROSTRABAJO on a.CentroTrabajo1 equals b.ID_CENTROTRABAJO
                                 where b.ID_LECCIONESAPRENDIDAS == Id_leccion
                                 select a).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método para insertar un nuevo registro
        /// </summary>
        /// <param name="id_CentroTrabajo"></param>
        /// <param name="id_LeccionesAprendidas"></param>
        /// <returns></returns>
        public int SetLeccionesCentroTrabajo(string id_CentroTrabajo, int id_LeccionesAprendidas)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    TR_LECCIONES_CENTROSTRABAJO obj = new TR_LECCIONES_CENTROSTRABAJO();

                    obj.ID_CENTROTRABAJO = id_CentroTrabajo;
                    obj.ID_LECCIONESAPRENDIDAS = id_LeccionesAprendidas;

                    //Agregamos el nuevo registro
                    Conexion.TR_LECCIONES_CENTROSTRABAJO.Add(obj);

                    //Guardamos los cambios
                    Conexion.SaveChanges();

                    return obj.ID_LECCIONES_CENTROTRABAJO;

                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro segun el id
        /// </summary>
        /// <param name="id_Lecciones_CentroTrabajo"></param>
        /// <returns></returns>
        public int DeleteLeccionesCentroTrabajo(int id_Lecciones)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    var CentrosTrabajo = (from a in Conexion.TR_LECCIONES_CENTROSTRABAJO
                               where a.ID_LECCIONESAPRENDIDAS == id_Lecciones
                               select a).ToList();

                    foreach (var item in CentrosTrabajo)
                    {
                        //Borramos el registro
                        Conexion.Entry(item).State = EntityState.Deleted;
                    }
                    Conexion.SaveChanges();
                    //Guardamos cambios
                    return 1;
                }
            }
            catch (Exception)
            {
                //si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar un registro existente mediante el id
        /// </summary>
        /// <param name="id_Lecciones_CentroTrabajo"></param>
        /// <param name="Id_CentroTrabajo"></param>
        /// <param name="Id_LeccionesAprendidas"></param>
        /// <returns></returns>
        public int UpdateLeccionesCentroTrabajo(int id_Lecciones_CentroTrabajo, string Id_CentroTrabajo, int Id_LeccionesAprendidas)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    TR_LECCIONES_CENTROSTRABAJO obj = Conexion.TR_LECCIONES_CENTROSTRABAJO.Where(x => x.ID_LECCIONES_CENTROTRABAJO == id_Lecciones_CentroTrabajo).FirstOrDefault();

                    obj.ID_CENTROTRABAJO = Id_CentroTrabajo;
                    obj.ID_LECCIONESAPRENDIDAS = Id_LeccionesAprendidas;

                    //Modificamos el registro asignando el nuevo archivo
                    Conexion.Entry(obj).State = EntityState.Modified;

                    //Guardamos cambios
                    return Conexion.SaveChanges();

                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion
    }
}
