/*
 * Created by SharpDevelop.
 * User: M0051722
 * Date: 13/03/2017
 * Time: 10:45 a.m.
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System.Windows.Controls;

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
        }

        private void txtOvalityMin_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtOvalityMin.Text == "0.00000")
                txtOvalityMin.Text = "";
        }

        private void txtOvalityMin_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtOvalityMin.Text))
                txtOvalityMin.Text = "0.00000";
        }

        private void txtOvalityMax_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtOvalityMax.Text == "0.00000")
                txtOvalityMax.Text = "";
        }

        private void txtOvalityMax_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtOvalityMax.Text))
                txtOvalityMax.Text = "0.00000";
        }

        private void txtTension_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtTension.Text == "0.00000")
                txtTension.Text = "";
        }
    

        private void txtTension_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTension.Text))
                txtTension.Text = "0.00000";
        }

        private void txtTensionTol_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtTensionTol.Text == "0.00000")
                txtTensionTol.Text = "";
        }

        private void txtTensionTol_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtTensionTol.Text))
                txtTensionTol.Text = "0.00000";
        }

        private void txtFreeGap_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtFreeGap.Text == "0.00000")
                txtFreeGap.Text = "";
        }

        private void txtFreeGap_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFreeGap.Text))
                txtFreeGap.Text = "0.00000";
        }

        private void txtMass_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtMass.Text == "0.00000")
                txtMass.Text = "";
        }

        private void txtMass_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMass.Text))
                txtMass.Text = "0.00000";
        }

        private void txtHardnessMin_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtHardnessMin.Text == "0.00000")
                txtHardnessMin.Text = "";
        }

        private void txtHardnessMin_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtHardnessMin.Text))
                txtHardnessMin.Text = "0.00000";
        }

        private void txtHardnessMax_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtHardnessMax.Text == "0.00000")
                txtHardnessMax.Text = "";
        }

        private void txtHardnessMax_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtHardnessMax.Text))
                txtHardnessMax.Text = "0.00000";
        }

        private void txtD1_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtD1.Text == "0.00000")
                txtD1.Text = "";
        }

        private void txtD1_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtD1.Text))
                txtD1.Text = "0.00000";
        }

        private void txtH1_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtH1.Text == "0.00000")
                txtH1.Text = "";
        }

        private void txtH1_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtH1.Text))
                txtH1.Text = "0.00000";
        }

        private void txtD1_2_GotFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (txtD1_2.Text == "0.00000")
                txtD1_2.Text = "";
        }

        private void txtD1_2_LostFocus(object sender, System.Windows.RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtD1_2.Text))
                txtD1_2.Text = "0.00000";
        }
    }
}