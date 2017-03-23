using System.Collections.ObjectModel;
using Model;
using Model.Interfaces;

namespace View.Services
{
    public class Router
    {
        /// <summary>
		/// Método que calcula las operaciones para el material Hierro Gris.
		/// </summary>
		/// <param name="elAnillo">Plano del anillo esperado.</param>
		/// <returns>Colección que representa las operaciones que se necesitan para fabricar el anillo.</returns>
		public static ObservableCollection<IOperacion> CalcularHierroGris(Anillo elAnillo)
        {
            Anillo _ElAnillo;
            _ElAnillo = elAnillo;


            //Declaramos una lista observable la cual guardará las operaciones y será la que retornemos en el método.
            ObservableCollection<IOperacion> ListaOperaciones = new ObservableCollection<IOperacion>();



            //Retornamos la lista generada.
            return ListaOperaciones;
        }
    }
}
