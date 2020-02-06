using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;

namespace View.Services.TiempoEstandar.Gasolina
{
    public class CentroTrabajo140 : BaseCentroTrabajo, ICentroTrabajo
    {
        #region Properties

        #region Properties of ICentroTrabajo

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

        private double d1;
        private double h1;
        private int origen;

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

            //Ejecutamos el método para calcular los tiempos estándar.
            Calcular();
        }

        /// <summary>
        /// Método que calcula los tiempos estándar.
        /// </summary>
        public void Calcular()
        {
            TiempoSetup = DataManager.GetTimeSetup(CentroTrabajo);
            d1  = Module.GetValorPropiedad("D1", PropiedadesRequeridadas);
            h1 = Module.GetValorPropiedad("H1", PropiedadesRequeridadas);
            origen = Convert.ToInt32(Module.GetValorPropiedad("origenDesengrase", PropiedadesRequeridadas));

            int rieles = GetNumRieles();
            double carga = Math.Round(((rieles * 20) / h1), 0);
            if (origen == 1 || origen == 2)
            {
                double cicloCarga = (507.59 + 61.846 + 0);
                TiempoMachine = 100 * ((cicloCarga) / 3600) / (carga);
            }
            else
            {
                double tciclo = 688.19;
                int rielesPremaquinado = GetRielesPremaquinado();
                TiempoMachine = ((478.6622 + tciclo) / (36 * (2 * rielesPremaquinado * (29.5 / h1))));
            }

            TiempoMachine = Math.Round(TiempoMachine * 100, 3);

            TiempoLabor = TiempoMachine * FactorLabor;
        }
        #endregion

        private int GetRielesPremaquinado()
        {

            if (d1 >= 0 && d1 <= 1.7499)
                return 28;
            else
            {
                if (d1 >= 1.75 && d1 <= 3.499)
                    return 14;
                else
                {
                    if (d1 >= 3.5 && d1 <=4.899)
                        return 10;
                    else
                    {
                        if (d1 >= 4.9 && d1<=5.699)
                            return 8;
                        else
                            return 5;
                    }
                }
            }
        }

        private int GetNumRieles()
        {
            int rieles = 0;
            if (d1 >= 0 && d1 <= 4.2999)
                rieles = 12;
            else
            {
                if (d1 >= 4.3 && d1<=5.59999)
                    rieles = 5;
                else
                    rieles = 3;
            }

            return rieles;
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

        #region Constructors
        public CentroTrabajo140()
        {
            CentroTrabajo = "140";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            //Inicializamos los datos requeridos para el cálculo.
            Propiedad _d1 = new Propiedad { Nombre = "D1", TipoDato = "Distance", DescripcionCorta = "Diámetro nominal", DescripcionLarga = "Diámetro nominal del anillo", Imagen = null, Unidad = "Inch (in)", Valor = 0 };
            PropiedadesRequeridadas.Add(_d1);

            Propiedad _h1 = new Propiedad { Nombre = "H1", TipoDato = "Distance", DescripcionCorta = "Width nominal", DescripcionLarga = "Width nominal del anillo", Imagen = null, Unidad = "Inch (in)", Valor = 0 };
            PropiedadesRequeridadas.Add(_h1);

            Propiedad _origenDesengrase = new Propiedad { Nombre = "origenDesengrase", TipoDato = "Distance", DescripcionCorta = "Origen desengrase:", DescripcionLarga = "Ingresar 1.-ByK; 2.- Diskus", Imagen = null, Unidad = "Cantidad", Valor = 0 };
            PropiedadesRequeridadas.Add(_origenDesengrase);


        } 
        #endregion
    }
}
