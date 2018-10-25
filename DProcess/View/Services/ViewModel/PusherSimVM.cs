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
using View.Resources;

namespace View.Services.ViewModel
{
    public class PusherSimVM : INotifyPropertyChanged
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

        private DataTable _ListaHerramentales;
        public DataTable ListaHerramentales
        {
            get
            {
                return _ListaHerramentales;
            }
            set
            {
                _ListaHerramentales = value;
                NotifyChange("ListaHerramentales");
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

        private string _titulo;
        public string Titulo
        {
            get { return _titulo; }
            set { _titulo = value; NotifyChange("Titulo"); }
        }

        private string _texto;
        public string Texto
        {
            get { return _texto; }
            set { _texto = value; NotifyChange("Texto"); }
        }
        #endregion

        #region Commands
        /// <summary>
        /// Comando que obtiene los registros buscados
        /// </summary>
        public ICommand BusquedaHerramental
        {
            get
            {
                return new RelayCommand(param => busquedaSim((string)param));
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
        private void busquedaSim(string texto)
        {
            ListaHerramentales = DataManager.GetAllPusherSim(texto);
        }

        /// <summary>
        /// Método que busca los herramentales más óptimos de acuerdo a..
        /// </summary>
        private async void buscarOptimos()
        {
            //Limpiamos las listas.
            ListaOptimos = new DataTable();
            ListaMejores = new DataTable();

            //Si los campos son difrentes de nulo o cero.
            if (Diam != 0)
            {
                ObservableCollection<Herramental> ListaHerramentales = new ObservableCollection<Herramental>();
                //Obtenemos la lista de los herramentales optimos.
                ListaOptimos = DataManager.GetPusherSim(Diam,out ListaHerramentales);
                //Obtenemos la lista del mejor herramental.

                ListaMejores = DataManager.SelectBest_PusherSim(ListaOptimos);

                //Si la lista no tiene información.
                if (ListaMejores.Rows.Count == 0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHerramental);
            }
            else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
        }
        #endregion

        #region Constructor
        public PusherSimVM()
        {
            dialog = new DialogService();
            busquedaSim(string.Empty);
            Titulo = "Pusher Sim";
            Texto = "Diámetro bushing";
        }
        #endregion

    }
}
