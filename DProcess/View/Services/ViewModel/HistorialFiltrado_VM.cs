using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Resources;

namespace View.Services.ViewModel
{
    class HistorialFiltrado_VM : INotifyPropertyChanged
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

        private DateTime _fechaInicioChart = DataManagerControlDocumentos.Get_DateTime();
        public DateTime FechaInicioChart
        {
            get
            {
                return _fechaInicioChart;
            }
            set
            {
                _fechaInicioChart = value;
                NotifyChange("FechaInicioChart");
            }
        }

        private DateTime _fechaFinChart = DataManagerControlDocumentos.Get_DateTime();
        public DateTime FechaFinChart
        {
            get
            {
                return _fechaFinChart;
            }
            set
            {
                _fechaFinChart = value;
                NotifyChange("FechaFinChart");
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

        //Variable para el control de la gráfica.
        private string _SelectedEstatus_Chart;
        public string SelectedEstatusChart
        {
            get { return _SelectedEstatus_Chart; }
            set { _SelectedEstatus_Chart = value; NotifyChange("SelectedEstatusChart"); }
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

        private ObservableCollection<HistorialVersion> _ListaCount;
        public ObservableCollection<HistorialVersion> ListaCount {
            get { return _ListaCount; }
            set { _ListaCount = value; NotifyChange("ListaCount"); }
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

        private int id_tipo;
        public int ID_Tipo {
            get { return id_tipo; }
            set { id_tipo = value; NotifyChange("ID_Tipo"); }
        }

        //Variable para el control de filtrado en gráfica.
        private int id_tipoChart;
        public int IDTipo_Chart
        {
            get { return id_tipoChart; }
            set { id_tipoChart = value; NotifyChange("IDTipo_Chart"); }
        }

        //Variable para el control de la gráfica.
        private int id_depChart;
        public int IdDep_Chart
        {
            get { return id_depChart; }
            set { id_depChart = value; NotifyChange("IdDep_Chart"); }
        }

        private int id_dep;
        public int ID_dep
        {
            get { return id_dep; }
            set { id_dep = value; NotifyChange("ID_dep"); }
        }

        private ObservableCollection<KeyValuePair<string, int>> _ListaGrafica;
        public ObservableCollection<KeyValuePair<string, int>> ListaGrafica {
            get
            {
                return _ListaGrafica;
            }
            set
            {
                _ListaGrafica = value;
                NotifyChange("ListaGrafica");
            }
        }

        DialogService dialog;
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

        /// <summary>
        /// Comando que cambia la fecha final de la gráfica.
        /// </summary>
        public ICommand CambiarFechaChart
        {
            get
            {
                return new RelayCommand(o => cambiaFechaChart());
            }
        }

        /// <summary>
        /// Comando que filtra la tabla.
        /// </summary>
        public ICommand FiltrarEstatus
        {
            get
            {
                return new RelayCommand(o => filtrar());
            }
        }

        /// <summary>
        /// Comando que filtra la gráfica.
        /// </summary>
        public ICommand FiltrarGrafica
        {
            get
            {
                return new RelayCommand(o => filtrarGrafica());
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Método que asigna la fecha de inicio que se seleccionó, a la fecha final.
        /// </summary>
        private void cambiaFecha()
        {
            FechaFin = FechaInicio;
        }

        /// <summary>
        /// Método que asigna la fecha de inicio a la fecha final.
        /// </summary>
        private void cambiaFechaChart()
        {
            FechaFinChart = FechaInicioChart;
        }

        /// <summary>
        /// Método que filtra la información para la tabla.
        /// </summary>
        private async void filtrar()
        {
            //Valída las fechas.
            if (ComparaFechaTabla()) {
                //Si no se ha seleccionado el estado.
                if (SelectedEstatus != null)
                {
                    if (ValidaCampos())
                    {
                        //Obtiene la información de los documetnos filtrados, el resultado lo asigna a la lista.
                        ListaHistorial = DataManagerControlDocumentos.GetHistorialDocumentos(FechaInicio, FechaFin, SelectedEstatus, id_dep, id_tipo);
                    }
                    else
                    {
                        //Muestra en pantalla el msenaje.
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSeleccionarEstatus);
                    }
                }
            }
            else
            {
                //Muestra mensaje en pantalla.
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgRangoFechasInvalido);
            }
        }

        /// <summary>
        /// Método que filtra los documentos y muestra la gráfica.
        /// </summary>
        private async void filtrarGrafica()
        {
            //Limpia la lista.
            _ListaGrafica.Clear();
          
            //Compara las fecha.
            if (ComparaFechaGrafica()) {
                //Si se ha seleccionado el estado. 
                if (SelectedEstatusChart != null)
                {
                    //Obtiene los documentos de acuerdo a los parámetros, se asigna a la lista.
                    ListaCount = DataManagerControlDocumentos.GetCountDocumentos(FechaInicioChart, FechaFinChart, SelectedEstatusChart, id_depChart, id_tipoChart);

                    //Si la lista no se encuentra vacía.
                    if (ListaCount.Count > 0)
                    {
                        //Iteramos los objetos de la lista         
                        foreach (var item in ListaCount)
                        {
                            //Agrega a la gráfica los valores de fecha y cantidad de cada item.
                            _ListaGrafica.Add(new KeyValuePair<string, int>(item.fecha.ToShortDateString(), item.cantidad));
                        }
                    }
                }
            }
            else
            {
                //Muestra mensaje en pantalla.
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgRangoFechasInvalido);
            }
        }

        /// <summary>
        /// Método que inicializa las listas.
        /// </summary>
        private void InicializaCampos()
        {
            //Objetos de valor nulo.
            Departamento dep = new Departamento();
            TipoDocumento tipo = new TipoDocumento();
            dep.id_dep = 0;
            dep.nombre_dep = "NINGUNO";
            tipo.id_tipo = 0;
            tipo.tipo_documento = "NINGUNO";
           
            //Método que obtiene la lista de los departamentos.   
            ListaDepartamento = DataManagerControlDocumentos.GetDepartamento();
            //Agrega el objeto  nulo.
            ListaDepartamento.Add(dep);

            //Método que obtiene la lista de tipo de documentos.
            ListaTipo = DataManagerControlDocumentos.GetTipo();
            //Agrega el objeto nulo.
            ListaTipo.Add(tipo);
            ListaEstatus = new ObservableCollection<string>();
            ListaEstatus.Add("LIBERADO");           
            ListaEstatus.Add("OBSOLETO");
            ListaEstatus.Add("PENDIENTE POR CORREGIR");
            ListaEstatus.Add("PENDIENTE POR LIBERAR");
            ListaEstatus.Add("PENDIENTE POR APROBACIÓN");
        }

        /// <summary>
        /// Método que valída los campos.
        /// </summary>
        /// <returns></returns>
        private bool ValidaCampos()
        {
            //Si los combobox no se han seleccionado.
            if (SelectedEstatus != null & _fechaInicio != null & _fechaFin != null)
                return true;
            else
                return false;
                
        }

        /// <summary>
        /// Método que compara las fechas para el filtrado de la gráfica.
        /// </summary>
        /// <returns></returns>
        private bool ComparaFechaGrafica()
        {
            //Retorna false si la fecha de inicio es mayor a la fecha final.
            if (FechaInicioChart.CompareTo(FechaFinChart) == 1)
                return false;
            else
                //Si las fechas son iguales, o la fecha de incio es menor a la final.
                return true; 
        }

        /// <summary>
        /// Método que compara las fechas para el filtrado de la tabla.
        /// </summary>
        /// <returns></returns>
        private bool ComparaFechaTabla()
        {
            //Retorna false si la fecha de inicio es mayor a la fecha final.
            if (FechaInicio.CompareTo(FechaFin) == 1)
                return true;
            else
                //Si las fechas son iguales, o la fecha de incio es menor a la final.
                return false;
        }
        #endregion

        #region Constructor

        public HistorialFiltrado_VM()
        {
            ListaDepartamento = new ObservableCollection<Departamento>();
            ListaTipo = new ObservableCollection<TipoDocumento>();
            InicializaCampos();
            dialog = new DialogService();
            ListaGrafica = new ObservableCollection<KeyValuePair<string, int>>();
        }
        #endregion
    }
}
