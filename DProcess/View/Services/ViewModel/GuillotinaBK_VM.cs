﻿using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.Services.ViewModel
{
    public class GuillotinaBK_VM: INotifyPropertyChanged
    {
        #region Attributtes
        DialogService dialog;
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

        #region Propiedades
        private DataTable _ListaBK;
        public DataTable ListaBK
        {
            get { return _ListaBK; }
            set { _ListaBK = value; NotifyChange("ListaBK"); }
        }

        private DataTable _ListaMejores;
        public DataTable ListaMejores
        {
            get { return _ListaMejores; }
            set { _ListaMejores = value; NotifyChange("ListaMejores"); }
        }

        private DataTable _ListaOptimos;
        public DataTable ListaOptimos
        {
            get { return _ListaOptimos; }
            set { _ListaOptimos = value; NotifyChange("ListaOptimos"); }
        }
        //Diametro nominal
        private double _d1;
        public double D1 {
            get { return _d1; }
            set { _d1 = value; NotifyChange("D1"); }
        }

        //Width del anillo
        private double _h1;
        public double H1 {
            get { return _h1; }
            set { _h1 = value; NotifyChange("H1"); }
        }

        #endregion

        #region Commands
        /// <summary>
        /// Comando que obtiene los registros buscados.
        /// </summary>
        public ICommand BusquedaBK
        {
            get
            {
                return new RelayCommand(param => busquedaBK((string)param));
            }
        }

        /// <summary>
        /// Comando que buscar las coincidencias de acuerdo a la dimensión.
        /// </summary>
        public ICommand BuscarOptimos
        {
            get
            {
                return new RelayCommand(o => buscarOptimos());
            }
        }
        #endregion

        #region Propiedades
        /// <summary>
        /// Método que obtiene los registros.
        /// </summary>
        /// <param name="texto"></param>
        private void busquedaBK(string texto)
        {
            ListaBK = DataManager.GetAllGuillotinaBK(texto);
        }

        /// <summary>
        /// Método que obtiene un herramental de acuerdo a las dimensiones.
        /// </summary>
        private async void buscarOptimos()
        {
            //Limpiamos las listas
            ListaMejores.Clear();
            ListaOptimos.Clear();

            //Si los parámetros son diferente de cero.
            if (_d1 !=0 & _h1!=0)
            {
                ListaOptimos = DataManager.GetGuillotina(_d1, _h1);

                if(ListaOptimos.Rows.Count == 0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage("Alerta", "No se encontró herramental con estas características..");
            }
            else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage("Alerta", "Se debe llenar todos los campos...");
        }
        #endregion

        #region MyRegion
        public GuillotinaBK_VM()
        {
            dialog = new DialogService();
            busquedaBK(string.Empty);
            ListaMejores = new DataTable();
            ListaOptimos = new DataTable();
        }
        #endregion
    }
}