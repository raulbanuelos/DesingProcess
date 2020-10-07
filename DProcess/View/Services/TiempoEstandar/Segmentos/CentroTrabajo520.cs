using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace View.Services.TiempoEstandar.Segmentos
{
    public class CentroTrabajo520 : BaseCentroTrabajo, ICentroTrabajo
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
        public CentroTrabajo520()
        {
            CentroTrabajo = "520";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            Propiedad widthAnillo = DataManager.GetPropiedadByNombre("H1");
            widthAnillo.Unidad = "Inch (in)";
            PropiedadesRequeridadas.Add(widthAnillo);

            ObservableCollection<FO_Item> listaOpcionesPrograma = new ObservableCollection<FO_Item>();
            listaOpcionesPrograma.Add(new FO_Item { Nombre = "Programa 1", Valor = 1 });
            listaOpcionesPrograma.Add(new FO_Item { Nombre = "Programa 2", Valor = 2 });
            listaOpcionesPrograma.Add(new FO_Item { Nombre = "Programa 3", Valor = 3 });
            listaOpcionesPrograma.Add(new FO_Item { Nombre = "Programa 4", Valor = 4 });
            listaOpcionesPrograma.Add(new FO_Item { Nombre = "Programa 5", Valor = 5 });
            listaOpcionesPrograma.Add(new FO_Item { Nombre = "Programa 6", Valor = 6 });
            listaOpcionesPrograma.Add(new FO_Item { Nombre = "Programa 7", Valor = 7 });
            listaOpcionesPrograma.Add(new FO_Item { Nombre = "Programa 8", Valor = 8 });

            PropiedadOptional pRecubrimiento = new PropiedadOptional { lblTitle = "Num. Programa Normalizado:", ListaOpcional = listaOpcionesPrograma, Nombre = "NumProgramaNormalizado" };
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

            PropiedadOptional pPrograma = Module.GetPropiedadOpcional("NumProgramaNormalizado", PropiedadesRequeridasOpcionles);

            Propiedad pWidth = Module.GetPropiedad("H1", PropiedadesRequeridadas);
            double width = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), pWidth.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), pWidth.Valor);

            int tc = GetTiempo(Convert.ToInt32(pPrograma.ElementSelected.Valor));

            TiempoMachine = Math.Round((((5165.6 + tc) * width) / (15798.24)) * 100, 3);

            //Obtenermos el valor específico de las propiedades requeridas.
            TiempoLabor = TiempoMachine * FactorLabor;

        }
        #endregion

        private int GetTiempo(int numPrograma)
        {
            int tc = 0;

            if (numPrograma == 1)
                tc = 1920;
            else
            {
                if (numPrograma == 2)
                    tc = 2520;
                else
                {
                    if (numPrograma == 3)
                        tc = 2520;
                    else
                    {
                        if (numPrograma == 4)
                            tc = 3900;
                        else
                        {
                            if (numPrograma == 5)
                                tc = 4200;
                            else
                            {
                                if (numPrograma == 6)
                                    tc = 4800;
                                else
                                {
                                    if (numPrograma == 7)
                                        tc = 3900;
                                    else
                                    {
                                        if (numPrograma == 8)
                                            tc = 4800;
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return tc;
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
