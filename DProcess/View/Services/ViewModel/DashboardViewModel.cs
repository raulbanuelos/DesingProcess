using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace View.Services.ViewModel
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        #region Attributes
        public Usuario ModelUsuario;
        #endregion

        #region Propierties
        #region Propiedades de Usuario
        public string Nombre
        {
            get
            {
                return ModelUsuario.Nombre;
            }
            set
            {
                ModelUsuario.Nombre = value;
                NotifyChange("Nombre");
            }
        }

        public string ApellidoPaterno
        {
            get
            {
                return ModelUsuario.ApellidoPaterno;
            }
            set
            {
                ModelUsuario.ApellidoPaterno = value;
                NotifyChange("ApellidoPaterno");
            }
        }

        public string ApellidoMaterno
        {
            get
            {
                return ModelUsuario.ApellidoMaterno;
            }
            set
            {
                ModelUsuario.ApellidoMaterno = value;
                NotifyChange("ApellidoMaterno");
            }
        }

        public string NombreUsuario
        {
            get
            {
                return ModelUsuario.NombreUsuario;
            }
            set
            {
                ModelUsuario.NombreUsuario = value;
                NotifyChange("NombreUsuario");
            }
        }

        public bool Block
        {
            get
            {
                return ModelUsuario.Block;
            }
            set
            {
                ModelUsuario.Block = value;
                NotifyChange("Block");
            }
        }

        public bool Conectado
        {
            get
            {
                return ModelUsuario.Conectado;
            }
            set
            {
                ModelUsuario.Conectado = value;
                NotifyChange("Conectado");
            }
        }

        public List<Model.Rol> Roles
        {
            get
            {
                return ModelUsuario.Roles;
            }
            set
            {
                ModelUsuario.Roles = value;
                NotifyChange("Roles");
            }
        }

        public UserDetails Details
        {
            get
            {
                return ModelUsuario.Details;
            }
            set
            {
                ModelUsuario.Details = value;
                NotifyChange("Details");
            }
        }
        #endregion

        private Page pagina;
        public Page Pagina
        {
            get { return pagina; }
            set
            {
                pagina = value;
                NotifyChange("Pagina");
            }
        }

        public SeriesCollection Series { get; set; }
        public SeriesCollection SerieLeccionesMensual { get; set; }

        public string[] Labels { get; set; }
        private int _TotalLeccionesAprendidas;
        public int TotalLeccionesAprendidas
        {
            get { return _TotalLeccionesAprendidas; }
            set { _TotalLeccionesAprendidas = value; NotifyChange("TotalLeccionesAprendidas"); }
        }

        private FO_Item _TotalLeccionesMesActual;
        public FO_Item TotalLeccionesMesActual
        {
            get { return _TotalLeccionesMesActual; }
            set { _TotalLeccionesMesActual = value; NotifyChange("TotalLeccionesMesActual"); }
        }

        private int _TotalDocumentos;
        public int TotalDocumentos
        {
            get { return _TotalDocumentos; }
            set { _TotalDocumentos = value; NotifyChange("TotalDocumentos"); }
        }

        private int _TotalDocumentosPorLiberar;
        public int TotalDocumentosPorLiberar
        {
            get { return _TotalDocumentosPorLiberar; }
            set { _TotalDocumentosPorLiberar = value; NotifyChange("TotalDocumentosPorLiberar"); }
        }

        public string[] LabelsDocumento { get; set; }
        public SeriesCollection SeriesCollectionDocumento { get; set; }

        #endregion

        #region Constructor
        public DashboardViewModel(Page _pagina, Usuario modelUsuario)
        {
            ModelUsuario = modelUsuario;
            Pagina = _pagina;

            #region Información Lecciones Aprendidas
            #region Gráfica Motivo Lección aprendida
            Series = new SeriesCollection();
            List<FO_Item> listaMotivoCambio = new List<FO_Item>();
            listaMotivoCambio = DataManagerControlDocumentos.GetMotivoCambioGrafica();

            foreach (var item in listaMotivoCambio)
            {
                PieSeries pieSeries = new PieSeries();

                pieSeries.Title = item.Nombre;
                pieSeries.Values = new ChartValues<ObservableValue> { new ObservableValue(item.Valor) };
                pieSeries.DataLabels = true;

                Series.Add(pieSeries);
            }
            #endregion

            #region Gráfica de cantidad de lecciones aprendidas últimos 5 menses.
            List<FO_Item> listaLeccionesMensual = DataManagerControlDocumentos.GetTotalLeccionesPorMes();

            ColumnSeries columnaSerie = new ColumnSeries();
            columnaSerie.Title = "Cantidad de Lecciones Aprendidas";
            columnaSerie.Values = new ChartValues<double>();
            listaLeccionesMensual.Reverse();
            foreach (FO_Item item in listaLeccionesMensual)
            {
                columnaSerie.Values.Add(item.Valor);

            }
            Labels = new string[listaLeccionesMensual.Count];
            int j = 0;
            foreach (var item in listaLeccionesMensual)
            {
                Labels[j] = item.Nombre;
                j++;
            }

            SerieLeccionesMensual = new SeriesCollection();
            SerieLeccionesMensual.Add(columnaSerie);
            #endregion

            //Obtenermos el total de lecciones aprendidas.
            TotalLeccionesAprendidas = DataManagerControlDocumentos.GetTotalLecciones();

            //Obtenemos el total de lecciones aprendidas en el mes actual.
            TotalLeccionesMesActual = DataManagerControlDocumentos.GetTotalLeccionesMesActual();
            #endregion

            #region Información Control de Documentos
            TotalDocumentos = DataManagerControlDocumentos.GetGridDocumentos(string.Empty).Count;

            TotalDocumentosPorLiberar = DataManagerControlDocumentos.GetDocumentos_PendientesLiberar("").Count;
            
            SeriesCollectionDocumento = new SeriesCollection
            {
                new StackedColumnSeries
                {
                    Title = "Proceso automático",
                    Values = new ChartValues<double> { 4, 5, 6, 8,5 },
                    StackMode = StackMode.Values, // this is not necessary, values is the default stack mode
                    DataLabels = true
                },
                new StackedColumnSeries
                {
                    Title = "Proceso manual",
                    Values = new ChartValues<double> { 2, 5, 6, 7,5 },
                    StackMode = StackMode.Values,
                    DataLabels = true
                },
                
            };

            ////adding series updates and animates the chart
            //SeriesCollectionDocumento.Add(new StackedColumnSeries
            //{
            //    Values = new ChartValues<double> { 6, 2, 7 },
            //    StackMode = StackMode.Values,
            //    DataLabels = true
            //});
            


            LabelsDocumento = Labels;
            #endregion

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
