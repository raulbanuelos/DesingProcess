﻿using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Data;
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
        public  static string ImportCollarBK()
        {
            try
            {
                //Creamos un objeto de tipo OpenDialog
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();

                //Filtar los dpocumentos por extensión 
                dlg.Filter = "Excel (97-2003)|*.xlsx";

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

                        //Obtiene el número de filas de la hoja
                        int rowCount = range.Rows.Count;

               
                        //Se empieza a leer el documento en la fila 3
                        int aux = 3;
                        string componente, MaxA, MaxB;

                        //Asigamos el nombre de la hoja a la tabla
                        dTable.TableName = sheet.Name;

                        //Creamos las columnas del nuevo archivo
                        dTable.Columns.Add("Componente");
                        dTable.Columns.Add("Código");
                        dTable.Columns.Add("Descripción");

                        //Repite mientras que el auxiliar sea menor al número de columnas
                        while (aux <= rowCount) {

                            //Extraemos los datos del archivo de excel, los guardamos el variables locales
                            componente = range.Cells[aux, 1].Value2.ToString();
                            MaxA = range.Cells[aux, 2].Value2.ToString();
                            MaxB = range.Cells[aux, 3].Value2.ToString();
                            
                            //Obtenemos los datos de los dos primeros registros con el max que se obtuvo en el excel
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

                    //Cerramos el excel
                    ExcelWork.Close();
                    
                    //Se manda a llamar a la función para crear el archivo de Excel
                    string e = ExportToExcel.Export(ds);

                }
                //Si no hay error retorna nulo
                    return null;
               
            }
            catch (Exception er)
            {
                //Si hay error, retorna el error
                return er.ToString();
            }
        }

        public static void ImportAV()
        {
            try
            {
                string path = "C:\\Users\\Ing.practicante\\Documents\\AYUDAVISUAL.xls";
                //Creamos una instancia de la aplicación.
                Excel.Application ExcelApp = new Excel.Application();

                //Abre el documento
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(path, true);

                foreach (Excel.Worksheet sheet in ExcelWork.Sheets)
                {
                    //Obtenemos el rango de la hoja que estamos leyendo
                    Excel.Range range = sheet.UsedRange;

                    //Obtiene el número de filas de la hoja
                    int rowCount = range.Rows.Count;
                    int columCount = range.Columns.Count;

                    int aux = 2;

                    string url, proceso;
                    Double fecha;

                    while (aux <= rowCount)
                    {
                        Documento objDocumento = new Documento();
                        Archivo objArchivo = new Archivo();
                        Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
                        //Alta del documento
                        proceso = range.Cells[aux, 2].Value2.ToString();

                        //Obtenemos el id del proceso
                        objDocumento.id_dep = DataManagerControlDocumentos.GetID_Dep(proceso);
                        objDocumento.nombre = range.Cells[aux, 3].Value2.ToString();
                        objDocumento.id_tipo_documento =1004;
                        objDocumento.usuario = "sistema";
                        objDocumento.descripcion = range.Cells[aux, 5].Value2.ToString();
                        fecha = range.Cells[aux, 6].Value2;
                        DateTime f= DateTime.FromOADate(fecha);
                        // objDocumento.fecha_emision = DateTime.Parse(fecha);
                        //DateTime.ParseExact("24/01/2013", "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        objDocumento.id_estatus = 5;

                        //Alta de Versión

                        //objVersion.id_documento =?;
                        objVersion.id_estatus_version = 1;
                        objVersion.id_usuario_autorizo="sistema";
                        objVersion.id_usuario_autorizo = "sistema";
                        objVersion.no_version= range.Cells[aux, 10].Value2.ToString();
                        objVersion.fecha_version = DateTime.Now;
                        objVersion.no_copias = Convert.ToInt32(range.Cells[aux, 9].Value2.ToString());

                        //Alta del Archivo
                        url = range.Cells[aux, 4].Cells.Hyperlinks[1].Address;
                        url = url.Replace("..\\..\\M0051722\\AppData\\Roaming\\", "");
                        url= url.Replace("..\\..\\", "");
                        url = string.Concat("Z:\\", url);
                        //string u="\\agufileserv2\INGENIERIA\RESPRUTAS\MANUELITO\manuelito\MANUELITO\CIT\AYUDAS VISUALES (FISICAS)";

                        objArchivo.ext= System.IO.Path.GetExtension(url);

                        objArchivo.nombre = System.IO.Path.GetFileNameWithoutExtension(url);
                       
                        objArchivo.archivo= File.ReadAllBytes(url);

                        aux++;
                    }

                }

                ExcelWork.Close();
                }
            catch(Exception er)
            {
                

            }
        }
    }
}
