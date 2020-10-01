using Frames.UserControl;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Frames.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {

        #region Properties
        private string search;
        public string Search
        {
            get { return search; }
            set { search = value; NotifyChange("Search"); }
        }

        private System.Windows.Controls.UserControl pagina;

        public System.Windows.Controls.UserControl Pagina
        {
            get { return pagina; }
            set { pagina = value; NotifyChange("Pagina"); }
        }

        private ObservableCollection<Documento> _ListaDocumento;

        public ObservableCollection<Documento> ListaDocumento
        {
            get { return _ListaDocumento; }
            set { _ListaDocumento = value; NotifyChange("ListaDocumento"); }
        }

        private ObservableCollection<Documento> _ListaDocumentoOriginal;
        public ObservableCollection<Documento> ListaDocumentoOriginal
        {
            get { return _ListaDocumentoOriginal; }
            set { _ListaDocumentoOriginal = value; NotifyChange("ListaDocumentoOriginal"); }
        }


        #endregion

        #region Commands

        /// <summary>
        /// Comando que al cambiar el textBox, busca un archivo de la lista
        /// Recibe como parámetro la palabra a buscar
        /// </summary>
        public ICommand BuscarDocumentos
        {
            get
            {
                return new RelayCommand(param => changeScreen((string)param));
            }
        }

        #endregion

        #region Methods

        private void changeScreen(string param)
        {
            if (!String.IsNullOrWhiteSpace(param))
            {
                UserControlListDocuments ucListDocuments = new UserControlListDocuments();
                
                List<Documento> lista = ListaDocumentoOriginal.Where(x => x.nombre.ToLower().Contains(param.ToLower()) || x.descripcion.ToLower().Contains(param.ToLower())).ToList();
                
                ListaDocumento = new ObservableCollection<Documento>();

                foreach (var item in lista)
                    ListaDocumento.Add(item);

                ucListDocuments.DataContext = this;

                Pagina = ucListDocuments;
            }
            else
            {
                UserControlTile ucTile = new UserControlTile();
                ucTile.DataContext = this;
                Pagina = ucTile;
            }
                
        }

        #endregion

        #region Constructors

        public MainViewModel()
        {
            Pagina = new UserControlTile();
            
            ListaDocumentoOriginal = DataManagerControlDocumentos.GetGridDocumentos(string.Empty);
            ListaDocumento = ListaDocumentoOriginal;
        }

        #endregion

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

    }
}