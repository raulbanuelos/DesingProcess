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
    /// Lógica de interacción para ControlShimOTCS.xaml
    /// </summary>
    public partial class ControlShimOTCS : UserControl, IControlTooling
    {
        public ControlShimOTCS()
        {
            InitializeComponent();
        }

        public int Guardar(string codigo)
        {
            Coil obj = new Coil();

            obj.codigo = codigo;
            obj.code = code.Text;
            obj.dimA = double.Parse(dimA.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_min = double.Parse(WMin.Text, CultureInfo.InvariantCulture.NumberFormat);
            obj.wire_width_max = double.Parse(WMax.Text, CultureInfo.InvariantCulture.NumberFormat);

            return DataManager.SetSHIM_OF_THE_CUT_SYSTEM(obj);
        }

        public void Inicializa()
        {
            InitializeComponent();
        }

        public bool ValidaError()
        {
            if (!string.IsNullOrEmpty(code.Text) & !string.IsNullOrEmpty(dimA.Text) & !string.IsNullOrEmpty(WMin.Text) & !string.IsNullOrEmpty(WMax.Text))
                return true;
            else
                return false;
        }

        public bool ValidaRangos()
        {
            return false;
        }

        private void NumberValidation(object sender, TextCompositionEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(e.Text))
            {
                Regex regex = new Regex("[^0-9.]+");
                e.Handled = regex.IsMatch(e.Text);
            }
        }
    }
}