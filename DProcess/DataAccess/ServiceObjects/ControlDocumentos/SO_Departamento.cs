using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
   public  class SO_Departamento
    {
        /// <summary>
        /// Méotodo para obtener todos los registros de la tabla.
        /// </summary>
        /// <returns>Si hay algún error,retorna cero.</returns>
        public IList GetDepartamento()
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from d in Conexion.TBL_DEPARTAMENTO
                                 select new
                                 {
                                     d.ID_DEPARTAMENTO,
                                     d.NOMBRE_DEPARTAMENTO,
                                     d.FECHA_ACTUALIZACION,
                                     d.FECHA_CREACION
                                 }).ToList();
                    //se retorna la lista
                    return Lista;

                }
            }
            catch (Exception)
            {
                //Si hay algún error, se retorna un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método para insertar un registro a la tabla TBL_Departamento.
        /// </summary>
        /// <param name="id_dep"></param>
        /// <param name="nombre_dep"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <returns>Retorna cero, si hay algún error.</returns>
        public int SetDepartamento(int id_dep,string nombre_dep,DateTime fecha_creacion,DateTime fecha_actualizacion)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    TBL_DEPARTAMENTO obj = new TBL_DEPARTAMENTO();

                    //Se asiganan los valores.
                    obj.ID_DEPARTAMENTO = id_dep;
                    obj.NOMBRE_DEPARTAMENTO = nombre_dep;
                    obj.FECHA_CREACION = fecha_creacion;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;

                    //Agrega el objeto a la tabla.
                    Conexion.TBL_DEPARTAMENTO.Add(obj);
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del departamento insertado
                    return obj.ID_DEPARTAMENTO;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar un registro de la tabla TBL_Departamento
        /// </summary>
        /// <param name="id_dep"></param>
        /// <param name="nombre_dep"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <returns>Retorna cero, si hay cambios.</returns>
        public int UpdateDepartamento(int id_dep, string nombre_dep, DateTime fecha_creacion, DateTime fecha_actualizacion)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_DEPARTAMENTO obj = Conexion.TBL_DEPARTAMENTO.Where(x => x.ID_DEPARTAMENTO == id_dep).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    obj.NOMBRE_DEPARTAMENTO = nombre_dep;
                    obj.FECHA_CREACION = fecha_creacion;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;

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
        /// Método para eliminar un registro a la tabla TBL_depatarmento.
        /// </summary>
        /// <param name="id_dep"></param>
        /// <returns></returns>
        public int DeleteDepartamento(int id_dep)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TBL_DEPARTAMENTO obj = Conexion.TBL_DEPARTAMENTO.Where(x => x.ID_DEPARTAMENTO == id_dep).FirstOrDefault();

                    //Se estable el estado de registro a eliminado.
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error, se regresa 0.
                return 0;
            }
        }

       
    }
}
