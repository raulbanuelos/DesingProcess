using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_Cuffs 
    {
        #region Métodos
        /// <summary>
        /// Método el cual se obtiene los registro de la tabla cuffs.
        /// </summary>
        /// <returns></returns>
        public IList GetCuff()
        {
            try
            {
                //Establecemos la conexíon a través de Entity Framework
                using (var Conexion= new EntitiesMateriaPrima())
                {
                    //Se realiza la consulta
                    var Lista = (from c in Conexion.cuffs
                                 select new
                                 {
                                     c.no_cuff,c.dia_ext,c.dia_int, c.largo,c.peso
                                 }).ToList();
                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay algún error, se retorna nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que inserta un nuevo registro a la tabla Cuffs.
        /// </summary>
        /// <param name="no_cuff"></param>
        /// <param name="dia_ext"></param>
        /// <param name="dia_int"></param>
        /// <param name="largo"></param>
        /// <param name="peso"></param>
        /// <returns>retorn una cadena vacía si encuentra error.</returns>
        public string SetCuff(string no_cuff,double dia_ext,double dia_int,double largo,double peso)
        {
            try
            {
                //Se establece la conexión a la BD.
                using (var Conexion= new EntitiesMateriaPrima())
                {
                    //Se crea un objeto de tipo Cuff, el cúal será añadido a la tabla Cuffs
                    cuffs obj = new cuffs();
                    //Asignamos los parámetros recibidos a cada uno de los valores del objeto cuff
                    obj.no_cuff = no_cuff;
                    obj.dia_ext = dia_ext;
                    obj.dia_int = dia_int;
                    obj.largo = largo;
                    obj.peso = peso;

                    //Insertamos el objeto a la base de datos
                    Conexion.cuffs.Add(obj);

                    //Se guardan los camabios
                    Conexion.SaveChanges();

                    //Se retorna el númeor de cuff
                    return obj.no_cuff;
                }

            }
            catch (Exception)
            {
                //Si hay un error se retorna el string vacío.
                return string.Empty;
            }
        }
        /// <summary>
        /// Método para actualizar los valores de un registro de la tabla Cuff.
        /// </summary>
        /// <param name="no_cuff"></param>
        /// <param name="dia_ext"></param>
        /// <param name="dia_int"></param>
        /// <param name="largo"></param>
        /// <param name="peso"></param>
        /// <returns>Retorna cero si hay algún error</returns>
        public int UpdateCuffs(string no_cuff, double dia_ext, double dia_int, double largo, double peso)
        {
            try
            {
                //Se establece la conexión a la BD.
                using (var Conexion= new EntitiesMateriaPrima())
                {
                    //Se crea el objeto de tipo cuff.
                    cuffs obj = Conexion.cuffs.Where(x => x.no_cuff == no_cuff).FirstOrDefault();
                    //Se modifican los datos.
                    obj.dia_ext = dia_ext;
                    obj.dia_int = dia_int;
                    obj.largo = largo;
                    obj.peso = peso;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State= EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si encuentra error devuelve cero.
                return 0;
            }
        }
        
           
        /// <summary>
        /// Método para eliminar un registro en la tabla Cuffs.
        /// </summary>
        /// <param name="no_cuff"></param>
        /// <returns>Si encuentre un error, regresa cero.</returns>
        public int DeleteCuffs(string no_cuff)
        {
            try
            {   //Se inicializa la conexión a la base de datos.
                using (var Conexion= new EntitiesMateriaPrima())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    cuffs obj = Conexion.cuffs.Where(x => x.no_cuff == no_cuff).FirstOrDefault();

                    //Se estable el estado de registro a eliminado.
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();

                }
            }
            catch (Exception)
            {
                //Si hay error, se regresa 0.
                return 0;
            }
        }

        #endregion
    }
}
