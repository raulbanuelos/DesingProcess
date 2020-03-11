using Model;
using Model.Interfaces;
using System.Collections.Generic;
using System;

namespace View.Services.TiempoEstandar.FCI
{
    public class CentroTrabajo2060 : BaseCentroTrabajo, ICentroTrabajo
    {
        #region Atributos

        private double _width;
        private double _espesorEspaciado;
        private double No_espaciadores;
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
        public CentroTrabajo2060()
        {
            CentroTrabajo = "2060";
            FactorLabor = .33;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            _anillo = new Anillo();

            Propiedad widthNominal = new Propiedad { DescripcionCorta = "Width Nominal", DescripcionLarga = "Width nominal del anillo(Franklin)", Imagen = null, Nombre = "WidthNominal", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance) };
            PropiedadesRequeridadas.Add(widthNominal);

            Propiedad espesorEspaciador2060 = new Propiedad { DescripcionCorta = "Espesor Espaciador", DescripcionLarga = "Espesor espaciador en operación SPLITTER CUFFS (FRANKLIN)", Imagen = null, Nombre = "espesorEspaciador2060", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance) };
            PropiedadesRequeridadas.Add(espesorEspaciador2060);

            Propiedad noEspaciadores2060 = new Propiedad { DescripcionCorta = "No. de espaciadores", DescripcionLarga = "Numero de espaciadores en operación SPLITTER CUFFS (FRANKLIN)", Imagen = null, Nombre = "noEspaciadores2060", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Cantidad) };
            PropiedadesRequeridadas.Add(noEspaciadores2060);
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
            _width = Module.GetValorPropiedad("WidthNominal", PropiedadesRequeridadas);
            _espesorEspaciado = Module.GetValorPropiedad("espesorEspaciador2060", PropiedadesRequeridadas);
            No_espaciadores = Module.GetValorPropiedad("noEspaciadores2060", PropiedadesRequeridadas);
            TiempoSetup = double.Parse(DataManager.GetTiempo(CentroTrabajo));
            TiempoMachine = Math.Round((((_width) * (99.8379971681416)) / (36 * ((_espesorEspaciado * No_espaciadores) + 0.465))) * 100, 3, MidpointRounding.AwayFromZero);
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
