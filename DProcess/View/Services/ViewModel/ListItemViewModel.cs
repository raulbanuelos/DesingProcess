using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace View.Services.ViewModel
{
    public class ListItemViewModel : INotifyPropertyChanged
    {
        #region Properties
        private ObservableCollection<string> _ItemsList;
        public ObservableCollection<string> ItemsList
        {
            get { return _ItemsList; }
            set { _ItemsList = value; NotifyChange("ItemsList"); }
        }

        private string _SelectedItem;
        public string SelectedItem
        {
            get { return _SelectedItem; }
            set { _SelectedItem = value; NotifyChange("SelectedItem"); }
        }

        #endregion

        #region Constructors
        public ListItemViewModel(List<string> lista)
        {
            ItemsList = new ObservableCollection<string>();
            ListToObservableCollection(lista);
        }

        public ListItemViewModel(List<double> lista)
        {
            ItemsList = new ObservableCollection<string>();
            ListToObservableCollection(lista);
        }
        #endregion

        #region Methods

        void ListToObservableCollection(List<double> lista)
        {
            foreach (double item in lista)
                ItemsList.Add(item.ToString());
            
        }

        void ListToObservableCollection(List<string> lista)
        {
            foreach (string item in lista)
                ItemsList.Add(item);
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

    }
}
