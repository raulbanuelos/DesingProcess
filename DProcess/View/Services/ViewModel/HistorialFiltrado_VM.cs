using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.Services.ViewModel
{
    class HistorialFiltrado_VM: INotifyPropertyChanged
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

        private DateTime _fechaInicio = DataManagerControlDocumentos.Get_DateTime();
        public DateTime FechaInicio
        {
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

        private DateTime _fechaFin = DataManagerControlDocumentos.Get_DateTime();
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

        private DateTime _dateNow = DataManagerControlDocumentos.Get_DateTime();
        public DateTime DateNow
        {
            get
            {
                return _dateNow;
            }
            set
            {
                _dateNow = value;
                NotifyChange("DateNow");
            }
        }

        private ObservableCollection<string> _ListaEstatus;
        public ObservableCollection<string> ListaEstatus {
            get { return _ListaEstatus; }
            set { _ListaEstatus = value; NotifyChange("ListaEstatus"); }
        }

        private string _SelectedEstatus;
        public string SelectedEstatus
        {
            get { return _SelectedEstatus; }
            set { _SelectedEstatus = value; NotifyChange("SelectedEstatus"); }
        }

        private ObservableCollection<TipoDocumento> _ListaTipo;
        public ObservableCollection<TipoDocumento> ListaTipo
        {
            get { return _ListaTipo; }
            set { _ListaTipo = value; NotifyChange("ListaTipo"); }
        }

        private ObservableCollection<Departamento> _ListaDepartamento;
        public ObservableCollection<Departamento> ListaDepartamento
        {
            get { return _ListaDepartamento; }
            set { _ListaDepartamento = value; NotifyChange("ListaDepartamento"); }
        }

        private ObservableCollection<Documento> _ListaHistorial;
        public ObservableCollection<Documento> ListaHistorial
        {
            get { return _ListaHistorial; }
            set { _ListaHistorial = value; NotifyChange("ListaHistorial"); }
        }

        private TipoDocumento _SelectedTipo;
        public TipoDocumento SelectedTipo {
            get { return _SelectedTipo; }
            set { _SelectedTipo = value; NotifyChange("SelectedTipo"); }
        }

        private Departamento _SelectedDepartamento;
        public Departamento SelectedDepartamento {
            get { return _SelectedDepartamento; }
            set { _SelectedDepartamento = value; NotifyChange("SelectedDepartamento"); }
        }
        #endregion

        #region Commands

        /// <summary>
        /// Comando que cambie la fecha final, cuando se cambie la fecha de Inicio
        /// </summary>
        public ICommand CambiarFecha
        {
            get
            {
                return new RelayCommand(o => cambiaFecha());
            }
        }

        public ICommand FiltrarEstatus
        {
            get
            {
                return new RelayCommand(o => filtrar());
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Método que asigna la fecha de inicio que se seleccionó a la fecha final
        /// </summary>
        private void cambiaFecha()
        {
            FechaFin = FechaInicio;
        }

        private void filtrar()
        {
            if (ValidaCampos())
            {
               
                ListaHistorial = DataManagerControlDocumentos.GetHistorialDocumentos(FechaInicio,FechaFin,SelectedEstatus,0,0);
            }
        }

        private void InicializaCampos()
        {
            ListaDepartamento = DataManagerControlDocumentos.GetDepartamento();
            ListaTipo = DataManagerControlDocumentos.GetTipo();
            ListaEstatus = new ObservableCollection<string>();
            ListaEstatus.Add("LIBERADO");           
            ListaEstatus.Add("APROBADO");
            ListaEstatus.Add("OBSOLETO");
            ListaEstatus.Add("PENDIENTE POR CORREGIR");
            ListaEstatus.Add("PENDIENTE POR LIBERAR");
            ListaEstatus.Add("PENDIENTE POR APROBACIÓN");
        }

        private bool ValidaCampos()
        {
            if (SelectedEstatus != null & _fechaInicio != null & _fechaFin != null)
                return true;
            else
                return false;
                
        }
        #endregion

        #region Constructor

        public HistorialFiltrado_VM()
        {
            InicializaCampos();
        }
        #endregion
    }
}
