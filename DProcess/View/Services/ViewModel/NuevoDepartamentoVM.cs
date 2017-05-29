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

        #region

        public ICommand Guardar
        {
            get
            {
               return new RelayCommand(o => guardarDepartamento());
            }
        }

        public async void guardarDepartamento()
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

                if (!string.IsNullOrEmpty(_nombreDep) & !string.IsNullOrEmpty(_abreviatura))
                {
                    //Creamos un objeto de tipo departamento
                    Departamento objDep = new Departamento();

                    //Asiganmos los valores al objeto.
                    objDep.nombre_dep = _nombreDep;
                    objDep.Abreviatura = _abreviatura;
                    objDep.fecha_actualizacion = DateTime.Now;
                    objDep.fecha_creacion = DateTime.Now;

                    int val = DataManagerControlDocumentos.ValidateDepartamento(objDep);

                    if (val == 0)
                    {
                        //Ejecutamos el método, el resultado lo asignamos a una variable
                        int id = DataManagerControlDocumentos.SetDepartamento(objDep);

                        //si se inserto correctamente 
                        if (id != 0)
                        {
                            await dialog.SendMessage("Información", "Departamento agregado..");

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
                            await dialog.SendMessage("Alerta", "No se pudo agregar el departamento..");
                        }
                    }
                    else
                    {
                        await dialog.SendMessage("Alerta", "El nombre de departamento ya existe..");
                    }
                }
                else
                {
                    await dialog.SendMessage("Alerta", "De debe de llenar todos los campos..");
                }
            }
        }

        #endregion
    }
}
