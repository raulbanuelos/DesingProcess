using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Cuffs : Arquetipo
    {
        #region Propiedades
        public PropiedadCadena no_cuff { get; set; }
        public Propiedad dia_ext { get; set; }
        public Propiedad dia_int { get; set; }
        public Propiedad largo { get; set; }
        public Propiedad peso { get; set; }
        #endregion

        #region Constructor
        public Cuffs()
        {
            no_cuff = new PropiedadCadena();
            dia_ext = new Propiedad();
            dia_int = new Propiedad();
            largo = new Propiedad();
            peso = new Propiedad();

        }
        #endregion
    }
}
