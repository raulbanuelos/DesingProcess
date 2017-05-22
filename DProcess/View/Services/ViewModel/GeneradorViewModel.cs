using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.ViewModel
{
    class GeneradorViewModel : INotifyPropertyChanged
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

        private ObservableCollection<TipoDocumento> _ListaTipoDocumento = DataManagerControlDocumentos.GetTipo();
        public ObservableCollection<TipoDocumento> ListaTipoDocumento
        {
            get
            {
                return _ListaTipoDocumento;
            }
            set
            {
                _ListaTipoDocumento = value;
                NotifyChange("ListaTipoDocumento");
            }
        }

        private ObservableCollection<Departamento> _ListaDepartamento = DataManagerControlDocumentos.GetDepartamento();
        public ObservableCollection<Departamento> ListaDepartamento
        {
            get
            {
                return _ListaDepartamento;
            }
            set
            {
                _ListaDepartamento = value;
                NotifyChange("ListaDepartamento");
            }
        }

        private TipoDocumento selectedTipoDocumento;
        public TipoDocumento SelectedTipoDocumento
        {
            get
            {
                return selectedTipoDocumento;
            }
            set
            {
                selectedTipoDocumento = value;
                NotifyChange("SelectedTipoDocumento");
            }
        }

        private Departamento selectedDepartamento;
        public Departamento SelectedDepartamento {
            get
            {
                return selectedDepartamento;
            }
            set
            {
                selectedDepartamento = value;
                NotifyChange("SelectedDepartamento");
            }
        }
        #endregion

        #region Commands

        
        #endregion
    }
}
