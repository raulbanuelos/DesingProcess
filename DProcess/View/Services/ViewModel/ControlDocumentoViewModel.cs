using Model.ControlDocumentos;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using System;
using View.Forms.ControlDocumentos;

namespace View.Services.ViewModel
{
    public class ControlDocumentoViewModel : INotifyPropertyChanged
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

        private ObservableCollection<Documento> _Lista = DataManagerControlDocumentos.GetDataGrid();
        public ObservableCollection<Documento> Lista {
           get
            {
                return _Lista;
            }
           set
            {
                _Lista = value;
                NotifyChange("Lista");
            }
        }

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
        #endregion

        #region Commands
        public ICommand IrNuevoDocumento
        {
            get
            {
                return new RelayCommand(o => irNuevoDocumento());
            }
        }

        private void irNuevoDocumento()
        {
            FrmDocumento frm = new FrmDocumento();
            //DocumentoViewModel context = new DocumentoViewModel("IF00-0093", "2", "2", "Ayuda Visual Especificaciones de abertura de máquina H", 1026);
            DocumentoViewModel context = new DocumentoViewModel();
            frm.DataContext = context;
            frm.ShowDialog();
        }
        #endregion
    }
}
