using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using Model;

namespace View.Services.ViewModel
{
    public class BarrelLapAnillos_VM : INotifyPropertyChanged
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
        private DataTable _ListaHerramentalesBarrelLapAnillo;
        public DataTable ListaHerramentalesBarrelLapAnillo
        {
            get
            {
                return _ListaHerramentalesBarrelLapAnillo;
            }
            set
            {
                _ListaHerramentalesBarrelLapAnillo = value;
                NotifyChange("ListaHerramentalesBarrelLapAnillo");
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

        private string _MedidaNominal;
        public string MedidaNominal
        {
            get { return _MedidaNominal; }
            set { _MedidaNominal = value; NotifyChange("MedidaNominal"); }
        }
        #endregion

        #region Construcor
        public BarrelLapAnillos_VM()
        {
            Busqueda(string.Empty);
        }
        #endregion

        #region Métodos
        public void Busqueda(string TextoBuscar)
        {
            ListaHerramentalesBarrelLapAnillo = DataManager.GetALLBarrelLapAnillos_(TextoBuscar);
        }
        #endregion


    }
}
