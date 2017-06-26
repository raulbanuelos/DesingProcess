using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace View.Services
{
    public static class ExportToExcel
    {
        /// <summary>
        /// Método para exportar un archivo excel desde un dataset
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Export(DataSet data)
        {
            try
            {
                if (data != null)
                {
                    //Creamos una instancia de la aplicación.
                    Excel.Application ExcelApp = new Excel.Application();

                    //Crea un nuevo documento.
                    Excel.Workbook ExcelWork = ExcelApp.Workbooks.Add();

                    //Iteramos el dataset.
                    foreach (DataTable table in data.Tables)
                    {
                        //Añadimos una nueva hoja de cálculo 
                        Excel.Worksheet ExcelWoorkSheet = ExcelWork.Sheets.Add();
                        //Se asigna el nombre de la tabla a la hoja de cálculo.
                        ExcelWoorkSheet.Name = table.TableName;

                        //Recorre número de columnas en la tabla.
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            //A cada celda se asigna el nombre de la columna de la tabla.
                            ExcelWoorkSheet.Cells[1, i + 1] = table.Columns[i].ColumnName;
                            //Establece el color de la columna.
                            ExcelWoorkSheet.Cells[1, i + 1].Interior.Color = Excel.XlRgbColor.rgbMidnightBlue;
                            //Establece el color de la fuente.
                            ExcelWoorkSheet.Cells[1, i + 1].Font.Color = Excel.XlRgbColor.rgbWhiteSmoke;
                            //Establece el tamaño del texto
                            ExcelWoorkSheet.Rows[1].Cells[i + 1].Style.Font.Size = 12;
                            //El texto lo pone en negrita
                            ExcelWoorkSheet.Cells[1, i + 1].Font.Bold = true;
                            //Alinea el texto al centro
                            ExcelWoorkSheet.Cells[1, i + 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                            //Dibuja los Bordes
                            Excel.Borders border = ExcelWoorkSheet.Cells[1, i + 1].Borders;
                            border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                            border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                            border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                            border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                            //Establece automáticamente el tamaño de la columna.
                          //  ExcelWoorkSheet.Columns.AutoFit();
                           ExcelWoorkSheet.Cells[1, i + 1].EntireColumn.ColumnWidth = 25;
                        }
                        //Reccorre el número de filas de la tabla.
                        for (int j = 0; j < table.Rows.Count; j++)
                        {
                            //Recorre el número de columnas de la tabla.
                            for (int k = 0; k < table.Columns.Count; k++)
                            {
                                //Llenamos la hoja de calculo con la información de la tabla.
                                ExcelWoorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                                //Alinea el texto en el centro.
                                ExcelWoorkSheet.Cells[j + 2, k + 1].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                                //Dibuja los Bordes
                                Excel.Borders border = ExcelWoorkSheet.Cells[j + 2, k + 1].Borders;
                                border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
          
                            }
                        }
                    }
                    //Muestra el documento.
                    ExcelApp.Visible = true;
                }

                //Si no hay error retorna nulo
                return null;
            }//Si hay algún error.
            catch (Exception ex)
            {
                //Retorna el error.
                return ex.Message;
            }
        }
    }
}
