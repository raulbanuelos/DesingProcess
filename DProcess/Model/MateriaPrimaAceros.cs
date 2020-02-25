using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MateriaPrimaAceros : MateriaPrima
    {
        
        public double ESP_AXIAL { get; set; }
        public double ESP_RADIAL { get; set; }
        public string PROVEEDOR { get; set; }
        public string PROVEEDOR2 { get; set; }
        public bool IsSelected { get; set; }

    }
}
