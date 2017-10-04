using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MahApps.Metro.Controls;

namespace View.Services.ViewModel
{
    class Busqueda_HerramentalVM : INotifyPropertyChanged
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

        #region Atributtes
        DialogService dialog = new DialogService();
        #endregion

        #region Propiedades

        private ObservableCollection<ClasificacionHerramental> _ListaClasificacion;
        public ObservableCollection<ClasificacionHerramental> ListaClasificacion
        {
            get
            {
                return _ListaClasificacion;
            }
            set
            {
                _ListaClasificacion = value;
                NotifyChange("ListaClasificacion");
            }
        }

        private ClasificacionHerramental _SelectedClasificacion;
        public ClasificacionHerramental SelectedClasificacion
        {
            get
            {
                return _SelectedClasificacion;
            }
            set
            {
                _SelectedClasificacion = value;
                NotifyChange("SelectedClasificacion");
            }
        }
        #endregion

        #region Commands

        /// <summary>
        /// Filtra la tabla por la palabra 
        /// </summary>
        public ICommand Buscar
        {
            get
            {
                return new RelayCommand(param => buscarHerramental((string)param));
            }
        }

        /// <summary>
        /// Comando que selecciona el herramental 
        /// </summary>
        public ICommand SelectHerramental
        {
            get
            {
                return new RelayCommand(o => selectHerramental());
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Método que busca las coincidencias de la clasifiacion de herramenl
        /// </summary>
        /// <param name="texto"></param>
        private void buscarHerramental(string texto)
        {
            //Obtiene la lista de clasificacion herramental
            ListaClasificacion = DataManager.GetClasificacionHerramental(texto);
        }

        /// <summary>
        /// Método que se ejecuta cuando se selecciona un herramental
        /// </summary>
        private void selectHerramental()
        {
            //Si se selecciono un herramental
            if (SelectedClasificacion != null)
            {
                var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                //Verificamos que la pantalla sea diferente de nulo.
                if (window != null)
                {
                    //Cerramos la pantalla
                    window.Close();
                }
            }
         }
        #endregion

        #region Constructor
        public Busqueda_HerramentalVM()
        {
            ListaClasificacion = DataManager.GetClasificacionHerramental(string.Empty);
        }
        #endregion
    }
}
