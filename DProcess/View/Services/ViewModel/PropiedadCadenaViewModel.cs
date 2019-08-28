using MahApps.Metro.Controls.Dialogs;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using View.Forms.Routing;
using View.Resources;
using MahApps.Metro.Controls;
using System.Linq;

namespace View.Services.ViewModel
{
    public class PropiedadCadenaViewModel : INotifyPropertyChanged
    {
        #region Propiedades

        private ObservableCollection<PropiedadCadena> _ListaPropiedades;
        public ObservableCollection<PropiedadCadena> ListaPropiedades
        {
            get
            {
                return _ListaPropiedades;
            }
            set
            {
                _ListaPropiedades = value;
                NotifyChange("ListaPropiedades");
            }
        }

        private PropiedadCadena _PropiedadSeleccionada;
        public PropiedadCadena PropiedadSeleccionada
        {
            get
            {
                return _PropiedadSeleccionada;
            }set
            {
                _PropiedadSeleccionada = value;
                NotifyChange("PropiedadSeleccionada");
            }
        }

        public bool InsertarNuevoComponente = false;

        #endregion
        
        #region Atributos
        private PropiedadCadena model;
        #endregion

        #region Constructores
        public PropiedadCadenaViewModel(PropiedadCadena propiedad)
        {
            this.model = propiedad;
        }

        /// <summary>
        /// Constructor que obtiene todos los registros de la tabla
        /// </summary>
        /// <param name="ct1"></param>
        public PropiedadCadenaViewModel(bool ct1)
        {
            ListaPropiedades = DataManager.GetAllPropiedadCadena();
        }

        /// <summary>
        /// Constructor para insertar una nueva propiedad
        /// </summary>
        public PropiedadCadenaViewModel()
        {
            model = new PropiedadCadena();
            DialogService dialog = new DialogService();
            InsertarNuevoComponente = true;
        }
        #endregion

        #region Comandos

        public ICommand NewPropiedad
        {
            get
            {
                return new RelayCommand(a => _NewPropiedad());
            }
        }

        public ICommand EditarPropiedad
        {
            get
            {
                return new RelayCommand(a => _EditarPropiedad());
            }
        }

        public ICommand GuardarPropiedad
        {
            get
            {
                return new RelayCommand(a => _GuardarPropiedad());
            }
        }

        public ICommand EliminarPropiedad
        {
            get
            {
                return new RelayCommand(a => _EliminarPropiedad());
            }
        }

        public ICommand SeleccionarImagen
        {
            get
            {
                return new RelayCommand(a => _SeleccionarImagen());
            }
        }
        #endregion

        #region Propiedades del modelo
        /// <summary>
        /// Cadena que representa el nombre de la propiedad.
        /// </summary>
        public string Nombre {
            get {
                return model.Nombre;
            }
            set {
                model.Nombre = value;
                NotifyChange("Nombre");
            }
        }

        /// <summary>
        /// Cadena que representa el valor de la propiedad.
        /// </summary>
        public string Valor {
            get {
                return model.Valor;
            }
            set {
                model.Valor = value;
                NotifyChange("Valor");


            }
        }

        /// <summary>
        /// Cadena que representa una descripción corta de la propiedad.
        /// </summary>
        public string DescripcionCorta {
            get {
                return model.DescripcionCorta;
            }
            set {
                model.DescripcionCorta = value;
                NotifyChange("DescripcionCorta");
            }
        }

        /// <summary>
        /// Cadena que representa una descripción larga de la propiedad.
        /// </summary>
        public string DescripcionLarga {
            get {
                return model.DescripcionLarga;
            }
            set {
                model.DescripcionLarga = value;
            }
        }

        /// <summary>
        /// Arreglo de bytes que representa la imagen de la propiedad.
        /// </summary>
        public byte[] Imagen {
            get {
                return model.Imagen;
            }
            set {
                model.Imagen = value;
                NotifyChange("Imagen");
            }
        }

        #endregion

        #region Events INotifyPropertyChanged
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

        #region Métodos

        /// <summary>
        /// Método para insertar una nueva leccion aprendida
        /// </summary>
        public void _NewPropiedad()
        {
            WPropiedadCadena Form = new WPropiedadCadena();
            PropiedadCadenaViewModel data = new PropiedadCadenaViewModel();

            Form.DataContext = data;
            Form.ShowDialog();

            //Actualizamos los valores de la lista
            ListaPropiedades = DataManager.GetAllPropiedadCadena();
        }

        /// <summary>
        /// Método para editar una propiedad seleccionada
        /// </summary>
        public void _EditarPropiedad()
        {
            if (PropiedadSeleccionada.idPropiedad != 0)
            {
                WPropiedadCadena form = new WPropiedadCadena();
                PropiedadCadenaViewModel data = new PropiedadCadenaViewModel(PropiedadSeleccionada);

                form.DataContext = data;
                form.ShowDialog();

                //Actualizamos los valores de la lista
                ListaPropiedades = DataManager.GetAllPropiedadCadena();
            }
        }

        /// <summary>
        /// Método para guardar los cambios de una propiedad
        /// </summary>
        public async void _GuardarPropiedad()
        {
            int r = 0;

            DialogService dialog = new DialogService();

            MetroDialogSettings settings = new MetroDialogSettings();
            settings.AffirmativeButtonText = StringResources.lblYes;
            settings.NegativeButtonText = StringResources.lblNo;

            if (validar())
            {
                MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgConfirmacion, settings, MessageDialogStyle.AffirmativeAndNegative);

                if (MessageDialogResult.Affirmative == result)
                {
                    if (InsertarNuevoComponente == true)
                    {
                        r = DataManager.InsertarNuevaPropiedadCadena(model);
                    }else
                    {
                        r = DataManager.UpdatePropiedadCadena(model);
                    }

                    if (r>0)
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.ttlDone);

                        //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                        var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                        //Verificamos que la pantalla sea diferente de nulo.
                        if (window != null)
                        {
                            //Cerramos la pantalla
                            window.Close();
                        }
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                    }
                }
            }else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }
        }

        /// <summary>
        /// Método para eliminar una propiedad
        /// </summary>
        public async void _EliminarPropiedad()
        {
            if (model.idPropiedad != 0)
            {
                DialogService dialog = new DialogService();
                MetroDialogSettings settings = new MetroDialogSettings();

                settings.AffirmativeButtonText = StringResources.lblYes;
                settings.NegativeButtonText = StringResources.lblNo;

                MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEliminarRegistro, settings, MessageDialogStyle.AffirmativeAndNegative);

                if (MessageDialogResult.Affirmative == result)
                {
                    int e = DataManager.DeletePropiedadCadena(model.idPropiedad);

                    if (e != 0)
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.ttlDone);

                        //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                        var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                        //Verificamos que la pantalla sea diferente de nulo.
                        if (window != null)
                        {
                            //Cerramos la pantalla
                            window.Close();
                        }
                    }else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                    }
                }
            }
        }

        /// <summary>
        /// Método para seleccionar una imagen
        /// </summary>
        public async void _SeleccionarImagen()
        {
            //Abre la ventana de explorador de archivos
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.Filter = "Imagenes|*.png;*.bmp;*.jpg;*.jpeg";
            // Mostrar el explorador de archivos
            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                if (!Module.IsFileInUse(filename))
                {
                    Imagen = await Task.Run(() => File.ReadAllBytes(filename));
                }
            }
        }

        #endregion

        #region Funciones

        /// <summary>
        /// Funcion que valida que ningun campo este vacio
        /// </summary>
        /// <returns></returns>
        private bool validar()
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(DescripcionCorta) || string.IsNullOrEmpty(DescripcionLarga) || Imagen == null)
                return false;
            else
                return true;
        }

        #endregion
    }
}
