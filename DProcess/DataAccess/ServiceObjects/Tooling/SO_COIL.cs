using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Tooling
{
    public class SO_COIL
    {
        /// <summary>
        /// Método que inserta un registro en la tabla TBL_COIL_FEED_ROLLER
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int SetCOIL_FEED_ROLLER(string codigo, string code,double dimA,double dimB, double dimC, double DimD,double W_Min,double W_Max)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var conexion = new EntitiesTooling())
                {
                    //Se  crea un objeto, el cual se va agregar a la tabla 
                    TBL_COIL_FEED_ROLLER obj = new TBL_COIL_FEED_ROLLER();

                    //Se asiganan los valores.
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.DIMC = DimD;
                    obj.WIRE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;

                    //Agrega el objeto a la tabla.
                    conexion.TBL_COIL_FEED_ROLLER.Add(obj);
                    conexion.SaveChanges();
                    //Retorna el id del registro insertado
                    return obj.ID_COIL_FEED_ROLLER;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa cero
                return 0;
            }
        }
        /// <summary>
        /// Método que modifica un registro de la tabla TBL_COIL_FEED_ROLLER
        /// </summary>
        /// <param name="id_coil"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int UpdateCOIL_FEED_ROLLER(int id_coil,string codigo, string code, double dimA, double dimB, double dimC,double dimD ,double W_Min, double W_Max)
        {
            try
            {   //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    TBL_COIL_FEED_ROLLER obj = Conexion.TBL_COIL_FEED_ROLLER.Where(x => x.ID_COIL_FEED_ROLLER == id_coil).FirstOrDefault();
                    //Asiganmos los valores
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.DIMD = dimD;
                    obj.WIRE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;
                    //Se guardan los cambios y se retorna el número de registros afectados.
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
        /// Método que elimina un registro de la tabla TBL_COIL_FEED_ROLLER
        /// </summary>
        /// <param name="id_coil"></param>
        /// <returns></returns>
        public int DeleteCOIL_FEED_ROLLER(int id_coil)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion= new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TBL_COIL_FEED_ROLLER obj = Conexion.TBL_COIL_FEED_ROLLER.Where(x => x.ID_COIL_FEED_ROLLER == id_coil).FirstOrDefault();
                    //Se estable el estado del registro a eliminado.
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
        /// Método que obtiene un registro que cumpla con el rango del width
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public IList GetCOIL_FEED_ROLLER(double width)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y lo guardamos en una variable
                    var Lista = (from a in Conexion.TBL_COIL_FEED_ROLLER
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.WIRE_WIDTH_MIN > width && a.WIRE_WIDTH_MAX <= width
                                 select new {
                                     CODIGO = b.Codigo,
                                     DESCRIPCION = b.Descripcion,
                                     a.DIMA,a.DIMB,a.DIMC,a.DIMD,a.DETALLE
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros, se filtran por el código o descripción
        /// </summary>
        /// <param name="textoBusq"></param>
        /// <returns></returns>
        public IList GetAllCOIL_FEED_ROLLER(string textoBusq)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion= new EntitiesTooling())
                {
                    //Ejecutamos la consulta y guardamos el resultado en una variable
                    var Lista = (from c in Conexion.TBL_COIL_FEED_ROLLER
                                 join m in Conexion.MaestroHerramentales on c.CODIGO equals m.Codigo
                                 where c.CODIGO.Contains(textoBusq) || m.Descripcion.Contains(textoBusq)
                                 select new
                                 {
                                     CODIGO = m.Codigo,
                                     DESCRIPCION = m.Descripcion,
                                     c.DIMA,
                                     c.DIMB,
                                     c.DIMC,
                                     c.DIMD,
                                     c.DETALLE,
                                     c.WIRE_WIDTH_MAX,
                                     c.WIRE_WIDTH_MIN
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método que inserta un registro a la tabla TBL_COIL_CENTER_GUIDE
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <param name="R_Mn"></param>
        /// <param name="R_Max"></param>
        /// <returns></returns>
        public int SetCOIL_CENTER_GUIDE(string codigo, string code,double dimA,double dimB, double dimC, double W_Min,double W_Max,double R_Mn,double R_Max)
        {
            try
            { //Establecemos la conexión a través de EntityFramework.
                using (var conexion = new EntitiesTooling())
                {   
                    //Se  crea un objeto, el cual se va agregar a la tabla 
                    TBL_COIL_CENTER_GUIDE obj = new TBL_COIL_CENTER_GUIDE();
                    //Se asiganan los valores.
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIRE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    obj.RADIAL_WIRE_MIN = R_Mn;
                    obj.RADIAL_WIRE_MAX = R_Max;

                    //Agrega el objeto a la tabla.
                    conexion.TBL_COIL_CENTER_GUIDE.Add(obj);
                    conexion.SaveChanges();
                    //Retorna el id del registro insertado
                    return obj.ID_COIL_CENTER_GUIDE;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa cero
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de la tabla TBL_COIL_CENTER_GUIDE
        /// </summary>
        /// <param name="id_coil"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <param name="R_Mn"></param>
        /// <param name="R_Max"></param>
        /// <returns></returns>
        public int UpdateCOIL_CENTER_GUIDE(int id_coil,string codigo, string code, double dimA, double dimB, double dimC, double W_Min, double W_Max, double R_Mn, double R_Max)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    TBL_COIL_CENTER_GUIDE obj = conexion.TBL_COIL_CENTER_GUIDE.Where(x => x.ID_COIL_CENTER_GUIDE == id_coil).FirstOrDefault();
                    //Asiganmos los valores
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIRE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    obj.RADIAL_WIRE_MIN = R_Mn;
                    obj.RADIAL_WIRE_MAX = R_Max;
                    //Se cambia el estado de registro a modificado.
                    conexion.Entry(obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si encuentra error devuelve cero.
                return 0;
            }
        }
        /// <summary>
        /// Método que elimina un registro de la tabla TBL_COIL_CENTER_GUIDE
        /// </summary>
        /// <param name="id_coil"></param>
        /// <returns></returns>
        public int DeleteCOIL_CENTER_GUIDE(int id_coil)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TBL_COIL_CENTER_GUIDE obj = Conexion.TBL_COIL_CENTER_GUIDE.Where(x => x.ID_COIL_CENTER_GUIDE == id_coil).FirstOrDefault();
                    //Se estable el estado del registro a eliminado.
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
        /// Método que obtiene un registro que cumpla con el rango del width y el rango de radial
        /// </summary>
        /// <param name="width"></param>
        /// <param name="radial"></param>
        /// <returns></returns>
        public IList GetCOIL_CENTER_GUIDE(double width, double radial)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y lo guardamos en una variable
                    var Lista = (from a in Conexion.TBL_COIL_CENTER_GUIDE
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where ( a.WIRE_WIDTH_MIN > width && a.WIRE_WIDTH_MAX <= width) && (a.RADIAL_WIRE_MIN > radial && a.RADIAL_WIRE_MAX <= radial)
                                 select new
                                 {
                                     CODIGO = b.Codigo,
                                     DESCRIPCION = b.Descripcion,
                                     a.DIMA,
                                     a.DIMB,
                                     a.DIMC,
                                     a.DETALLE
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }
        /// <summary>
        /// Método que obtiene todos los registros, se filtran por el código o descripción
        /// </summary>
        /// <param name="textoBusq"></param>
        /// <returns></returns>
        public IList GetAllCOIL_CENTER_GUIDE(string textoBusq)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y guardamos el resultado en una variable
                    var Lista = (from c in Conexion.TBL_COIL_CENTER_GUIDE
                                 join m in Conexion.MaestroHerramentales on c.CODIGO equals m.Codigo
                                 where c.CODIGO.Contains(textoBusq) || m.Descripcion.Contains(textoBusq)
                                 select new
                                 {
                                     CODIGO = m.Codigo,
                                     DESCRIPCION = m.Descripcion,
                                     c.DIMA,
                                     c.DIMB,
                                     c.DIMC,
                                     c.DETALLE,
                                     c.WIRE_WIDTH_MAX,
                                     c.WIRE_WIDTH_MIN,
                                     c.RADIAL_WIRE_MAX,
                                     c.RADIAL_WIRE_MIN
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método que inserta un registro a la tabla TBL_EXIT_GUIDE
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <param name="R_Mn"></param>
        /// <param name="R_Max"></param>
        /// <returns></returns>
        public int SetExit_GUIDE(string codigo, string code, double dimA, double dimB, double dimC, double W_Min, double W_Max, double R_Mn, double R_Max)
        {
            try { 
                //Establecemos la conexión a través de EntityFramework.
                using (var conexion = new EntitiesTooling())
                {
                    //Se  crea un objeto, el cual se va agregar a la tabla 
                    TBL_EXIT_GUIDE obj = new TBL_EXIT_GUIDE();
                    //Se asiganan los valores.
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    obj.RADIAL_WIRE_MIN = R_Mn;
                    obj.RADIAL_WIRE_MAX = R_Max;

                    //Agrega el objeto a la tabla.
                    conexion.TBL_EXIT_GUIDE.Add(obj);
                    conexion.SaveChanges();
                    //Retorna el id del registro insertado
                    return obj.ID_EXIT_GUIDE;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa cero
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de la tabla TBL_EXIT_GUIDE
        /// </summary>
        /// <param name="id_exit"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <param name="R_Mn"></param>
        /// <param name="R_Max"></param>
        /// <returns></returns>
        public int UpdateExit_GUIDE(int id_exit, string codigo, string code, double dimA, double dimB, double dimC, double W_Min, double W_Max, double R_Mn, double R_Max)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    TBL_EXIT_GUIDE obj = conexion.TBL_EXIT_GUIDE.Where(x => x.ID_EXIT_GUIDE == id_exit).FirstOrDefault();
                    //Asiganmos los valores
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    obj.RADIAL_WIRE_MIN = R_Mn;
                    obj.RADIAL_WIRE_MAX = R_Max;

                    //Se cambia el estado de registro a modificado.
                    conexion.Entry(obj).State = EntityState.Modified;
                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si encuentra error devuelve cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro de la tabla TBL_EXIT_GUIDE
        /// </summary>
        /// <param name="id_exit"></param>
        /// <returns></returns>
        public int DeleteExit_GUIDE(int id_exit)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TBL_EXIT_GUIDE obj = Conexion.TBL_EXIT_GUIDE.Where(x => x.ID_EXIT_GUIDE == id_exit).FirstOrDefault();

                    //Se estable el estado del registro a eliminado.
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
        /// Método que obtiene un registro que cumpla con el rango del width y el rango de radial
        /// </summary>
        /// <param name="width"></param>
        /// <param name="radial"></param>
        /// <returns></returns>
        public IList GetEXIT_GUIDE(double width, double radial)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y lo guardamos en una variable
                    var Lista = (from a in Conexion.TBL_EXIT_GUIDE
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where (a.WIRE_WIDTH_MIN > width && a.WIDE_WIDTH_MAX <= width) && (a.RADIAL_WIRE_MIN > radial && a.RADIAL_WIRE_MAX <= radial)
                                 select new
                                 {
                                     CODIGO = b.Codigo,
                                     DESCRIPCION = b.Descripcion,
                                     a.DIMA,
                                     a.DIMB,
                                     a.DIMC,
                                     a.DETALLE
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }

        /// <summary>
        ///  Método que obtiene todos los registros, se filtran por el código o descripción
        /// </summary>
        /// <param name="textoBusq"></param>
        /// <returns></returns>
        public IList GetAllEXIT_GUIDE(string textoBusq)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y guardamos el resultado en una variable
                    var Lista = (from c in Conexion.TBL_EXIT_GUIDE
                                 join m in Conexion.MaestroHerramentales on c.CODIGO equals m.Codigo
                                 where c.CODIGO.Contains(textoBusq) || m.Descripcion.Contains(textoBusq)
                                 select new
                                 {
                                     CODIGO = m.Codigo,
                                     DESCRIPCION = m.Descripcion,
                                     c.DIMA,
                                     c.DIMB,
                                     c.DIMC,
                                     c.DETALLE,
                                     c.WIRE_WIDTH_MIN,
                                     c.WIDE_WIDTH_MAX,
                                     c.RADIAL_WIRE_MIN,
                                     c.RADIAL_WIRE_MAX
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método que inserta un registro a la tabla TBL_EXTERNAL_GUIDE_ROLLER_1PIECE
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimB"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int SetExternal_GR_1P(string codigo, string code, double dimB, double W_Min, double W_Max)
        {
            try
            {   //Establecemos la conexión a través de EntityFramework.
                using (var conexion = new EntitiesTooling())
                {
                    //Se  crea un objeto, el cual se va agregar a la tabla 
                    TBL_EXTERNAL_GUIDE_ROLLER_1PIECE obj = new TBL_EXTERNAL_GUIDE_ROLLER_1PIECE();
                    //Se asiganan los valores.
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMB = dimB;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;

                    //Agrega el objeto a la tabla.
                    conexion.TBL_EXTERNAL_GUIDE_ROLLER_1PIECE.Add(obj);
                    conexion.SaveChanges();
                    //Retorna el id del registro insertado
                    return obj.ID_EGR_1P;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa cero
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_1PIECE
        /// </summary>
        /// <param name="id_external"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimB"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int UpdateExternal_GR_1P(int id_external,string codigo, string code, double dimB, double W_Min, double W_Max)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    TBL_EXTERNAL_GUIDE_ROLLER_1PIECE obj = conexion.TBL_EXTERNAL_GUIDE_ROLLER_1PIECE.Where(x => x.ID_EGR_1P == id_external).FirstOrDefault();
                    //Asiganmos los valores
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMB = dimB;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    //Se cambia el estado de registro a modificado.
                    conexion.Entry(obj).State = EntityState.Modified;
                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si encuentra error devuelve cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_1PIECE
        /// </summary>
        /// <param name="id_external"></param>
        /// <returns></returns>
        public int DeleteExternal_GR_1P(int id_external)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    // Se obtiene el objeto que se va a eliminar.
                    TBL_EXTERNAL_GUIDE_ROLLER_1PIECE obj = Conexion.TBL_EXTERNAL_GUIDE_ROLLER_1PIECE.Where(x => x.ID_EGR_1P == id_external).FirstOrDefault();

                    //Se estable el estado del registro a eliminado.
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
        /// Método que obtiene un registro que cumpla con el rango del width
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public IList GetEXTERNAL_GR_1P(double width)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y lo guardamos en una variable
                    var Lista = (from a in Conexion.TBL_EXTERNAL_GUIDE_ROLLER_1PIECE
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.WIRE_WIDTH_MIN > width && a.WIDE_WIDTH_MAX <= width
                                 select new
                                 {
                                     CODIGO = b.Codigo,
                                     DESCRIPCION = b.Descripcion,
                                     a.DIMB,
                                     a.DETALLE
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros, se filtran por el código o descripción
        /// </summary>
        /// <param name="textoBusq"></param>
        /// <returns></returns>
        public IList GetAllEXTERNAL_GR_1P(string textoBusq)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y guardamos el resultado en una variable
                    var Lista = (from c in Conexion.TBL_EXTERNAL_GUIDE_ROLLER_1PIECE
                                 join m in Conexion.MaestroHerramentales on c.CODIGO equals m.Codigo
                                 where c.CODIGO.Contains(textoBusq) || m.Descripcion.Contains(textoBusq)
                                 select new
                                 {
                                     CODIGO = m.Codigo,
                                     DESCRIPCION = m.Descripcion,
                                     c.DIMB,
                                     c.DETALLE,
                                     c.WIDE_WIDTH_MAX,
                                     c.WIRE_WIDTH_MIN
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }
        /// <summary>
        /// Método que inserta un registro a la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="DimD"></param>
        /// <returns></returns>
        public int SetExternal_GR_3P_1(string codigo, string code, double dimA, double dimB, double dimC, double W_Min, double W_Max)
        {
            try
            {   //Establecemos la conexión a través de EntityFramework.
                using (var conexion = new EntitiesTooling())
                {
                    //Se  crea un objeto, el cual se va agregar a la tabla 
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1 obj = new TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1();
                    //Se asiganan los valores.
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    obj.WIDE_WIDTH_MAX = W_Max;

                    //Agrega el objeto a la tabla.
                    conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1.Add(obj);
                    conexion.SaveChanges();
                    //Retorna el id del registro insertado
                    return obj.ID_EGR_3P_1;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa cero
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1
        /// </summary>
        /// <param name="id_ext"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int UpdateExternal_GR_3P_1(int id_ext, string codigo, string code, double dimA, double dimB, double dimC, double W_Min, double W_Max)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {   
                    //Se obtiene el objeto que se va a modificar.
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1 obj = Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1.Where(x => x.ID_EGR_3P_1 == id_ext).FirstOrDefault();

                    //Asiganmos los valores
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;
                    //Se guardan los cambios y se retorna el número de registros afectados.
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
        ///  Método que elimina un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1
        /// </summary>
        /// <param name="id_external"></param>
        /// <returns></returns>
        public int DeleteExternal_GR_3P_1(int id_external)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion= new EntitiesTooling())
                {
                    // Se obtiene el objeto que se va a eliminar.
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1 obj = Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1.Where(x => x.ID_EGR_3P_1 == id_external).FirstOrDefault();
                    //Se estable el estado del registro a eliminado.
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
        /// Método que obtiene un registro que cumpla con el rango del width
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public IList GetEXTERNAL_GR_3P_1(double width)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y lo guardamos en una variable
                    var Lista = (from a in Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.WIRE_WIDTH_MIN > width && a.WIDE_WIDTH_MAX <= width
                                 select new
                                 {
                                     CODIGO = b.Codigo,
                                     DESCRIPCION = b.Descripcion,
                                     a.DIMA,
                                     a.DIMB,
                                     a.DIMC,
                                     a.DETALLE
                                 }).ToList();

                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros, se filtran por el código o descripción
        /// </summary>
        /// <param name="textoBusq"></param>
        /// <returns></returns>
        public IList GetAllEXTERNAL_GR_3P_1(string textoBusq)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y guardamos el resultado en una variable
                    var Lista = (from c in Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1
                                 join m in Conexion.MaestroHerramentales on c.CODIGO equals m.Codigo
                                 where c.CODIGO.Contains(textoBusq) || m.Descripcion.Contains(textoBusq)
                                 select new
                                 {
                                     CODIGO = m.Codigo,
                                     DESCRIPCION = m.Descripcion,
                                     c.DIMA,
                                     c.DIMB,
                                     c.DIMC,
                                     c.DETALLE,
                                     c.WIRE_WIDTH_MIN,
                                     c.WIDE_WIDTH_MAX
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }


        /// <summary>
        /// Método que inserta un registro a la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="DimD"></param>
        /// <returns></returns>
        public int SetExternal_GR_3P_2(string codigo, string code, double dimA, double dimB, double dimC, double W_Min, double W_Max)
        {
            try
            {   //Establecemos la conexión a través de EntityFramework.
                using (var conexion = new EntitiesTooling())
                {
                    //Se  crea un objeto, el cual se va agregar a la tabla 
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2 obj = new TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2();
                    //Se asiganan los valores.
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    obj.WIDE_WIDTH_MAX = W_Max;

                    //Agrega el objeto a la tabla.
                    conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2.Add(obj);
                    conexion.SaveChanges();
                    //Retorna el id del registro insertado
                    return obj.ID_EGR_3P_2;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa cero
                return 0;
            }
        }
        /// <summary>
        /// Método que modifica un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2
        /// </summary>
        /// <param name="id_ext"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int UpdateExternal_GR_3P_2(int id_ext, string codigo, string code, double dimA, double dimB, double dimC, double W_Min, double W_Max)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2 obj = Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2.Where(x => x.ID_EGR_3P_2 == id_ext).FirstOrDefault();

                    //Asiganmos los valores
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;
                    //Se guardan los cambios y se retorna el número de registros afectados.
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
        ///  Método que elimina un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2
        /// </summary>
        /// <param name="id_external"></param>
        /// <returns></returns>
        public int DeleteExternal_GR_3P_2(int id_external)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    // Se obtiene el objeto que se va a eliminar.
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2 obj = Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2.Where(x => x.ID_EGR_3P_2 == id_external).FirstOrDefault();
                    //Se estable el estado del registro a eliminado.
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
        /// Método que obtiene un registro que cumpla con el rango del width
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public IList GetEXTERNAL_GR_3P_2(double width)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y lo guardamos en una variable
                    var Lista = (from a in Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.WIRE_WIDTH_MIN > width && a.WIDE_WIDTH_MAX <= width
                                 select new
                                 {
                                     CODIGO = b.Codigo,
                                     DESCRIPCION = b.Descripcion,
                                     a.DIMA,
                                     a.DIMB,
                                     a.DIMC,
                                     a.DETALLE
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros, se filtran por el código o descripción
        /// </summary>
        /// <param name="textoBusq"></param>
        /// <returns></returns>
        public IList GetAllEXTERNAL_GR_3P_2(string textoBusq)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y guardamos el resultado en una variable
                    var Lista = (from c in Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2
                                 join m in Conexion.MaestroHerramentales on c.CODIGO equals m.Codigo
                                 where c.CODIGO.Contains(textoBusq) || m.Descripcion.Contains(textoBusq)
                                 select new
                                 {
                                     CODIGO = m.Codigo,
                                     DESCRIPCION = m.Descripcion,
                                     c.DIMA,
                                     c.DIMB,
                                     c.DIMC,
                                     c.DETALLE,
                                     c.WIRE_WIDTH_MIN,
                                     c.WIDE_WIDTH_MAX
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }
        /// <summary>
        /// Método que inserta un registro a la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="DimD"></param>
        /// <returns></returns>
        public int SetExternal_GR_3P_3(string codigo, string code, double dimA, double dimB, double dimC, double W_Min, double W_Max)
        {
            try
            {   //Establecemos la conexión a través de EntityFramework.
                using (var conexion = new EntitiesTooling())
                {
                    //Se  crea un objeto, el cual se va agregar a la tabla 
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3 obj = new TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3();
                    //Se asiganan los valores.
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    obj.WIDE_WIDTH_MAX = W_Max;

                    //Agrega el objeto a la tabla.
                    conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3.Add(obj);
                    conexion.SaveChanges();
                    //Retorna el id del registro insertado
                    return obj.ID_EGR_3P_3;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa cero
                return 0;
            }
        }
        /// <summary>
        /// Método que modifica un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3
        /// </summary>
        /// <param name="id_ext"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="dimB"></param>
        /// <param name="dimC"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int UpdateExternal_GR_3P_3(int id_ext, string codigo, string code, double dimA, double dimB, double dimC, double W_Min, double W_Max)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3 obj = Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3.Where(x => x.ID_EGR_3P_3 == id_ext).FirstOrDefault();
                    //Asiganmos los valores
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.DIMB = dimB;
                    obj.DIMC = dimC;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;
                    //Se guardan los cambios y se retorna el número de registros afectados.
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
        /// Método que elimina un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3
        /// </summary>
        /// <param name="id_external"></param>
        /// <returns></returns>
        public int DeleteExternal_GR_3P_3(int id_external)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    // Se obtiene el objeto que se va a eliminar.
                    TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3 obj = Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3.Where(x => x.ID_EGR_3P_3 == id_external).FirstOrDefault();
                    //Se estable el estado del registro a eliminado.
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
        /// Método que obtiene un registro que cumpla con el rango del width
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public IList GetEXTERNAL_GR_3P_3(double width)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y lo guardamos en una variable
                    var Lista = (from a in Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.WIRE_WIDTH_MIN > width && a.WIDE_WIDTH_MAX <= width
                                 select new
                                 {
                                     CODIGO = b.Codigo,
                                     DESCRIPCION = b.Descripcion,
                                     a.DIMA,
                                     a.DIMB,
                                     a.DIMC,
                                     a.DETALLE
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros, se filtran por el código o descripción
        /// </summary>
        /// <param name="textoBusq"></param>
        /// <returns></returns>
        public IList GetAllEXTERNAL_GR_3P_3(string textoBusq)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y guardamos el resultado en una variable
                    var Lista = (from c in Conexion.TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3
                                 join m in Conexion.MaestroHerramentales on c.CODIGO equals m.Codigo
                                 where c.CODIGO.Contains(textoBusq) || m.Descripcion.Contains(textoBusq)
                                 select new
                                 {
                                     CODIGO = m.Codigo,
                                     DESCRIPCION = m.Descripcion,
                                     c.DIMA,
                                     c.DIMB,
                                     c.DIMC,
                                     c.DETALLE,
                                     c.WIDE_WIDTH_MAX,
                                     c.WIRE_WIDTH_MIN
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }
        /// <summary>
        /// Método que inserta un registro a la tabla TBL_SHIM_OF_THE_CUT_SYSTEM
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int SetSHIM_OF_THE_CUT_SYSTEM(string codigo, string code, double dimA, double W_Min, double W_Max)
        {
            try
            {   //Establecemos la conexión a través de EntityFramework.
                using (var conexion = new EntitiesTooling())
                {
                    //Se  crea un objeto, el cual se va agregar a la tabla 
                    TBL_SHIM_OF_THE_CUT_SYSTEM obj = new TBL_SHIM_OF_THE_CUT_SYSTEM();
                    //Se asiganan los valores.
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;

                    //Agrega el objeto a la tabla.
                    conexion.TBL_SHIM_OF_THE_CUT_SYSTEM.Add(obj);
                    //Se guardan los cambios
                    conexion.SaveChanges();
                    //Retorna el id del registro insertado
                    return obj.ID_SHIM_OTCS;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa cero
                return 0;
            }
        }
        /// <summary>
        /// Método que modifica un registro de la tabla TBL_SHIM_OF_THE_CUT_SYSTEM
        /// </summary>
        /// <param name="id"></param>
        /// <param name="codigo"></param>
        /// <param name="code"></param>
        /// <param name="dimA"></param>
        /// <param name="W_Min"></param>
        /// <param name="W_Max"></param>
        /// <returns></returns>
        public int UpdateSHIM_OF_THE_CUT_SYSTEM(int id,string codigo, string code, double dimA, double W_Min, double W_Max)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var conexion = new EntitiesTooling())
                {
                    //Se obtiene el objeto que se va a modificar.
                    TBL_SHIM_OF_THE_CUT_SYSTEM obj = conexion.TBL_SHIM_OF_THE_CUT_SYSTEM.Where(x => x.ID_SHIM_OTCS == id).FirstOrDefault();
                    //Asiganmos los valores
                    obj.CODIGO = codigo;
                    obj.DETALLE = code;
                    obj.DIMA = dimA;
                    obj.WIDE_WIDTH_MAX = W_Max;
                    obj.WIRE_WIDTH_MIN = W_Min;
                    //Se cambia el estado de registro a modificado.
                    conexion.Entry(obj).State = EntityState.Modified;
                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si encuentra error devuelve cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro de la tabla TBL_SHIM_OF_THE_CUT_SYSTEM
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteSHIM_OF_THE_CUT_SYSTEM(int id)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesTooling())
                {
                    // Se obtiene el objeto que se va a eliminar.
                    TBL_SHIM_OF_THE_CUT_SYSTEM obj = Conexion.TBL_SHIM_OF_THE_CUT_SYSTEM.Where(x => x.ID_SHIM_OTCS == id).FirstOrDefault();
                    //Se estable el estado del registro a eliminado.
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
        /// Método que obtiene un registro que cumpla con el rango del width
        /// </summary>
        /// <param name="width"></param>
        /// <returns></returns>
        public IList GetSHIM_CSYSTEM(double width)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y lo guardamos en una variable
                    var Lista = (from a in Conexion.TBL_SHIM_OF_THE_CUT_SYSTEM
                                 join b in Conexion.MaestroHerramentales on a.CODIGO equals b.Codigo
                                 where a.WIRE_WIDTH_MIN > width && a.WIDE_WIDTH_MAX <= width
                                 select new
                                 {
                                     CODIGO = b.Codigo,
                                     DESCRIPCION = b.Descripcion,
                                     a.DIMA,
                                     a.DETALLE
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todos los registros, se filtran por el código o descripción
        /// </summary>
        /// <param name="textoBusq"></param>
        /// <returns></returns>
        public IList GetAllSHIM_CUT_SYSTEM(string textoBusq)
        {
            try
            {
                //Establecemos la conexion
                using (var Conexion = new EntitiesTooling())
                {
                    //Ejecutamos la consulta y guardamos el resultado en una variable
                    var Lista = (from c in Conexion.TBL_SHIM_OF_THE_CUT_SYSTEM
                                 join m in Conexion.MaestroHerramentales on c.CODIGO equals m.Codigo
                                 where c.CODIGO.Contains(textoBusq) || m.Descripcion.Contains(textoBusq)
                                 select new
                                 {
                                     CODIGO = m.Codigo,
                                     DESCRIPCION = m.Descripcion,
                                     c.DIMA,
                                     c.DETALLE,
                                     c.WIRE_WIDTH_MIN,
                                     c.WIDE_WIDTH_MAX
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si hay error retorna nulo
                return null;
            }
        }
    }
}
