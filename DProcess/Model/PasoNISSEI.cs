using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PasoNISSEI
    {
        public int NumPaso { get; set; }
        public CorteNISSEI[] Cortes;

        public PasoNISSEI()
        {
            NumPaso = 0;
        }
    }
}
