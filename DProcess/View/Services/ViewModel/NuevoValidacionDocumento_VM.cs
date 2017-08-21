using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace View.Services.ViewModel
{
   public class NuevoValidacionDocumento_VM : INotifyPropertyChanged
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
        private string _validacion;
        public string Validacion
        {
            get
            {
                return _validacion;
            }
            set
            {
                _validacion = value;
                NotifyChange("Validacion");
            }

        }

        private string _descripcion;
        public string Descripcion
        {
            get
            {
                return _descripcion;
            }
            set
            {
                _descripcion = value;
                NotifyChange("Descripcion");
            }

        }

        private DialogService dialog = new DialogService();
        #endregion

        #region Metodos

        /// <summary>
        /// Comando para guardar validaciones de documento
        /// </summary>
        public ICommand GuardarValidacion
        {
            get
            {
                return new RelayCommand(o => guardarValidacion());
            }
        }

        /// <summary>
        /// Método que guarda un registro en la tabla de validaciones
        /// </summary>
        private async void guardarValidacion()
        {
            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage("Attention", "¿Desea guardar los cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);

            //Si el resultado es afirmativo
            if (result == MessageDialogResult.Affirmative)
            {
                //Si no están vacíos los campos
                if (!string.IsNullOrEmpty(_descripcion) && !string.IsNullOrEmpty(_validacion))
                {

                    ValidacionDocumento obj = new ValidacionDocumento();

                    //Asiganmos los valores al objeto
                    obj.validacion_descripcion = _descripcion.ToUpper();
                    obj.validacion_documento = _validacion.ToUpper();

                    //Verificamos que no se repita la validación de documento
                    int idV = DataManagerControlDocumentos.GetIDValidacion(_validacion);

                    //Si la validación no existe                
                    if (idV == 0)
                    {
                        //Ejecutamos el métoodo para guardar el registro
                        int validacion = DataManagerControlDocumentos.SetValidacion(obj);

                        //Si se guardo correctamente el registro
                        if (validacion != 0)
                        {
                            //Ejecutamos el método para enviar un mensaje de confirmación al usuario.
                            await dialog.SendMessage("Información", "Los cambios fueron guardados exitosamente...");

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
                            //Muestra mensaje de error
                            await dialog.SendMessage("Alerta", "Error al registrar la validación");
                        }
                    }
                    else
                    {
                        await dialog.SendMessage("Alerta", "La validación ya existe ");
                    }
                }
                else
                {
                    await dialog.SendMessage("Alerta", "Se debe llenar todos los campos");
                }
            }
        }


        #endregion
    }
}
