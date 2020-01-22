﻿using MahApps.Metro.Controls;
using System;
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
using System.Windows.Shapes;

namespace View.Forms.User
{
    /// <summary>
    /// Lógica de interacción para ModificarContrasena.xaml
    /// </summary>
    public partial class ModificarContrasena : MetroWindow
    {
        public ModificarContrasena()
        {
            InitializeComponent();
            txt_password.Focus();
        }
    }
}