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
using System.Globalization;

namespace View.Services.ViewModel
{
    public class NuevoTipoDocumentoVM : INotifyPropertyChanged
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

        #region propiedades

        private string _tipoDocumento;
        public string TipoDocumento
        {
            get
            {
                return _tipoDocumento;
            }
            set
            {
                _tipoDocumento = value;
                NotifyChange("TipoDocumento");
            }
        }

        private string _abreviatura;
        public string Abreviatura
        {
            get
            {
                return _abreviatura;
            }
            set
            {
                _abreviatura = value;
                NotifyChange("Abreviatura");
            }
        }
        private string numMatriz;
        public string NumMatriz {
            get
            {
                return numMatriz;
            }
            set
            {
                numMatriz = value;
                NotifyChange("NumMatriz");
            }
        }
        #endregion

        #region Icommands
        /// <summary>
        /// Comando para guardar el tipo de documento
        /// </summary>
        public ICommand GuardarTipo
        {
            get
            {
                return new RelayCommand(o => guardar());
            }
        }
        /// <summary>
        /// Método que valida si el tipo de documento no existe
        /// Guarda el nuevo tipo de documento
        /// </summary>
        private async void guardar()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage("Attention", "¿Desea guardar los cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);

            //Si el resultado es afirmativo
            if (result == MessageDialogResult.Affirmative)
            {
                if (!string.IsNullOrEmpty(_abreviatura) & !string.IsNullOrEmpty(_tipoDocumento) & !string.IsNullOrWhiteSpace(_tipoDocumento) & !string.IsNullOrWhiteSpace(_abreviatura))
                {
                    if (_abreviatura.Length <= 6)
                    {
                        //Creamos un objeto de tipo TipoDcouemtno
                        TipoDocumento obj = new TipoDocumento();

                        //Asiganmos los valores al objeto
                        obj.tipo_documento = _tipoDocumento.ToUpper();
                        obj.abreviatura = _abreviatura.ToUpper();
                        obj.num_matriz = numMatriz;
                        obj.fecha_actualizacion = DataManagerControlDocumentos.Get_DateTime();
                        obj.fecha_creacion = DataManagerControlDocumentos.Get_DateTime();

                        //Validamos que no exista el tipo de documento
                        int val = DataManagerControlDocumentos.ValidateTipo(obj);

                        //Si el tipo no existe
                        if (val == 0)
                        {
                            //Ejecutamos el método para insertar el tipo de documento
                            int n = DataManagerControlDocumentos.SetTipo(obj);

                            if (n != 0)
                            {
                                await dialog.SendMessage("Información", "Los cambios fueron guardados exitosamente..");
                                //Obtenemos la ventana actual.
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
                                //Si existe un error al dar de alta el tipo
                                await dialog.SendMessage("RGP: Alerta", "Error al registrar el tipo de documento");
                            }
                        }
                        else
                        {
                            //Si el documento existe
                            await dialog.SendMessage("RGP: Alerta", "El tipo de documento ya existe..");
                        }
                    }
                    else
                    {
                        await dialog.SendMessage("Alerta", "La abreviatura debe tener menos de 7 caracteres..");
                    }
                }
                else
                {
                    //Si no se llenaron todos los campos
                    await dialog.SendMessage("RGP: Alerta", "Se debe llenar todos los campos");
                }
            }
        }
        #endregion
    }
}
