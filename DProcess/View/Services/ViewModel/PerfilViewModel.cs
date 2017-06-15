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

namespace View.Services.ViewModel
{
    public class PerfilViewModel : INotifyPropertyChanged
    {

        #region Attributes
        Perfil modelPerfil;
        DialogService dialogService;
        #endregion

        #region Properties

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
        #endregion
        
        #region Methods

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
            controllerProgressAsync = await dialogService.SendProgressAsync("Please Wait", "Saving...");

            //Ejecutamos el método para guardar los datos del perfil.
            await ServicioPerfil.SetPerfil(SelectedTipoPerfil.IdTipoPerfil, Nombre, Descripcion, Imagen, 1);

            //Ejecutamos el método para cerrar el mensaje de espera.
            await controllerProgressAsync.CloseAsync();

            //Ejecutamos el método para enviar un mensaje de confirmación al usuario.
            await dialogService.SendMessage("Alert", "Done!");
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
