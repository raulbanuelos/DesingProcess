using MahApps.Metro.Controls.Dialogs;
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
using View.Forms.ControlDocumentos;

namespace View.Services.ViewModel
{
    public class Buscar_DocumentoVM : INotifyPropertyChanged
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
        private ObservableCollection<Documento> _ListaDocumentos;
        public ObservableCollection<Documento> ListaDocumentos
        {
            get
            {
                return _ListaDocumentos;
            }
            set
            {
                _ListaDocumentos = value;
                NotifyChange("ListaDocumentos");
            }
        }

        private Documento selectedDocumento;
        public Documento SelectedDocumento
        {
            get
            {
                return selectedDocumento;
            }
            set
            {
                selectedDocumento = value;
                NotifyChange("SelectedDocumento");
            }
        }
        #endregion

        #region Constructor

        public Buscar_DocumentoVM()
        {
            //Inicializa la lista de documentos
            GetGrid(string.Empty);
        }
        #endregion

        #region Métodos
        /// <summary>
        /// Función que obtiene todos los documentos liberados, los asigna a la lista para mostrar en el dataGrid
        /// </summary>
        /// <param name="texto"></param>
        private void GetGrid(string texto)
        {
            ListaDocumentos = DataManagerControlDocumentos.GetGridDocumentos(texto);
        }

        /// <summary>
        /// Comando que busca un archivo de la lista
        /// Recibe como parámetro la palabra a buscar
        /// </summary>
        public ICommand BuscarDocumentos
        {
            get
            {
                return new RelayCommand(param => GetGrid((string)param));
            }
        }

        /// <summary>
        /// Comando para exportar el datagrid a un archivo excel
        /// </summary>
        public ICommand GetExcel
        {
            get
            {
                return new RelayCommand(o => getExcel());
            }
        }

        /// <summary>
        /// Método que generar un archivo excel a partir de la lista de documentos
        /// </summary>
        private async void getExcel()
        {
            DataSet ds = new DataSet();

            //inicializamos objeto de Datatable
            DataTable table = new DataTable();

            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController Progress;

            //Si la lista de documentos contiene algún registro
            if (ListaDocumentos.Count != 0)
            {
                //Ejecutamos el método para enviar un mensaje de espera mientras el archivo de excel se genera
                Progress = await dialog.SendProgressAsync("Por favor espere", "Generando archivo excel...");

                //Se añade las columnas
                table.Columns.Add("Numero de Documento");
                table.Columns.Add("Descripción");
                table.Columns.Add("Version");
                table.Columns.Add("Responsable");
                table.Columns.Add("Tipo de Documento");
                table.Columns.Add("Usuario Elaboró");
                table.Columns.Add("Usuario Autorizó");

                //Iteramos la lista de documentos
                foreach (var item in ListaDocumentos)
                {
                    //Se crea una nueva fila
                    DataRow newRow = table.NewRow();

                    //Se añaden los valores a las columnas
                    newRow["Numero de Documento"] = item.nombre;
                    newRow["Descripción"] = item.descripcion;
                    newRow["Version"] = item.version.no_version;
                    newRow["Responsable"] = item.Departamento;
                    newRow["Tipo de Documento"] = item.tipo.tipo_documento;
                    newRow["Usuario Elaboró"] = item.usuario;
                    newRow["Usuario Autorizó"] = item.usuario_autorizo;

                    //Agregamos la fila a la tabla
                    table.Rows.Add(newRow);
                }
                //Se agrega la tabla al dataset
                ds.Tables.Add(table);

                //Ejecutamos el método para exportar el archivo
                string e = await ExportToExcel.Export(ds);

                //Si hay un error
                if (e != null)
                {
                    //Cerramos el mensaje de espera
                    await Progress.CloseAsync();

                    //Mostramos mensaje de error
                    await dialog.SendMessage("Alerta", "Error al generar el archivo");
                }
                //Ejecutamos el método para cerrar el mensaje de espera.
                await Progress.CloseAsync();
            }
        }

        /// <summary>
        /// Comando para ver las versiones de un documento
        /// </summary>
        public ICommand verDocumento
        {
            get
            {
                return new RelayCommand(o => abrirDocumento());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void abrirDocumento()
        {
            if (selectedDocumento != null)
            {
                FrmVersiones frm = new FrmVersiones();

                VersionesVM context = new VersionesVM(selectedDocumento);

                frm.DataContext = context;

                frm.ShowDialog();
            }
        }
            #endregion
        }
    }

