using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Services.TiempoEstandar.Gasolina.PreMaquinado;

namespace View.Services.Operaciones.Gasolina.PreMaquinado
{
    public class Splitter : GenericOperation, IOperacion, IObserverWidth
    {
        #region Attibutes
        string proceso;
        double od, id;
        #endregion

        #region Constructors

        public Splitter(Anillo _elAnillo)
        {
            NombreOperacion = "SPLITTER CASTINGS";
            CentroCostos = "32014170";
            CentroTrabajo = "130";
            ControlKey = "MA42";
            elPlano = _elAnillo;

            ListaHerramentales = new ObservableCollection<Herramental>();
            ListaMateriaPrima = new ObservableCollection<MateriaPrima>();
            ListaPropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            NotasOperacion = new ObservableCollection<string>();
            AlertasOperacion = new ObservableCollection<string>();
            
            CalcularWidth();
        }

        #endregion

        #region Properties of IOperacion

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

        #region Properties of IObserverWidth
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

        #region Methods

        private void CalcularWidth()
        {
            proceso = Module.GetValorPropiedadString("Proceso", elPlano.PerfilOD.PropiedadesCadena);

            WidthOperacion = DataManager.GetWidthSplitterCasting(proceso, elPlano.H1.Valor);

            if (WidthOperacion == 0)
            {
                AlertasOperacion.Add("No se encontró el width de la operación. Favor de checar la tabla SplitterSpacerChart. Cálculo de width en la hoja de ruta incorrecto.");
            }
            else
            {
                double? widthFirstRoughGrind = DataManager.GetWidthFirstRoughGrind(proceso, elPlano.H1.Valor);

                if (widthFirstRoughGrind != null && widthFirstRoughGrind > 0)
                {
                    MatRemoverWidth = Convert.ToDouble(widthFirstRoughGrind) - WidthOperacion;
                }
                else
                {
                    AlertasOperacion.Add("No se encontró el width de la operación First Rough Grind. Favor de checar la tabla SplitterSpacerChart. Cálculo de width en la hoja de ruta incorrecto.");
                }
            }
        }
        
        #region Methods of IOperacion
        /// <summary>
        /// Método en el cual se calcula la operación.
        /// </summary>
        /// <param name="ElAnilloProcesado">Anillo que presenta el anillo como se recibe de la operación anterior.</param>
        /// <param name="elPlano">Anillo que representa el plano que ingresó el usuario.</param>
        public void CrearOperacion(Anillo ElAnilloProcesado, Anillo _elPlano)
        {

            anilloProcesado = ElAnilloProcesado;
            elPlano = _elPlano;

            double timeSplitter = DataManager.GetCycleTimeSplitter(elPlano.MaterialBase.Especificacion);

            if (timeSplitter == 0)
            {
                AlertasOperacion.Add("No se encontró el tiempo ciclo de la operación. Favor de verificar la tabla TiempoCicloSplitter");
            }

            TextoProceso += "*SPLIT \n";
            TextoProceso += "" + Convert.ToString(WidthOperacion) + " +- .004 CYC TIME " + timeSplitter + " +- 2 SEG." + "\n";
            
            od = DataManager.GetODSplitterCasting(elPlano.MaterialBase.Codigo);
            id = DataManager.GetIDSplitterCasting(elPlano.MaterialBase.Codigo);
            double diff = od - id;
            TextoProceso += "O.D " + od + " I.D " + id + " DIFF. " + diff + "\n";

            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el método para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {
            foreach (var item in DataManager.GetSpacerSplitterCastings(proceso, elPlano.H1.Valor))
            {
                ListaHerramentales.Add(item);
            }

            ListaHerramentales.Add(DataManager.GetCutterSplitterCasting(0.031));

            ListaHerramentales.Add(DataManager.GetChuckSplitter(id));

            if (DataManager.GetHasUretanoSplitter(id))
            {
                ListaHerramentales.Add(DataManager.GetUretanoSplitter(id));
            }
            
            foreach (var Herramental in ListaHerramentales)
            {
                TextoHerramienta += Herramental.DescripcionRuta + "\n";
            }
        }

        /// <summary>
        /// Método en el cual se calculan los tiempos estandar.
        /// </summary>
        public void CalcularTiemposEstandar()
        {
            try
            {
                CentroTrabajo130 objTiempo = new CentroTrabajo130();

                objTiempo.Calcular(anilloProcesado);

                TiempoLabor = objTiempo.TiempoLabor;
                TiempoMachine = objTiempo.TiempoMachine;
                TiempoSetup = objTiempo.TiempoSetup;

                //Verificamos si no se generaron alertas durante el calculo de tiempos.
                if (objTiempo.Alertas.Count > 0)
                {
                    AlertasOperacion.Add("Error en calculo de tiempo estándar");
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
                AlertasOperacion.Add("Error en cálculo de tiempos estándar. \n" + er.StackTrace);
            }

        }
        #endregion

        #region Methods of IObserverWidth

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
        
        /// <summary>
        /// Método que establece que cantidad de material a remover va tener la operación.
        /// </summary>
        /// <param name="operaciones"></param>
        /// <param name="posOperacion"></param>
        public void setMaterialRemover(ObservableCollection<IOperacion> operaciones, int posOperacion)
        {
            
        }
        #endregion

        #endregion

        #region Methods override
        public override string ToString()
        {
            return NombreOperacion;
        }
        #endregion
    }
}
