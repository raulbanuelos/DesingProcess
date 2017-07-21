using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.Operaciones.Gasolina
{
    public class CalculaPlacaModelo
    {
        #region Attibuttes
        private Anillo _elAnillo;

        #endregion

        #region Properties
        public double free_gap { get; set; }
        public double piece { get; set; }
        #endregion

        #region Constructor
        public CalculaPlacaModelo(Anillo elAnillo)
        {
            _elAnillo = elAnillo;
            free_gap = Module.ConvertTo("Distance", _elAnillo.FreeGap.TipoDato, "Inch (in)", elAnillo.FreeGap.Valor);
        }
        #endregion

        #region Methods
        public MateriaPrima CalcularPlacaModelo()
        {
            MateriaPrima materiaprima = new MateriaPrima();
            piece = calculaPiece();




            return materiaprima;
        }

        private double calculaPiece()
        {
            //double _piece = 0;
            //double widthMin = Module.ToInch(Module.getPropiedad("WidthMin", _elAnillo.PerfilLateral.Propiedades)).Valor;
            //double widthMax = Module.ToInch(Module.getPropiedad("WidthMax", _elAnillo.PerfilLateral.Propiedades)).Valor;
            //double promedioWidth = (widthMin + widthMax) / 2;
            //double closingStress = Module.getValorPropiedad("CLOSING STRESS", _elAnillo.PerfilOD.Propiedades);
            //if ((closingStress <= 30000) && (promedioWidth > 0.035) && (promedioWidth < 0.07))
            //{
            //    _piece = free_gap / 0.96;
            //}
            //else if ((closingStress >= 30000) && (promedioWidth > 0.035) && (promedioWidth < 0.070))
            //{
            //    _piece = free_gap / .945;
            //}
            //else if ((closingStress <= 30000) && (promedioWidth > .0701) && (promedioWidth <= 0.090))
            //{
            //    _piece = free_gap / .935;
            //}
            //else if ((closingStress >= 30000) && (promedioWidth > 0.0701) && (promedioWidth < 0.090))
            //{
            //    _piece = free_gap / .925;
            //}
            //else if ((promedioWidth > 0.901))
            //{
            //    _piece = free_gap / .86;
            //}
            //return Module.getPieceCompensado(_piece, _elAnillo);
            return 0;
        }
        #endregion

    }
}
