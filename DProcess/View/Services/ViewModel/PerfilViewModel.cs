using DataAccess.ServiceObjects.Perfiles;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.Routing;
using View.Forms.Shared;
using View.Resources;

namespace View.Services.ViewModel
{
    public class PerfilViewModel : INotifyPropertyChanged
    {

        #region Attributes
        Perfil modelPerfil;
        DialogService dialogService;
        #endregion

        #region Properties
        
        public int idPerfil
        {
            get { return  modelPerfil.idPerfil; }
            set { modelPerfil.idPerfil = value; NotifyChange("idPerfil"); }
        }
        
        public string Nombre {
            get
            {
                return modelPerfil.Nombre;
            }
            set
            {
                modelPerfil.Nombre = value;
                NotifyChange("Nombre");
            }
        }

        public string Descripcion {
            get
            {
                return modelPerfil.Descripcion;
            }
            set
            {
                modelPerfil.Descripcion = value;
                NotifyChange("Descripcion");
            }
        }

        public byte[] Imagen {
            get
            {
                return modelPerfil.Imagen;
            }
            set
            {
                modelPerfil.Imagen = value;
                NotifyChange("Imagen");
            }
        }

        public ObservableCollection<TipoPerfil> ListaTipoPerfil { get; set; }

        private TipoPerfil selectedTipoPerfil;
        public TipoPerfil SelectedTipoPerfil
        {
            get { return selectedTipoPerfil; }
            set { selectedTipoPerfil = value; NotifyChange("SelectedTipoPerfil"); }
        }

        private ObservableCollection<Perfil> listaAllPerfiles;
        public ObservableCollection<Perfil> ListaAllPerfiles
        {
            get { return listaAllPerfiles; }
            set { listaAllPerfiles = value; NotifyChange("ListaAllPerfiles"); }
        }

        private Perfil _PerfilSeleccionado;
        public Perfil PerfilSeleccionado
        {
            get { return _PerfilSeleccionado; }
            set { _PerfilSeleccionado = value; NotifyChange("PerfilSeleccionado"); }
        }

        private ObservableCollection<Propiedad> _ListaPropiedades;
        public ObservableCollection<Propiedad> ListaPropiedades
        {
            get { return _ListaPropiedades; }
            set { _ListaPropiedades = value; NotifyChange("ListaPropiedades"); }
        }

        private ObservableCollection<PropiedadCadena> _ListaPropiedadesCadena;
        public ObservableCollection<PropiedadCadena> ListaPropiedadesCadena
        {
            get { return _ListaPropiedadesCadena; }
            set { _ListaPropiedadesCadena = value; NotifyChange("ListaPropiedadesCadena"); }
        }

        private ObservableCollection<PropiedadBool> _ListaPropiedadesBool;
        public ObservableCollection<PropiedadBool> ListaPropiedadesBool
        {
            get { return _ListaPropiedadesBool; }
            set { _ListaPropiedadesBool = value; NotifyChange("ListaPropiedadesBool"); }
        }

        private string _EtiquetaPropiedades;
        public string EtiquetaPropiedades
        {
            get { return _EtiquetaPropiedades; }
            set { _EtiquetaPropiedades = value; NotifyChange("EtiquetaPropiedades"); }
        }

        private string _EtiquetaPropiedadesCadena;
        public string EtiquetaPropiedadesCadena
        {
            get { return _EtiquetaPropiedadesCadena; }
            set { _EtiquetaPropiedadesCadena = value; NotifyChange("EtiquetaPropiedadesCadena"); }
        }

        private string _EtiquetaPropiedadesBool;
        public string EtiquetaPropiedadesBool
        {
            get { return _EtiquetaPropiedadesBool; }
            set { _EtiquetaPropiedadesBool = value; NotifyChange("EtiquetaPropiedadesBool"); }
        }
        
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor por default.
        /// </summary>
        public PerfilViewModel()
        {
            //Ejecutamos el método en el cual obtenemos todos los tipos de perfil. El resultado lo asignamos a la propiedad.
            ListaTipoPerfil = DataManager.GetAllTipoPerfil();

            //Obtenermos el primer elemento de la lista y lo asignamos a la propiedad.
            SelectedTipoPerfil = ListaTipoPerfil.First();

            modelPerfil = new Perfil();

            //Inicializamos los servicios de DialogService.
            dialogService = new DialogService();

            ListaAllPerfiles = DataManager.GetAllPerfiles();
        }
        #endregion

        #region Commands
        public ICommand GuardarPerfil
        {
            get
            {
                return new RelayCommand(o => guardarPerfil());
            }
        }

        public ICommand SeleccionarImagen
        {
            get
            {
                return new RelayCommand(o => seleccionarImagen());
            }
        }

        public ICommand EditarPerfil
        {
            get
            {
                return new RelayCommand(o => editarPerfil());
            }
        }

        public ICommand EditarPropiedades
        {
            get
            {
                return new RelayCommand(o => editarPropiedades());
            }
        }

        public ICommand EditarPropiedadesCadena
        {
            get
            {
                return new RelayCommand(o => editarPropiedadesCadena());
            }
        }
        #endregion

        private ObservableCollection<Propiedad> allPropiedades;
        private ObservableCollection<PropiedadCadena> allPropiedadesCadena;

        #region Methods

        private void editarPropiedadesCadena()
        {
            //if (allPropiedadesCadena == null || allPropiedadesCadena.Count == 0)
            //    / allPropiedadesCadena = DataManager.GetAllPropiedades
        }

        /// <summary>
        /// Método que abre una ventana con las posibles propiedades numéricas a seleccionar.
        /// </summary>
        private void editarPropiedades()
        {
            //Identificamos si ya consultamos por primera vez todas las propiedades. Si ya lo consultamos, no volvemos a consultar.
            if (allPropiedades == null || allPropiedades.Count == 0)
                allPropiedades = DataManager.GetAllPropiedades();


            ObservableCollection<FO_Item> listadoPropiedades = new ObservableCollection<FO_Item>();

            foreach (Propiedad propiedad in allPropiedades)
            {
                FO_Item item = new FO_Item();
                item.Nombre = propiedad.Nombre;
                item.id = propiedad.idPropiedad;
                item.Descripcion = propiedad.DescripcionCorta;
                if (ListaPropiedades.Where(x => x.idPropiedad == propiedad.idPropiedad).ToList().Count > 0)
                    item.IsSelected = true;
                else
                    item.IsSelected = false;

                listadoPropiedades.Add(item);
            }
            
            FO_ItemViewModel vm = new FO_ItemViewModel(listadoPropiedades, "Lista de propiedades numéricas");

            WSelectedOption ventana = new WSelectedOption();
            ventana.DataContext = vm;

            ventana.ShowDialog();

            ListaPropiedades.Clear();
            foreach (FO_Item item in vm.ListaAllOptions)
            {
                if (item.IsSelected)
                {
                    ListaPropiedades.Add(DataManager.GetPropiedadById(item.id));
                }
            }
        }

        private void editarPerfil()
        {
            if (PerfilSeleccionado.idPerfil > 0)
            {
                WViewPerfil ventana = new WViewPerfil();
                ListaPropiedades = DataManager.GetAllPropiedadesByPerfil(PerfilSeleccionado.idPerfil,false);
                EtiquetaPropiedades = ListaPropiedades.Count > 0 ? "Hay " + ListaPropiedades.Count + " propiedades asignadas." : "No ha propiedades numéricas asignadas.";

                ListaPropiedadesCadena = DataManager.GetAllPropiedadesCadenaByPerfil(PerfilSeleccionado.idPerfil);
                EtiquetaPropiedadesCadena = ListaPropiedadesCadena.Count > 0 ? "Hay " + ListaPropiedadesCadena.Count + " propiedades asignadas." : "No hay propiedades cadena asignadas.";

                ListaPropiedadesBool = DataManager.GetallPropiedadesBoolByPerfil(PerfilSeleccionado.idPerfil);
                EtiquetaPropiedadesBool = ListaPropiedadesBool.Count > 0 ? "Hay " + ListaPropiedadesBool.Count + " propiedades asignadas." : "No hay propiedades booleanas asignadas.";

                ventana.DataContext = this;

                ventana.ShowDialog();
            }
        }

        private void seleccionarImagen()
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "PNG Files (.png)|*.png";

            Nullable<bool> result = dialog.ShowDialog();

            if (result == true)
            {
                string fileName = dialog.FileName;
                Imagen = File.ReadAllBytes(fileName);
            }
        }

        private async void guardarPerfil()
        {
            //Inicializamos los servicios de SO_Perfil.
            SO_Perfil ServicioPerfil = new SO_Perfil();

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController controllerProgressAsync;

            //Ejecutamos el método para enviar un mensaje de espera mientras el documento se guarda.
            controllerProgressAsync = await dialogService.SendProgressAsync(StringResources.msgEspera, StringResources.msgGuardando);

            //Ejecutamos el método para guardar los datos del perfil.
            await ServicioPerfil.SetPerfil(SelectedTipoPerfil.IdTipoPerfil, Nombre, Descripcion, Imagen, 1);

            //Ejecutamos el método para cerrar el mensaje de espera.
            await controllerProgressAsync.CloseAsync();

            //Ejecutamos el método para enviar un mensaje de confirmación al usuario.
            await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgCambiosRealizados);
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
