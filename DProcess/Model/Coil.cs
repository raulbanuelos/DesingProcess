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
        public double dimA { get; set; }
        public double dimB { get; set; }
        public double dimC { get; set; }
        public double dimD { get; set; }
        public double wire_width_min { get; set; }
        public double wire_width_max { get; set; }
        public double radial_wire_min { get; set; }
        public double radial_wire_max { get; set; }
    }
}
