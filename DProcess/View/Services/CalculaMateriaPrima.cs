using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services
{
    public class CalculaMateriaPrima
    {
        #region Attributes
        private Anillo _elAnillo;
        private double rise;
        string codigoPlacaModelo;
        #endregion

        #region Properties
        public double Piece { get; set; }
        public double FreeGap { get; set; }
        #endregion

        #region Constructors
        public CalculaMateriaPrima(Anillo elAnillo)
        {
            _elAnillo = elAnillo;
            FreeGap = Module.ConvertTo("Distance", _elAnillo.FreeGap.Unidad, "Inch (in)", _elAnillo.FreeGap.Valor);
        }

        #endregion

        #region Methods
        public MateriaPrima CalcularPlacaModelo()
        {
            MateriaPrima m = new MateriaPrima();
            Piece = calculaPiece();



            return m;
        }

        private double calculaPiece()
        {
            double _piece = 0;
            double widthMin = Module.GetValorPropiedadMin("h1", _elAnillo.PerfilLateral.Propiedades,true);
            double widthMax = Module.GetValorPropiedadMax("h1", _elAnillo.PerfilLateral.Propiedades, true);

            double promedioWidth = (widthMin + widthMax) / 2;

            double closingStress = Module.GetValorPropiedad("CLOSING STRESS", _elAnillo.PerfilOD.Propiedades); //Verificar como se llama la propiedad en el archivo RDCT.

            if ((closingStress <= 30000) && (promedioWidth > 0.035) && (promedioWidth < 0.07))
            {
                _piece = FreeGap / 0.96;
            }
            else if ((closingStress >= 30000) && (promedioWidth > 0.035) && (promedioWidth < 0.070))
            {
                _piece = FreeGap / .945;
            }
            else if ((closingStress <= 30000) && (promedioWidth > .0701) && (promedioWidth <= 0.090))
            {
                _piece = FreeGap / .935;
            }
            else if ((closingStress >= 30000) && (promedioWidth > 0.0701) && (promedioWidth < 0.090))
            {
                _piece = FreeGap / .925;
            }
            else if ((promedioWidth > 0.901))
            {
                _piece = FreeGap / .86;
            }
            return _piece;
        }
        #endregion
    }
}
