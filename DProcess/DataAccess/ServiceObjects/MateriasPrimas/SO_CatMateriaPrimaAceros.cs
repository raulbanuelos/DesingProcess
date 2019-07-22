using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_CatMateriaPrimaAceros
    {
        /// <summary>
        /// Método para insertar un registro a la tabla CAT_MATERIA_PRIMA_ACEROS
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="id_material"></param>
        /// <param name="esp_axial"></param>
        /// <param name="esp_radial"></param>
        /// <param name="proveedor"></param>
        /// <param name="proveedor2"></param>
        /// <returns></returns>
        public string SetCatMateriaPrimaAcero(string codigo, string id_material, double esp_axial, double esp_radial, string proveedor, string proovedor2)
        {
            try
            {
                // Realizamos la conexión a través de EntityFramework
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    // Declaramos el objeto de la tabla
                    CAT_MATERIA_PRIMA_ACEROS obj = new CAT_MATERIA_PRIMA_ACEROS();

                    //Asignamos los valores
                    obj.CODIGO = codigo;
                    obj.ID_MATERIAL = id_material;
                    obj.ESP_AXIAL = esp_axial;
                    obj.ESP_RADIAL = esp_radial;
                    obj.PROVEEDOR = proveedor;
                    obj.PROVEEDOR2 = proovedor2;

                    //Agregar el objeto a la tabla
                    Conexion.CAT_MATERIA_PRIMA_ACEROS.Add(obj);
                    //Guardamos los cambios
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.CODIGO;
                }
            }
            catch (Exception)
            {
                // Si hay error retornamos 0
                return null;
            }
        }

        /// <summary>
        /// Método para modificar un registro de la tabla CAT_MATERIA_PRIMA_ACEROS
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="id_material"></param>
        /// <param name="esp_axial"></param>
        /// <param name="esp_radial"></param>
        /// <param name="proveedor"></param>
        /// <param name="proveedor2"></param>
        /// <returns></returns>
        public int UpdateCatMateriaPrimaAcero(string codigo, string id_material, double esp_axial, double esp_radial, string proveedor, string proovedor2)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Declaramos el objeto de la tabla.
                    CAT_MATERIA_PRIMA_ACEROS obj = Conexion.CAT_MATERIA_PRIMA_ACEROS.Where(x => x.CODIGO == codigo).FirstOrDefault();

                    //Asignamos los valores
                    obj.CODIGO = codigo;
                    obj.ID_MATERIAL = id_material;
                    obj.ESP_AXIAL = esp_axial;
                    obj.ESP_RADIAL = esp_radial;
                    obj.PROVEEDOR = proveedor;
                    obj.PROVEEDOR2 = proovedor2;

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
        /// Método para eliminar un registro de la tabla CAT_MATERIA_PRIMA_ACEROS
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int DeleteCatMateriaPrimaAcero(string codigo)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    CAT_MATERIA_PRIMA_ACEROS obj = Conexion.CAT_MATERIA_PRIMA_ACEROS.Where(x => x.CODIGO == codigo).FirstOrDefault();

                    Conexion.Entry(obj).State = EntityState.Deleted;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList GetMateriaPrimaPVD(double h1, double mpaxialWidthMinPVD, double mpaxialWidthMaxPVD, double a1Min, double a1Max, double mpradialThickMinPVD, double MPRadialThickMaxPVD, double quita_scotch)
        {
            double a1 = Math.Round((a1Min + a1Max) / 2, 4);

            try
            {
                //Incializamos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta para obtener todos los registros
                    var lista = (from c in Conexion.CAT_MATERIA_PRIMA_ACEROS
                                 where c.ESP_AXIAL >= (h1 - mpaxialWidthMinPVD) && c.ESP_AXIAL <= (h1 + mpaxialWidthMaxPVD)
                                        && c.ESP_RADIAL >= (a1 - mpradialThickMinPVD) && c.ESP_RADIAL <= (a1 + MPRadialThickMaxPVD)
                                        && (c.ESP_RADIAL - quita_scotch - .001) >= a1Min
                                 select new
                                 {
                                     c.CODIGO,
                                     c.ESP_AXIAL,
                                     c.ESP_RADIAL,
                                     c.ID_MATERIAL,
                                     c.PROVEEDOR,
                                     c.PROVEEDOR2,
                                 }).ToList();

                    //Renornamos el resultado de la consulta
                    return lista;
                }
            }
            catch (Exception er)
            {
                //Si se generó algún error, retornamos un nulo.
                return null;
            }
        }
    }
}
