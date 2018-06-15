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
using View.Resources;

namespace View.Services.ViewModel
{
    public class ClosingSleeveVM : INotifyPropertyChanged
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
        public DataTable ListaBK {
            get
            {
                return _ListaBK;
            }
            set
            {
                _ListaBK = value;
                NotifyChange("ListaBK");
            }
        }

        private DataTable _ListaOptimos;
        public DataTable ListaOptimos {
            get {
                return _ListaOptimos;
            }
            set
            {
                _ListaOptimos = value;
                NotifyChange("ListaOptimos");
            }
        }

        private DataTable _ListaMejores;
        public DataTable ListaMejores { get
            {
                return _ListaMejores;
            }
            set
            {
                _ListaMejores = value;
                NotifyChange("ListaMejores");
            }
        }

        private double diam;
        public double Diam
        {
            get { return diam; }
            set { diam = value; NotifyChange("Diam"); }
        }

        private double gap;
        public double Gap {
            get { return gap; }
            set { gap = value; NotifyChange("Gap"); } }

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
        public ICommand BusquedaBK
        {
            get
            {
                return new RelayCommand(param => busquedaBK((string) param));
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
        /// <summary>
        /// Comando que lee un excel y obtiene los codigos de los herramnetales.
        /// </summary>
        public ICommand LeerExcel
        {
            get
            {
                return new RelayCommand(o => leerExcel());
            }
        }

        #endregion

        #region Methods
        /// <summary>
        /// Método que obtiene la lista que coincidan con el texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        private void busquedaBK(string texto)
        {
            ListaBK = DataManager.GetAllClosingSleeve(texto);
        }

        /// <summary>
        /// Método que busca los herramentales más óptimos de acuerdo al diam y gap
        /// </summary>
        private async void buscarOptimos()
        {
            //Limpiamos las listas
            ListaMejores.Clear();
            ListaOptimos.Clear();

            //Si las variables son diferentes de cero
            if (diam !=0 & gap!=0 )
            {
                //Ejecutamos el método para buscar los collarines optimos.
                ListaOptimos = DataManager.GetClosingSleeve(diam, gap);

                //Ejecutamos el método para seleccionar la mejor opción de collarines.
                ListaMejores = DataManager.SelectBestClosingBK(ListaOptimos);

                //Verificamos que la cantidad de mejores herramentales sea mayor a cero.
                if (ListaOptimos.Rows.Count == 0)
                    //Enviamos un mensaje si no hay herramentales.
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHerramental);
            }
            else
                //Si están vacíos muestra un mensaje en pantalla
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
        }

        /// <summary>
        ///Método para importar un archivo excel, la información de ClosingSleeve.
        /// </summary>
        private async void leerExcel()
        {       

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController Progress;

            //Ejecutamos el método para enviar un mensaje de espera mientras el documento se guarda.
            Progress = await dialog.SendProgressAsync(StringResources.msgEspera, StringResources.msgGenerandoExcell);

            //Ejecutamos el método para obtener la información del Excel y crear el nuevo archivo excel.
            string result= await ImportExcel.ImportClosingSleeve();

            //Si hubo un error al leer el archivo o crear un nuevo archivo.
            if (result != null)
            {
                //Mostramos mensaje de error
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorGenerarArchivo);
            }

            //Ejecutamos el método para cerrar el mensaje de espera.
            await Progress.CloseAsync();
        }
        #endregion

        #region Constructor

        public ClosingSleeveVM()
        {
            //Obtiene la lista de todos los registros
            busquedaBK(string.Empty);
            dialog = new DialogService();
            ListaMejores = new DataTable();
            ListaOptimos = new DataTable();
            Texto = "Closing sleeve bk";
        }
        #endregion
    }
}
