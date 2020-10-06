using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace View.Services.TiempoEstandar.Segmentos
{
    public class CentroTrabajo555 : BaseCentroTrabajo, ICentroTrabajo
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
        public CentroTrabajo555()
        {
            CentroTrabajo = "555";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            Propiedad widthAnillo = DataManager.GetPropiedadByNombre("H1");
            widthAnillo.Unidad = "Inch (in)";
            PropiedadesRequeridadas.Add(widthAnillo);

            ObservableCollection<FO_Item> listaOpcionesReceta = new ObservableCollection<FO_Item>();
            listaOpcionesReceta.Add(new FO_Item { Nombre = "Nitrurado", ValorCadena = "Nitrurado" });
            listaOpcionesReceta.Add(new FO_Item { Nombre = "Cromado", ValorCadena = "Cromado" });

            PropiedadOptional pRecubrimiento = new PropiedadOptional { lblTitle = "Recubrimiento:", ListaOpcional = listaOpcionesReceta, Nombre = "recubrimiento" };
            PropiedadesRequeridasOpcionles.Add(pRecubrimiento);

            PropiedadBool pChafalnInterno = new PropiedadBool { Nombre = "chaflan_interno555", DescripcionCorta = "Chaflan interno", DescripcionLarga = "Activar si lleva chaflan interno.", Valor = false};
            PropiedadesRequeridasBool.Add(pChafalnInterno);

            PropiedadBool pRebabeo = new PropiedadBool { Nombre = "rebabeoCT555", DescripcionCorta = "Rebabeo", DescripcionLarga = "Activar si lleva Rebabeo", Valor = false };
            PropiedadesRequeridasBool.Add(pRebabeo);

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

            PropiedadOptional pRecubrimiento = Module.GetPropiedadOpcional("recubrimiento", PropiedadesRequeridasOpcionles);
            bool chaflanInterno = Module.GetValorPropiedadBool("chaflan_interno555", PropiedadesRequeridasBool);
            bool rebabeo = Module.GetValorPropiedadBool("rebabeoCT555", PropiedadesRequeridasBool);

            double tcChaflan, tcRebabeo = 0;

            Propiedad pWidth = Module.GetPropiedad("H1", PropiedadesRequeridadas);
            double width = Module.ConvertTo("Distance", pWidth.Unidad, "Inch (in)", pWidth.Valor);

            if (pRecubrimiento.ElementSelected.ValorCadena == "Nitrurado")
            {
                tcChaflan = chaflanInterno ? 4.911818 : 0;
                tcRebabeo = rebabeo ? 3.17 : 0;
                TiempoMachine = Math.Round((((211.23 + 43.87 + (tcChaflan + tcRebabeo)) * width) / 226.8) * 100, 3);
            }
            else
            {
                if (pRecubrimiento.ElementSelected.ValorCadena == "Cromado")
                {
                    tcChaflan = chaflanInterno ? 4.911818 : 0;
                    tcRebabeo = rebabeo ? 3.172491 : 0;
                    TiempoMachine = Math.Round((((226.6923 + 34.27 + (tcChaflan + tcRebabeo)) * width) / 226.6452) * 100, 3);
                }
            }
            
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
