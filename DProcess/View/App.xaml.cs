using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TableDependency.SqlClient;
using View.Resources;

namespace View
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            //Establecemos el idioma por default es el ingles.
            Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("en-US");
            
            //Idioma en español
            //Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-MX");
            
        }
        
        protected override void OnExit(ExitEventArgs e)
        {
            base.OnExit(e);
        }
    }
}
