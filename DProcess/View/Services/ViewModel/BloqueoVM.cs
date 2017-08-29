using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;

namespace View.Services.ViewModel
{
    public class BloqueoVM : INotifyPropertyChanged
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

        private DateTime _fechaInicio= DataManagerControlDocumentos.Get_DateTime();
        public DateTime FechaInicio {
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

        private string _observaciones;
        public string Observaciones {
            get
            {
                return _observaciones;
            }
            set
            {
                _observaciones = value;
                NotifyChange("Observaciones");
            }
        }

        private bool bttnGuardar;
        public bool BttnGuardar {
            get
            {
                return bttnGuardar;
            }
            set
            {
                bttnGuardar = value;
                NotifyChange("BttnGuardar");
            }
        }

        private bool _isEnabled;
        public bool IsEnabled
        {
            get
            {
                return _isEnabled;
            }
            set
            {
                _isEnabled = value;
                NotifyChange("IsEnabled");
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
        private int id_bloqueo { get; set; }
        
        private DialogService dialog = new DialogService();
        #endregion

        #region Constructor

        public BloqueoVM()
        {
            Bloqueo obj = new Bloqueo();

            //Método que obtiene un registro si se encuentra activo
            obj = DataManagerControlDocumentos.GetBloqueo();

            //Si existe un registro activo
            if (obj.id_bloqueo != 0)
            {
                //Asigna los valores para mostrarlos
                FechaInicio = obj.fecha_inicio;
                FechaFin = obj.fecha_fin;
                Observaciones = obj.observaciones;
                id_bloqueo = obj.id_bloqueo;

                //Esconde el botón guardar, muestra botón modificar y desbloquear
                BttnGuardar = false;
                IsEnabled = true;
            }
            else
            {   //Si no hay ningun registro en estado bloqueado
                //Muestra el botón de guardar
                IsEnabled = false;
                BttnGuardar = true;
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Comando para guardar el reigstro de bloqueo
        /// </summary>
        public ICommand GuardarBloqueo
        {
            get
            {
                return new RelayCommand(o => guardar());
            }
        }

        /// <summary>
        /// Método que guardar un nuevo registro de bloqueo
        /// </summary>
        private async void guardar()
        {
            //Valida los campos no están vacíos
            if (Valida())
            {  
                //Declaramos un objeto de tipo Bloqueo
                Bloqueo obj = new Bloqueo();

                //Asignamos los valores 
                obj.fecha_inicio = _fechaInicio;
                obj.fecha_fin = _fechaFin;
                obj.observaciones = _observaciones;

                //Ejecutamos el método para dar de alta el bloqueo
                int id = DataManagerControlDocumentos.SetBloqueo(obj);

                //Si se agregó correctamente el registro
                if (id != 0)
                {
                    await dialog.SendMessage("Información", "Los datos fueron guardados correctamente...");

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
                    //Si hubo error al dar de alta
                    await dialog.SendMessage("Alert", "Error al registrar el bloqueo...");
                }

            }
            else
            {
                //Si están vacíos los campos
                await dialog.SendMessage("Información", "Se deben de llenar todos los campos...");
            }
        }

        /// <summary>
        /// Comando que ejecuta el método para modificar un registro de bloqueo
        /// </summary>
        public ICommand Modificar
        {
            get
            {
                return new RelayCommand(o=> modificar());
            }
        }

        /// <summary>
        /// Método que modifica el registro que se encuentre en estado bloqueado
        /// </summary>
        private async void modificar()
        {
            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage("Attention", "¿Desea guardar los cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                //Valida que todos los campos estén llenos
                if (Valida() & id_bloqueo != 0)
                {

                    //Declaramos un objeto de tipo Bloqueo
                    Bloqueo obj = new Bloqueo();

                    //Asignamos los valores, sólo se modifica el rango y observaciones
                    obj.fecha_inicio = _fechaInicio;
                    obj.fecha_fin = _fechaFin;
                    obj.observaciones = _observaciones;
                    obj.id_bloqueo = id_bloqueo;

                    //Ejecutamos el método para modificar el registro
                    int update = DataManagerControlDocumentos.UpdateBloqueo(obj);

                    //Si se modificó correctamente
                    if (update != 0)
                    {
                        await dialog.SendMessage("Información", "Los cambios fueron guardados correctamente...");

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
                        //Si hubo error al realizar la modificación
                        await dialog.SendMessage("Alert", "Error al modificar el registro...");
                    }

                }
                else
                {
                    //Si los campos se encuentran vacíos
                    await dialog.SendMessage("Información", "Se debe de llenar todos los campos...");
                }
            }
        }

        /// <summary>
        /// Comando que ejeucta el método para desbloquear el sistema
        /// </summary>
        public ICommand Desbloquear
        {
            get
            {
                return new RelayCommand(o => desbloquear());
            }
        }

        /// <summary>
        /// Método que desbloquea el sistema
        /// </summary>
        private async void desbloquear()
        {

            if (id_bloqueo != 0)
            {
                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = "SI";
                setting.NegativeButtonText = "NO";

                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialog.SendMessage("Attention", "¿Desea desbloquear el sistema?", setting, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    //Declaramos un objeto de tipo Bloqueo
                    Bloqueo obj = new Bloqueo();

                    //Asignamos los valores
                    obj.id_bloqueo = id_bloqueo;

                    //Ejecutamos el método para modificar el estado a desbloqueado
                    int desbloq = DataManagerControlDocumentos.UpdateEstado(obj);

                    //Si se realizó el cambio
                    if (desbloq != 0)
                    {
                        await dialog.SendMessage("Información", "Los cambios fueron realizados correctamente...");

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
                        await dialog.SendMessage("Alert", "Error al realizar los cambios...");
                    }
                }
            }
        }

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
        /// Método que asigna la fecha de inicio que se seleccionó a la fecha final
        /// </summary>
        private void cambiaFecha()
        {
            FechaFin = FechaInicio;
        }

        //Método que valida los campos no estén vacíos
        private bool Valida()
        {
            if (_fechaFin != null & _fechaInicio != null & !string.IsNullOrEmpty(_observaciones))
                return true;
            else
                return false;
        }
        #endregion
    }
}
