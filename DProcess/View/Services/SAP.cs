using Model;
using System;
using System.IO;
using System.Windows.Forms;

namespace View.Services
{
    public class SAP
    {
        #region Properties
        public Anillo Componente { get; set; }
        public string Path { get; set; }
        public int noOperacionExternas { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Contructor default.
        /// </summary>
        public SAP()
        {

        }
        #endregion

        #region Methods

        /// <summary>
        /// Método que genera un archivo de excel con la hoja de ruta de un componente, con el formato para que sea cargado en SAP.
        /// </summary>
        /// <param name="componente"></param>
        public void ExportSAPRoute(Anillo componente)
        {
            Componente = componente;

            Path = definePath();

            if (!string.IsNullOrEmpty(Path))
            {
                StreamWriter osw = new StreamWriter(Path);

                deleteExternOperation();

                osw.WriteLine(sap_1());             //Routing Header
                osw.WriteLine(sap_2());             //Routing Material Allocation
                osw.WriteLine(sap_3());             //Routing Operations
                if (hasTooling())
                    osw.WriteLine(sap_4());         //Routing PRT
                osw.WriteLine(sap_5());             //Routing Text Allocation
                osw.WriteLine(sap_6());             //Routing Text
                if (hasRawMaterial())
                    osw.WriteLine(sap_7());         //Routing Component Allocation

                osw.WriteLine(sap_9());             //BOM Header

                if (hasRawMaterial())
                    osw.WriteLine(sap_0());        //BOM Item

                osw.Flush();
                osw.Close();
            }
        }

        private string sap_0()
        {
            string text = string.Empty;

            int i = 0;
            int j = 0;
            bool ban = false;
            while (i < Componente.Operaciones.Count)
            {
                while (j < Componente.Operaciones[i].ListaMateriaPrima.Count)
                {
                    ban = true;
                    text += "0";                                                                                    //Field Identifier  --> 0
                    text += addZero("1", 5);                                                                        //Generated Id number that will be used to link subsequent routing file lines together
                    text += addZero(Componente.Operaciones[i].NoOperacion.ToString(), 4);                           //Item Number
                    text += "L";                                                                                    //Item Category
                    text += addSpace(Componente.Operaciones[i].ListaMateriaPrima[j].Codigo, 18);                    //BOM Component
                    text += addSpace(Componente.Operaciones[i].ListaMateriaPrima[j].Cantidad.ToString(), 18);       //Component Quantity
                    text += addSpace(Componente.Operaciones[i].ListaMateriaPrima[j].Measurement, 3);                //Component Unit of Measurement
                    text += "X";
                    text += "X";
                    text += "X";

                    j++;
                    if (j < Componente.Operaciones[i].ListaMateriaPrima.Count)
                        text += Environment.NewLine;
                }

                j = 0;
                i++;
                if (i < Componente.Operaciones.Count && Componente.Operaciones[i].ListaMateriaPrima.Count > 0)
                {
                    if (!ban)
                        ban = true;
                    else
                        text += Environment.NewLine;
                }
            }

            return text;
        }

        private string sap_9()
        {
            string text = string.Empty;
            text = "9";                                 //Field Identifier  --> 7
            text += addZero("1", 5);                    //Numero Subsecuente (Revisar como se va a generar)
            text += "3201";                             //Numero de planta
            text += addSpace(Componente.Codigo, 18);    //Material Number ?
            text += "1";                                //BOM Usage
            text += addSpace("1000", 17);               //Base Quantity
            text += addSpace("EA", 3);                  //Base Unit of Measurement
            
            return text;
        }

        private string sap_7()
        {
            string text = string.Empty;
            int i = 0;
            int j = 0;
            bool ban = false;
            while (i < Componente.Operaciones.Count)
            {
                while (j < Componente.Operaciones[i].ListaMateriaPrima.Count)
                {
                    ban = true;                                                                                     //Field Identifier  --> 7
                    text += "7";                                                                                    //Generated Id number that will be used to link subsequent routing file lines together
                    text += addZero("1", 5);
                    text += addZero(Componente.Operaciones[i].NoOperacion.ToString(), 4);                           //Activity (From Routing Operation)
                    text += addSpace(Componente.Operaciones[i].ListaMateriaPrima[j].Cantidad.ToString(), 16);       //Component Quantity ?
                    text += addSpace(Componente.Operaciones[i].ListaMateriaPrima[j].Measurement.ToString(), 3);     //Component Unit of Measurement
                    text += " ";                                                                                    //Backflush Flag
                    text += "3201";                                                                                 //Número de planta
                    text += addSpace(Componente.Operaciones[i].ListaMateriaPrima[j].Codigo, 18);                    //Component Material

                    j++;
                    if (j < Componente.Operaciones[i].ListaMateriaPrima.Count)
                        text += Environment.NewLine;
                }

                j = 0;
                i++;
                if (i < Componente.Operaciones.Count && Componente.Operaciones[i].ListaMateriaPrima.Count > 0)
                {
                    if (!ban)
                        ban = true;
                    else
                        text += Environment.NewLine;
                }
            }
            return text;
        }

        private string sap_6()
        {
            string text = string.Empty;

            string encabezado = "6";                                //Field Identifier  --> 6
            encabezado += addZero("1", 5);                          //Generated Id number that will be used to link subsequent routing file lines together
            int j = 0;
            int i = 0;
            while (j < Componente.Operaciones.Count)
            {
                string[] vector = Componente.Operaciones[j].TextoSyteline.Split('\n');
                while (i < vector.Length)
                {
                    text += encabezado + vector[i];
                    i++;
                    if (i < vector.Length)
                        text += Environment.NewLine;
                }
                i = 0;
                j++;
                if (j < Componente.Operaciones.Count)
                    text += Environment.NewLine;
            }

            //CARATULA
            string[] vectorCaratula = Componente.Caratula.Split('\n');
            i = 0;
            text += Environment.NewLine;
            while (i < vectorCaratula.Length)
            {
                text += encabezado + vectorCaratula[i];
                i++;
                if (i < vectorCaratula.Length)
                    text += Environment.NewLine;
            }

            return text;
        }

        private string sap_5()
        {
            string text = string.Empty;

            int i = 0;
            int inicio = 0;
            int final = 0;
            int numEspacios = 0;

            while (i < Componente.Operaciones.Count)
            {
                inicio = final + 1;
                numEspacios = countRenglones(Componente.Operaciones[i].TextoSyteline);
                final = inicio + numEspacios - 1;
                text += "5";                                                                //Field Identifier  --> 5
                text += addZero("1", 5);                                                    //Generated Id number that will be used to link subsequent routing file lines together
                text += addZero(Componente.Operaciones[i].NoOperacion.ToString(), 4);       //Numero operacion
                text += addZero(inicio.ToString(), 8);                                      //Line From
                text += addZero(final.ToString(), 9);                                       //Line To

                i++;
                if (i < Componente.Operaciones.Count)
                {
                    text += Environment.NewLine;
                }
            }

            numEspacios = 0;
            numEspacios = countRenglones(Componente.Caratula);
            inicio = final + 1;
            final = inicio + numEspacios - 1;
            text += Environment.NewLine;
            text += "8";
            text += addZero("1", 5);                                                        //Numero Subsecuente (Revisar como se va a generar)         
            text += addZero(inicio.ToString(), 8);                                          //Line From
            text += addZero(final.ToString(), 9);                                           //Line To

            return text;
        }
        
        private string sap_4()
        {
            string text = string.Empty;
            int i = 0;
            int j = 0;
            int itemCount = 0;
            bool bandera = false;
            while (i < Componente.Operaciones.Count)
            {
                while (j < Componente.Operaciones[i].ListaHerramentales.Count)
                {
                    text += "4";                                                                                //Field Identifier  --> 4
                    text += addZero("1", 5);                                                                    //Generated Id number that will be used to link subsequent routing file lines together
                    itemCount = j + 1;
                    text += addZero(itemCount.ToString(), 8);                                                   //PRT Item Count
                    text += addZero(Componente.Operaciones[i].NoOperacion.ToString(), 4);                       //Activity
                    text += "Z2  ";                                                                             //Control Key
                    text += addZero(Componente.Operaciones[i].ListaHerramentales[j].Cantidad.ToString(), 13);   //Quantity
                    text += "3201";                                                                             //Numero de planta.
                    text += addSpace(Componente.Operaciones[i].ListaHerramentales[j].Codigo, 18);               //Material Number

                    j++;
                    if (j < Componente.Operaciones[i].ListaHerramentales.Count)
                        text += Environment.NewLine;
                }
                j = 0;

                i++;
                if (i < Componente.Operaciones.Count && Componente.Operaciones[i].ListaHerramentales.Count > 0 && bandera)
                    text += Environment.NewLine;
            }


            return text;
        }

        private string sap_3()
        {
            string text = string.Empty;
            int i = 0;
            int aux, aux2;
            while (i < Componente.Operaciones.Count)
            {
                text += "3";                                                            //Field Identifier  --> 3
                text += addZero("1", 5);                                                //Generated Id number that will be used to link subsequent routing file lines together
                text += addZero(Componente.Operaciones[i].NoOperacion.ToString(), 4);   //Activity
                text += addSpace(Componente.Operaciones[i].ControlKey, 4);              //ControlKey
                text += "3201";                                                         //Numero de planta
                text += addSpace(Componente.Operaciones[i].NombreOperacion, 40);        //Operation Description
                text += addSpace("1", 5);                                               //Denominator
                text += addSpace("1", 5);                                               //Nominator
                text += "X";                                                            //Cost Relevant Indicator
                text += addSpace("10000", 16);                                          //Base Quantity
                text += "X";                                                            //Individual Splitting Required ?
                text += addZero("1", 3);                                                //Max Number of Splits ?
                text += Componente.Operaciones[i].CentroTrabajo;                        //work center

                aux2 = Componente.Operaciones[i].CentroTrabajo.Length - 3;
                //Setup Activity Time
                aux = Componente.Operaciones[i].TiempoSetup.ToString("0.000").Length;
                while (aux < (16 - aux2))
                {
                    text += " ";
                    aux += 1;
                }
                text += Componente.Operaciones[i].TiempoSetup.ToString("0.000");

                //Machine Activity Time
                aux = Componente.Operaciones[i].TiempoMachine.ToString("0.000").Length;
                while (aux < 13)
                {
                    text += " ";
                    aux += 1;
                }
                text += Componente.Operaciones[i].TiempoMachine.ToString("0.000");

                //Labor Activity Time
                aux = Componente.Operaciones[i].TiempoLabor.ToString("0.000").Length;
                while (aux < 13)
                {
                    text += " ";
                    aux += 1;
                }
                text += Componente.Operaciones[i].TiempoLabor.ToString("0.000");

                i++;
                if (i < Componente.Operaciones.Count)
                    text += Environment.NewLine;
            }

            return text;
        }

        private string sap_2()
        {
            string text = string.Empty;
            text = "2";                                 //Field Identifier  --> 2
            text += addZero("1", 5);                    //Generated Id number that will be used to link subsequent routing file lines together
            text += "3201";                             //Numero de Planta
            text += addSpace(Componente.Codigo, 18);    //Material Number of the routing
            return text;
        }

        private string sap_1()
        {
            string text = string.Empty;
            text = "1";                                             //Field Identifier  --> 1
            text += addZero("1",5);                                 //Generated Id number that will be used to link subsequent routing file lines together
            text += "3201";                                         //Numero de Planta
            text += addSpace("EA", 3);                              //Task Unit of Measurement
            text += addSpace(Componente.DescripcionGeneral, 40);    //Routing Header Description
            return text;
        }

        /// <summary>
        /// Función que indica si un componente tiene al menos un herramiental asignado.
        /// </summary>
        /// <returns></returns>
        private bool hasTooling()
        {
            foreach (var item in Componente.Operaciones)
            {
                foreach (var item2 in item.ListaHerramentales)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Función que indica si un componente tiene alguna materia prima asignada.
        /// </summary>
        /// <returns></returns>
        private bool hasRawMaterial()
        {
            foreach (var item in Componente.Operaciones)
            {
                foreach (var item2 in item.ListaMateriaPrima)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Función que agrega ceros a la izquierda.
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="longitud"></param>
        /// <returns></returns>
        private string addZero(string texto,int longitud)
        {
            string newText = string.Empty;
            int tamano = texto.Length;
            int resta = longitud - tamano;
            
            if (resta < 0)
            {
                newText = texto.Substring(0, longitud);
                return newText;
            }else
            {
                int i = 0;
                string agregado = string.Empty;
                while (i < resta)
                {
                    agregado += "0";
                    i += 1;
                }
                newText = agregado + texto;
                return newText;
            }
        }

        /// <summary>
        /// Función que agrega espacios a la derecha.
        /// </summary>
        /// <param name="texto"></param>
        /// <param name="longitud"></param>
        /// <returns></returns>
        private string addSpace(string texto, int longitud)
        {
            string newText = string.Empty;
            int tamano = texto.Length;
            int resta = longitud - tamano;
            if (resta < 0)
            {
                newText = texto.Substring(0, longitud);
                return newText;
            }else
            {
                int i = 0;
                string agregado = string.Empty;
                while (i < resta)
                {
                    agregado += " ";
                    i += 1;
                }
                newText = texto + agregado;
                return newText;
            }
        }

        /// <summary>
        /// Función que devuelve el número de saltos de linea que contiene una cadena.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        private int countRenglones(string texto)
        {
            string[] arreglo = texto.Split('\n');

            return arreglo.Length;
        }

        /// <summary>
        /// Método que elimina las operaciones externas del componente.
        /// </summary>
        private void deleteExternOperation()
        {
            string txtOperaciones = string.Empty;
            int t = 0;
            int c = 0;
            while (c < Componente.Operaciones.Count)
            {
                if (Componente.Operaciones[c].ControlKey == "MA52")
                {
                    txtOperaciones = txtOperaciones + Componente.Operaciones[c].NombreOperacion;
                    t += 1;
                    c -= 1;
                }
                c += 1;
            }

            noOperacionExternas = t;
        }

        /// <summary>
        /// Método que define el path donde se guardará el archivo.
        /// </summary>
        /// <returns></returns>
        private string definePath()
        {
            string path = string.Empty;
            FolderBrowserDialog dialog = new FolderBrowserDialog();

            DialogResult dresult = dialog.ShowDialog();

            if (dresult == DialogResult.OK)
            {
                path = dialog.SelectedPath.ToString() + @"\" + Componente.Codigo.Trim() + "_uploadToSap.txt";
            }

            return path;
        }
        #endregion
    }
}
