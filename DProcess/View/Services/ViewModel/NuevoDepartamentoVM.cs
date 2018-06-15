using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using View.Resources;

namespace View.Services.ViewModel
{
   public class NuevoDepartamentoVM : INotifyPropertyChanged
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


        private string _nombreDep;
        public string NombreDep
        {
            get
            {
                return _nombreDep;
            }
            set
            {
                _nombreDep = value;
                NotifyChange("NombreDep");
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

        #region Comandos

        /// <summary>
        /// Comando para guardar un nuevo departamento
        /// </summary>
        public ICommand Guardar
        {
            get
            {
               return new RelayCommand(o => guardarDepartamento());
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que valida si el departamento ingresado no existe, guarda el registro del departamento
        /// </summary>
        public async void guardarDepartamento()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgConfirmacion, setting, MessageDialogStyle.AffirmativeAndNegative);

            //Si el resultado es afirmativo
            if (result == MessageDialogResult.Affirmative)
            {
                //Si los campos son diferentes de nulo vacío
                if (!string.IsNullOrEmpty(_nombreDep) & !string.IsNullOrEmpty(_abreviatura) & !string.IsNullOrWhiteSpace(_abreviatura) & !string.IsNullOrWhiteSpace(_nombreDep))
                {
                    if (_abreviatura.Length <= 6)
                    {
                        //Creamos un objeto de tipo departamento
                        Departamento objDep = new Departamento();

                        //Asiganmos los valores al objeto.
                        objDep.nombre_dep = _nombreDep.ToUpper();
                        objDep.Abreviatura = _abreviatura.ToUpper();
                        objDep.fecha_actualizacion = DataManagerControlDocumentos.Get_DateTime();
                        objDep.fecha_creacion = DataManagerControlDocumentos.Get_DateTime();

                        //Ejecuta el método para validar si existe el departamento
                        int val = DataManagerControlDocumentos.ValidateDepartamento(objDep);
                        //Si no existe
                        if (val == 0)
                        {
                            //Ejecutamos el método, el resultado lo asignamos a una variable
                            int id = DataManagerControlDocumentos.SetDepartamento(objDep);

                            //si se inserto correctamente 
                            if (id != 0)
                            {
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgAgregarDepartamento);

                                //Obtenemos para pantalla
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
                                //Si hubo error al registrar el departamento
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorAgregarDepartamento);
                            }
                        }
                        else
                        {
                            //Si el nombre del departamento existe
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorDepartamentoExistente);
                        }
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorAbreviatura);
                    }
                }

                else
                {
                    //Si los campos están vacíos
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
                }
            }
        }

        #endregion


    }
}
