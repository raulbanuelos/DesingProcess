using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class LeccionesAprendidas
    {
        #region Propiedades
        public int ID_LECCIONES_APRENDIDAS { get; set; }
        public string ID_USUARIO { get; set; }
        public string COMPONENTE { get; set; }
        public string CAMBIO_REQUERIDO { get; set; }
        public string NIVEL_DE_CAMBIO { get; set; }
        public string CENTRO_DE_TRABAJO { get; set; }
        public string OPERACION { get; set; }
        public string DESCRIPCION_PROBLEMA { get; set; }
        public DateTime FECHA_ULTIMO_CAMBIO { get; set; }
        public DateTime FECHA_ACTUALIZACION { get; set; }
        public string REPORTADO_POR { get; set; }
        public string SOLICITUD_DE_TRABAJO { get; set; }
        public string CRITERIO_1 { get; set; }
        public string NombreCompleto
        {
            get;set;
        }
        #endregion
    }
}
