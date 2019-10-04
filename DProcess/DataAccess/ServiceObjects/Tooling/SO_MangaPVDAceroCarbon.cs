using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Collections;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_MangaPVDAceroCarbon
    {
        /// <summary>
        /// Método para insertar un registro a la tabla TBL_MANGA_PVD_ACER_CARBON
        /// </summary>
        /// <param name="id"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public int SetAceroCarbon(float min, float max, float a, float factor)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesTooling())
                {
                    // Declaramos el objeto de la tabla
                    TBL_MANGA_PVD_ACERO_CARBON obj = new TBL_MANGA_PVD_ACERO_CARBON();

                    //Asignamos los valores
                    obj.N_MIN = min;
                    obj.N_MAX = max;
                    obj.A = a;
                    obj.FACTOR = factor;

                    //Agregar el objeto a la tabla
                    Conexion.TBL_MANGA_PVD_ACERO_CARBON.Add(obj);
                    //Guardamos los cambios
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.ID_MANGA_PVD_ACERO_CARBON;
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar un registro de la tabla TBL_MANGA_PVD_ACERO_CARBON
        /// </summary>
        /// <param name="id"></param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <param name="a"></param>
        /// <returns></returns>
        public int UpdateAceroCarbon(int id, double a)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    TBL_MANGA_PVD_ACERO_CARBON obj = Conexion.TBL_MANGA_PVD_ACERO_CARBON.Where(x => x.ID_MANGA_PVD_ACERO_CARBON == id).FirstOrDefault();

                    //Asignamos los valores
                    obj.A = a;

                    //Guardamos los cambios
                    Conexion.Entry(obj).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla TBL_MANGA_PVD_ACERO_CARBON
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteAceroCarbon(int id)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    TBL_MANGA_PVD_ACERO_CARBON obj = Conexion.TBL_MANGA_PVD_ACERO_CARBON.Where(x => x.ID_MANGA_PVD_ACERO_CARBON == id).FirstOrDefault();

                    Conexion.Entry(obj).State = EntityState.Deleted;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList GetAll()
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var lista = (from a in Conexion.TBL_MANGA_PVD_ACERO_CARBON
                                 select a).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IList GetManga(double n)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    var listaMangas = (from a in Conexion.TBL_MANGA_PVD_ACERO_CARBON
                                       where n >= a.N_MIN && n <= a.N_MAX
                                       select a).ToList();

                    return listaMangas;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
