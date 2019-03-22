using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using View.Services;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Resources;
using System.Collections.ObjectModel;

namespace View.Services.ViewModel
{
    public class LoadingGuideAnillosVM : INotifyPropertyChanged
    {
        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Propiedades
        private DataTable _ListaHerramentalesLoadingGuideAnillos;
        public DataTable ListaHerramentalesLoadingGuideAnillos
        {
            get
            {
                return _ListaHerramentalesLoadingGuideAnillos;
            }
            set
            {
                _ListaHerramentalesLoadingGuideAnillos = value;
                NotifyChange("ListaHerramentalesLoadingGuideAnillos");
            }
        }

        private DataTable _ListaOptimos;
        public DataTable ListaOptimos
        {
            get
            {
                return _ListaOptimos;
            }
            set
            {
                _ListaOptimos = value;
                NotifyChange("ListaOptimos");
            }
        }

        private DataTable _ListaMejores;
        public DataTable ListaMejores
        {
            get
            {
                return _ListaMejores;
            }
            set
            {
                _ListaMejores = value;
                NotifyChange("ListaMejores");
            }
        }

        private decimal _DiametroAnillo;
        public decimal DiametroAnillo
        {
            get { return _DiametroAnillo; }
            set { _DiametroAnillo = value; NotifyChange("DiametroAnillo"); }
        }
        #endregion

        public ICommand BusquedaLoadingGuideAnillos
        {
            get
            {
                return new RelayCommand(param => Busqueda((string)param));
            }
        }

        public ICommand BuscarOptimos
        {
            get
            {
                return new RelayCommand(a => BuscarOptimoLoadingGuideAnillos());
            }
        }

        #region Constructor
        public LoadingGuideAnillosVM()
        {
            ListaMejores = new DataTable();
            ListaOptimos = new DataTable();
            Busqueda(string.Empty);
        }
        #endregion

        #region Métodos

        public void Busqueda(string TextoBuscar)
        {
            ListaHerramentalesLoadingGuideAnillos = DataManager.GetAllLoadingGuideAnillos(TextoBuscar);
        }

        public async void BuscarOptimoLoadingGuideAnillos()
        {
            DialogService dialog = new DialogService();

            ListaMejores.Clear();
            ListaOptimos.Clear();

            decimal DiamAnillo = Convert.ToDecimal(DiametroAnillo);

            if (DiamAnillo != 0)
            {
                ObservableCollection<Herramental> Data = DataManager.GetOptimosLoadingGuideAnillos();

                ObservableCollection<Herramental> DatosConvertidos = new ObservableCollection<Herramental>();

                
                foreach (var item in Data)
                {
                    Herramental NewCodigo = new Herramental();
                    PropiedadCadena NewMedidaNominal = new PropiedadCadena();

                    decimal Fraccion = Module.ConvertFracToDecimal(item.PropiedadesCadena[0].Valor);

                    decimal comparacion = (DiamAnillo - Fraccion);
                    decimal ValorEstatico = Convert.ToDecimal(0.0625);

                    if (comparacion <= ValorEstatico)
                    {
                        NewCodigo.Codigo = item.Codigo;

                        NewMedidaNominal.DescripcionCorta = "Medida Nominal";
                        NewMedidaNominal.Valor = Convert.ToString(Fraccion);

                        NewCodigo.PropiedadesCadena.Add(NewMedidaNominal);

                        DatosConvertidos.Add(NewCodigo);
                        
                    }
                }
                //llenamos la lista con los datos con los datos optimos
                ListaOptimos = DataManager.ConverToObservableCollectionHerramental_DataSet(DatosConvertidos, "ListaOptimos");

                ListaMejores = DataManager.SelectBestOptionLoadingGuideAnillos(ListaOptimos);             
            }
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }

            //decimal n = Module.ConvertFracToDecimal("2 1/2");
        }
        #endregion
    }
}
