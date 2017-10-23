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
        /// OBtiene la información de herramental Collar BK.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoCollarBK(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.CollarBK
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimB,
                                     c.Plano,
                                     c.Parte,
                                     c.DimA,
                                     c.DimA_Unidad,
                                     c.DimB_Unidad,
                                     m.Descripcion,
                                     m.Activo,
                                     c.ID_COLLAR_BK
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
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    //Declaramos el objeto
                    CollarBK obj = new CollarBK();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.Plano = plano;
                    obj.Parte = parte;
                    obj.DimA = dimA;
                    obj.DimA_Unidad = dimA_unidad;
                    obj.DimB = dimB;
                    obj.DimB_Unidad = dimB_unidad;

                    //Guardamos los cambios
                    Conexion.CollarBK.Add(obj);
                    Conexion.SaveChanges();
                    //Retornamos el id
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
        /// Modifica un registro de la tabla Collar Bk.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="parte"></param>
        /// <param name="dimA"></param>
        /// <param name="dimA_unidad"></param>
        /// <param name="dimB"></param>
        /// <param name="dimB_unidad"></param>
        /// <returns></returns>
        public int UpdateCollar(int id,string codigo, string plano, string parte, double dimA, string dimA_unidad, double dimB, string dimB_unidad)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto
                    CollarBK obj = Conexion.CollarBK.Where(x => x.ID_COLLAR_BK == id).FirstOrDefault();

                    //Asignamos los valores
                    obj.Plano = plano;
                    obj.Parte = parte;
                    obj.DimA = dimA;
                    obj.DimA_Unidad = dimA_unidad;
                    obj.DimB = dimB;
                    obj.DimB_Unidad = dimB_unidad;

                    Conexion.Entry(obj).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //retornamos cero si hubo un error
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene el registro de closing Sleeve con las medidas específicas,
        /// </summary>
        /// <param name="sleeveMin"></param>
        /// <param name="sleeveMax"></param>
        /// <returns></returns>
        public IList GetClosingSleeveBK(double sleeveMin, double sleeveMax)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.ClosingSleeveBK
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.DimB >= sleeveMin && c.DimB <= sleeveMax
                                 select new
                                 {
                                     m.Codigo,
                                     m.Descripcion,
                                     m.Activo,
                                     c.DimB,
                                     c.ID_CLOSINGSLEEVE_BK,
                                     c.Plano,
                                 }).ToList();
                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error, regresa nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros de ClosingSleeve BK
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllClosingSleeveBK(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
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
                    //Retornamos el resultado de la consulta.

                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene lainformación de Closing Sleeve BK.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoClosingSleeve(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.ClosingSleeveBK
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimB,
                                     c.Plano,  
                                     m.Descripcion,
                                     m.Activo,
                                     c.ID_CLOSINGSLEEVE_BK
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
        /// Método que da de alta un registro a la tabla ClosingSleeve
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dimB"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int SetClosingSleeveBK(string codigo, double dimB, string plano)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla
                    ClosingSleeveBK obj = new ClosingSleeveBK();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.DimB = dimB;
                    obj.Plano = plano;

                    //Guardamos los cambios
                    Conexion.ClosingSleeveBK.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.ID_CLOSINGSLEEVE_BK;
                }
            }
            catch (Exception)
            {
                //Si hay error, retorna cero
                return 0;
            }
        }

        /// <summary>
        ///  Método que actualiza un registro en la tabla closing Sleeve BK
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
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    ClosingSleeveBK obj = Conexion.ClosingSleeveBK.Where(x => x.ID_CLOSINGSLEEVE_BK == id).FirstOrDefault();

                    //Asiganmos los valores                 
                    obj.DimB = dimB;
                    obj.Plano = plano;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    Conexion.Entry(obj).State= EntityState.Modified;

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
        /// Método que elimina un registro de la tabla Closing Sleeve BK
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteClosingSleeveBK(int id)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    ClosingSleeveBK obj = Conexion.ClosingSleeveBK.Where(x => x.ID_CLOSINGSLEEVE_BK == id).FirstOrDefault();

                    //eliminamos el registro
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros de acuerdo a la plabra de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllGuidePlate(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from g in Conexion.GuidePlateBK_
                                 join m in Conexion.MaestroHerramentales on g.Codigo equals m.Codigo
                                 where g.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     m.Codigo,
                                     m.Descripcion,m.Activo,g.MedidaNominal,g.Width,g.SobreMedida, g.Id_GuidePlate
                                 }).ToList();
                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene los herramentales óptimos de Guide Plate BK.
        /// </summary>
        /// <param name="_width"></param>
        /// <param name="medidaN"></param>
        /// <param name="SobreM"></param>
        /// <returns></returns>
        public IList GetGuidePlate(string _width,string medidaN,string SobreM)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                  
                    if (_width== "5/64" || _width == "3/32")
                    {
                        //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                        var Lista = (from g in Conexion.GuidePlateBK_
                                     join m in Conexion.MaestroHerramentales on g.Codigo equals m.Codigo
                                     where (g.Width=="5/64" || g.Width=="3/32") && g.MedidaNominal == medidaN && g.SobreMedida == SobreM
                                     select new
                                     {
                                         m.Codigo,
                                         m.Descripcion,
                                         g.Width,
                                         g.MedidaNominal, 
                                         g.SobreMedida

                                     }).ToList();

                        //Retornamos la lista.
                        return Lista;
                    } else if (_width == "5/32")
                    {
                        //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                        var Lista = (from g in Conexion.GuidePlateBK_
                                     join m in Conexion.MaestroHerramentales on g.Codigo equals m.Codigo
                                     where (g.Width == "5/32" || g.Width == "1/8") && g.MedidaNominal == medidaN && g.SobreMedida == SobreM
                                     select new
                                     {
                                         m.Codigo,
                                         m.Descripcion,
                                         g.Width,
                                         g.MedidaNominal,
                                         g.SobreMedida
                                     }).ToList();
                        //Retornamos la lista.
                        return Lista;
                    }
                    else
                    {
                        //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                        var Lista = (from g in Conexion.GuidePlateBK_
                                    join m in Conexion.MaestroHerramentales on g.Codigo equals m.Codigo
                                    where g.Width== _width && g.MedidaNominal == medidaN && g.SobreMedida == SobreM
                                    select new
                                    {
                                        m.Codigo,
                                        m.Descripcion,
                                        g.Width,
                                        g.MedidaNominal,
                                        g.SobreMedida
                                    }).ToList();

                        //Retornamos la lista.
                        return Lista;
                    }                 
                }
            }
            catch (Exception er)
            {

                return null;
            }
        }

        /// <summary>
        /// Método que obtiene lainformación de Guide Plate BK.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoGuidePlate(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.GuidePlateBK_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.MedidaNominal,
                                     c.Width,
                                     c.SobreMedida,
                                     m.Descripcion,
                                     m.Activo,
                                     c.Id_GuidePlate
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
        /// Método que guarda un registro en la tbla.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="medidaN"></param>
        /// <param name="width"></param>
        /// <param name="sobreM"></param>
        /// <returns></returns>
        public int SetGuidePlate(string codigo, string medidaN, string width, string sobreM)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla
                    GuidePlateBK_ guideP = new GuidePlateBK_();

                    //Asignamos los valores
                    guideP.Codigo = codigo;
                    guideP.MedidaNominal = medidaN;
                    guideP.Width = width;
                    guideP.SobreMedida = sobreM;

                    //Guardamos los cambios
                    Conexion.GuidePlateBK_.Add(guideP);
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return guideP.Id_GuidePlate;
                }
            }
            catch (Exception)
            {
                //Si hay error, retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que actualiza un registro en la tabla GuidePlateBK_
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="medidaN"></param>
        /// <param name="width"></param>
        /// <param name="sobreM"></param>
        /// <returns></returns>
        public int UpdateGuidePlate(int id,string codigo, string medidaN, string width, string sobreM)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    GuidePlateBK_ guideP = Conexion.GuidePlateBK_.Where(x => x.Id_GuidePlate == id).FirstOrDefault();

                    //Asiganmos los valores
                    guideP.Codigo = codigo;
                    guideP.MedidaNominal = medidaN;
                    guideP.Width = width;
                    guideP.SobreMedida = sobreM;

                    //Se guardan los cambios y se retorna el número de registros afectados
                    Conexion.Entry(guideP).State = EntityState.Modified;

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
        /// Método que elimina un registro de la tabla GuidePlateBK
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteGuidePlate(int id)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion= new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    GuidePlateBK_ guideP = Conexion.GuidePlateBK_.Where(x => x.Id_GuidePlate == id).FirstOrDefault();

                    //eliminamos el registro
                    Conexion.Entry(guideP).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros de GuillotinaBK.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllGuillotinaBK(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from g in Conexion.GuillotinaBK_
                                 join m in Conexion.MaestroHerramentales on g.Codigo equals m.Codigo
                                 where g.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     m.Codigo,
                                     m.Descripcion,
                                     m.Activo,
                                     g.MedidaNominal,
                                     g.Width,
                                     g.SobreMedida,
                                     g.Id_GuillotinaBK
                                 }).ToList();
                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene lainformación de Guillotina BK.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoGuillotina(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.GuillotinaBK_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.MedidaNominal,
                                     c.Width,
                                     c.SobreMedida,
                                     m.Descripcion,
                                     m.Activo,
                                     c.Id_GuillotinaBK
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
        /// Método que guarda un registro en la tabla Guillotina BK.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="medidaN"></param>
        /// <param name="width"></param>
        /// <param name="sobreM"></param>
        /// <returns></returns>
        public int SetGuillotinaBK(string codigo, string medidaN, string width, string sobreM)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla
                    GuillotinaBK_ obj = new GuillotinaBK_();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.MedidaNominal = medidaN;
                    obj.Width = width;
                    obj.SobreMedida = sobreM;

                    //Guardamos los cambios
                    Conexion.GuillotinaBK_.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.Id_GuillotinaBK;
                }
            }
            catch (Exception)
            {
                //Si hay error, retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que actualiza un registro de la tabla GuillotinaBK_.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="medidaN"></param>
        /// <param name="width"></param>
        /// <param name="sobreM"></param>
        /// <returns></returns>
        public int UpdateGuillotinaBK(int id, string codigo, string medidaN, string width, string sobreM)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    GuillotinaBK_ obj = Conexion.GuillotinaBK_.Where(x => x.Id_GuillotinaBK == id).FirstOrDefault();

                    //Asiganmos los valores
                    obj.MedidaNominal = medidaN;
                    obj.Width = width;
                    obj.SobreMedida = sobreM;

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
        /// Método que elimina un registro de la tabla GuillotinaBK_.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteGuillotinaBK(int id)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    GuillotinaBK_ obj = Conexion.GuillotinaBK_.Where(x => x.Id_GuillotinaBK == id).FirstOrDefault();

                    //eliminamos el registro
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene el herramental de GuillotinaBK.
        /// </summary>
        /// <param name="width"></param>
        /// <param name="medidaNom"></param>
        /// <param name="sobreM"></param>
        /// <returns></returns>
        public IList GetGuillotinaBK(string width,string medidaNom, string sobreM)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from g in Conexion.GuillotinaBK_
                                 join m in Conexion.MaestroHerramentales on g.Codigo equals m.Codigo
                                 where g.Width== width && g.MedidaNominal== medidaNom && g.SobreMedida == sobreM
                                 select new
                                 {
                                     m.Codigo,
                                     m.Descripcion,
                                     g.Id_GuillotinaBK,
                                     g.SobreMedida,
                                     g.MedidaNominal,
                                     g.Width,
                                     m.Activo
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene la medida nominal y sobremedida para GuillotinaBK o Guide PlateBK.
        /// </summary>
        /// <param name="d1"></param>
        /// <returns></returns>
        public IList GetMedidaGuillotina(double d1)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var lista = (from c in Conexion.CriDiaGuillBK
                                 where d1 >= c.RangoMin && d1 <= c.RangoMax
                                 select new
                                 {
                                     MEDIDANOMINAL=c.Dia,
                                     SOBREMEDIDA=c.SobreMedida
                                 }).ToList();

                    //Retornamos la lista.
                    return lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el width para guillotina BK o GuidePlateBK.
        /// </summary>
        /// <param name="h1"></param>
        /// <returns></returns>
        public string GetWidthGuillotina(double h1)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion= new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var width = (from g in Conexion.CriGillBK
                                 from p in Conexion.CriGPBK
                                 where h1 >= g.RangoMin && h1 <= g.RangoMax && g.D==p.Width
                                 select p.Width ).FirstOrDefault();
                    //Retornamos la lista.
                    return width;
                }
            }
            catch (Exception)
            {
                // Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

    }
}
