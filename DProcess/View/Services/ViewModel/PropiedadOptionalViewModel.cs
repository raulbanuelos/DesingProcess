using Model;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace View.Services.ViewModel
{
    public class PropiedadOptionalViewModel : INotifyPropertyChanged
    {
        #region Attributes
        public PropiedadOptional model;
        #endregion

        #region Properties

        public ObservableCollection<FO_Item> ListaOptional
        {
            get { return model.ListaOpcional; }
            set { model.ListaOpcional = value; NotifyChange("ListaOptional"); }
        }
        
        public FO_Item ElementSelected
        {
            get { return model.ElementSelected; }
            set { model.ElementSelected = value; NotifyChange("ElementSelected"); }
        }
        
        public string lblTitle
        {
            get { return model.lblTitle; }
            set { model.lblTitle = value; NotifyChange("lblTitle"); }
        }

        public string TipoPefil {
            get { return model.TipoPerfil; }
            set { model.TipoPerfil = value; NotifyChange("TipoPefil"); }
        }

        #endregion

        #region Contructor
        public PropiedadOptionalViewModel(List<FO_Item> Lista, string title)
        {
            model = new PropiedadOptional();
            ListaOptional = new ObservableCollection<FO_Item>();
            fillList(Lista);
            lblTitle = title;
        }

        public PropiedadOptionalViewModel(PropiedadOptional propiedad)
        {
            model = propiedad;
            ListaOptional = propiedad.ListaOpcional;
            lblTitle = propiedad.lblTitle;
            TipoPefil = propiedad.TipoPerfil;
        }


        #endregion

        #region Methods
        private void fillList(List<FO_Item> Lista)
        {
            foreach (var item in Lista)
            {
                ListaOptional.Add(item);
            }
        } 
        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
    }
}
