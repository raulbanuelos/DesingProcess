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

                        int rowCount = range.Rows.Count;
                        int colCount = 3; //
                        int aux = 3;
                        string componente, MaxA, MaxB;

                        dTable.TableName = sheet.Name;

                        dTable.Columns.Add("Componente");
                        dTable.Columns.Add("Código");
                        dTable.Columns.Add("Descripción");

                        while (aux <= rowCount) {

                            componente = range.Cells[aux, 1].Value2.ToString();
                            MaxA = range.Cells[aux, 2].Value2.ToString();
                            MaxB = range.Cells[aux, 3].Value2.ToString();

                            table = DataManager.SelectBestCollar(DataManager.GetCollarBK(Convert.ToDouble(MaxA), Convert.ToDouble(MaxB)));
                            int cont = 1;

                            foreach (DataRow row in table.Rows)
                            {
                                string code = row["Code"].ToString();
                                string descp = row["Description"].ToString();

                                DataRow newRow = dTable.NewRow();

                                if (cont == 1)
                                {
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

                            /* DataRow newR = dTable.NewRow();
                             newR["Componente"] = "";
                             newR["Código"] = "";
                             newR["Descripción"] = "";

                             dTable.Rows.Add(newR);*/
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

                        ds.Tables.Add(dTable);
                        
                    }
                    string e = ExportToExcel.Export(ds);

                    if (e != null)
                    {
                        
                    }
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
