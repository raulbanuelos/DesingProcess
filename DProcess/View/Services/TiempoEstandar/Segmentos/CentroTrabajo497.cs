using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace View.Services.TiempoEstandar.Segmentos
{
    public class CentroTrabajo497 : BaseCentroTrabajo, ICentroTrabajo
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
        public CentroTrabajo497()
        {
            CentroTrabajo = "497";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            ObservableCollection<FO_Item> listaOpcionesReceta = new ObservableCollection<FO_Item>();
            listaOpcionesReceta.Add(new FO_Item { Nombre = "PRG-36", ValorCadena = "PRG-36" });
            listaOpcionesReceta.Add(new FO_Item { Nombre = "PRG-45", ValorCadena = "PRG-45" });
            listaOpcionesReceta.Add(new FO_Item { Nombre = "PRG-5", ValorCadena = "PRG-5" });
            listaOpcionesReceta.Add(new FO_Item { Nombre = "PRG-10", ValorCadena = "PRG-10" });
            listaOpcionesReceta.Add(new FO_Item { Nombre = "OTRO", ValorCadena = "OTRO" });
            PropiedadOptional pReceta = new PropiedadOptional { lblTitle = "Receta Nitrurado", ListaOpcional = listaOpcionesReceta, Nombre = "receta497" };
            PropiedadesRequeridasOpcionles.Add(pReceta);

            Propiedad widthAnillo = DataManager.GetPropiedadByNombre("H1");
            widthAnillo.Unidad = "Inch (in)";
            PropiedadesRequeridadas.Add(widthAnillo);

            Propiedad pCiclo497 = new Propiedad { Nombre = "tCiclo497", TipoDato = "Tiempo", Unidad = "second ('')", DescripcionLarga = "SI conoce la receta ingrese el digito \"0\"" + System.Environment.NewLine + "Si NO conoce la receta ingrese el tiempo ciclo.", Imagen = null, DescripcionCorta = "Tiempo ciclo Nitrurado:" };
            PropiedadesRequeridadas.Add(pCiclo497);

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

            double _tiempoCiclo = 0;
            PropiedadOptional pReceta= Module.GetPropiedadOpcional("receta497", PropiedadesRequeridasOpcionles);

            if (string.IsNullOrEmpty(pReceta.ElementSelected.ValorCadena) || pReceta.ElementSelected.ValorCadena == "OTRO")
            {
                Propiedad pTiempoCiclo = Module.GetPropiedad("tCiclo497", PropiedadesRequeridadas);
                _tiempoCiclo = Module.ConvertTo("Distance", pTiempoCiclo.Unidad, "second ('')", pTiempoCiclo.Valor);
            }else
            {
                string receta = pReceta.ElementSelected.ValorCadena;
                if (receta == "PRG-36")
                    _tiempoCiclo = 47160;
                else{
                    if (receta == "PRG-45")
                        _tiempoCiclo = 37800;
                    else
                    {
                        if (receta == "PRG-5")
                            _tiempoCiclo = 40980;
                        else
                        {
                            if (receta == "PRG-10")
                                _tiempoCiclo = 43440;
                        }
                    }
                }
            }

            Propiedad pWidth = Module.GetPropiedad("H1", PropiedadesRequeridadas);
            double width = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), pWidth.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), pWidth.Valor);

            TiempoMachine = Math.Round((((_tiempoCiclo + 1520) * width) / 56844) * 100, 3);

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
