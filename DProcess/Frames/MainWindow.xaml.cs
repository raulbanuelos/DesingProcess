
using ControlzEx.Theming;
using Frames.ViewModels;
using System.Windows;

namespace Frames
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //ThemeManager.Current.ChangeTheme(this, "Light");

            DataContext = new MainViewModel();

        }
    }
}
