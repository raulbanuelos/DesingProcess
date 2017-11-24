using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_MotingWidth
    {
        /// <summary>
        /// Método que obtiene todos los registros.
        /// </summary>
        /// <returns></returns>
        public IList GetAllMoutingWidth()
        {
            try
            {
                //Establecemos la conexión al EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Relizamos la consulta y la guardamos en una variable local.
                    var Lista = (from m in Conexion.MoutingWidth
                                 select m).ToList();
                    //Retornamos la lista resultante.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si se genera algún error retorna nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que guarda un registro de MoutingWidth.
        /// </summary>
        /// <param name="wmin"></param>
        /// <param name="wmax"></param>
        /// <param name="detalle"></param>
        /// <param name="gate"></param>
        /// <returns></returns>
        public int SetMoutingWidth(double wmin, double wmax, string detalle, double gate)
        {
            try
            {
                //Establecemos la conexión al EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    MoutingWidth  obj = new MoutingWidth();

                    obj.Width_Min = wmin;
                    obj.Width_Max = wmax;
                    obj.Detalle = detalle;
                    obj.Altura_Gate = gate;

                    Conexion.MoutingWidth.Add(obj);

                    //Guardamos los cambios.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si se genera algún error retorna cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de MoutingWidth.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="wmin"></param>
        /// <param name="wmax"></param>
        /// <param name="detalle"></param>
        /// <param name="gate"></param>
        /// <returns></returns>
        public int UpdateMoutingWidth(int id,double wmin, double wmax, string detalle, double gate)
        {
            try
            {
                //Establecemos la conexión al EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    MoutingWidth obj = Conexion.MoutingWidth.Where(x => x.Id_MountingWidth == id).FirstOrDefault();

                    obj.Width_Min = wmin;
                    obj.Width_Max = wmax;
                    obj.Detalle = detalle;
                    obj.Altura_Gate = gate;

                    //Guardamos los cambios.
                    Conexion.Entry(obj).State = EntityState.Modified;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si se genera algún error retorna cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro de MoutingWidth.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteMoutingWidth(int id)
        {
            try
            {
                //Establecemos la conexión al EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    MoutingWidth obj = Conexion.MoutingWidth.Where(x => x.Id_MountingWidth == id).FirstOrDefault();

                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Guardamos los cambios.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si se genera algún error retorna cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene el detalle de MoutingWidth.
        /// </summary>
        /// <param name="H1"></param>
        /// <returns></returns>
        public string GetDetalle(double H1)
        {
            try
            {
                //Establecemos la conexión al EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Relizamos la consulta y la guardamos en una variable local.
                    string detalle = (from m in Conexion.MoutingWidth
                                      where H1 >= m.Width_Min && H1 <= m.Width_Max
                                      select m.Detalle).FirstOrDefault();
                    //Retornamos la variable.
                    return detalle;
                }
            }
            catch (Exception)
            {
                //Si se genera algún error retornamos vacío.
                return string.Empty;
            }
        }

    }
}
