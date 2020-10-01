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
using System.Windows.Input;
using View.Forms.Index;
using View.Forms.Shared;
using System.Windows;
using MahApps.Metro.Controls;

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
        public SeriesCollection SerieDocumentosLiberados { get; set; }
        public SeriesCollection SeriesLeccionesUsuario { get; set; }

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

        private int _TotalDocumentosVencidos;
        public int TotalDocumentosVencidos
        {
            get { return _TotalDocumentosVencidos; }
            set { _TotalDocumentosVencidos = value; NotifyChange("TotalDocumentosVencidos"); }
        }

        private double _ValueAngularDoc;
        public double ValueAngularDoc
        {
            get { return _ValueAngularDoc; }
            set { _ValueAngularDoc = value; NotifyChange("ValueAngularDoc"); }
        }

        private int _TotalDocumentosPorRevisar;
        public int TotalDocumentosPorRevisar
        {
            get { return _TotalDocumentosPorRevisar; }
            set { _TotalDocumentosPorRevisar = value; NotifyChange("TotalDocumentosPorRevisar"); }
        }


        public string[] LabelsDocumentoLiberados { get; set; }


        #endregion

        #region Commands
        public ICommand IrHome
        {
            get
            {
                return new RelayCommand(o => irHome());
            }
        }
        
        #endregion

        #region Methods
        private void irHome()
        {

            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            //Cerramos la pantalla.
            window.Close();

            Home PantallaHome = new Home(ModelUsuario.NombreUsuario);

            //Creamos un objeto UsuarioViewModel, y le asignamos los valores correspondientes, a la propiedad Pagina se le asigna la pantalla inicial de Home.
            UsuarioViewModel context = new UsuarioViewModel(ModelUsuario, PantallaHome);
            context.ModelUsuario = ModelUsuario;

            //NOTA IMPORTANTE: Se hizo una redundancia al asignarle en la propiedad página su misma pantalla. Solo es por ser la primeva vez y tenernos en donde descanzar la primera pantalla.
            context.Pagina = PantallaHome;

            //Asignamos al DataContext de la PantallaHome el context creado anteriormente.
            PantallaHome.DataContext = context;

            //Declaramos la pantalla en la que descanzan todas las páginas.
            Layout masterPage = new Layout();

            //Asingamos el DataContext.
            masterPage.DataContext = context;

            //Ejecutamos el método el cual despliega la pantalla.
            masterPage.ShowDialog();
        }
        #endregion

        #region Constructor
        public DashboardViewModel(Usuario modelUsuario)
        {
            ModelUsuario = modelUsuario;
            
            #region Información Lecciones Aprendidas
            #region Gráfica Motivo Lección aprendida
            Series = new SeriesCollection();
            List<FO_Item> listaMotivoCambio = new List<FO_Item>();
            listaMotivoCambio = DataManagerControlDocumentos.GetMotivoCambioGrafica();

            foreach (var item7 in listaMotivoCambio)
            {
                PieSeries pieSeries = new PieSeries();

                pieSeries.Title = item7.Nombre;
                pieSeries.Values = new ChartValues<ObservableValue> { new ObservableValue(item7.Valor) };
                pieSeries.DataLabels = true;

                Series.Add(pieSeries);
            }
            #endregion

            #endregion

            #region Gráfica de cantidad de lecciones aprendidas últimos 5 meses.
            List<FO_Item> listaLeccionesMensual = DataManagerControlDocumentos.GetTotalLeccionesPorMes();

            ColumnSeries columnaSerie = new ColumnSeries();
            columnaSerie.Title = "Cantidad de Lecciones Aprendidas";
            columnaSerie.Values = new ChartValues<double>();
            listaLeccionesMensual.Reverse();
            foreach (FO_Item item1 in listaLeccionesMensual)
            {
                columnaSerie.Values.Add(item1.Valor);

            }
            Labels = new string[listaLeccionesMensual.Count];
            int j = 0;
            foreach (var item2 in listaLeccionesMensual)
            {
                Labels[j] = item2.Nombre;
                j++;
            }

            SerieLeccionesMensual = new SeriesCollection();
            SerieLeccionesMensual.Add(columnaSerie);
            #endregion

            //Obtenermos el total de lecciones aprendidas.
            TotalLeccionesAprendidas = DataManagerControlDocumentos.GetTotalLecciones();

            //Obtenemos el total de lecciones aprendidas en el mes actual.
            TotalLeccionesMesActual = DataManagerControlDocumentos.GetTotalLeccionesMesActual();

            #region Lecciones aprendidas por usuario
            SeriesLeccionesUsuario = new SeriesCollection();
            List<FO_Item> listaLeccionesUsuario = new List<FO_Item>();
            listaLeccionesUsuario = DataManagerControlDocumentos.GetCantidadLeccionesAprendidasByUsuario();

            foreach (var item7 in listaLeccionesUsuario)
            {
                PieSeries pieSeries = new PieSeries();

                pieSeries.Title = item7.Nombre;
                pieSeries.Values = new ChartValues<ObservableValue> { new ObservableValue(item7.Valor) };
                pieSeries.DataLabels = true;

                SeriesLeccionesUsuario.Add(pieSeries);
            } 
            #endregion
            #endregion

            #region Información Control de Documentos
            TotalDocumentos = DataManagerControlDocumentos.GetGridDocumentos(string.Empty).Count;

            TotalDocumentosPorLiberar = DataManagerControlDocumentos.GetDocumentos_PendientesLiberar("").Count;

            TotalDocumentosVencidos = DataManagerControlDocumentos.GetDocumentosAprobadosNoRecibidos(true).Count;

            TotalDocumentosPorRevisar = DataManagerControlDocumentos.GetDocumentosValidar("", "").Count;

            if (TotalDocumentosVencidos == 0 && TotalDocumentosPorLiberar == 0)
            {
                ValueAngularDoc = 100;
            }
            else
            {
                ValueAngularDoc = 100 - ((Convert.ToDouble(TotalDocumentosVencidos) / Convert.ToDouble(TotalDocumentosPorLiberar)) * 100);
            }
            
            SerieDocumentosLiberados = new SeriesCollection();
            
            StackedColumnSeries columnaManual = new StackedColumnSeries();
            columnaManual.StackMode = StackMode.Values;
            columnaManual.DataLabels = true;
            columnaManual.Title = "MANUAL";

            StackedColumnSeries columnaAutomatico = new StackedColumnSeries();
            columnaAutomatico.StackMode = StackMode.Values;
            columnaAutomatico.DataLabels = true;
            columnaAutomatico.Title = "AUTOMÁTICO";
            
            int[] valores = new int[] { 3, 2, 1, 0, -1 };

            int[] valoresManual = new int[5];
            int[] valoresAutomatico = new int[5];
            LabelsDocumentoLiberados = new string[valoresAutomatico.Length];

            j = 0;
            foreach (var item2 in valores)
            {
                FO_Item foItem = DataManagerControlDocumentos.GetCantidadDocumentoLiberado("MANUAL", item2);
                valoresManual[j] = Convert.ToInt32(foItem.Valor);
                LabelsDocumentoLiberados[j] = foItem.Nombre;
                j++;
            }

            j = 0;
            foreach (var item2 in valores)
            {
                FO_Item foItem = DataManagerControlDocumentos.GetCantidadDocumentoLiberado("AUTOMATICO", item2);
                valoresAutomatico[j] = Convert.ToInt32(foItem.Valor);
                j++;
            }

            columnaManual.Values = new ChartValues<int>(valoresManual);

            columnaAutomatico.Values = new ChartValues<int>(valoresAutomatico);
            
            SerieDocumentosLiberados.Add(columnaManual);
            SerieDocumentosLiberados.Add(columnaAutomatico);

            
            #endregion

        }
        

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
