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
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace View.Services
{
    public class GenerarPDF
    {
        #region Constructor

        /// <summary>
        /// Constructor para Generar Archivos PDF.
        /// </summary> 
        public GenerarPDF(Anillo Componentes)
        {

        }
        #endregion

        #region Variables Globales
        private static PdfDocument pdf;
        private static PdfPage pag;
        private static XGraphics gfx;
        private static int PaginaActual = 1;
        private static int TotalPaginas;
        #endregion

        #region Métodos

        /// <summary>
        /// Método para dibujar una linea
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="number"></param>
        private static void LineaSeparadoraEncabezado(XGraphics gfx)
        {
            gfx.DrawLine(XPens.Blue, 35, 55, 565, 55);
        }

        /// <summary>
        /// Método para imprimir el pie de pagina
        /// </summary>
        private static void LineaSeparadoraPiePagina(XGraphics gfx)
        {
            gfx.DrawLine(XPens.Green, 35, 770, 565, 770);
        }

        /// <summary>
        /// Método para imprimir el encabezado de las paginas
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="pag"></param>
        /// <param name="fondo"></param>
        /// <param name="paginaActual"></param>
        /// <param name="TotalPaginas"></param>
        private static void EncabezadoPagina(XGraphics gfx, PdfPage pag, XFont fondo)
        {
            DateTime fecha = DataManagerControlDocumentos.Get_DateTime();
            //escribimos en el PDF.
            gfx.DrawString("Fecha de elaboración : " + fecha, fondo, XBrushes.Black, new XRect(35, 15, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            gfx.DrawString("No. de Componente : ", fondo, XBrushes.Black, new XRect(35, 25, pag.Width.Point, pag.Height.Point), XStringFormat.TopLeft);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos

            LineaSeparadoraEncabezado(gfx);
            LineaSeparadoraPiePagina(gfx);
        }

        /// <summary>
        /// Método para dibujar la linea separadora entre cada bloque
        /// </summary>
        private static void LineaSeparadora(XGraphics gfx, int Margen)
        {
            gfx.DrawLine(XPens.Black,35,Margen,565,Margen);
        }

        /// <summary>
        /// Método para imprimir el contenido del pie de pagina
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="pag"></param>
        /// <param name="fondo"></param>
        /// <param name="PaginaActual"></param>
        /// <param name="TotalPaginas"></param>
        private static void PiePagina(XGraphics gfx, PdfPage pag, XFont fondo)
        {
#pragma warning disable CS0618 // El tipo o el miembro están obsoletos
            gfx.DrawString("Página :" + " " + PaginaActual + "/" + TotalPaginas, fondo, XBrushes.Black, new XRect(510, 785, pag.Width.Point, pag.Height.Point), XStringFormat.TopLeft);
#pragma warning restore CS0618 // El tipo o el miembro están obsoletos
        }

        /// <summary>
        /// Método que genera la hoja de ruta en PDF
        /// </summary>
        /// <param name="Componente"></param>
        public static void Traveler(Anillo Componente)
        {
            DialogService dialog = new DialogService();

            //Declaramos el objeto para crear el PDF.
            pdf = new PdfDocument();

            //Agregamos el titulo al PDF.
            pdf.Info.Title = "Prueba No.1";

            //Definimos el tipo de letra para el texto en general del PDF
            XFont TextoGeneral = new XFont("Verdana", 9, XFontStyle.Regular);

            //Definimos el tipo de letra para el encabezado
            XFont TextoEncabezado = new XFont("Verdana", 9, XFontStyle.Italic);

            //Agregamos una hoja al PDF.
            pag = pdf.AddPage();

            //Definimos el tamaño de la hoja del PDF.
            pag.Size = PageSize.A4;

            //Definimos un objeto para escribir en el PDF
            gfx = XGraphics.FromPdfPage(pag);

            //establecemos el valor del Margen Izquierdo
            int MargenIzquierdo = 35;

            //Separamos los valores que tenga la caratulo por saltos de linea y los guardamos en un vector
            string[] vector = Componente.Caratula.Split('\n');

            int margenSuperior = 65;

            //Obtenemos las lineas que se van a imprimir en la caratula
            int sumalineas = vector.Length;

            //Aqui obtenemos las lineas impresas en cada operación
            foreach (var ope in Componente.Operaciones)
            {
                string[] vector1 = ope.TextoSyteline.Split('\n');
                sumalineas += vector1.Length;
                sumalineas += ope.ListaHerramentales.Count;

                sumalineas += 1; //<--Numero de lineas en Titulo de herramentales
                sumalineas += 5; //<--Numero de lineas en 2do bloque.
            }

            //Dividimos el total de lineas obtenidas en todo el PDF sobre las lineas que hay en cada hoja
            double r = Convert.ToDouble(sumalineas) / 71.0;

            //redondeamos el valor hacia arriba
            TotalPaginas = (int)Math.Ceiling(r);

            //Agregamos el encabezado a la primer pagina
            EncabezadoPagina(gfx, pag, TextoEncabezado);
            //Agregamos el pie de pagina a la primer hoja
            PaginaActual = 1;
            PiePagina(gfx, pag, TextoEncabezado);

            //Comenzamos a imprimir la caratula
            for (int i = 0; i < vector.Length; i++)
            {
                gfx.DrawString(vector[i], TextoGeneral, XBrushes.Black, new XRect(MargenIzquierdo, margenSuperior, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);

                //incrementamos el valor del margen superior
                margenSuperior += 10;

                //Mandamos llamar el metodo para ver si se necesita agregar mas hojas
                margenSuperior = ControlDePaginas(margenSuperior, TextoEncabezado);
            }

            //Mandamos llamar el metodo para imprimir las operaciones y los herramentales
            TercerBloque(margenSuperior, MargenIzquierdo, Componente, TextoEncabezado);

            //Generamos una cadena aleatoria para concatenarsela al nombre del archivo y poder mostrarlo
            string aleatorio = Module.GetRandomString(5);

            //Asignamos el nombre del archivo con la cadena aleatoria
            string NombrePDF = "Prueba" + "_" + aleatorio + ".pdf";

            //Guardamos el PDF con el nombre asignado
            pdf.Save(NombrePDF);

            //Abrimos el PDF
            Process.Start(NombrePDF);
        }

        /// <summary>
        /// Método que escribe el segundo bloque del PDF.
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="font"></param>
        /// <param name="margensup"></param>
        /// <param name="margenizq"></param>
        /// <param name="pag"></param>
        /// <returns></returns>
        private static int  SegundoBloque(int margensup, int margenizq, XFont Encabezado)
        {

            //Definimos el tipo de letra para el texto en general del PDF
            XFont TextoGeneral = new XFont("Verdana", 9, XFontStyle.Regular);

            //hacemos la linea separadora
            LineaSeparadora(gfx,margensup);

            string SegundoParrafo = " " + Environment.NewLine;
            SegundoParrafo += "Clock No." + "    " + "    " + "Date" + "       " + "  " + "Yield" + "      " + "     " + "Scrap" + "             " + "Mach" + Environment.NewLine;
            SegundoParrafo += "_________" + "  " + "_________" + "  " + "_________" + "  " + "_________" + "  " + "_________" + Environment.NewLine;
            SegundoParrafo += "_________" + "  " + "_________" + "  " + "_________" + "  " + "_________" + "  " + "_________" + Environment.NewLine;
            SegundoParrafo += "_________" + "  " + "_________" + "  " + "_________" + "  " + "_________" + "  " + "_________" + Environment.NewLine;

            //separamos la variable por saltos de linea
            string[] vector = SegundoParrafo.Split('\n');

            //Imprimimosel cuadro
            for (int i = 0; i < vector.Length; i++)
            {
                gfx.DrawString(vector[i], TextoGeneral, XBrushes.Black, new XRect(margenizq, margensup, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);
                margensup += 10;

                //Mandamos llamar el método para verificar que no se necesiten agregar mas hojas
                margensup = ControlDePaginas(margensup, Encabezado);

            }
            //Regresamos el valor con el que se quedo el margen superior para que a partir de ahi se comience a escribir
            return margensup;
        }
        
        /// <summary>
        /// Método para generar los parrafos de las operaciones e imprimir los herramentales de cada operacíon
        /// </summary>
        /// <param name="gfx"></param>
        /// <param name="font"></param>
        /// <param name="margensup"></param>
        /// <param name="margeninzq"></param>
        /// <param name="paga"></param>
        /// <param name="Model"></param>
        /// <returns></returns>
        private static void TercerBloque(int margensup, int margenizq, Anillo Model, XFont TextoEncabezado)
        {
            //Definimos el tipo de letra para el txto de herramentales
            XFont TextoHerramentales = new XFont("Arial", 10, XFontStyle.Bold);

            //Definimos el tipo de letra para el texto de operación
            XFont TextoOperacion = new XFont("Arial", 9, XFontStyle.Regular);

            foreach (var operacion in Model.Operaciones)
            {
                //Mandamos llamar el segundo bloque. regresa un valor entero que es donde se quedo al imprimir el cuadro y de ahi comenzaremos a escribir las operaciones
                int Comenzar = SegundoBloque(margensup, margenizq, TextoEncabezado);

                //obtenemos las operaciones y las separamos por saltos de linea
                string[] ListaOperaciones = operacion.TextoSyteline.Split('\n');

                //imprimimos cada linea de la operación
                for (int i = 0; i < ListaOperaciones.Length; i++)
                {
                    //Verificamos que las operaciones no sean valores nulos
                    if (!string.IsNullOrEmpty(ListaOperaciones[i]))
                    {
                        //Obtenemos el primer caracter de cada operacion
                        string r = ListaOperaciones[i].Substring(0, 1);
                        //Si el primer caracter es * ó si son las dos primeras lineas se imprimen alineados a la izquierda
                        if (r == "*" || i< 2)
                        {
                            gfx.DrawString(ListaOperaciones[i], TextoOperacion, XBrushes.Black, new XRect(margenizq, Comenzar, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);
                            Comenzar += 10;

                            //Mandamos llamar el método para verificar que no se necesiten agregar mas hojas
                            Comenzar = ControlDePaginas(Comenzar, TextoEncabezado);
                        }else
                        {
                            gfx.DrawString(ListaOperaciones[i], TextoOperacion, XBrushes.Black, new XRect(150, Comenzar, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);
                            Comenzar += 10;

                            //Mandamos llamar el método para verificar que no se necesiten agregar mas hojas
                            Comenzar = ControlDePaginas(Comenzar, TextoEncabezado);
                        }
                    }      
                }

                //Recorremos cada  lista de herramental para saber si la operación cuenta con lista de herramentales
                foreach (var herrmental in operacion.ListaHerramentales)
                {
                    //si existe una lista de herramentales se imprimen los titulos
                    if (herrmental != null)
                    {
                        //Imprimimos el titulo
                        string titleTooling = "Tools for operation " + operacion.NoOperacion;
                        gfx.DrawString(titleTooling, TextoHerramentales, XBrushes.Black, new XRect(margenizq, Comenzar, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);
                        Comenzar += 10;

                        //Imprimimos los titulos de las columnas
                        gfx.DrawString("Código", TextoHerramentales, XBrushes.Black, new XRect(margenizq, Comenzar, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);
                        gfx.DrawString("Descripción", TextoHerramentales, XBrushes.Black, new XRect(200, Comenzar, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);
                        gfx.DrawString("Cantidad", TextoHerramentales, XBrushes.Black, new XRect(500, Comenzar, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);
                        Comenzar += 10;

                        //Mandamos llamar el método para verificar que no se necesiten agregar mas hojas
                        Comenzar = ControlDePaginas(Comenzar, TextoEncabezado);
                        //rompemos el ciclo si ya se encontro un herramental para que no se impriman los titulos mas de una vez
                        break;
                    }
                }

                //Recorremos cada lista de herramental e imprimimos el código y la descripción general
                foreach (var herramental in operacion.ListaHerramentales)
                {
                    //Obtenemos los datos de cada herramental
                    string CodigoHerramental = herramental.Codigo + " ";
                    string DescripcionHerramental = herramental.DescripcionGeneral + " ";
                    string CantidadHerramental = herramental.clasificacionHerramental.CantidadUtilizar.ToString();

                    //Imprimimos los datos de cada herramental
                    gfx.DrawString(CodigoHerramental, TextoHerramentales, XBrushes.Black, new XRect(margenizq, Comenzar, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);
                    gfx.DrawString(DescripcionHerramental, TextoHerramentales, XBrushes.Black, new XRect(200, Comenzar, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);
                    gfx.DrawString(CantidadHerramental, TextoHerramentales, XBrushes.Black, new XRect(500, Comenzar, pag.Width.Point, pag.Height.Point), XStringFormats.TopLeft);

                    Comenzar += 10;
                    //Mandamos llamar el método para verificar que no se necesiten agregar mas hojas
                    Comenzar = ControlDePaginas(Comenzar, TextoEncabezado);
                }

                //Asignamos el valor de comenzar para llevar un control de los margenes
                margensup = Comenzar;
            }
        }

        /// <summary>
        /// Método para llevar el control de las hojas que se van agregando al documento PDF.
        /// </summary>
        private static int ControlDePaginas(int margensup ,XFont Encabezado)
        {
            if (margensup >= 765)
            {
                // se inicializan los valores para escribir sobre esa hoja nueva         
                pag = pdf.AddPage();
                pag.Size = PageSize.A4;
                gfx = XGraphics.FromPdfPage(pag);
                margensup = 65;

                //aumentamos el valor de la hoja
                PaginaActual++;                

                //Mandamos llamar el método para que se imprima el encabezado en la nueva hoja
                EncabezadoPagina(gfx, pag, Encabezado);
                //Mandamos llamar el método para que se imprima el pie de pagina en la nueva hoja
                PiePagina(gfx, pag, Encabezado);
            }
            return margensup;
        }
    }
        #endregion
}