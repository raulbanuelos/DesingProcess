using MahApps.Metro.Controls.Dialogs;
using Model.ControlDocumentos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.ControlDocumentos;
using View.Resources;

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

        private ValidacionDocumento _selectedValidacion;
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

        private bool _Enabled;
        public bool Enabled {
            get
            {
                return _Enabled;
            }
            set
            {
                _Enabled = value;
                NotifyChange("Enabled");
            }
        }

        private DialogService dialog = new DialogService();
        private int ID_tipo;
        private ObservableCollection<ValidacionDocumento> ListaAux;
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
        /// Comando para guardar relación de una validación con un tipo de documento
        /// </summary>
        public ICommand GuardarR
        {
            get
            {
                return new RelayCommand(o => guardarRelacion());
            }
        }

        /// <summary>
        /// Comando para eliminar varios registros de validación 
        /// </summary>
        public ICommand EliminarValidacion
        {
            get
            {
                return new RelayCommand(o => eliminarVal());
            }
        }

        /// <summary>
        ///Comando para quitar relación entre tipo de documento y validación
        /// </summary>
        public ICommand QuitarRelacion
        {
            get
            {
                return new RelayCommand(o => eliminarRelacion());
            }
        }

        public ICommand irNuevaValidacion
        {
            get
            {
                return new RelayCommand(o => NuevaValidacion());
            }
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Método que obtienen las validaciones de acuerdo al tipo de documento
        /// </summary>
        private void getValidacion()
        {
            //Si se seleccionó un tipo de documento
            if (_SelectedTipoDocumento != null)
            {
                ID_tipo = SelectedTipoDocumento.id_tipo;
                //Inicializamos las listas para mostrar
                InitComp(SelectedTipoDocumento.id_tipo);
                //Habilitamos los botones
                Enabled = true;
            }          
        }

        /// <summary>
        /// Método que inicializa la lista de validacion y lista de relación
        /// Muestra las validaciones que no contiene un tipo de documento
        /// </summary>
        /// <param name="idTipo"></param>
        private void InitComp(int idTipo)
        {
            //Obtenemos las relaciones de acuerdo al tipo de documento
            ListaR = DataManagerControlDocumentos.GetValidacion_Documento(idTipo);
            //Volvemos a inicializar las lista de validaciones
            ListaValidaciones = DataManagerControlDocumentos.GetValidaciones();

            //Iteramos la lista de Relaciones
            foreach (var item in ListaR)
            {
                //Iteramos la lista de validaciones
                foreach (var var in ListaValidaciones)
                {
                    //Si el objeto de la ListaR se encuentra en la lista de Validaciones
                    if (var.id_validacion == item.id_validacion)
                    {
                        //Borramos el objto de la lista
                        ListaValidaciones.Remove(var);
                        break;
                    }
                }
            }
        }
       
        /// <summary>
        /// Método que guarda la relación de validación y tipo de documento
        /// </summary>
        private async void guardarRelacion()
        {
            //Variable auxiliar que llevar el conteo de los registros guardados
            int cont = 0;

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgConfirmacion, setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
             {
                //Si se escogió tipo de documento
                if (_SelectedTipoDocumento != null)
                     {
                //Valída que se haya seleccionado por lo menos un objeto
                    if (ValidaSelected())
                    {
                        //Iteramos la lista
                        foreach (var item in ListaValidaciones)
                        {
                            //Si se seleccionó el objeto
                            if (item.selected)
                            {
                                //Buscamos si existe la relación
                                int val = DataManagerControlDocumentos.SearchValidacion(item.id_validacion, _SelectedTipoDocumento.id_tipo);

                                //Si no existe la relación
                                if (val == 0)
                                {
                                    ValidacionDocumento obj = new ValidacionDocumento();

                                    //Asiganmos los valores al objeto
                                    obj.id_validacion = item.id_validacion;
                                    obj.id_tipo = _SelectedTipoDocumento.id_tipo;

                                    //Guardamos la relación
                                    int id_vDocumento = DataManagerControlDocumentos.SetRelacion(obj);

                                    //Si se guardó correctamente
                                    if (id_vDocumento != 0)
                                    {
                                        //agregamos uno al contador
                                        cont++;
                                       
                                    }
                                }
                            }
                        }
                       
                        //Si se guardó la relación 
                        if (cont ==0)
                        {
                            //Inicializa la lista de validacion y de relación
                            InitComp(_SelectedTipoDocumento.id_tipo);
                        }
                    }
                    else
                    {
                        //Muestra mensaje de error
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgValidacion);
                    }
                }
                else
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgTipoDocumento);
                }
            }
        }

        /// <summary>
        /// Método para eliminar los objetos seleccionados de la lista de Validaciones
        /// </summary>
        private async void eliminarVal()
        {
            //Variable auxiliar que llevar el conteo de los registros eliminados
            int cont = 0;
            // Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgConfirmacion, setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                //Verifica que por los menos se haya seleccionado un objeto
                if (ValidaSelected())
                {
                    //Iteramos la Lista
                    foreach (var item in ListaValidaciones)
                    {
                        //Si fue seleccionado el objeto
                        if (item.selected)
                        {
                            //Obtiene todas las relaciones de una validación
                            ListaAux = DataManagerControlDocumentos.GetR_Val_Tipo(item.id_validacion);

                            //iteramos la lista auxiliar
                            foreach (var aux in ListaAux)
                            {
                                //Eliminamos la relación de tipo de documento y validación
                                int deleR = DataManagerControlDocumentos.DeleteTR_Validacion(aux.id_val_tipo);
                            }

                            //Eliminamos el registro de validación
                            int delV = DataManagerControlDocumentos.DeleteValidacion(item.id_validacion);

                            //Si el registro se elimino correctamente
                            if (delV != 0)
                            {
                                //Sumamos el contador
                                cont++;
                            }
                        }
                    }

                    //Inicializamos la lista de Validaciones
                    ListaValidaciones = DataManagerControlDocumentos.GetValidaciones();

                    //Si el contador es cero, no sé eliminó ningún registro
                    if (cont == 0)
                    {
                        //Muestra mendaje de error
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorEliminarValidacion);
                    }
                }
                else
                {
                    //No hay ninguna validación seleccionada
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSeleccionarValidacion);
                }
            }
        }

        /// <summary>
        /// Método que elimina una relación entre tipo de documento y validación
        /// </summary>
        private async void eliminarRelacion()
        {
            //Variable auxiliar que llevar el conteo de las relaciones eliminadas
            int aux = 0;
            // Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgQuitarRelacion, setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                //Verifica que la lista tenga por lo menos un objeto seleccionado
                if (ValidaListaR())
                {
                    //Recorre la lista
                    foreach (var item in ListaR)
                    {
                        //Si el objeto está seleccionado
                        if (item.selected)
                        {
                            //Elimina la relación
                            int resul = DataManagerControlDocumentos.DeleteTR_Validacion(item.id_val_tipo);

                            //Si se eliminó correctamente la relación 
                            if (resul !=0)
                            {
                                //Sumamos la variable
                                aux++;
                            }
                        }
                    }

                    //Inicializamos las listas, para mostrarlas 
                    InitComp(_SelectedTipoDocumento.id_tipo);
                    //Si no se elminó ninguno de los registros que se seleccionaron
                    if (aux ==0)
                    {
                        //Muestra mensaje de error
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorEliminarRelacion);
                    }
                }
                else
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgSeleccionarValidacion);
                }
            }
        }

        private void NuevaValidacion()
        {
            FrmNuevo_ValidacionDocumento frm = new FrmNuevo_ValidacionDocumento();
            NuevoValidacionDocumento_VM context = new NuevoValidacionDocumento_VM();
            frm.DataContext = context;
            frm.ShowDialog();

            if(_SelectedTipoDocumento !=null)
            //Inicializamos las listas, para mostrarlas 
            InitComp(_SelectedTipoDocumento.id_tipo);
            else
                ListaValidaciones = DataManagerControlDocumentos.GetValidaciones();
        }

        /// <summary>
        /// Método que valída que la lista de Validaciones tenga por lo menos un objeto seleccionado
        /// </summary>
        /// <returns></returns>
        private bool ValidaSelected()
        {
            //Contador, guarda el número de objetos seleccionados
            int aux=0;
            //Itera la lista de Validaciones
            foreach (var item in ListaValidaciones)
            {
                //Si el objeto está seleccionado, sumammos uno al auxiliar
                if (item.selected)
                    aux++;
            }
            //Si no hay ningún objeto seleccionado, retornamos falso, de lo contrario retornamos verdadero
            if (aux == 0)
                return false;
            else
                return true;
        }

        /// <summary>
        /// Método que válida la lista de Relaciones
        /// </summary>
        /// <returns></returns>
        private bool ValidaListaR()
        {
            //Contador, guarda el número de objetos seleccionados
            int aux = 0;
            //Itera la lista de Relaciones
            foreach (var item in ListaR)
            {
                //Si el objeto está seleccionado, sumammos uno al auxiliar
                if (item.selected)
                    aux++;
            }
            //Si no hay ningún objeto seleccionado, retornamos falso, de lo contrario retornamos verdadero
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
