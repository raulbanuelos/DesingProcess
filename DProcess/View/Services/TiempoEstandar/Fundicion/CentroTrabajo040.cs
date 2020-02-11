using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;

namespace View.Services.TiempoEstandar.Fundicion
{
    public class CentroTrabajo040 : BaseCentroTrabajo, ICentroTrabajo
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

        public List<string> Alertas
        {
            get;
            set;
        }
        public List<Model.PropiedadOptional> PropiedadesRequeridasOpcionles
        {
            get;
            set;
        }
        #endregion

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
            double tc_od = 0.0;
            double tc_id = 0.0;

            TiempoSetup = DataManager.GetTimeSetup(CentroTrabajo);

            string materialMahle = Module.GetValorPropiedadString("Material MAHLE", PropiedadesRequeridasCadena);
            string tipoMaterial = DataManager.GetTipoMaterial(materialMahle);

            double widthCasting = Module.GetValorPropiedad("WidthCasting", PropiedadesRequeridadas);
            double diaB = Module.GetValorPropiedad("BDiaCasting", PropiedadesRequeridadas);

            if (tipoMaterial.Equals("HIERRO GRIS") || tipoMaterial.Equals("HIERRO GRIS CENTRIFUGADO"))
            {
                tc_od = 8.6;
                tc_id = 4.22;
            }
            else if(tipoMaterial.Equals("HIERRO GRIS ALTO MODULO"))
            {
                tc_od = 5.67;
                tc_id = 8.79;
            }
            else if(tipoMaterial.Equals("HIERRO GRIS INTERMEDIO"))
            {
                tc_od = 5.67;
                tc_id = 8.79;
            }
            else
                Alertas.Add("Error al calcular tiempo estandar debido a que el tipo de material no se encuentra en el calculo." + Environment.NewLine + "Recomendación:" + Environment.NewLine + "Revice el calculo proporcionado por Ing. Industrial");

            TiempoMachine = Math.Round((widthCasting * (2.97 + (((tc_od * diaB / 3.917) + (tc_id * diaB / 3.917)))) / 129.6) * 100, 3);
            TiempoLabor = TiempoMachine;

        }
        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Default constructor
        /// </summary>
        public CentroTrabajo040()
        {
            CentroTrabajo = "040";
            FactorLabor = 0.737;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            //Inicializamos los datos requeridos para el cálculo.
            Propiedad widthCasting = new Propiedad { DescripcionCorta = "Width del casting", DescripcionLarga = "Width del casting", Imagen = null, Nombre = "WidthCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch) };
            PropiedadesRequeridadas.Add(widthCasting);

            Propiedad B_Dia = new Propiedad { DescripcionCorta = "B Dia", DescripcionLarga = "B Dia", Imagen = null, Nombre = "BDiaCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), Valor = 0 };
            PropiedadesRequeridadas.Add(B_Dia);

            PropiedadCadena espeMaterial = new PropiedadCadena { Nombre = "Material MAHLE", DescripcionCorta = "Material:", DescripcionLarga = "Especificación de materia prima (MF012-S,SPR-128,ETC)" };
            PropiedadesRequeridasCadena.Add(espeMaterial);

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
