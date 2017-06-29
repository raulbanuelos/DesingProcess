using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_Validacion
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_tipoDocumento"></param>
        /// <returns></returns>
        public IList GetValidacion(int id_tipoDocumento)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from v in Conexion.TBL_VALIDACION_DOCUMENTO
                                 join tr in Conexion.TR_VALIDACION_TIPO_DOCUMENTO on v.ID_VALIDACION_DOCUMENTO equals tr.ID_VALIDACION_DOCUMENTO
                                 where tr.ID_TIPO_DOCUMENTO == id_tipoDocumento
                                 select new
                                 {
                                     v.ID_VALIDACION_DOCUMENTO,
                                     v.VALIDACION_DOCUMENTO,
                                     v.VALIDACION_DESCRIPCION,
                                     v.FECHA_CREACION
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
    }
}
