using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.ControlDocumentos;
using Model.Interfaces;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using View.Forms.Cotizaciones;
using View.Forms.UserControls;
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

        private ObservableCollection<string> listaDuplicado;
        public ObservableCollection<string> ListaDuplicado
        {
            get { return listaDuplicado; }
            set { listaDuplicado = value; }
        }


        private ObservableCollection<StackPanel> panelPropiedades;
        public ObservableCollection<StackPanel> PanelPropiedades
        {
            get { return panelPropiedades; }
            set { panelPropiedades = value; NotifyChange("PanelPropiedades"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesBool;
        public ObservableCollection<StackPanel> PanelPropiedadesBool
        {
            get { return panelPropiedadesBool; }
            set { panelPropiedadesBool = value; NotifyChange("PanelPropiedadesBool"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesCadena;
        public ObservableCollection<StackPanel> PanelPropiedadesCadena
        {
            get { return panelPropiedadesCadena; }
            set { panelPropiedadesCadena = value; NotifyChange("PanelPropiedadesCadena"); }
        }

        private ObservableCollection<StackPanel> panelPropiedadesOpcionales;
        public ObservableCollection<StackPanel> PanelPropiedadesOpcionales
        {
            get { return panelPropiedadesOpcionales; }
            set { panelPropiedadesOpcionales = value; NotifyChange("PanelPropiedadesOpcionales"); }
        }
        
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
            cargarListaDuplicados();
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

        private void cargarListaDuplicados()
        {
            ListaDuplicado = new ObservableCollection<string>();
            ListaDuplicado.Add("COLOCARBANDA");
            ListaDuplicado.Add("RMP1_110");
            ListaDuplicado.Add("RMP2_110");
            ListaDuplicado.Add("RMP_112");
            ListaDuplicado.Add("numeroDeJorobas");
            ListaDuplicado.Add("origenDesengrase");
            ListaDuplicado.Add("RPM1_150");
            ListaDuplicado.Add("RPM2_150");
            ListaDuplicado.Add("numeroPasadas160");
            ListaDuplicado.Add("espesorEspaciador2040");
            ListaDuplicado.Add("espesorEspaciador2050");
            ListaDuplicado.Add("espesorEspaciador2060");
            ListaDuplicado.Add("AutomaticMacLarge");
            ListaDuplicado.Add("AutomaticMacLarge2150");
            ListaDuplicado.Add("tCiclo420");
            ListaDuplicado.Add("tCiclo421");
            ListaDuplicado.Add("arbol455");
            ListaDuplicado.Add("programa455");
            ListaDuplicado.Add("programa456");
            ListaDuplicado.Add("numeroPaso491");
            ListaDuplicado.Add("inspeccion100_500");
            ListaDuplicado.Add("rmp5002");
            ListaDuplicado.Add("rpm5032");
            ListaDuplicado.Add("programa520");
            ListaDuplicado.Add("tCiclo715");
            ListaDuplicado.Add("tiempoHorneado757");
            ListaDuplicado.Add("numeroMandriles766");
            ListaDuplicado.Add("retrabajo901");
            ListaDuplicado.Add("receta9204");
            ListaDuplicado.Add("shap1");
            ListaDuplicado.Add("pasadas9215");
            ListaDuplicado.Add("numeroPasadas9221");
            ListaDuplicado.Add("numeroPaso9242");
            ListaDuplicado.Add("cargaAnillos9243");
            ListaDuplicado.Add("numeroStrokes9243");
            ListaDuplicado.Add("piezasXCuff9262");
            ListaDuplicado.Add("anilloXCuff9263");
            ListaDuplicado.Add("tipoMaquinado9266");
            ListaDuplicado.Add("anilloXCuff9266");
            ListaDuplicado.Add("anillosXMandril9301");
            ListaDuplicado.Add("tiempoCiclo9341");
            ListaDuplicado.Add("tiempoCiclo9361");
            ListaDuplicado.Add("tiempoCiclo9362");
            ListaDuplicado.Add("tCiclo2200");
            ListaDuplicado.Add("numeroPasadas180");
        }

        /// <summary>
        /// /Método el cual nos envía a la pantalla de Lista de Centros de trabajo, insertar los valores en la lista, validar los botones de "Aceptar" y "Cancelar"
        /// </summary>
        private void irListaCentroTrabajo()
        {
            PanelPropiedades = new ObservableCollection<StackPanel>();
            PanelPropiedadesCadena = new ObservableCollection<StackPanel>();
            PanelPropiedadesBool = new ObservableCollection<StackPanel>();
            PanelPropiedadesOpcionales = new ObservableCollection<StackPanel>();

            IApplicationContext ctx;
            XmlApplicationContext file;
            ICentroTrabajo _elCentroTrabajo;
            List<ICentroTrabajo> ListaCreadaCentroTrabajo = new List<ICentroTrabajo>();

            ObservableCollection<NumericEntry> propiedadesNumeric = new ObservableCollection<NumericEntry>();
            ObservableCollection<BoolEntry> propiedadesBool = new ObservableCollection<BoolEntry>();
            ObservableCollection<StringEntry> propiedadesCadena = new ObservableCollection<StringEntry>();

            FrmListaCentroTrabajo frm = new FrmListaCentroTrabajo();
            ListaCentroTrabajoViewModel context = new ListaCentroTrabajoViewModel();
            frm.DataContext = context;
            frm.ShowDialog();

            if (frm.DialogResult.HasValue && frm.DialogResult.Value)
            {
                Lista = context.ListaCentroTrabajo;

                file = new XmlApplicationContext(@"\\agufileserv2\INGENIERIA\RESPRUTAS\RrrrUUUUUULLL\RepositoryCentroTrabajoDisenoProceso.xml");
                ctx = file;

                foreach (string item in Lista)
                {
                    //Obtenemos el ID del Centro de Trabajo.
                    string id = ListaCentroTrabajo.Where(x => x.CentroTrabajo == item).FirstOrDefault().ObjetoXML;
                    //Obtenemos un objeto del Centro de Trabajo.
                    _elCentroTrabajo = (ICentroTrabajo)ctx.GetObject(id);
                    //Asignamos el Centro de Trabajo a la lista.
                    ListaCreadaCentroTrabajo.Add(_elCentroTrabajo);
                }

                foreach (ICentroTrabajo centroTrabajo in ListaCreadaCentroTrabajo)
                {
                    foreach (Propiedad propiedad in centroTrabajo.PropiedadesRequeridadas)
                    {
                        NumericEntry numeric = new NumericEntry();
                        PropiedadViewModel propiedadViewModel = new PropiedadViewModel(propiedad);
                        numeric.DataContext = propiedadViewModel;
                        propiedadesNumeric.Add(numeric);
                    }

                    foreach (PropiedadBool propiedadBool in centroTrabajo.PropiedadesRequeridasBool)
                    {
                        BoolEntry boolEntry = new BoolEntry();
                        PropiedadBoolViewModel propiedadBoolViewModel = new PropiedadBoolViewModel(propiedadBool);
                        boolEntry.DataContext = propiedadBoolViewModel;
                        propiedadesBool.Add(boolEntry);

                    }

                    foreach (PropiedadCadena propiedadCadena in centroTrabajo.PropiedadesRequeridasCadena)
                    {
                        StringEntry stringEntry = new StringEntry();
                        PropiedadCadenaViewModel propiedadCadenaViewModel = new PropiedadCadenaViewModel(propiedadCadena);
                        stringEntry.DataContext = propiedadCadena;
                        propiedadesCadena.Add(stringEntry);
                    }
                }

                foreach (NumericEntry numericEntry in propiedadesNumeric)
                {
                    StackPanel panel = new StackPanel();
                    panel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    panel.Children.Add(numericEntry);

                    PanelPropiedades.Add(panel);
                }

                foreach (BoolEntry boolEntry in propiedadesBool)
                {
                    StackPanel panel = new StackPanel();
                    panel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    panel.Children.Add(boolEntry);

                    PanelPropiedadesBool.Add(panel);
                }

                foreach (StringEntry stringEntry in propiedadesCadena)
                {
                    StackPanel panel = new StackPanel();
                    panel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    panel.Children.Add(stringEntry);

                    PanelPropiedadesCadena.Add(panel);
                }
                
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
