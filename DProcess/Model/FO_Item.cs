using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class FO_Item
    {
        public int id { get; set; }

        public string Nombre { get; set; }

        public double Valor { get; set; }

        public string ValorCadena { get; set; }

        public bool IsSelected { get; set; }

        public override string ToString()
        {
            return Nombre;
        }
    }
}
