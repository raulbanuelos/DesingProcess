using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.ServiceObjects.Normas
{
    public class SO_Normas
    {
        public IList GetAll()
    {
        try
        {
            using(var Conexion = new EntitiesNormas())
            {
                var lista = (from a in Conexion.TBL_NORMAS
                             select a).ToList();

                return lista;
            }
        }
        catch (Exception er)
        {
            return null;
        }
    }

        /// <summary>
        /// Método para insertar un registro a la tabla TBL_NOMRMAS
        /// </summary>
        /// <param name="id_norma"></param>
        /// <param name="especificacion"></param>
        /// <param name="descripcion_corta"></param>
        /// <param name="descripcion_larga"></param>
        /// <returns></returns>
        public int SetNorma(string especificacion, string descripcion_corta, string descripcion_larga)
        {
            try
            {
                //Realizamos la conexión a través de Entity Framework
                using(var Conexion = new EntitiesNormas())
                {
                    TBL_NORMAS obj = new TBL_NORMAS();

                    //Asignamos valores
                    obj.ESPECIFICACION = especificacion;
                    obj.DESCRIPCION_CORTA = descripcion_corta;
                    obj.DESCRIPCION_LARGA = descripcion_larga;

                    //Agregamos el objeto a la tabla
                    Conexion.TBL_NORMAS.Add(obj);

                    //Guardamos los cambios
                    Conexion.SaveChanges();

                    //retornamos el ID
                    return obj.ID_NORMA;
                }
            }
            catch (Exception)
            {
                //Si hay error retornamos 0
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar un registro de la tabla TBL_NORMAS
        /// </summary>
        /// <param name="id_norma"></param>
        /// <param name="especificacion"></param>
        /// <param name="descripcion_corta"></param>
        /// <param name="descripcion_larga"></param>
        /// <returns></returns>
        public int UpdateNorma(int id_norma, string especificacion, string descripcion_corta, string descripcion_larga)
        {
            try
            {
                //Realizamos la conexión a través de Entity Framework
                using (var Conexion = new EntitiesNormas())
                {
                    //Declaramos el objeto de la tabla
                    TBL_NORMAS obj = Conexion.TBL_NORMAS.Where(x => x.ID_NORMA == id_norma).FirstOrDefault();

                    //Asignamos los valores
                    obj.ESPECIFICACION = especificacion;
                    obj.DESCRIPCION_CORTA = descripcion_corta;
                    obj.DESCRIPCION_LARGA = descripcion_larga;

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
        /// Método para eliminar un registro de la tabla TBL_NORMAS
        /// </summary>
        /// <param name="id_norma"></param>
        /// <returns></returns>
        public int DeleteNorma(int id_norma)
        {
            try
            {
                //Establecemos conexión a través de Entity Framework
                using(var Conexion = new EntitiesNormas())
                {
                    //Declaramos el objeto de la tabla
                    TBL_NORMAS obj = Conexion.TBL_NORMAS.Where(x => x.ID_NORMA == id_norma).FirstOrDefault();

                    Conexion.Entry(obj).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos o
                return 0;
            }
        }

    }
}
