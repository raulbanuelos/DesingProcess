using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.Services.ViewModel
{
    public class CalculoFreeGapViewModel : INotifyPropertyChanged
    {

        #region Events INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region Constructors
        public CalculoFreeGapViewModel()
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="d1">Diámetro nominal Millimeter</param>
        /// <param name="h1">Width del anillo Millimeter</param>
        /// <param name="a1">Thickness del anillo Millimeter</param>
        /// <param name="s1">Gap del anillo</param>
        /// <param name="material">Espec material</param>
        /// <param name="ft">Tension N</param>
        /// <param name="ov">Ovalidad</param>
        public CalculoFreeGapViewModel(double d1, double h1, double a1, double s1, string material, double ft, double ov)
        {
            D1 = d1;
            H1 = h1;
            A1 = a1;
            S1 = s1;
            Material = material;
            FT = ft;
            OV = ov;
        } 
        #endregion

        #region Properties

        #region Properties Input
        private double _d1;
        public double D1
        {
            get { return _d1; }
            set { _d1 = value; NotifyChange("D1"); }
        }

        private double _h1;
        public double H1
        {
            get { return _h1; }
            set { _h1 = value; NotifyChange("H1"); }
        }

        private double _a1;
        public double A1
        {
            get { return _a1; }
            set { _a1 = value; NotifyChange("A1"); }
        }

        private double _s1;
        public double S1
        {
            get { return _s1; }
            set { _s1 = value; NotifyChange("S1"); }
        }

        private string _material;
        public string Material
        {
            get { return _material; }
            set { _material = value; NotifyChange("Material"); }
        }

        private double _ft;
        public double FT
        {
            get { return _ft; }
            set { _ft = value; NotifyChange("FT"); }
        }

        private double _ov;
        public double OV
        {
            get { return _ov; }
            set { _ov = value; NotifyChange("OV"); }
        }
        #endregion

        #region Properties output

        private double _tx;
        public double TX
        {
            get { return _tx; }
            set { _tx = value; NotifyChange("TX"); }
        }

        private double _ly;
        public double LY
        {
            get { return _ly; }
            set { _ly = value; NotifyChange("LY"); }
        }

        private double _e;
        public double E
        {
            get { return _e; }
            set { _e = value; NotifyChange("E"); }
        }

        private double _k;
        public double K
        {
            get { return _k; }
            set { _k = value; NotifyChange("K"); }
        }

        private double _FTOutput;
        public double FTOutput
        {
            get { return _FTOutput; }
            set { _FTOutput = value; NotifyChange("FTOutput"); }
        }

        private double _fd;
        public double FD
        {
            get { return _fd; }
            set { _fd = value; NotifyChange("FD"); }
        }

        private double _ovOutput;
        public double OVOutput
        {
            get { return _ovOutput; }
            set { _ovOutput = value; NotifyChange("OVOutput"); }
        }

        private double _m;
        public double M
        {
            get { return _m; }
            set { _m = value; NotifyChange("M"); }
        }

        private double _freeGapInch;
        public double FreeGapInch
        {
            get { return _freeGapInch; }
            set { _freeGapInch = value; NotifyChange("FreeGapInch"); }
        }
        
        private double _p;
        public double P
        {
            get { return _p; }
            set { _p = value; NotifyChange("P"); }
        }

        private double _z;
        public double Z
        {
            get { return _z; }
            set { _z = value; NotifyChange("Z"); }
        }

        private double _fov;
        public double FOv
        {
            get { return _fov; }
            set { _fov = value; NotifyChange("FOv"); }
        }

        private double _fab;
        public double FAB
        {
            get { return _fab; }
            set { _fab = value; NotifyChange("FAB"); }
        }

        private double _old;

        public double OLD
        {
            get { return _old; }
            set { _old = value;  NotifyChange("OLD"); }
        }

        #endregion
        
        #endregion

        #region Methods
        public void calcular()
        {
            if (errores())
            {
                TX = Math.Round(A1 / 2, 3);
                LY = Math.Round(H1 * (Math.Pow(A1, 3)) / 12, 3);
                E = GetMaterialProperties(Material);
                K = (FT * Math.Pow((D1 - 2 * TX), 2)) / (4 * E * LY);
                FTOutput =  4 * K * E * LY / Math.Pow((D1 - (2 * TX)), 2);
                FD = Math.Round(2 * FTOutput, 3);
                M = Math.Round(4.76 * K * (D1 - 2 * TX) - 1.25 * OV + S1 - 0.004 * (D1 - 2 * TX), 3);
                FreeGapInch = Math.Round(M / 25.4, 3);
                P = Math.Round((2 * FTOutput) / (D1 * H1), 3);
                Z = Math.Round((18 / (Math.PI + 4)) * (OV / (K * (D1 - 2 * TX))), 3);
                FOv = Math.Round(OV / (D1 - 2 * TX) * 100, 3);
                FAB = Math.Round(M / (D1 - 2 * TX) * 100, 3);
                OLD = Math.Round(D1 * (0.998 + 1.86 * K) - 0.986 * OV, 3);
            }
            else
            {
                //Mandar mensaje de error.
            }
            
        }

        private bool errores()
        {
            if (string.IsNullOrEmpty(Material))
                return false;


            return true;
        }

        private double GetMaterialProperties(string especMaterial)
        {
            if (especMaterial.StartsWith("MF012"))
                return 95000;
            
            if (especMaterial.StartsWith("MF017"))
                return 120000;

            if (especMaterial.StartsWith("MF025"))
                return 130000;

            if (especMaterial.StartsWith("MF032"))
                return 140000;

            if (especMaterial.StartsWith("MF053") || especMaterial.StartsWith("MF056"))
                return 160000;

            if (especMaterial.StartsWith("MS064"))
                return 210000;

            if (especMaterial.StartsWith("MS066"))
                return 210000;

            return 0;
        }
        #endregion

        #region Commands
        public ICommand Calcular
        {
            get
            {
                return new RelayCommand(o => calcular());
            }
        } 
        #endregion

    }
}
