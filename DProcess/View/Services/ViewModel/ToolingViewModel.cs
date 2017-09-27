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
        }
        #endregion

        #region Commands
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
        #endregion

        #region Methods
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
        #endregion
    }
}
