using DataAccess.ServiceObjects.ControlDocumentos;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace View.Services
{

    public static class ImportExcel
    {
        /// <summary>
        /// Método para importar a un archivo excel, la información de collarBk
        /// </summary>
        /// <returns></returns>
        public async static Task<string> ImportCollarBK()
        {
            try
            {
                //Creamos un objeto de tipo OpenDialog
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                //Filtar los dpocumentos por extensión 
                dlg.Filter = "Excel Workbook|*.xlsx";

                // Mostrar el explorador de archivos
                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    //Se obtiene el nombre del documento
                    string filename = dlg.FileName;

                    //Creamos una instancia de la aplicación.
                    Excel.Application ExcelApp = new Excel.Application();

                    //Abre el documento
                    Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(filename, true);

                    //Creamos un objeto de tipo dataSet, donde se guardaran las tablas para crear el archivo Excel
                    DataSet ds = new DataSet();
                    //
                    DataTable table = new DataTable();

                    //Iteramos las hojas del archivo leído
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {

                        //Tabla donde se guardará la información del nuevo archivo de excel
                        DataTable dTable = new DataTable();

                        //Obtenemos el rango de la hoja que estamos leyendo
                        Excel.Range range = sheet.UsedRange;

                        //Verifica si la hoja no está vacía. 
                        int columUsed = range.Columns.Count;

                        //Obtiene el número de filas de la hoja
                        int rowCount = range.Rows.Count;

                        //Verifica que la hoja no esté vacía.
                        if (rowCount > 1)
                        {
                            //Se empieza a leer el documento en la fila 2.
                            int aux = 2;
                            string componente, MaxA, MaxB;

                            //Asigamos el nombre de la hoja a la tabla.
                            dTable.TableName = sheet.Name;

                            //Creamos las columnas del nuevo archivo.
                            dTable.Columns.Add("Componente");
                            dTable.Columns.Add("Código");
                            dTable.Columns.Add("Descripción");

                            //Repite mientras que el auxiliar sea menor al número de columnas.
                            while (aux <= rowCount)
                            {

                                //Extraemos los datos del archivo de excel, los guardamos el variables locales.
                                componente = range.Cells[aux, 1].Value2.ToString();
                                MaxA = range.Cells[aux, 2].Value2.ToString();
                                MaxB = range.Cells[aux, 3].Value2.ToString();

                                //Obtenemos los datos de los dos primeros registros con el max que se obtuvo en el excel.
                                table = DataManager.SelectBestCollar(DataManager.GetCollarBK(Convert.ToDouble(MaxA), Convert.ToDouble(MaxB)));
                                int cont = 1;

                                //Iteramos las filas
                                foreach (DataRow row in table.Rows)
                                {
                                    //Obtenemos los valores de la tabla resultante
                                    string code = row["Code"].ToString();
                                    string descp = row["Description"].ToString();

                                    //creamos una fila de la tabla para crear el excel
                                    DataRow newRow = dTable.NewRow();

                                    if (cont == 1)
                                    {
                                        //Agregamos los datos a la fila de la tabla
                                        newRow["Componente"] = componente;
                                        newRow["Código"] = code;
                                        newRow["Descripción"] = descp;

                                    }
                                    else if (cont == 2)
                                    {
                                        //Como es el mismo componente, sólo de agrega el código y descripción
                                        newRow["Componente"] = "";
                                        newRow["Código"] = code;
                                        newRow["Descripción"] = descp;
                                    }
                                    //Se agrega la fila a la tabla resultante
                                    dTable.Rows.Add(newRow);

                                    //Se suma 1 al contador
                                    cont++;
                                }

                                //Si no se encontro componente en la base de datos
                                if (table.Rows.Count == 0)
                                {
                                    DataRow newR = dTable.NewRow();
                                    //Agregamos los datos a la fila 
                                    newR["Componente"] = componente;
                                    newR["Código"] = "No se encontró herramental";
                                    newR["Descripción"] = "A= " + MaxA + " B= " + MaxB;
                                    //Se agrega la fila a la tabla resultante
                                    dTable.Rows.Add(newR);
                                }
                                //Se suma uno al auxiliar
                                aux++;
                            }
                            //Añadimos la tabla al DataSet
                            ds.Tables.Add(dTable);
                        }
                    }

                    //Cerramos el excel
                    ExcelWork.Close();

                    //Se manda a llamar a la función para crear el archivo de Excel
                    string e = await ExportToExcel.Export(ds);

                    //Si hubo un error al generar el excel, regresa el error.
                    if (e != null)
                        return e;
                }
                //Si no hay error retorna nulo
                return null;

            }
            catch (IOException er)
            {
                return er.ToString();
            }
            catch (Exception er)
            {
                //Si hay error, retorna el error
                return er.ToString();
            }
        }

        // *NO SE BLOQUEA LA CELDA DE NUMERACIÓN PARA QUE NO AFECTE A LA MACRO Y PUEDAN GENERAR MÁS HOJAS EN EL DOCUMENTO SIN ERROR.

        /// <summary>
        /// Método para crear el formato de las HOE
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fechaFin"></param>
        /// <param name="NombreAbreviado"></param>
        /// <param name="personaCreo"></param>
        /// <param name="personaAutorizo"></param>
        /// <param name="descripcion"></param>
        /// <param name="codigo"></param>
        /// <param name="departamento"></param>
        /// <param name="version"></param>
        /// <param name="id_documento"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static string ExportFormatoHOE(string filename, DateTime fechaFin, string NombreAbreviado, string personaCreo, string personaAutorizo, string codigo, string departamento, int version, int id_documento)
        {
            string a = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] vec = a.Split(',');

            try
            {
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(filename, true);

                string dia = fechaFin.Day.ToString().Length > 1 ? fechaFin.Day.ToString() : "0" + fechaFin.Day.ToString();
                string mes = fechaFin.Month.ToString().Length > 1 ? fechaFin.Month.ToString() : "0" + fechaFin.Month.ToString();

                if (version > 1)
                {
                    int CVF = 1;
                    int Take = 9;

                    ObservableCollection<Model.ControlDocumentos.Version> L = DataManagerControlDocumentos.GetVersionesAnterioresXDocumento(id_documento, Take);

                    // Desbloqueamos el archivo para poder escribir la información
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Unprotect("?W3s.t7/");
                    }

                    foreach (var item in L.Reverse())
                    {
                        DateTime FechaV = item.fecha_version;

                        string diaV = FechaV.Day.ToString().Length > 1 ? FechaV.Day.ToString() : "0" + FechaV.Day.ToString();
                        string mesV = FechaV.Month.ToString().Length > 1 ? FechaV.Month.ToString() : "0" + FechaV.Month.ToString();

                        string UsuarioVersiones = "USUARIO_A" + CVF;
                        string VersionVersiones = "VERSION_" + CVF;
                        string FechaVersiones = "FECHA_A" + CVF;
                        string NivelVersiones = "NIVEL_C" + (CVF);

                        foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                        {
                            sheet.Range[UsuarioVersiones].Value = item.id_usuario;
                            sheet.Range[VersionVersiones].Value = item.no_version;
                            sheet.Range[FechaVersiones].Value = "'" + FechaV.Year + "-" + mesV + "-" + diaV;
                            sheet.Range[NivelVersiones].Value = vec[CVF - 1];
                        }
                        CVF++;
                    }

                    int Aux = 0;
                    int AuxTipoDocumento = 10;

                    if (version > AuxTipoDocumento)
                    {
                        Aux = version - AuxTipoDocumento;
                    }

                    string UsuarioActual = "USUARIO_A" + (version - Aux);
                    string VersionActual = "VERSION_" + (version - Aux);
                    string FechaActual = "FECHA_A" + (version - Aux);
                    string NivelActual = "NIVEL_C" + (version - Aux);

                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Range["FECHA_LIBERACION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range["ELABORO"].Value = personaCreo;
                        sheet.Range["REVISO"].Value = personaAutorizo;
                        sheet.Range["APROBO"].Value = personaAutorizo;
                        sheet.Range["CODIGO"].Value = codigo;
                        sheet.Range["PROCESO"].Value = departamento;

                        sheet.Range[UsuarioActual].Value = NombreAbreviado;
                        sheet.Range[VersionActual].Value = version;
                        sheet.Range[FechaActual].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range[NivelActual].Value = vec[version - 1];
                    }

                    // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                    }

                    ExcelApp.Visible = true;

                    return "Ok";
                }
                else
                {
                    // Desbloqueamos el archivo para poder escribir la información
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Unprotect("?W3s.t7/");
                    }

                    string UsuarioActual = "USUARIO_A" + version;
                    string VersionActual = "VERSION_" + version;
                    string FechaActual = "FECHA_A" + version;
                    string NivelActual = "NIVEL_C" + version;

                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Range["FECHA_LIBERACION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range["ELABORO"].Value = personaCreo;
                        sheet.Range["REVISO"].Value = personaAutorizo;
                        sheet.Range["APROBO"].Value = personaAutorizo;
                        sheet.Range["CODIGO"].Value = codigo;
                        sheet.Range["PROCESO"].Value = departamento;

                        sheet.Range[UsuarioActual].Value = NombreAbreviado;
                        sheet.Range[VersionActual].Value = version;
                        sheet.Range[FechaActual].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range[NivelActual].Value = vec[version - 1];
                    }

                    // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                    }

                    ExcelApp.Visible = true;

                    return "Ok";
                }
            }
            catch (Exception er)
            {
                return er.Message;
            }
        }

        /// <summary>
        /// Método para crear el formato de las JES
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fechaFin"></param>
        /// <param name="NombreAbreviado"></param>
        /// <param name="personaCreo"></param>
        /// <param name="personaAutorizo"></param>
        /// <param name="descripcion"></param>
        /// <param name="codigo"></param>
        /// <param name="departamento"></param>
        /// <param name="version"></param>
        /// <param name="id_documento"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static string ExportFormatoJES(string filename, DateTime fechaFin, string NombreAbreviado, string personaCreo, string personaAutorizo, string descripcion, string codigo, string departamento, int version, int id_documento)
        {
            string a = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] vec = a.Split(',');

            try
            {
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(filename, true);

                string dia = fechaFin.Day.ToString().Length > 1 ? fechaFin.Day.ToString() : "0" + fechaFin.Day.ToString();
                string mes = fechaFin.Month.ToString().Length > 1 ? fechaFin.Month.ToString() : "0" + fechaFin.Month.ToString();

                if (version > 1)
                {
                    int CVF = 1;
                    int Take = 10;

                    ObservableCollection<Model.ControlDocumentos.Version> L = DataManagerControlDocumentos.GetVersionesAnterioresXDocumento(id_documento, Take);

                    // Desbloqueamos el archivo para poder escribir la información
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Unprotect("?W3s.t7/");
                    }

                    foreach (var item in L.Reverse())
                    {
                        DateTime FechaV = item.fecha_version;

                        string diaV = FechaV.Day.ToString().Length > 1 ? FechaV.Day.ToString() : "0" + FechaV.Day.ToString();
                        string mesV = FechaV.Month.ToString().Length > 1 ? FechaV.Month.ToString() : "0" + FechaV.Month.ToString();

                        string UsuarioVersiones = "USUARIO_A" + CVF;
                        string VersionVersiones = "VERSION_" + CVF;
                        string FechaVersiones = "FECHA_A" + CVF;
                        string NivelVersiones = "NIVEL_C" + (CVF);

                        foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                        {
                            sheet.Range[UsuarioVersiones].Value = item.id_usuario;
                            sheet.Range[VersionVersiones].Value = item.no_version;
                            sheet.Range[FechaVersiones].Value = "'" + FechaV.Year + "-" + mesV + "-" + diaV;
                            sheet.Range[NivelVersiones].Value = vec[CVF - 1];
                        }
                        CVF++;
                    }

                    int Aux = 0;
                    int AuxTipoDocumento = 11;

                    if (version > AuxTipoDocumento)
                    {
                        Aux = version - AuxTipoDocumento;
                    }

                    string UsuarioActual = "USUARIO_A" + (version - Aux);
                    string VersionActual = "VERSION_" + (version - Aux);
                    string FechaActual = "FECHA_A" + (version - Aux);
                    string NivelActual = "NIVEL_C" + (version - Aux);

                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Range["FECHA_LIBERACION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range["DESCRIPCION"].Value = descripcion;
                        sheet.Range["ELABORO"].Value = personaCreo;
                        sheet.Range["REVISO"].Value = personaAutorizo;
                        sheet.Range["APROBO"].Value = personaAutorizo;
                        sheet.Range["CODIGO"].Value = codigo;
                        sheet.Range["PROCESO"].Value = departamento;

                        sheet.Range[UsuarioActual].Value = NombreAbreviado;
                        sheet.Range[VersionActual].Value = version;
                        sheet.Range[FechaActual].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range[NivelActual].Value = vec[version - 1];
                    }

                    // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        
                        sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                    }

                    ExcelApp.Visible = true;

                    return "Ok";
                }
                else
                {
                    // Desbloqueamos el archivo para poder escribir la información
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Unprotect("?W3s.t7/");
                    }

                    string UsuarioActual = "USUARIO_A" + version;
                    string VersionActual = "VERSION_" + version;
                    string FechaActual = "FECHA_A" + version;
                    string NivelActual = "NIVEL_C" + version;

                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Range["FECHA_LIBERACION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range["DESCRIPCION"].Value = descripcion;
                        sheet.Range["ELABORO"].Value = personaCreo;
                        sheet.Range["REVISO"].Value = personaAutorizo;
                        sheet.Range["APROBO"].Value = personaAutorizo;
                        sheet.Range["CODIGO"].Value = codigo;
                        sheet.Range["PROCESO"].Value = departamento;

                        sheet.Range[UsuarioActual].Value = NombreAbreviado;
                        sheet.Range[VersionActual].Value = version;
                        sheet.Range[FechaActual].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range[NivelActual].Value = vec[version - 1];
                    }

                    // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                    }

                    ExcelApp.Visible = true;

                    return "Ok";
                }
            }
            catch (Exception er)
            {
                return er.Message;
            }
        }

        /// <summary>
        /// Método para crear el formato de las HII
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fechaFin"></param>
        /// <param name="NombreAbreviado"></param>
        /// <param name="personaCreo"></param>
        /// <param name="personaAutorizo"></param>
        /// <param name="descripcion"></param>
        /// <param name="codigo"></param>
        /// <param name="departamento"></param>
        /// <param name="version"></param>
        /// <param name="id_documento"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static string ExportFormatoHII(string filename, DateTime fechaFin, string personaCreo, string personaAutorizo, string descripcion, string codigo, string departamento, int version, int id_documento)
        {
            try
            {
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(filename, true);

                string dia = fechaFin.Day.ToString().Length > 1 ? fechaFin.Day.ToString() : "0" + fechaFin.Day.ToString();
                string mes = fechaFin.Month.ToString().Length > 1 ? fechaFin.Month.ToString() : "0" + fechaFin.Month.ToString();

                //Si el archivo tiene mas versiones

                string FechaPrimerVersion = DataManagerControlDocumentos.GetFechaPrimeraVersion(id_documento);

                if (id_documento == 0)
                {
                    FechaPrimerVersion = fechaFin.ToString();
                }

                DateTime FV1 = Convert.ToDateTime(FechaPrimerVersion);

                string dia1 = FV1.Day.ToString().Length > 1 ? FV1.Day.ToString() : "0" + FV1.Day.ToString();
                string mes1 = FV1.Month.ToString().Length > 1 ? FV1.Month.ToString() : "0" + FV1.Month.ToString();

                // Desbloqueamos el archivo para poder escribir la información
                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    sheet.Unprotect("?W3s.t7/");
                }

                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    sheet.Range["FECHA_V1"].Value = "'" + FV1.Year + "-" + mes1 + "-" + dia1;
                    if (id_documento == 0)
                    {
                        sheet.Range["FECHA_V1"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                    }
                    sheet.Range["FECHA_ACTUAL"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                    sheet.Range["DESCRIPCION"].Value = descripcion;
                    sheet.Range["NOMBRE_ELABORO"].Value = personaCreo;
                    sheet.Range["NOMBRE_REVISO"].Value = personaAutorizo;
                    sheet.Range["CODIGO"].Value = codigo;
                    sheet.Range["NOMBRE_DEPARTAMENTO"].Value = departamento;
                    sheet.Range["VERSION_ACTUAL"].Value = version;
                }

                // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                }

                ExcelApp.Visible = true;

                return "Ok";

            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// Método para crear el formato de las AVY
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fechaFin"></param>
        /// <param name="NombreAbreviado"></param>
        /// <param name="personaCreo"></param>
        /// <param name="personaAutorizo"></param>
        /// <param name="descripcion"></param>
        /// <param name="codigo"></param>
        /// <param name="departamento"></param>
        /// <param name="version"></param>
        /// <param name="id_documento"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>        
        public static string ExportFormatoAVY(string filename, DateTime fechaFin, string personaCreo, string personaAutorizo, string descripcion, string codigo, string departamento, int version, int id_documento)
        {
            try
            {
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(filename, true);

                string dia = fechaFin.Day.ToString().Length > 1 ? fechaFin.Day.ToString() : "0" + fechaFin.Day.ToString();
                string mes = fechaFin.Month.ToString().Length > 1 ? fechaFin.Month.ToString() : "0" + fechaFin.Month.ToString();

                string FechaPrimerVersion = DataManagerControlDocumentos.GetFechaPrimeraVersion(id_documento);

                if (id_documento == 0)
                {
                    FechaPrimerVersion = fechaFin.ToString();
                }

                DateTime FV1 = Convert.ToDateTime(FechaPrimerVersion);

                string dia1 = FV1.Day.ToString().Length > 1 ? FV1.Day.ToString() : "0" + FV1.Day.ToString();
                string mes1 = FV1.Month.ToString().Length > 1 ? FV1.Month.ToString() : "0" + FV1.Month.ToString();

                // Desbloqueamos el archivo para poder escribir la información
                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    sheet.Unprotect("?W3s.t7/");
                }

                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    sheet.Range["FECHA_ELABORACION"].Value = "'" + FV1.Year + "-" + mes1 + "-" + dia1;
                    sheet.Range["CODIGO"].Value = codigo;
                    sheet.Range["ELABORO"].Value = personaCreo;
                    sheet.Range["APROBO"].Value = personaAutorizo;
                    sheet.Range["Version"].Value = version;
                    sheet.Range["NOMBRE_DEPARTAMENTO"].Value = departamento;
                    sheet.Range["FECHA_REVISION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                }

                // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                }

                ExcelApp.Visible = true;

                return "Ok";
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Método para importar un archivo excel, la información de ClosingSleeveBk.
        /// </summary>
        /// <returns></returns>
        public async static Task<string> ImportClosingSleeve()
        {
            try
            {
                //Creamos un objeto de tipo OpenDialog
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                //Filtar los dpocumentos por extensión 
                dlg.Filter = "Excel Workbook|*.xlsx";

                // Mostrar el explorador de archivos
                Nullable<bool> result = dlg.ShowDialog();

                if (result == true)
                {
                    //Se obtiene el nombre del documento
                    string filename = dlg.FileName;

                    //Creamos una instancia de la aplicación.
                    Excel.Application ExcelApp = new Excel.Application();

                    //Abre el documento
                    Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(filename, true);

                    //Creamos un objeto de tipo dataSet, donde se guardaran las tablas para crear el archivo Excel
                    DataSet ds = new DataSet();

                    //Declaramos la tabla donde se guarda la información obtenida de la base de datos.
                    DataTable table = new DataTable();

                    //Iteramos las hojas del archivo leído
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        //Tabla donde se guardará la información del nuevo archivo de excel
                        DataTable dTable = new DataTable();

                        //Obtenemos el rango de la hoja que estamos leyendo
                        Excel.Range range = sheet.UsedRange;

                        //Obtiene el número de filas de la hoja
                        int rowCount = range.Rows.Count;
                        int columCount = range.Columns.Count;
                        int rangeCount = range.Count;

                        //Verifica que la hoja no esté vacía.
                        if (rowCount > 1)
                        {
                            //Se empieza a leer el documento en la fila 2 (preguntar a raúl, cambiar dependiendo de el excel)
                            int aux = 2;

                            //Variables de diámetro y Gap finish mill.
                            string componente, diamFM, gapFM;

                            //Asigamos el nombre de la hoja a la tabla
                            dTable.TableName = sheet.Name;

                            //Creamos las columnas del nuevo archivo
                            dTable.Columns.Add("Componente");
                            dTable.Columns.Add("Código");
                            dTable.Columns.Add("Descripción");

                            //Repite mientras que el auxiliar sea menor o igual al número de columnas
                            while (aux <= rowCount)
                            {
                                //Extraemos los datos del archivo de excel, los guardamos el variables locales
                                componente = range.Cells[aux, 1].Value2.ToString();
                                diamFM = range.Cells[aux, 2].Value2.ToString();
                                gapFM = range.Cells[aux, 3].Value2.ToString();

                                //Obtenemos los datos de los dos primeros registros con el max que se obtuvo en el excel
                                table = DataManager.SelectBestClosingBK(DataManager.GetClosingSleeve(Convert.ToDouble(diamFM), Convert.ToDouble(gapFM)));

                                //Iteramos las filas obtenidas de la base de datos.
                                foreach (DataRow row in table.Rows)
                                {
                                    //Obtenemos los valores de la tabla resultante.
                                    string code = row["Code"].ToString();
                                    string descp = row["Description"].ToString();

                                    //Creamos una fila de la tabla para crear el excel.
                                    DataRow newRow = dTable.NewRow();

                                    //Agregamos los datos a la fila de la tabla.
                                    newRow["Componente"] = componente;
                                    newRow["Código"] = code;
                                    newRow["Descripción"] = descp;

                                    //Se agrega la fila a la tabla resultante
                                    dTable.Rows.Add(newRow);
                                }

                                //Si no se encontro componente en la base de datos
                                if (table.Rows.Count == 0)
                                {
                                    DataRow newR = dTable.NewRow();
                                    //Agregamos los datos a la fila 
                                    newR["Componente"] = componente;
                                    newR["Código"] = "No se encontró herramental";
                                    newR["Descripción"] = "Diámetro Finish Mill= " + diamFM + " Gap Finish Mill= " + gapFM;
                                    //Se agrega la fila a la tabla resultante
                                    dTable.Rows.Add(newR);
                                }
                                //Se suma uno al auxiliar
                                aux++;
                            }
                            //Añadimos la tabla al DataSet
                            ds.Tables.Add(dTable);
                        }
                    }

                    //Cerramos el excel
                    ExcelWork.Close();

                    //Se manda a llamar a la función para crear el archivo de Excel
                    string e = await ExportToExcel.Export(ds);

                    //Si hubo un error al generar el excel, regresa el error.
                    if (e != null)
                        return e;
                }
                //Si no hay error retorna nulo
                return null;
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }

        /// <summary>
        /// Método para corregir el formato de las HOE
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fechaFin"></param>
        /// <param name="NombreAbreviado"></param>
        /// <param name="personaCreo"></param>
        /// <param name="personaAutorizo"></param>
        /// <param name="descripcion"></param>
        /// <param name="codigo"></param>
        /// <param name="departamento"></param>
        /// <param name="version"></param>
        /// <param name="id_documento"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static string ExportHOECorrecto(string nfilename, DateTime fechaFin, string NombreAbreviado, string personaCreo, string personaAutorizo, string codigo, string departamento, int version, int id_documento)
        {
            string a = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,,WX,Y,Z";
            string[] vec = a.Split(',');

            try
            {
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(nfilename, true);

                string dia = fechaFin.Day.ToString().Length > 1 ? fechaFin.Day.ToString() : "0" + fechaFin.Day.ToString();
                string mes = fechaFin.Month.ToString().Length > 1 ? fechaFin.Month.ToString() : "0" + fechaFin.Month.ToString();
                int Aux2 = 1;

                if (version > 1)
                {
                    int CVF = 1;
                    int Take = 9;

                    ObservableCollection<Model.ControlDocumentos.Version> L = DataManagerControlDocumentos.GetVersionesAnterioresXDocumento(id_documento, Take);

                    // Desbloqueamos el archivo para poder escribir la información
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Unprotect("?W3s.t7/");
                    }

                    foreach (var item in L.Reverse())
                    {
                        DateTime FechaV = item.fecha_version;

                        string diaV = FechaV.Day.ToString().Length > 1 ? FechaV.Day.ToString() : "0" + FechaV.Day.ToString();
                        string mesV = FechaV.Month.ToString().Length > 1 ? FechaV.Month.ToString() : "0" + FechaV.Month.ToString();

                        string UsuarioVersiones = "USUARIO_A" + CVF;
                        string VersionesVersiones = "VERSION_" + CVF;
                        string FechaVersiones = "FECHA_A" + CVF;
                        string NivelVersiones = "NIVEL_C" + CVF;

                        foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                        {
                            sheet.Range[UsuarioVersiones].Value = item.id_usuario;
                            sheet.Range[VersionesVersiones].Value = item.no_version;
                            sheet.Range[FechaVersiones].Value = "'" + FechaV.Year + "-" + mesV + "-" + diaV;
                            sheet.Range[NivelVersiones].Value = vec[CVF - 1];
                        }
                        CVF++;
                    }

                    int Aux = 0;
                    int AuxTipoDocumento = 10;

                    if (version > AuxTipoDocumento)
                    {
                        Aux = version - AuxTipoDocumento;
                    }

                    string UsuarioActual = "USUARIO_A" + (version - Aux);
                    string VersionActual = "VERSION_" + (version - Aux);
                    string FechaActual = "FECHA_A" + (version - Aux);
                    string NivelActual = "NIVEL_C" + (version - Aux);

                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        foreach (Excel.Worksheet sheets in ExcelWork.Sheets)
                        {
                            sheet.Range["X7"].Value = "Hoja " + Aux2 + " de " + ExcelWork.Sheets.Count;
                        }
                        Aux2++;

                        sheet.Range["FECHA_LIBERACION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range["ELABORO"].Value = personaCreo;
                        sheet.Range["REVISO"].Value = personaAutorizo;
                        sheet.Range["APROBO"].Value = personaAutorizo;
                        sheet.Range["CODIGO"].Value = codigo;
                        sheet.Range["PROCESO"].Value = departamento;

                        sheet.Range[UsuarioActual].Value = NombreAbreviado;
                        sheet.Range[VersionActual].Value = version;
                        sheet.Range[FechaActual].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range[NivelActual].Value = vec[version - 1];
                    }

                    // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                    }

                    ExcelWork.Save();
                    ExcelApp.Visible = false;
                    ExcelWork.Close();

                    return "Ok";
                }
                else
                {
                    // Desbloqueamos el archivo para poder escribir la información
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Unprotect("?W3s.t7/");
                    }

                    string UsuarioActual = "USUARIO_A" + version;
                    string VersionActual = "VERSION_" + version;
                    string FechaActual = "FECHA_A" + version;
                    string NivelActual = "NIVEL_C" + version;

                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        foreach (Excel.Worksheet sheets in ExcelWork.Sheets)
                        {
                            sheet.Range["X7"].Value = "Hoja " + Aux2 + " de " + ExcelWork.Sheets.Count;
                        }
                        Aux2++;

                        sheet.Range["FECHA_LIBERACION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range["ELABORO"].Value = personaCreo;
                        sheet.Range["REVISO"].Value = personaAutorizo;
                        sheet.Range["APROBO"].Value = personaAutorizo;
                        sheet.Range["CODIGO"].Value = codigo;
                        sheet.Range["PROCESO"].Value = departamento;

                        sheet.Range[UsuarioActual].Value = NombreAbreviado;
                        sheet.Range[VersionActual].Value = version;
                        sheet.Range[FechaActual].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range[NivelActual].Value = vec[version - 1];
                    }

                    // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                    }

                    ExcelWork.Save();
                    ExcelApp.Visible = false;
                    ExcelWork.Close();

                    return "Ok";
                }
            }
            catch (Exception er)
            {
                return er.Message;
            }
        }

        /// <summary>
        /// Método para corregir el formato de las JES
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fechaFin"></param>
        /// <param name="NombreAbreviado"></param>
        /// <param name="personaCreo"></param>
        /// <param name="personaAutorizo"></param>
        /// <param name="descripcion"></param>
        /// <param name="codigo"></param>
        /// <param name="departamento"></param>
        /// <param name="version"></param>
        /// <param name="id_documento"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static string ExportJESCorrecto(string nfilename, DateTime fechaFin, string NombreAbreviado, string personaCreo, string personaAutorizo, string descripcion, string codigo, string departamento, int version, int id_documento)
        {
            string a = "A,B,C,D,E,F,G,H,I,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z";
            string[] vec = a.Split(',');

            try
            {
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(nfilename, true);

                string dia = fechaFin.Day.ToString().Length > 1 ? fechaFin.Day.ToString() : "0" + fechaFin.Day.ToString();
                string mes = fechaFin.Month.ToString().Length > 1 ? fechaFin.Month.ToString() : "0" + fechaFin.Month.ToString();
                int Aux2 = 1;

                if (version > 1)
                {
                    int CVF = 1;
                    int Take = 10;

                    ObservableCollection<Model.ControlDocumentos.Version> L = DataManagerControlDocumentos.GetVersionesAnterioresXDocumento(id_documento, Take);

                    // Desbloqueamos el archivo para poder escribir la información
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Unprotect("?W3s.t7/");
                    }

                    foreach (var item in L.Reverse())
                    {
                        DateTime FechaV = item.fecha_version;

                        string diaV = FechaV.Day.ToString().Length > 1 ? FechaV.Day.ToString() : "0" + FechaV.Day.ToString();
                        string mesV = FechaV.Month.ToString().Length > 1 ? FechaV.Month.ToString() : "0" + FechaV.Month.ToString();

                        string UsuarioVersiones = "USUARIO_A" + CVF;
                        string VersionVersiones = "VERSION_" + CVF;
                        string FechaVersiones = "FECHA_A" + CVF;
                        string NivelVersiones = "NIVEL_C" + (CVF);

                        foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                        {
                            sheet.Range[UsuarioVersiones].Value = item.id_usuario;
                            sheet.Range[VersionVersiones].Value = item.no_version;
                            sheet.Range[FechaVersiones].Value = "'" + FechaV.Year + "-" + mesV + "-" + diaV;
                            sheet.Range[NivelVersiones].Value = vec[CVF - 1];
                        }
                        CVF++;
                    }

                    int Aux = 0;
                    int AuxTipoDocumento = 11;

                    if (version > AuxTipoDocumento)
                    {
                        Aux = version - AuxTipoDocumento;
                    }

                    string UsuarioActual = "USUARIO_A" + (version - Aux);
                    string VersionActual = "VERSION_" + (version - Aux);
                    string FechaActual = "FECHA_A" + (version - Aux);
                    string NivelActual = "NIVEL_C" + (version - Aux);

                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        foreach (Excel.Worksheet sheets in ExcelWork.Sheets)
                        {
                            sheet.Range["X7"].Value = "Hoja " + Aux2 + " de " + ExcelWork.Sheets.Count;
                        }
                        Aux2++;

                        sheet.Range["FECHA_LIBERACION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range["DESCRIPCION"].Value = descripcion;
                        sheet.Range["ELABORO"].Value = personaCreo;
                        sheet.Range["REVISO"].Value = personaAutorizo;
                        sheet.Range["APROBO"].Value = personaAutorizo;
                        sheet.Range["CODIGO"].Value = codigo;
                        sheet.Range["PROCESO"].Value = departamento;

                        sheet.Range[UsuarioActual].Value = NombreAbreviado;
                        sheet.Range[VersionActual].Value = version;
                        sheet.Range[FechaActual].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range[NivelActual].Value = vec[version - 1];
                    }

                    // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                    }

                    ExcelWork.Save();
                    ExcelApp.Visible = false;
                    ExcelWork.Close();

                    return "Ok";
                }
                else
                {
                    // Desbloqueamos el archivo para poder escribir la información
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Unprotect("?W3s.t7/");
                    }

                    string UsuarioActual = "USUARIO_A" + version;
                    string VersionActual = "VERSION_" + version;
                    string FechaActual = "FECHA_A" + version;
                    string NivelActual = "NIVEL_C" + version;

                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        foreach (Excel.Worksheet sheets in ExcelWork.Sheets)
                        {
                            sheet.Range["X7"].Value = "Hoja " + Aux2 + " de " + ExcelWork.Sheets.Count;
                        }
                        Aux2++;

                        sheet.Range["FECHA_LIBERACION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range["DESCRIPCION"].Value = descripcion;
                        sheet.Range["ELABORO"].Value = personaCreo;
                        sheet.Range["REVISO"].Value = personaAutorizo;
                        sheet.Range["APROBO"].Value = personaAutorizo;
                        sheet.Range["CODIGO"].Value = codigo;
                        sheet.Range["PROCESO"].Value = departamento;

                        sheet.Range[UsuarioActual].Value = NombreAbreviado;
                        sheet.Range[VersionActual].Value = version;
                        sheet.Range[FechaActual].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range[NivelActual].Value = vec[version - 1];
                    }

                    // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                    }

                    ExcelWork.Save();
                    ExcelApp.Visible = false;
                    ExcelWork.Close();

                    return "Ok";
                }
            }
            catch (Exception er)
            {
                return er.Message;
            }
        }

        /// <summary>
        /// Método para corregir el formato de las HII
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fechaFin"></param>
        /// <param name="NombreAbreviado"></param>
        /// <param name="personaCreo"></param>
        /// <param name="personaAutorizo"></param>
        /// <param name="descripcion"></param>
        /// <param name="codigo"></param>
        /// <param name="departamento"></param>
        /// <param name="version"></param>
        /// <param name="id_documento"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static string ExportHIICorrecto(string nfilename, DateTime fechaFin, string personaCreo, string personaAutorizo, string descripcion, string codigo, string departamento, int version, int id_documento)
        {
            try
            {
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(nfilename, true);

                string dia = fechaFin.Day.ToString().Length > 1 ? fechaFin.Day.ToString() : "0" + fechaFin.Day.ToString();
                string mes = fechaFin.Month.ToString().Length > 1 ? fechaFin.Month.ToString() : "0" + fechaFin.Month.ToString();

                string FechaPrimerVersion = DataManagerControlDocumentos.GetFechaPrimeraVersion(id_documento);

                if (id_documento == 0)
                {
                    FechaPrimerVersion = fechaFin.ToString();
                }

                DateTime FV1 = Convert.ToDateTime(FechaPrimerVersion);

                string dia1 = FV1.Day.ToString().Length > 1 ? FV1.Day.ToString() : "0" + FV1.Day.ToString();
                string mes1 = FV1.Month.ToString().Length > 1 ? FV1.Month.ToString() : "0" + FV1.Month.ToString();
                int Aux = 1;

                // Desbloqueamos el archivo para poder escribir la información
                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    sheet.Unprotect("?W3s.t7/");
                }

                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    foreach (Excel.Worksheet sheets in ExcelWork.Sheets)
                    {
                        sheet.Range["HOJAS"].Value = "Hoja " + Aux + " de " + ExcelWork.Sheets.Count;
                    }
                    Aux++;

                    sheet.Range["FECHA_V1"].Value = "'" + FV1.Year + "-" + mes1 + "-" + dia1; ;
                    if (id_documento == 0)
                    {
                        sheet.Range["FECHA_V1"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                    }

                    sheet.Range["FECHA_ACTUAL"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                    sheet.Range["DESCRIPCION"].Value = descripcion;
                    sheet.Range["NOMBRE_ELABORO"].Value = personaCreo;
                    sheet.Range["NOMBRE_REVISO"].Value = personaAutorizo;
                    sheet.Range["CODIGO"].Value = codigo;
                    sheet.Range["NOMBRE_DEPARTAMENTO"].Value = departamento;
                    sheet.Range["VERSION_ACTUAL"].Value = version;
                }

                // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                }

                ExcelWork.Save();
                ExcelApp.Visible = false;
                ExcelWork.Close();

                return "Ok";

            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// Método para corregir el formato de las AVY
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="fechaFin"></param>
        /// <param name="NombreAbreviado"></param>
        /// <param name="personaCreo"></param>
        /// <param name="personaAutorizo"></param>
        /// <param name="descripcion"></param>
        /// <param name="codigo"></param>
        /// <param name="departamento"></param>
        /// <param name="version"></param>
        /// <param name="id_documento"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>  
        public static string ExportAVYCorrecto(string nfilename, DateTime fechaFin, string personaCreo, string personaAutorizo, string descripcion, string codigo, string departamento, int version, int id_documento)
        {
            try
            {
                Excel.Application ExcelApp = new Excel.Application();
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(nfilename, true);

                string dia = fechaFin.Day.ToString().Length > 1 ? fechaFin.Day.ToString() : "0" + fechaFin.Day.ToString();
                string mes = fechaFin.Month.ToString().Length > 1 ? fechaFin.Month.ToString() : "0" + fechaFin.Month.ToString();

                string FechaPrimerVersion = DataManagerControlDocumentos.GetFechaPrimeraVersion(id_documento);

                if (id_documento == 0)
                {
                    FechaPrimerVersion = fechaFin.ToString();
                }

                DateTime FV1 = Convert.ToDateTime(FechaPrimerVersion);

                string dia1 = FV1.Day.ToString().Length > 1 ? FV1.Day.ToString() : "0" + FV1.Day.ToString();
                string mes1 = FV1.Month.ToString().Length > 1 ? FV1.Month.ToString() : "0" + FV1.Month.ToString();
                int Aux = 1;

                // Desbloqueamos el archivo para poder escribir la información
                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    sheet.Unprotect("?W3s.t7/");
                }

                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    foreach (Excel.Worksheet sheets in ExcelWork.Sheets)
                    {
                        sheet.Range["NUMERACION"].Value = "Hoja:(" + Aux + "/" + ExcelWork.Sheets.Count + ")";
                    }
                    Aux++;

                    sheet.Range["FECHA_ELABORACION"].Value = "'" + FV1.Year + "-" + mes1 + "-" + dia1;
                    sheet.Range["CODIGO"].Value = codigo;
                    sheet.Range["ELABORO"].Value = personaCreo;
                    sheet.Range["APROBO"].Value = personaAutorizo;
                    sheet.Range["Version"].Value = version;
                    sheet.Range["NOMBRE_DEPARTAMENTO"].Value = departamento;
                    sheet.Range["FECHA_REVISION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                }

                // Bloqueamos las celdas para que no puedan ser modificadas por el usuario
                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    sheet.Protect("?W3s.t7/", false, true, true, true, true, true, true, true, true, true, true, true, true, true, true);
                }

                ExcelWork.Save();
                ExcelApp.Visible = false;
                ExcelWork.Close();

                return "Ok";
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
