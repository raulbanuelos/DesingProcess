﻿using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.Tooling;

namespace View.Services.ViewModel
{
    public class ToolingViewModel : INotifyPropertyChanged
    {
        #region Attributtes
        public Usuario usuario;
        #endregion

        #region Properties
        private ObservableCollection<Herramental> maestroHerramental;
        public ObservableCollection<Herramental> MaestroHerramentales {
            get
            {
                return maestroHerramental;
            }
            set
            {
                maestroHerramental = value;
                NotifyChange("MaestroHerramentales");
            }
        }

        string textoBusqueda;
        public string TextoBusqueda {
            get
            {
                return textoBusqueda;
            }
            set
            {
                textoBusqueda = value;
                NotifyChange("TextoBusqueda");
            }
        }

        private Herramental _SelectedMHerramental;
        public Herramental SelectedMHerramental { get
            {
                return _SelectedMHerramental;
            }
            set
            {
                _SelectedMHerramental = value;
                NotifyChange("SelectedMHerramental");
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
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Constructors

        public ToolingViewModel(Usuario ModelUsuario)
        {
            usuario = ModelUsuario;
            maestroHerramental = new ObservableCollection<Herramental>();
            MaestroHerramentales = DataManager.GetMaestroHerramental(TextoBusqueda);
            TextoBusqueda = "";
        }
        #endregion

        #region Commands
        /// <summary>
        /// 
        /// </summary>
        public ICommand BuscarTooling
        {
            get
            {
                return new RelayCommand(param => buscarTooling((string)param));
            }
        }
        /// <summary>
        /// Comando para abrir la ventana de collar bk
        /// </summary>
        public ICommand IrCollarBK
        {
            get
            {
                return new RelayCommand(o => irCollarBK());
            }
        }

        /// <summary>
        /// Comando para abrir la ventana de Coil Feed Roller
        /// </summary>
        public ICommand IrCoilFR
        {
            get
            {
                return new RelayCommand(o => irCoilFR());
            }
        }

        /// <summary>
        /// Comando para abrir la ventana de Coil center guide
        /// </summary>
        public ICommand IrCoilCG
        {
            get
            {
                return new RelayCommand(o => irCoilCG());
            }
        }

        /// <summary>
        /// Comando para abrir la ventana de exit guide
        /// </summary>
        public ICommand IrExitGuide
        {
            get
            {
                return new RelayCommand(o => irExitGuide());
            }
        }

        /// <summary>
        /// Comando para abrir la ventana de external guide roller 1 piece
        /// </summary>
        public ICommand IrExternalGR1P
        {
            get
            {
                return new RelayCommand(o => irExternalGR1P());
            }
        }
        /// <summary>
        /// Comando para abrir la ventana de external guide roller 3 pieces_1
        /// </summary>
        public ICommand ExternalGR3P_1
        {
            get
            {
                return new RelayCommand(o => externalGR3P_1());
            }
        }
        /// <summary>
        /// Comando para abrir la ventana de external guide roller 3 pieces_2
        /// </summary>
        public ICommand ExternalGR3P_2
        {
            get
            {
                return new RelayCommand(o => externalGR3P_2());
            }
        }
        /// <summary>
        /// Comando para abrir la ventana de external guide roller 3 pieces_3
        /// </summary>
        public ICommand ExternalGR3P_3
        {
            get
            {
                return new RelayCommand(o => externalGR3P_3());
            }
        }
        /// <summary>
        /// Comando para abrir la ventana de shim of the cut system
        /// </summary>
        public ICommand IrShimCS
        {
            get
            {
                return new RelayCommand(o => irShimCS());
            }
        }

        /// <summary>
        /// Comando para agregar un nuevo herramental
        /// </summary>
        public ICommand IrNuevoMaestro
        {
            get
            {
                return new RelayCommand(o => nuevoMaestro());
            }
        }
        /// <summary>
        /// Comando para ver la informacion de un herramental
        /// </summary>
        public ICommand VerDetalleHerramental
        {
            get
            {
                return new RelayCommand(o => verHerramental());
            }
        }

        /// <summary>
        /// Comando para abrir la ventana de Cutter Spacer Splitter
        /// </summary>
        public ICommand IrCutterSpacer
        {
            get
            {
                return new RelayCommand(o => irCutterSpacer());
            }
        }

        /// <summary>
        /// Comando para abrir la ventana de Cutter Splitter
        /// </summary>
        public ICommand IrCutterSplitter
        {
            get
            {
                return new RelayCommand(o => irCutterSplitter());
            }
        }

        /// <summary>
        /// Comando que abre la ventana de ChuckSplitter
        /// </summary>
        public ICommand IrChuckSplitter
        {
            get
            {
                return new RelayCommand(o => irChuckSplitter());
            }
        }

        /// <summary>
        /// Comando que abre la ventana de Uretano Splitter
        /// </summary>
        public ICommand IrUretanoSplitter
        {
            get
            {
                return new RelayCommand(o => irUretanoSplitter());
            }
        }

        /// <summary>
        /// Comando que abre la ventana de Closing Sleeve BK.
        /// </summary>
        public ICommand IrClosingS
        {
            get
            {
                return new RelayCommand(o => irClosingS());
            }
        }

        /// <summary>
        /// Comando que abre la ventana de Guide Plate BK.
        /// </summary>
        public ICommand IrGuidePlate
        {
            get
            {
                return new RelayCommand(o => irGuideP());
            }
        }

        /// <summary>
        /// Comando que abre la ventana de GuillotinaBK
        /// </summary>
        public ICommand IrGuillotinaBK
        {
            get
            {
                return new RelayCommand(o => irGuillotinaBK());
            }
        }

        /// <summary>
        /// Comando que abre la ventana de CollarSpacer.
        /// </summary>
        public ICommand IrCollarSpacer
        {
            get
            {
                return new RelayCommand(o => irCollarSpacer());
            }
        }

        /// <summary>
        /// Comando que abre la ventana de WorkCamTurn.
        /// </summary>
        public ICommand IrWorkCam
        {
            get
            {
                return new RelayCommand(o => irWorkcam());
            }
        }
        /// <summary>
        /// Comando que abre la ventana de Cutter Cam Turn.
        /// </summary>
        public ICommand IrCutterCT
        {
            get
            {
                return new RelayCommand(o => ircutterCT());
            }
        }

        /// <summary>
        /// Comando que abre la ventana de bushing Bates bore.
        /// </summary>
        public ICommand IrBushingBB
        {
            get
            {
                return new RelayCommand(o => irBushingBB());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand IrBushingFM
        {
            get
            {
                return new RelayCommand(o => irBushingFM());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand IrCamBK
        {
            get
            {
                return new RelayCommand(o => irCamBK());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public ICommand IrShieldBK
        {
            get
            {
                return new RelayCommand(o => irShieldBK());
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Método que muestra la ventana de Cutter Spacer splitter
        /// </summary>
        private void irCutterSpacer()
        {
            WCutterSpacerS frm = new WCutterSpacerS();
            CutterSpacerS_VM context = new CutterSpacerS_VM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// Método que muestra la ventana de Cutter splitter
        /// </summary>
        private void irCutterSplitter()
        {
            WCutterSplitter frm = new WCutterSplitter();
            CutterSplitterVM context = new CutterSplitterVM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="busqueda"></param>
        private void buscarTooling(string busqueda)
        {
            MaestroHerramentales = DataManager.GetMaestroHerramental(busqueda);
        }

        /// <summary>
        /// método que muestra la ventana de collarbk
        /// </summary>
        private void irCollarBK()
        {
            WCollarBK wCollar = new WCollarBK();

            CollarAutoFinTurnViewModel vm = new CollarAutoFinTurnViewModel();

            wCollar.DataContext = vm;

            wCollar.ShowDialog();
        }
        /// <summary>
        /// Método que muestra la ventana de coil feed roller
        /// </summary>
        private void irCoilFR()
        {
            WCoil wcoil = new WCoil();
            CoilFeedRoller_VM vm = new CoilFeedRoller_VM();
            wcoil.DataContext = vm;
            wcoil.ShowDialog();
        }

        /// <summary>
        /// Método que muestra la ventana de coil center guide
        /// </summary>
        private void irCoilCG()
        {
            WCoil_D wcoil_dos = new WCoil_D();
            CoilCenterG_VM vm = new CoilCenterG_VM();
            wcoil_dos.DataContext = vm;
            wcoil_dos.ShowDialog();
        }

        /// <summary>
        /// Método que muestra la ventana de exit guide
        /// </summary>
        private void irExitGuide()
        {
            WCoil_D wcoil_dos = new WCoil_D();
            ExitGuide_VM vm = new ExitGuide_VM();
            wcoil_dos.DataContext = vm;
            wcoil_dos.ShowDialog();
        }
        /// <summary>
        /// Método que muestra la ventana de external guide roller 1 piece
        /// </summary>
        private void irExternalGR1P()
        {
            WCoil wcoil = new WCoil();
            External_GR_1P vm = new External_GR_1P();
            wcoil.DataContext = vm;
            wcoil.ShowDialog();
        }
        /// <summary>
        /// Método que muestra la ventana de external guide roller 3 pieces_1
        /// </summary>
        private void externalGR3P_1()
        {
            WCoil wcoil = new WCoil();
            External_GR_3P_1VM vm = new External_GR_3P_1VM();
            wcoil.DataContext = vm;
            wcoil.ShowDialog();
        }
        /// <summary>
        /// Método que muestra la ventana de external guide roller 3 pieces_2
        /// </summary>
        private void externalGR3P_2()
        {
            WCoil wcoil = new WCoil();
            External_GR_3P_2VM vm = new External_GR_3P_2VM();
            wcoil.DataContext = vm;
            wcoil.ShowDialog();
        }
        /// <summary>
        /// Método que muestra la ventana de external guide roller 3 pieces_3
        /// </summary>
        private void externalGR3P_3()
        {
            WCoil wcoil = new WCoil();
            External_GR_3P_3VM vm = new External_GR_3P_3VM();
            wcoil.DataContext = vm;
            wcoil.ShowDialog();
        }
        /// <summary>
        /// Método que muestra la ventana de shim of the cut system
        /// </summary>
        private void irShimCS()
        {
            WCoil wcoil = new WCoil();
            ShimCutSystem_VM vm = new ShimCutSystem_VM();
            wcoil.DataContext = vm;
            wcoil.ShowDialog();
        }
        /// <summary>
        /// Método que muestra la ventana para agregar un nuevo registro de maestro herramental
        /// </summary>
        private void nuevoMaestro()
        {
            WMaestroHerramental wmaestro = new WMaestroHerramental();
            NuevoMaestroHerramental_VM vm = new NuevoMaestroHerramental_VM(usuario);
            wmaestro.DataContext = vm;
            wmaestro.ShowDialog();
            buscarTooling(string.Empty);
        }

        /// <summary>
        /// Método que muestra la ventana para ver el  herramental seleccionado
        /// </summary>
        private void verHerramental()
        {
            //Si se seleccionó un herramental
            if (SelectedMHerramental!= null)
            {
                WMaestroHerramental wHerramental = new WMaestroHerramental();
                NuevoMaestroHerramental_VM vm = new NuevoMaestroHerramental_VM(usuario,SelectedMHerramental);
                wHerramental.DataContext = vm;
                wHerramental.ShowDialog();
                //Obtiene la lista de herramentales
                TextoBusqueda = "";
                buscarTooling(string.Empty);
                
            }
        }

        /// <summary>
        /// Método que muestra la ventana de CutterSplitter
        /// </summary>
        private void irChuckSplitter()
        {
            WCutterSplitter frm = new WCutterSplitter();
            ChuckSplitterVM context = new ChuckSplitterVM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// Método que muestra la ventana de Uretano Splitter
        /// </summary>
        private void irUretanoSplitter()
        {
            WCutterSplitter frm = new WCutterSplitter();
            UretanoSplitterVM context = new UretanoSplitterVM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// Método que muestra la ventana de Closgin Sleeve BK.
        /// </summary>
        private void irClosingS()
        {
            WClosingSleeve frm = new WClosingSleeve();
            ClosingSleeveVM context = new ClosingSleeveVM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// Método que muestra la ventana de Guide Plate BK.
        /// </summary>
        private void irGuideP()
        {
            WGuillotinaBK frm = new WGuillotinaBK();
            GuidePlateBK_VM context = new GuidePlateBK_VM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// Método que muestra la ventana de Guillotina BK
        /// </summary>
        private void irGuillotinaBK()
        {
            WGuillotinaBK frm = new WGuillotinaBK();
            GuillotinaBK_VM context = new GuillotinaBK_VM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// Método que muestra la ventana de CollarSpacer.
        /// </summary>
        private void irCollarSpacer()
        {
            WCamTurn frm = new WCamTurn();
            CollarSpacerVM context = new CollarSpacerVM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// étodo que muestra la ventana de WorkCam.
        /// </summary>
        private void irWorkcam()
        {
            WWorkCamTurn frm = new WWorkCamTurn();
            WorkCamVM context = new WorkCamVM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// étodo que muestra la ventana de Cutter Cam turn.
        /// </summary>
        private void ircutterCT()
        {
            WCutterCamTurn frm = new WCutterCamTurn();
            CutterCamTurnVM context = new CutterCamTurnVM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// Método que muestra la ventana de Bushing BatesBore.
        /// </summary>
        private void irBushingBB()
        {
            WBatesBore frm = new WBatesBore();
            BushingBatesBoreVM context = new BushingBatesBoreVM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void irBushingFM()
        {
            WBatesBore frm = new WBatesBore();
            BushingFinishMill_VM context = new BushingFinishMill_VM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void irCamBK()
        {
            WClosingSleeve frm = new WClosingSleeve();
            CamBK_VM context = new CamBK_VM();
            frm.DataContext = context;
            frm.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        private void irShieldBK()
        {
            WClosingSleeve frm = new WClosingSleeve();
            ShieldBK_VM context = new ShieldBK_VM();
            frm.DataContext = context;
            frm.ShowDialog();
        }
        #endregion
    }
}
