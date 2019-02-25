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

    public  static class ImportExcel
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

                if (result== true) {
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
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets) {

                        //Tabla donde se guardará la información del nuevo archivo de excel
                        DataTable dTable = new DataTable();

                        //Obtenemos el rango de la hoja que estamos leyendo
                        Excel.Range range = sheet.UsedRange;

                        //Verifica si la hoja no está vacía. 
                        int columUsed = range.Columns.Count;

                        //Obtiene el número de filas de la hoja
                        int rowCount = range.Rows.Count;

                        //Verifica que la hoja no esté vacía.
                        if (rowCount > 1) {
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
                            while (aux <= rowCount) {

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

                                    } else if (cont == 2)
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
                  string e =await ExportToExcel.Export(ds);

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

        public static string ExportTipoFormato(string filename, DateTime fechaFin,string NombreAbreviado ,string personaCreo, string personaAutorizo, string descripcion, string codigo, string departamento, int version, int id_documento, int id_tipo)
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
                    if (id_tipo == 1015)
                    {
                        Take = 10;
                    }
                    ObservableCollection<Model.ControlDocumentos.Version> L = DataManagerControlDocumentos.GetVersionesAnterioresXDocumento(id_documento, Take);


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
                            sheet.Range[NivelVersiones].Value = vec[CVF-1];
                        }
                        CVF++;
                    }
                    int Aux = 0;
                    int AuxCTipoDocumento = 11;
                    if (id_tipo == 2)
                    {
                        AuxCTipoDocumento = 10;
                    }
                    if (version > AuxCTipoDocumento)
                    {
                        Aux = version - AuxCTipoDocumento;
                    }

                    string UsuarioActual = "USUARIO_A" + (version - Aux);
                    string VersionActual = "VERSION_" + (version - Aux);
                    string FechaActual = "FECHA_A" + (version - Aux);
                    string NivelActual = "NIVEL_C" + (version - Aux);

                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        
                        sheet.Range["FECHA_LIBERACION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        if (id_tipo == 1015)
                        {
                            sheet.Range["DESCRIPCION"].Value = descripcion;
                        }
                        sheet.Range["ELABORO"].Value = personaCreo;
                        sheet.Range["REVISO"].Value = personaAutorizo;
                        sheet.Range["APROBO"].Value = personaAutorizo;
                        sheet.Range["CODIGO"].Value = codigo;
                        sheet.Range["PROCESO"].Value = departamento;

                        sheet.Range[UsuarioActual].Value = NombreAbreviado;
                        sheet.Range[VersionActual].Value = version;
                        sheet.Range[FechaActual].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range[NivelActual].Value = vec[version-1];

                    }

                    ExcelApp.Visible = true;

                    return "Ok";
                }
                else
                {
                    string UsuarioActual = "USUARIO_A" + version;
                    string VersionActual = "VERSION_" + version;
                    string FechaActual = "FECHA_A" + version;
                    string NivelActual = "NIVEL_C" + version;

                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                    {
                        sheet.Range["FECHA_LIBERACION"].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        if (id_tipo == 1015)
                        {
                            sheet.Range["DESCRIPCION"].Value = descripcion;
                        }

                        sheet.Range["ELABORO"].Value = personaCreo;
                        sheet.Range["REVISO"].Value = personaAutorizo;
                        sheet.Range["APROBO"].Value = personaAutorizo;
                        sheet.Range["CODIGO"].Value = codigo;
                        sheet.Range["PROCESO"].Value = departamento;

                        sheet.Range[UsuarioActual].Value = NombreAbreviado;
                        sheet.Range[VersionActual].Value = version;
                        sheet.Range[FechaActual].Value = "'" + fechaFin.Year + "-" + mes + "-" + dia;
                        sheet.Range[NivelActual].Value = vec[version-1];

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
    }
}
