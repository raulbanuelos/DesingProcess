using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace View.Services
{
    public class DialogService
    {
        /// <summary>
        /// Método que muestra un mensaje el la pantalla actual.
        /// </summary>
        /// <param name="title">Cadena que representa el título que contendrá el mensaje.</param>
        /// <param name="message">Cadena que representa el mensaje que mostrará el mensaje.</param>
        /// <returns></returns>
        public async Task SendMessage(string title, string message)
        {
            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            //Verificamos que la pantalla que obtuvimos sea diferente de nulo.
            if (window != null)

                //Ejecutamos el método para mostrar el mensaje en la pantalla actual.
                await window.ShowMessageAsync(title,message);
        }
    }
}
