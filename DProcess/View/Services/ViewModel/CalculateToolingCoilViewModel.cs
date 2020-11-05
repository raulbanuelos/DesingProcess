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

        private bool _banExportarExcel;
        public bool banExportarExcel
        {
            get { return _banExportarExcel; }
            set { _banExportarExcel = value; NotifyChange("banExportarExcel"); }
        }

        private List<Herramental> _ListaHerramental;
        public List<Herramental> ListaHerramental
        {
            get { return _ListaHerramental; }
            set { _ListaHerramental = value; NotifyChange("ListaHerramental"); }
        }

        #endregion

        #region Contructor
        public CalculateToolingCoilViewModel()
        {
            WidthAlambre = 0;
            ThicknessAlambre = 0;
            Componente = string.Empty;
            banExportarExcel = true;
            ListaHerramental = new List<Herramental>();
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
        public async void calcular()
        {
            DialogService dialogService = new DialogService();
            
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
                    Herramental herr1PieceExternal = new Herramental();
                    DataManager.GetEXTERNAL_GR_1P(WidthAlambre, out herr1PieceExternal);
                    ListaHerramental.Add(herr1PieceExternal);

                    Herramental herr1PieceInternal = new Herramental();
                    DataManager.GetINTERNAL_GR_1P(WidthAlambre, out herr1PieceInternal);
                    ListaHerramental.Add(herr1PieceInternal);

                    if (banExportarExcel)
                        ExportToExcel.ExportToolCoilCuadrado(Componente, herrFeed, herrCenterGuide, herrEntranceGuide, idealExitGuide, herr1PieceExternal, herr1PieceInternal);
                    
                }
                else
                {
                    Herramental aux1 = new Herramental();
                    DataManager.GetEXTERNAL_GR_3P_1(WidthAlambre, out aux1);
                    ListaHerramental.Add(aux1);

                   
                    Herramental aux1_1 = aux1;
                    aux1_1.DescripcionGeneral = "INTERNAL GUIDE ROLLER 3 PIECES 2487-110-01-4";
                    ListaHerramental.Add(aux1_1);

                    
                    Herramental aux1_2 = aux1;
                    aux1_2.DescripcionGeneral = "INTERNAL GUIDE ROLLER 3 PIECES 2487 - 110 - 02 - 4";
                    ListaHerramental.Add(aux1_2);

                    
                    Herramental aux1_3 = aux1;
                    aux1_3.DescripcionGeneral = "INTERNAL GUIDE ROLLER 3 PIECES 2487-110-03-4";
                    ListaHerramental.Add(aux1_3);

                    Herramental aux2 = new Herramental();
                    DataManager.GetEXTERNAL_GR_3P_2(WidthAlambre, out aux2);
                    ListaHerramental.Add(aux2);

                   
                    Herramental aux2_1 = aux2;
                    aux2_1.DescripcionGeneral = "External roller (2487 111 01 4)";
                    ListaHerramental.Add(aux2_1);

                   
                    Herramental aux2_2 = aux2;
                    aux2_2.DescripcionGeneral = "External roller (2487 111 02 4)";
                    ListaHerramental.Add(aux2_2);

                    
                    
                    Herramental aux2_3 = aux2;
                    aux2_3.DescripcionGeneral = "External roller (2487 111 03 4)";
                    ListaHerramental.Add(aux2_3);

                    Herramental aux3 = new Herramental();
                    DataManager.GetEXTERNAL_GR_3P_3(WidthAlambre, out aux3);
                    ListaHerramental.Add(aux3);

                    if (banExportarExcel)
                        ExportToExcel.ExportToolCoilTHM(Componente, herrFeed, herrCenterGuide, herrEntranceGuide, idealExitGuide, aux1,aux1_1,aux1_2,aux1_3, aux2, aux2_1,aux2_2,aux2_3, aux3);
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
