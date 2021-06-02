using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.TiempoEstandar.Gasolina.Maquinado
{
    public class CentroTrabajo260 : BaseCentroTrabajo, ICentroTrabajo
    {
        #region Properties

        #region Properties of ICentroTrabajo

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

        //private double d1;
        //private double h1;
        //private int origen;

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
            string espec = Module.GetValorPropiedadString("ESPEC_MATERIAL", PropiedadesRequeridasCadena);
            string tipoMaterial = DataManager.GetTipoMaterial(espec);
            double t_ciclo = 0;

            if (string.IsNullOrEmpty(tipoMaterial))
                Alertas.Add("No se puede encontrar el tipo de material de la especificación " + espec + ", por favor ingrese una especificación válida.");

            if (tipoMaterial == "HIERRO GRIS" || tipoMaterial == "HIERRO GRIS CENTRIFUGADO")
                t_ciclo = .89;
            else
            {
                if (tipoMaterial == "HIERRO GRIS INTERMEDIO")
                    t_ciclo = .898;
                else
                {
                    if (tipoMaterial == "HIERRO GRIS ALTO MODULO")
                        t_ciclo = .89;
                    else
                    {
                        if (tipoMaterial == "HIERRO DUCTIL")
                            t_ciclo = 1.138;
                        else
                        {
                            if (tipoMaterial == "ACERO INOXIDABLE" || tipoMaterial == "ACERO AL CARBON")
                                t_ciclo = 1.138;
                            else
                                Alertas.Add("El tipo de material " + tipoMaterial + " no esta disponible para este centro de trabajo, los calculos podrían no correspoder a los reales");
                        }
                    }
                }
            }

            TiempoMachine = Math.Round(((0.280351 + t_ciclo) / 36) * 100, 3);
            
            TiempoLabor = Math.Round(TiempoMachine * FactorLabor,3);
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

        #region Constructors
        public CentroTrabajo260()
        {
            CentroTrabajo = "260";
            FactorLabor = 0.5;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();

            //Inicializamos los datos requeridos para el cálculo.
            PropiedadCadena _especAnillo = new PropiedadCadena { Nombre = "ESPEC_MATERIAL", DescripcionCorta = "Especificación de material", DescripcionLarga = "Especificación de material del anillo(MF012-S, SPR-128, ect)", Imagen = null};
            PropiedadesRequeridasCadena.Add(_especAnillo);
        }
        #endregion
    }
}
