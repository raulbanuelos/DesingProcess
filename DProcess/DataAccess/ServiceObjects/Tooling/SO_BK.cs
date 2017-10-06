using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_BK
    {
        /// <summary>
        /// Método que obtiene el collarin a partir de los valores mínimos y máximos.
        /// </summary>
        /// <param name="maxA"></param>
        /// <param name="minB"></param>
        /// <returns></returns>
        public IList GetCollar(double maxA, double minB)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var Lista = (from a in Conexion.MaestroHerramentales
                                 join b in Conexion.CollarBK on a.Codigo equals b.Codigo
                                 where b.DimA <= maxA && b.DimB >= minB
                                 select new
                                 {
                                     CODIGO = a.Codigo,
                                     DESCRIPCION = a.Descripcion,
                                     DIM_A = b.DimA,
                                     DIM_B = b.DimB,
                                     DIM_B_UNIDAD = b.DimB_Unidad,
                                     DIM_A_UNIDAD = b.DimA_Unidad,
                                     PARTE = b.Parte,
                                     PAREDCOLLARIN = b.DimA - b.DimB
                                 }
                                 ).OrderByDescending(o => o.PAREDCOLLARIN).ToList();

                    //Retornamos el resultado de la consulta.
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
        /// Método que obtiene el collarin con las medidas específicas, y que sea diferente en su campo parte.
        /// </summary>
        /// <param name="maxA"></param>
        /// <param name="minB"></param>
        /// <param name="parte"></param>
        /// <returns></returns>
        public IList GetCollar(double maxA, double minB, string parte)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var Lista = (from a in Conexion.MaestroHerramentales
                                 join b in Conexion.CollarBK on a.Codigo equals b.Codigo
                                 where b.DimA == maxA && b.DimB == minB && b.Parte != parte
                                 select new
                                 {
                                     CODIGO = a.Codigo,
                                     DESCRIPCION = a.Descripcion,
                                     PARTE = b.Parte,
                                     DIM_A = b.DimA,
                                     DIM_A_UNIDAD = b.DimA_Unidad,
                                     DIM_B = b.DimB,
                                     DIM_B_UNIDAD = b.DimB_Unidad
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
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
        /// Método que retorna todos los registros de collarines de Auto Finish Turn.
        /// </summary>
        /// <returns></returns>
        public IList GetAllCollar(string busqueda)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var Lista = (from a in Conexion.MaestroHerramentales
                                 join b in Conexion.CollarBK on a.Codigo equals b.Codigo
                                 where b.Codigo.Contains(busqueda) || a.Descripcion.Contains(busqueda)
                                 select new
                                 {
                                     CODIGO = a.Codigo,
                                     DESCRIPCION = a.Descripcion,
                                     PARTE = b.Parte,
                                     DIM_A = b.DimA,
                                     DIM_A_UNIDAD = b.DimA_Unidad,
                                     DIM_B = b.DimB,
                                     DIM_B_UNIDAD = b.DimB_Unidad
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que inserta un registro a la tabla
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="parte"></param>
        /// <param name="dimA"></param>
        /// <param name="dimA_unidad"></param>
        /// <param name="dimB"></param>
        /// <param name="dimB_unidad"></param>
        /// <returns></returns>
        public int SetCollar(string codigo, string plano, string parte,double dimA, string dimA_unidad, double dimB, string dimB_unidad)
        {
            try
            {
                using (var Conexion= new EntitiesTooling())
                {
                    CollarBK obj = new CollarBK();

                    obj.Codigo = codigo;
                    obj.Plano = plano;
                    obj.Parte = parte;
                    obj.DimA = dimA;
                    obj.DimA_Unidad = dimA_unidad;
                    obj.DimB = dimB;
                    obj.DimB_Unidad = dimB_unidad;

                    Conexion.CollarBK.Add(obj);
                    Conexion.SaveChanges();

                    return obj.ID_COLLAR_BK;
                }
            }
            catch (Exception)
            {
                //retornamos cero si hubo un error
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllClosingSleeveBK(string texto)
        {
            try
            {

                using (var Conexion = new EntitiesTooling())
                {
                    var Lista = (from c in Conexion.ClosingSleeveBK
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimB,
                                     c.Plano,
                                     c.ID_CLOSINGSLEEVE_BK,
                                     m.Descripcion,
                                     m.Activo
                                 }).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dimB"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int SetClosingSleeveBK(string codigo, double dimB, string plano)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    ClosingSleeveBK obj = new ClosingSleeveBK();

                    obj.Codigo = codigo;
                    obj.DimB = dimB;
                    obj.Plano = plano;

                    Conexion.ClosingSleeveBK.Add(obj);
                    Conexion.SaveChanges();

                    return obj.ID_CLOSINGSLEEVE_BK;
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="dimB"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int UpdateClosingSleeveBK(int id, string codigo, double dimB,string plano)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    ClosingSleeveBK obj = Conexion.ClosingSleeveBK.Where(x => x.ID_CLOSINGSLEEVE_BK == id).FirstOrDefault();

                    obj.Codigo = codigo;
                    obj.DimB = dimB;
                    obj.Plano = plano;

                    Conexion.Entry(obj).State= EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteClosingSleeveBK(int id)
        {
            try
            {
                using (var Conexion = new EntitiesTooling())
                {
                    ClosingSleeveBK obj = Conexion.ClosingSleeveBK.Where(x => x.ID_CLOSINGSLEEVE_BK == id).FirstOrDefault();


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
