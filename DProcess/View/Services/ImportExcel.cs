using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace View.Services
{
   public static class ImportExcel
    {

        public static void Import()
        {
            try
            {
                //Creamos una instancia de la aplicación.
                Excel.Application ExcelApp = new Excel.Application();

                string filepath = "Z:/RrrrUUUUUULLL/Analisis calculo de collarines/Revisión Mangas y Collarines 15 06 2017.xlsx";

                //Abre el documento
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Open(filepath, true);

                //se abre la hoja 
                Excel.Worksheet sheet = ExcelWork.Sheets["BT10-RBT10"];
               
                //El rango 
                Excel.Range range = sheet.UsedRange;

                int rowCount = range.Rows.Count;
                int colCount = 3; //
                int aux = 1;
                string codigo, MaxA, maxB;

                while (aux <= rowCount) {

                         codigo=range.Cells[aux, 1].Value2.ToString();
                         MaxA = range.Cells[aux, 2].Value2.ToString();
                         maxB = range.Cells[aux, 3].Value2.ToString();

                    aux++;
                }

            }
            catch (Exception er)
            {

            }
        }
    }
}
