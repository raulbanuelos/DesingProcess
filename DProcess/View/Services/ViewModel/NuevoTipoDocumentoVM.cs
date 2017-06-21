﻿using MahApps.Metro.Controls;
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
        #endregion

        #region Icommands

        public ICommand GuardarTipo
        {
            get
            {
                return new RelayCommand(o => guardar());
            }
        }

        private async void guardar()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage("Attention", "¿Deseas guardar los cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                if (!string.IsNullOrEmpty(_abreviatura) & !string.IsNullOrEmpty(_tipoDocumento))
                {
                    TipoDocumento obj = new TipoDocumento();

                    obj.tipo_documento = _tipoDocumento;
                    obj.abreviatura = _abreviatura;
                    obj.fecha_actualizacion = DateTime.Now;
                    obj.fecha_creacion = DateTime.Now;

                    int val = DataManagerControlDocumentos.ValidateTipo(obj);

                    if (val == 0)
                    {
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
                            await dialog.SendMessage("RGP: Alerta", "Error al registrar el tipo de documento");
                        }
                    }
                    else
                    {
                        await dialog.SendMessage("RGP: Alerta", "El tipo de documento ya existe..");
                    }
                }
                else
                {
                    await dialog.SendMessage("RGP: Alerta", "Se debe llenar todos los campos");
                }
            }
        }
        #endregion
    }
}