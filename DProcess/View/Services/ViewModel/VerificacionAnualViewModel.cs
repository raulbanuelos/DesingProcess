using DataAccess.ServiceObjects.VerificacionAnual;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.ControlDocumentos;
using SpreadsheetLight;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using View.Forms.ControlDocumentos;
using View.Forms.Tooling;
using View.Resources;

namespace View.Services.ViewModel
{
    public class VerificacionAnualViewModel : INotifyPropertyChanged
    {

        #region Propiedades

        public Usuario ModelUsuario;

        #endregion

        public string nombrearchivo;

        #region PropertyChanged

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

        #region Constructor

        /// <summary>
        ///  Constructor para cargar lista de grupos por usuario logueado
        /// </summary>
        /// <param name="Usuario"></param>
        public VerificacionAnualViewModel(Usuario Usuario)
        {
            ModelUsuario = Usuario;
        }

        #endregion

        #region Comandos

        /// <summary>
        /// Comando que manda al método "adjuntararchivoherramentales"
        /// </summary>
        public ICommand AdjuntarArchivoHerramentales
        {
            get
            {
                return new RelayCommand(o => adjuntararchivoherramentales());
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para ir a la ventana para notificar
        /// </summary>
        public void irnotificara()
        {
            WNotificarA notificara = new WNotificarA();
            NotificarAViewModel vwnotifa = new NotificarAViewModel(ModelUsuario);

            notificara.DataContext = vwnotifa;
            notificara.ShowDialog();
        }

        /// <summary>
        /// Método para adjuntar el documento de herramental para subir 
        /// </summary>
        /// <param name="nombrearchivo"></param>
        private async void adjuntararchivoherramentales()
        {
            // Inicializamos los servicios
            DialogService dialog = new DialogService();

            // Declaramos la ventana de explorador de archivos
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            // Filtramos los archivos excel
            dlg.Filter = "Excel Files (.xlsm, .xlsx)|*.xlsm; *.xlsx";

            // Mensaje para informar nombre de celdas en archivo
            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCellsName);

            // Abrimos la ventana de explorador de archivos
            bool? respuesta = dlg.ShowDialog();

            // Validación que no truene si no se selecciona un archivo
            if (respuesta == true)
            {
                // Asignamos a la variable la ruta del archivo que fue seleccionado
                nombrearchivo = dlg.FileName;

                // Validamos que el archivo no este abierto
                if (!Module.IsFileInUse(nombrearchivo))
                {
                    // Declaramos librería para poder leer el archivo
                    SLDocument sl = new SLDocument(nombrearchivo);

                    // Inicializamos variable para leer solo el primer registro o renglón
                    int iRow = 1;

                    // Obtenemos valores de las celdas A1, A2, A3 (títulos de columna)
                    string A1_Obtenida = sl.GetCellValueAsString(iRow, 1);
                    string A2_Obtenida = sl.GetCellValueAsString(iRow, 2);
                    string A3_Obtenida = sl.GetCellValueAsString(iRow, 3);

                    // Nombre que deberán contener dichas celdas
                    string CellA1 = "Material";
                    string CellA2 = "Codigo_Herramental";
                    string CellA3 = "Descripcion";

                    // Validamos que el valor de las celdas obtenidas sea igual al valor necesario
                    if ((A1_Obtenida.ToUpper() == CellA1.ToUpper()) && (A2_Obtenida.ToUpper() == CellA2.ToUpper()) && (A3_Obtenida.ToUpper() == CellA3.ToUpper()))
                    {
                        // Mandamos llamar el método para leer los datos del archivo excel
                        LeerExcel(nombrearchivo);
                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFormatoNoValido);
                    }
                }
                else
                {
                    // Si el archivo está en uso mandamos un mensaje de alerta
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCierreArchivo);
                }
            }
        }

        /// <summary>
        /// Método para leer el archivo excel adjuntado
        /// </summary>
        /// <param name="nombrearchivo"></param>
        public void LeerExcel(string nombrearchivo)
        {
            // Asignamos la ruta del archivo a una nueva variable
            string path = nombrearchivo;

            // Declaramos método que funciona a la par con el paquete SpreadsheetLight instalado
            SLDocument sl = new SLDocument(path);

            // Declaramos una lista de tipo objeto
            List<DO_ProgramaAnual> ListaRegistros = new List<DO_ProgramaAnual>();

            // Declaramos el objeto
            var Objeto = new DO_ProgramaAnual();

            // Manda llamar al método que hace una consulta y elimina todos los registros de la tabla TBL_PROGRAMA_ANUAL
            DataManager.Delete_AllRecords();

            // Inicializamos la variable para que comience a partir del registro 2, ignorando los encabezados de la tabla
            int iRow = 2;

            // Ciclo que recorre los renglones del excel hasta encontrar uno vacío
            while (!string.IsNullOrEmpty(sl.GetCellValueAsString(iRow, 1)))
            {
                // Se declaran variables para guardar los datos de las celdas
                string valormaterial = sl.GetCellValueAsString(iRow, 1);
                string valorcodigoherramental = sl.GetCellValueAsString(iRow, 2);
                string valordescripcion = sl.GetCellValueAsString(iRow, 3);

                // Limpiamos el objeto
                Objeto = new DO_ProgramaAnual();

                // Le asignamos los valores al objeto
                Objeto.material = valormaterial;
                Objeto.codigo_herramental = valorcodigoherramental;
                Objeto.descripcion = valordescripcion;

                // Agregamos el registro completo leído
                ListaRegistros.Add(Objeto);

                // Contador 
                iRow++;
            }

            // Revisamos que la lista de registros no esté vacía
            if (ListaRegistros != null)
            {
                // Recorremos toda la lista
                foreach (var item in ListaRegistros)
                {
                    // Mandamos llamar al método que inserta registros
                    DataManager.Insert_ProgramaAnual(item.material, item.codigo_herramental, item.descripcion);
                }
            }

            // Ejecutamos la consulta de búsqueda por descripción guardada como procedimiento almacenado
            List<DO_ProgramaAnual> ListaExportar = DataManager.Get_ToolingVerificacionAnual();

            // Mandamos llamar el método para crear el excel que se va a exportar
            CrearExcel(ListaExportar);
        }

        /// <summary>
        /// Método para crear el archivo excel a partir de el DataSet
        /// </summary>
        /// <param name="ListaExportar"></param>
        public async void CrearExcel(List<DO_ProgramaAnual> ListaExportar)
        {
            // Inicializamos los servicios
            DialogService dialog = new DialogService();
            MetroDialogSettings settings = new MetroDialogSettings();

            settings.AffirmativeButtonText = StringResources.lblYes;
            settings.NegativeButtonText = StringResources.lblNo;

            // Ruta del archivo vacía
            string ruta_nombre = string.Empty;

            // Desglosamos el nombre del archivo (nombre y extensión)      
            string nombre = "ProgramaVerificación";
            string extension = ".xlsx";

            // Asignamos el nombre del documento al crear  
            string nombrearchivo = nombre + extension;

            // Declaramos el uso de librería para abrir el explorador de archivos
            FolderBrowserDialog WindowDialog = new FolderBrowserDialog();

            // Abrimos el explorador de archivos para elegir ruta
            DialogResult result = WindowDialog.ShowDialog();

            // Validamos que la ruta no esté vacía
            if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(WindowDialog.SelectedPath))
            {
                // Concatenamos la ruta y el nombre del archivo
                ruta_nombre = WindowDialog.SelectedPath + "\\" + nombrearchivo;

                int contNombre = 1;

                // Mientras exista un archivo con el mismo nombre
                while (File.Exists(ruta_nombre))
                {
                    // Concatenamos la ruta del archivo, nombre, contador y extensión
                    ruta_nombre = WindowDialog.SelectedPath + "\\" + nombre + "_" + contNombre + extension;

                    contNombre++;
                }
            }

            try
            {
                // Creamos el objeto SLDocument el cual creará el excel
                SLDocument sl = new SLDocument();

                // Declaramos contador en 2 para ignorar el encabezado de las columnas
                int cont = 2;

                // Definimos el estilo a utilizar
                SLStyle estilo = new SLStyle();

                // Títulos en negritas y letra 12
                estilo.Font.Bold = true;
                estilo.Font.FontSize = 12;

                // Aplicamos estilo a las celdas
                sl.SetCellStyle("A1", estilo);
                sl.SetCellStyle("B1", estilo);
                sl.SetCellStyle("C1", estilo);

                // Asignamos nombre a las celdas
                sl.SetCellValue("A1", "Material");
                sl.SetCellValue("B1", "Codigo Herramental");
                sl.SetCellValue("C1", "Descripción");

                // Iteramos la lista para empezar a llenar las celdas necesarias
                foreach (var item in ListaExportar)
                {
                    sl.SetCellValue("A" + cont, item.material);
                    sl.SetCellValue("B" + cont, item.codigo_herramental);
                    sl.SetCellValue("C" + cont, item.descripcion);

                    // Incrementamos contador
                    cont++;
                }

                // Guardamos como, ponemos la ruta concatenada con el nombre del archivo
                sl.SaveAs(ruta_nombre);

                // Mensaje cuando termina el proceso
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHerramentalrevisar);

                // Leemos la respuesta
                MessageDialogResult resp = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgNotifySomeone, settings, MessageDialogStyle.AffirmativeAndNegative);

                // Si la respuesta es si...
                if (MessageDialogResult.Affirmative == resp)
                {
                    // Se manda llamar el método que abre la ventana para notificar
                    irnotificara();
                }
                else
                {
                    //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                    var window = System.Windows.Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                    //Verificamos que la pantalla sea diferente de nulo.
                    if (window != null)
                    {
                        //Cerramos la pantalla
                        window.Close();
                    }
                }
            }
            catch (Exception er)
            {
                // Si algo sale mal, mandamos este mensaje
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorexportar);
            }
        }

        #endregion
    }
}
