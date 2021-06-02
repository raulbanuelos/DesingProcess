using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class DO_FactorContraccion
    {
        #region Properties
        public double DIA_EXT_MAYOR { get; set; }
        public double DIA_EXT_MENOR { get; set; }
        public double F_WIDTH { get; set; }
        public double F_THICKNESS { get; set; }
        public double C_OLD { get; set; }
        public double C_OSD { get; set; }
        public double C_THICKNESS { get; set; }
        public double C_WIDTH { get; set; }
        public double C_THROW { get; set; }
        public double P_THROW { get; set; }
        public bool Exists { get; set; }
        public string Material { get; set; }
        public bool IsLB { get; set; }

        public double ExtB
        {
            get
            {
                return Math.Round(-1 + DIA_EXT_MAYOR, 5);
            }
        }
        public double ExtMenor
        {
            get
            {
                return Math.Round(-1 + DIA_EXT_MENOR, 5);
            }
        }
        public double Width_M
        {
            get
            {
                return Math.Round(-1 + F_WIDTH, 5);
            }
        }
        public double Thickness_M
        {
            get
            {
                return Math.Round(-1 + F_THICKNESS, 5);
            }
        }
        #endregion

        #region Constructors
        public DO_FactorContraccion()
        {
            Exists = false;
        }
        #endregion
    }
}
