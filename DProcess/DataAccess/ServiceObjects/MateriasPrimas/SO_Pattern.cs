using System;
using System.Collections;
using System.Linq;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_Pattern
    {
        #region Propiedades
        #endregion

        #region Constructores
        #endregion

        #region Métodos

        /// <summary>
        /// Método con el cual se obtienen  todas las placas modelo del sistema.
        /// </summary>
        /// <returns></returns>
        public IList GetAllPattern()
        {
            try
            {
                //Establesemos la conexión a través de Entity Framework
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta.
                    var Lista = (from p in Conexion.Pattern2
                                 join c in Conexion.Cliente on p.CUSTOMER equals c.id_cliente
                                 join t in Conexion.Tipo_Materia_Prima on p.TIPO equals t.id_tipo_mp
                                 select new {
                                     p.codigo,
                                     DIAMETRO = p.MEDIDA,
                                     WIDTH = p.DIAMETRO,
                                     c.Cliente1,p.MOUNTING,p.ON_14_RD_GATE,p.BUTTON,p.CONE,
                                     p.M_CIRCLE,p.RING_WTH_min,p.RING_WTH_max,p.DATE_ORDERED,
                                     p.B_DIA,p.FIN_DIA,p.TURN_ALLOW,p.CSTG_SM_OD,p.SHRINK_ALLOW,p.PATT_SM_OD,p.PIECE_IN_PATT,p.BORE_ALLOW,
                                     p.PATT_SM_ID,p.PATT_THICKNESS,p.JOINT,p.NICK,p.NICK_DRAF,p.NICK_DEPTH,p.SIDE_RELIEF,p.CAM,p.CAM_ROLL,
                                     p.RISE,p.OD,p.ID,p.DIFF,TIPO = t.materia_prima,p.mounted,p.ordered,p.@checked,p.date_checked,p.esp_inst,p.factor_k,p.rise_built,p.ring_th_min,p.ring_th_max,
                                     p.estado,p.Plato,p.Detalle,p.Diseno
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Registrar el error.

                //Si se generó algún error, retornamos un nulo.
                return null;
            }
        }

        #endregion
    }
}
