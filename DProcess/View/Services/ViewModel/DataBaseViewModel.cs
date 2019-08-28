using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.Routing;

namespace View.Services.ViewModel
{
    public class DataBaseViewModel : INotifyPropertyChanged
    {
        #region Commands

        public ICommand IrPropiedades
        {
            get
            {
                return new RelayCommand(o => irPropiedades());
            }
        }

        public ICommand IrPropiedadesBool
        {
            get
            {
                return new RelayCommand(a => irPropiedadesBool());
            }
        }

        public ICommand IrPropiedadesCadena
        {
            get
            {
                return new RelayCommand(a => irPropiedadesCadena());
            }
        }

        public ICommand IrAllPerfil
        {
            get
            {
                return new RelayCommand(a => irAllPerfil());
            }
        }

        #endregion

        #region Métodos de navegación

        private void irPropiedades()
        {
            WPropiedadesNumeric ventana = new WPropiedadesNumeric();

            PropiedadViewModel context = new PropiedadViewModel(true);

            ventana.DataContext = context;

            ventana.ShowDialog();
        }

        public void irPropiedadesBool()
        {
            WPropiedadesBool Form = new WPropiedadesBool();

            PropiedadBoolViewModel context = new PropiedadBoolViewModel(true);

            Form.DataContext = context;

            Form.ShowDialog();
        }

        private void irPropiedadesCadena()
        {
            WPropiedadesCadena Form = new WPropiedadesCadena();

            PropiedadCadenaViewModel context = new PropiedadCadenaViewModel(true);

            Form.DataContext = context;

            Form.ShowDialog();
        }

        private void irAllPerfil()
        {
            WViewAllPerfiles ventana = new WViewAllPerfiles();

            PerfilViewModel vmPerfil = new PerfilViewModel();

            ventana.DataContext = vmPerfil;

            ventana.ShowDialog();
        }

        #endregion

        #region INotifyPropertyChanged Métodos

        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }

        #endregion

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }
}
