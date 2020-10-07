using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;

namespace View.Services.TiempoEstandar.Segmentos
{
    public class CentroTrabajo456 : BaseCentroTrabajo, ICentroTrabajo
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
        public CentroTrabajo456()
        {
            CentroTrabajo = "456";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();
            
            Propiedad widthAnillo = DataManager.GetPropiedadByNombre("H1");
            widthAnillo.Unidad = "Inch (in)";
            PropiedadesRequeridadas.Add(widthAnillo);

            Propiedad parograma456 = DataManager.GetPropiedadByNombre("programa456");
            parograma456.Unidad = "Unidades";
            PropiedadesRequeridadas.Add(parograma456);

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

            double noPrograma = Module.GetValorPropiedad("programa456", PropiedadesRequeridadas);

            double tct = 0;

            if (noPrograma == 1)
                tct = 5;
            else
            {
                if (noPrograma == 2 || noPrograma == 4)
                    tct = 20;
                else
                {
                    if (noPrograma == 3)
                        tct = 15;
                    else
                    {
                        if (noPrograma == 5)
                            tct = 25;
                        else
                        {
                            if (noPrograma == 6)
                                tct = 30;
                            else
                            {
                                if (noPrograma == 7)
                                    tct = 35;
                                else
                                {
                                    if (noPrograma == 8)
                                        tct = 40;
                                    else
                                    {
                                        if (noPrograma == 9)
                                            tct = 45;
                                        else
                                        {
                                            if (noPrograma == 10)
                                                tct = 50;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }


            double carga_por_ciclo, ciclo_por_carga;
            carga_por_ciclo = (3.5 / width);
            ciclo_por_carga = (tct + 43.0728262579484 + 23.2685100589874);
            TiempoMachine = Math.Round((100 * (ciclo_por_carga / 3600) / (carga_por_ciclo)) * 100, 3);

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
