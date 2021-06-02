using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_TipoAnillo
    {
        /// <summary>
        /// Método que obtiene el id del tipo de anillo.
        /// </summary>
        /// <param name="tipoAnillo"></param>
        /// <returns></returns>
        public IList GetTipoAnillo(string tipoAnillo)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una lista anónima.
                    IList Lista = (from a in Conexion.Tipo_Anillo
                                   where a.Tipo == tipoAnillo
                                   select new
                                   {
                                       IdTipoAnillo = a.Id_Tipo
                                   }).ToList();

                    //Retornamos el resultado de la lista.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todos los tipos de anillo
        /// </summary>
        /// <returns></returns>
        public IList GetAllTipoAnillo()
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una lista anónima.
                    IList Lista = (from a in Conexion.Tipo_Anillo
                                   select new
                                   {
                                       a.Id_Tipo,
                                       a.Tipo,
                                       a.Recubrimiento,
                                       a.Diseno
                                   }).OrderBy(x => x.Tipo).ToList();

                    //Retornamos el resultado de la lista.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }
    }
}
