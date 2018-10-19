using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Usuario
{
    public class SO_LeccionesTipoCambio
    {
        #region Métodos

        /// <summary>
        /// Método que obtiene todos los registros
        /// </summary>
        /// <returns></returns>
        public IList GetLeccionesTipoCambio(int Id_Leccion)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    //Seleccionamos todos los registros y lo convertimos a una lista
                    var ListaLeccionesTipoCambio = (from a in Conexion.TBL_TIPOCAMBIO
                                                    join b in Conexion.TR_LECCIONES_TIPOCAMBIO on a.ID_TIPOCAMBIO equals b.ID_TIPOCAMBIO
                                                    where b.ID_LECCIONAPRENDIDA == Id_Leccion
                                                    select a).ToList();

                    //Regresamos todos los valores seleccionados
                    return ListaLeccionesTipoCambio;
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
        /// <param name="Id_TipoCambio"></param>
        /// <param name="Id_LeccionesAprendidas"></param>
        /// <returns></returns>
        public int InsertLeccionesTipoCambio(int Id_TipoCambio, int Id_LeccionesAprendidas)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    TR_LECCIONES_TIPOCAMBIO obj = new TR_LECCIONES_TIPOCAMBIO();
                    obj.ID_TIPOCAMBIO = Id_TipoCambio;
                    obj.ID_LECCIONAPRENDIDA = Id_LeccionesAprendidas;

                    Conexion.TR_LECCIONES_TIPOCAMBIO.Add(obj);
                    Conexion.SaveChanges();

                    return obj.ID_LECCIONES_TIPOCAMBIO;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro
        /// </summary>
        /// <param name="id_lecciones_tipoCambio"></param>
        /// <returns></returns>
        public int DeleteLeccionesTipoCambio(int id_lecciones)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    var ListaTiposCambio = (from a in Conexion.TR_LECCIONES_TIPOCAMBIO
                                   where a.ID_LECCIONAPRENDIDA == id_lecciones
                                   select a).ToList();

                    foreach (var item in ListaTiposCambio)
                    {
                        Conexion.Entry(item).State = EntityState.Deleted;
                    }
                    Conexion.SaveChanges();
                    return 1;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para actualizar un registro
        /// </summary>
        /// <param name="id_LeccionesTipoCambio"></param>
        /// <param name="Id_TipoCambio"></param>
        /// <param name="id_LeccionAprendida"></param>
        /// <returns></returns>
        public int UpdateLeccionesTipoCambio(int id_LeccionesTipoCambio, int Id_TipoCambio, int id_LeccionAprendida)
        {
            try
            {
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    TR_LECCIONES_TIPOCAMBIO obj = Conexion.TR_LECCIONES_TIPOCAMBIO.Where(x => x.ID_LECCIONES_TIPOCAMBIO == id_LeccionesTipoCambio).FirstOrDefault();

                    obj.ID_TIPOCAMBIO = Id_TipoCambio;
                    obj.ID_LECCIONAPRENDIDA = id_LeccionAprendida;

                    Conexion.Entry(obj).State = EntityState.Modified;

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
