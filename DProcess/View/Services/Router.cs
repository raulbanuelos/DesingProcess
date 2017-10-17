using System.Collections.ObjectModel;
using Model;
using Model.Interfaces;
using View.Services.Operaciones.Gasolina.PreMaquinado;
using View.Services.Operaciones.Gasolina;
using View.Services.Operaciones.Gasolina.Maquinado;

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

            //Agregamos las operaciones necesarias. Se sigue el diagrama de flujo del archivo de excel ubicado en resprutas\RrrrUUUUUULLL\Diagrama de flujo Router.xlsx
            ListaOperaciones.Add(new FirstRoughGrind(elAnillo));


            if (Module.GetValorPropiedadString("Proceso", elAnillo.PerfilOD.PropiedadesCadena) != "Sencillo")
            {
                ListaOperaciones.Add(new Splitter(elAnillo));
                ListaOperaciones.Add(new SecondRoughGrind(elAnillo));
            }
            ListaOperaciones.Add(new FinishGrind(elAnillo));
            ListaOperaciones.Add(new DegreaseRings(elAnillo));
            ListaOperaciones.Add(new VisualInspectPremGasoline(elAnillo));

            if (Module.HasPropiedad("CTB",elAnillo.PerfilID.Propiedades))
            {

            }
            else
            {
                ListaOperaciones.Add(new CamTurn(elAnillo));
            }

            //Asignamos el número de operación a cada operación. (Saltando de 10 en 10).
            int noOperacion = 0;
            foreach (IOperacion operacion in ListaOperaciones)
            {
                noOperacion += 10;
                operacion.NoOperacion = noOperacion;
            }

            //Retornamos la lista generada.
            return ListaOperaciones;
        }
    }
}
