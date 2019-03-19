using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private string _MedidaNominal;
        public string MedidaNominal
        {
            get { return _MedidaNominal; }
            set { _MedidaNominal = value; NotifyChange("MedidaNominal"); }
        }

        private string _DescripcionHerramental;
        public string DescripcionHerramental
        {
            get { return _DescripcionHerramental; }
            set { _DescripcionHerramental = value; NotifyChange("DescripcionHerramental"); }
        }
        #endregion

        public ClosingBandLapeadoVM()
        {
            Busqueda(string.Empty);
        }

        public void Busqueda(string TextoBuscar)
        {
            ListaHerramentalesClosingBand = DataManager.GetAllClosingbandLapeado(TextoBuscar);
        }
    }
}
