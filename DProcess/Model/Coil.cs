using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Coil
    {
        public int ID { get; set; }
        public string codigo { get; set; }
        public string code { get; set; }
        public float dimA { get; set; }
        public float dimB { get; set; }
        public float dimC { get; set; }
        public float dimD { get; set; }
        public float wire_width_min { get; set; }
        public float wire_width_max { get; set; }
        public float radial_wire_min { get; set; }
        public float radial_wire_max { get; set; }
    }
}
