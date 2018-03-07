using System.Collections.ObjectModel;
using Model;
using Model.Interfaces;
using View.Services.Operaciones.Gasolina;
using View.Services.Operaciones.Gasolina.PreMaquinado;
using View.Services.Operaciones.Gasolina.Maquinado;
using View.Services.Operaciones.Gasolina.RectificadosFinos;
using View.Services.Operaciones.Gasolina.Miscelaneos;
using View.Services.Operaciones.Gasolina.Recubrimientos;
using View.Services.Operaciones.Gasolina.Inspeccion;

namespace View.Services
{
    public class Router
    {
        /// <summary>
        /// Método que calculo las operaciones para generar una placa modelo
        /// </summary>
        /// <param name="elAnillo"></param>
        /// <returns></returns>
        public static ObservableCollection<IOperacion> CalcularPatternHierroGris(Anillo elAnillo)
        {
            ObservableCollection<IOperacion> ListaOperaciones = new ObservableCollection<IOperacion>();



            return ListaOperaciones;
        }

        /// <summary>
		/// Método que calcula las operaciones para el material Hierro Gris.
		/// </summary>
		/// <param name="elAnillo">Plano del anillo esperado.</param>
		/// <returns>Colección que representa las operaciones que se necesitan para fabricar el anillo.</returns>
		public static ObservableCollection<IOperacion> CalcularHierroGris(Anillo elAnillo)
        {
            Anillo _ElAnillo;
            _ElAnillo = elAnillo;

            //Obtenemos algunos valores que utilizaremos en el método.
            double h1 = Module.GetValorPropiedad("h1", elAnillo.PerfilLateral.Propiedades);
            double h1Tol = Module.GetValorPropiedad("h1 Tol", elAnillo.PerfilLateral.Propiedades);
            double h1Min = h1 - h1Tol;
            double h1Max = h1 + h1Tol;
            double rangoWidth = h1Max - h1Min;

            double s1Min = Module.GetValorPropiedadMin("s1", elAnillo.PerfilPuntas.Propiedades,true);
            double s1Max = Module.GetValorPropiedadMax("s1", elAnillo.PerfilPuntas.Propiedades, true);
            double rangoGap = s1Max - s1Min;

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
                ListaOperaciones.Add(new CTB(elAnillo));
            else
            {
                ListaOperaciones.Add(new CamTurn(elAnillo));
                ListaOperaciones.Add(new BatesBore(elAnillo));
                ListaOperaciones.Add(new FinishMill(elAnillo));
            }

            if (Module.GetValorPropiedadBool("Engrave",elAnillo.PerfilLateral.PropiedadesBool))
                ListaOperaciones.Add(new Engrave(elAnillo));

            if (Module.GetValorPropiedadBool("HookLap",elAnillo.PerfilPuntas.PropiedadesBool))
            {
                ListaOperaciones.Add(new MillHookLapAutMac(elAnillo));
                ListaOperaciones.Add(new HookLapAssembly(elAnillo));
            }

            if (Module.HasPropiedad("CromoMin",elAnillo.PerfilOD.Propiedades) || Module.HasPropiedad("MolyMin",elAnillo.PerfilOD.Propiedades))
            {
                ListaOperaciones.Add(new AutoFinTurn(elAnillo));
                ListaOperaciones.Add(new DegreaseRings(elAnillo));
                
                if (Module.HasPropiedad("CromoMin", elAnillo.PerfilOD.Propiedades)) // <--Si tiene Cromo
                {
                    ListaOperaciones.Add(new ChromeOD(elAnillo));
                    ListaOperaciones.Add(new GapSizer(elAnillo));
                    ListaOperaciones.Add(new Lapping(elAnillo));
                    ListaOperaciones.Add(new Diskus(elAnillo));
                    ListaOperaciones.Add(new DegreaseRings(elAnillo));
                    if (Module.HasPropiedad("NoVentilas",elAnillo.PerfilOD.Propiedades))
                    {
                        ListaOperaciones.Add(new GrindChannelNorton(elAnillo));
                        if (Module.HasPropiedad("IDGroove", elAnillo.PerfilID.Propiedades))
                        {
                            ListaOperaciones.Add(new AutoFinTurn(elAnillo));
                            ListaOperaciones.Add(new DegreaseRings(elAnillo));
                        }
                    }

                    ListaOperaciones.Add(new Duplex(elAnillo));
                    ListaOperaciones.Add(new Pick(elAnillo));
                    ListaOperaciones.Add(new IDBrush(elAnillo));
                }
                else // <--Si tiene Moly
                {
                    ListaOperaciones.Add(new Molibdenum(elAnillo));
                    ListaOperaciones.Add(new Diskus(elAnillo));
                    ListaOperaciones.Add(new DegreaseRings(elAnillo));
                    ListaOperaciones.Add(new GapSizer(elAnillo));
                    ListaOperaciones.Add(new Lapping(elAnillo));
                    ListaOperaciones.Add(new GapSizer(elAnillo));
                    ListaOperaciones.Add(new Diskus(elAnillo));
                    ListaOperaciones.Add(new DegreaseRings(elAnillo));
                    if (Module.HasPropiedad("TaperLateral", elAnillo.PerfilLateral.Propiedades))
                    {
                        ListaOperaciones.Add(new TaperSide(elAnillo));
                        ListaOperaciones.Add(new DegreaseRings(elAnillo));
                    }
                    else if (Module.HasPropiedad("Bevel", elAnillo.PerfilID.Propiedades))
                    {
                        ListaOperaciones.Add(new AutoFinTurn(elAnillo));
                        ListaOperaciones.Add(new DegreaseRings(elAnillo));
                    }
                }

                if (Module.HasPropiedad("Scotchbrite",elAnillo.PerfilLateral.Propiedades))
                    ListaOperaciones.Add(new ScotchBrite(elAnillo));
            }
            else
            {
                if (rangoWidth <= 0.0006)
                {
                    ListaOperaciones.Add(new Diskus(elAnillo));
                    ListaOperaciones.Add(new DegreaseRings(elAnillo));
                }

                if (Module.HasPropiedad("NoVentilas",elAnillo.PerfilOD.Propiedades))
                {
                    ListaOperaciones.Add(new Duplex(elAnillo));
                    ListaOperaciones.Add(new Pick(elAnillo));
                    ListaOperaciones.Add(new IDBrush(elAnillo));
                }

                ListaOperaciones.Add(new AutoFinTurn(elAnillo));
                ListaOperaciones.Add(new DegreaseRings(elAnillo));
            }

            if (rangoGap <= 0.006)
                ListaOperaciones.Add(new GapSizer(elAnillo));

            if (Module.HasPropiedad("PistaLapeado",elAnillo.PerfilOD.Propiedades))
                ListaOperaciones.Add(new Lapping(elAnillo));

            if (!elAnillo.Treatment.Equals("Ninguno"))
                ListaOperaciones.Add(new Phosphate(elAnillo));

            if (Module.HasPropiedad("LasserEngrave",elAnillo.PerfilLateral.Propiedades))
                ListaOperaciones.Add(new LasserEngrave(elAnillo));

            ListaOperaciones.Add(new InspeccionFinal(elAnillo));

            if (rangoGap < 0.008)
                ListaOperaciones.Add(new AutoGap(elAnillo));

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
