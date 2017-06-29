using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
   public  class SO_Rol
    {
        /// <summary>
        /// Método para obtener los roles
        /// </summary>
        /// <returns></returns>
        public IList GetRol()
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from r in Conexion.TBL_ROL
                                 select new
                                 {
                                     r.ID_ROL,
                                     r.NOMBRE_ROL,
                                     r.FECHA_CREACION,
                                     r.FECHA_ACTUALIZACION
                                 }).ToList();
                    //se retorna la lista
                    return Lista;

                }
            }
            catch (Exception)
            {
                //Si hay algún error, se retorna nulo.
                return null;
            }
        }

        /// <summary>
        /// Método para insertar un resistro a la tabla TBL_rol
        /// </summary>
        /// <param name="id_rol"></param>
        /// <param name="nombre_rol"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <returns>Retorna cero, si hay error.</returns>
        public int SetRol(int id_rol,string nombre_rol,DateTime fecha_creacion,DateTime fecha_actualizacion)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    TBL_ROL rol = new TBL_ROL();

                    //Se asiganan los valores.
                   //rol.ID_ROL=id_rol;
                    rol.NOMBRE_ROL = nombre_rol;
                    rol.FECHA_CREACION = fecha_creacion;
                    rol.FECHA_ACTUALIZACION = fecha_actualizacion;

                    //Agrega el objeto a la tabla.
                    Conexion.TBL_ROL.Add(rol);
                    
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del usuario insertado
                    return rol.ID_ROL;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar los registros de la tabla.
        /// </summary>
        /// <param name="id_rol"></param>
        /// <param name="nombre_rol"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <returns>Retorna cero si hay error.</returns>
        public int UpdateRol(int id_rol, string nombre_rol, DateTime fecha_creacion, DateTime fecha_actualizacion)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_ROL rol = Conexion.TBL_ROL.Where(x => x.ID_ROL == id_rol).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.

                    rol.NOMBRE_ROL = nombre_rol;
                    rol.FECHA_CREACION = fecha_creacion;
                    rol.FECHA_ACTUALIZACION = fecha_actualizacion;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(rol).State = EntityState.Modified;

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
        /// Método para eliminar un registro de la tabla.
        /// </summary>
        /// <param name="id_rol"></param>
        /// <returns></returns>
        public int DeleteRol(int id_rol)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TBL_ROL rol = Conexion.TBL_ROL.Where(x => x.ID_ROL == id_rol).FirstOrDefault();

                    //Se estable el estado de registro a eliminado.
                    Conexion.Entry(rol).State = EntityState.Deleted;

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

        /// <summary>
        /// Método para obtener los roles de un usuario en específico.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public IList GetRol_Usuario(string usuario)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from r in Conexion.TBL_ROL
                                 join tr in Conexion.TR_ROL_USUARIOS on r.ID_ROL equals tr.ID_ROL
                                 where tr.ID_USUARIO.Contains(usuario)
                                 select new
                                 {
                                     r.ID_ROL,
                                     r.NOMBRE_ROL
                                 }).ToList();
                    //se retorna la lista
                    return Lista;

                }
            }
            catch (Exception)
            {
                //Si hay algún error, se retorna nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que insertar los roles de cada usuario
        /// </summary>
        /// <param name="id_rol"></param>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public int SetRol_Usuario(int id_rol,string id_usuario)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    TR_ROL_USUARIOS rol = new TR_ROL_USUARIOS();

                    //Se asiganan los valores.
                    rol.ID_ROL=id_rol;
                    rol.ID_USUARIO = id_usuario;

                    //Agrega el objeto a la tabla.
                    Conexion.TR_ROL_USUARIOS.Add(rol);

                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del usuario insertado
                    return rol.ID_ROL_USUARIOS;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }
    }

}
