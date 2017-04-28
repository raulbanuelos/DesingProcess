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

        /// <summary>
        /// Método que muestra un mensaje el la pantalla actual.
        /// </summary>
        /// <param name="title"></param>
        /// <param name="message"></param>
        /// <param name="setting"></param>
        /// <param name="style"></param>
        /// <returns></returns>
        public async Task<MessageDialogResult> SendMessage(string title, string message, MetroDialogSettings setting, MessageDialogStyle style)
        {
            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

            //Comprobamos que la ventana sea diferente de nulo.
            if (window != null)
            {
                //Ejecutamos el método para mostrar el mensaje. El resultado lo guardamos en una variable local.
                MessageDialogResult result = await window.ShowMessageAsync(title, message, style, setting);

                //Retornamos el resultado.
                return result;
            }
            else
                //Si la ventana no fue encontrada, retornamos un valor Negative.
                return MessageDialogResult.Negative;
        }
    }
}
