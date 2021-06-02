using Frames.UserControl;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;

namespace Frames.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public bool banFiltro { get; set; }

        #region Properties

        private string _PathDownload;
        public string PathDownload
        {
            get { return _PathDownload; }
            set { _PathDownload = value; NotifyChange("PathDownload"); }
        }

        private int indexPagination;
        public int IndexPagination
        {
            get { return indexPagination; }
            set { indexPagination = value; NotifyChange("IndexPagination"); }
        }

        private string search;
        public string Search
        {
            get { return search; }
            set { search = value; NotifyChange("Search"); }
        }

        private System.Windows.Controls.UserControl pagina;
        public System.Windows.Controls.UserControl Pagina
        {
            get { return pagina; }
            set { pagina = value; NotifyChange("Pagina"); }
        }

        private ObservableCollection<Documento> _ListaDocumento;
        public ObservableCollection<Documento> ListaDocumento
        {
            get { return _ListaDocumento; }
            set { _ListaDocumento = value; NotifyChange("ListaDocumento"); }
        }

        private ObservableCollection<Documento> _ListaDocumentoAux;
        public ObservableCollection<Documento> ListaDocumentoAux
        {
            get {
                return changeList();
            }
        }
        private string lblNumPages;
        public string LblNumPages
        {
            get { return lblNumPages; }
            set { lblNumPages = value; NotifyChange("LblNumPages"); }
        }

        private ObservableCollection<Documento> _ListaDocumentoOriginal;
        public ObservableCollection<Documento> ListaDocumentoOriginal
        {
            get { return _ListaDocumentoOriginal; }
            set { _ListaDocumentoOriginal = value; NotifyChange("ListaDocumentoOriginal"); }
        }

        private bool _BoolImageDownload;
        public bool BoolImageDownload
        {
            get { return _BoolImageDownload; }
            set { _BoolImageDownload = value; NotifyChange("BoolImageDownload"); }
        }

        private bool _BoolGifDownload;
        public bool BoolGifDownload
        {
            get { return _BoolGifDownload; }
            set { _BoolGifDownload = value; NotifyChange("BoolGifDownload"); }
        }

        private bool _BoolIconBack;
        public bool BoolIconBack
        {
            get { return _BoolIconBack; }
            set { _BoolIconBack = value; NotifyChange("BoolIconBack"); }
        }

        private string filtroA;
        #endregion

        #region Commands

        public ICommand AllBack
        {
            get
            {
                return new RelayCommand(param => allBack());
            }
        }

        public ICommand SimpleBack
        {
            get
            {
                return new RelayCommand(param => simpleBack());
            }
        }

        public ICommand SimpleNext
        {
            get
            {
                return new RelayCommand(param => simpleNext());
            }
        }

        public ICommand AllNext
        {
            get
            {
                return new RelayCommand(param => allNext());
            }
        }

        /// <summary>
        /// Comando que al cambiar el textBox, busca un archivo de la lista
        /// Recibe como parámetro la palabra a buscar
        /// </summary>
        public ICommand BuscarDocumentos
        {
            get
            {
                return new RelayCommand(param => changeScreen((string)param));
            }
        }

        public ICommand FiltrarPorTipo
        {
            get
            {
                return new RelayCommand(param => buscarPor((string)param));
            }
        }

        public ICommand VerArchivo
        {
            get
            {
                return new RelayCommand(param => verArchivo(Convert.ToInt32(param)));
            }
        }

        public ICommand BackZero
        {
            get
            {
                return new RelayCommand(param => backZero());
            }
        }
        #endregion

        #region Methods

        private void backZero()
        {
            Search = string.Empty;
            BoolIconBack = false;
            UserControlTile ucTile = new UserControlTile();
            ucTile.DataContext = this;
            Pagina = ucTile;
            banFiltro = false;
            ListaDocumento = ListaDocumentoOriginal;
        }

        private void verArchivo(int idVersion)
        {
            ObservableCollection<Archivo> archivos = DataManagerControlDocumentos.GetArchivos(idVersion);
            if (archivos.Count > 0)
            {
                Archivo archivo = archivos[0];
                try
                {
                    string filename = GetPathTempFile(archivo);
                    File.WriteAllBytes(filename, archivo.archivo);
                    Process.Start(filename);
                }
                catch (Exception)
                {
                }
            }

        }

        private ObservableCollection<Documento> changeList()
        {
            if (IndexPagination == 0)
            {
                ObservableCollection<Documento> aux = new ObservableCollection<Documento>();
                int paginas = ListaDocumento.Count % 16 > 0 ? (ListaDocumento.Count / 16) + 1 : ListaDocumento.Count / 16;
                lblNumPages = "1 de " + paginas + " páginas";
                NotifyChange("LblNumPages");
                foreach (var item in ListaDocumento.Take(16).ToList())
                {
                    aux.Add(item);
                }
                return aux;
            }
            else
            {
                ObservableCollection<Documento> aux = new ObservableCollection<Documento>();

                int tempIndex = (indexPagination * 16);
                int paginas = ListaDocumento.Count % 16 > 0 ? (ListaDocumento.Count / 16) + 1 : ListaDocumento.Count / 16;

                lblNumPages = (indexPagination + 1) + " de " + paginas + " páginas";
                NotifyChange("LblNumPages");
                if ((indexPagination + 1) == paginas)
                {
                    foreach (var item in ListaDocumento.ToList().GetRange(tempIndex, ListaDocumento.Count % 16))
                    {
                        aux.Add(item);
                    }
                }
                else
                {
                    foreach (var item in ListaDocumento.ToList().GetRange(tempIndex, 16))
                    {
                        aux.Add(item);
                    }
                }
                NotifyChange("ListaDocumento");
                return aux;
            }
        }

        private void allNext()
        {
            int paginas = ListaDocumento.Count % 16 > 0 ? (ListaDocumento.Count / 16) + 1 : ListaDocumento.Count / 16;
            IndexPagination = paginas - 1;
            NotifyChange("ListaDocumentoAux");
        }

        private void simpleNext()
        {
            int paginas = ListaDocumento.Count % 16 > 0 ? (ListaDocumento.Count / 16) + 1 : ListaDocumento.Count / 16;

            IndexPagination++;
            if ((IndexPagination) >= paginas)
            {
                IndexPagination = paginas - 1;
            }
            else
            {
                NotifyChange("ListaDocumentoAux");
            }
        }

        private void simpleBack()
        {
            if (IndexPagination > 0)
            {
                IndexPagination--;
                NotifyChange("ListaDocumentoAux");
            }
        }

        private void allBack()
        {
            IndexPagination = 0;
            NotifyChange("ListaDocumentoAux");
        }

        private void buscarPor(string filtro)
        {
            BoolIconBack = true;
            IndexPagination = 0;
            banFiltro = true;
            filtroA = filtro;
            UserControlListCustom ucListDocuments = new UserControlListCustom();

            List<Documento> lista = new List<Documento>();

            if (filtro == "OHSAS")
            {
                lista = ListaDocumentoOriginal.Where(x => x.tipo.tipo_documento == "FORMATO OHSAS").ToList();

                lista.AddRange(ListaDocumentoOriginal.Where(x => x.tipo.tipo_documento == "PROCEDIMIENTO OHSAS").ToList());
            }
            else if(filtro == "ISO-14001")
            {
                lista = ListaDocumentoOriginal.Where(x => x.tipo.tipo_documento == "FORMATO ISO-14001").ToList();

                lista.AddRange(ListaDocumentoOriginal.Where(x => x.tipo.tipo_documento == "PROCEDIMIENTO ISO-14001").ToList());
            }
            else
            {
                lista = ListaDocumentoOriginal.Where(x => x.tipo.tipo_documento == filtro).ToList();
            }

            ListaDocumento = new ObservableCollection<Documento>();

            List<Documento> tempListaDocumento = new List<Documento>();
            tempListaDocumento = lista.OrderBy(x => x.nombre).ToList();

            foreach (var item in tempListaDocumento)
                ListaDocumento.Add(item);

            ucListDocuments.DataContext = this;

            Pagina = ucListDocuments;
        }

        /// <summary>
        /// Busqueda por nombre, descripción etc.
        /// </summary>
        /// <param name="param"></param>
        private void changeScreen(string param)
        {
            BoolIconBack = true;
            IndexPagination = 0;
            if (!String.IsNullOrWhiteSpace(param))
            {
                if (banFiltro)
                {
                    List<Documento> lista = new List<Documento>();

                    if (filtroA == "OHSAS")
                    {
                        lista = ListaDocumentoOriginal.Where(x => x.tipo.tipo_documento == "FORMATO OHSAS").ToList();

                        lista.AddRange(ListaDocumentoOriginal.Where(x => x.tipo.tipo_documento == "PROCEDIMIENTO OHSAS").ToList());
                    }
                    else if (filtroA == "ISO-14001")
                    {
                        lista = ListaDocumentoOriginal.Where(x => x.tipo.tipo_documento == "FORMATO ISO-14001").ToList();

                        lista.AddRange(ListaDocumentoOriginal.Where(x => x.tipo.tipo_documento == "PROCEDIMIENTO ISO-14001").ToList());
                    }
                    else
                    {
                        lista = ListaDocumentoOriginal.Where(x => x.tipo.tipo_documento == filtroA).ToList();
                    }

                    List<Documento> listado = lista.Where(x => x.nombre.ToLower().Contains(param.ToLower()) || x.descripcion.ToLower().Contains(param.ToLower()) || x.Departamento.ToLower().Contains(param.ToLower())).OrderBy(x => x.nombre).ToList();

                    ListaDocumento.Clear();

                    foreach (var item in listado)
                        ListaDocumento.Add(item);

                    NotifyChange("ListaDocumentoAux");
                }
                else
                {
                    UserControlListCustom ucListDocuments = new UserControlListCustom();

                    List<Documento> lista = ListaDocumentoOriginal.Where(x => x.nombre.ToLower().Contains(param.ToLower()) || x.descripcion.ToLower().Contains(param.ToLower()) || x.Departamento.ToLower().Contains(param.ToLower())).OrderBy(x => x.tipo.tipo_documento).ToList();

                    ListaDocumento = new ObservableCollection<Documento>();

                    foreach (var item in lista)
                        ListaDocumento.Add(item);

                    ucListDocuments.DataContext = this;

                    Pagina = ucListDocuments;
                }
            }
            else
            {
                if (string.IsNullOrEmpty(param))
                {
                    BoolIconBack = false;
                    UserControlTile ucTile = new UserControlTile();
                    ucTile.DataContext = this;
                    Pagina = ucTile;
                    banFiltro = false;
                    ListaDocumento = ListaDocumentoOriginal;
                }
                else
                {
                    if (banFiltro)
                    {
                        List<Documento> listado = ListaDocumentoOriginal.Where(x => x.tipo.tipo_documento == filtroA && (x.nombre.ToLower().Contains(param.ToLower()) || x.descripcion.ToLower().Contains(param.ToLower()))).ToList();

                        ListaDocumento.Clear();

                        foreach (var item in listado)
                            ListaDocumento.Add(item);
                    }
                    else
                    {
                        UserControlTile ucTile = new UserControlTile();
                        ucTile.DataContext = this;
                        Pagina = ucTile;
                        banFiltro = false;
                        ListaDocumento = ListaDocumentoOriginal;
                    }
                }
            }
        }

        private void Animation0_Completed(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private string GetPathTempFile(Archivo item)
        {
            var tempFolder = Path.GetTempPath();
            string filename = string.Empty;

            do
            {
                string aleatorio = GetRandomString(5);
                filename = Path.Combine(tempFolder, item.nombre + item.numero + "_" + aleatorio + item.ext);
            } while (File.Exists(filename));

            return filename;
        }

        public static string GetRandomString(int length)
        {
            Random random = new Random();
            const string chars = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        #endregion

        #region Constructors

        public MainViewModel()
        {
            Pagina = new UserControlTile();

            ListaDocumentoOriginal = DataManagerControlDocumentos.GetGridDocumentos(string.Empty);
            ListaDocumento = ListaDocumentoOriginal;

            BoolGifDownload = false;
            BoolImageDownload = true;
            BoolIconBack = false;
        }

        #endregion

        #region Events INotifyPropertyChanged
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