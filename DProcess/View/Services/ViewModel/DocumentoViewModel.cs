﻿using MahApps.Metro.Controls.Dialogs;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.ControlDocumentos;

namespace View.Services.ViewModel
{
    public class DocumentoViewModel : INotifyPropertyChanged
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
        private string nombre;
        public string Nombre
        {
            get
            {
                return this.nombre;
            }
            set
            {
                this.nombre = value;
                NotifyChange("Nombre");
            }
        }

        private string version;
        public string Version
        {
            get
            {
                return this.version;
            }
            set
            {
                this.version = value;
                NotifyChange("Version");
            }
        }

        private DateTime fecha = DateTime.Now;
        public DateTime Fecha
        {
            get
            {

                return this.fecha;
            }
            set
            {
                this.fecha = value;
                NotifyChange("Fecha");
            }
        }

        private string copias;
        public string Copias
        {
            get
            {
                return this.copias;
            }
            set
            {
                this.copias = value;
                NotifyChange("Copias");
            }
        }

        private string descripcion;
        public string Descripcion
        {
            get
            {
                return this.descripcion;
            }
            set
            {
                this.descripcion = value;
                NotifyChange("Descripcion");
            }
        }

        private int id_documento;
        public int ID_documento
        {
            get
            {
                return id_documento;
            }
            set
            {
                id_documento = value;
            }
        }

        private int _id_tipo;
        public int id_tipo
        {
            get
            {
                return _id_tipo;
            }
            set
            {
                _id_tipo = value;
                NotifyChange("id_tipo");
            }
        }

        private string botonGuardar;
        public string BotonGuardar
        {
            get
            {
                return botonGuardar;
            }
            set
            {
                botonGuardar = value;
                NotifyChange("BotonGuardar");
            }
        }

        private ObservableCollection<Archivo> _ListaDocumentos = new ObservableCollection<Archivo>();
        public ObservableCollection<Archivo> ListaDocumentos
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

        private ObservableCollection<TipoDocumento> _ListaTipo = DataManagerControlDocumentos.GetTipo();
        public ObservableCollection<TipoDocumento> ListaTipo
        {
            get
            {
                return _ListaTipo;
            }
            set
            {
                _ListaTipo = value;
                NotifyChange("ListaTipo");
            }
        }

        private Archivo _selectedItem;
        public Archivo SelectedItem {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyChange("SelectedItem");
            }

        }
        #endregion

        #region Constructor
        public DocumentoViewModel(string _nombre, string _version, string _copias, string _descripcion, int _id_documento)
        {
            Nombre = _nombre;
            Version = _version;
            Copias = _copias;
            Descripcion = _descripcion;
            id_documento = _id_documento;
            BotonGuardar = "Guardar";
        }

        #endregion

        #region Commands
        /// <summary>
        /// Comando para guardar un registro de documento
        /// </summary>
        public ICommand GuardarControl
        {
            get
            {
                return new RelayCommand(o => guardarControl());
            }
        }
        private async void guardarControl()
        {
            DialogService dialog = new DialogService();
            if (nombre != null & version != null & fecha != null & copias != null & descripcion != null & id_tipo != 0)
            {
                if (BotonGuardar=="Guardar") {
                    Documento obj = new Documento();
                    Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

                    obj.nombre = nombre;
                    obj.id_usuario = "2";
                    obj.id_tipo_documento = _id_tipo;
                    obj.descripcion = descripcion;
                    obj.version_actual = version;
                    obj.fecha_creacion = fecha;
                    obj.fecha_actualizacion = Convert.ToDateTime("03/02/2017");
                    obj.fecha_emision = Convert.ToDateTime("03/02/2015");

                    id_documento = DataManagerControlDocumentos.SetDocumento(obj);

                    objVersion.no_version = version;
                    objVersion.no_copias = Convert.ToInt32(copias);
                    objVersion.id_documento = id_documento;
                    objVersion.id_usuario = "2";
                    objVersion.fecha_version = fecha;

                    int id_version = DataManagerControlDocumentos.SetVersion(objVersion);

                    foreach (var item in _ListaDocumentos)
                    {
                        Archivo objArchivo = new Archivo();

                        objArchivo.id_version = id_version;
                        objArchivo.archivo = item.archivo;
                        objArchivo.ext = item.ext;

                        int n = DataManagerControlDocumentos.SetArchivo(objArchivo);
                    }
                }
                else
                {
                    Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();

                    objVersion.no_version = version;
                    objVersion.no_copias = Convert.ToInt32(copias);
                    objVersion.id_documento = id_documento;
                    objVersion.id_usuario = "2";
                    objVersion.fecha_version = fecha;

                    int id_version = DataManagerControlDocumentos.SetVersion(objVersion);

                    foreach (var item in _ListaDocumentos)
                    {
                        Archivo objArchivo = new Archivo();

                        objArchivo.id_version = id_version;
                        objArchivo.archivo = item.archivo;
                        objArchivo.ext = item.ext;

                        int id_archivo = DataManagerControlDocumentos.SetArchivo(objArchivo);
                    }
                    BotonGuardar = "Guardar";
                }
            }
            else
            {
                await dialog.SendMessage("RGP: Alerta", "Se debe llenar todos los campos");
            }
        }

        /// <summary>
        /// Comando para agregar un documento a la lista, desde el explorador de archivos
        /// </summary>
        public ICommand LlenarLista
        {
            get
            {
                return new RelayCommand(o => llenarLista());
            }
        }
        private void llenarLista()
        {
            //Abre la ventana de explorador de archivos
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //Filtar los dpocumentos por extensión 
            dlg.Filter = "Word (97-2003)|*.doc|PDF Files (.pdf)|*.pdf";


            // Mostrar el explorador de archivos
            Nullable<bool> result = dlg.ShowDialog();


            // Si fue seleccionado un documento 
            if (result == true)
            {
                //Se obtiene el nombre del documento
                string filename = dlg.FileName;

                //Se crea el objeto de tipo archivo
                Archivo obj = new Archivo();

                //Se convierte el archvio a tipo byte y se le asigna al objeto
                obj.archivo = File.ReadAllBytes(filename);

                //Obtiene la extensión del documento y se le asigna al objeto
                obj.ext = System.IO.Path.GetExtension(filename);

                //Se obtiene sólo el nombre, sin extensión.
                obj.nombre = System.IO.Path.GetFileNameWithoutExtension(filename);

                if (obj.ext == ".pdf")
                {

                    //img_icono.Source = new BitmapImage(new Uri(@"/Images/pdf.ico", UriKind.Relative));
                    obj.ruta = @"/Images/p.png";
                }
                else
                {
                    //img_icono.Source = new BitmapImage(new Uri(@"/Images/piston.ico", UriKind.Relative));
                    obj.ruta = @"/Images/w.png";
                }
                //Se agrega el objeto a la lista
                ListaDocumentos.Add(obj);

            }
        }

        /// <summary>
        /// Comando para cancelar
        /// </summary>
        public ICommand Cancelar
        {
            get
            {
                return new RelayCommand(o => cancelar());
            }
        }
        private void cancelar()
        {

        }

        /// <summary>
        /// Comando para eliminar un registro
        /// </summary>
        public ICommand Eliminar
        {
            get
            {
                return new RelayCommand(o => eliminar());
            }
        }
        private void eliminar()
        {
            if (id_documento != 0)
            {

                Documento obj = new Documento();
                obj.id_documento = id_documento;
                int n = DataManagerControlDocumentos.DeleteDocumento(obj);
            }
        }

        /// <summary>
        /// Comando para modificar un registro
        /// </summary>
        public ICommand Modificar
        {
            get
            {
                return new RelayCommand(o => modificar());
            }
        }
        private async void modificar()
        {
            DialogService dialog = new DialogService();
            if (nombre != null & version != null & fecha != null & copias != null & descripcion != null & _id_tipo != 0)
            {

                Documento obj = new Documento();
                obj.id_documento = id_documento;
                obj.nombre = nombre;
                obj.id_usuario = "2";
                obj.id_tipo_documento = _id_tipo;
                obj.descripcion = descripcion;
                obj.version_actual = version;
                obj.fecha_creacion = Convert.ToDateTime("03/02/2017");
                obj.fecha_actualizacion = fecha;
                obj.fecha_emision = Convert.ToDateTime("03/02/2015");

                int n = DataManagerControlDocumentos.UpdateDocumento(obj);
            }
            else
            {
                await dialog.SendMessage("RGP: Alerta", "Se debe llenar todos los campos");
            }
        }

        public ICommand GenerarVersion
        {
            get
            {
                return new RelayCommand(o => generarVersion());
            }
        }
        private  void generarVersion()
        {
            Version = string.Empty;
            Fecha = DateTime.Now;
            Copias=string.Empty;
            ListaDocumentos.Clear();
            BotonGuardar = "Guardar Version";
        }

        public ICommand VerArchivo
        {
            get
            {
                return new RelayCommand(o => verArchivo());
            }
        }

        private  void verArchivo()
        {

            //Se guarda la ruta del directorio temporal.
            var tempFolder = Path.GetTempPath();
            //se asigna el nombre del archivo temporal, se concatena el nombre y la extensión.
            string filename = Path.Combine(tempFolder, "temp" + _selectedItem.ext);
            //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
            File.WriteAllBytes(filename, _selectedItem.archivo);

            //Se inicializa el programa para visualizar el archivo.
            Process.Start(filename);

        }
        #endregion
    }
}
