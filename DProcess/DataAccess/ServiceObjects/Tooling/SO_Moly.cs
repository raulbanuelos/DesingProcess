﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_Moly
    {
        /// <summary>
        ///  Método que obtiene todos los registros de Camisa Moly.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllCamisaMoly(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.CamisaMoly_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where m.Descripcion.Contains(texto) || c.Codigo.Contains(texto)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimA,
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
        /// Método que obtiene la información de un herramental Camisa Moly.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoCamisaMoly(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.CamisaMoly_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimA,
                                     c.Plano,
                                     m.Descripcion,
                                     m.Activo,
                                    c.Id_CamisaMoly
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
        /// Método que obtiene los herramentales óptimos para Camisa Moly a partir de diametro de operacion anterior.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IList GetCamisaMoly(double min, double max)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.CamisaMoly_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where b.DimA >= min && b.DimA <= max && m.Activo == true
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimA,
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
        /// Método que guarda un registro de Camisa Moly.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public int SetCamisaMoly(string codigo,string plano, double dimA)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CamisaMoly_ obj = new CamisaMoly_();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.DimA = dimA;
                    obj.Plano = plano;

                    //Guardamos los cambios
                    Conexion.CamisaMoly_.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.Id_CamisaMoly;
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        ///  Método que modifica un registro de Camisa Moly.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="plano"></param>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public int UpdateCamisaMoly(int id, string plano, double dimA)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CamisaMoly_ obj = Conexion.CamisaMoly_.Where(x => x.Id_CamisaMoly == id).FirstOrDefault();

                    //Asignamos los valores
                    obj.DimA = dimA;
                    obj.Plano = plano;

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
        /// étodo que elimna un registro de Camisa Moly.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCamisaMoly(int id)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CamisaMoly_ obj = Conexion.CamisaMoly_.Where(x => x.Id_CamisaMoly == id).FirstOrDefault();

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

        /// <summary>
        /// Método que obtiene todos los registros de Collar Moly.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllCollarMoly(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.CollarMoly_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where m.Descripcion.Contains(texto) || c.Codigo.Contains(texto)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimA,
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
        ///  Método que obtiene la información de un herramental Collar Moly.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoCollarMoly(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.CollarMoly_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimA,
                                     c.Plano,
                                     m.Descripcion,
                                     m.Activo,
                                     c.Id_CollarMoly
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
        /// Método que obtiene los herramentales óptimos para Collar Moly a partir de la medida de camisa.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IList GetCollarMoly(double min, double max)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.CollarMoly_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where b.DimA >= min && b.DimA <= max && m.Activo == true
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimA,
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
        /// Método que guarda un registro de Collar Moly.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public int SetCollarMoly(string codigo, string plano, double dimA)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CollarMoly_ obj = new CollarMoly_();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.DimA = dimA;
                    obj.Plano = plano;

                    //Guardamos los cambios
                    Conexion.CollarMoly_.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.Id_CollarMoly;
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de Collar Moly.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="plano"></param>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public int UpdateCollarMoly(int id, string plano, double dimA)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CollarMoly_ obj = Conexion.CollarMoly_.Where(x => x.Id_CollarMoly == id).FirstOrDefault();

                    //Asignamos los valores
                    obj.DimA = dimA;
                    obj.Plano = plano;

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
        /// Método que elimina un registro de Collar Moly.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCollarMoly(int id)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CollarMoly_ obj = Conexion.CollarMoly_.Where(x => x.Id_CollarMoly == id).FirstOrDefault();

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

        /// <summary>
        /// Método que obtiene todos los registros de Protector Superior Moly.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllProtectorSuperior(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.ProtectorSupMoly_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where m.Descripcion.Contains(texto) || c.Codigo.Contains(texto)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimA,
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
        ///   Método que obtiene los herramentales óptimos para Protector Superior Moly a partir de medida de collar.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IList GetProtectorSuperior(double min, double max)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.ProtectorSupMoly_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where b.DimA >= min && b.DimA <= max && m.Activo == true
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimA,
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
        /// Método que obtiene la información de un herramental Protector Superior Moly.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoProtectorSuperior(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.ProtectorSupMoly_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimA,
                                     c.Plano,
                                     m.Descripcion,
                                     m.Activo,
                                     c.Id_PSM
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
        ///  Método que guarda un registro de Protector Superior Moly.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public int SetProtectoSuperior(string codigo, string plano, double dimA)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    ProtectorSupMoly_ obj = new ProtectorSupMoly_();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.DimA = dimA;
                    obj.Plano = plano;

                    //Guardamos los cambios
                    Conexion.ProtectorSupMoly_.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.Id_PSM;
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de Protector Superior Moly.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="plano"></param>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public int UpdateProtectorSuperior(int id, string plano, double dimA)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    ProtectorSupMoly_ obj = Conexion.ProtectorSupMoly_.Where(x => x.Id_PSM == id).FirstOrDefault();

                    //Asignamos los valores
                    obj.DimA = dimA;
                    obj.Plano = plano;

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
        /// Método que elimina un registro de Protector Superior Moly.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteProtectorSuperior(int id)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    ProtectorSupMoly_ obj = Conexion.ProtectorSupMoly_.Where(x => x.Id_PSM == id).FirstOrDefault();

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

        /// <summary>
        /// Método que obtiene todos los registros de Protector Inferior Moly.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllProtectorInferior(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.ProtectorInfMoly_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where m.Descripcion.Contains(texto) || c.Codigo.Contains(texto)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimA,
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
        /// Método que obtiene los herramentales óptimos para Protector Inferior Moly a partir de medida de collar.
        /// </summary>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public IList GetProtectorInferior(double min, double max)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from b in Conexion.ProtectorInfMoly_
                                 join m in Conexion.MaestroHerramentales on b.Codigo equals m.Codigo
                                 join c in Conexion.ClasificacionHerramental on m.idClasificacionHerramental equals c.idClasificacion
                                 where b.DimA >= min && b.DimA <= max && m.Activo == true
                                 select new
                                 {
                                     b.Codigo,
                                     b.DimA,
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
        /// Método que obtiene la información de un herramental Protector Inf Moly.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetInfoProtectorInferior(string codigo)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.ProtectorInfMoly_
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Equals(codigo)
                                 select new
                                 {
                                     c.Codigo,
                                     c.DimA,
                                     c.Plano,
                                     m.Descripcion,
                                     m.Activo,
                                     c.Id_PIM
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
        /// Método que guarda un registro de Protector Inferior Moly.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public int SetProtectoInferior(string codigo, string plano, double dimA)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    ProtectorInfMoly_ obj = new ProtectorInfMoly_();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.DimA = dimA;
                    obj.Plano = plano;

                    //Guardamos los cambios
                    Conexion.ProtectorInfMoly_.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.Id_PIM;
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de Protector Inferior Moly.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="plano"></param>
        /// <param name="dimA"></param>
        /// <returns></returns>
        public int UpdateProtectorInferior(int id, string plano, double dimA)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    ProtectorInfMoly_ obj = Conexion.ProtectorInfMoly_.Where(x => x.Id_PIM == id).FirstOrDefault();

                    //Asignamos los valores
                    obj.DimA = dimA;
                    obj.Plano = plano;

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
        /// Método que elimina un registro de Protector Inferior Moly.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteProtectorInferior(int id)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    ProtectorInfMoly_ obj = Conexion.ProtectorInfMoly_.Where(x => x.Id_PIM == id).FirstOrDefault();

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
