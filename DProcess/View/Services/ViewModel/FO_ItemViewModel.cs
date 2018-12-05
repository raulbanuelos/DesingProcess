using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.ViewModel
{
    public class FO_ItemViewModel : INotifyPropertyChanged
    {
        private FO_Item model;

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

        #region Properties

        private ObservableCollection<FO_Item> _ListaAllOptions;
        public ObservableCollection<FO_Item> ListaAllOptions
        {
            get { return _ListaAllOptions; }
            set { _ListaAllOptions = value; NotifyChange("ListaAllOptions"); }
        }

        private string _NombreLista;
        public string NombreLista
        {
            get { return _NombreLista; }
            set { _NombreLista = value; NotifyChange("NombreLista"); }
        }
        
        #region Properties of Model
        public int id
        {
            get
            {
                return model.id;
            }
            set
            {
                model.id = value;
                NotifyChange("id");
            }
        }

        public string Nombre
        {
            get
            {
                return model.Nombre;
            }
            set
            {
                model.Nombre = value;
                NotifyChange("Nombre");
            }
        }

        public double Valor
        {
            get
            {
                return model.Valor;
            }
            set
            {
                model.Valor = value;
                NotifyChange("Valor");
            }
        }

        public string ValorCadena
        {
            get
            {
                return model.ValorCadena;
            }
            set
            {
                model.ValorCadena = value;
                NotifyChange("ValorCadena");
            }
        }

        public bool IsSelected
        {
            get
            {
                return model.IsSelected;
            }
            set
            {
                model.IsSelected = value;
                NotifyChange("IsSelected");

            }
        }

        public string Descripcion {
            get
            {
                return model.Descripcion;
            }
            set
            {
                model.Descripcion = value;
                NotifyChange("Descripcion");
            }
        }
        #endregion
        #endregion

        #region Constructors
        public FO_ItemViewModel()
        {
            model = new FO_Item();

        } 

        public FO_ItemViewModel(ObservableCollection<FO_Item> Listado, string nombreLista)
        {
            model = new FO_Item();
            ListaAllOptions = Listado;
            NombreLista = nombreLista;
        }
        #endregion
    }
}
