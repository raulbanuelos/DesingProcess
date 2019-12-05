using System;
using System.Windows;
using System.Windows.Controls;

namespace View.Forms.UserControls
{
    /// <summary>
    /// Lógica de interacción para NumericEntry.xaml
    /// </summary>
    public partial class NumericEntry : UserControl
    {
        public NumericEntry()
        {
            
            InitializeComponent();
            
            myStoryBoard.Height = 0;
        }

        private void Storyboard_Completed(object sender, System.EventArgs e)
        {
            this.myStoryBoard.Height = 0;
        }

        private void txtNumber_GotFocus(object sender, RoutedEventArgs e)
        {
            if (txtNumber.Text == "0.00000")
                txtNumber.Text = "";
        }

        private void txtNumber_LostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(txtNumber.Text))
                txtNumber.Text = "0.00000";
        }
    }
}
