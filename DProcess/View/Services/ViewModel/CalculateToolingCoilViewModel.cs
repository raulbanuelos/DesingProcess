using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.Services.ViewModel
{
    public class CalculateToolingCoilViewModel : INotifyPropertyChanged
    {
        #region Properties
        private double _WidthAlambre;
        public double WidthAlambre
        {
            get { return _WidthAlambre; }
            set { _WidthAlambre = value; NotifyChange("WidthAlambre"); }
        }

        private double _ThicknessAlambre;
        public double ThicknessAlambre
        {
            get { return _ThicknessAlambre; }
            set { _ThicknessAlambre = value; NotifyChange("ThicknessAlambre"); }
        }

        private bool _banCuadrado;
        public bool banCuadrado
        {
            get { return _banCuadrado; }
            set { _banCuadrado = value; NotifyChange("banCuadrado"); }
        }
        
        private bool _banTHM;
        public bool banTHM
        {
            get { return _banTHM; }
            set { _banTHM = value; NotifyChange("banTHM"); }
        }

        #endregion

        #region Contructor
        public CalculateToolingCoilViewModel()
        {

        }
        #endregion

        #region Commands
        public ICommand Calcular
        {
            get
            {
                return new RelayCommand(o => calcular());
            }
        }
        #endregion

        #region Methods
        private void calcular()
        {
            if (validar())
            {
                //calcular
                //Feed Roller
                //Declaramos un objeto el cual almacenará el herramental ideal.
                Herramental herrFeed = new Herramental();

                //Obtiene la lista de los herramentales optimos
                DataManager.GetCOIL_Feed_Roller(WidthAlambre, out herrFeed);

                Herramental herrCenterGuide = new Herramental();
                DataManager.GetCOIL_CENTER_GUIDE(WidthAlambre, ThicknessAlambre, out herrCenterGuide);




            }
            else
            {
                //error, enviar mensaje en pantalla.
            }
        } 

        private bool validar()
        {
            if (!banCuadrado && !banTHM)
            {
                return false;
            }

            if (WidthAlambre == 0 || ThicknessAlambre == 0)
            {
                return false;
            }
            return true;
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
