using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services
{
    public class DibujaPattern
    {
        public double BDia;
        public double PattSMDO;
        public double FactorK;
        public double PieceReal;
        public double WidthPattern;
        public double AnguloSalida;
        public double ThicknessPlaca;
        public double DiaHerramienta;
        public double CompensacionFactorK;


        public double _bdia;
        public double _pattSMOD;
        public double _ValorCompensadoFactorK;
        public double _FactorK;
        public double _diaHerramienta;


        public DibujaPattern(double bDia, double pattSMOD, double factorK, double pieceReal, double widthPattern, double anguloSalida, double thicknessPlaca, double diaHerramienta, double compensacionFactorK)
        {
            BDia = bDia;
            PattSMDO = pattSMOD;
            FactorK = factorK;
            PieceReal = pieceReal;
            WidthPattern = widthPattern;
            AnguloSalida = anguloSalida;
            ThicknessPlaca = thicknessPlaca;
            DiaHerramienta = diaHerramienta;
            CompensacionFactorK = compensacionFactorK;

            compensaciones();
        }

        
        public bool Auditoria()
        {
            double freeGap = PieceReal * 24.4;
            double rise,bRequerido,pattSMRadioRequerido,pattSMRadioCalculado;

            rise = _pattSMOD > _bdia ? Math.Round(((PieceReal * _FactorK * 64) - 0.005),3) : Math.Round(((PieceReal * _FactorK * 64) + 0.005),3);
            bRequerido = Math.Round(((_bdia) * 25.4) / 2, 4);
            pattSMRadioRequerido = Math.Round((_ValorCompensadoFactorK / 2) * 25.4, 4);
            //pattSMRadioCalculado = 

            
            return true;
        }
        
        private void compensaciones()
        {
            if (WidthPattern < 0.39)
            {
                _bdia = BDia;
                _pattSMOD = PattSMDO;
            }
            else
            {
                _bdia = BDia + ((Math.Tan(AnguloSalida / 57.29)) * WidthPattern);
                _pattSMOD = PattSMDO + ((Math.Tan(AnguloSalida / 57.29)) * WidthPattern);
            }

            _ValorCompensadoFactorK = _pattSMOD - CompensacionFactorK;
            
            _FactorK = PattSMDO > BDia ? FactorK * 10 : FactorK;


            if (WidthPattern < 0.39)
            {
                _diaHerramienta = DiaHerramienta + (((WidthPattern / 2 + 0.003) * (Math.Tan(AnguloSalida / 57.29))) * 2);
            }
            else
            {
                _diaHerramienta = DiaHerramienta + (((WidthPattern + 0.003) * (Math.Tan(AnguloSalida / 57.29))) * 2);
            }

            _diaHerramienta = Math.Round(_diaHerramienta, 5);
        }
        
    }
}
