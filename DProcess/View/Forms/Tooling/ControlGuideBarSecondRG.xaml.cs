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
    /// Lógica de interacción para ControlGuideBarSecondRG.xaml
    /// </summary>
    public partial class ControlGuideBarSecondRG : UserControl, IControlTooling
    {
        public ControlGuideBarSecondRG()
        {
            InitializeComponent();
        }

        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();
            Propiedad min = new Propiedad();
            Propiedad max = new Propiedad();
            Propiedad Pespesor = new Propiedad();
            obj.Codigo = codigo;
            min.Valor = double.Parse(Wmin.Text, CultureInfo.InvariantCulture.NumberFormat);
            max.Valor = double.Parse(WMax.Text, CultureInfo.InvariantCulture.NumberFormat);
            Pespesor.Valor = double.Parse(espesor.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.Propiedades.Add(min);
            obj.Propiedades.Add(max);
            obj.Propiedades.Add(Pespesor);

            return DataManager.SetSecondtRG(obj);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(Wmin.Text) & !string.IsNullOrWhiteSpace(Wmin.Text) & !string.IsNullOrEmpty(WMax.Text) & !string.IsNullOrEmpty(espesor.Text))
                return true;
            else
                return false;
        }

        public bool ValidaRangos()
        {
            double wmin, wmax;
            wmin = double.Parse(Wmin.Text, CultureInfo.InvariantCulture.NumberFormat);
            wmax = double.Parse(WMax.Text, CultureInfo.InvariantCulture.NumberFormat);

            if (wmin < wmax)
                return true;
            else
                return false;
        }

        public int Update()
        {
            return 0;
        }

        public int Delete()
        {
            return 0;
        }

        public void InicializaCampos(string codigoHerramental)
        {

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            string text = textBox.Text;
            Regex regex = new Regex("[^0-9.]+");
            //Valida si el caracter es un número o punto, si el texto ya contiene un punto no permite escribir otro punto
            if (!regex.IsMatch(e.Text))
            {
                if (e.Text.Equals("."))
                {
                    if (text.Contains("."))
                        e.Handled = true;
                    else
                        e.Handled = false;
                }
                else
                {
                    e.Handled = regex.IsMatch(e.Text);
                }
            }
            else
            {
                e.Handled = true;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void KeyValidation(object sender, KeyEventArgs e)
        {
            //si la tecla presionada es un espacio no la escribe
            if (e.Key == Key.Space)
                e.Handled = true;
        }
    }
}
