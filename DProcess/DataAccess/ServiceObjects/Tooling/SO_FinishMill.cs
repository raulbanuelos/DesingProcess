using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_FinishMill
    {
        /// <summary>
        /// Método que obtiene todos lso registros de Bushing Finish Mill.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllBushingFM(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.BushingFinishMill
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 where b.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     b.Codigo,
                                     b.Plano,
                                     b.DimC,
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
        /// Método que obtiene los herramentales óptimos a partir de los valores mínimos y máximos.
        /// </summary>
        /// <param name="diaMin"></param>
        /// <param name="diaMax"></param>
        /// <returns></returns>
        public IList GetBushingFM(double diaMin, double diaMax)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.BushingFinishMill
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where b.DimC >= diaMin && b.DimC <= diaMax &&  m.Activo == true
                                 select new
                                 {
                                     b.Codigo,
                                     b.Plano,
                                     b.DimC,
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
                                 }).OrderBy(x => x.DimC).ToList();
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
        /// Método que obtiene lainformación de Bushing Finish Mill.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoBushingFM(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.BushingFinishMill
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.Plano,
                                     c.DimC,
                                     m.Descripcion,
                                     m.Activo,
                                     c.Id_BushingFM
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
        /// Método que inserta un registro a la tabla BusgingFinish Mill.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="medidaNom"></param>
        /// <param name="dimB"></param>
        /// <returns></returns>
        public int SetBushingFM(string codigo, string plano, double dimC)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    BushingFinishMill obj = new BushingFinishMill();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.Plano = plano;
                    obj.DimC = dimC;

                    //Guardamos los cambios
                    Conexion.BushingFinishMill.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.Id_BushingFM;
                }
            }
            catch (Exception)
            {
                //Si hay error, retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro en la tabla Bushing Finish Mill.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="dimC"></param>
        /// <returns></returns>
        public int UpdateBushingFM(int id, string codigo, string plano, double dimC)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    BushingFinishMill obj = Conexion.BushingFinishMill.Where(x => x.Id_BushingFM == id).FirstOrDefault();

                    //Asiganmos los valores                    
                    obj.Plano = plano;
                    obj.DimC = dimC;

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
        /// Método que elimina un registro de la tabla Bushing Finish Mill.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteBushingFM(int id)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    BushingFinishMill obj = Conexion.BushingFinishMill.Where(x => x.Id_BushingFM == id).FirstOrDefault();

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
