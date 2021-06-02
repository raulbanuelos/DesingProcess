using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_Cromo
    {
        /// <summary>
        /// Método que obtiene todos los herramentales de BushingCromo.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllBushingCromo(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.BushingCromo_
                                 join m in Conexion.MaestroHerramentales on b.CODIGO equals m.Codigo
                                 where m.Descripcion.Contains(texto) || b.CODIGO.Contains(texto)
                                 select new
                                 {
                                     b.CODIGO,
                                     b.DimD,
                                     b.Plano,
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
        /// Método que obtiene los herramentales que se encuentren entre el rango min y max.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IList GetBushingCromo(double min, double max)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.BushingCromo_
                                 join m in Conexion.MaestroHerramentales on b.CODIGO equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where b.DimD >= min && b.DimD <= max && m.Activo == true
                                 select new
                                 {
                                     Codigo=b.CODIGO,
                                     b.DimD,
                                     b.Plano,
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
        /// Método que obtiene la información de un herramental BushingCromo.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoBushingCromo(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.BushingCromo_
                                 join m in Conexion.MaestroHerramentales on b.CODIGO equals m.Codigo
                                 where b.CODIGO.Equals(codigo)
                                 select new
                                 {
                                     b.CODIGO,
                                     b.DimD,
                                     b.Plano,
                                     m.Descripcion,
                                     m.Activo,
                                     b.Id_BushingCromo
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
        /// Método que guarda un registro en la tabla BushingCromo.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dimD"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int SetBushingCromo(string codigo, double dimD, string plano)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    BushingCromo_ obj = new BushingCromo_();

                    //Asignamos los valores
                    obj.CODIGO = codigo;
                    obj.DimD = dimD;
                    obj.Plano = plano;
                    //Guardamos los cambios
                    Conexion.BushingCromo_.Add(obj);
                    Conexion.SaveChanges();
                    //Retornamos el id
                    return obj.Id_BushingCromo;
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de la tabla BushingCromo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="dimD"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int UpdateBushingCromo(int id, string codigo, double dimD, string plano)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    BushingCromo_ obj = Conexion.BushingCromo_.Where(x => x.Id_BushingCromo == id).FirstOrDefault();

                    //Asignamos los valores
                    obj.DimD = dimD;
                    obj.Plano = plano;
                    //Guardamos los cambios
                    Conexion.Entry(obj).State = EntityState.Modified;
                    //Retornamos el id
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
        /// Método que elimina un registro de la tabla BushingCromo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteBushingCromo(int id)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    BushingCromo_ obj = Conexion.BushingCromo_.Where(x => x.Id_BushingCromo == id).FirstOrDefault();

                    //Guardamos los cambios
                    Conexion.Entry(obj).State = EntityState.Deleted;
                    //Retornamos el id
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
        /// Método que obtiene todos los registros de collars Cromo.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllCollarsCromo(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.CollarsCromo_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where m.Descripcion.Contains(texto) || c.Codigo.Contains(texto)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimA,
                                     c.Plano,
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
        /// Método que obtiene los herramentales óptimos un herramental CollarsCromo.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IList GetCollarsCromo(double min,double max)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.CollarsCromo_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 join cl in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals cl.idClasificacion
                                 where c.DimA >= min && c.DimA <= max && m.Activo == true
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimA,
                                     c.Plano,
                                     m.Descripcion,
                                     m.Activo,
                                     Clasificacion = cl.Descripcion,
                                     cl.UnidadMedida,
                                     cl.Costo,
                                     cl.CantidadUtilizar,
                                     cl.VidaUtil,
                                     cl.idClasificacion,
                                     cl.ListaCotasRevisar,
                                     cl.VerificacionAnual
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
        /// Método que obtiene la información de un herramental CollarsCromo.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoCollarsCromo(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.CollarsCromo_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimA,
                                     c.Plano,
                                     m.Descripcion,
                                     m.Activo,
                                     c.Id_Collar
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
        /// Método que guarda un registro de la tabala CollarsCromo.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dimA"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int SetCollarsCromo(string codigo, double dimA, string plano)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CollarsCromo_ obj = new CollarsCromo_();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.DimA = dimA;
                    obj.Plano = plano;
                    //Guardamos los cambios
                    Conexion.CollarsCromo_.Add(obj);
                    Conexion.SaveChanges();
                    //Retornamos el id
                    return obj.Id_Collar;
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de la tabala CollarsCromo.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="dimA"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int UpdateCollarsCromo(int id, string codigo, double dimA, string plano)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CollarsCromo_ obj = Conexion.CollarsCromo_.Where(x => x.Id_Collar == id).FirstOrDefault();

                    //Asignamos los valores
                    obj.DimA = dimA;
                    obj.Plano = plano;
                    //Guardamos los cambios
                    Conexion.Entry(obj).State = EntityState.Modified;
                    //Retornamos el id
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
        /// Método que elimina un registro de la tabala CollarsCromo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCollarsCromo(int id)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CollarsCromo_ obj = Conexion.CollarsCromo_.Where(x => x.Id_Collar == id).FirstOrDefault();

                    Conexion.Entry(obj).State = EntityState.Deleted;
                    //Retornamos el id
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
