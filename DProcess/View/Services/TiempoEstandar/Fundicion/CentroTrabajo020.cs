using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;

namespace View.Services.TiempoEstandar.Fundicion
{
    public class CentroTrabajo020 : ICentroTrabajo
    {
        #region Atributtes
        private string especMaterial;
        private double mouting;
        #endregion

        #region Propiedades

        #region Propiedades ICentroTrabajo
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
            TiempoSetup = DataManager.GetTimeSetup(CentroTrabajo);

            string materialMahle = Module.GetValorPropiedadString("Material MAHLE", PropiedadesRequeridasCadena);
            string tipoMaterial = DataManager.GetTipoMaterial(materialMahle);
            double mouting = Module.GetValorPropiedad("MoutingCasting", PropiedadesRequeridadas);
            
            double tiempoCicloMaterial = 0;
            if (tipoMaterial.Equals("HIERRO GRIS") || tipoMaterial.Equals("HIERRO GRIS CENTRIFUGADO") || tipoMaterial.Equals("HIERRO GRIS ALTO MODULO"))
                tiempoCicloMaterial = 1.88;
            else if (tipoMaterial.Equals("HIERRO GRIS INTERMEDIO"))
                tiempoCicloMaterial = 2.45;
            else
                Alertas.Add("Error al calcular tiempo estandar debido a que el tipo de material no se encuentra en el calculo." + Environment.NewLine + "Recomendación:" + Environment.NewLine + "Revice el calculo proporcionado por Ing. Industrial");

            TiempoMachine = Math.Round((11.46 + tiempoCicloMaterial) / (36 * mouting) * 100, 3);
            TiempoLabor = TiempoMachine;

        }
        #endregion

        #endregion

        #region Constructors
        public CentroTrabajo020()
        {
            CentroTrabajo = "020";
            FactorLabor = 1.0;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();

            Alertas = new List<string>();

            Propiedad mounting = new Propiedad { DescripcionCorta = "Mouting", DescripcionLarga = "Mouting", Imagen = null, Nombre = "MoutingCasting", TipoDato = EnumEx.GetEnumDescription(DataManager.TipoDato.Mass), Unidad = EnumEx.GetEnumDescription(DataManager.UnidadMass.Gram) };
            PropiedadesRequeridadas.Add(mounting);

            PropiedadCadena espeMaterial = new PropiedadCadena { Nombre = "Material MAHLE", DescripcionCorta = "Material:", DescripcionLarga = "Especificación de materia prima (MF012-S,SPR-128,ETC)" };
            PropiedadesRequeridasCadena.Add(espeMaterial);

            /**
             Ejemplo de carga de una propiedad Opcional.
              **/
            //PropiedadOptional material = new PropiedadOptional();
            //material.Source = 1;
            //material.ListaOpcional.Add(new FO_Item { Nombre = "MF012-S", ValorCadena = "MF012-S" });
            //material.ListaOpcional.Add(new FO_Item { Nombre = "SPR-128", ValorCadena = "SPR-128" });
            //material.ListaOpcional.Add(new FO_Item { Nombre = "PCR-128", ValorCadena = "PCR-128" });
            //material.lblTitle = "Espec Material";

            //PropiedadesRequeridasOpcionles.Add(material);

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
