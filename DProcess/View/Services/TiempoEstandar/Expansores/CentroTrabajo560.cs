using Model;
using Model.Interfaces;
using System.Collections.Generic;
using System;

namespace View.Services.TiempoEstandar.Expansores
{
    public class CentroTrabajo560 : BaseCentroTrabajo, ICentroTrabajo
    {
        #region Atributos
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
        public CentroTrabajo560()
        {
            CentroTrabajo = "560";
            FactorLabor = 0.50;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            _anillo = new Anillo();
            Propiedad NumJornadas = new Propiedad { DescripcionCorta = "Numero Jorobas", DescripcionLarga = "Numero de jorobas del Componente", Imagen = null, Nombre = "NumJorobas", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Cantidad), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadCantidad.Unidades) };
            PropiedadesRequeridadas.Add(NumJornadas);

            Propiedad diametroAnillo = new Propiedad { Nombre = "D1", TipoDato = "Distance", Unidad = "Inch (in)", DescripcionLarga = "Diámetro nominal del segmento (Plano)", Imagen = null, DescripcionCorta = "Diámetro nominal del segmento:" };
            PropiedadesRequeridadas.Add(diametroAnillo);
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
            Propiedad pDiametro = Module.GetPropiedad("D1", PropiedadesRequeridadas);
            double diametro = Module.ConvertTo("Distance", pDiametro.Unidad, "Inch (in)", pDiametro.Valor);
            
            Propiedad pJorobas = Module.GetPropiedad("NumJorobas", PropiedadesRequeridadas);
            double jorobas = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Cantidad), pJorobas.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadCantidad.Unidades), pJorobas.Valor);

            TiempoSetup = double.Parse(DataManager.GetTiempo(CentroTrabajo));
            TiempoMachine = Math.Round( (((0.1702 + (jorobas * ((diametro * 4.4954) / 3.1988) / 27)) / 36) * 100),3, MidpointRounding.AwayFromZero);
            TiempoLabor = Math.Round(TiempoMachine * FactorLabor,3, MidpointRounding.AwayFromZero);

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
