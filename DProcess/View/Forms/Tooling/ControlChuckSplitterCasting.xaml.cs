﻿using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Forms.Tooling
{
    /// <summary>
    /// Lógica de interacción para ControlChuckSplitterCasting.xaml
    /// </summary>
    public partial class ControlChuckSplitterCasting : UserControl, IControlTooling
    {
        string Codigo;
        Herramental obj;
        public ControlChuckSplitterCasting()
        {
            InitializeComponent();
            obj = new Herramental();
        }

        /// <summary>
        /// Método que guarda la información registrada.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int Guardar(string codigo)
        {
            //Declaración del objeto y sus propiedades.
            Herramental obj = new Herramental();          
            Propiedad DMin = new Propiedad();
            Propiedad DMax = new Propiedad();
            PropiedadCadena Pensamble = new PropiedadCadena();

            //Asignamos los valores.
            obj.Codigo = codigo;
            DMin.Valor= double.Parse(dimMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            DMax.Valor= double.Parse(dimMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            Pensamble.Valor = ensamble.Text;
            //Agregamos las propiedades.
            obj.Propiedades.Add(DMin);
            obj.Propiedades.Add(DMax);
            obj.PropiedadesCadena.Add(Pensamble);

            //Mandamos a llamar al método para insertar el objeto y retornamos el resultado.
            return DataManager.SetChuckSplitter(obj);
        }

        /// <summary>
        /// Método que inicializa los componentes que se muestran en pantalla.
        /// </summary>
        public void Inicializa()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Método que valída si los campos se encuentran vacíos.
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            //Si los campos no están vacíos, retorna true.
            if (!string.IsNullOrEmpty(dimMax.Text) & !string.IsNullOrEmpty(dimMin.Text) & !string.IsNullOrEmpty(ensamble.Text))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Método que valída los rangos, si el diamtro min es menor al diametro máximo, regresa true.
        /// </summary>
        /// <returns></returns>
        public bool ValidaRangos()
        {
            double dmin, dmax;
            dmin = double.Parse(dimMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            dmax = double.Parse(dimMax.Text, CultureInfo.InvariantCulture.NumberFormat);

            //si el min es menor al máximo.
            if (dmin < dmax)
                return true;
            else
                return false;
        }

        public int Update()
        {
            Herramental herram = new Herramental();
            Propiedad DMin = new Propiedad();
            Propiedad DMax = new Propiedad();
            PropiedadCadena Pensamble = new PropiedadCadena();

            //Asignamos los valores.
            herram.Codigo = Codigo;
            herram.idHerramental = obj.idHerramental;
            DMin.Valor = double.Parse(dimMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            DMax.Valor = double.Parse(dimMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            Pensamble.Valor = ensamble.Text;
            //Agregamos las propiedades.
            herram.Propiedades.Add(DMin);
            herram.Propiedades.Add(DMax);
            herram.PropiedadesCadena.Add(Pensamble);

            return DataManager.UpdateChuckSplitter(herram);
        }

        public int Delete()
        {
            return DataManager.DeleteChuckSplitter(obj.idHerramental);
        }

        public void InicializaCampos(string codigoHerramental)
        {
            obj = DataManager.GetInfoChuckSplitter(codigoHerramental);
            Codigo = obj.Codigo;
            dimMin.Text = Convert.ToString(obj.Propiedades[0].Valor);
            dimMax.Text = Convert.ToString(obj.Propiedades[1].Valor);
            ensamble.Text = obj.PropiedadesCadena[0].Valor;
        }

        /// <summary>
        /// Método que válida la entrada del textbox sólo sea número flotante.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            string text = textBox.Text;
            Regex regex = new Regex("[^0-9.]+");
            //Valída si el caracter es un número o punto, si el texto ya contiene un punto no permite escribir otro punto.
            if (!regex.IsMatch(e.Text))
            {
                //Si el objeto recibido es un punto
                if (e.Text.Equals("."))
                {
                    //Si el texto contiene un punto, no se escribe el texto de entrada.
                    if (text.Contains("."))
                        e.Handled = true;
                    else
                        e.Handled = false;
                }
                else
                {
                    //Si no es un punto pero coicide con las expresión. 
                    e.Handled = regex.IsMatch(e.Text);
                }
            }
            else
            {
                //Si no es un número o punto no escribe el caracter.
                e.Handled = true;
            }
        }

        /// <summary>
        /// Método que valida si la tecla recibida es un espacio.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyValidation(object sender, KeyEventArgs e)
        {
            //si la tecla presionada es un espacio no escribe el caracter.
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}
