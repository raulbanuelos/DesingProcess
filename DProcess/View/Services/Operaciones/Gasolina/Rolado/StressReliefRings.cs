using Model;
using Model.Interfaces;
using System;
using System.Collections.ObjectModel;
using View.Services.TiempoEstandar.Gasolina.Rolado;

namespace View.Services.Operaciones.Gasolina.Rolado
{
    public class StressReliefRings : GenericOperation, IOperacion
    {
        #region Properties
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
            TextoProceso = "*RELEVADO DE ESFUERZOS \n";
            TextoProceso += "RECETA 6 \n";
            TextoProceso += "TEMPERATURA 420 +- 5 GRA CENT \n";
            TextoProceso += "TIEMPO: 90 +- 2 MIN \n";
            TextoProceso += "OLD XXXX  +- .13 MM \n";
            TextoProceso += "OVALIDAD  -0.33 A .33 MM \n";
            TextoProceso += "TENSION 2.00 +- 0.60 LBS F.T. A 3.4055 \n";
            TextoProceso += "TENSION 2.00 +- 0.60 LBS F.T. A 86.49 MM \n";
            TextoProceso += "\n";
            TextoProceso += "MANTENER EL CICLO DE TRATAMIENTO TERMICO \n";
            TextoProceso += "DE ACUERDO AL MANUAL 4.9-2.89 \n";
            TextoProceso += "\n";
            TextoProceso += "POINT DEFELECTION 0 A 0.001 \n";
            TextoProceso += "USAR UN GAGE CON 30 GRADOS DE ABERTURA \n";
            TextoProceso += "\n";
            TextoProceso += "NOTA: ALINEAR CON EL GROOVE EN DIRECCIÓN HACIA EL OPERADOR Y \n";
            TextoProceso += "MARCAR LOS\n";
            TextoProceso += "ANILLOS DE LADO DERECHO\n";



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
                //Declaramos un objeto del tipo CentroTrabajo495.
                CentroTrabajo496 objTiempo = new CentroTrabajo496();

                //Ejecutamos el método para calcular los tiempos.
                objTiempo.Calcular(anilloProcesado);

                //Mapeamos los valores correspondientes.
                this.TiempoLabor = objTiempo.TiempoLabor;
                this.TiempoMachine = objTiempo.TiempoMachine;
                this.TiempoSetup = objTiempo.TiempoSetup;

                //Verificamos si no se generaron alertas durante el calculo de tiempos.
                if (objTiempo.Alertas.Count > 0)
                {
                    AlertasOperacion.Add("Error en cálculo de tiempo estándar.");
                    AlertasOperacion.CopyTo(objTiempo.Alertas.ToArray(), 0);
                }
                else
                {
                    NotasOperacion.Add("Tiempos estándar calculados correctamente.");
                }
            }
            catch (Exception er)
            {
                //Si ocurrio algún error, lo agregamos a la lista de alertas de la operación.
                AlertasOperacion.Add("Error en cálculo de tiempos estádar. \n" + er.StackTrace);
            }
        }

        public void InicializarDatosGenerales()
        {
            //Asignamos los valores por default a las propiedades.
            NombreOperacion = "STRESS RELIEF FOR STEEL COMP. RINGS";
            CentroCostos = "32012674";
            CentroTrabajo = "496";
            ControlKey = "MA42";
            ListaHerramentales = new ObservableCollection<Herramental>();
            ListaMateriaPrima = new ObservableCollection<MateriaPrima>();
            ListaPropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            AlertasOperacion = new ObservableCollection<string>();
        }
        #endregion

        #region Constructors
        public StressReliefRings(Anillo plano)
        {
            InicializarDatosGenerales();
            elPlano = plano;
        }

        public StressReliefRings()
        {
            InicializarDatosGenerales();
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
