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
    public class DocumentosObsoletosVM : INotifyPropertyChanged
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

        private ObservableCollection<Documento> _ListaDocumentosObsoletos;
        public ObservableCollection<Documento> ListaDocumentosObsoletos
        {
            get
            {
                return _ListaDocumentosObsoletos;
            }
            set
            {
                _ListaDocumentosObsoletos = value;
                NotifyChange("ListaDocumentosObsoletos");
            }
        }

        private string _Titulo = "Deseleccionar Todos";
        public string Titulo
        {
            get
            {
                return _Titulo;
            }
            set
            {
                _Titulo = value;
                NotifyChange("Titulo");
            }
        }

        public bool IsSelected;

        #endregion



        #region Constructor
        public DocumentosObsoletosVM()
        {
            ListaDocumentosObsoletos = DataManagerControlDocumentos.GetDocumentosObsoletos(string.Empty);
        }
        #endregion

        #region Comandos

        public ICommand SelecDeselec
        {
            get
            {
                return new RelayCommand(a => _SelecDeselec());
            }
        }

        public ICommand LiberarEspacioBD
        {
            get
            {
                return new RelayCommand(a => _LiberarEspacioBD());
            }
        }
        #endregion


        #region Métodos

        /// <summary>
        /// Método para seleccionar/deseleccionar todo
        /// </summary>
        public void _SelecDeselec()
        {
            if (IsSelected)
            {
                Titulo = "Deseleccionar Todos";
                _SeleccionarTodos();
                IsSelected = false;

            }
            else
            {
                Titulo = "Seleccionar Todos";
                _DeseleccionarTodos();
                IsSelected = true;
            }
        }

        /// <summary>
        /// Método que selecciona todos los archivos
        /// </summary>
        public void _SeleccionarTodos()
        {
            ObservableCollection<Documento> Aux = new ObservableCollection<Documento>();

            foreach (var item in ListaDocumentosObsoletos)
            {
                item.IsSelected = true;
                Aux.Add(item);
            }
            ListaDocumentosObsoletos.Clear();
            foreach (var item in Aux)
            {
                ListaDocumentosObsoletos.Add(item);
            }
        }

        /// <summary>
        /// Método que deselecciona todos los archivos
        /// </summary>
        public void _DeseleccionarTodos()
        {
            ObservableCollection<Documento> Aux = new ObservableCollection<Documento>();

            foreach (var item in ListaDocumentosObsoletos)
            {
                item.IsSelected = false;
                Aux.Add(item);
            }
            ListaDocumentosObsoletos.Clear();
            foreach (var item in Aux)
            {
                ListaDocumentosObsoletos.Add(item);
            }
        }

        /// <summary>
        /// Método para liberar espacio en la base de datos
        /// </summary>
        public string _LiberarEspacioBD()
        {
            try
            {
                foreach (var item in ListaDocumentosObsoletos)
                {
                    string NombreFolder = string.Empty;

                    switch (item.id_tipo_documento)
                    {                       
                        case 2:
                            //asignamos la ruta donde se va a crear el nuevo folder mas el nombre del folder
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\HOJA DE OPERACION ESTANDAR\" + item.nombre;
                            break;
                        case 1002:
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\HOJA DE INSTRUCCION DE INSPECCION\" + item.nombre;
                            break;                                          
                        case 1003:
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\PROCEDIMIENTO OHSAS\" + item.nombre;
                            break;                       
                        case 1004:
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\AYUDAS VISUALES\" + item.nombre;
                            break;                  
                        case 1005:
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\PROCEDIMIENTO ESPECIFICO\" + item.nombre;
                            break;         
                        case 1006:
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\PROCEDIMIENTO ISO\" + item.nombre;
                            break;                       
                        case 1007:
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\HOJA DE METODO DE TRABAJO ESTANDAR\" + item.nombre;
                            break;
                        case 1011:
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\METODO DE INSPECCION ESTANDARIZADO\" + item.nombre;
                            break;
                        case 1012:
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\FORMATO ESPECIFICO\" + item.nombre;
                            break;        
                        case 1013:
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\FORMATO OHSAS\" + item.nombre;
                            break;              
                        case 1014:
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\FORMATO ISO\" + item.nombre;
                            break;                      
                        case 1015:
                            NombreFolder = @"Z:\RrrrUUUUUULLL\Respaldo\JES\" + item.nombre;
                            break;
                    }

                    if (!System.IO.Directory.Exists(NombreFolder))
                    {
                        //creamos el folder
                        System.IO.Directory.CreateDirectory(NombreFolder);
                    }

                    //Asignamos el nombre del archivo, concatenamos el nombre y el número de la version.
                    string NombreArchivo = item.nombre + "_" +item.version.no_version+item.version.archivo.ext;

                    //Creamos la ruta donde se pondran los archivos
                    string pathString = System.IO.Path.Combine(NombreFolder, NombreArchivo);

                    //Obtenemos el arreglo de bytes que representan el archivo
                    byte[] file = item.version.archivo.archivo;

                    //Lo copiamos a la carpeta
                    System.IO.File.WriteAllBytes(pathString, file);

                }

                EliminarDocumentos();

                return "Ok";
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        public void EliminarDocumentos()
        {
            foreach (var item in ListaDocumentosObsoletos)
            {
                DataManagerControlDocumentos.DeleteArchivo(item.version.archivo);
            }
        }
    }
    #endregion


}
