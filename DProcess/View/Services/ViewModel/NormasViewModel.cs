using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Resources;
using System.Windows;
using View.Forms.Routing;

namespace View.Services.ViewModel
{
    public class NormasViewModel : INotifyPropertyChanged
    {
        #region Propiedades

        private ObservableCollection<DO_Norma> _ListaNormas;
        public ObservableCollection<DO_Norma> ListaNormas
        {
            get
            {
                return _ListaNormas;
            }
            set
            {
                _ListaNormas = value;
                NotifyChange("ListaNormas");
            }
        }

        private DO_Norma _NormaSeleccionada;
        public DO_Norma NormaSeleccionada
        {
            get
            {
                return _NormaSeleccionada;
            }
            set
            {
                _NormaSeleccionada = value;
                NotifyChange("NormaSeleccionada");
            }
        }

        public bool InsertarNuevaNorma = false;

        private bool _enabledEliminar;
        public bool EnabledEliminar
        {
            get
            {
                return _enabledEliminar;
            }
            set
            {
                _enabledEliminar = value;
                NotifyChange("EnabledEliminar");
            }
        }

        #endregion

        #region Atributos

        private DO_Norma model;

        private DialogService dialogService;

        #endregion

        #region Constructores

        public NormasViewModel()
        {
            model = new DO_Norma();
            DialogService dialog = new DialogService();
            InsertarNuevaNorma = true;
            ListaNormas = DataManager.GetAllNormas();
        }

        public NormasViewModel(DO_Norma Model)
        {
            //Mapeamos el valor del modelo recibido al atributo de la clase.
            model = Model;

            dialogService = new DialogService();
        }

        #endregion

        #region Commands

        public ICommand Guardarnorma
        {
            get
            {
                return new RelayCommand(a => guardarnorma());
            }
        }

        public ICommand Editarnorma
        {
            get
            {
                return new RelayCommand(o => editarnorma());
            }
        }

        public ICommand Eliminarnorma
        {
            get
            {
                return new RelayCommand(a => eliminarnorma());
            }
        }

        public ICommand IrNuevaNorma
        {
            get
            {
                return new RelayCommand(a => irAddNorma());
            }
        }

        #endregion

        #region Propiedades del modelo

        public int idNorma
        {
            get
            {
                return model.idNorma;
            }
            set
            {
                model.idNorma = value;
                NotifyChange("idNorma");
            }
        }

        public string especificacion
        {
            get
            {
                return model.especificacion;
            }
            set
            {
                model.especificacion = value;
                NotifyChange("especificacion");
            }
        }

        public string descripcionCorta
        {
            get
            {
                return model.descripcionCorta;
            }
            set
            {
                model.descripcionCorta = value;
                NotifyChange("descripcionCorta");
            }
        }

        public string descripcionLarga
        {
            get
            {
                return model.descripcionLarga;
            }
            set
            {
                model.descripcionLarga = value;
                NotifyChange("descripcionLarga");
            }
        }

        #endregion

        #region Métodos

        private void irAddNorma()
        {
            NormasViewModel vm = new NormasViewModel();

            WAddNorma ventana = new WAddNorma();
            ventana.DataContext = vm;

            ventana.ShowDialog();

            //Obtenemos todas las propiedades.
            ListaNormas = DataManager.GetAllNormas();
        }

        /// <summary>
        /// Método para editar una norma seleccionada
        /// </summary>
        public void editarnorma()
        {
            if (NormaSeleccionada.idNorma != 0)
            {

                WAddNorma Form = new WAddNorma();
                
                NormasViewModel Data = new NormasViewModel(NormaSeleccionada);
                Data.EnabledEliminar = true;

                Form.DataContext = Data;
                Form.ShowDialog();

                //Actualizamos los valores de la lista
                ListaNormas = DataManager.GetAllNormas();

            }
        }

        /// <summary>
        /// Método para guardar los cambios de una norma
        /// </summary>
        public async void guardarnorma()
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
                    if (InsertarNuevaNorma == true)
                    {
                        r = DataManager.SetNorma(model);
                    }
                    else
                    {
                        r = DataManager.UpdateNorma(model);
                    }

                    if (r > 0)
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
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }
        }

        /// <summary>
        /// Método para eliminar una norma
        /// </summary>
        public async void eliminarnorma()
        {
            if (model.idNorma != 0)
            {
                DialogService dialog = new DialogService();
                MetroDialogSettings settings = new MetroDialogSettings();

                settings.AffirmativeButtonText = StringResources.lblYes;
                settings.NegativeButtonText = StringResources.lblNo;

                MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEliminarRegistro, settings, MessageDialogStyle.AffirmativeAndNegative);

                if (MessageDialogResult.Affirmative == result)
                {
                    int e = DataManager.DeleteNorma(model.idNorma);

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

        #region Funciones

        /// <summary>
        /// Funcion que valida que ningun campo este vacio
        /// </summary>
        /// <returns></returns>
        private bool validar()
        {
            if (string.IsNullOrEmpty(especificacion) || string.IsNullOrEmpty(descripcionCorta) || string.IsNullOrEmpty(descripcionLarga))
                return false;
            else
                return true;
        }

        #endregion
    }
}
