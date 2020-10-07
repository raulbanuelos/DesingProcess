using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;

namespace View.Services.TiempoEstandar.Segmentos
{
    public class CentroTrabajo517 : BaseCentroTrabajo, ICentroTrabajo
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
        public CentroTrabajo517()
        {
            CentroTrabajo = "517";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            Propiedad diametroAnillo = new Propiedad { Nombre = "D1", TipoDato = "Distance", Unidad = "Inch (in)", DescripcionLarga = "Diámetro nominal del segmento (Plano)", Imagen = null, DescripcionCorta = "Diámetro nominal del segmento:" };
            PropiedadesRequeridadas.Add(diametroAnillo);

            Propiedad widthAnillo = DataManager.GetPropiedadByNombre("H1");
            widthAnillo.Unidad = "Inch (in)";
            PropiedadesRequeridadas.Add(widthAnillo);

            PropiedadBool pChafalnInterno = new PropiedadBool { Nombre = "ope_hurricane", DescripcionCorta = "¿Desengrase Hurricane?", DescripcionLarga = "Activar si el desengrase Lapeado es Hurricane", Valor = false };
            PropiedadesRequeridasBool.Add(pChafalnInterno);

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
            bool banHurricane = Module.GetValorPropiedadBool("ope_hurricane", PropiedadesRequeridasBool);

            Propiedad pDiametro = Module.GetPropiedad("D1", PropiedadesRequeridadas);
            double diametro = Module.ConvertTo("Distance", pDiametro.Unidad, "Inch (in)", pDiametro.Valor);

            Propiedad pWidth = Module.GetPropiedad("H1", PropiedadesRequeridadas);
            double width = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), pWidth.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), pWidth.Valor);

            int rieles = 0;

            if (banHurricane)
            {
                rieles = GetRielesHurricane(diametro);
                TiempoMachine = Math.Round(((1440.8321 * width) / (575.964 * rieles)) * 100, 3);
            }
            else
            {
                rieles = GetRieles(diametro);
                TiempoMachine = Math.Round((((1266.27 + (rieles * 7.8698)) * width) / ((rieles * 15) * 36)) * 100, 3);
            }


            //Obtenermos el valor específico de las propiedades requeridas.
            TiempoLabor = TiempoMachine * FactorLabor;

        }
        #endregion

        private int GetRielesHurricane(double _diametro)
        {
            int riel = 0;

            if (_diametro <= 1.8749)
                riel = 0;
            else
            {
                if (_diametro >= 1.875 && _diametro <= 4.74)
                    riel = 12;
                else
                {
                    if (_diametro >= 4.75 && _diametro <= 5.5)
                        riel = 5;
                    else
                    {
                        if (_diametro >= 5.51 && _diametro <= 6.125)
                            riel = 3;
                        else
                            riel = 0;
                    }
                }
            }

            return riel;
        }

        private int GetRieles(double _diametro)
        {
            int riel = 0;

            if (_diametro < 2.5)
                riel = 4;
            else
            {
                if (_diametro >= 2.5 && _diametro < 4.9)
                    riel = 3;
                else
                {
                    if (_diametro >= 4.9 && _diametro < 7)
                        riel = 2;
                    else
                        riel = 0;
                }
            }

            return riel;
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
