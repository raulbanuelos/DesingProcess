using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.TiempoEstandar.Gasolina.RectificadosFinos
{
    public class CentroTrabajo455 : BaseCentroTrabajo, ICentroTrabajo
    {
        #region Propiedades

        #region Propiedades ICentroTrabajo

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
        public CentroTrabajo455()
        {
            CentroTrabajo = "455";
            FactorLabor = 1;
            PropiedadesRequeridadas = new List<Propiedad>();
            PropiedadesRequeridasBool = new List<PropiedadBool>();
            PropiedadesRequeridasCadena = new List<PropiedadCadena>();
            PropiedadesRequeridasOpcionles = new List<PropiedadOptional>();
            Alertas = new List<string>();
            //Inicializamos los datos requeridos para el cálculo.
            Propiedad _h1 = new Propiedad { Nombre = "H1", TipoDato = "Distance", DescripcionCorta = "Width nominal", DescripcionLarga = "Width nominal del anillo", Imagen = null, Unidad = "Inches (in)", Valor = 0 };
            PropiedadesRequeridadas.Add(_h1);

            Propiedad numCarreras = new Propiedad { Nombre = "NUM_CARRERAS_LAPEADO", DescripcionCorta = "Num de carreras LAPEADO:", DescripcionLarga = "Número de carreras en LAPEADO, requerido solo si es el programa 44", Imagen = null, Unidad = "Unidades", TipoDato = "Cantidad", Valor = 0 };
            PropiedadesRequeridadas.Add(numCarreras);

            PropiedadCadena programa455 = new PropiedadCadena { Nombre = "NUM_PROGRAMA_LAPEADO", DescripcionCorta = "Num. programa LAPEADO:", DescripcionLarga = "Número de programa en la operación LAPEADO", Imagen = null, Valor = string.Empty };
            PropiedadesRequeridasCadena.Add(programa455);

            PropiedadCadena arbol455 = new PropiedadCadena { Nombre = "TIPO_PROGRAMA_LAPEADO", DescripcionCorta = "Tipo de arbol en LAPEADO:", DescripcionLarga = "Tipo de arbol, ingresar CORTO o LARGO", Imagen = null, Valor = string.Empty };
            PropiedadesRequeridasCadena.Add(arbol455);

            PropiedadBool caidaRadio = new PropiedadBool { Nombre = "BAN_CAIDA_RADIO", DescripcionCorta = "¿El anillo tiene caida de radio?", DescripcionLarga = "Indica si el anillo tiene caida de radio", Imagen = null, Valor = false };
            PropiedadesRequeridasBool.Add(caidaRadio);
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
            double widthNominal = Module.GetValorPropiedad("H1", PropiedadesRequeridadas);
            string arbol = Module.GetValorPropiedadString("TIPO_PROGRAMA_LAPEADO", PropiedadesRequeridasCadena);
            int numProgramam = Convert.ToInt32(Module.GetValorPropiedadString("NUM_PROGRAMA_LAPEADO", PropiedadesRequeridasCadena));
            bool banCaidaRadio = Module.GetValorPropiedadBool("BAN_CAIDA_RADIO", PropiedadesRequeridasBool);
            int numCarreras = Convert.ToInt32(Module.GetValorPropiedad("NUM_CARRERAS_LAPEADO", PropiedadesRequeridadas));

            double tiempoCiclo = GetTiempoCiclo(arbol, banCaidaRadio, numProgramam, numCarreras);
            if (arbol == "CORTO")
            {
                TiempoMachine = Math.Round((((tiempoCiclo + 17.59788) * widthNominal) / 75.96) * 100, 3);
            }
            else if (arbol == "LARGO")
            {
                TiempoMachine = Math.Round((((tiempoCiclo + 17.59788) * widthNominal) / 126) * 100, 3);
            }

            TiempoLabor = TiempoMachine * FactorLabor;
        }
        #endregion
        
        private double GetTiempoCiclo(string arbol, bool _llevaRadio, int programa, int numCarreras)
        {
            double tiempoCiclo = 0;
            if (arbol == "CORTO" & _llevaRadio == false)
            {
                if (programa == 1)
                {
                    tiempoCiclo = 60;
                }
                else if (programa == 2)
                {
                    tiempoCiclo = 120;
                }
                else if (programa == 3)
                {
                    tiempoCiclo = 180;
                }
                else if (programa == 28)
                {
                    tiempoCiclo = 30;
                }
                else if (programa == 29)
                {
                    tiempoCiclo = 40;
                }
                else if (programa == 30)
                {
                    tiempoCiclo = 50;
                }
                else if (programa == 44)
                {
                    tiempoCiclo = 0.625 * 1 * numCarreras;

                }
            }
            else if (arbol == "LARGO" & _llevaRadio == false)
            {
                if (programa == 4)
                {
                    tiempoCiclo = 15;
                }
                else if (programa == 5)
                {
                    tiempoCiclo = 20;
                }
                else if (programa == 6)
                {
                    tiempoCiclo = 28;
                }
                else if (programa == 7)
                {
                    tiempoCiclo = 60;
                }
                else if (programa == 8)
                {
                    tiempoCiclo = 90;
                }
                else if (programa == 9)
                {
                    tiempoCiclo = 120;
                }
                else if (programa == 10)
                {
                    tiempoCiclo = 150;
                }
                else if (programa == 11)
                {
                    tiempoCiclo = 180;
                }
                else if (programa == 12)
                {
                    tiempoCiclo = 42;
                }
                else if (programa == 13)
                {
                    tiempoCiclo = 60;
                }
                else if (programa == 14)
                {
                    tiempoCiclo = 120;
                }
                else if (programa == 15)
                {
                    tiempoCiclo = 360;
                }
                else if (programa == 44)
                {
                    tiempoCiclo = 0.625 * 1 * numCarreras;
                }
            }
            else if (arbol == "CORTO" & _llevaRadio == true)
            {
                if (programa == 16)
                {
                    tiempoCiclo = 60;
                }
                else if (programa == 17)
                {
                    tiempoCiclo = 90;
                }
                else if (programa == 18)
                {
                    tiempoCiclo = 60;
                }
                else if (programa == 19)
                {
                    tiempoCiclo = 120;
                }
                else if (programa == 31)
                {
                    tiempoCiclo = 60;
                }
            }
            else if (arbol == "LARGO" & _llevaRadio == true)
            {
                if (programa == 20)
                {
                    tiempoCiclo = 60;
                }
                else if (programa == 21)
                {
                    tiempoCiclo = 90;
                }
                else if (programa == 22)
                {
                    tiempoCiclo = 120;
                }
                else if (programa == 23)
                {
                    tiempoCiclo = 45;
                }
                else if (programa == 24)
                {
                    tiempoCiclo = 90;
                }
                else if (programa == 25)
                {
                    tiempoCiclo = 60;
                }
                else if (programa == 26)
                {
                    tiempoCiclo = 120;
                }
                else if (programa == 27)
                {
                    tiempoCiclo = 180;
                }
            }

            return tiempoCiclo;
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