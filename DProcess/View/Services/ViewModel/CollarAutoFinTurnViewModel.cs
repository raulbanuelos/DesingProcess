using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.Services.ViewModel
{
    public class CollarAutoFinTurnViewModel : INotifyPropertyChanged
    {
        #region Attributtes

        #endregion

        #region Properties
        private DataTable listaHerramentales;
        public DataTable ListaHerramentales
        {
            get { return listaHerramentales; }
            set { listaHerramentales = value; NotifyChange("ListaHerramentales"); }
        }


        private DataTable listaHerramentalesOptimos;
        public DataTable ListaHerramentalesOptimos
        {
            get { return listaHerramentalesOptimos; }
            set { listaHerramentalesOptimos = value; NotifyChange("ListaHerramentalesOptimos"); }
        }


        private double dimA;
        public double DimA
        {
            get { return dimA; }
            set { dimA = value;  NotifyChange("DimA"); }
        }

        private double dimB;
        public double DimB
        {
            get { return dimB; }
            set { dimB = value; NotifyChange("DimB"); }
        }


        #endregion

        public ICommand BuscarCollarBK
        {
            get
            {
                return new RelayCommand( param => buscarCollarBK((string)param));
            }
        }

        public ICommand BuscarCollars
        {
            get
            {
                return new RelayCommand(o => buscarCollarBK());
            }
        }
        #region Constructor
        public CollarAutoFinTurnViewModel()
        {
            buscarCollarBK(string.Empty);
        }
        #endregion

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

        #region Methods
        private void buscarCollarBK(string busqueda)
        {
            ListaHerramentales = DataManager.GetCollarBK(busqueda);
        }

        private void buscarCollarBK()
        {
            ListaHerramentalesOptimos = DataManager.GetCollarBK(DimA, DimB);
        }
        #endregion
    }
}
