using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using MahApps.Metro.IconPacks;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.LeccionesAprendidas;
using View.Resources;

namespace View.Services.ViewModel
{
    public class LeccionesAprendidasVM : INotifyPropertyChanged
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
        private ObservableCollection<LeccionesAprendidas> _Lista;
        public ObservableCollection<LeccionesAprendidas> Lista
        {
            get
            {
                return _Lista;
            }
            set
            {
                _Lista = value;
                NotifyChange("Lista");
            }
        }

        private ObservableCollection<objUsuario> _ListaUsuarios;
        public ObservableCollection<objUsuario> ListaUsuarios
        {
            get
            {
                return _ListaUsuarios;
            }
            set
            {
                _ListaUsuarios = value;
                NotifyChange("ListaUsuarios");
            }
        }

        private LeccionesAprendidas _SelectedLeccion;
        public LeccionesAprendidas SelectedLeccion
        {
            get
            {
                return _SelectedLeccion;
            }
            set
            {
                _SelectedLeccion = value;
                NotifyChange("SelectedLeccion");
            }
        }

        public Usuario user;

        private HamburgerMenuItemCollection _menuItems;
        public HamburgerMenuItemCollection MenuItems
        {
            get
            {
                return _menuItems;
            }
            set
            {
                if (Equals(value, _menuItems)) return;
                _menuItems = value;
                //OnPropertyChanged();
                NotifyChange("MenuItems");
            }
        }

        private HamburgerMenuItemCollection _menuOptionItems;
        public HamburgerMenuItemCollection MenuOptionItems
        {
            get
            {
                return _menuOptionItems;
            }
            set
            {
                if (Equals(value, _menuOptionItems)) return;
                _menuOptionItems = value;
                //OnPropertyChanged();
                NotifyChange("MenuOptionItems");
            }
        }

        private DateTime _FechaInicial;
        public DateTime FechaInicial
        {
            get { return _FechaInicial; }
            set { _FechaInicial = value; NotifyChange("FechaInicial"); }
        }

        private DateTime _FechaFinal;
        public DateTime FechaFinal
        {
            get { return _FechaFinal; }
            set { _FechaFinal = value; NotifyChange("FechaFinal"); }
        }

        private int _TotalRegistros;

        public int TotalRegistros
        {
            get { return _TotalRegistros; }
            set { _TotalRegistros = value; NotifyChange("TotalRegistros"); }
        }

        private ObservableCollection<FO_Item> _ListaMotivos;
        public ObservableCollection<FO_Item> ListaMotivos
        {
            get { return _ListaMotivos; }
            set { _ListaMotivos = value; NotifyChange("ListaMotivos"); }
        }

        private FO_Item _MotivoSelected;
        public FO_Item MotivoSelected
        {
            get { return _MotivoSelected; }
            set { _MotivoSelected = value; NotifyChange("MotivoSelected"); }
        }


        #endregion

        #region Constructor

        public LeccionesAprendidasVM(Usuario ModelUsuario)
        {
            user = ModelUsuario;
            Constructor();
        }

        #endregion

        #region Comandos

        public ICommand ExportExcel
        {
            get
            {
                return new RelayCommand(o => exportHistorialExcel());
            }
        }

        /// <summary>
        /// Comando para Modificar una Leccion Seleccionada
        /// </summary>
        public ICommand EditarLeccion
        {
            get
            {
                return new RelayCommand(o => editarleccion(user));
            }
        }

        /// <summary>
        /// comando para buscar una leccion
        /// </summary>
        public ICommand BuscarLeccion
        {
            get
            {
                return new RelayCommand(param => buscarleccion((string)param));
            }
        }

        public ICommand BuscarLeccionByFecha
        {
            get
            {
                return new RelayCommand(param => buscarLeccionByFecha((string)param));
            }
        }

        public ICommand InsertNuevaLeccion
        {
            get
            {
                return new RelayCommand(a => InsertarNuevaLeccion(user));
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que exporta el historial de lecciones aprendidas del componente seleccionado.
        /// </summary>
        private async void exportHistorialExcel()
        {
            DataSet ds = new DataSet();
            DataTable table = new DataTable();

            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController Progress;

            //Ejecutamos el método para enviar un mensaje de espera mientras el documento se guarda.
            Progress = await dialog.SendProgressAsync(StringResources.msgDoingOperation, StringResources.msgGenerandoExcell);

            //Si la lista de documentos es diferente de cero
            if (Lista.Count != 0)
            {
                //Se añade las columnas, se especifíca el tipo fecha para dar formato a la columna
                //Se tien que especificar el tipo, si no la fecha se escribe mal en Excel
                table.Columns.Add("Cambio Realizado Por");
                table.Columns.Add("Componente");
                table.Columns.Add("Descripción Problema");
                table.Columns.Add("Fecha Actualización", typeof(DateTime));
                table.Columns.Add("Reportado Por");
                table.Columns.Add("Solicitud de Trabajo");

                //Iteramos la lista de documentos
                foreach (var item in Lista)
                {
                    //Se crea una nueva fila
                    DataRow newRow = table.NewRow();

                    //Se añaden los valores a las columnas
                    newRow["Cambio Realizado Por"] = item.NombreCompleto;
                    newRow["Componente"] = item.COMPONENTE;
                    newRow["Descripción Problema"] = item.DESCRIPCION_PROBLEMA;
                    newRow["Fecha Actualización"] = item.FECHA_ACTUALIZACION;
                    newRow["Reportado Por"] = item.REPORTADO_POR;
                    newRow["Solicitud de Trabajo"] = item.SOLICITUD_DE_TRABAJO;

                    //Agregamos la fila a la tabla
                    table.Rows.Add(newRow);
                }
                //Se agrega la tabla al dataset
                ds.Tables.Add(table);

                //Ejecutamos el método para exportar el archivo
                string e = await ExportToExcel.Export(ds);

                if (e != null)
                {
                    //Cerramos el mensaje de espera
                    await Progress.CloseAsync();

                    //Mostramos mensaje de error
                    await dialog.SendMessage(StringResources.msgError, StringResources.msgGenerandoExcell);
                }

                //Ejecutamos el método para cerrar el mensaje de espera.
                await Progress.CloseAsync();
            }
        }

        /// <summary>
        /// Método que obtiene la lista de las lecciones aprendidas
        /// </summary>
        private void Constructor()
        {
            Lista = DataManagerControlDocumentos.GetLec("");
            TotalRegistros = Lista.Count;
            FechaFinal = DateTime.Now;
            FechaInicial = DateTime.Now;
            ListaMotivos = DataManagerControlDocumentos.GetMotivoCambio();
            
            if (ListaMotivos.Count>0)
            {
                MotivoSelected = ListaMotivos[0];
            }
            CreateMenuItems();
        }

        /// <summary>
        /// Método para modificar una leccion seleccionada
        /// </summary>
        private void editarleccion(Usuario ModelUsuario)
        {
            if (SelectedLeccion!=null)
            {
                user = ModelUsuario;
                //declaramos un objeto de tipo ModificarLeccion
                ModificarLeccion Form = new ModificarLeccion();

                //declaramos un objeto de tipo ModificarLeccionVM
                ModificarLeccionVM context = new ModificarLeccionVM(SelectedLeccion,user);

                //abrimos la ventana
                Form.DataContext = context;
                Form.ShowDialog();
                Lista = DataManagerControlDocumentos.GetLec("");
            }
        }

        /// <summary>
        /// Método para buscar una leccion
        /// </summary>
        private void buscarleccion(string TextoBusqueda)
        {
            Lista = DataManagerControlDocumentos.GetLec(TextoBusqueda);
            TotalRegistros = Lista.Count;
        }

        /// <summary>
        /// Método para realizar una consulta por fecha y texto
        /// </summary>
        /// <param name="TextoBusqueda"></param>
        private void buscarLeccionByFecha(string TextoBusqueda)
        {
            FechaInicial = new DateTime(FechaInicial.Year, FechaInicial.Month, FechaInicial.Day, 0, 0, 0);
            FechaFinal = new DateTime(FechaFinal.Year, FechaFinal.Month, FechaFinal.Day, 23, 59, 59);

            Lista = DataManagerControlDocumentos.GetLec(TextoBusqueda, FechaInicial, FechaFinal);
            TotalRegistros = Lista.Count;
        }

        /// <summary>
        /// Método para generar el menu de hamburguesa
        /// </summary>
        public void CreateMenuItems()
        {
            MenuItems = new HamburgerMenuItemCollection();
            MenuOptionItems = new HamburgerMenuItemCollection();

            //Boton para agregar una nueva lección aprendida
            this.MenuItems.Add(
                 new HamburgerMenuIconItem()
                 {
                     Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.File },
                     Label = StringResources.lblNuevaLeccion,
                     Command = InsertNuevaLeccion,
                     Tag = StringResources.lblNuevaLeccion,
                 }
            );

            this.MenuItems.Add(
                    new HamburgerMenuIconItem()
                    {
                        //Icono del Menú para guardar los cambios hechos a la lección aprendida
                        Icon = new PackIconMaterial() { Kind = PackIconMaterialKind.FileExcel },
                        Label = "Exportar Historial",
                        Command = ExportExcel,
                        Tag = "Exportar Historial",
                    }
                );

        }

        /// <summary>
        /// Método que muetra la pantalla para insertar una nueva lección aprendida
        /// </summary>
        private async void InsertarNuevaLeccion(Usuario ModelUsuario)
        {
            DialogService dialog = new DialogService();

            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "+2";
            setting.NegativeButtonText = "1";

            MessageDialogResult resul = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblDescripcionSimilar, setting, MessageDialogStyle.AffirmativeAndNegative);

            if (resul == MessageDialogResult.Affirmative)
            {
                FrmSelect frmLista = new FrmSelect();
                frmLista.DataContext = this;
                bool result = (bool)frmLista.ShowDialog();

                if (result)
                {
                    InsertarComponentes Descripcion = new InsertarComponentes();
                    InsertarNuevaLeccionVW Context = new InsertarNuevaLeccionVW(ModelUsuario, true, MotivoSelected);
                    Descripcion.DataContext = Context;
                    Descripcion.ShowDialog();
                }

                Lista = DataManagerControlDocumentos.GetLec("");

            }
            else
            {
                FrmSelect frmLista = new FrmSelect();
                frmLista.DataContext = this;

                bool result = (bool)frmLista.ShowDialog();

                if (result)
                {
                    InsertarNuevaLeccion Insertar = new InsertarNuevaLeccion();
                    InsertarNuevaLeccionVW InsertarVW = new InsertarNuevaLeccionVW(ModelUsuario, false, MotivoSelected);
                    Insertar.DataContext = InsertarVW;
                    Insertar.ShowDialog();
                }

                Lista = DataManagerControlDocumentos.GetLec("");
            }
        }
        #endregion
    }
}
