using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace View.Services.TiempoEstandar.Segmentos
{
    public class CentroTrabajo720 : BaseCentroTrabajo, ICentroTrabajo
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
        public CentroTrabajo720()
        {
            CentroTrabajo = "720";
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

            ObservableCollection<FO_Item> listaOpcionesReceta = new ObservableCollection<FO_Item>();
            listaOpcionesReceta.Add(new FO_Item { Nombre = "Nitrurado", ValorCadena = "Nitrurado" });
            listaOpcionesReceta.Add(new FO_Item { Nombre = "Cromado", ValorCadena = "Cromado" });

            PropiedadOptional pRecubrimiento = new PropiedadOptional { lblTitle = "Recubrimiento:", ListaOpcional = listaOpcionesReceta, Nombre = "recubrimiento" };
            PropiedadesRequeridasOpcionles.Add(pRecubrimiento);

            ObservableCollection<FO_Item> listaOpcionesPavonado = new ObservableCollection<FO_Item>();
            listaOpcionesPavonado.Add(new FO_Item { Nombre = "Blackening", ValorCadena = "Blackening" });
            listaOpcionesPavonado.Add(new FO_Item { Nombre = "Línea automática", ValorCadena = "Línea automática" });

            PropiedadOptional pPavonado = new PropiedadOptional { lblTitle = "Tipo de Pavonado:", ListaOpcional = listaOpcionesPavonado, Nombre = "tipo_pavonado" };
            PropiedadesRequeridasOpcionles.Add(pPavonado);


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

            Propiedad pDiametro = Module.GetPropiedad("D1", PropiedadesRequeridadas);
            double diametro = Module.ConvertTo("Distance", pDiametro.Unidad, "Inch (in)", pDiametro.Valor);

            Propiedad pWidth = Module.GetPropiedad("H1", PropiedadesRequeridadas);
            double width = Module.ConvertTo("Distance", pWidth.Unidad, "Inch (in)", pWidth.Valor);

            PropiedadOptional pRecubrimiento = Module.GetPropiedadOpcional("recubrimiento", PropiedadesRequeridasOpcionles);
            PropiedadOptional pTipoPavonado = Module.GetPropiedadOpcional("tipo_pavonado", PropiedadesRequeridasOpcionles);

            int rack = 0;
            double tc, ac;

            if (pTipoPavonado.ElementSelected.ValorCadena == "Blackening")
            {
                rack = diametro <= 4.5 ? 12 : 6;
                tc = pRecubrimiento.ElementSelected.ValorCadena == "Cromado" ? 510.25 : 471.56;
                ac = pRecubrimiento.ElementSelected.ValorCadena == "Cromado" ? 510.25 : 471.46;

                TiempoMachine = Math.Round((((600.5064 + tc + ac) * width) / (432 * rack)) * 100, 3);
            }
            else
            {
                int rieles = GetRieles(diametro);
                TiempoMachine = Math.Round(((734.22 * width) / ((36 * (6.33 * rieles)))) * 100, 3);
            }

            //Obtenermos el valor específico de las propiedades requeridas.
            TiempoLabor = TiempoMachine * FactorLabor;

        }
        #endregion

        private int GetRieles(double diametro)
        {
            int rieles = 0;

            if (diametro <= 1.8749)
                rieles = 0;
            else
            {
                if (diametro >= 1.875 && diametro <= 5.49)
                    rieles = 12;
                else
                {
                    if (diametro >= 5.5 && diametro <= 6.125)
                        rieles = 9;
                    else
                        rieles = 0;
                }
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
    }
}
