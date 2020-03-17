using Model;
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
    public class DocumentoFirmadoViewModel : INotifyPropertyChanged
    {

        #region Attributes
        Usuario user;
        #endregion

        private ObservableCollection<Documento> _ListaNumeroDocumento;
        public ObservableCollection<Documento> ListaNumeroDocumento
        {
            get { return _ListaNumeroDocumento; }
            set { _ListaNumeroDocumento = value; NotifyChange("ListaNumeroDocumento"); }
        }


        #region Conected
        public DocumentoFirmadoViewModel(Usuario userConected)
        {
            user = userConected;
            ListaNumeroDocumento = DataManagerControlDocumentos.GetPendientes_Liberar(user.NombreUsuario);
        } 
        #endregion

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
    }
}
