using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class Departamento
    {
        #region Propiedades
        public int  id_dep { get; set; }
        public string nombre_dep { get; set; }
        public DateTime fecha_creacion { get; set; }
        public DateTime fecha_actualizacion { get; set; }
        #endregion
        #region Constructores
        public Departamento()
        {
            id_dep = 0;
            nombre_dep = "";
            fecha_creacion = new DateTime();
            fecha_actualizacion = new DateTime();
            
        }

        #endregion

        //int id_dep,string nombre_dep,DateTime fecha_creacion,DateTime fecha_actualizacion
        
    }
}
