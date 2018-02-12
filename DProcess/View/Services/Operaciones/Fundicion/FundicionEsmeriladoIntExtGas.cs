using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Services.TiempoEstandar.Fundicion;

namespace View.Services.Operaciones.Fundicion
{
    public class FundicionEsmeriladoIntExtGas : IOperacion
    {
        #region Propiedades de IOperacion
        /// <summary>
        /// Cadena que representa las instrucciones de una operación en la hoja de ruta.
        /// </summary>
        public string TextoProceso { get; set; }

        /// <summary>
        /// Cadena que representa las dimenciones de las herramientas de una operación en la hoja de ruta.
        /// </summary>
        public string TextoHerramienta { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TextoSyteline
        {
            get
            {
                return NombreOperacion + Environment.NewLine + "MET=000 OPR= 00" + Environment.NewLine + TextoProceso + Environment.NewLine + "TOOLING" + Environment.NewLine + TextoHerramienta;
            }
            set
            {
                TextoSyteline = value;
            }
        }

        /// <summary>
        /// Cadena que representa el nombre de la operación en ingles.
        /// </summary>
        public string NombreOperacion { get; set; }

        /// <summary>
        /// Cadena que representa el número de centro de trabajo de la operación.
        /// </summary>
        public string CentroTrabajo { get; set; }

        /// <summary>
        /// Cadena que representa el centro de costos de la operación.
        /// </summary>
        public string CentroCostos { get; set; }

        /// <summary>
        /// Cadena que representa el control key de la operación, esto para control de ERP.
        /// </summary>
        public string ControlKey { get; set; }

        /// <summary>
        /// Entero que representa el número de operación en hoja de ruta.
        /// </summary>
        public int NoOperacion { get; set; }

        /// <summary>
        /// Double que presenta el tiempo maquina de la operación.
        /// </summary>
        public double TiempoMachine { get; set; }

        /// <summary>
        /// Double que representa el tiempo de preparación de la operación.
        /// </summary>
        public double TiempoSetup { get; set; }

        /// <summary>
        /// Double que representa el tiempo de trabajo hombre de la operación.
        /// </summary>
        public double TiempoLabor { get; set; }

        /// <summary>
        /// Booleano que representa si una operación esta en ejecucioón(Ejecutando el métod CrearOperacion())
        /// </summary>
        public bool IsWorking { get; set; }

        /// <summary>
        /// Collección que representa la lista de herramentales de la operación.
        /// </summary>
        public ObservableCollection<Herramental> ListaHerramentales { get; set; }

        /// <summary>
        /// Colección que representa todas las propiedades adquiridas en la operación.
        /// </summary>
        /// <example>
        /// RPM en First Rougth Grind
        /// </example>
        public ObservableCollection<Propiedad> ListaPropiedadesAdquiridasProceso { get; set; }

        /// <summary>
        /// Colección que representa las alertas y posibles causas que puedan generar un mal cálculo dentro de la operación.
        /// </summary>
        public ObservableCollection<string> AlertasOperacion { get; set; }

        /// <summary>
        /// Colección que representa las notas de información al usuario de calculos relevantes en la operación.
        /// </summary>
        public ObservableCollection<string> NotasOperacion { get; set; }

        /// <summary>
        /// Colección de Materia prima que representan todas las materias primas utilizadas en la operación.
        /// </summary>
        public ObservableCollection<MateriaPrima> ListaMateriaPrima { get; set; }

        /// <summary>
        /// Anillo que presenta el anillo físico con las medidas que llega a la operación.
        /// </summary>
        public Anillo anilloProcesado { get; set; }

        /// <summary>
        /// Anillo que representa el plano ingresado por el usuario.
        /// </summary>
        public Anillo elPlano { get; set; }
        #endregion

        #region Métodos de IOperacion
        /// <summary>
        /// Método en el cual se calcula la operación.
        /// </summary>
        /// <param name="ElAnilloProcesado">Anillo que presenta el anillo como se recibe de la operación anterior.</param>
        /// <param name="elPlano">Anillo que representa el plano que ingresó el usuario.</param>
        public void CrearOperacion(Anillo ElAnilloProcesado, Anillo elPlano)
        {
            //Asignamos el valor del anillor procesado al anillo de la operación.
            anilloProcesado = ElAnilloProcesado;

            //Agregamos el texto con las instrucciones de la operación.
            double tol_width = 0.005;
            double widthMin, widthMax = 0.0;
            double tol_thick = 0.010;
            double thickMin, thickMax = 0.0;

            double widthCasting = Module.GetValorPropiedad("WidthCasting", elPlano.PropiedadesAdquiridasProceso);
            widthCasting = widthCasting + 0.010;
            widthMin = widthCasting - tol_width;
            widthMax = widthCasting + tol_width;

            double PattThicknessCasting = Module.GetValorPropiedad("PattThicknessCasting", elPlano.PropiedadesAdquiridasProceso);
            PattThicknessCasting = PattThicknessCasting + 0.015;
            thickMin = PattThicknessCasting - tol_thick;
            thickMax = PattThicknessCasting + tol_thick;

            TextoProceso = "WIDTH " + String.Format("{0:0.000}", widthMin) + " - " + String.Format("{0:0.000}", widthMax) + Environment.NewLine;
            TextoProceso += "THICKNESS " + String.Format("{0:0.000}", thickMin) + " - " + String.Format("{0:0.000}", thickMax) + Environment.NewLine;

            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el méotodo para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {

        }

        /// <summary>
        /// Método en el cual se calculan los tiempos estandar.
        /// </summary>
        public void CalcularTiemposEstandar()
        {
            try
            {
                //Declaramos un objeto del tipo CentroTrabajo010.
                CentroTrabajo040 objTiempos = new CentroTrabajo040();

                //Ejecutamos el método para calcular los tiempos.
                objTiempos.Calcular(elPlano);

                //Mapeamos los valores correspondientes.
                this.TiempoLabor = objTiempos.TiempoLabor;
                this.TiempoMachine = objTiempos.TiempoMachine;
                this.TiempoSetup = objTiempos.TiempoSetup;

                //Verificamos si no se generaron alertas durante el calculo de tiempos.
                if (objTiempos.Alertas.Count > 0)
                {
                    AlertasOperacion.Add("Error en calculo de tiempo estándar");
                    AlertasOperacion.CopyTo(objTiempos.Alertas.ToArray(), 0);
                }
                else
                {
                    NotasOperacion.Add("Tiempos estándar calculados correctamente.");
                }
            }
            catch (Exception er)
            {
                //Si ocurrio algún error, lo agregamos a la lista de alertas de la operación.
                AlertasOperacion.Add("Error en cálculo de tiempos estándar. \n" + er.StackTrace);
            }
        }
        #endregion

        #region Constructors
        public FundicionEsmeriladoIntExtGas(Anillo plano)
        {
            //Asignamos los valores por default a las propiedades.
            NombreOperacion = "FUND.ESMERILADO INT. Y EXT";
            CentroCostos = "32011542";
            CentroTrabajo = "040";
            ControlKey = "MA40";
            elPlano = plano;
            ListaHerramentales = new ObservableCollection<Herramental>();
            ListaMateriaPrima = new ObservableCollection<MateriaPrima>();
            ListaPropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            NotasOperacion = new ObservableCollection<string>();
            AlertasOperacion = new ObservableCollection<string>();
        }
        #endregion

        #region Methods override
        public override string ToString()
        {
            return NombreOperacion;
        }
        #endregion
    }
}
