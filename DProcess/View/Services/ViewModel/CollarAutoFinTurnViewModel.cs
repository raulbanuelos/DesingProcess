using MahApps.Metro.Controls.Dialogs;
using Model;
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
    public class CollarAutoFinTurnViewModel : INotifyPropertyChanged
    {
        #region Attributtes
        DialogService dialogService;
        #endregion

        #region Properties
        private DataTable listaHerramentales;
        public DataTable ListaHerramentales
        {
            get { return listaHerramentales; }
            set { listaHerramentales = value; NotifyChange("ListaHerramentales"); }
        }


        private DataTable listaHerramentalesOptimos;
        public DataTable ListaHerramentalesOptimos
        {
            get { return listaHerramentalesOptimos; }
            set { listaHerramentalesOptimos = value; NotifyChange("ListaHerramentalesOptimos"); }
        }

        private DataTable listaMejoresHerramentales;

        public DataTable ListaMejoresHerramentales
        {
            get { return listaMejoresHerramentales; }
            set { listaMejoresHerramentales = value; NotifyChange("ListaMejoresHerramentales"); }
        }



        private double dimA;
        public double DimA
        {
            get { return dimA; }
            set { dimA = value;  NotifyChange("DimA"); }
        }

        private double dimB;
        public double DimB
        {
            get { return dimB; }
            set { dimB = value; NotifyChange("DimB"); }
        }


        #endregion

        #region Commands
        public ICommand BuscarCollarBK
        {
            get
            {
                return new RelayCommand(param => buscarCollarBK((string)param));
            }
        }

        public ICommand BuscarCollars
        {
            get
            {
                return new RelayCommand(o => buscarCollarBK());
            }
        } 

        public ICommand LeerExcel
        {
            get
            {
                return new RelayCommand(o => leerExcel());
            }
        }
        
        #endregion

        #region Constructor
        public CollarAutoFinTurnViewModel()
        {
            buscarCollarBK(string.Empty);
            dialogService = new DialogService();
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

        #region Methods

        private async void leerExcel()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController Progress;

            //Ejecutamos el método para enviar un mensaje de espera mientras el documento se guarda.
            Progress = await dialog.SendProgressAsync("Por favor espere", "Generando archivo excel...");

            string e= await ImportExcel.ImportCollarBK();

            if (e != null)
            {
                //Cerramos el mensaje de espera
                await Progress.CloseAsync();

                //Mostramos mensaje de error
                await dialogService.SendMessage("Alerta", "Error al leer el archivo");
            }

            //Ejecutamos el método para cerrar el mensaje de espera.
            await Progress.CloseAsync();
        }

        /// <summary>
        /// Método que busca algun collar con el parámetro recibido.
        /// </summary>
        /// <param name="busqueda"></param>
        private void buscarCollarBK(string busqueda)
        {
            //Ejecutamos el método para buscar un collar con el parámetro recibido, el resultado lo asignamos a la lista de herramentales.
            ListaHerramentales = DataManager.GetCollarBK(busqueda);
        }

        /// <summary>
        /// Método que busca un collar de acuerdo a las dimenciones MaxA y MinB
        /// </summary>
        private async void buscarCollarBK()
        {
            //Inicializamos las listas de herramentales.
            ListaHerramentalesOptimos = new DataTable();
            ListaMejoresHerramentales = new DataTable();

            //Ejecutamos el método para buscar los collarines optimos.
            ListaHerramentalesOptimos = DataManager.GetCollarBK(DimA, DimB);

            //Ejecutamos el método para seleccionar la mejor opción de collarines.
            ListaMejoresHerramentales = DataManager.SelectBestCollar(ListaHerramentalesOptimos);

            //Verificamos que la cantidad de mejores herramentales sea mayor a cero.
            if (ListaMejoresHerramentales.Rows.Count == 0)
            {
                //Enviamos un mensaje si no hay herramentales.
                await dialogService.SendMessage("Alerta","No se encontro herramental con estas caracteristicas");
            }
        }
        #endregion
    }
}
