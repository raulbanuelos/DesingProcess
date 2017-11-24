using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class MoutingDia
    {
        public int id { get; set; }
        public double plato { get; set; }
        public double dia_min { get; set; }
        public double dia_max { get; set; }
        public int num_impresiones { get; set; }
        public string  gate { get; set; }
        public string  medios_circulos { get; set; }
        public string boton { get; set; }
        public string conos { get; set; }
        public int orden { get; set; }
    }
}
