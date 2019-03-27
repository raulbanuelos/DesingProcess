using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Resources;

namespace View.Services.ViewModel
{
    public class ClosingBandLapeadoVM : INotifyPropertyChanged
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
        private DataTable _ListaHerramentalesClosingBand;
        public DataTable ListaHerramentalesClosingBand
        {
            get
            {
                return _ListaHerramentalesClosingBand;
            }
            set
            {
                _ListaHerramentalesClosingBand = value;
                NotifyChange("ListaHerramentalesClosingBand");
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

        private decimal _Diam;
        public decimal Diam
        {
            get { return _Diam; }
            set { _Diam = value; NotifyChange("Diam"); }
        }

        private string _TipoA;
        public string TipoA
        {
            get
            {
                return _TipoA;
            }
            set
            {
                _TipoA = value;
                NotifyChange("TipoA");
            }
        }

        public string TipoAnillo = string.Empty;

        #endregion

        public ICommand BusquedaClosingBand
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
                return new RelayCommand(a => BuscarOptimoClosingBand());
            }
        }

        public ClosingBandLapeadoVM()
        {

            ListaOptimos = new DataTable();
            ListaMejores = new DataTable();

            Busqueda(string.Empty);
        }

        public void Busqueda(string TextoBuscar)
        {
            ListaHerramentalesClosingBand = DataManager.GetAllClosingbandLapeado(TextoBuscar);
        }

        public async void BuscarOptimoClosingBand()
        {
            DialogService dialog = new DialogService();

            ListaMejores.Clear();
            ListaOptimos.Clear();

            if (Diam != 0)
            {
                TipoAnillo = "CLOSING BAND (CORTA)";

                if (TipoA == "RF10U" || TipoA == "RF18U" || TipoA == "00K10U" || TipoA == "BR18U" || TipoA == "RFK18U" || TipoA == "GTK18U")
                {
                    TipoAnillo = "CLOSING BAND (LARGA)";
                }

                ObservableCollection<Herramental> Data = DataManager.GetOptimosClosingBandLapeado(TipoAnillo);

                ObservableCollection<Herramental> NewData = new ObservableCollection<Herramental>();

                foreach (var item in Data)
                {
                    Herramental NewCodigo = new Herramental();
                    PropiedadCadena DescripcionHerramental = new PropiedadCadena();
                    PropiedadCadena NewMedidaNominal = new PropiedadCadena();

                    decimal Fraccion = Module.ConvertFracToDecimal(item.PropiedadesCadena[1].Valor);

                    decimal comparacion = (Diam - Fraccion);
                    decimal ValorEstatico = Convert.ToDecimal(0.0625);

                    if (comparacion <= ValorEstatico)
                    {
                        NewCodigo.Codigo = item.Codigo;

                        DescripcionHerramental.DescripcionCorta = "Descripción Herramental";
                        DescripcionHerramental.Valor = item.PropiedadesCadena[0].Valor;
                        NewCodigo.PropiedadesCadena.Add(DescripcionHerramental);

                        NewMedidaNominal.DescripcionCorta = "Medida Nominal";
                        NewMedidaNominal.Valor = Convert.ToString(Fraccion);
                        NewCodigo.PropiedadesCadena.Add(NewMedidaNominal);

                        NewData.Add(NewCodigo);
                    }
                }

                ListaOptimos = DataManager.ConverToObservableCollectionHerramental_DataSet(NewData, "ListaOptimos");

                ListaMejores = DataManager.SelectBestClosingBandLapeado(ListaOptimos);

                //Si la lista no tiene información.
                if (ListaMejores.Rows.Count == 0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHerramental);

            }
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }
        }
    }
}
