using System;
using System.Collections;
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
    }
}
