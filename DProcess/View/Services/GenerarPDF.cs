using Model;
using PdfSharp.Pdf;
using PdfSharp.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using System.Diagnostics;
using Model.ControlDocumentos;
using System.IO;
using MahApps.Metro.Controls.Dialogs;

namespace View.Services
{
    public class GenerarPDF
    {

        #region Constructor

        /// <summary>
        /// Constructor para Generar Archivos PDF.
        /// </summary> 
        public GenerarPDF()
        {
            DialogService dialog = new DialogService();
            
            //Declaramos el objeto para crear el PDF.
            PdfDocument pdf = new PdfDocument();

            //Agregamos el titulo al PDF.
            pdf.Info.Title = "Prueba No.1";

            //Definimos el tipo de letra, el tamaño, y el estilo respectivamente
            XFont font = new XFont("Verdana", 9, XFontStyle.Italic);

            //Agregamos una hoja al PDF.
            PdfPage pagina = pdf.AddPage();

            //Definimos el tamaño de la hoja del PDF.
            pagina.Size = PageSize.A4;

            //Definimos un objeto para escribir en el PDF
            XGraphics graph = XGraphics.FromPdfPage(pagina);

            int MargenIzquierdo = 35;

            string aux = "dasdsa" +Environment.NewLine;
            aux += "1"  + " "+ MargenIzquierdo +Environment.NewLine;
            aux += "Componente :" + " " +"PLPAS" + Environment.NewLine;
            aux += "Descripción : " + " "+ "EIJI" + Environment.NewLine;
            aux += "4" + Environment.NewLine;
            aux += "5" + Environment.NewLine;
            aux += "6" + Environment.NewLine;
            aux += "7" + Environment.NewLine;
            aux += "8" + Environment.NewLine;
            aux += "9" + Environment.NewLine; 
            aux += "10" + Environment.NewLine;
            aux += "11" + Environment.NewLine;
            aux += "12" + Environment.NewLine;
            aux += "13" + Environment.NewLine;
            aux += "14" + Environment.NewLine;
            aux += "15" + Environment.NewLine;
            aux += "16" + Environment.NewLine;
            aux += "17" + Environment.NewLine;
            aux += "18" + Environment.NewLine;
            aux += "19" + Environment.NewLine;
            aux += "20" + Environment.NewLine;
            aux += "21" + Environment.NewLine;
            aux += "22" + Environment.NewLine;
            aux += "23" + Environment.NewLine;
            aux += "24" + Environment.NewLine;
            aux += "25" + Environment.NewLine;
            aux += "26" + Environment.NewLine;
            aux += "27" + Environment.NewLine;
            aux += "28" + Environment.NewLine;
            aux += "29" + Environment.NewLine;
            aux += "30" + Environment.NewLine;
            aux += "31" + Environment.NewLine;
            aux += "32" + Environment.NewLine;
            aux += "33" + Environment.NewLine;
            aux += "34" + Environment.NewLine;
            aux += "35" + Environment.NewLine;
            aux += "36" + Environment.NewLine;
            aux += "37" + Environment.NewLine;
            aux += "38" + Environment.NewLine;
            aux += "39" + Environment.NewLine;
            aux += "40" + Environment.NewLine;
            aux += "41" + Environment.NewLine;
            aux += "42" + Environment.NewLine;
            aux += "43" + Environment.NewLine;
            aux += "10" + Environment.NewLine;
            aux += "11" + Environment.NewLine;
            aux += "12" + Environment.NewLine;
            aux += "13" + Environment.NewLine;
            aux += "14" + Environment.NewLine;
            aux += "15" + Environment.NewLine;
            aux += "16" + Environment.NewLine;
            aux += "17" + Environment.NewLine;
            aux += "18" + Environment.NewLine;
            aux += "19" + Environment.NewLine;
            aux += "20" + Environment.NewLine;
            aux += "21" + Environment.NewLine;
            aux += "22" + Environment.NewLine;
            aux += "23" + Environment.NewLine;
            aux += "24" + Environment.NewLine;
            aux += "25" + Environment.NewLine;
            aux += "26" + Environment.NewLine;
            aux += "27" + Environment.NewLine;
            aux += "28" + Environment.NewLine;
            aux += "29" + Environment.NewLine;
            aux += "30" + Environment.NewLine;
            aux += "31" + Environment.NewLine;
            aux += "32" + Environment.NewLine;
            aux += "33" + Environment.NewLine;
            aux += "34" + Environment.NewLine;
            aux += "35" + Environment.NewLine;
            aux += "36" + Environment.NewLine;
            aux += "37" + Environment.NewLine;
            aux += "38" + Environment.NewLine;
            aux += "39" + Environment.NewLine;
            aux += "40" + Environment.NewLine;
            aux += "41" + Environment.NewLine;
            aux += "42" + Environment.NewLine;
            aux += "43" + Environment.NewLine;
            aux += "10" + Environment.NewLine;
            aux += "11" + Environment.NewLine;
            aux += "12" + Environment.NewLine;
            aux += "13" + Environment.NewLine;
            aux += "14" + Environment.NewLine;
            aux += "15" + Environment.NewLine;
            aux += "16" + Environment.NewLine;
            aux += "17" + Environment.NewLine;
            aux += "18" + Environment.NewLine;
            aux += "19" + Environment.NewLine;
            aux += "20" + Environment.NewLine;
            aux += "21" + Environment.NewLine;
            aux += "22" + Environment.NewLine;
            aux += "23" + Environment.NewLine;
            aux += "24" + Environment.NewLine;
            aux += "25" + Environment.NewLine;
            aux += "26" + Environment.NewLine;
            aux += "27" + Environment.NewLine;
            aux += "28" + Environment.NewLine;
            aux += "29" + Environment.NewLine;
            aux += "30" + Environment.NewLine;
            aux += "31" + Environment.NewLine;
            aux += "32" + Environment.NewLine;
            aux += "33" + Environment.NewLine;
            aux += "34" + Environment.NewLine;
            aux += "35" + Environment.NewLine;
            aux += "36" + Environment.NewLine;
            aux += "37" + Environment.NewLine;
            aux += "38" + Environment.NewLine;
            aux += "39" + Environment.NewLine;
            aux += "40" + Environment.NewLine;
            aux += "41" + Environment.NewLine;
            aux += "42" + Environment.NewLine;
            aux += "43" + Environment.NewLine;

            string[] vector = aux.Split('\n');

            int margenSuperior = 65;
            int NumeroPagina = 1;

            //calculamos las lineas que se van a escribir en la hoja
            decimal LineasEscribir = vector.Length;

            //lineas máximas que caben en una hoja
            decimal LineasXHojaMax = 72;

            //Número de paginas totales
            decimal tot = (LineasEscribir / LineasXHojaMax);

            int TotalPaginas = 0;

            double w = Convert.ToDouble(tot % 1);

            if (w > .05)
            {
                //redondeamos el valor entero siguiente
                TotalPaginas = (int)Math.Ceiling(tot);
            }
            else
            {
                //redondeamos el valor entero bajo
                TotalPaginas = (int)Math.Floor(tot);
            }

            //Agregamos el encabezado a la primer pagina
            EncabezadoPagina(graph, pagina, font);
            //Agregamos el pie de pagina a la primer hoja
            PiePagina(graph, pagina, font, NumeroPagina, TotalPaginas);

            for (int i = 0; i < vector.Length; i++)
            {
                graph.DrawString(vector[i], font, XBrushes.Black, new XRect(MargenIzquierdo, margenSuperior, pagina.Width.Point, pagina.Height.Point), XStringFormats.TopLeft);

                //incrementamos el valor del margen superior
                margenSuperior +=10;

                //si el margen superior supera el valor máximo de una hoja se agrega otra hoja
                if (margenSuperior >= 765 && NumeroPagina != TotalPaginas)
                { 
                    // se inicializan los valores para escribir sobre esa hoja nueva         
                    pagina = pdf.AddPage();
                    pagina.Size = PageSize.A4;
                    graph = XGraphics.FromPdfPage(pagina);
                    margenSuperior = 65;

                    //aumentamos el valor de la hoja
                    NumeroPagina++;

                    //Mandamos llamar el método para que se imprima el encabezado en la nueva hoja
                    EncabezadoPagina(graph, pagina, font);
                    //Mandamos llamar el método para que se imprima el pie de pagina en la nueva hoja
                    PiePagina(graph, pagina, font, NumeroPagina, TotalPaginas);
                }  
            }

            //Generamos una cadena aleatoria para concatenarsela al nombre del archivo y poder mostrarlo
            string aleatorio = Module.GetRandomString(5);

            //Asignamos el nombre del archivo con la cadena aleatoria
            string NombrePDF = "Prueba" + "_" + aleatorio + ".pdf";

            //Guardamos el PDF con el nombre asignado
            pdf.Save(NombrePDF);

            //Abrimos el PDF
            Process.Start(NombrePDF);
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método para dibujar una linea
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="number"></param>
        private void LineaSeparadoraEncabezado(XGraphics gfx)
        {           
            gfx.DrawLine(XPens.Black, 35, 55, 565, 55);
        }

        /// <summary>
        /// Método para imprimir el pie de pagina
        /// </summary>
        private void LineaSeparadoraPiePagina(XGraphics gfx)
        {
            gfx.DrawLine(XPens.Black, 35, 770, 565, 770);
        }

        /// <summary>
        /// Método para imprimir el encabezado de las paginas
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="pag"></param>
        /// <param name="fondo"></param>
        /// <param name="paginaActual"></param>
        /// <param name="TotalPaginas"></param>
        private void EncabezadoPagina(XGraphics gfx, PdfPage pag, XFont fondo)
        {
            DateTime fecha = DataManagerControlDocumentos.Get_DateTime();
            //escribimos en el PDF.
            gfx.DrawString("Fecha de elaboración : " + fecha , fondo, XBrushes.Black, new XRect(35, 15, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            gfx.DrawString("No. de Componente : ", fondo, XBrushes.Black, new XRect(35, 25, pag.Width.Point, pag.Height.Point),XStringFormat.TopLeft);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

            LineaSeparadoraEncabezado(gfx);
            LineaSeparadoraPiePagina(gfx);
        }

        /// <summary>
        /// Método para imprimir el contenido del pie de pagina
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="pag"></param>
        /// <param name="fondo"></param>
        /// <param name="PaginaActual"></param>
        /// <param name="TotalPaginas"></param>
        private void PiePagina(XGraphics gfx, PdfPage pag, XFont fondo, int PaginaActual, int TotalPaginas)
        {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            gfx.DrawString("Página :" + " " + PaginaActual + "/" + TotalPaginas, fondo, XBrushes.Black, new XRect(510, 785, pag.Width.Point, pag.Height.Point), XStringFormat.TopLeft);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        }
        #endregion
    }
}