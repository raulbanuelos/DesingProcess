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
    }
}
