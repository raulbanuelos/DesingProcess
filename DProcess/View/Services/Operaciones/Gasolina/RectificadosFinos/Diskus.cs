using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.Operaciones.Gasolina.RectificadosFinos
{
    public class Diskus : GenericOperation, IOperacion, IObserverWidth
    {
        #region Propiedades

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

        #region Propiedades de IObserverWidth
        /// <summary>
        /// Double que representa la medida del width del anillo en la operación.
        /// </summary>
        public double WidthOperacion { get; set; }

        /// <summary>
        /// Double que representa el material a remover en la operación.
        /// Si en la operación se agrega material(por ejemplo cromo lateral) el valor será negativo.
        /// </summary>
        public double MatRemoverWidth { get; set; }

        #endregion

        #endregion

        #region Métodos

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
            //TextoProceso = "Width operación: " + WidthOperacion;

            double widthDecimal = Math.Round(WidthOperacion * 25.4,3);

            TextoProceso = "*FIN GRIND \n";
            TextoProceso += "mm(1) (" + widthDecimal + " +- 0.0060) \n";
            TextoProceso += "\n";
            TextoProceso += "REF.BLOCK PATRON:\n";
            TextoProceso += "IN(" + WidthOperacion + " +- 0.0060)\n";
            TextoProceso += "ROUGHNESS 25 Ra MAX.\n";
            
            anilloProcesado.H1.Valor = WidthOperacion;

            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el méotodo para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {
            ObservableCollection<Herramental> listaDiscos = new ObservableCollection<Herramental>();
            List<string> ListaAlertasDisco = new List<string>();

            double piece = Module.GetValorPropiedad("piece", anilloProcesado.PropiedadesAdquiridasProceso);

            DataManager.GetDiscoDiskus(anilloProcesado.MaterialBase.Especificacion, anilloProcesado.D1.Valor, piece, anilloProcesado.H1.Valor, anilloProcesado.FreeGap.Valor, out listaDiscos, out ListaAlertasDisco);

            if (listaDiscos.Where(x => x.Encontrado == false).ToList().Count > 0)
                AlertasOperacion.Add("Herramental Disco no encontrado.");

            foreach (string alerta in ListaAlertasDisco)
            {
                AlertasOperacion.Add(alerta);
            }

            ListaHerramentales.Add(listaDiscos[0]);
            
            TextoHerramienta = Module.GetTextoListaHerramentales(ListaHerramentales);

        }

        /// <summary>
        /// Método en el cual se calculan los tiempos estandar.
        /// </summary>
        public void CalcularTiemposEstandar()
        {

        }
        #endregion

        #region Métodos de IObserverWidth

        /// <summary>
        /// Método que actualiza el valor del width en la operación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="MaterialRemoverAfterOperacion"></param>
        /// <param name="WidthAfterOperacion"></param>
        public void UpdateState(ISubjectWidth sender, double MaterialRemoverAfterOperacion, double WidthAfterOperacion)
        {

            //Actualizamos el width de la operación.
            WidthOperacion = WidthAfterOperacion + MaterialRemoverAfterOperacion;
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
        public Diskus(Anillo plano)
        {
            //Asignamos los valores por default a las propiedades.
            NombreOperacion = "FIN GRIND (DISKUS)";
            CentroCostos = "32012529";
            CentroTrabajo = "160";
            ControlKey = "MA42";
            MatRemoverWidth = 0.0005;
            elPlano = plano;
            ListaHerramentales = new ObservableCollection<Herramental>();
            ListaMateriaPrima = new ObservableCollection<MateriaPrima>();
            ListaPropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            AlertasOperacion = new ObservableCollection<string>();
            NotasOperacion = new ObservableCollection<string>();
        } 
        #endregion
    }
}
