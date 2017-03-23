/*
 * Created by SharpDevelop.
 * User: M0051722
 * Date: 13/03/2017
 * Time: 10:45 a.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using View.UserControls;

namespace View.Forms.Routing
{
	/// <summary>
	/// Interaction logic for PRouting.xaml
	/// </summary>
	public partial class PRouting : Page
	{
		public PRouting()
		{
			InitializeComponent();

            //DProcessPropiedad obj = new DProcessPropiedad();

            //myGrid.Children.Add(obj);

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            DataManager.GetClasificacionHerramental();
        }
    }
}