using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class TipoPerfil
    {
        #region Properties
        public int IdTipoPerfil { get; set; }

        public string NombreTipoPerfil { get; set; } 
        #endregion

        #region Methods
        public override string ToString()
        {
            return NombreTipoPerfil;
        } 
        #endregion
    }
}
