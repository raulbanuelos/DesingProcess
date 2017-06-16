using MahApps.Metro.Controls;
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
using System.Windows;

namespace View.Services.ViewModel
{
    public class ValidarDocumentoViewM : INotifyPropertyChanged
    {
        #region Propiedades
        private ObservableCollection<Archivo> _ListaArchivos = new ObservableCollection<Archivo>();
        public ObservableCollection<Archivo> ListaArchivos
        {
            get
            {
                return _ListaArchivos;
            }
            set
            {
                _ListaArchivos = value;
                NotifyChange("ListaArchivos");
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

        private Archivo _selectedItem;
        public Archivo SelectedItem
        {
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

        private Model.ControlDocumentos.Version _usuario;
        public Model.ControlDocumentos.Version Usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
                NotifyChange("Usuario");
            }

        }

        private bool isSelected = false;
        public bool IsSelected {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                NotifyChange("IsSelected");
            }
        }

        private string _estatus = "PENDIENTE POR CORREGIR";
        public string Estatus
        {
            get
            {
                return _estatus;
            }
            set
            {
                _estatus = value;
                NotifyChange("Estatus");
            }
        }

        public DialogService dialog = new DialogService();

        #endregion

        #region Constructores

        public ValidarDocumentoViewM(Documento documento)
        {
            SelectedDocumento = DataManagerControlDocumentos.GetDocumento(documento.id_documento, documento.version.no_version);

            ObservableCollection<Documento> Lista = DataManagerControlDocumentos.GetArchivos(documento.id_documento, documento.version.id_version);

            Usuario = DataManagerControlDocumentos.GetIdUsuario(documento.version.id_version);

            foreach (var item in Lista)
            {
                SelectedDocumento.tipo.tipo_documento = item.tipo.tipo_documento;
                SelectedDocumento.Departamento = item.Departamento;
                Archivo objArchivo = new Archivo();

                objArchivo.nombre = item.nombre;
                objArchivo.id_archivo = item.version.archivo.id_archivo;
                objArchivo.archivo = item.version.archivo.archivo;
                objArchivo.ext = item.version.archivo.ext;

                if (objArchivo.ext == ".pdf")
                {
                    //asigna la imagen del pdf al objeto
                    objArchivo.ruta = @"/Images/p.png";
                }
                else
                {
                    //Si es archivo de word asigna la imagen correspondiente.
                    objArchivo.ruta = @"/Images/w.png";
                }
                ListaArchivos.Add(objArchivo);
            }
        }
        #endregion

        #region Commandos
         /// <summary>
        /// Comando para ver el archivo desde la lista de documentos.
        /// </summary>
        public ICommand VerArchivo
        {
            get
            {
                return new RelayCommand(o => verArchivo(SelectedItem));
            }
        }
        private  void verArchivo(Archivo item)
        {
            if (item != null)
            {
                //se asigna el nombre del archivo temporal, se concatena el nombre del archivo, la posicion de la lista y la extensión.
                string filename = GetPathTempFile(item);

                //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                File.WriteAllBytes(filename, item.archivo);

                //Se inicializa el programa para visualizar el archivo.
                Process.Start(filename);
            }
        }


        /// <summary>
        /// Método que genera una cadena para cargar un archivo en la carpeta temporal del sistema.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetPathTempFile(Archivo item)
        {
            //Se guarda la ruta del directorio temporal.
            var tempFolder = Path.GetTempPath();

            string filename = string.Empty;
            do
            {
                string aleatorio = Module.GetRandomString(5);

                filename = Path.Combine(tempFolder, item.nombre + item.numero + "_" + aleatorio + item.ext);
            } while (File.Exists(filename));

            return filename;
        }

        public ICommand Guardar
        {
            get
            {
                return new RelayCommand(g => guardarEstatus());
            }
        }

        public ICommand Checked
        {
            get
            {
                return new RelayCommand(g => check());
            }
        }

        private void check()
        {
            Estatus = "APROBADO, PENDIENTE POR LIBERAR";  
        }

        public ICommand Unchecked
        {
            get
            {
                return new RelayCommand(g => uncheck());
            }
        }

        private void uncheck()
        {
            Estatus = "PENDIENTE POR CORREGIR";
        }


        private async void guardarEstatus()
        {
            //isSelected es falso, id_estatus=pendiente por corregir, verdadero estatus= aprobado pendiente por liberar
            //
          
            string version = SelectedDocumento.version.no_version;
            Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
            objVersion.id_version = SelectedDocumento.version.id_version;

            // Si el checkbox es verdadero
            if (isSelected == true)
            {
                //Si el documento no tiene una versión liberada
                if (version.Equals("1"))
                {
                    //Actualiza el estatus de la versión y del documento a pendiente por liberar
                    selectedDocumento.id_estatus = 4;
                    objVersion.id_estatus_version = 5;
                   
                    //Se llama al método para actualizar el estatus del documento
                    int n = DataManagerControlDocumentos.Update_EstatusDocumento(SelectedDocumento);
                    
                    //si se realizo la actualizacion
                    if (n != 0)
                    {
                        //Se llama a la función para actualizar el estatus de la versión
                        UpdateVersion(objVersion);
                    }
                    else
                    {
                        //Se muestra que hubo un error al actualizar el documento
                        await dialog.SendMessage("Alerta", "Error al actualizar el estatus del documento ..");
                    }
                }
                else
                {
                    //si es un documento con versión liberada.
                    objVersion.id_estatus_version = 5;

                    //Se llama a la función para actualizar el estatus de la versión
                    UpdateVersion(objVersion);
                }

            }else
            {
                //Si el documento no tiene una versión liberada
                if (version.Equals("1"))
                {
                    //Actualiza el estatus de la versión y del documento a pendiente por corregir
                    selectedDocumento.id_estatus = 3;
                    objVersion.id_estatus_version = 4;

                    //Se llama al método para actualizar el estatus del documento
                    int n = DataManagerControlDocumentos.Update_EstatusDocumento(SelectedDocumento);

                    //si se realizo la actualizacion
                    if (n != 0)
                    {
                        //Se llama a la función para actualizar el estatus de la versión
                        UpdateVersion(objVersion);
                    }
                    else
                    {
                        //Se muestra que hubo un error al actualizar el documento
                        await dialog.SendMessage("Alerta", "Error al actualizar el estatus del documento ..");
                    }
                }
                else
                {
                    //si es un documento con versión .
                    //Estatus pendiente por corregir.
                    objVersion.id_estatus_version = 4;
                    //Se llama a la función para actualizar el estatus de la versión
                    UpdateVersion(objVersion);
                }

            }
        }

        //método para modificar la versión.
        public async void UpdateVersion(Model.ControlDocumentos.Version objVersion)
        {
            //Se llama al método para actualizar el estatus de la version
            int update_version = DataManagerControlDocumentos.Update_EstatusVersion(objVersion);

            if (update_version!=0)
            {
                await dialog.SendMessage("Información", "Se actualizó el estatus de la versión..");
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
                await dialog.SendMessage("Alerta", "Error al actualizar el estatus de la versión..");
            }
        }

        
        #endregion

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




    }
}
