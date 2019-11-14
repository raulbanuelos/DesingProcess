using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Usuario
{
    public class SO_Bitacora_Ingreso
    {
        /// <summary>
        /// Método para ingresar un registro en la tabla TBL_BITACORA_INGRESO.
        /// </summary>
        /// <param name="nameComputer">Nombre de la computadora.</param>
        /// <param name="nameUser">Nomrbe del usuario.</param>
        /// <returns></returns>
        public int Insert(string nameComputer, string nameUser)
        {
            try
            {
                using (var Conexion = new EntitiesUsuario())
                {
                    TBL_BITACORA_INGRESO tblBitacora = new TBL_BITACORA_INGRESO();

                    tblBitacora.FECHA_INGRESO = DateTime.Now;
                    tblBitacora.NOMBRE_COMPUTADORA = nameComputer;
                    tblBitacora.USUARIO = nameUser;

                    Conexion.TBL_BITACORA_INGRESO.Add(tblBitacora);

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
