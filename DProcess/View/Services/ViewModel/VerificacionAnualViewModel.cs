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

        #region Attributes

        string rutaArchivo;

        #endregion

        #region Propiedades

        public Usuario ModelUsuario;
        public string nombreArchivoAdjutado;
        public string nombreArchivoSalida;

        #endregion

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

            string body = "<BR><FONT size=2 face=Helv><FONT size=2 face=Helv>";
            body += "<P><P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Para notificar que ya está disponible el archivo con los herramentales a verificar de este año " + DateTime.Now.Year + ", </P></P>";
            body += "<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; el cual se hizo el análisis de acuerdo al pronóstico de producción de este año el cual fue compartido por logística así </P>";
            body += "<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; como los herramentales establecidos en el procedimiento <STRONG>W-3571-49282-es.</STRONG></P>";
            body += "<P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Dicho archivo se encuentra en la siguiente ruta:</P>";
            body += "<BR><P>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; " + rutaArchivo + "</P>";
            body += "<P>Cualquier duda quedo a sus órdenes.</P></FONT></FONT>";

            // Declaramos un objeto tipo archivo
            Archivo documento = new Archivo();

            // Le asignamos valores
            documento.nombre = nombreArchivoSalida;
            documento.ruta = rutaArchivo;
            documento.rutaIcono = @"/Images/E.jpg";

            // Declaramos lista tipo Archivo
            ObservableCollection<Archivo> ListaDoc = new ObservableCollection<Archivo>();

            // Insertamos el objeto a la lista
            ListaDoc.Add(documento);

            NotificarAViewModel vwnotifa = new NotificarAViewModel(ModelUsuario, body, ListaDoc, new List<Usuarios>(), "Listado de Verificación Anual " + DateTime.Now.Year);

            notificara.DataContext = vwnotifa;
            notificara.ShowDialog();
        }

        /// <summary>
        /// Método para adjuntar el documento de herramental para subir 
        /// </summary>
        /// <param name="nombrearchivo"></param>
        public async void adjuntararchivoherramentales()
        {
            // Inicializamos los servicios
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
            ProgressDialogController AsyncProgress;

            // Declaramos la ventana de explorador de archivos
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

            //Se crea el objeto de tipo archivo
            Archivo obj = new Archivo();

            // Filtramos los archivos Excel
            dlg.Filter = "Excel Files (.xlsm, .xlsx)|*.xlsm; *.xlsx";

            // Mensaje para informar nombre de celdas en archivo
            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgCellsName);

            // Abrimos la ventana de explorador de archivos
            bool? respuesta = dlg.ShowDialog();

            // Validación que no truene si no se selecciona un archivo
            if (respuesta == true)
            {
                // Asignamos a la variable la ruta del archivo que fue seleccionado
                nombreArchivoAdjutado = dlg.FileName;

                // Validamos que el archivo no este abierto
                if (!Module.IsFileInUse(nombreArchivoAdjutado))
                {
                    // Declaramos librería para poder leer el archivo
                    SLDocument sl = new SLDocument(nombreArchivoAdjutado);

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
                        //Ejecutamos el método para enviar un mensaje de espera mientras se lee el archivo.
                        AsyncProgress = await dialog.SendProgressAsync(StringResources.msgEspera, "");

                        // Mandamos llamar el método para leer los datos del archivo excel
                        List<DO_ProgramaAnual> ListaCrear = await LeerExcel(nombreArchivoAdjutado);

                        //Ejecutamos el método para cerrar el mensaje de espera.
                        await AsyncProgress.CloseAsync();

                        // Mandamos llamar el método para crear nuevo archivo a partir de la lista recibida
                        CrearExcel(ListaCrear);
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
        public Task<List<DO_ProgramaAnual>> LeerExcel(string nombrearchivo)
        {
            return Task.Run(() =>
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

                // Retornamos la lista con los registros finales
                return ListaExportar;
            });
        }

        /// <summary>
        /// Método para crear el archivo excel a partir de el DataSet
        /// </summary>
        /// <param name="ListaExportar"></param>
        public async void CrearExcel(List<DO_ProgramaAnual> ListaExportar)
        {
            // Inicializamos los servicios
            DialogService dialog = new DialogService();

            try
            {
                // Desglosamos el nombre del archivo (nombre y extensión)      
                string nombre = "ProgramaVerificación";
                string extension = ".xlsx";

                // Asignamos el nombre del documento al crear  
                nombreArchivoSalida = nombre + extension;

                // Declaramos el uso de librería para abrir el explorador de archivos
                FolderBrowserDialog WindowDialog = new FolderBrowserDialog();

                // Abrimos el explorador de archivos para elegir ruta
                DialogResult result = WindowDialog.ShowDialog();

                // Validamos que la ruta no esté vacía
                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(WindowDialog.SelectedPath))
                {
                    // Concatenamos la ruta y el nombre del archivo
                    rutaArchivo = WindowDialog.SelectedPath + "\\" + nombreArchivoSalida;

                    int contNombre = 1;

                    // Mientras exista un archivo con el mismo nombre
                    while (File.Exists(rutaArchivo))
                    {
                        nombreArchivoSalida = nombre + "_" + contNombre + extension;
                        // Concatenamos la ruta del archivo, nombre, contador y extensión
                        rutaArchivo = WindowDialog.SelectedPath + "\\" + nombreArchivoSalida;

                        contNombre++;
                    }

                    // Creamos el objeto SLDocument el cual creará el excel
                    SLDocument sl = new SLDocument();

                    // Declaramos contador en 4 para ignorar el encabezado de las columnas y los herramentales a verificar por default
                    int cont = 4;

                    // Definimos el estilo a utilizar
                    SLStyle estilo = new SLStyle();

                    // Títulos en negritas y letra 12
                    estilo.Font.Bold = true;
                    estilo.Font.FontSize = 12;                                        

                    // Aplicamos estilo a las celdas
                    sl.SetCellStyle("A1", estilo);
                    sl.SetCellStyle("B1", estilo);
                    sl.SetCellStyle("C1", estilo);

                    // Asignamos nombre de título a las celdas
                    sl.SetCellValue("A1", "Material");
                    sl.SetCellValue("B1", "Codigo Herramental");
                    sl.SetCellValue("C1", "Descripción");

                    // Asignamos valores de herramentales a verificar por default
                    sl.SetCellValue("A2", string.Empty);
                    sl.SetCellValue("B2", "1005296");
                    sl.SetCellValue("C2", "ARBOR  RL4 - 80  PTE 1  CAM TURN");
                    // Asignamos valores de herramentales a verificar por default
                    sl.SetCellValue("A3", string.Empty);
                    sl.SetCellValue("B3", "1005288");
                    sl.SetCellValue("C3", "ARBOR AND NUT PTES. 1 Y 2      CAM TURN");

                    // Iteramos la lista para empezar a llenar las celdas necesarias
                    foreach (var item in ListaExportar)
                    {
                        sl.SetCellValue("A" + cont, item.material);
                        sl.SetCellValue("B" + cont, item.codigo_herramental);
                        sl.SetCellValue("C" + cont, item.descripcion);

                        // Incrementamos contador
                        cont++;
                    }

                    // Guardamos como..., ponemos la ruta concatenada con el nombre del archivo
                    sl.SaveAs(rutaArchivo);

                    // Mensaje cuando termina el proceso               
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgHerramentalrevisar);

                    // Se manda llamar el método que abre la ventana para notificar
                    irnotificara();

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
