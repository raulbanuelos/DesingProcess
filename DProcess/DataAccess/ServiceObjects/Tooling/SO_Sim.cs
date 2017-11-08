using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_Sim
    {
        /// <summary>
        /// Método que obtiene todos los registros de Bushing Sim.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllBushingSim(string texto)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var lista = (from b in Conexion.BushingSIM_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 where b.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimB,
                                     b.Notas,
                                     m.Descripcion,
                                     b.Id_Bushing,
                                     m.Activo
                                 }).ToList();
                    //Retorna la lista
                    return lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la información de un herramental Bushing Sim.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoBushingSim(string codigo)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var Lista = (from b in conexion.BushingSIM_
                                 join m in conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 where b.Codigo.Equals(codigo)
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimB,
                                     b.Notas,
                                     m.Descripcion,
                                     b.Id_Bushing,
                                     m.Activo
                                 }).ToList();
                    //Retornamos la lista.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene los herramentales óptimos deBushing Sim de acuerdo al min y max.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IList GetBushingSim(double min, double max)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.BushingSIM_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where b.DimB >= min && b.DimB <= max && m.Activo == true
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimB,
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
            catch (Exception er)
            {
                //si hay error retornamos nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que inserta un registro en la tabla Bushing Sim.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dimB"></param>
        /// <param name="notas"></param>
        /// <returns></returns>
        public int SetBushingSim(string codigo, double dimB, string notas)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto.
                    BushingSIM_ obj = new BushingSIM_();

                    //Asiganmos los valores.
                    obj.Codigo = codigo;
                    obj.DimB = dimB;
                    obj.Notas = notas;

                    //Guardamos los cambios.
                    Conexion.BushingSIM_.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id.
                    return obj.Id_Bushing;
                }
            }
            catch (Exception)
            {
                //retornamos cero si hubo un error.
                return 0;
            }
        }

        /// <summary>
        /// Modifica un registro de la tabla Bushing Sim.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dimB"></param>
        /// <param name="notas"></param>
        /// <returns></returns>
        public int UpdateBushingSim(int id, double dimB, string notas)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    BushingSIM_ obj = Conexion.BushingSIM_.Where(x => x.Id_Bushing == id).FirstOrDefault();
                  
                    obj.DimB = dimB;
                    obj.Notas = notas;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    Conexion.Entry(obj).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Elimina un registro de la tabla Bushing Sim.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteBushingSim(int id)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    BushingSIM_ obj = Conexion.BushingSIM_.Where(x => x.Id_Bushing == id).FirstOrDefault();

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros de Pusher Sim.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllPusherSim(string texto)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var lista = (from b in Conexion.PusherSIM_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 where b.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimD,
                                     m.Descripcion,
                                     b.ID_Pushing,
                                     m.Activo
                                 }).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
               return null;
            }
        }

        /// <summary>
        /// Método que obtiene la información de un herramental Pusher a partir del código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoPusher(string codigo)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    var lista = (from b in Conexion.PusherSIM_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 where b.Codigo.Equals(codigo)
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimD,
                                     m.Descripcion,
                                     b.ID_Pushing,
                                     m.Activo
                                 }).ToList();
                    //Retornamos la lista.
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene los registros óptimos para Pusher Sim a partir del diametro Bushing.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IList GetPusher(double min, double max)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.PusherSIM_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where b.DimD >= max && b.DimD <= min && m.Activo == true
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimD,
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
            catch (Exception er)
            {
                //si hay error retornamos nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que guarda un registro Pusher Sim.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dimD"></param>
        /// <returns></returns>
        public int SetPusher(string codigo, double dimD)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    PusherSIM_ obj = new PusherSIM_();

                    obj.Codigo = codigo;
                    obj.DimD = dimD;

                    Conexion.PusherSIM_.Add(obj);
                    Conexion.SaveChanges();

                    return obj.ID_Pushing;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de la tabla Bushing Sim.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dimD"></param>
        /// <returns></returns>
        public int UpdatePusher(int id, double dimD)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    PusherSIM_ obj = Conexion.PusherSIM_.Where(x => x.ID_Pushing == id).FirstOrDefault();

                    obj.DimD = dimD;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    Conexion.Entry(obj).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro de la tabla Bushing Sim.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeletePusher(int id)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    PusherSIM_ obj = Conexion.PusherSIM_.Where(x => x.ID_Pushing == id).FirstOrDefault();

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    Conexion.Entry(obj).State = EntityState.Deleted;
                    //Guardamos los cambios.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que pbtiene todos los registros de la tabla Guillotina Sim.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllGuillotinaSim(string texto)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var lista = (from b in Conexion.GuillotinaSIM_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 where b.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimA,
                                     m.Descripcion,
                                     b.WidthMax,
                                     b.WidthMin,
                                     b.Id_Guillotina,
                                     m.Activo
                                 }).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la información de un herramental Guillotina Sim.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoGuillotinaSim(string codigo)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var lista = (from b in Conexion.GuillotinaSIM_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 where b.Codigo.Equals(codigo)
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimA,
                                     b.WidthMin,
                                     b.WidthMax,
                                     b.CantidadAnillos,
                                     m.Descripcion,
                                     b.Id_Guillotina,
                                     m.Activo
                                 }).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene los herramentales óptimos para Guillotina Sim.
        /// </summary>
        /// <param name="h1"></param>
        /// <returns></returns>
        public IList GetguillotinaSim(double h1)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.GuillotinaSIM_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where h1 >= b.WidthMin && h1 <= b.WidthMax && m.Activo == true
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimA,
                                     b.WidthMin,
                                     b.WidthMax,
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
            catch (Exception er)
            {
                //si hay error retornamos nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que guarda un registro en la tabla Guillotina Sim.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dimA"></param>
        /// <param name="WidthMin"></param>
        /// <param name="WidthMax"></param>
        /// <param name="anillos"></param>
        /// <returns></returns>
        public int SetGuillotinaSim(string codigo,double dimA, double WidthMin, double WidthMax,int anillos )
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    GuillotinaSIM_ obj = new GuillotinaSIM_();

                    obj.Codigo = codigo;
                    obj.DimA = dimA;
                    obj.WidthMin = WidthMin;
                    obj.WidthMax = WidthMax;
                    obj.CantidadAnillos = anillos;

                    Conexion.GuillotinaSIM_.Add(obj);
                    Conexion.SaveChanges();

                    return obj.Id_Guillotina;
                }
            }
            catch (Exception er)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro en la tabla Guillotina Sim.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dimA"></param>
        /// <param name="WidthMin"></param>
        /// <param name="WidthMax"></param>
        /// <param name="anillos"></param>
        /// <returns></returns>
        public int UpdateGuillotinaSim(int id, double dimA, double WidthMin, double WidthMax, int anillos)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    GuillotinaSIM_ obj = Conexion.GuillotinaSIM_.Where(x => x.Id_Guillotina == id).FirstOrDefault();
                    
                    obj.DimA = dimA;
                    obj.WidthMin = WidthMin;
                    obj.WidthMax = WidthMax;
                    obj.CantidadAnillos = anillos;

                    Conexion.Entry(obj).State = EntityState.Modified;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro en la tabla Guillotina Sim.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteGuillotinaSim(int id)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    GuillotinaSIM_ obj = Conexion.GuillotinaSIM_.Where(x => x.Id_Guillotina == id).FirstOrDefault();

                    Conexion.Entry(obj).State = EntityState.Deleted;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
