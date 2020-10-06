using Model;
using Model.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace View.Services.TiempoEstandar.Expansores
{
    public class CentroTrabajo832 : BaseCentroTrabajo, ICentroTrabajo
    {
        #region Atributos 
        private double pzsXrollo;
        private double AcP;
        private bool _tipo;
        #endregion

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
        public CentroTrabajo832()
        {
            CentroTrabajo = "832";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            _anillo = new Anillo();

            ObservableCollection<FO_Item> lista = new ObservableCollection<FO_Item>();
            lista.Add(new FO_Item { Nombre = "<--ES80-->" });
            lista.Add(new FO_Item { Nombre = "0.0590", Valor = 0.0590 });
            lista.Add(new FO_Item { Nombre = "0.0772", Valor = 0.0772 });
            lista.Add(new FO_Item { Nombre = "0.0787", Valor = 0.0787 });
            lista.Add(new FO_Item { Nombre = "0.0984", Valor = 0.0984 });
            lista.Add(new FO_Item { Nombre = "0.1102", Valor = 0.1102 });
            lista.Add(new FO_Item { Nombre = "0.1181", Valor = 0.1181 });
            lista.Add(new FO_Item { Nombre = "0.1378", Valor = 0.1378 });
            lista.Add(new FO_Item { Nombre = "0.1575", Valor = 0.1575 });
            lista.Add(new FO_Item { Nombre = "0.1577", Valor = 0.1577 });
            lista.Add(new FO_Item { Nombre = "0.1840", Valor = 0.1840 });
            lista.Add(new FO_Item { Nombre = "0.1875", Valor = 0.1875 });
            lista.Add(new FO_Item { Nombre = "0.0590", Valor = 0.0590 });

            lista.Add(new FO_Item { Nombre = "<--SS50-->" });
            lista.Add(new FO_Item { Nombre = "0.0984", Valor = 0.0984 });
            lista.Add(new FO_Item { Nombre = "0.1100", Valor = 0.1100 });
            lista.Add(new FO_Item { Nombre = "0.1102", Valor = 0.1102 });
            lista.Add(new FO_Item { Nombre = "0.1180", Valor = 0.1180 });
            lista.Add(new FO_Item { Nombre = "0.1181", Valor = 0.1181 });
            lista.Add(new FO_Item { Nombre = "0.1250", Valor = 0.1250 });
            lista.Add(new FO_Item { Nombre = "0.1378", Valor = 0.1378 });
            lista.Add(new FO_Item { Nombre = "0.1560", Valor = 0.1560 });
            lista.Add(new FO_Item { Nombre = "0.1575", Valor = 0.1575 });
            lista.Add(new FO_Item { Nombre = "0.1577", Valor = 0.1577 });
            lista.Add(new FO_Item { Nombre = "0.1772", Valor = 0.1772 });
            lista.Add(new FO_Item { Nombre = "0.1840", Valor = 0.1840 });
            lista.Add(new FO_Item { Nombre = "0.1870", Valor = 0.1870 });
            lista.Add(new FO_Item { Nombre = "0.1875", Valor = 0.1875 });
            lista.Add(new FO_Item { Nombre = "0.1969", Valor = 0.1969 });
            lista.Add(new FO_Item { Nombre = "0.1970", Valor = 0.1970 });
            lista.Add(new FO_Item { Nombre = "0.2480", Valor = 0.2480 });
            lista.Add(new FO_Item { Nombre = "0.2490", Valor = 0.2490 });
            lista.Add(new FO_Item { Nombre = "0.2500", Valor = 0.2500 });

            PropiedadOptional widthNominal = new PropiedadOptional { ListaOpcional = lista, lblTitle = "Width Nominal" };
            PropiedadesRequeridasOpcionles.Add(widthNominal);

            Propiedad numeroDeFranjasPintura = new Propiedad { DescripcionCorta = "No. de franjas a pintar", DescripcionLarga = "Numero de franjas de pintura", Imagen = null, Nombre = "CantidadFranjas", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Cantidad), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadCantidad.Unidades) };
            PropiedadesRequeridadas.Add(numeroDeFranjasPintura);

            PropiedadBool tipoExpansorSS50 = new PropiedadBool { Nombre = "SS50", DescripcionCorta = "SS50", DescripcionLarga = "SS50" };
            PropiedadesRequeridasBool.Add(tipoExpansorSS50);

            PropiedadBool tipoExpansorES80 = new PropiedadBool { Nombre = "ES80", DescripcionCorta = "ES80", DescripcionLarga = "ES80" };
            PropiedadesRequeridasBool.Add(tipoExpansorES80);

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
            TiempoSetup = double.Parse(DataManager.GetTiempo(CentroTrabajo));
            
            Propiedad pCantidadFranjas = Module.GetPropiedad("CantidadFranjas", PropiedadesRequeridadas);
            double _noFranjas = Module.ConvertTo("Cantidad", pCantidadFranjas.Unidad, "Unidades", pCantidadFranjas.Valor);
            
            FO_Item espesor = PropiedadesRequeridasOpcionles[0].ElementSelected;
            
            if (_noFranjas > 0)
                AcP = 2.4410206;
            
            //-----------------------------EXPANSOR ES80------------------------------
            if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.0590)
                pzsXrollo = 380;
            else if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.0772)
                pzsXrollo = 330;
            else if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.0787)
                pzsXrollo = 320;
            else if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.0984)
                pzsXrollo = 230;
            else if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.1102)
                pzsXrollo = 220;
            else if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.1181)
                pzsXrollo = 210;
            else if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.1378)
                pzsXrollo = 160;
            else if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.1575)
                pzsXrollo = 140;
            else if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.1577)
                pzsXrollo = 140;
            else if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.1840)
                pzsXrollo = 110;
            else if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.1875)
                pzsXrollo = 380;
            else if (PropiedadesRequeridasBool[1].Valor == true && espesor.Valor == 0.0590)
                pzsXrollo = 380;

            ////-----------------------------EXPANSOR SS50------------------------------
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.0984)
                pzsXrollo = 140;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1100)
                pzsXrollo = 125;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1102)
                pzsXrollo = 125;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1180)
                pzsXrollo = 120;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1181)
                pzsXrollo = 120;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1250)
                pzsXrollo = 110;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1378)
                pzsXrollo = 110;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1560)
                pzsXrollo = 90;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1575)
                pzsXrollo = 90;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1577)
                pzsXrollo = 90;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1772)
                pzsXrollo = 80;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1840)
                pzsXrollo = 70;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1870)
                pzsXrollo = 75;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1875)
                pzsXrollo = 75;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1969)
                pzsXrollo = 70;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.1970)
                pzsXrollo = 70;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.2480)
                pzsXrollo = 55;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.2490)
                pzsXrollo = 55;
            else if (PropiedadesRequeridasBool[0].Valor == true && espesor.Valor == 0.2500)
                pzsXrollo = 55;

            TiempoMachine = Math.Round(((98.855494 + (17.43625 * _noFranjas) + (AcP)) / (36 * pzsXrollo)) * 100,3, MidpointRounding.AwayFromZero);
            TiempoLabor = TiempoMachine;
            TiempoMachine = 0;
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
