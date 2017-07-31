using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_Bloqueo
    {
        /// <summary>
        /// Obtenemos un registro que tenga el estado de bloqueado
        /// </summary>
        /// <returns></returns>
        public IList GetBloqueo()
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion= new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, si hay algún estado en bloqueado
                    var Lista = (from b in Conexion.TBL_BLOQUEO
                                 where b.ESTADO == true
                                 select new
                                 {
                                     b.ID_BLOQUEO,
                                     b.FECHA_FIN,
                                     b.FECHA_INICIO,
                                     b.OBSERVACIONES,
                                     b.ESTADO
                                 }).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception e)
            {
                //Si hay error, retornamos nulo
                return null;
            }
        }

        /// <summary>
        /// Método que inserta un nuevo registro de bloqueo a la base de datos
        /// </summary>
        /// <param name="fecha_inicio"></param>
        /// <param name="fecha_fin"></param>
        /// <param name="estado"></param>
        /// <param name="observaciones"></param>
        /// <returns></returns>
        public int SetBloqueo(DateTime fecha_inicio, DateTime fecha_fin, string observaciones)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se crea un objeto de tipo bloqueo, el cual va agregar a la lista
                    TBL_BLOQUEO obj = new TBL_BLOQUEO();

                    //Se asiganan los valores.
                    obj.FECHA_INICIO = fecha_inicio;
                    obj.FECHA_FIN = fecha_fin;
                    obj.ESTADO = true;
                    obj.OBSERVACIONES = observaciones;

                    //Agrega el objeto a la tabla.
                    Conexion.TBL_BLOQUEO.Add(obj);
                    //Guardamos los cambios
                    Conexion.SaveChanges();

                    //Retorna el id del archivo agregado
                    return obj.ID_BLOQUEO;
                }
            }
            catch (Exception er)
            {
                //Si hay error retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene el id del bloqueo si la fecha es igual a la del sistema o si la fecha final es menor a la fecha del sistema
        /// </summary>
        /// <param name="fecha_fin"></param>
        /// <returns></returns>
        public int GetID_Bloqueo(DateTime fecha_fin)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta, regresa el id del bloqueo si la fecha final es igual a la fecha recibida
                    int id = (from b in Conexion.TBL_BLOQUEO
                              where (b.FECHA_FIN == fecha_fin || b.FECHA_FIN < fecha_fin) && b.ESTADO == true
                              select b.ID_BLOQUEO).ToList().FirstOrDefault();
                    //Retornamos el id
                    return id;
                }
            }
            catch (Exception e)
            {
                //Si hay error, retornamos cero
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_bloqueo"></param>
        /// <param name="fecha_inicio"></param>
        /// <param name="fecha_fin"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public int UpdateBloqueo(int id_bloqueo,DateTime fecha_inicio, DateTime fecha_fin, string observaciones)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion= new EntitiesControlDocumentos())
                {
                    //Creacion del objeto archivo 
                    TBL_BLOQUEO obj = Conexion.TBL_BLOQUEO.Where(x => x.ID_BLOQUEO == id_bloqueo).FirstOrDefault();

                    //Asignamos los valores
                    obj.FECHA_INICIO = fecha_inicio;
                    obj.FECHA_FIN = fecha_fin;
                    obj.OBSERVACIONES = observaciones;

                    //Guardamos las modificaciones
                    Conexion.Entry(obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();

                }
            }
            catch (Exception)
            {

                return 0;
            }
        }

        /// <summary>
        /// Método que modifica el estado a desbloqueado
        /// </summary>
        /// <param name="id_bloqueo"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public int UpdateEstado(int id_bloqueo)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Creacion del objeto archivo 
                    TBL_BLOQUEO obj = Conexion.TBL_BLOQUEO.Where(x => x.ID_BLOQUEO == id_bloqueo).FirstOrDefault();

                    //Asignamos los valores
                    obj.ESTADO = false;

                    //Guardamos las modificaciones
                    Conexion.Entry(obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();

                }
            }
            catch (Exception)
            {
                //Retorna cero
                return 0;
            }
        }
    }
}
