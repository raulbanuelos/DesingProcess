using Model;
using Model.Interfaces;
using System.Collections.Generic;
using System;

namespace View.Services.TiempoEstandar.FCI
{
    public class CentroTrabajo2290 : BaseCentroTrabajo, ICentroTrabajo
    {
        #region Atributos
        private double _h1;
        private double _d1;
        private int b;
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
        public CentroTrabajo2290()
        {
            CentroTrabajo = "2290";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            _anillo = new Anillo();
            Propiedad widthNominalAnillo = new Propiedad { DescripcionCorta = "Width nominal", DescripcionLarga = "Width nominal del anillo (Plano)", Imagen = null, Nombre = "widthNominalAnillo", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance) };
            PropiedadesRequeridadas.Add(widthNominalAnillo);

            Propiedad diaNominalAnillo = new Propiedad { DescripcionCorta = "Diámetro nominal", DescripcionLarga = "Diámetro nominal del anillo (Plano)", Imagen = null, Nombre = "diaNominalAnillo", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance) };
            PropiedadesRequeridadas.Add(diaNominalAnillo);
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
            int bastidor;
            double carga, ciclo_por_carga;
            _d1 = Module.GetValorPropiedad("diaNominalAnillo", PropiedadesRequeridadas);
            _h1 = Module.GetValorPropiedad("widthNominalAnillo", PropiedadesRequeridadas);
            TiempoSetup = double.Parse(DataManager.GetTiempo(CentroTrabajo));
            bastidor = buscar_bastidor();
            carga = Math.Round(((bastidor * 20) / _h1), 0);
            ciclo_por_carga = 1264.82915;
            TiempoMachine = Math.Round((100 * (ciclo_por_carga / 3600) / (carga)) * 100, 3, MidpointRounding.AwayFromZero);
            TiempoLabor = TiempoMachine;
        }

        private int buscar_bastidor()
        {
            if(_d1 >= 5.71)
            {
                b = 5;
            }else if (_d1 >= 4.6)
            {
                b = 9;
            }else if(_d1 >= 4.1)
            {
                b = 11;
            }else if (_d1 >= 1.75)
            {
                b = 15;
            }else if (_d1 >= 0.8268)
            {
                b = 29;
            }
            return b;
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
