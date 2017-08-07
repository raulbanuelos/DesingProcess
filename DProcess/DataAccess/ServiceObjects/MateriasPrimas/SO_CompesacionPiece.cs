using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_CompesacionPiece
    {
        /// <summary>
        /// Método el cual obtiene la compensación para el piece.
        /// </summary>
        /// <param name="idMaterial"></param>
        /// <param name="idTipoAnillo"></param>
        /// <returns></returns>
        public IList GetCompensacion(string idMaterial, int idTipoAnillo)
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta, el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.Compensacion_Piece
                                 where a.IdMaterial == idMaterial && a.IdTipoAnillo == idTipoAnillo
                                 select new
                                 {
                                     a.Compensacion
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }
    }
}
