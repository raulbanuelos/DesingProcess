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
    /// Lógica de interacción para CutterSpacerS.xaml
    /// </summary>
    public partial class CutterSpacerS : UserControl, IControlTooling
    {
        Herramental obj;
        public CutterSpacerS()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Método que guarda la información
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();
            Propiedad a = new Propiedad();
            Propiedad b = new Propiedad();

            obj.Codigo = codigo;
            obj.Plano = plano.Text;
            a.Valor= double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            b.Valor = double.Parse(dimB.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.Propiedades.Add(a);
            obj.Propiedades.Add(b);

            return DataManager.SetCutterSpacerS(obj);
            
        }

        /// <summary>
        /// Método que inicializa los componentes
        /// </summary>
        public void Inicializa()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Método que valida si los campos no estan vacíos.
        /// </summary>
        /// <returns></returns>
        public bool ValidaError()
        {
            if(!string.IsNullOrEmpty(plano.Text) & !string.IsNullOrEmpty(dimA.Text) & !string.IsNullOrEmpty(dimB.Text))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Método que valida  los rangos.
        /// </summary>
        /// <returns></returns>
        public bool ValidaRangos()
        {
            return true;
        }

        public int Update()
        {
            Herramental herram = new Herramental();
            Propiedad a = new Propiedad();
            Propiedad b = new Propiedad();

            herram.Codigo = obj.Codigo;
            herram.idHerramental = obj.idHerramental;
            herram.Plano = plano.Text;
            a.Valor = double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            b.Valor = double.Parse(dimB.Text, CultureInfo.InvariantCulture.NumberFormat);
            herram.Propiedades.Add(a);
            herram.Propiedades.Add(b);

            return DataManager.UpdateCutterSpacerS(herram);
        }

        public int Delete()
        {
            return DataManager.DeleteCutterSpacerS(obj.idHerramental);
        }

        public void InicializaCampos(string codigoHerramental)
        {
            obj = DataManager.GetInfoCutterSpacer(codigoHerramental);
            dimA.Text = Convert.ToString(obj.Propiedades[0].Valor);
            dimB.Text = Convert.ToString(obj.Propiedades[1].Valor);
        }

        /// <summary>
        /// Método que válida la entrada del textbox sólo sea número flotante
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
                //Si el objeto recibido es un punto
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
                //Si no es un número o punto no escribe el caracter
                e.Handled = true;
            }
        }

        /// <summary>
        /// Método que valida si la tecla recibida es un espacio
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
