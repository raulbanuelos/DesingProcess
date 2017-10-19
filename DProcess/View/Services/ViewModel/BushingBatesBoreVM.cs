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

namespace View.Services.ViewModel
{
    public class BushingBatesBoreVM : INotifyPropertyChanged
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

        private DataTable _ListaBatesBore;
        public DataTable ListaBatesBore
        {
            get
            {
                return _ListaBatesBore;
            }
            set
            {
                _ListaBatesBore = value;
                NotifyChange("ListaBatesBore");
            }
        }

        private DataTable _ListaOptimos;
        public DataTable ListaOptimos
        {
            get
            {
                return _ListaOptimos;
            }
            set
            {
                _ListaOptimos = value;
                NotifyChange("ListaOptimos");
            }
        }

        private DataTable _ListaMejores;
        public DataTable ListaMejores
        {
            get
            {
                return _ListaMejores;
            }
            set
            {
                _ListaMejores = value;
                NotifyChange("ListaMejores");
            }
        }

        private double _diam;
        public double Diam
        {
            get { return _diam; }
            set { _diam = value; NotifyChange("Diam"); }
        }


        #endregion

        #region Commands
        /// <summary>
        /// Comando que obtiene los registros buscados
        /// </summary>
        public ICommand BusquedaBatesBore
        {
            get
            {
                return new RelayCommand(param => busquedaBatesBore((string)param));
            }
        }

        /// <summary>
        /// Comando que buscar las coincidencias de acuerdo a las dimensiones
        /// </summary>
        public ICommand BuscarOptimos
        {
            get
            {
                return new RelayCommand(o => buscarOptimos());
            }
        }


        #endregion

        #region Methods

        /// <summary>
        /// Método que obtiene la lista que coincidan con el texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        private void busquedaBatesBore(string texto)
        {
            ListaBatesBore = DataManager.GetAllBushingBB(texto);
        }

        /// <summary>
        /// Método que busca los herramentales más óptimos de acuerdo a..
        /// </summary>
        private async void buscarOptimos()
        {
            //Limpiamos las listas.
            ListaMejores.Clear();
            ListaOptimos.Clear();

            //Si los campos son difrentes de nulo o cero.
            if ( Diam != 0)
            {
                //Obtenemos la lista de los herramentales optimos.
                ListaOptimos = DataManager.GetBushing(Diam);
                //Obtenemos la lista del mejor herramental.
                ListaMejores = DataManager.SelectBestBushing(ListaOptimos);

                //Si la lista no tiene información.
                if (ListaMejores.Rows.Count == 0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage("Alerta", "No se encontró herramental con estas características..");
            }
            else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage("Alerta", "Se debe llenar todos los campos...");
        }
        #endregion

        #region Constructor

        public BushingBatesBoreVM()
        {
            busquedaBatesBore(string.Empty);
            ListaOptimos = new DataTable();
            ListaMejores = new DataTable();
            dialog = new DialogService();
        }
        #endregion
    }
}