using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Tubos_CL : Arquetipo
    {
        #region Propiedades
        public PropiedadCadena Tubo { get; set; }
        public Propiedad DiaExt { get; set; }
        public Propiedad DiaInt { get; set; }
        public Propiedad Thickness { get; set; }
        public Propiedad Largo { get; set; }
        #endregion

        #region Constructor
        public Tubos_CL()
        {
            Tubo = new PropiedadCadena();
            DiaExt = new Propiedad();
            DiaInt = new Propiedad();
            Thickness = new Propiedad();
            Largo = new Propiedad();

        }
        #endregion
    }
}
