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
    /// Lógica de interacción para ControlCutterCamTurn.xaml
    /// </summary>
    public partial class ControlCutterCamTurn : UserControl, IControlTooling
    {
        public ControlCutterCamTurn()
        {
            InitializeComponent();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();
            Propiedad dimension = new Propiedad();           

            obj.Codigo = codigo;
            obj.Plano = plano.Text;
            obj.DescripcionGeneral = desc.Text;
            dimension.Valor = double.Parse(dim.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.Propiedades.Add(dimension);

            return DataManager.SetCutterCamTurn(obj);
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(dim.Text) || !string.IsNullOrEmpty(plano.Text) || !string.IsNullOrEmpty(desc.Text) || !string.IsNullOrWhiteSpace(desc.Text))
                return true;
            else
                return false;
               
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public bool ValidaRangos()
        {
            return true;
        }
    }
}