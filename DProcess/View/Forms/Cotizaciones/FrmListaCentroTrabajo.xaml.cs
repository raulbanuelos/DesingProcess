﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

namespace View.Forms.Cotizaciones
{
    /// <summary>
    /// Lógica de interacción para FrmListaCentroTrabajo.xaml
    /// </summary>
    public partial class FrmListaCentroTrabajo : MahApps.Metro.Controls.MetroWindow
    {
        public FrmListaCentroTrabajo()
        {
            InitializeComponent();
        }
        //Se ejecuta cuando se da click en el botón
        void Button_Click(object sender, RoutedEventArgs e)
        {
            //Acepta el Dialog y retorna el DialogResults
            this.DialogResult = true;
        }

    }
}