using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_TubosHD
    {
        #region Métodos
        /// <summary>
        /// Método el cúal se obtiene los registros de la tabla TubosHD.
        /// </summary>
        /// <returns></returns>
        public IList GetTubosHD()
        {
            try
            {
                //Establecemos la conexióna través de Entity Framework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Se realiza la consulta
                    var Lista = (from t in Conexion.TubosHD
                                 select new
                                 {
                                     t.Tubo,
                                     t.DiaExt,
                                     t.DiaInt,
                                     t.Thickness,
                                     t.Largo,
                                     t.Molde,
                                     t.RPM
                                 }).ToList();
                    //Retornamos la lista, con el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay algún error se retorna nulo.
                return null;
            }
        }

        /// <summary>
        /// Método para insetar un registro a la tabla TubosHD.
        /// </summary>
        /// <param name="tubo"></param>
        /// <param name="diaExt"></param>
        /// <param name="diaInt"></param>
        /// <param name="thickness"></param>
        /// <param name="largo"></param>
        /// <param name="molde"></param>
        /// <param name="rpm"></param>
        /// <returns>Retorna una cadena vacía si hay un error.</returns>
        public string SetTubosHD(string tubo, double diaExt, double diaInt, double thickness, double largo,string molde,int rpm)
        {
            try
            {
                //Se inicia la conexión a la BD.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Se crea un objeto de la tabla.
                    TubosHD obj = new TubosHD();

                    //Se asignan los valores que se recibieron coomo parámetro.
                    obj.Tubo = tubo;
                    obj.DiaExt = diaExt;
                    obj.DiaInt = diaInt;
                    obj.Thickness = thickness;
                    obj.Largo = largo;
                    obj.Molde = molde;
                    obj.RPM = rpm;

                    //agrega el objeto a la BD.
                    Conexion.TubosHD.Add(obj);

                    //Se guardan los cambios realizados.
                    Conexion.SaveChanges();

                    //Retorna código del tubo que se agregó.
                    return obj.Tubo;
                }
            }
            catch (Exception)
            {
                //Si existe algún error se retorna una cadena vacía.
                return string.Empty;
            }
        }

        /// <summary>
        /// Método para actualizar registros en la tabla TubosHD.
        /// </summary>
        /// <param name="tubo"></param>
        /// <param name="diaExt"></param>
        /// <param name="diaInt"></param>
        /// <param name="thickness"></param>
        /// <param name="largo"></param>
        /// <param name="molde"></param>
        /// <param name="rpm"></param>
        /// <returns></returns>
        public int UpdateTubosHD(string tubo, double diaExt, double diaInt, double thickness, double largo, string molde, int rpm)
        {
            try
            {
                //Se inicia la conexión a la BD.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Se obtiene el registro que se va a modificar.
                    TubosHD obj = Conexion.TubosHD.Where(x=>x.Tubo==tubo).FirstOrDefault();
                    //se modifican los datos
                    obj.DiaExt = diaExt;
                    obj.DiaInt = diaInt;
                    obj.Thickness = thickness;
                    obj.Largo = largo;
                    obj.Molde = molde;
                    obj.RPM = rpm;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;


                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error, regresa cero.
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro de la BD.
        /// </summary>
        /// <param name="tubo"></param>
        /// <returns></returns>
        public int DeleteTubosHD(string tubo)
        {
            try
            {
                //Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TubosHD obj = Conexion.TubosHD.Where(x => x.Tubo == tubo).FirstOrDefault();

                    //Se estable el estado de registro a eliminado.
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //si hay error, regresa cero.
                return 0;
            }
        }
        #endregion
    }
}
