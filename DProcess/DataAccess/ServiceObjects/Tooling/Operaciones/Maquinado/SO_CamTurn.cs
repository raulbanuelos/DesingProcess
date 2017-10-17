using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling.Operaciones.Maquinado
{
    public class SO_CamTurn
    {
        private string SP_RGP_GET_TIMECAMTURN = "SP_RGP_GET_TIMECAMTURN";

        public DataSet GetTimeCamTurn(string v, string especMaterial)
        {
            try
            {
                DataSet datos = null;

                Desing_SQL conexion = new Desing_SQL();

                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("especMaterial",especMaterial);
                parametros.Add("CamTurn",v);

                datos = conexion.EjecutarStoredProcedure(SP_RGP_GET_TIMECAMTURN, parametros);

                return datos;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        ///  Método que obtiene todos los registros de acuerdo a la plabra de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllCollarSpacer(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from c in Conexion.CollarSpacer
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where c.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     c.Codigo,
                                     c.Plano,
                                     c.MedidaNominal,
                                     c.DimE,
                                     c.DimF,
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
        /// <param name="dimE_min"></param>
        /// <param name="dimE_max"></param>
        /// <param name="dimF_min"></param>
        /// <param name="dimF_max"></param>
        /// <returns></returns>
        public IList GetCollarSpacer(double dimE_min,double dimE_max, double dimF_min,double dimF_max)
        {
            try
            {
                using (var Conexion= new EntitiesTooling())
                {
                    var lista = (from c in Conexion.CollarSpacer
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where (c.DimE >= dimE_min && c.DimE <= dimE_max) && (c.DimF >= dimF_min && c.DimF <= dimF_max)
                                 select new
                                 {
                                     c.Codigo,
                                     m.Descripcion,
                                     c.DimE,
                                     c.DimF,
                                     c.MedidaNominal,
                                     DESCRIPCIONCT= c.Descripcion
                                 }).ToList();
                    return lista;
                }
            }
            catch (Exception er)
            {

                return null;
            }
        }

        /// <summary>
        /// Método que guarda un registro en la tbla.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="medidaNom"></param>
        /// <param name="dimE"></param>
        /// <param name="dimF"></param>
        /// <returns></returns>
        public int SetCollarSpacer(string codigo, string plano, string medidaNom,string descripcion, double dimE, double dimF)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CollarSpacer obj = new CollarSpacer();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.Plano = plano;
                    obj.MedidaNominal = medidaNom;
                    obj.Descripcion = descripcion;
                    obj.DimE = dimE;
                    obj.DimF = dimF;
                    obj.ident = 0;

                    //Guardamos los cambios
                    Conexion.CollarSpacer.Add(obj);
                    Conexion.SaveChanges();
                    //Retornamos el id
                    return obj.Id_CollarSpacer;
                }
            }
            catch (Exception)
            {
                //Si hay error, retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que actualiza un registro en la tabla Collar Spacer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="plano"></param>
        /// <param name="medidaNom"></param>
        /// <param name="dimE"></param>
        /// <param name="dimF"></param>
        /// <returns></returns>
        public int UpdateCollarSpcaer(int id, string codigo, string plano, string medidaNom, double dimE, double dimF)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    CollarSpacer obj = Conexion.CollarSpacer.Where(x => x.Id_CollarSpacer == id).FirstOrDefault();

                    //Asiganmos los valores
                    obj.Codigo = codigo;
                    obj.Plano = plano;
                    obj.MedidaNominal = medidaNom;
                    obj.DimE = dimE;
                    obj.DimF = dimF;
                    obj.ident = 0;

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
        /// Método que elimina un registro de la tabla CollarSpacer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCollarSpacer(int id)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    CollarSpacer obj = Conexion.CollarSpacer.Where(x => x.Id_CollarSpacer == id).FirstOrDefault();

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

        /// <summary>
        ///  Método que obtiene todos los registros de acuerdo a la plabra de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllWorkCam(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var lista = (from w in Conexion.WorkCam
                                 join m in Conexion.MaestroHerramentales on w.Codigo equals m.Codigo
                                 where w.Codigo.Contains(texto) || m.Descripcion.Contains(texto)
                                 select new
                                 {
                                     w.Id_WorkCam,
                                     w.MedidaNominal,
                                     m.Codigo,
                                     m.Descripcion,
                                     m.Activo
                                 }).ToList();
                    //Retornamos el resultado de la consulta.
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
        /// Método que guarda un registro en la tbla.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="descripcion"></param>
        /// <param name="medidaN"></param>
        /// <returns></returns>
        public int SetWorkCam(string codigo, string descripcion, string medidaN)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    WorkCam obj = new WorkCam();
                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.Descripcion = descripcion;
                    obj.MedidaNominal = medidaN;

                    //Guardamos los cambios
                    Conexion.WorkCam.Add(obj);
                    Conexion.SaveChanges();
                    //Retornamos el id
                    return obj.Id_WorkCam;
                }
            }
            catch (Exception)
            {
                //Si hay error, retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que actualiza un registro en la tabla WorkCam
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="descripcion"></param>
        /// <param name="medidaN"></param>
        /// <returns></returns>
        public int UpdateWorkCam(int id, string codigo, string descripcion, string medidaN)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    WorkCam obj = Conexion.WorkCam.Where(x => x.Id_WorkCam == id).FirstOrDefault();

                    //Asiganmos los valores
                    obj.Codigo = codigo;
                    obj.Descripcion = descripcion;
                    obj.MedidaNominal = medidaN;

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
        /// Método que elimina un registro de la tabla WorkCam.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteWorkCam(int id)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    WorkCam obj = Conexion.WorkCam.Where(x => x.Id_WorkCam == id).FirstOrDefault();

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

        /// <summary>
        /// Método que obtiene todos los registros de acuerdo a la plabra de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetAllCutterCamTurn(string texto)
        {
            try
            {
                //Realizamos la conexíon a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var lista = (from c in Conexion.CutterCamTurn
                                 join m in Conexion.MaestroHerramentales on c.Codigo equals m.Codigo
                                 where m.Descripcion.Contains(texto) || m.Codigo.Contains(texto)
                                 select new
                                 {
                                     m.Codigo,
                                     m.Descripcion,
                                     c.Dimencion,
                                     c.Plano
                                 }).ToList();
                    //Retornamos el resultado de la consulta.
                    return lista;
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
        /// <param name="descripcion"></param>
        /// <param name="dimension"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int SetCutterCamTurn(string codigo, string descripcion, double dimension, string plano)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesTooling())
                {
                    //Declaramos el objeto de la tabla.
                    CutterCamTurn obj = new CutterCamTurn();

                    //Asignamos los valores
                    obj.Codigo = codigo;
                    obj.Descripcion = descripcion;
                    obj.Dimencion = dimension;
                    obj.Plano = plano;

                    //Guardamos los cambios
                    Conexion.CutterCamTurn.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id
                    return obj.Id_CutterCamTurn;
                }
            }
            catch (Exception)
            {
                // Si hay error, retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que actualiza un registro en la tabla Collar Spacer
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="descripcion"></param>
        /// <param name="dimension"></param>
        /// <param name="plano"></param>
        /// <returns></returns>
        public int UpdateCutterCamTurn(int id, string codigo, string descripcion, double dimension, string plano)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    CutterCamTurn obj = Conexion.CutterCamTurn.Where(x => x.Id_CutterCamTurn == id).FirstOrDefault();

                    //Asiganmos los valores
                    obj.Codigo = codigo;
                    obj.Descripcion = descripcion;
                    obj.Dimencion = dimension;
                    obj.Plano = plano;

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
        ///  Método que elimina un registro de la tabla CamTurn.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCutterCamTurn(int id)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    CutterCamTurn obj = Conexion.CutterCamTurn.Where(x => x.Id_CutterCamTurn == id).FirstOrDefault();

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
