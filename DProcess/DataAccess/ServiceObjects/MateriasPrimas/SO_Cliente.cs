using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_Cliente
    {
        /// <summary>
        /// Método que obtiene todos los registros de clientes.
        /// </summary>
        /// <returns>Lista anónima con la información de clientes. Si se genera algún error retorna un nulo.</returns>
        public IList GetAllClientes()
        {
            try
            {
                //Establecemos la conexión al EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Relizamos la consulta y la guardamos en una variable local.
                    var ListaResultante = (from a in Conexion.Cliente
                                           select a).OrderBy(x => x.Cliente1).ToList();

                    //Retornamos la lista resultante.
                    return ListaResultante;
                }
            }
            catch (Exception er)
            {
                //Si se genera algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que guarda un registro en la tabla cliente.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public int SetCliente(string nombre)
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework
                using (var Conexion= new EntitiesMateriaPrima())
                {
                    //Declaramos un objeto de tipo Cliente.
                    Cliente obj = new Cliente();
                    //Asignamos los valores.
                    obj.Cliente1 = nombre;

                    //Guardamos los cambios.
                    Conexion.Cliente.Add(obj);
                    Conexion.SaveChanges();

                    //Retornamos el id.
                    return obj.id_cliente;
                }
            }
            catch (Exception)
            {
                //Si hay error retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro en la tabla cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public int UpdateCliente(int id, string nombre)
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Obtenemos el objeto que se va a modificar.
                    Cliente obj = Conexion.Cliente.Where(x => x.id_cliente == id).FirstOrDefault();

                    //Modificamos el nombre.
                    obj.Cliente1 = nombre;

                    //Guardamos cambios, retornamos el resultado.
                    Conexion.Entry(obj).State = EntityState.Modified;
                 
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error retornamos cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que elimna un registro de la tabla cliente.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public int DeleteCliente(int id)
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Obtenemos el objeto que se va a eliminar.
                    Cliente obj = Conexion.Cliente.Where(x => x.id_cliente == id).FirstOrDefault();

                    //Guardamos cambios, retornamos el número de registros afectados.
                    Conexion.Entry(obj).State = EntityState.Deleted;
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error retornamos cero.
                return 0;
            }
        }

    }
}
