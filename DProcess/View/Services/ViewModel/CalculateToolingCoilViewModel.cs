using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Resources;

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

        private string _Componente;

        public string Componente
        {
            get { return _Componente; }
            set { _Componente = value; NotifyChange("Componente"); }
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
            WidthAlambre = 0;
            ThicknessAlambre = 0;
            Componente = string.Empty;
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
        private async void calcular()
        {
            DialogService dialogService = new DialogService();

            List<Herramental> ListaHerramental = new List<Herramental>();

            if (validar())
            {
                Herramental herrFeed = new Herramental();
                DataManager.GetCOIL_Feed_Roller(WidthAlambre, out herrFeed);
                ListaHerramental.Add(herrFeed);

                Herramental herrCenterGuide = new Herramental();
                DataManager.GetCOIL_CENTER_GUIDE(WidthAlambre, ThicknessAlambre, out herrCenterGuide);
                ListaHerramental.Add(herrCenterGuide);

                Herramental herrEntranceGuide = new Herramental();
                DataManager.GetCOIL_ENTRANCE_GUIDE(WidthAlambre, ThicknessAlambre, out herrEntranceGuide);
                ListaHerramental.Add(herrEntranceGuide);

                Herramental idealExitGuide = new Herramental();
                DataManager.GetEXIT_GUIDE(WidthAlambre, ThicknessAlambre, out idealExitGuide);
                ListaHerramental.Add(idealExitGuide);

                if (banCuadrado)
                {
                    Herramental herr1Piece = new Herramental();
                    DataManager.GetEXTERNAL_GR_1P(WidthAlambre, out herr1Piece);
                    ListaHerramental.Add(herr1Piece);
                    ExportToExcel.ExportToolCoilCuadrado(Componente, herrFeed, herrCenterGuide, herrEntranceGuide, idealExitGuide, herr1Piece);
                }
                else
                {
                    Herramental aux1 = new Herramental();
                    DataManager.GetEXTERNAL_GR_3P_1(WidthAlambre, out aux1);
                    ListaHerramental.Add(aux1);

                    Herramental aux2 = new Herramental();
                    DataManager.GetEXTERNAL_GR_3P_2(WidthAlambre, out aux2);
                    ListaHerramental.Add(aux2);

                    Herramental aux3 = new Herramental();
                    DataManager.GetEXTERNAL_GR_3P_3(WidthAlambre, out aux3);
                    ListaHerramental.Add(aux3);

                    ExportToExcel.ExportToolCoilTHM(Componente, herrFeed, herrCenterGuide, herrEntranceGuide, idealExitGuide, aux1,aux2,aux3);
                }
                
            }
            else
            {
                await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }
        }

        private bool validar()
        {
            if (!banCuadrado && !banTHM)
                return false;

            if (WidthAlambre == 0 || ThicknessAlambre == 0)
                return false;

            if (string.IsNullOrEmpty(Componente))
                return false;

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
