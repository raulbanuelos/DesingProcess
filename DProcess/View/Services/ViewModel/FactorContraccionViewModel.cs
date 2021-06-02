using MahApps.Metro.Controls;
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
    public class FactorContraccionViewModel : INotifyPropertyChanged
    {
        #region Atributtes
        DO_FactorContraccion model;
        #endregion

        #region Properties
        public double DIA_EXT_MAYOR {
            get
            {
                return model.DIA_EXT_MAYOR;
            }
            set
            {
                model.DIA_EXT_MAYOR = value;
                NotifyChange("DIA_EXT_MAYOR");
            }
        }
        public double DIA_EXT_MENOR {
            get
            {
                return model.DIA_EXT_MENOR;
            }
            set
            {
                model.DIA_EXT_MENOR = value;
                NotifyChange("DIA_EXT_MENOR");
            }
        }
        public double F_WIDTH {
            get
            {
                return model.F_WIDTH;
            }
            set
            {
                model.F_WIDTH = value;
                NotifyChange("F_WIDTH");
            }
        }
        public double F_THICKNESS {
            get
            {
                return model.F_THICKNESS;
            }
            set
            {
                model.F_THICKNESS = value;
                NotifyChange("F_THICKNESS");
            }
        }
        public double C_OLD {
            get
            {
                return model.C_OLD;
            }
            set
            {
                model.C_OLD = value;
                NotifyChange("C_OLD");
            }
        }
        public double C_OSD {
            get
            {
                return model.C_OSD;
            }
            set
            {
                model.C_OSD = value;
                NotifyChange("C_OSD");
            }
        }
        public double C_THICKNESS {
            get
            {
                return model.C_THICKNESS;
            }
            set
            {
                model.C_THICKNESS = value;
                NotifyChange("C_THICKNESS");
            }
        }
        public double C_WIDTH {
            get
            {
                return model.C_WIDTH;
            }
            set
            {
                model.C_WIDTH = value;
                NotifyChange("C_WIDTH");
            }
        }
        public double C_THROW {
            get
            {
                return model.C_THROW;
            }
            set
            {
                model.C_THROW = value;
                NotifyChange("C_THROW");
            }
        }
        public double P_THROW {
            get
            {
                return model.P_THROW;
            }
            set
            {
                model.P_THROW = value;
                NotifyChange("P_THROW");
            }
        }
        public bool Exists {
            get
            {
                return model.Exists;
            }
            set
            {
                model.Exists = value;
                NotifyChange("Exists");
            }
        }
        public string Material {
            get
            {
                return model.Material;
            }
            set
            {
                model.Material = value;
                NotifyChange("Material");
            }
        }
        public double ExtB
        {
            get
            {
                return Math.Round(-1 + DIA_EXT_MAYOR, 5);
            }
        }
        public double ExtMenor
        {
            get
            {
                return Math.Round(-1 + DIA_EXT_MENOR, 5);
            }
        }
        public double Width_M
        {
            get
            {
                return Math.Round(-1 + F_WIDTH, 5);
            }
        }
        public double Thickness_M
        {
            get
            {
                return Math.Round(-1 + F_THICKNESS, 5);
            }
        }
        public bool IsLB {
            get
            {
                return model.IsLB;
            }
            set
            {
                model.IsLB = value;
                NotifyChange("IsLB");
            }
        }
        #endregion

        #region Contructors
        public FactorContraccionViewModel(string _material, bool _IsLB)
        {
            model = new DO_FactorContraccion();
            model.Material = _material;
            IsLB = _IsLB;
        }
        #endregion

        #region Commands
        public ICommand Alta
        {
            get
            {
                return new RelayCommand(o => altaFactor());
            }
        }
        #endregion

        #region Methods
        private async void altaFactor()
        {
            int r = DataManager.InsertFactorContraccion(Material, DIA_EXT_MAYOR, DIA_EXT_MENOR, F_WIDTH, F_THICKNESS, IsLB);
            DialogService dialogService = new DialogService();
            if (r > 0)
                await dialogService.SendMessage("Atención", "Registro dado de alta correctamente");
            else
                await dialogService.SendMessage("Alerta", "No fue posible dar de alta la información, por favor intente mas tarde.");

            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
            var window = System.Windows.Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            window.Close();
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
