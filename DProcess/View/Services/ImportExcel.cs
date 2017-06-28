using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace View.Services
{
   public static class ImportExcel
    {
       

        public  static string ImportCollarBK()
        {
            try
            {
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
                    DataSet ds = new DataSet();
                    DataTable table = new DataTable();
                   
                    foreach (Excel.Worksheet sheet in ExcelWork.Sheets) {

                        DataTable dTable = new DataTable();
                        //se abre la hoja 
                        //Excel.Worksheet sheet = ExcelWork.Sheets["BT10-RBT10"];

                        //El rango 
                        Excel.Range range = sheet.UsedRange;

                        //Obtiene el número de filas
                        int rowCount = range.Rows.Count;

                        //Número de columnas siempre es fijo para estos documentos
                        int colCount = 3; //
                        //Se empieza a leer el documento en la fila 3
                        int aux = 3;
                        string componente, MaxA, MaxB;

                        //Asigamos el nombre de la hoja a la tabla
                        dTable.TableName = sheet.Name;

                        //Creamos las columnas
                        dTable.Columns.Add("Componente");
                        dTable.Columns.Add("Código");
                        dTable.Columns.Add("Descripción");

                        //Repite mientras que el auxiliar sea menor al número de columnas
                        while (aux <= rowCount) {

                            //Extraemos los datos del excel
                            componente = range.Cells[aux, 1].Value2.ToString();
                            MaxA = range.Cells[aux, 2].Value2.ToString();
                            MaxB = range.Cells[aux, 3].Value2.ToString();

                            //Obtenemos los datos de los dos primeros registros
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
                                    newRow["Componente"] = "";
                                    newRow["Código"] = code;
                                    newRow["Descripción"] = descp;
                                }
                                dTable.Rows.Add(newRow);

                                cont++;
                            }

                            //Si no se encontro componente
                            if (table.Rows.Count == 0)
                            {
                                DataRow newR = dTable.NewRow();
                                newR["Componente"] = componente;
                                newR["Código"] = "No se encontró herramental";
                                newR["Descripción"] = "A= " + MaxA + " B= " + MaxB;
                                dTable.Rows.Add(newR);
                            }
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
                    return null;
               
            }
            catch (Exception er)
            {
                return er.ToString();
            }
        }
    }
}
