using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class CentrosTrabajo
    {
        #region Propiedades
        public string CentroTrabajo { get; set; }
        public double TiempoSetup { get; set; }
        public string NombreOperacion { get; set; }
        public string ObjetoXML { get; set; }
        public string ObjetoXMLVista { get; set; }
        public string NombreIngles { get; set; }
        public bool IsSelected { get; set; }
        #endregion
    }
}
