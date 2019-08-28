using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using View.Forms.Routing;
using View.Resources;

namespace View.Services.ViewModel
{
    public class PropiedadBoolViewModel : INotifyPropertyChanged
    {
        #region Atributos
        private PropiedadBool model;
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
        /// Booleano que representa el valor de la propiedad. (true:false)
        /// </summary>
        public bool Valor {
            get {
                return model.Valor;
            }
            set {
                model.Valor = value;
                NotifyChange("Valor");
            }
        }

        /// <summary>
        /// Cadena que representa una descripción larga de la propiedad
        /// </summary>
        public string DescripcionLarga {
            get {
                return model.DescripcionLarga;
            }
            set {
                model.DescripcionLarga = value;
                NotifyChange("DescripcionLarga");
            }
        }

        /// <summary>
        /// Cadena que representa una descripción corta de la propiedad.
        /// </summary>
        public string DescripcionCorta {
            get {
                return model.DescripcionCorta;
            }
            set
            {
                model.DescripcionCorta = value;
                NotifyChange("DescripcionCorta");
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

        #region Propiedades

        private ObservableCollection<PropiedadBool> _ListaPropiedades;
        public ObservableCollection<PropiedadBool> ListaPropiedades
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

        private PropiedadBool _PropiedadSeleccionada;
        public PropiedadBool PropiedadSeleccionada
        {
            get
            {
                return _PropiedadSeleccionada;
            }
            set
            {
                _PropiedadSeleccionada = value;
                NotifyChange("PropiedadSeleccionada");
            }
        }

        public bool InsertarNuevoComponente = false;

        #endregion

        #region Contructors

        /// <summary>
        /// Constructor para modificar los datos de una propiedad
        /// </summary>
        /// <param name="propiedad"></param>
        public PropiedadBoolViewModel(PropiedadBool propiedad)
        {
            model = propiedad;
        }

        /// <summary>
        /// Constructor cuando se requieren administrar todas las propiedades.
        /// </summary>
        public PropiedadBoolViewModel(bool ct1)
        {
            //obtenemos todas las propiedades
            ListaPropiedades = DataManager.GetAllPropiedadesBool();
        }

        /// <summary>
        /// Constructor para insertar una nueva propiedad
        /// </summary>
        public PropiedadBoolViewModel()
        {
            model = new PropiedadBool();
            DialogService dialog = new DialogService();
            InsertarNuevoComponente = true;
        }

        #endregion

        #region Comandos

        public ICommand EditarPropiedad
        {
            get
            {
                return new RelayCommand(a=> _EditarPropiedad());
            }
        }

        public ICommand NewPropiedad
        {
            get
            {
                return new RelayCommand(a => _NewPropiedad());
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
        /// Método para editar la información de una propiedad
        /// </summary>
        public void _EditarPropiedad()
        {
            if (PropiedadSeleccionada.idPropiedad != 0)
            {

                WPropiedadBool Form = new WPropiedadBool();
                PropiedadBoolViewModel Data = new PropiedadBoolViewModel(PropiedadSeleccionada);

                Form.DataContext = Data;
                Form.ShowDialog();

                //Actualizamos los valores de la lista
                ListaPropiedades = DataManager.GetAllPropiedadesBool();

            }
        }

        /// <summary>
        /// Método para insertar una nueva propiedad
        /// </summary>
        public void _NewPropiedad()
        {
            WPropiedadBool form = new WPropiedadBool();
            PropiedadBoolViewModel data = new PropiedadBoolViewModel();
            form.DataContext = data;

            form.ShowDialog();

            //Actualizamos los valores de la lista
            ListaPropiedades = DataManager.GetAllPropiedadesBool();
        }

        /// <summary>
        /// Método para guardar la informacion de una propiedad
        /// </summary>
        public async void _GuardarPropiedad()
        {
            int r = 0;

            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            if (validar())
            {
                MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgConfirmacion, setting, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    if (InsertarNuevoComponente == true)
                    {
                        r = DataManager.InsertarNuevaPropiedadBool(model);
                    }
                    else
                    {
                        r = DataManager.UpdatePropiedadBool(model);
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
                //si faltan campos por llenar
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }
        }

        /// <summary>
        /// Método para eliminar un registro
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
                    int e = DataManager.DeletePropiedadBool(model.idPropiedad);

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

                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                    }


                }
            }
        }

        /// <summary>
        /// Método para insertar una imagen a la propiedad
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
        /// Funcion que valida que todos los campos esten llenos
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
