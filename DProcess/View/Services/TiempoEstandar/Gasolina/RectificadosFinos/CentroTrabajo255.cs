using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.TiempoEstandar.Gasolina.RectificadosFinos
{
    public class CentroTrabajo255 : BaseCentroTrabajo, ICentroTrabajo
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

        #endregion

        #region Contructors
        public CentroTrabajo255()
        {
            CentroTrabajo = "255";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();
            //Inicializamos los datos requeridos para el cálculo.
            PropiedadCadena _especAnillo = new PropiedadCadena { Nombre = "ESPEC_MATERIAL", DescripcionCorta = "Especificación de material", DescripcionLarga = "Especificación de material del anillo(MF012-S, SPR-128, ect)", Imagen = null };
            PropiedadesRequeridasCadena.Add(_especAnillo);

            Propiedad _h1 = new Propiedad { Nombre = "H1", TipoDato = "Distance", DescripcionCorta = "Width nominal", DescripcionLarga = "Width nominal del anillo", Imagen = null, Unidad = "Inches (in)", Valor = 0 };
            PropiedadesRequeridadas.Add(_h1);

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
            double widthNominal = Module.GetValorPropiedad("H1", PropiedadesRequeridadas);
            double t_ciclo = GetTiempoCiclo(tipoMaterial);
            double dim_guillotina = GetDimGuillotina(widthNominal);
            
            TiempoMachine = Math.Round((((t_ciclo + 1.42539013) / (36 * dim_guillotina / widthNominal))) * 100, 3);

            TiempoLabor = TiempoMachine * FactorLabor;
        }
        #endregion

        private double GetDimGuillotina(double _h1)
        {
            double dim_guillotina = 0;
            if (_h1 >= 0.02 & _h1 < 0.046)
            {
                dim_guillotina = 0.274;
            }
            else if (_h1 >= 0.046 & _h1 < 0.058)
            {
                dim_guillotina = 0.274;
            }
            else if (_h1 >= 0.058 & _h1 < 0.0625)
            {
                dim_guillotina = 0.352;
            }
            else if (_h1 >= 0.0625 & _h1 < 0.068)
            {
                dim_guillotina = 0.373;
            }
            else if (_h1 >= 0.068 & _h1 < 0.078)
            {
                dim_guillotina = 0.406;
            }
            else if (_h1 >= 0.078 & _h1 < 0.0935)
            {
                dim_guillotina = 0.388;
            }
            else if (_h1 >= 0.0935 & _h1 < 0.096)
            {
                dim_guillotina = 0.465;
            }
            else if (_h1 >= 0.096 & _h1 < 0.098)
            {
                dim_guillotina = 0.325;
            }
            else if (_h1 >= 0.098 & _h1 < 0.118)
            {
                dim_guillotina = 0.388;
            }
            else if (_h1 >= 0.118 & _h1 < 0.125)
            {
                dim_guillotina = 0.352;
            }
            else if (_h1 >= 0.125 & _h1 < 0.137)
            {
                dim_guillotina = 0.373;
            }
            else if (_h1 >= 0.137 & _h1 < 0.155)
            {
                dim_guillotina = 0.274;
            }
            else if (_h1 >= 0.155 & _h1 < 0.157)
            {
                dim_guillotina = 0.31;
            }
            else if (_h1 >= 0.157 & _h1 < 0.187)
            {
                dim_guillotina = 0.465;
            }
            else if (_h1 >= 0.187 & _h1 < 0.195)
            {
                dim_guillotina = 0.373;
            }
            else if (_h1 >= 0.195 & _h1 < 0.249)
            {
                dim_guillotina = 0.388;
            }
            else if (_h1 >= 0.249 & _h1 < 0.3)
            {
                dim_guillotina = 0.499;
            }

            return dim_guillotina;
        }

        private double GetTiempoCiclo(string tipoMaterial)
        {
            double t_ciclo = 0;
            if (tipoMaterial == "HIERRO GRIS" | tipoMaterial == "HIERRO GRIS CENTRIFUGADO")
            {
                t_ciclo = 5.06;
            }
            else if (tipoMaterial == "HIERRO DUCTIL")
            {
                t_ciclo = 8.32;
            }
            else if (tipoMaterial == "ACERO INOXIDABLE" | tipoMaterial == "ACERO AL CARBON")
            {
                t_ciclo = 12.076;
            }
            else if (tipoMaterial == "HIERRO GRIS ALTO MODULO")
            {
                t_ciclo = 7.065;
            }
            else if (tipoMaterial == "HIERRO GRIS INTERMEDIO")
            {
                t_ciclo = 10.79;
            }

            return t_ciclo;
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
