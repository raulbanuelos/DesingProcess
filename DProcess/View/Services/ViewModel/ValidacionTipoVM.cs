using MahApps.Metro.Controls.Dialogs;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.Services.ViewModel
{
    public class ValidacionTipoVM : INotifyPropertyChanged
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
        private ObservableCollection<TipoDocumento> _listaTipo;
        public ObservableCollection<TipoDocumento> ListaTipoDocumento {
            get
            {
                return _listaTipo;
            }
            set
            {
                _listaTipo = value;
                NotifyChange("ListaTipoDocumento");
            }
        }

        private TipoDocumento _SelectedTipoDocumento;
        public TipoDocumento SelectedTipoDocumento {
            get
            {
                return _SelectedTipoDocumento;
            }
            set
            {
                _SelectedTipoDocumento = value;
                NotifyChange("SelectedTipoDocumento");
            }
        }

        private ObservableCollection<ValidacionDocumento> _listaValidaciones;
        public ObservableCollection<ValidacionDocumento> ListaValidaciones
        {
            get
            {
                return _listaValidaciones;
            }
            set
            {
                _listaValidaciones = value;
                NotifyChange("ListaValidaciones");
            }
        }

        private ObservableCollection<ValidacionDocumento> _listaR;
        public ObservableCollection<ValidacionDocumento> ListaR
        {
            get
            {
                return _listaR;
            }
            set
            {
                _listaR = value;
                NotifyChange("ListaR");
            }
        }

        public string _validacion;
        public string Validacion {
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

        public string _descripcion;
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

        public ValidacionDocumento _selectedValidacion;
        public ValidacionDocumento SelectedValidacion
        {
            get
            {
                return _selectedValidacion;
            }
            set
            {
                _selectedValidacion = value;
                NotifyChange("SelectedValidacion");
            }

        }

        private DialogService dialog = new DialogService();
        #endregion

        #region Comandos

        /// <summary>
        /// Comando para obtener las validaciones de acuerdo al tipo de documento
        /// </summary>
        public ICommand GetValidacion
        {
            get
            {
                return new RelayCommand(o => getValidacion());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand GuardarValidacion
        {
            get
            {
                return new RelayCommand(o => guardarValidacion());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand GuardarR
        {
            get
            {
                return new RelayCommand(o => guardarRelacion());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand EliminarItem
        {
            get
            {
                return new RelayCommand(o => eliminarItem());
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método que obtienen las validaciones de acuerdo al tipo de documento
        /// </summary>
        private void getValidacion()
        {
            if (_SelectedTipoDocumento != null)
            {
                ListaR = DataManagerControlDocumentos.GetValidacion_Documento(SelectedTipoDocumento.id_tipo);
                ListaValidaciones.Clear();
                ListaValidaciones = DataManagerControlDocumentos.GetV_Tipo(SelectedTipoDocumento.id_tipo);

                //foreach (var validacion in ListaValidaciones)
                //{

                //    foreach (var item in ListaR)
                //    {

                //        if (validacion.id_validacion == item.id_validacion)
                //        {
                //            validacion.selected = true;
                //        }
                //    }
                //}
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

            if (result == MessageDialogResult.Affirmative)
            {
                if (!string.IsNullOrEmpty(_descripcion) && !string.IsNullOrEmpty(_validacion))
                {
                    ValidacionDocumento obj = new ValidacionDocumento();

                    obj.validacion_descripcion = _descripcion;
                    obj.validacion_documento = _validacion;

                    // int idV = DataManagerControlDocumentos.GetIDValidacion(_validacion);

                    //Si la validación no existe

                    int validacion = DataManagerControlDocumentos.SetValidacion(obj);

                    if (validacion != 0)
                    {
                        ListaValidaciones = DataManagerControlDocumentos.GetValidaciones();
                    }

                }
            }
            else
            {
                await dialog.SendMessage("Alerta", "Se debe llenar todos los campos");
            }
        }
 

        /// <summary>
        /// Método que guarda la relación de validación y tipo de documento
        /// </summary>
        private async void guardarRelacion()
        {
            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage("Attention", "¿Desea guardar los cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
             {
                if (_SelectedTipoDocumento != null)
                     {
                
                    if (ValidaSelected())
                    {
                        foreach (var item in ListaValidaciones)
                        {
                            if (item.selected)
                            {
                                int val = DataManagerControlDocumentos.SearchValidacion(item.id_validacion, _SelectedTipoDocumento.id_tipo);

                                //Si no existe la relación
                                if (val == 0)
                                {
                                    ValidacionDocumento obj = new ValidacionDocumento();

                                    obj.id_validacion = item.id_validacion;
                                    obj.id_tipo = _SelectedTipoDocumento.id_tipo;

                                    int id_vDocumento = DataManagerControlDocumentos.SetRelacion(obj);

                                    if (id_vDocumento != 0)
                                    {

                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        await dialog.SendMessage("Alerta", "Se debe seleccionar al menos una validación");
                    }
                }
                else
                {
                    await dialog.SendMessage("Alerta", "Se debe seleccionar un tipo de documento");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private async void eliminarItem()
        {
           // Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage("Attention", "¿Desea guardar los cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                if (SelectedValidacion !=null)
                {
                    
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private bool ValidaSelected()
        {
            int aux=0;
            //Itera la lista de Validaciones
            foreach (var item in ListaValidaciones)
            {

                if (item.selected)
                    aux++;
            }
            if (aux == 0)
                return false;
            else
                return true;
        }
        #endregion
        #region Constructor

        public ValidacionTipoVM()
        {
            _listaTipo = DataManagerControlDocumentos.GetTipo();
            ListaValidaciones = DataManagerControlDocumentos.GetValidaciones();
        }
        #endregion
    }
}
