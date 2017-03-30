using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class Rol
    {
        #region Propiedades
        public int id_rol { get; set; }
        public string nombre_rol { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        #endregion
        #region Constructores
        public Rol()
        {
            id_rol = 0;
            nombre_rol = "";
            fecha_creacion = new DateTime();
            fecha_actualizacion = new DateTime();

        }
        //int id_rol,string nombre_rol,DateTime fecha_creacion,DateTime fecha_actualizacion
        #endregion
    }
}
