using Encriptar;
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
using System.Windows;
using MahApps.Metro.Controls;
using System.Windows.Controls;
using View.Forms.Tooling;
using Model.Interfaces;
using Spring.Context;
using Spring.Context.Support;

namespace View.Services.ViewModel
{
    public class NuevoMaestroHerramental_VM : INotifyPropertyChanged
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

        #region Atributtes
        public Usuario usuario;
        DialogService dialog = new DialogService();
        Encriptacion encriptar = new Encriptacion();
        IApplicationContext ctx;
        XmlApplicationContext file;

        #endregion

        #region Propiedades
        private string codigo;
        public string Codigo { get
            {
                return codigo;
            }
            set
            {
                codigo = value;
                NotifyChange("Codigo");
            }
        }

        private string descripcion;
        public string Descripcion { get
            {
                return descripcion;
            }
            set
            {
                descripcion = value;
                NotifyChange("Descripcion");
            } }

        private ObservableCollection<ClasificacionHerramental> _ListaClasificacion;
        public ObservableCollection<ClasificacionHerramental> ListaClasificacion { get
            {
                return _ListaClasificacion;
            }
            set
            {
                _ListaClasificacion = value;
                NotifyChange("ListaClasificacion");
            }
        }

        private ObservableCollection<Plano> _ListaPlano;
        public ObservableCollection<Plano> ListaPlano
        {
            get
            {
                return _ListaPlano;
            }
            set
            {
                _ListaPlano = value;
                NotifyChange("ListaPlano");
            }
        }

        private ClasificacionHerramental _SelectedClasificacion;
        public ClasificacionHerramental SelectedClasificacion
        { get
            {
                return _SelectedClasificacion;
            }
            set
            {
                _SelectedClasificacion = value;
                NotifyChange("SelectedClasificacion");
            }
        }

        private Plano _SelectedPlano;
        public Plano SelectedPlano { get
            {
                return _SelectedPlano;
            }
            set
            {
                _SelectedPlano = value;
                NotifyChange("SelectedPlano");
            }
        }

        private bool _IsSelected;
        public bool IsSelected { get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                NotifyChange("IsSelected");
            }
        }

        private bool _EnabledCodigo=true;
        public bool EnabledCodigo { get
            {
                return _EnabledCodigo;
            }
            set
            {
                _EnabledCodigo = value;
                NotifyChange("EnabledCodigo");
            }
        }

        private bool _BttnEliminar = true;
        public bool BttnEliminar
        {
            get
            {
                return _BttnEliminar;
            }
            set
            {
                _BttnEliminar = value;
                NotifyChange("BttnEliminar");
            }
        }

        private int _Id_clasificacion;
        public int Id_clasificacion { get
            {
                return _Id_clasificacion;
            }
            set
            { _Id_clasificacion = value;
                NotifyChange("Id_clasificacion");
            } }

        private int _Idplano;
        public int IdPlano
        {
            get
            {
                return _Idplano;
            }
            set
            {
                _Idplano = value;
                NotifyChange("IdPlano");
            }
        }

        private IControlTooling _controlador;
        public IControlTooling Controlador { get
            {
                return _controlador;
            }
            set
            {
                _controlador = value;
                NotifyChange("Controlador");
            }
        }

        private string _textCombo;
        public string textCombo { get { return _textCombo; } set { _textCombo = value; NotifyChange("textCombo"); } }
        private bool bandCambios;
        #endregion

        #region Commands
        /// <summary>
        /// Comando para guardar un registro
        /// </summary>
        public ICommand GuardarMaestro
        {
            get
            {
                return new RelayCommand(o => guardar());
            }
        }

        /// <summary>
        /// Comando que elimina un registro de Maestro herramental
        /// </summary>
        public ICommand EliminarMaestro
        {
            get
            {
                return new RelayCommand(o => eliminar());
            }
        }

        /// <summary>
        /// Comando que muestra el controlador de los herramentales
        /// </summary>
        public ICommand MostrarControl
        {
            get
            {
                return new RelayCommand(o => nuevoControl());
            }
        }

        /// <summary>
        /// Comando que muestra la ventana para buscar un herramental
        /// </summary>
        public ICommand IrClasificacionH
        {
            get
            {
                return new RelayCommand(o => irClasificacionH());
            }
        }
        #endregion
        #region Methods

        /// <summary>
        /// Método para guardar un maestro herramental
        /// </summary>
        private async void guardar()
        {
            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";
            //Valida que los campos esten completos
            if (ValidaValores() & Controlador.ValidaError())
            {
                if (Controlador.ValidaRangos())
                {
                    //Ejecutamos el método para mostrar el mensaje con la información que el usuario capturó.El resultado lo asignamos a una variable local.
                    MessageDialogResult result = await dialog.SendMessage("Attention", "¿Desea guardar los cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);
                    //Si el resultado es verdadero
                    if (result == MessageDialogResult.Affirmative)
                    {
                        MaestroHerramental obj = new MaestroHerramental();
                        // si la bandera de cambios es falsa, se va agregar un nuevo herramental
                        if (bandCambios == false)
                        {
                            //Si no se repite el código
                            if (DataManager.GetCodigoMaestro(Codigo) == null)
                            {
                                //se asigna el valor del checkbox
                                if (IsSelected)
                                    obj.activo = true;
                                else
                                    obj.activo = false;
                                //Asignamos los valores
                                obj.Codigo = Codigo;
                                obj.descripcion = Descripcion;
                                obj.fecha_creacion = DateTime.Now.ToShortDateString();
                                obj.fecha_cambio = DateTime.Now.ToShortDateString();
                                obj.usuario_cambio = encriptar.desencript(usuario.NombreUsuario);
                                obj.usuario_creacion = encriptar.desencript(usuario.NombreUsuario);
                                obj.id_clasificacion = SelectedClasificacion.IdClasificacion;
                                obj.id_plano = 0;

                                //Ejecutamos el método para insertar el maestro herramental
                                string codigo_maestro = DataManager.SetMaestroHerramentales(obj);

                                //si el herramental se insertó correctamente
                                if (codigo_maestro != null)
                                {
                                    if (Controlador.Guardar(codigo_maestro) != 0)
                                    {
                                        //Se muestra un mensaje en pantalla
                                        await dialog.SendMessage("Información", "Los cambios fueron guardados exitosamente..");

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
                                        //Se muestra un mensaje en pantalla
                                        await dialog.SendMessage("Alerta", "Error al guardar el maestro herramental...");
                                    }
                                }
                                else
                                {
                                    //si hay erro al guardar el archivo, muestra un mensaje
                                    await dialog.SendMessage("Alerta", "Error al guardar el maestro herramental...");
                                }
                            }
                            else
                            {
                                //si el código ya existe
                                await dialog.SendMessage("Alerta", "El código de maestro herramental ya existe...");
                            }
                        }
                        else
                        {
                            // la band es verdera, se haran cambios a un resgistro

                            //si el check box se seleccionó, el herramental está activo
                            if (IsSelected)
                                obj.activo = true;
                            else
                                obj.activo = false;

                            //Asignamos los valores
                            obj.Codigo = codigo;
                            obj.descripcion = Descripcion;
                            obj.fecha_cambio = DateTime.Now.ToShortDateString();
                            obj.usuario_cambio = encriptar.desencript(usuario.NombreUsuario);
                            obj.id_clasificacion = SelectedClasificacion.IdClasificacion;
                            obj.id_plano = 0;

                            //Ejecutamos el método para actualizar el registro
                            int update = DataManager.UpdateMaestroHerramental(obj);

                            //Si se actualizó correctamente
                            if (update != 0)
                            {

                                if (Controlador.Update() != 0) {
                                    //Se muestra en pantalla, mensaje de cambios guardados
                                    await dialog.SendMessage("Información", "Los cambios fueron guardados exitosamente..");

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
                                    //si hay erro al guardar el archivo, muestra un mensaje
                                    await dialog.SendMessage("Alerta", "Error al actualizar el herramental..");
                                }
                            }
                            else
                            {
                                //si hay erro al guardar el archivo, muestra un mensaje
                                await dialog.SendMessage("Alerta", "Error al guardar los cambios...");
                            }
                        }
                    }
                }
                else
                {
                    await dialog.SendMessage("Alerta", "Error de rangos...");
                }
            }
            else
            {
                //si todos los campos no estan llenos
                await dialog.SendMessage("Alerta", "Se deben llenar todos los campos...");
            }
            }

        /// <summary>
        /// Método que valida los valores, si está completos retorna verdadero
        /// Si un campo es nulo retorna falso
        /// </summary>
        /// <returns></returns>
        private bool ValidaValores()
        {
            if (!string.IsNullOrEmpty(codigo) & !string.IsNullOrWhiteSpace(codigo) & !string.IsNullOrWhiteSpace(descripcion) & !string.IsNullOrEmpty(descripcion) & SelectedClasificacion != null & SelectedPlano == null)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Método que abre la ventana para buscar un registro  clasificacion herramental
        /// </summary>
        private void irClasificacionH()
        {
            WBusquedaHerramenal frm = new WBusquedaHerramenal();
            Busqueda_HerramentalVM context = new Busqueda_HerramentalVM();
            frm.DataContext = context;
            frm.ShowDialog();

            //Se obtiene el objeto seleccionado, de la ventana de búsqueda
            if (context.SelectedClasificacion != null) {
                SelectedClasificacion = context.SelectedClasificacion;
                //Se asigna al id para moestrar en el combobox
                Id_clasificacion = SelectedClasificacion.IdClasificacion;
            }

        }
        /// <summary>
        /// Método que muestra el control dependiendo de la clasificacion de herramental
        /// </summary>
        private async void nuevoControl()
        {
            //Si selecciono algun herramental
            if (SelectedClasificacion != null)
            {
                try
                {
                    //Se obtiene el archivo
                    file = new XmlApplicationContext(@"\\agufileserv2\INGENIERIA\RESPRUTAS\NUEVO SOFTWARE RUTAS\raul\nueva\RoutingGenerationProgram\ClasificacionHerramental.xml");
                    ctx = file;
                    string objetoXML = SelectedClasificacion.objetoXML;
                    //Se obtiene el objeto dependiendo del id asignado
                    Controlador =(IControlTooling) ctx.GetObject(objetoXML);
                    //Inicializa el controlador
                    Controlador.Inicializa();                  
                }
                catch (Exception er)
                {
                    //si hay error, o no encuentra el objeto muestra un mensaje en pantalla
                    await dialog.SendMessage("Información", "");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objetoXML"></param>
        private async  void CargarControlador(string objetoXML)
        {
            if (!string.IsNullOrEmpty(objetoXML)) {
                try
                {
                    //Se obtiene el archivo
                    file = new XmlApplicationContext(@"\\agufileserv2\INGENIERIA\RESPRUTAS\NUEVO SOFTWARE RUTAS\raul\nueva\RoutingGenerationProgram\ClasificacionHerramental.xml");
                    ctx = file;

                    //Se obtiene el objeto dependiendo del id asignado
                    Controlador = (IControlTooling)ctx.GetObject(objetoXML);
                    //Inicializa el controlador
                    Controlador.Inicializa();
                    Controlador.InicializaCampos(codigo);
                }
                catch (Exception er)
                {
                    //si hay error, o no encuentra el objeto muestra un mensaje en pantalla
                    await dialog.SendMessage("Información", "");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private async void eliminar()
        {
            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";


            //Ejecutamos el método para mostrar el mensaje con la información que el usuario capturó.El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage("Attention", "¿Desea guardar los cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);
            //Si el resultado es verdadero
            if (result == MessageDialogResult.Affirmative & Controlador != null)
            {
                MaestroHerramental obj = new MaestroHerramental();

                obj.Codigo = codigo;
                //Se ejecuta el método de Eliminar maestro herramental.
                int delete = Controlador.Delete();

                //Si el resultado es diferente de cero.
                if (delete!=0)
                {
                    //Ejecutamos el método de eliminar del controlador.
                    if(DataManager.DeleteMaestroHerramental(obj) != 0)
                    {
                        //Se muestra en pantalla, mensaje de cambios guardados
                        await dialog.SendMessage("Información", "Los cambios fueron guardados exitosamente..");

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
                        await dialog.SendMessage("Alerta", "Error al eliminar el herramental...");
                }
                else
                    //si hay erro al eliminar el registro, muestra un mensaje
                    await dialog.SendMessage("Alerta", "Error al eliminar el herramental...");
            }
          }
        #endregion
        #region Constructor
        public NuevoMaestroHerramental_VM(Usuario ModelUsuario)
        {
            usuario = ModelUsuario;
            ListaPlano = DataManager.GetPlano_Herramental();
            ListaClasificacion = DataManager.GetClasificacionHerramental(string.Empty);
            //Bandera en falso si se va a dar de alta un herramental
            bandCambios = false;
            BttnEliminar = false;
        }

        public NuevoMaestroHerramental_VM(Usuario ModelUsuario, Herramental MHerramental)
        {
            //Constructor para ver detalles de un herramental y cambiar la información
            usuario = ModelUsuario;
            ListaPlano = DataManager.GetPlano_Herramental();
            ListaClasificacion = DataManager.GetClasificacionHerramental(string.Empty);
            //Obtiene las propiedades del herramental
            MaestroHerramental ObjHerramental = DataManager.GetPropiedadesHerramental(MHerramental.Codigo);
           
            //Asignamos las propiedades 
            bandCambios = true;
            BttnEliminar = true;
            Codigo = MHerramental.Codigo;
            Descripcion = MHerramental.DescripcionGeneral;
            EnabledCodigo = false;
            IsSelected = ObjHerramental.activo;
            Id_clasificacion = ObjHerramental.id_clasificacion;
            CargarControlador(ObjHerramental.objetoXML);
        }
        #endregion
    }
}
