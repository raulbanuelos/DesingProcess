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
    /// Lógica de interacción para ControlPusherSim.xaml
    /// </summary>
    public partial class ControlPusherSim : UserControl, IControlTooling
    {
        Herramental herram;

        public ControlPusherSim()
        {
            InitializeComponent();
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

        public int Guardar(string codigo)
        {
            Herramental obj = new Herramental();
            Propiedad D = new Propiedad();
    
            obj.Codigo = codigo;

            D.Valor = double.Parse(dimD.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.Propiedades.Add(D);
       
            return DataManager.SetPusherSim(obj);
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(dimD.Text))
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

        public void InicializaCampos(string codigoHerramental)
        {
            herram = DataManager.GetInfoPusherSim(codigoHerramental);       
            dimD.Text = Convert.ToString(herram.Propiedades[0].Valor);
        }

        public int Update()
        {
            Herramental obj = new Herramental();
            Propiedad D = new Propiedad();

            obj.idHerramental = herram.idHerramental;

            D.Valor = double.Parse(dimD.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.Propiedades.Add(D);

            return DataManager.UpdatePusherSim(obj);
        }

        public int Delete()
        {
            return DataManager.DeletePusherSim(herram.idHerramental);
        }
    }
}
