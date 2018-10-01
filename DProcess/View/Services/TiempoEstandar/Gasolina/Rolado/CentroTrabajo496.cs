using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.TiempoEstandar.Gasolina.Rolado
{
    public class CentroTrabajo496
    {
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
        #endregion

        #endregion

        #region Contructors
        public CentroTrabajo496()
        {
            CentroTrabajo = "496";
            FactorLabor = 0.50;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();

            //Inicializamos los datos requeridos para el cálculo.
            Propiedad _d1 = new Propiedad { Nombre = "D1", TipoDato = "Distance", DescripcionCorta = "Diámetro nominal", DescripcionLarga = "Diámetro nominal del anillo", Imagen = null, Unidad = "Inches (in)", Valor = 0 };
            PropiedadesRequeridadas.Add(_d1);

            Propiedad _h1 = new Propiedad { Nombre = "H1", TipoDato = "Distance", DescripcionCorta = "Width nominal", DescripcionLarga = "Width nominal del anillo", Imagen = null, Unidad = "Inches (in)", Valor = 0 };
            PropiedadesRequeridadas.Add(_h1);

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
        public void Calcular(List<Propiedad> ListaPropiedades, List<PropiedadBool> ListaPropiedadesBool, List<PropiedadCadena> ListaPropiedadesCadena)
        {
            //Obtenemos los valores de las propiedades requeridas.
            PropiedadesRequeridadas = Module.AsignarValoresPropiedades(PropiedadesRequeridadas, ListaPropiedades);
            PropiedadesRequeridasBool = Module.AsignarValoresPropiedadesBool(PropiedadesRequeridasBool, ListaPropiedadesBool);
            PropiedadesRequeridasCadena = Module.AsignarValoresPropiedadesCadena(PropiedadesRequeridasCadena, ListaPropiedadesCadena);

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

            //Ejecutamos el método para calcular los tiempos estándar.
            Calcular();
        }

        /// <summary>
        /// Método que calcula los tiempos estándar.
        /// </summary>
        public void Calcular()
        {

            TiempoSetup = DataManager.GetTimeSetup(CentroTrabajo);
            double diametroNominal = Module.GetValorPropiedad("D1", PropiedadesRequeridadas);
            double widthNominal = Module.GetValorPropiedad("H1", PropiedadesRequeridadas);
            int cantPinoNivel = getCargaPino(diametroNominal) == 11.92 ? 35 : 69;
            int cantPinoTotales = cantPinoNivel * 2;

            TiempoMachine = Math.Round(((35026 * widthNominal) / ((cantPinoNivel * cantPinoTotales)) * 36) * 100, 3);
            TiempoLabor = TiempoMachine * FactorLabor;
        }

        private double getCargaPino(double diametro)
        {
            return diametro <= 4.3307 ? 12.1 : 11.92;
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
