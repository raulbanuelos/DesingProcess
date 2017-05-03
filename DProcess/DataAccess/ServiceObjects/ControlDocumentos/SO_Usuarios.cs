using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_Usuarios
    {
        /// <summary>
        /// Método para obtener  todos los registros de la tabla Usuarios.
        /// </summary>
        /// <returns>Retorns nulo si hay error.</returns>
        public IList GetUsuario()
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from u in Conexion.Usuarios select new
                                 {
                                     u.Usuario,
                                     u.Password,
                                     u.Nombre,
                                     u.APaterno,
                                     u.AMaterno,
                                     u.Estado,
                                     u.Usql,
                                     u.Psql,
                                     u.Bloqueado
                                    
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
        /// Método para insertar un usuario en la tabla Usuarios.
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <param name="nombre"></param>
        /// <param name="APaterno"></param>
        /// <param name="AMaterno"></param>
        /// <param name="estado"></param>
        /// <param name="usql"></param>
        /// <param name="psql"></param>
        /// <param name="bloqueado"></param>
        /// <param name="id_departartemento"></param>
        /// <returns>Si hay error, regresa una cadena vacía</returns>

        public string SetUsuario(string usuario,string password,string nombre,string APaterno,string AMaterno,
                                 int estado,string usql,string psql,bool bloqueado)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion=new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    Usuarios user = new Usuarios();

                    //Se asiganan los valores.
                    user.Usuario = usuario;
                    user.Password = password;
                    user.Nombre = nombre;
                    user.APaterno = APaterno;
                    user.AMaterno = AMaterno;
                    user.Estado = estado;
                    user.Usql = usql;
                    user.Psql = psql;
                    user.Bloqueado = bloqueado;
                    //Agrega el objeto a la tabla.
                    Conexion.Usuarios.Add(user);
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del usuario insertado
                    return user.Usuario;
                }
            }
            catch (Exception er)
            {
                //Si hay error regresa una cadena vacía.
                return string.Empty;
            }
        }

        /// <summary>
        /// Método que actualiza los valores de un registro en la tabla Usuarios.
        /// </summary>
        /// <param name="usuario"></param>
        /// <param name="password"></param>
        /// <param name="nombre"></param>
        /// <param name="APaterno"></param>
        /// <param name="AMaterno"></param>
        /// <param name="estado"></param>
        /// <param name="usql"></param>
        /// <param name="psql"></param>
        /// <param name="bloqueado"></param>
        /// <param name="id_departartemento"></param>
        /// <returns></returns>
        public int UpdateUsuarios(string usuario, string password, string nombre, string APaterno, string AMaterno,
                                 int estado, string usql, string psql, bool bloqueado)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion=new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo Usuarios.
                    Usuarios user = Conexion.Usuarios.Where(x=>x.Usuario==usuario).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    user.Password = password;
                    user.Nombre = nombre;
                    user.APaterno = APaterno;
                    user.AMaterno = AMaterno;
                    user.Estado = estado;
                    user.Usql = usql;
                    user.Psql = psql;
                    user.Bloqueado = bloqueado;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(user).State= EntityState.Modified;

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
        ///  Método que elimina un registro de la tabla Usuarios.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns>Retorna cero si no hay error.</returns>
        public int DeleteUsuario(string usuario)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion= new EntitiesControlDocumentos())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    Usuarios user = Conexion.Usuarios.Where(x => x.Usuario == usuario).FirstOrDefault();

                    //Se estable el estado de registro a eliminado.
                    Conexion.Entry(user).State = EntityState.Deleted;

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
