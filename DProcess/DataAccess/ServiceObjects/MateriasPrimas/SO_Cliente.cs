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
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Relizamos la conexión a través de EntityFramework.
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
        /// 
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public int SetCliente(string nombre)
        {
            try
            {
                using (var Conexion= new EntitiesMateriaPrima())
                {
                    Cliente obj = new Cliente();

                    obj.Cliente1 = nombre;

                    Conexion.Cliente.Add(obj);
                    Conexion.SaveChanges();

                    return obj.id_cliente;
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
        /// <param name="nombre"></param>
        /// <returns></returns>
        public int UpdateCliente(int id, string nombre)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    Cliente obj = Conexion.Cliente.Where(x => x.id_cliente == id).FirstOrDefault();

                    obj.Cliente1 = nombre;

                    Conexion.Entry(obj).State = EntityState.Modified;
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
        public int DeleteCliente(int id)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    Cliente obj = Conexion.Cliente.Where(x => x.id_cliente == id).FirstOrDefault();

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
