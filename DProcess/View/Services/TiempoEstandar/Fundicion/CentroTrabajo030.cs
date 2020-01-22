using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.TiempoEstandar.Fundicion
{
    public class CentroTrabajo030 : ICentroTrabajo
    {
        #region Atributtes
        private double pesoCasting;
        #endregion

        #region Propiedades

        #region Propiedades ICentroTrabajo
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

        public List<string> Alertas
        {
            get;
            set;
        }
        public List<Model.PropiedadOptional> PropiedadesRequeridasOpcionles
        {
            get;
            set;
        }
        #endregion

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

            pesoCasting = Module.GetValorPropiedad("PesoCasting", PropiedadesRequeridadas);

            //Ejecutamos el método para calcular los tiempos estándar.
            Calcular();
        }

        /// <summary>
        /// Método que calcula los tiempos estándar.
        /// </summary>
        public void Calcular()
        {
            TiempoSetup = DataManager.GetTimeSetup(CentroTrabajo);

            TiempoMachine = Math.Round((1011.99 * pesoCasting / 4776156) * 100, 3);

            TiempoLabor = Math.Round(TiempoMachine * FactorLabor, 3);
        }
        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CentroTrabajo030()
        {
            CentroTrabajo = "030";
            FactorLabor = 0.625;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            Alertas = new List<string>();

            //Inicializamos los datos requeridos para el cálculo.
            Propiedad pesoCasting = new Propiedad { DescripcionCorta = "Peso casting", DescripcionLarga = "Peso del casting", Imagen = null, Nombre = "PesoCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Mass), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadMass.Gram), Valor = 0 };
            PropiedadesRequeridadas.Add(pesoCasting);
        }
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
