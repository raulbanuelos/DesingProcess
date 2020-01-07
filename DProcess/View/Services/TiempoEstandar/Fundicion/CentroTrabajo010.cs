using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.TiempoEstandar.Fundicion
{
    public class CentroTrabajo010 : ICentroTrabajo
    {
        #region Atributtes
        private double mouting;
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
        #endregion

        #endregion

        #region Métodos

        #region Métodos de ICentroTrabajo

        /// <summary>
        /// Método que se utiliza cuando se calculan los tiempos estándar por fuera de las operaciones. (Cálculo individual, cotizaciones).
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

            pesoCasting = Module.GetValorPropiedad("PesoCasting", PropiedadesRequeridadas);
            mouting = Module.GetValorPropiedad("MoutingCasting", PropiedadesRequeridadas);

            //Ejecutamos el método para calcular los tiempos estándar.
            Calcular();
        }

        /// <summary>
        /// Método que calcula los tiempos estándar.
        /// </summary>
        public void Calcular()
        {
            TiempoSetup = DataManager.GetTimeSetup(CentroTrabajo);

            double pesoEsqueleto = 0;

            if (mouting.Equals(1))
                pesoEsqueleto = 33000;
            else if (mouting.Equals(2))
                pesoEsqueleto = 24600;
            else if (mouting.Equals(3))
                pesoEsqueleto = 31500;
            else if (mouting.Equals(4))
                pesoEsqueleto = 26200;
            else if (mouting.Equals(5))
                pesoEsqueleto = 14288;
            else if (mouting.Equals(6))
                pesoEsqueleto = 10600;
            else if (mouting.Equals(7))
                pesoEsqueleto = 13500;
            else if (mouting.Equals(8))
                pesoEsqueleto = 16000;
            else if (mouting.Equals(9))
                pesoEsqueleto = 18600;
            else if (mouting.Equals(10))
                pesoEsqueleto = 19300;
            else if (mouting.Equals(12))
                pesoEsqueleto = 19350;
            else if (mouting.Equals(14))
                pesoEsqueleto = 19400;
            else if (mouting.Equals(16))
                pesoEsqueleto = 25000;
            else if (mouting.Equals(0))
                pesoEsqueleto = 0;
            else
                Alertas.Add("No se calculo correctamente el tiempo estandar debido a que el número de impreciones de la placa modelo no está dentro de los calculos" +
                                Environment.NewLine + "Recomendación:" + Environment.NewLine + "Revice el calculo proporcionado por Ing. Industrial.");

            //Evaluamos si mouting es direfente de cero, lo que significará que es una placa modelo. Si no es un cuff.
            if (!mouting.Equals(0))
                TiempoMachine = Math.Round((((7308.48 + (141.91 * (((1400000 / (pesoEsqueleto + (pesoCasting * mouting * 19))) / 4) / 2)))) / (((1400000 / (pesoEsqueleto + (pesoCasting * mouting * 19))) * mouting * 19) * 36)) * 100, 3);
            else
                TiempoMachine = Math.Round((17210.5 / (((2850 / pesoCasting) * 12) * 36)) * 100, 3);

            TiempoLabor = FactorLabor;
        }
        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>

        public CentroTrabajo010()
        {
            CentroTrabajo = "010";
            FactorLabor = 0.737;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            Alertas = new List<string>();

            //Inicializamos los datos requeridos para el cálculo.
            Propiedad mounting = new Propiedad { DescripcionCorta = "Mouting", DescripcionLarga = "Mouting", Imagen = null, Nombre = "MoutingCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Mass), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadMass.Gram) };
            PropiedadesRequeridadas.Add(mounting);

            Propiedad pesoCasting = new Propiedad { DescripcionCorta = "Peso casting", DescripcionLarga = "Peso del casting", Imagen = null, Nombre = "PesoCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Mass), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadMass.Gram), Valor = 0 };
            PropiedadesRequeridadas.Add(pesoCasting);
        }
        #endregion

        #region Functions

        #region ICentroTrabajo Function´s
        public bool Test()
        {
            if (mouting.Equals(1))
                return true;
            else if (mouting.Equals(2))
                return true;
            else if (mouting.Equals(3))
                return true;
            else if (mouting.Equals(4))
                return true;
            else if (mouting.Equals(5))
                return true;
            else if (mouting.Equals(6))
                return true;
            else if (mouting.Equals(7))
                return true;
            else if (mouting.Equals(8))
                return true;
            else if (mouting.Equals(9))
                return true;
            else if (mouting.Equals(10))
                return true;
            else if (mouting.Equals(12))
                return true;
            else if (mouting.Equals(14))
                return true;
            else if (mouting.Equals(16))
                return true;
            else if (mouting.Equals(0))
                return true;
            else
                return false;
        }
        #endregion

        #endregion
    }
}
