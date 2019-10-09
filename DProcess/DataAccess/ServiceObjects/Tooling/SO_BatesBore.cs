using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_BatesBore
    {
        /// <summary>
        ///  Método que obtiene todos los registros de acuerdo a la plabra de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllBushing(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.BushingBatesBore_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where (c.Codigo.Contains(texto) || m.Descripcion.Contains(texto) ) && m.Activo == true
                                 select new
                                 {
                                     c.Codigo,
                                     c.Plano,
                                     c.MedidaNominal,
                                     c.DimB,
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
        /// Método que obtiene el collarin a partir de los valores mínimos y máximos.
        /// </summary>
        /// <param name="diaMin"></param>
        /// <param name="diaMax"></param>
        /// <returns></returns>
        public IList GetBushingBB(double diaMin, double diaMax)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.BushingBatesBore_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 join cH in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals cH.idClasificacion
                                 where c.MedidaNominal >= diaMin && c.MedidaNominal <= diaMax && m.Activo == true
                                 select new
                                 {
                                     c.Codigo,
                                     c.Plano,
                                     c.MedidaNominal,
                                     c.DimB,
                                     m.Descripcion,
                                     m.Activo,
                                     Clasificacion = cH.Descripcion,
                                     cH.UnidadMedida,
                                     cH.Costo,
                                     cH.CantidadUtilizar,
                                     cH.VidaUtil,
                                     cH.idClasificacion,
                                     cH.ListaCotasRevisar,
                                     cH.VerificacionAnual
                                 }).OrderBy(x => x.MedidaNominal).ToList();
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
        /// Método que obtiene lainformación de BushingBates Bore.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoBushing(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.BushingBatesBore_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.Plano,
                                     c.MedidaNominal,
                                     c.DimB,
                                     m.Descripcion,
                                     m.Activo,
                                     c.Id_Bushing
                                 }).OrderBy(x => x.MedidaNominal).ToList();
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
        /// Método que inserta un registro en la tabla Bushing Bates Bore.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="medidaNom"></param>
        /// <param name="dimB"></param>
        /// <returns></returns>
        public int SetBushing(string codigo, string plano, double medidaNom, string dimB)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    BushingBatesBore_ obj = new BushingBatesBore_();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.Plano = plano;
                    obj.MedidaNominal = medidaNom;
                    obj.DimB = dimB;
                  
                    //Guardamos los cambios
                    Conexion.BushingBatesBore_.Add(obj);
                    Conexion.SaveChanges();
                    //Retornamos el id
                    return obj.Id_Bushing;
                }
            }
            catch (Exception)
            {
                //Si hay error, retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de la tabla Bushing Bates Bore.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="medidaNom"></param>
        /// <param name="dimB"></param>
        /// <returns></returns>
        public int UpdateBushing(int id, string codigo, string plano, double medidaNom, string dimB)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    BushingBatesBore_ obj = Conexion.BushingBatesBore_.Where(x => x.Id_Bushing == id).FirstOrDefault();

                    //Asiganmos los valores                    
                    obj.Plano = plano;
                    obj.MedidaNominal = medidaNom;
                    obj.DimB = dimB;

                    //Se guardan los cambios y se retorna el número de registros afectados
                    Conexion.Entry(obj).State = EntityState.Modified;
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
        /// Método que elimina un registro de la tabla Bushing BB.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteBushing(int id)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    BushingBatesBore_ obj = Conexion.BushingBatesBore_.Where(x => x.Id_Bushing == id).FirstOrDefault();

                    //Se guardan los cambios y retorna el número de registros afectados.
                    Conexion.Entry(obj).State = EntityState.Deleted;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error retorna cero
                return 0;
            }
        }
    }
}
