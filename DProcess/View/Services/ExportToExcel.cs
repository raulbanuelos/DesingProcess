using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Excel = Microsoft.Office.Interop.Excel;


namespace View.Services
{
    public static class ExportToExcel
    {
        /// <summary>
        /// Método que exporta el herramental de Coil a una hoja de excel.
        /// </summary>
        /// <param name="componente"></param>
        /// <param name="herrFeed"></param>
        /// <param name="herrCenterGuide"></param>
        /// <param name="herrEntranceGuide"></param>
        /// <param name="idealExitGuide"></param>
        /// <param name="aux1"></param>
        /// <param name="aux2"></param>
        /// <param name="aux3"></param>
        /// <returns></returns>
        public static string  ExportToolCoilTHM(string componente, Herramental herrFeed, Herramental herrCenterGuide, Herramental herrEntranceGuide, Herramental idealExitGuide, Herramental aux1, Herramental aux1_1, Herramental aux1_2, Herramental aux1_3, Herramental aux2, Herramental aux2_1, Herramental aux2_2, Herramental aux2_3, Herramental aux3)
        {
            try
            {
                //Creamos una instancia de la aplicación.
                Excel.Application ExcelApp = new Excel.Application();

                //Crea un nuevo documento.
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Add();

                //Añadimos una nueva hoja de cálculo 
                Excel.Worksheet ExcelWoorkSheet = ExcelWork.Sheets.Add();

                #region Encabezado
                ExcelWoorkSheet.Cells[4, 2] = "Componente";
                ExcelWoorkSheet.Range["B4", "C4"].Merge();
                ExcelWoorkSheet.Cells[4, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ExcelWoorkSheet.Cells[4, 2].Font.Bold = true;
                ExcelWoorkSheet.Cells[4, 2].Interior.Color = Excel.XlRgbColor.rgbLightBlue;

                ExcelWoorkSheet.Cells[4, 4] = componente;
                ExcelWoorkSheet.Range["D4", "J4"].Merge();
                ExcelWoorkSheet.Cells[4, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ExcelWoorkSheet.Cells[4, 4].Font.Bold = true;
                ExcelWoorkSheet.Cells[4, 4].Interior.Color = Excel.XlRgbColor.rgbLightBlue;

                //Dibuja los Bordes
                Excel.Borders border = ExcelWoorkSheet.Range["B4", "C4"].Borders;
                border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

                border = ExcelWoorkSheet.Range["D4", "J4"].Borders;
                border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                #endregion

                #region Columnas
                ExcelWoorkSheet.Cells[5, 2] = "Ítem";
                ExcelWoorkSheet.Cells[5, 3] = "Herramental";
                ExcelWoorkSheet.Cells[5, 4] = "Plano";
                ExcelWoorkSheet.Cells[5, 5] = "Code";
                ExcelWoorkSheet.Cells[5, 6] = "DIM A";
                ExcelWoorkSheet.Cells[5, 7] = "DIM B";
                ExcelWoorkSheet.Cells[5, 8] = "DIM C";
                ExcelWoorkSheet.Cells[5, 9] = "Cantidad";
                ExcelWoorkSheet.Cells[5, 10] = "Código";
                
                ExcelWoorkSheet.Cells[5, 2].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 3].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 4].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 5].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 6].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 7].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 8].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 9].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 10].Font.Bold = true;

                ExcelWoorkSheet.Cells[5, 2].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 3].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 4].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 5].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 6].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 7].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 8].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 9].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 10].Interior.Color = Excel.XlRgbColor.rgbLightBlue;

                ExcelWoorkSheet.Cells[5, 2].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 3].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 4].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 5].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 6].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 7].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 8].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 9].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 10].Font.Color = Excel.XlRgbColor.rgbBlack;
                #endregion

                #region Feed Roller
                ExcelWoorkSheet.Cells[6, 2] = "1";
                ExcelWoorkSheet.Cells[6, 3] = herrFeed.DescripcionGeneral;
                ExcelWoorkSheet.Cells[6, 4] = herrFeed.Plano;
                ExcelWoorkSheet.Cells[6, 5] = Module.GetValorPropiedadString("Detalle", herrFeed.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[6, 6] = Module.GetValorPropiedad("DIMA", herrFeed.Propiedades.ToList());
                ExcelWoorkSheet.Cells[6, 7] = "";
                ExcelWoorkSheet.Cells[6, 8] = "";
                ExcelWoorkSheet.Cells[6, 9] = herrFeed.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[6, 10] = herrFeed.Codigo;
                #endregion

                #region Entrance guide
                ExcelWoorkSheet.Cells[7, 2] = "2";
                ExcelWoorkSheet.Cells[7, 3] = herrEntranceGuide.DescripcionGeneral;
                ExcelWoorkSheet.Cells[7, 4] = herrEntranceGuide.Plano;
                ExcelWoorkSheet.Cells[7, 5] = Module.GetValorPropiedadString("Detalle", herrEntranceGuide.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[7, 6] = Module.GetValorPropiedad("DIMA", herrEntranceGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[7, 7] = Module.GetValorPropiedad("DIMB", herrEntranceGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[7, 8] = Module.GetValorPropiedad("DIMC", herrEntranceGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[7, 9] = herrEntranceGuide.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[7, 10] = herrEntranceGuide.Codigo;
                #endregion

                #region Center Guide
                ExcelWoorkSheet.Cells[8, 2] = "3";
                ExcelWoorkSheet.Cells[8, 3] = herrCenterGuide.DescripcionGeneral;
                ExcelWoorkSheet.Cells[8, 4] = herrCenterGuide.Plano;
                ExcelWoorkSheet.Cells[8, 5] = Module.GetValorPropiedadString("Detalle", herrCenterGuide.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[8, 6] = Module.GetValorPropiedad("DIMA", herrCenterGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[8, 7] = Module.GetValorPropiedad("DIMB", herrCenterGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[8, 8] = Module.GetValorPropiedad("DIMC", herrCenterGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[8, 9] = herrCenterGuide.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[8, 10] = herrCenterGuide.Codigo;
                #endregion

                #region Exit Guide
                ExcelWoorkSheet.Cells[9, 2] = "4";
                ExcelWoorkSheet.Cells[9, 3] = idealExitGuide.DescripcionGeneral;
                ExcelWoorkSheet.Cells[9, 4] = idealExitGuide.Plano;
                ExcelWoorkSheet.Cells[9, 5] = Module.GetValorPropiedadString("Detalle", idealExitGuide.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[9, 6] = Module.GetValorPropiedad("DIMA", idealExitGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[9, 7] = Module.GetValorPropiedad("DIMB", idealExitGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[9, 8] = Module.GetValorPropiedad("DIMC", idealExitGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[9, 9] = idealExitGuide.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[9, 10] = idealExitGuide.Codigo;
                #endregion

                #region PARTE 1 INTERNAL ROLLER
                

                ExcelWoorkSheet.Cells[10, 2] = "6";
                ExcelWoorkSheet.Cells[10, 3] = "INTERNAL ROLLER 3 PIECES 2487 - 110 - 01 - 4 BI-PARTIDO";
                ExcelWoorkSheet.Cells[10, 4] = aux1_1.Plano;
                ExcelWoorkSheet.Cells[10, 5] = Module.GetValorPropiedadString("Detalle", aux1_1.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[10, 6] = Module.GetValorPropiedad("DIMA", aux1_1.Propiedades.ToList());
                ExcelWoorkSheet.Cells[10, 7] = Module.GetValorPropiedad("DIMB", aux1_1.Propiedades.ToList());
                ExcelWoorkSheet.Cells[10, 8] = Module.GetValorPropiedad("DIMC", aux1_1.Propiedades.ToList());
                ExcelWoorkSheet.Cells[10, 9] = aux1_1.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[10, 10] = aux1_1.Codigo;

                ExcelWoorkSheet.Cells[11, 2] = "7";
                ExcelWoorkSheet.Cells[11, 3] = "INTERNAL ROLLER 3 PIECES 2487 - 110 - 02 - 4 BI-PARTIDO";
                ExcelWoorkSheet.Cells[11, 4] = aux1_2.Plano;
                ExcelWoorkSheet.Cells[11, 5] = Module.GetValorPropiedadString("Detalle", aux1_2.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[11, 6] = Module.GetValorPropiedad("DIMA", aux1_2.Propiedades.ToList());
                ExcelWoorkSheet.Cells[11, 7] = Module.GetValorPropiedad("DIMB", aux1_2.Propiedades.ToList());
                ExcelWoorkSheet.Cells[11, 8] = Module.GetValorPropiedad("DIMC", aux1_2.Propiedades.ToList());
                ExcelWoorkSheet.Cells[11, 9] = aux1_2.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[11, 10] = aux1_2.Codigo;

                ExcelWoorkSheet.Cells[12, 2] = "8";
                ExcelWoorkSheet.Cells[12, 3] = "INTERNAL ROLLER 3 PIECES 2487 - 110 - 03 - 4 BI-PARTIDO";
                ExcelWoorkSheet.Cells[12, 4] = aux1_3.Plano;
                ExcelWoorkSheet.Cells[12, 5] = Module.GetValorPropiedadString("Detalle", aux1_3.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[12, 6] = Module.GetValorPropiedad("DIMA", aux1_3.Propiedades.ToList());
                ExcelWoorkSheet.Cells[12, 7] = Module.GetValorPropiedad("DIMB", aux1_3.Propiedades.ToList());
                ExcelWoorkSheet.Cells[12, 8] = Module.GetValorPropiedad("DIMC", aux1_3.Propiedades.ToList());
                ExcelWoorkSheet.Cells[12, 9] = aux1_3.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[12, 10] = aux1_3.Codigo;

                #endregion

                #region PARTE 2 EXTERNAL ROLLER
                

                ExcelWoorkSheet.Cells[13, 2] = "10";
                ExcelWoorkSheet.Cells[13, 3] = "EXTERNAL ROLLER (2487 111 01 4) BI-PARTIDO";
                ExcelWoorkSheet.Cells[13, 4] = aux2_1.Plano;
                ExcelWoorkSheet.Cells[13, 5] = Module.GetValorPropiedadString("Detalle", aux2_1.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[13, 6] = Module.GetValorPropiedad("DIMA", aux2_1.Propiedades.ToList());
                ExcelWoorkSheet.Cells[13, 7] = Module.GetValorPropiedad("DIMB", aux2_1.Propiedades.ToList());
                ExcelWoorkSheet.Cells[13, 8] = Module.GetValorPropiedad("DIMC", aux2_1.Propiedades.ToList());
                ExcelWoorkSheet.Cells[13, 9] = aux2_1.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[13, 10] = aux2_1.Codigo;

                ExcelWoorkSheet.Cells[14, 2] = "11";
                ExcelWoorkSheet.Cells[14, 3] = "EXTERNAL ROLLER (2487 111 02 4) BI-PARTIDO";
                ExcelWoorkSheet.Cells[14, 4] = aux2_2.Plano;
                ExcelWoorkSheet.Cells[14, 5] = Module.GetValorPropiedadString("Detalle", aux2_2.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[14, 6] = Module.GetValorPropiedad("DIMA", aux2_2.Propiedades.ToList());
                ExcelWoorkSheet.Cells[14, 7] = Module.GetValorPropiedad("DIMB", aux2_2.Propiedades.ToList());
                ExcelWoorkSheet.Cells[14, 8] = Module.GetValorPropiedad("DIMC", aux2_2.Propiedades.ToList());
                ExcelWoorkSheet.Cells[14, 9] = aux2_2.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[14, 10] = aux2_2.Codigo;

                ExcelWoorkSheet.Cells[15, 2] = "12";
                ExcelWoorkSheet.Cells[15, 3] = "EXTERNAL ROLLER (2487 111 03 4) BI-PARTIDO";
                ExcelWoorkSheet.Cells[15, 4] = aux2_3.Plano;
                ExcelWoorkSheet.Cells[15, 5] = Module.GetValorPropiedadString("Detalle", aux2_3.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[15, 6] = Module.GetValorPropiedad("DIMA", aux2_3.Propiedades.ToList());
                ExcelWoorkSheet.Cells[15, 7] = Module.GetValorPropiedad("DIMB", aux2_3.Propiedades.ToList());
                ExcelWoorkSheet.Cells[15, 8] = Module.GetValorPropiedad("DIMC", aux2_3.Propiedades.ToList());
                ExcelWoorkSheet.Cells[15, 9] = aux2_3.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[15, 10] = aux2_3.Codigo;
                #endregion

                #region PARTE 3
                //ExcelWoorkSheet.Cells[18, 2] = "13";
                //ExcelWoorkSheet.Cells[18, 3] = aux3.DescripcionGeneral;
                //ExcelWoorkSheet.Cells[18, 4] = aux3.Plano;
                //ExcelWoorkSheet.Cells[18, 5] = Module.GetValorPropiedadString("Detalle", aux3.PropiedadesCadena.ToList());
                //ExcelWoorkSheet.Cells[18, 6] = Module.GetValorPropiedad("DIMA", aux3.Propiedades.ToList());
                //ExcelWoorkSheet.Cells[18, 7] = Module.GetValorPropiedad("DIMB", aux3.Propiedades.ToList());
                //ExcelWoorkSheet.Cells[18, 8] = Module.GetValorPropiedad("DIMC", aux3.Propiedades.ToList());
                //ExcelWoorkSheet.Cells[18, 9] = aux3.clasificacionHerramental.CantidadUtilizar;
                //ExcelWoorkSheet.Cells[18, 10] = aux3.Codigo;
                #endregion

                //Ajustamos el tamaño de las columnas.
                ExcelWoorkSheet.Columns.AutoFit();

                #region Formato Borders
                int rowBegin = 5;
                int columnBegin = 2;
                int rowEnds = 15;
                int columnEnds = 10;

                for (int i = rowBegin; i <= rowEnds; i++)
                {
                    for (int j = columnBegin; j <= columnEnds; j++)
                    {
                        border = ExcelWoorkSheet.Cells[i, j].Borders;
                        border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                        border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                        border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

                        if (j != 3)
                            ExcelWoorkSheet.Cells[i, j].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    }
                }
                #endregion

                //Muestra el documento.
                ExcelApp.Visible = true;

                return "Ok";
            }
            catch (Exception ex)
            {
                //Retorna el error.
                return ex.Message;
            }
        }

        /// <summary>
        /// Método que exporta el herramental de Coil a una hoja de excel.
        /// </summary>
        /// <param name="componente"></param>
        /// <param name="herrFeed"></param>
        /// <param name="herrCenterGuide"></param>
        /// <param name="herrEntranceGuide"></param>
        /// <param name="idealExitGuide"></param>
        /// <param name="herr1PieceExternal"></param>
        /// <returns></returns>
        public static string  ExportToolCoilCuadrado(string componente, Herramental herrFeed, Herramental herrCenterGuide, Herramental herrEntranceGuide, Herramental idealExitGuide, Herramental herr1PieceExternal, Herramental herr1PieceInternal)
        {
            try
            {
                //Creamos una instancia de la aplicación.
                Excel.Application ExcelApp = new Excel.Application();

                //Crea un nuevo documento.
                Excel.Workbook ExcelWork = ExcelApp.Workbooks.Add();

                //Añadimos una nueva hoja de cálculo 
                Excel.Worksheet ExcelWoorkSheet = ExcelWork.Sheets.Add();

                #region Encabezado
                ExcelWoorkSheet.Cells[4, 2] = "Componente";
                ExcelWoorkSheet.Range["B4", "C4"].Merge();
                ExcelWoorkSheet.Cells[4, 2].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ExcelWoorkSheet.Cells[4, 2].Font.Bold = true;
                ExcelWoorkSheet.Cells[4, 2].Interior.Color = Excel.XlRgbColor.rgbLightBlue;

                ExcelWoorkSheet.Cells[4, 4] = componente;
                ExcelWoorkSheet.Range["D4", "J4"].Merge();
                ExcelWoorkSheet.Cells[4, 4].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
                ExcelWoorkSheet.Cells[4, 4].Font.Bold = true;
                ExcelWoorkSheet.Cells[4, 4].Interior.Color = Excel.XlRgbColor.rgbLightBlue;

                //Dibuja los Bordes
                Excel.Borders border = ExcelWoorkSheet.Range["B4", "C4"].Borders;
                border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

                border = ExcelWoorkSheet.Range["D4", "J4"].Borders;
                border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;
                #endregion

                #region Columnas
                ExcelWoorkSheet.Cells[5, 2] = "Ítem";
                ExcelWoorkSheet.Cells[5, 3] = "Herramental";
                ExcelWoorkSheet.Cells[5, 4] = "Plano";
                ExcelWoorkSheet.Cells[5, 5] = "Code";
                ExcelWoorkSheet.Cells[5, 6] = "DIM A";
                ExcelWoorkSheet.Cells[5, 7] = "DIM B";
                ExcelWoorkSheet.Cells[5, 8] = "DIM C";
                ExcelWoorkSheet.Cells[5, 9] = "Cantidad";
                ExcelWoorkSheet.Cells[5, 10] = "Código";

                ExcelWoorkSheet.Cells[5, 2].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 3].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 4].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 5].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 6].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 7].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 8].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 9].Font.Bold = true;
                ExcelWoorkSheet.Cells[5, 10].Font.Bold = true;

                ExcelWoorkSheet.Cells[5, 2].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 3].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 4].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 5].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 6].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 7].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 8].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 9].Interior.Color = Excel.XlRgbColor.rgbLightBlue;
                ExcelWoorkSheet.Cells[5, 10].Interior.Color = Excel.XlRgbColor.rgbLightBlue;

                ExcelWoorkSheet.Cells[5, 2].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 3].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 4].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 5].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 6].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 7].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 8].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 9].Font.Color = Excel.XlRgbColor.rgbBlack;
                ExcelWoorkSheet.Cells[5, 10].Font.Color = Excel.XlRgbColor.rgbBlack;
                #endregion

                #region Feed Roller
                ExcelWoorkSheet.Cells[6, 2] = "1";
                ExcelWoorkSheet.Cells[6, 3] = herrFeed.DescripcionGeneral;
                ExcelWoorkSheet.Cells[6, 4] = herrFeed.Plano;
                ExcelWoorkSheet.Cells[6, 5] = Module.GetValorPropiedadString("Detalle", herrFeed.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[6, 6] = Module.GetValorPropiedad("DIMA", herrFeed.Propiedades.ToList());
                ExcelWoorkSheet.Cells[6, 7] = "";
                ExcelWoorkSheet.Cells[6, 8] = "";
                ExcelWoorkSheet.Cells[6, 9] = herrFeed.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[6, 10] = herrFeed.Codigo;
                #endregion

                #region Entrance guide
                ExcelWoorkSheet.Cells[7, 2] = "2";
                ExcelWoorkSheet.Cells[7, 3] = herrEntranceGuide.DescripcionGeneral;
                ExcelWoorkSheet.Cells[7, 4] = herrEntranceGuide.Plano;
                ExcelWoorkSheet.Cells[7, 5] = Module.GetValorPropiedadString("Detalle", herrEntranceGuide.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[7, 6] = Module.GetValorPropiedad("DIMA", herrEntranceGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[7, 7] = Module.GetValorPropiedad("DIMB", herrEntranceGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[7, 8] = Module.GetValorPropiedad("DIMC", herrEntranceGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[7, 9] = herrEntranceGuide.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[7, 10] = herrEntranceGuide.Codigo;
                #endregion

                #region Center Guide
                ExcelWoorkSheet.Cells[8, 2] = "3";
                ExcelWoorkSheet.Cells[8, 3] = herrCenterGuide.DescripcionGeneral;
                ExcelWoorkSheet.Cells[8, 4] = herrCenterGuide.Plano;
                ExcelWoorkSheet.Cells[8, 5] = Module.GetValorPropiedadString("Detalle", herrCenterGuide.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[8, 6] = Module.GetValorPropiedad("DIMA", herrCenterGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[8, 7] = Module.GetValorPropiedad("DIMB", herrCenterGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[8, 8] = Module.GetValorPropiedad("DIMC", herrCenterGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[8, 9] = herrCenterGuide.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[8, 10] = herrCenterGuide.Codigo;
                #endregion

                #region Exit Guide
                ExcelWoorkSheet.Cells[9, 2] = "4";
                ExcelWoorkSheet.Cells[9, 3] = idealExitGuide.DescripcionGeneral;
                ExcelWoorkSheet.Cells[9, 4] = idealExitGuide.Plano;
                ExcelWoorkSheet.Cells[9, 5] = Module.GetValorPropiedadString("Detalle", idealExitGuide.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[9, 6] = Module.GetValorPropiedad("DIMA", idealExitGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[9, 7] = Module.GetValorPropiedad("DIMB", idealExitGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[9, 8] = Module.GetValorPropiedad("DIMC", idealExitGuide.Propiedades.ToList());
                ExcelWoorkSheet.Cells[9, 9] = idealExitGuide.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[9, 10] = idealExitGuide.Codigo;
                #endregion

                #region External Roller
                ExcelWoorkSheet.Cells[10, 2] = "5";
                ExcelWoorkSheet.Cells[10, 3] = herr1PieceExternal.DescripcionGeneral;
                ExcelWoorkSheet.Cells[10, 4] = herr1PieceExternal.Plano;
                ExcelWoorkSheet.Cells[10, 5] = Module.GetValorPropiedadString("Detalle", herr1PieceExternal.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[10, 6] = "";
                ExcelWoorkSheet.Cells[10, 7] = Module.GetValorPropiedad("DIMB", herr1PieceExternal.Propiedades.ToList());
                ExcelWoorkSheet.Cells[10, 8] = "";
                ExcelWoorkSheet.Cells[10, 9] = herr1PieceExternal.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[10, 10] = herr1PieceExternal.Codigo;
                #endregion

                #region internal Roller
                ExcelWoorkSheet.Cells[11, 2] = "6";
                ExcelWoorkSheet.Cells[11, 3] = herr1PieceInternal.DescripcionGeneral;
                ExcelWoorkSheet.Cells[11, 4] = herr1PieceInternal.Plano;
                ExcelWoorkSheet.Cells[11, 5] = Module.GetValorPropiedadString("Detalle", herr1PieceInternal.PropiedadesCadena.ToList());
                ExcelWoorkSheet.Cells[11, 6] = "";
                ExcelWoorkSheet.Cells[11, 7] = Module.GetValorPropiedad("DIMB", herr1PieceInternal.Propiedades.ToList());
                ExcelWoorkSheet.Cells[11, 8] = "";
                ExcelWoorkSheet.Cells[11, 9] = herr1PieceInternal.clasificacionHerramental.CantidadUtilizar;
                ExcelWoorkSheet.Cells[11, 10] = herr1PieceInternal.Codigo;
                #endregion

                //Ajustamos el tamaño de las columnas.
                ExcelWoorkSheet.Columns.AutoFit();

                #region Formato Borders
                int rowBegin = 5;
                int columnBegin = 2;
                int rowEnds = 11;
                int columnEnds = 11;

                for (int i = rowBegin; i <= rowEnds; i++)
                {
                    for (int j = columnBegin; j <= columnEnds; j++)
                    {
                        border = ExcelWoorkSheet.Cells[i, j].Borders;
                        border[Excel.XlBordersIndex.xlEdgeLeft].LineStyle = Excel.XlLineStyle.xlContinuous;
                        border[Excel.XlBordersIndex.xlEdgeTop].LineStyle = Excel.XlLineStyle.xlContinuous;
                        border[Excel.XlBordersIndex.xlEdgeBottom].LineStyle = Excel.XlLineStyle.xlContinuous;
                        border[Excel.XlBordersIndex.xlEdgeRight].LineStyle = Excel.XlLineStyle.xlContinuous;

                        if (j != 3)
                            ExcelWoorkSheet.Cells[i, j].HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;

                    }
                } 
                #endregion

                //Muestra el documento.
                ExcelApp.Visible = true;

                return "Ok";
            }
            catch (Exception ex)
            {
                //Retorna el error.
                return ex.Message;
            }
        }

        /// <summary>
        /// Método para exportar un archivo excel desde un dataset
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static Task<string> Export(DataSet data)
        {
            return Task.Run(() =>
            {
                try
                {
                    //Si la información recibida es diferente de cero
                    if (data != null)
                    {
                        //Creamos una instancia de la aplicación.
                        Excel.Application ExcelApp = new Excel.Application();

                        //Devuelve información sobre la configuración internacional y el país o región actual.
                        //Devuleve el símbolo del día, mes y año dependiento de la configuración internacional
                        var yearCode =ExcelApp.International[Excel.XlApplicationInternational.xlYearCode];
                        var monthcode = ExcelApp.International[Excel.XlApplicationInternational.xlMonthCode];
                        var daycode= ExcelApp.International[Excel.XlApplicationInternational.xlDayCode];

                        //Se concatena los valores para obtener el formato de la fecha 
                        string formato = daycode + daycode + "/" + monthcode + monthcode + "/" + yearCode + yearCode + yearCode + yearCode;
                        
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

                                //Si la columna es de tipo fecha 
                                if (table.Columns[i].DataType == Type.GetType("System.DateTime"))
                                {
                                    //Establecemos el formato de fecha a toda la columna
                                    ExcelWoorkSheet.Cells[1, i + 1].EntireColumn.NumberFormat = formato;
                                }
                            }
                            //Reccorre el número de filas de la tabla.
                            for (int j = 0; j < table.Rows.Count; j++)
                            {
                                //Recorre el número de columnas de la tabla.
                                for (int k = 0; k < table.Columns.Count; k++)
                                {
                                    //Si el valor es de tipo fecha 
                                    if (table.Rows[j].ItemArray[k].GetType() == Type.GetType("System.DateTime"))
                                    {
                                        //Asignamos a la celda la fecha de la tabla 
                                        ExcelWoorkSheet.Cells[j + 2, k + 1].Value = table.Rows[j].ItemArray[k];
                                    }
                                    else
                                    {
                                        //si no es de tipo fecha, convertimos en string el valor de la tabla y lo asignamos a la celda
                                        ExcelWoorkSheet.Cells[j + 2, k + 1] = table.Rows[j].ItemArray[k].ToString();
                                    }
                                    
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
            });
           }
        
    }
}
