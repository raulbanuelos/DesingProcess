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
using System.Windows.Forms;
using System.Windows.Input;
using View.Forms.Cotizaciones;
using View.Resources;

namespace View.Services.ViewModel
{
    public class CrearCotizacionViewModel : INotifyPropertyChanged
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
        private ObservableCollection<CentrosTrabajo> _ListaCentroTrabajo;
        public ObservableCollection<CentrosTrabajo> ListaCentroTrabajo
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
        private CentrosTrabajo selectedTipoCentroTrabajo; 
        public CentrosTrabajo SelectedTipoCentroTrabajo
        {
            get
            {
                return selectedTipoCentroTrabajo;
            }
            set
            {
                selectedTipoCentroTrabajo = value;
                NotifyChange("SelectedTipoCentroTrabajo");
            }

        }
        private List<string> _Lista;
        public List<string> Lista
        {
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
        #endregion

        #region Constructors
        public CrearCotizacionViewModel()
        {
            SelectedTipoCentroTrabajo = new CentrosTrabajo();
            agregarCentroTrabajo();
            
        }
        #endregion

        #region Commands
        public ICommand IrListaCentroTrabajo
        {
            get
            {
                return new RelayCommand(o => irListaCentroTrabajo());
            }
        }
        public ICommand Ir
        {
            get
            {
                return new RelayCommand(param => irListaCentroTrabajo());
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// /Método el cual nos envía a la pantalla de Lista de Centros de trabajo, insertar los valores en la lista, validar los botones de "Aceptar" y "Cancelar"
        /// </summary>
        private void irListaCentroTrabajo()
        {
            FrmListaCentroTrabajo frm = new FrmListaCentroTrabajo();
            ListaCentroTrabajoViewModel context = new ListaCentroTrabajoViewModel();
            frm.DataContext = context;
            frm.ShowDialog();
            if (frm.DialogResult.HasValue && frm.DialogResult.Value)
            {
                Lista = context.ListaCentroTrabajo;
                FrmVistaWPF vista = new FrmVistaWPF();
                vista.DataContext = this;
                vista.ShowDialog();
            }
            
        }
        /// <summary>
        /// Método que obtiene los Centros de trabajo y los nombres de operación
        /// </summary>
        private void agregarCentroTrabajo()
        {
            _ListaCentroTrabajo = DataManagerControlDocumentos.GetCentroTrabajo("");
        }
        #endregion
    }
}
