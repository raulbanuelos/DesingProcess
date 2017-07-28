using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.Services.ViewModel
{
    public class BloqueoVM : INotifyPropertyChanged
    {
        #region PropertyChanged
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

        #region Propiedades

        private DateTime _fechaInicio;
        public DateTime FechaInicio {
            get
            {
                return _fechaInicio;
            }
            set
            {
                _fechaInicio = value;
                NotifyChange("FechaInicio");
            }
        }

        private DateTime _fechaFin;
        public DateTime FechaFin
        {
            get
            {
                return _fechaFin;
            }
            set
            {
                _fechaFin = value;
                NotifyChange("FechaFin");
            }
        }

        private string _observaciones;
        public string Observaciones {
            get
            {
                return _observaciones;
            }
            set
            {
                _observaciones = value;
                NotifyChange("Observaciones");
            }
        }
        #endregion

        #region Métodos

        public ICommand GuardarBloqueo
        {
            get
            {
                return new RelayCommand(o => guardar());
            }
        }

        private void guardar()
        {

        }

        public ICommand Modificar
        {
            get
            {
                return new RelayCommand(o=> modificar());
            }
        }

        private void modificar()
        {

        }

        public ICommand Desbloquear
        {
            get
            {
                return new RelayCommand(o => desbloquear());
            }
        }

        private void desbloquear()
        {

        }
        #endregion
    }
}
