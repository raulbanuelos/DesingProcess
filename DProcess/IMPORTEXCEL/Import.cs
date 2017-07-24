using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;

namespace IMPORTEXCEL
{
   public static class Import
    {
        /// <summary>
        /// Importa formatos o documentos sin hiperlink
        /// </summary>
        /// <param name="usuario"></param>
         public async static void ImportAV(string usuario)
       {
           try
           {
               string path = "C:\\Users\\Ing.practicante\\Documents\\HIIS.xls";
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
                       objDocumento.nombre = range.Cells[aux, 4].Value2.ToString();
                       objDocumento.id_tipo_documento = 1003;
                       objDocumento.usuario = usuario;
                       objDocumento.descripcion = range.Cells[aux, 5].Value2.ToString();
                       fecha = range.Cells[aux, 7].Value2;
                       objDocumento.fecha_emision = DateTime.FromOADate(fecha);
                       objDocumento.fecha_actualizacion = DateTime.FromOADate(fecha);
                       objDocumento.id_estatus = 5;

                        int id_Documento = 1;//  DataManagerControlDocumentos.InsertDocumentos(objDocumento);

                       if (id_Documento != 0)
                       {
                           //Alta de Versión

                           objVersion.id_documento = id_Documento;
                           objVersion.id_estatus_version = 1;
                           objVersion.id_usuario_autorizo = usuario;
                           objVersion.id_usuario = usuario;
                           objVersion.no_version = range.Cells[aux, 6].Value2.ToString();
                           objVersion.no_copias = 0;
                           objVersion.fecha_version = DateTime.FromOADate(range.Cells[aux, 7].Value2);

                            int id_version = 1805;// DataManagerControlDocumentos.SetVersion(objVersion);

                           if (id_version != 0)
                           {
                               //Alta del Archivo

                               objArchivo.id_version = id_version;
                               url =string.Concat(range.Cells[aux, 4].Value2.ToString(),range.Cells[aux, 6].Value2.ToString());

                               url = string.Concat("C:\\Users\\Ing.practicante\\Documents\\OHSAS\\", url,".doc");
                             
                               objArchivo.ext = System.IO.Path.GetExtension(url);

                               objArchivo.nombre = System.IO.Path.GetFileNameWithoutExtension(url);

                               objArchivo.archivo = File.ReadAllBytes(url);

                              int archivo = await DataManagerControlDocumentos.SetArchivo(objArchivo);
                           }

                       }
                       aux++;
                   }

               }

               ExcelWork.Close();
           }
           catch (Exception er)
           {

               er.ToString();
           }
       }

        /// <summary>
        /// Importa ayudas Visuales, documentos con hiperlink
        /// </summary>
        /// <param name="usuario"></param>
        public async static void ImportAyuda_Visual(string usuario)
        {
            try
            {
                string path = "C:\\Users\\Ing.practicante\\Documents\\AYUDAVISUAL1.xls";
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
                    string f;

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
                        objDocumento.id_tipo_documento = 1004;
                        objDocumento.usuario = usuario;
                        objDocumento.descripcion = range.Cells[aux, 5].Value2.ToString();
                        fecha = range.Cells[aux, 6].Value2;
                        objDocumento.fecha_emision = DateTime.FromOADate(fecha);
                        objDocumento.fecha_actualizacion = DateTime.FromOADate(range.Cells[aux, 7].Value2);
                        objDocumento.id_estatus = 5;

                        int id_Documento = 325;
                        //DataManagerControlDocumentos.InsertDocumentos(objDocumento);

                        if (id_Documento != 0)
                        {
                            //Alta de Versión

                            objVersion.id_documento = id_Documento;
                            objVersion.id_estatus_version = 1;
                            objVersion.id_usuario_autorizo = usuario;
                            objVersion.id_usuario = usuario;
                            objVersion.no_version = range.Cells[aux, 10].Value2.ToString();
                            objVersion.no_copias = Convert.ToInt32(range.Cells[aux, 9].Value2.ToString());
                            objVersion.fecha_version = DateTime.FromOADate(range.Cells[aux, 7].Value2);

                            int id_version = DataManagerControlDocumentos.SetVersion(objVersion);

                            if (id_version != 0)
                            {
                                //Alta del Archivo

                                objArchivo.id_version = id_version;
                                url = range.Cells[aux, 4].Cells.Hyperlinks[1].Address;
                                url = url.Replace("..\\..\\M0051722\\AppData\\Roaming\\", "");
                                url = url.Replace("../../M0051722/AppData/Roaming/", "");
                                url = url.Replace("..\\..\\", "");

                                url = string.Concat("Z://", url);
                                //string u="\\agufileserv2\INGENIERIA\RESPRUTAS\MANUELITO\manuelito\MANUELITO\CIT\AYUDAS VISUALES (FISICAS)";

                                objArchivo.ext = System.IO.Path.GetExtension(url);

                                objArchivo.nombre = System.IO.Path.GetFileNameWithoutExtension(url);

                                objArchivo.archivo = File.ReadAllBytes(url);

                                int archivo = await DataManagerControlDocumentos.SetArchivo(objArchivo);
                            }

                        }
                        aux++;
                    }

                }

                ExcelWork.Close();
            }
            catch (Exception er)
            {

                er.ToString();
            }
        }


    }
}
