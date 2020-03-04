using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Office.Core;
using Model;
using Model.ControlDocumentos;
using Model.Interfaces;
using Spring.Context;
using Spring.Context.Support;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
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
using NsExcel = Microsoft.Office.Interop.Excel;
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
        private ObservableCollection<ICentroTrabajo> _ListaCreadaCentroTrabajo;
        public ObservableCollection<ICentroTrabajo> ListaCreadaCentroTrabajo
        {
            get
            {
                return _ListaCreadaCentroTrabajo;
            }
            set
            {
                _ListaCreadaCentroTrabajo = value;
                NotifyChange("ListaCreadaCentroTrabajo");
            }

        }

        private ObservableCollection<ICentroTrabajo> _ListaMostrar;
        public ObservableCollection<ICentroTrabajo> ListaMostrar
        {
            get
            {
                return _ListaMostrar;
            }
            set
            {
                _ListaMostrar = value;
                NotifyChange("ListaMostrar");
            }

        }


        private ObservableCollection<PropiedadViewModel> _PropiedadesViewModel;
        public ObservableCollection<PropiedadViewModel> PropiedadesViewModel
        {
            get
            {
                return _PropiedadesViewModel;
            }
            set
            {
                _PropiedadesViewModel = value;
                NotifyChange("PropiedadesViewModel");
            }
        }

        private ObservableCollection<PropiedadBoolViewModel> _PropiedadesBoolViewModel;
        public ObservableCollection<PropiedadBoolViewModel> PropiedadesBoolViewModel
        {
            get
            {
                return _PropiedadesBoolViewModel;
            }
            set
            {
                _PropiedadesBoolViewModel = value;
                NotifyChange("PropiedadBoolViewModel");
            }
        }

        private ObservableCollection<PropiedadCadenaViewModel> _PropiedadesCadenaViewModel;
        public ObservableCollection<PropiedadCadenaViewModel> PropiedadesCadenaViewModel
        {
            get
            {
                return _PropiedadesCadenaViewModel;
            }
            set
            {
                _PropiedadesCadenaViewModel = value;
                NotifyChange("PropiedadesCadenaViewModel");
            }
        }

        private ObservableCollection<PropiedadOptionalViewModel> _PropiedadesOptionalViewModel;
        public ObservableCollection<PropiedadOptionalViewModel> PropiedadesOptionalViewModel
        {
            get
            {
                return _PropiedadesOptionalViewModel;
            }
            set
            {
                _PropiedadesOptionalViewModel = value;
                NotifyChange("PropiedadesOptionalViewModel");
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

        private ICentroTrabajo selectedRow;
        public ICentroTrabajo SelectedRow
        {
            get
            {
                return selectedRow;
            }
            set
            {
                selectedRow = value;
                NotifyChange("SelectedRow");
            }

        }
        private bool habilitar;
        public bool Habilitar
        {
            get
            {
                return habilitar;
            }
            set
            {
                habilitar = value;
                NotifyChange("Habilitar");
            }
        }
        private bool cerrarVentanaWPF;
        public bool CerrarVentanaWPF
        {
            get
            {
                return cerrarVentanaWPF;
            }
            set
            {
                cerrarVentanaWPF = value;
                NotifyChange("CerrarVentanaWPF");
            }
        }
        FrmVistaWPF vista = new FrmVistaWPF();

        #endregion

        #region Constructors

        public CrearCotizacionViewModel()
        {
            SelectedTipoCentroTrabajo = new CentrosTrabajo();
            agregarCentroTrabajo();
            cargarListaDuplicados();
            PropiedadesViewModel = new ObservableCollection<PropiedadViewModel>();
            PropiedadesBoolViewModel = new ObservableCollection<PropiedadBoolViewModel>();
            PropiedadesCadenaViewModel = new ObservableCollection<PropiedadCadenaViewModel>();
            PropiedadesOptionalViewModel = new ObservableCollection<PropiedadOptionalViewModel>();
            ListaCreadaCentroTrabajo = new ObservableCollection<ICentroTrabajo>();
            ListaMostrar = new ObservableCollection<ICentroTrabajo>();



        }
        #endregion

        #region Commands

        public ICommand IrListaCentroTrabajo
        {
            get
            {
                return new RelayCommand(o => AbrirListaCentroTrabajo());
            }
        }
        public ICommand IrListaCT
        {
            get
            {
                return new RelayCommand(o => AgregarCentroTrabajo());
            }
        }
        public ICommand IrEliminarTodo
        {
            get
            {
                return new RelayCommand(o => irEliminarTodo());
            }
        }
        public ICommand IrEliminarUno
        {
            get
            {
                return new RelayCommand(o => irEliminarUno());
            }
        }
        public ICommand IrCalcular
        {
            get
            {
                return new RelayCommand(o => irCalcular());
            }
        }

        public ICommand IrExportExcel
        {
            get
            {
               
                return new RelayCommand(o => ListToExcel());
            }
        }

        #endregion

        #region Methods



        public void ListToExcel()

        {
            int letras = 0;
            var excel = new Microsoft.Office.Interop.Excel.Application();
            excel.Visible = true;
            var workbook = excel.Workbooks.Add(NsExcel.XlWBATemplate.xlWBATWorksheet);
            var sheet = (NsExcel.Worksheet)workbook.Sheets[1];



            var rango = sheet.get_Range("A1", "A1");
            rango.Value2 = "Operacion";
            
            rango.Font.Bold = true; 
            letras = rango.Value2.Length;
            rango.Columns.AutoFit();


            rango = sheet.get_Range("B1", "B1");
            rango.Value2 = "Centro de trabajo"; 

            rango.Font.Bold = true;
            rango.Columns.AutoFit();

            rango = sheet.get_Range("C1", "C1");
            rango.Value2 = "T. Setup";

            rango.Font.Bold = true;
            rango.Columns.AutoFit();

            rango = sheet.get_Range("D1", "D1");
            rango.Value2 = "T. Machine";

            rango.Font.Bold = true;
            rango.Columns.AutoFit();


            rango = sheet.get_Range("E1", "E1");
            rango.Value2 = "T.Labor";

            rango.Font.Bold = true;
            rango.Columns.AutoFit();

            string cellNom;
            int cantidadLetras = 0;
            int count = 2;
            foreach (var item in ListaMostrar)
            {
                cellNom = "A" + count.ToString();
                var rangoDatos = sheet.get_Range(cellNom);
                rangoDatos.Value2 = item.NombreOperacion.ToString();
                cantidadLetras = item.NombreOperacion.Length;
                //Validacion del numero de letras que contiene
                if (letras < cantidadLetras)
                {
                    letras = cantidadLetras;
                    rangoDatos.Columns.AutoFit();
                }

                cellNom = "B" + count.ToString();
                rangoDatos = sheet.get_Range(cellNom);
                rangoDatos.Value2 = "'" + item.CentroTrabajo.ToString();

                cellNom = "C" + count.ToString();
                rangoDatos = sheet.get_Range(cellNom);
                rangoDatos.Value2 = item.TiempoSetup.ToString();

                cellNom = "D" + count.ToString();
                rangoDatos = sheet.get_Range(cellNom);
                rangoDatos.Value2 = item.TiempoMachine.ToString();

                cellNom = "E" + count.ToString();
                rangoDatos = sheet.get_Range(cellNom);
                rangoDatos.Value2 = item.TiempoLabor.ToString();

                ++count;
            }

        }

        private async void irCalcular()
        {

            int c = PropiedadesViewModel.Count;
            List<Propiedad> Prop = new List<Propiedad>();
            List<PropiedadBool> PBool = new List<PropiedadBool>();
            List<PropiedadCadena> PCadena = new List<PropiedadCadena>();
            List<PropiedadOptional> POptional = new List<PropiedadOptional>();
            DialogService dialog = new DialogService();

            foreach (PropiedadViewModel datos in PropiedadesViewModel)
            {
                Prop.Add(datos.model);
            }
            foreach (PropiedadBoolViewModel datosBool in PropiedadesBoolViewModel)
            {
                PBool.Add(datosBool.model);
            }
            foreach (PropiedadCadenaViewModel datosCadena in PropiedadesCadenaViewModel)
            {
                PCadena.Add(datosCadena.model);
            }
            foreach (PropiedadOptionalViewModel datosOptional in PropiedadesOptionalViewModel)
            {
                POptional.Add(datosOptional.model);
            }
            int cont = 0;
            foreach (var item in ListaCreadaCentroTrabajo)
            {
                if(item.Test() == false)
                {
                    cont++; 
                }
            }
            if (cont >= 1)
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgDatosIncorrectos);
            }
            else
            {
                foreach (ICentroTrabajo a in ListaCreadaCentroTrabajo)
                {
                    a.Calcular(Prop, PBool, PCadena, POptional);
                }
                foreach (var item in ListaCreadaCentroTrabajo)
                {
                    //Agrega a la lista los datos por mostrar en el Datagrid
                   
                    ListaMostrar.Add(item);

                }
                vista.Close();
            }

        }

        private void cargarListaDuplicados()
        {
            ListaDuplicado = new ObservableCollection<string>();
            ListaDuplicado.Add("COLOCARBANDA");
            ListaDuplicado.Add("RPM1_110");
            ListaDuplicado.Add("RPM2_110");
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
        /// Metodo para eliminar todas las filas de Datagrid
        /// </summary>
        private void irEliminarTodo()
        {
            ListaMostrar = new ObservableCollection<ICentroTrabajo>();
            ListaCreadaCentroTrabajo = new ObservableCollection<ICentroTrabajo>();
            Habilitar = false;
        }
        /// <summary>
        /// Metodo para eliminar la fila seleccionada en el Datagrid
        /// </summary>
        private void irEliminarUno()
        {
            ListaMostrar.Remove(SelectedRow);
            if (ListaMostrar.Count == 0)
            {
                Habilitar = false;
            }
        }
        /// <summary>
        /// Metodo que se ejecuta cuando se agrega una lista de Centros de Trabajo
        /// </summary>
        private void AbrirListaCentroTrabajo()
        {
            List<string> Lista = new List<string>();
            FrmListaCentroTrabajo frm = new FrmListaCentroTrabajo();
            ListaCentroTrabajoViewModel context = new ListaCentroTrabajoViewModel();
            frm.DataContext = context;
            frm.ShowDialog();
            //Validacion del boton Aceptar o Cancelar
            if (frm.DialogResult.HasValue && frm.DialogResult.Value)
            {
                //Se actualizan las listas
                ListaMostrar = new ObservableCollection<ICentroTrabajo>();
                ListaCreadaCentroTrabajo = new ObservableCollection<ICentroTrabajo>();
                Lista = context.ListaCentroTrabajo;
                if (Lista.Count > 0)
                {
                    irListaCentroTrabajo(Lista);
                    Habilitar = true;
                }
                
            }
        }
        /// <summary>
        /// Metodo que se ejecutacuando se agrega un centro de trabajo
        /// </summary>
        private async void AgregarCentroTrabajo()
        {
            DialogService dialog = new DialogService();
            List<string> Lista = new List<string>();
            ListaCreadaCentroTrabajo = new ObservableCollection<ICentroTrabajo>();
            Lista.Add(SelectedTipoCentroTrabajo.CentroTrabajo);

            if (!Lista.Contains(null) && Lista.Count > 0)
            {
                irListaCentroTrabajo(Lista);
                Habilitar = true;
            }
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSeleccioneCT);
            }
        }

        private void irListaCentroTrabajo(List<string> Lista)
        {
            PanelPropiedades = new ObservableCollection<StackPanel>();

            PanelPropiedadesCadena = new ObservableCollection<StackPanel>();
            PanelPropiedadesBool = new ObservableCollection<StackPanel>();
            PanelPropiedadesOpcionales = new ObservableCollection<StackPanel>();

            IApplicationContext ctx;
            XmlApplicationContext file;
            ICentroTrabajo _elCentroTrabajo;


            ObservableCollection<Propiedad> propiedades_Numericas = new ObservableCollection<Propiedad>();
            ObservableCollection<PropiedadBool> propiedades_Bool = new ObservableCollection<PropiedadBool>();
            ObservableCollection<PropiedadCadena> propiedades_Cadenas = new ObservableCollection<PropiedadCadena>();
            ObservableCollection<PropiedadOptional> propiedades_Opcionales = new ObservableCollection<PropiedadOptional>();


            ObservableCollection<NumericEntry> propiedadesNumeric = new ObservableCollection<NumericEntry>();
            ObservableCollection<BoolEntry> propiedadesBool = new ObservableCollection<BoolEntry>();
            ObservableCollection<StringEntry> propiedadesCadena = new ObservableCollection<StringEntry>();
            ObservableCollection<OptionalEntry> propiedadesOpcionales = new ObservableCollection<OptionalEntry>();

            PropiedadesViewModel = new ObservableCollection<PropiedadViewModel>();
            PropiedadesCadenaViewModel = new ObservableCollection<PropiedadCadenaViewModel>();
            PropiedadesBoolViewModel = new ObservableCollection<PropiedadBoolViewModel>();
            PropiedadesOptionalViewModel = new ObservableCollection<PropiedadOptionalViewModel>();

            DialogService dialog = new DialogService();

            file = new XmlApplicationContext(@"\\agufileserv2\INGENIERIA\RESPRUTAS\RrrrUUUUUULLL\RepositoryCentroTrabajoDisenoProceso.xml");
            ctx = file;

            foreach (string item in Lista)
            {
                //Obtenemos el ID del Centro de Trabajo.
                int a = ListaCentroTrabajo.Where(x => x.CentroTrabajo == item).ToList().Count;
                if (a != 0)
                {
                    string id = ListaCentroTrabajo.Where(x => x.CentroTrabajo == item).FirstOrDefault().ObjetoXML;
                    //Obtenemos un objeto del Centro de Trabajo.
                    _elCentroTrabajo = (ICentroTrabajo)ctx.GetObject(id);
                    // Asignamos el Centro de Trabajo a la lista.
                    ListaCreadaCentroTrabajo.Add(_elCentroTrabajo);
                }

            }

            foreach (ICentroTrabajo centroTrabajo in ListaCreadaCentroTrabajo)
            {
                foreach (Propiedad propiedad in centroTrabajo.PropiedadesRequeridadas)
                {
                    NumericEntry numeric = new NumericEntry();
                    PropiedadViewModel propiedadViewModel = new PropiedadViewModel(propiedad);
                    numeric.DataContext = propiedadViewModel;
                    //Validar si el nombre esta en la lista de duplicados
                    int b = ListaDuplicado.Where(x => x == propiedad.Nombre).ToList().Count;
                    if (b > 0)
                    {
                        numeric.Name = propiedad.Nombre;
                        propiedades_Numericas.Add(propiedad);
                        propiedadesNumeric.Add(numeric);
                        PropiedadesViewModel.Add(propiedadViewModel);
                    }
                    else
                    {
                        if (propiedades_Numericas.Where(x => x.Nombre == propiedad.Nombre).ToList().Count == 0)
                        {
                            numeric.Name = propiedad.Nombre;
                            propiedades_Numericas.Add(propiedad);
                            propiedadesNumeric.Add(numeric);
                            PropiedadesViewModel.Add(propiedadViewModel);
                        }
                    }
                }

                foreach (PropiedadBool propiedadBool in centroTrabajo.PropiedadesRequeridasBool)
                {
                    BoolEntry boolEntry = new BoolEntry();
                    PropiedadBoolViewModel propiedadBoolViewModel = new PropiedadBoolViewModel(propiedadBool);
                    boolEntry.DataContext = propiedadBoolViewModel;
                    //Validar si el nombre esta en la lista de duplicados
                    int b = ListaDuplicado.Where(x => x == propiedadBool.Nombre).ToList().Count;
                    if (b > 0)
                    {
                        propiedades_Bool.Add(propiedadBool);
                        propiedadesBool.Add(boolEntry);
                        PropiedadesBoolViewModel.Add(propiedadBoolViewModel);
                    }
                    else
                    {
                        if (propiedades_Bool.Where(x => x.Nombre == propiedadBool.Nombre).ToList().Count == 0)
                        {
                            propiedades_Bool.Add(propiedadBool);
                            propiedadesBool.Add(boolEntry);
                            PropiedadesBoolViewModel.Add(propiedadBoolViewModel);
                        }
                    }
                }

                foreach (PropiedadCadena propiedadCadena in centroTrabajo.PropiedadesRequeridasCadena)
                {
                    StringEntry stringEntry = new StringEntry();
                    PropiedadCadenaViewModel propiedadCadenaViewModel = new PropiedadCadenaViewModel(propiedadCadena);
                    stringEntry.DataContext = propiedadCadenaViewModel;
                    //Validar si el nombre esta en la lista de duplicados
                    int b = ListaDuplicado.Where(x => x == propiedadCadena.Nombre).ToList().Count;
                    if (b > 0)
                    {
                        propiedades_Cadenas.Add(propiedadCadena);
                        propiedadesCadena.Add(stringEntry);
                        PropiedadesCadenaViewModel.Add(propiedadCadenaViewModel);
                    }
                    else
                    {
                        if (propiedades_Cadenas.Where(x => x.Nombre == propiedadCadena.Nombre).ToList().Count == 0)
                        {
                            propiedades_Cadenas.Add(propiedadCadena);
                            propiedadesCadena.Add(stringEntry);
                            PropiedadesCadenaViewModel.Add(propiedadCadenaViewModel);
                        }
                    }

                }

                foreach (PropiedadOptional propiedadOpcional in centroTrabajo.PropiedadesRequeridasOpcionles)
                {
                    OptionalEntry optionalEntry = new OptionalEntry();
                    PropiedadOptionalViewModel propiedadOpcionalViewModel = new PropiedadOptionalViewModel(propiedadOpcional);
                    optionalEntry.DataContext = propiedadOpcionalViewModel;
                    //Validar si el nombre esta en la lista de duplicados
                    int b = ListaDuplicado.Where(x => x == propiedadOpcional.Nombre).ToList().Count;
                    if (b > 0)
                    {
                        propiedades_Opcionales.Add(propiedadOpcional);
                        propiedadesOpcionales.Add(optionalEntry);
                        PropiedadesOptionalViewModel.Add(propiedadOpcionalViewModel);
                    }
                    else
                    {
                        if (propiedades_Opcionales.Where(x => x.Nombre == propiedadOpcional.Nombre).ToList().Count == 0)
                        {
                            propiedades_Opcionales.Add(propiedadOpcional);
                            propiedadesOpcionales.Add(optionalEntry);
                            PropiedadesOptionalViewModel.Add(propiedadOpcionalViewModel);
                        }
                    }
                }
            }
            foreach (NumericEntry numericEntry in propiedadesNumeric)
            {
                try
                {
                    StackPanel panel = new StackPanel();
                    panel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    panel.Name = numericEntry.Name;
                    panel.Children.Add(numericEntry);

                    PanelPropiedades.Add(panel);
                }
                catch (Exception a)
                {
                    string aa = a.Message;
                }
            }

            foreach (BoolEntry boolEntry in propiedadesBool)
            {
                try
                {
                    StackPanel panel = new StackPanel();
                    panel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    panel.Children.Add(boolEntry);

                    PanelPropiedadesBool.Add(panel);
                }
                catch (Exception a)
                {
                    string aa = a.Message;
                }
            }

            foreach (StringEntry stringEntry in propiedadesCadena)
            {
                try
                {
                    StackPanel panel = new StackPanel();
                    panel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    panel.Children.Add(stringEntry);

                    PanelPropiedadesCadena.Add(panel);
                }
                catch (Exception a)
                {
                    string aa = a.Message;
                }
            }

            foreach (OptionalEntry optionalEntry in propiedadesOpcionales)
            {
                try
                {
                    StackPanel panel = new StackPanel();
                    panel.Orientation = System.Windows.Controls.Orientation.Horizontal;
                    panel.Children.Add(optionalEntry);

                    PanelPropiedadesOpcionales.Add(panel);
                }
                catch (Exception a)
                {
                    string aa = a.Message;
                }
            }

            vista = new FrmVistaWPF();
            vista.DataContext = this;

            vista.ShowDialog();
           
           

            //if (vista.DialogResult.HasValue && vista.DialogResult.Value)
            //{
            //    foreach (var item in ListaCreadaCentroTrabajo)
            //    {
            //        //Agrega a la lista los datos por mostrar en el Datagrid
            //        ListaMostrar.Add(item);
            //    }
            //}
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
