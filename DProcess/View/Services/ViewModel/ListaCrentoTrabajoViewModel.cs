using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using View.Forms.Cotizaciones;
using View.Resources;

namespace View.Services.ViewModel
{
    class ListaCentroTrabajoViewModel : INotifyPropertyChanged
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
        private List<string> _ListaCentroTrabajo;
        public List<string> ListaCentroTrabajo
        {
            get
            {
                return _ListaCentroTrabajo;
            }
            set
            {
                _ListaCentroTrabajo = value;
                NotifyChange("ListaCentroTrabajo");
            }
        }

        private String _texto;
        public String texto
        {
            get
            {
                return _texto;
            }
            set
            {
                _texto = value;
                NotifyChange("texto");
            }
        }
        #endregion

        #region Constructors
        #endregion

        #region Commands
        public ICommand Aceptar
        {
            get
            {
                return new RelayCommand(param => CrearListaCretroTrabajo());
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Método para guardar en la lista los Centro de Trabajo escritos en el TextBox
        /// </summary>
        private void CrearListaCretroTrabajo()
        {
            ListaCentroTrabajo = texto.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        #endregion
    }
}
