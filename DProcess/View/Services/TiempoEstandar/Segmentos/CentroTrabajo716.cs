using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace View.Services.TiempoEstandar.Segmentos
{
    public class CentroTrabajo716 : BaseCentroTrabajo, ICentroTrabajo
    {
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
        public CentroTrabajo716()
        {
            CentroTrabajo = "716";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();
            
            Propiedad widthAnillo = DataManager.GetPropiedadByNombre("H1");
            widthAnillo.Unidad = "Inch (in)";
            PropiedadesRequeridadas.Add(widthAnillo);

            Propiedad capaCromo = new Propiedad { Nombre = "CapaCromo716", TipoDato = "Distance", Unidad = "Inch (in)", DescripcionLarga = "Capa de cromo", Imagen = null, DescripcionCorta = "Capa de cromo" };
            PropiedadesRequeridadas.Add(capaCromo);

            Propiedad tiempoCiclo = new Propiedad { Nombre = "tc716", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Tiempo), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadTiempo.Second), DescripcionLarga = "Tiempo ciclo en Cromo C.T. 716", DescripcionCorta = "Tiempo Ciclo Cromo", Imagen = null};
            PropiedadesRequeridadas.Add(tiempoCiclo);

            ObservableCollection<FO_Item> listaOpcionesReceta = new ObservableCollection<FO_Item>();
            listaOpcionesReceta.Add(new FO_Item { Nombre = "Línea Automática", ValorCadena = "Línea Automática" });
            listaOpcionesReceta.Add(new FO_Item { Nombre = "Línea Manual", ValorCadena = "Línea Manual" });

            PropiedadOptional pRecubrimiento = new PropiedadOptional { lblTitle = "Línea Cromo:", ListaOpcional = listaOpcionesReceta, Nombre = "linea716" };
            PropiedadesRequeridasOpcionles.Add(pRecubrimiento);

            _anillo = new Anillo();
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
            PropiedadesRequeridasOpcionles = Module.AsignarValoresPropiedadesOpcionales(PropiedadesRequeridasOpcionles, anillo);
            _anillo = anillo;

            //Ejecutamos el método para calcular los tiempos estándar.
            Calcular();
        }

        /// <summary>
        /// Método que calcula los tiempos estándar.
        /// </summary>
        public void Calcular()
        {
            TiempoSetup = DataManager.GetTimeSetup(CentroTrabajo);
            
            Propiedad pWidth = Module.GetPropiedad("H1", PropiedadesRequeridadas);
            double width = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), pWidth.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), pWidth.Valor);

            Propiedad pTiempoCiclo = Module.GetPropiedad("tc716", PropiedadesRequeridadas);
            double tiempoCiclo = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Tiempo), pTiempoCiclo.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadTiempo.Second), pTiempoCiclo.Valor);

            PropiedadOptional pLinea = Module.GetPropiedadOpcional("linea716", PropiedadesRequeridasOpcionles);

            if (pLinea.ElementSelected.ValorCadena == "Línea Automática")
                TiempoMachine = Math.Round((((948.8039 + tiempoCiclo) * width) / 38320.118) * 100, 3);
            else
                TiempoMachine = Math.Round((((742.9466 + tiempoCiclo) * width) / 5474.304) * 100, 3);

            //Obtenermos el valor específico de las propiedades requeridas.
            TiempoLabor = TiempoMachine * FactorLabor;

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
