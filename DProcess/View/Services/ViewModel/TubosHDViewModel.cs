using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.ViewModel
{
    class TubosHDViewModel : INotifyPropertyChanged
    {
        #region PropiedadesTubosCL 
        private PropiedadCadena _Tubo;
        public PropiedadCadena Tubo
        {
            get
            {
                return _Tubo;
            }
            set
            {
                _Tubo = value;
                NotifyChange("Tubo");
            }
        }
        private Propiedad _DiaExt;
        public Propiedad DiaExt
        {
            get
            {
                return _DiaExt;
            }
            set
            {
                _DiaExt = value;
                NotifyChange("DiaExt");
            }
        }

        private Propiedad _DiaInt;
        public Propiedad DiaInt
        {
            get
            {
                return _DiaInt;
            }
            set
            {
                _DiaInt = value;
                NotifyChange("DiaInt");
            }
        }
        private Propiedad _Thinckness;
        public Propiedad Thickness
        {
            get
            {
                return _Thinckness;
            }
            set
            {
                _Thinckness = value;
                NotifyChange("Thinckness");
            }
        }
        private Propiedad _Largo;
        public Propiedad Largo
        {
            get
            {
                return _Largo;
            }
            set
            {
                _Largo = value;
                NotifyChange("Largo");
            }
        }
        private PropiedadCadena _Molde;
        public PropiedadCadena Molde
        {
            get
            {
                return _Molde;
            }
            set
            {
                _Molde = value;
                NotifyChange("Molde");
            }
        }
        private PropiedadCadena _RPM;
        public PropiedadCadena RPM
        {
            get
            {
                return _RPM;
            }
            set
            {
                _RPM = value;
                NotifyChange("RPM");
            }
        }

        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
