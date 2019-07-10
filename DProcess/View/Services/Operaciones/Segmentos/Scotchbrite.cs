using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.Operaciones.Segmentos
{
    public class Scotchbrite : GenericOperation, IOperacion
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

        #region Methods

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
            //Opción 1
            TextoProceso = "OD BRUSH" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "FRECUENCIA DE GIRO DE MOTOR DE LIJA 45HZ" + Environment.NewLine;
            TextoProceso += "FRECUENCIA DE GIRO DE MANDRIL 45HZ" + Environment.NewLine;
            TextoProceso += "NUMERO DE CICLOS POR CARGA 1" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "NOTA: APROXIMAR RUEDA DE FLAP BRUSH A TOPE DEL MANDRIL Y DAR UNA VUELTA MAS PARA APLICAR LA CARGA DE LA RUEDA" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "*********************************************" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "DESENSAMBLE" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "INSPECCION VISUAL DE MATERIAL" + Environment.NewLine;
            TextoProceso += "(SIN DESPRENDIMIENTO DE PVD Y SIN PITTING)" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "LLEVAR MUESTRAS A LABORATORIO DE ACUERDO A HII" + Environment.NewLine;

            //Opción 2
            TextoProceso = "*SCOTCHBRITE" + Environment.NewLine;
            TextoProceso += "CARGA AL MOTOR  65 - 85 %" + Environment.NewLine;
            TextoProceso += "TIEMPO CICLO    20 SEG/CICLO" + Environment.NewLine;
            TextoProceso += "CANTIDAD DE CICLOS 2" + Environment.NewLine;
            TextoProceso += "USAR RUEDA  8SFIN" + Environment.NewLine;
            TextoProceso += "" + " DE CAIDA A " + "" + " GAGE" + Environment.NewLine;
            TextoProceso += "RADIO " + "" + " EN " + "" + " DEL CENTRO" + Environment.NewLine;
            TextoProceso += "CARTA COMPARADOR " + Environment.NewLine;
            TextoProceso += "MEDIR EL PERFIL A 10 PZAS CADA 4 RIELES DE MAT." + Environment.NewLine;
            TextoProceso += "VERIFICAR EL PERFIL DE LA CARA DE" + Environment.NewLine;
            TextoProceso += "CONTACTO DE ACUERDO A  PROCEDIMIENTO" + Environment.NewLine;
            TextoProceso += "4.10-1.5.15" + Environment.NewLine;
            TextoProceso += "NO DOBLAR LAS PIEZAS" + Environment.NewLine;
            TextoProceso += "ACABADO: CARA DE CONTACTO DE LOS" + Environment.NewLine;
            TextoProceso += "SEGMENTOS DEBE SER CONTINUO" + Environment.NewLine;
            TextoProceso += "HACER PRUEBA DE LUZ" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;

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

            }
            catch (Exception er)
            {
                //Si ocurrio algún error, lo agregamos a la lista de alertas de la operación.
                AlertasOperacion.Add("Error en cálculo de tiempos estándar. \n" + er.StackTrace);
            }
        }

        public void InicializarDatosGenerales()
        {
            //Asignamos los valores por default a las propiedades.
            NombreOperacion = "SCOTCHBRITE O.D.";
            CentroCostos = "";
            CentroTrabajo = "466";
            ControlKey = "MA42";
            ListaHerramentales = new ObservableCollection<Herramental>();
            ListaMateriaPrima = new ObservableCollection<MateriaPrima>();
            ListaPropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            AlertasOperacion = new ObservableCollection<string>();
            NotasOperacion = new ObservableCollection<string>();
        }
        #endregion

        #region Methods override
        public override string ToString()
        {
            return NombreOperacion;
        }
        #endregion 

        #endregion

        #region Constructors
        public Scotchbrite(Anillo plano)
        {
            InicializarDatosGenerales();
            elPlano = plano;
        }

        public Scotchbrite()
        {
            InicializarDatosGenerales();
        }
        #endregion
    }
}
