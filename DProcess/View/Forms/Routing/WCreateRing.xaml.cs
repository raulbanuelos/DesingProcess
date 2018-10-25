using MahApps.Metro.Controls;
using System.Windows;
using System.Windows.Controls;

namespace View.Forms.Routing
{
    /// <summary>
    /// Lógica de interacción para WCreateRing.xaml
    /// </summary>
    public partial class WCreateRing : MetroWindow
    {
        public WCreateRing()
        {
            InitializeComponent();
            
        }

        private void btnNext_Click(object sender, RoutedEventArgs e)
        {
            if (btnNext.Content.ToString() == "Finalizar")
                this.Close();
            else
            {
                int c = 0;
                foreach (TabItem item in tabControl.Items)
                {
                    if (item.IsSelected)
                        break;
                    c += 1;
                }
                c++;
                int i = 0;
                while (i <= c)
                {
                    if (i == c)
                        ((TabItem)tabControl.Items[i]).IsSelected = true;
                    i++;
                }

                if (c == (tabControl.Items.Count - 1))
                    btnNext.Content = "Finalizar";
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            int c = 0;
            foreach (TabItem item in tabControl.Items)
            {
                if (item.IsSelected)
                    break;
                c += 1;
            }

            c--;

            int i = 0;
            while (i <= c)
            {
                if (i == c)
                {
                    ((TabItem)tabControl.Items[i]).IsSelected = true;
                    btnNext.Content = "Siguiente";
                }
                i++;
            }
        }
    }
}
