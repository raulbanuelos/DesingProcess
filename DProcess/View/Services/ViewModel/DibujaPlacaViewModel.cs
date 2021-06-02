using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace View.Services.ViewModel
{
    public class DibujaPlacaViewModel
    {
        public string CodigoPlaca { get; set; }
        public DibujaPlacaViewModel(string _codigoPlaca, int _noImpreciones)
        {
            CodigoPlaca = _codigoPlaca;
        }

        public void drawPattern()
        {
            //Excel.ApplicationClass oExcel;
            //Excel.WorkbookClass oBook;
            //Excel.Workbooks oBooks;
            //Excel.Worksheet oHoja;

            Excel.Application xlApp = new Excel.Application();
            Excel.Workbook xlWorkBook;
            Excel.Worksheet xlWorkSheet;
            //Excel.Range range;

            xlApp = new Excel.Application();
            xlWorkBook = xlApp.Workbooks.Open(@"d:\csharp-Excel.xls", 0, true, 5, "", "", true, Microsoft.Office.Interop.Excel.XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            xlWorkSheet = (Excel.Worksheet)xlWorkBook.Worksheets.get_Item(1);


            xlWorkSheet.Range["B5"].Value = CodigoPlaca;



            xlWorkBook.Close(true, null, null);
            xlApp.Quit();

            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
            Marshal.ReleaseComObject(xlApp);


        }
    }
}
