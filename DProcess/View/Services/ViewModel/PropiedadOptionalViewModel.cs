using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace View.Services.ViewModel
{
    public class PropiedadOptionalViewModel : INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<FO_Item> _ListaOptional;
        public ObservableCollection<FO_Item> ListaOptional
        {
            get { return _ListaOptional; }
            set { _ListaOptional = value; NotifyChange("ListaOptional"); }
        }

        private FO_Item _ElementSelected;
        public FO_Item ElementSelected
        {
            get { return _ElementSelected; }
            set { _ElementSelected = value; NotifyChange("ElementSelected"); }
        }
        
        private string _lblTitle;
        public string lblTitle
        {
            get { return _lblTitle; }
            set { _lblTitle = value; NotifyChange("lblTitle"); }
        }
        #endregion

        #region Contructor
        public PropiedadOptionalViewModel()
        {

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
