using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.ViewModel
{
    public class CuffViewModel : INotifyPropertyChanged
    {
        #region PropiedadesCuffs
        private PropiedadCadena num_cuff;
        public PropiedadCadena no_cuff
        {
            get
            {
                return num_cuff;
            }
            set
            {
                num_cuff = value;
                NotifyChange("no_cuff");
            }
        }
        private Propiedad _dia_ext;
        public Propiedad dia_ext
        {
            get
            {
                return _dia_ext;
            }
            set
            {
                _dia_ext = value;
                NotifyChange("dia_ext");
            }
        }

        private Propiedad _dia_int;
        public Propiedad dia_int
        {
            get
            {
                return _dia_int;
            }
            set
            {
                _dia_int = value;
                NotifyChange("dia_int");
            }
        }
        private Propiedad _largo;
        public Propiedad largo
        {
            get
            {
                return _largo;
            }
            set
            {
                _largo = value;
                NotifyChange("largo");
            }
        }
        private Propiedad _peso;
        public Propiedad peso
        {
            get
            {
                return _peso;
            }
            set
            {
                _peso = value;
                NotifyChange("peso");
            }
        }

        private void NotifyChange(string v)
        {
            throw new NotImplementedException();
        }
        #endregion
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
