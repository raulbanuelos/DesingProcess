using Model;
using Model.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace View.Services.TiempoEstandar.Expansores
{
    public class CentroTrabajo745 : BaseCentroTrabajo, ICentroTrabajo
    {
        #region Atributos
        private double Espesor;
        private double carga;
        #endregion

        #region Propiedades

        #region Propiedades ICentroTrabajo

        public int NumeroOperacion
        {
            get;
            set;
        }
        public string NombreOperacion
        {
            get
            {
                return GetNombre(CentroTrabajo);
            }
        }

        public double TiempoSetup
        {
            get;
            set;
        }

        public double TiempoLabor
        {
            get;
            set;
        }

        public double TiempoMachine
        {
            get;
            set;
        }

        public double FactorLabor
        {
            get;
            set;
        }

        public string CentroTrabajo
        {
            get;
            set;
        }

        public List<Model.Propiedad> PropiedadesRequeridadas
        {
            get;
            set;
        }

        public List<Model.PropiedadBool> PropiedadesRequeridasBool
        {
            get;
            set;
        }

        public List<Model.PropiedadCadena> PropiedadesRequeridasCadena
        {
            get;
            set;
        }

        public List<Model.PropiedadOptional> PropiedadesRequeridasOpcionles
        {
            get;
            set;
        }

        public List<string> Alertas
        {
            get;
            set;
        }
        #endregion

        //Modelo del anillo
        private Anillo _anillo;
        #endregion

        #region Contructor
        public CentroTrabajo745()
        {
            CentroTrabajo = "745";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            _anillo = new Anillo();

            ObservableCollection<FO_Item> lista = new ObservableCollection<FO_Item>();

            lista.Add(new FO_Item { Nombre = "0.0787", Valor = 0.0787});
            lista.Add(new FO_Item { Nombre = "0.0984", Valor = 0.0984 });
            lista.Add(new FO_Item { Nombre = "0.1102", Valor = 0.1102 });
            lista.Add(new FO_Item { Nombre = "0.1181", Valor = 0.1181 });
            lista.Add(new FO_Item { Nombre = "0.125", Valor = 0.125 });
            lista.Add(new FO_Item { Nombre = "0.1378", Valor = 0.1378 });
            lista.Add(new FO_Item { Nombre = "0.1562", Valor = 0.1562 });
            lista.Add(new FO_Item { Nombre = "0.1563", Valor = 0.1563 });
            lista.Add(new FO_Item { Nombre = "0.1574", Valor = 0.1574 });
            lista.Add(new FO_Item { Nombre = "0.1575", Valor = 0.1575 });
            lista.Add(new FO_Item { Nombre = "0.1875", Valor = 0.1875 });
            lista.Add(new FO_Item { Nombre = "0.1968", Valor = 0.1968 });


            PropiedadOptional widthNominal = new PropiedadOptional {ListaOpcional = lista };
            PropiedadesRequeridasOpcionles.Add(widthNominal);


        }
        #endregion

        #region Métodos

        #region Métodos de ICentroTrabajo

        /// <summary>
        ///  Método que se utiliza cuando se calculan los tiempos estándar por fuera de las operaciones. (Cálculo individual, cotizaciones).
        /// </summary>
        /// <param name="ListaPropiedades">Lista de propiedades ingresadas por el usuario.</param>
        /// <param name="ListaPropiedadesBool">Lista de propiedades booleanos ingresados por el usuario.</param>
        /// <param name="ListaPropiedadesCadena">Lista de propiedades cadena ingresados por el usuario.</param>
        public void Calcular(List<Propiedad> ListaPropiedades, List<PropiedadBool> ListaPropiedadesBool, List<PropiedadCadena> ListaPropiedadesCadena, List<PropiedadOptional> ListaPropiedadesOpcionales)
        {
            //Obtenemos los valores de las propiedades requeridas.
            PropiedadesRequeridadas = Module.AsignarValoresPropiedades(PropiedadesRequeridadas, ListaPropiedades);
            PropiedadesRequeridasBool = Module.AsignarValoresPropiedadesBool(PropiedadesRequeridasBool, ListaPropiedadesBool);
            PropiedadesRequeridasCadena = Module.AsignarValoresPropiedadesCadena(PropiedadesRequeridasCadena, ListaPropiedadesCadena);
            PropiedadesRequeridasOpcionles = Module.AsignarValoresPropiedadesOpcionales(PropiedadesRequeridasOpcionles, ListaPropiedadesOpcionales);

            //Ejecutamos el método para calcular los tiempos estándar.
            Calcular();
        }

        /// <summary>
        /// Método que calcula los tiempos estándar a partir de un anillo.
        /// </summary>
        /// <param name="anillo"></param>
        public void Calcular(Anillo anillo)
        {
            //Obtenemos los valores de las propiedades requeridas.
            PropiedadesRequeridadas = Module.AsignarValoresPropiedades(PropiedadesRequeridadas, anillo);
            PropiedadesRequeridasBool = Module.AsignarValoresPropiedadesBool(PropiedadesRequeridasBool, anillo);
            PropiedadesRequeridasCadena = Module.AsignarValoresPropiedadesCadena(PropiedadesRequeridasCadena, anillo);
            _anillo = anillo;

            //Ejecutamos el método para calcular los tiempos estándar.
            Calcular();
        }

        /// <summary>
        /// Método que calcula los tiempos estándar.
        /// </summary>
        public void Calcular()
        {
            Espesor = Module.GetValorPropiedad("WidthNominal", PropiedadesRequeridadas);
            TiempoSetup = double.Parse(DataManager.GetTiempo(CentroTrabajo));
            if (Espesor.Equals(0.0787))
                carga = 25.184;
            else if (Espesor.Equals(0.0984))
                carga = 22.632;
            else if (Espesor.Equals(0.1102))
                carga = 24.244;
            else if (Espesor.Equals(0.1181))
                carga = 24.801;
            else if (Espesor.Equals(0.125))
                carga = 21.250;
            else if (Espesor.Equals(0.1378))
                carga = 22.048;
            else if (Espesor.Equals(0.1562))
                carga = 24.992;
            else if (Espesor.Equals(0.1563))
                carga = 25.008;
            else if (Espesor.Equals(0.1574))
                carga = 23.610;
            else if (Espesor.Equals(0.1575))
                carga = 22.050;
            else if (Espesor.Equals(0.1875))
                carga = 20.625;
            else if (Espesor.Equals(0.1968))
                carga = 21.648;
            TiempoMachine = (((399.04) * (Espesor)) / (carga * 36)) * 100;
            TiempoLabor = TiempoMachine;
        }
        #endregion

        #endregion

        #region Functions

        #region ICentroTrabajo Function´s
        public bool Test()
        {
            return true;
        }
        #endregion

        #endregion
    }
}
