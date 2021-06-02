using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_Scotchbrite
    {
        /// <summary>
        /// Método que obtiene todos los registros de Scotchbrite.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllCollarS(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.CollarScotchbrite_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where m.Descripcion.Contains(texto) || c.Codigo.Contains(texto)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimF,
                                     c.plano,
                                     m.Descripcion,
                                     m.Activo
                                 }).ToList();
                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error retornamos nulo.
                return null;
            }
        }

        /// <summary>
        /// Métood que obtiene la información de un herramental Collar Scotchbrite.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoCollarScotch(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.CollarScotchbrite_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimF,
                                     c.plano,
                                     m.Descripcion,
                                     m.Activo,
                                     c.Id
                                 }).ToList();
                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error retornamos nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene los herramentales óptimos para Collar Scotchbrite.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IList GetCollarScotch(double min, double max)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.CollarScotchbrite_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where b.DimF >= min && b.DimF <= max && m.Activo == true
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimF,
                                     b.plano,
                                     m.Descripcion,
                                     m.Activo,
                                     Clasificacion = c.Descripcion,
                                     c.UnidadMedida,
                                     c.Costo,
                                     c.CantidadUtilizar,
                                     c.VidaUtil,
                                     c.idClasificacion,
                                     c.ListaCotasRevisar,
                                     c.VerificacionAnual
                                 }).ToList();
                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error retornamos nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que guarda un registro de Scotchbrite.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public int SetCollarScotch(string codigo, string plano, double dimF)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CollarScotchbrite_ obj = new CollarScotchbrite_();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.DimF = dimF;
                    obj.plano = plano;

                    //Guardamos los cambios
                    Conexion.CollarScotchbrite_.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.Id;
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de Scotchbrite.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dimF"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int UpdateCollarScotchbrite(int id, string plano, double dimF)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CollarScotchbrite_ obj = Conexion.CollarScotchbrite_.Where(x => x.Id == id).FirstOrDefault();

                    //Asignamos los valores
                    obj.DimF = dimF;
                    obj.plano = plano;

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
        /// Método que elimina un regitro de CollarScotch
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCollarScotch(int id)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CollarScotchbrite_ obj = Conexion.CollarScotchbrite_.Where(x => x.Id == id).FirstOrDefault();

                    //Guardamos los cambios
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos cero.
                return 0;
            }
        }
    }
}
