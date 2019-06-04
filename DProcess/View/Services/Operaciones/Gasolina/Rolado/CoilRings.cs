using Model;
using Model.Interfaces;
using System;
using System.Collections.ObjectModel;
using View.Services.TiempoEstandar.Gasolina.Rolado;

namespace View.Services.Operaciones.Gasolina.Rolado
{
    public class CoilRings : GenericOperation, IOperacion,IObserverDiametro 
    {

        #region Properties

        private double widthMateriaPrima;
        private double thicknessMateriaPrima;
        private MateriaPrimaRolado materiaPrima;

        #region Properties of IObserverDiametro
        public double Diameter
        {
            get;
            set;
        }

        public double MatRemoverDiametro
        {
            get;
            set;
        }

        public double Gap
        {
            get;

            set;
        }

        private bool _RemueveGap = false;
        public bool RemueveGap
        {
            get
            {
                return _RemueveGap;
            }

            set
            {
                _RemueveGap = value;
            }
        }
        #endregion

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
        #region Methods of IObserverDiametro
        public void UpdateState(ISubjectDiametro sender, double MaterialRemoverAfterOperacion, double DiametroAfterOperacion, double GapAfterOperacion, bool RemueveGap)
        {
            if (RemueveGap)
            {
                double p, q;
                p = (MaterialRemoverAfterOperacion / Math.PI);
                q = ((GapAfterOperacion - Gap) / Math.PI);
                Diameter = Math.Round(p - q + (DiametroAfterOperacion), 3);
            }
            else
            {
                double p, q;
                p = Math.Round((Gap - GapAfterOperacion) / 3.1416, 4);
                q = DiametroAfterOperacion + MaterialRemoverAfterOperacion;
                Diameter = p + q;
            }
        }
        #endregion

        #region Métodos de IOperacion
        /// <summary>
        /// Método en el cual se calcula la operación.
        /// </summary>
        /// <param name="ElAnilloProcesado">Anillo que presenta el anillo como se recibe de la operación anterior.</param>
        /// <param name="elPlano">Anillo que representa el plano que ingresó el usuario.</param>
        public void CrearOperacion(Anillo ElAnilloProcesado, Anillo elPlano)
        {

            #region Pendiente. Es para la selección de herramental.
            int x = ListaMateriaPrima.Count;

            if (x > 0)
            {
                materiaPrima = (MateriaPrimaRolado)ListaMateriaPrima[0];
                widthMateriaPrima = materiaPrima._Width;
                thicknessMateriaPrima = materiaPrima.Thickness;
            }
            #endregion

            //Asignamos el valor del anillor procesado al anillo de la operación.
            anilloProcesado = ElAnilloProcesado;

            //Agregamos el texto con las instrucciones de la operación.
            TextoProceso = "*ROLADO DE ANILLOS \n";
            TextoProceso += "" + Diameter + "  GA.  " + Gap + " +- .006 \n";
            TextoProceso += "" + 
                Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance),EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), EnumEx.GetEnumDescription(DataManager.UnidadDistance.Milimeter),Diameter) + 
                "  GA.  " + Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), EnumEx.GetEnumDescription(DataManager.UnidadDistance.Milimeter), Gap) + " +- .15 (MM) \n";

            TextoProceso += "SIMETRIA: 0 +- 0.3 mm \n";
            TextoProceso += "PLANICIDAD CARAS LATERALES : CONTINUO EN \n";
            TextoProceso += "LOS 360 GRADOS DE LA CARA LATERAL \n";
            TextoProceso += "REFERENCIA DEL CALCULO=  07982176 \n";
            TextoProceso += "DIMENSIONES DE ALAMBRE = " + materiaPrima._Width + " X " + materiaPrima.Thickness + " S=" + materiaPrima.Groove + " \n";
            TextoProceso += "PROCEDIMIENTO  APLICABLE: 4.9 - 2.85 \n";
            TextoProceso += "\n";
            TextoProceso += "NOTA:PARA MEDICIONES INICIALES USAR HOJA \n";
            TextoProceso += "DE CALCULO DE COORDENADAS DE ACUERDO A LA ROLADORA \n";
            TextoProceso += "PARA EL COMPONENTE 07982176 \n";
            TextoProceso += "NOTA: EL NAPIER VA HACIA ABAJO. \n";

            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el méotodo para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {
            Herramental idealFeedRoller = new Herramental();
            DataManager.GetCOIL_Feed_Roller(widthMateriaPrima, out idealFeedRoller);
            ListaHerramentales.Add(idealFeedRoller);

            Herramental idealExitGuide = new Herramental();
            DataManager.GetEXIT_GUIDE(widthMateriaPrima, thicknessMateriaPrima, out idealExitGuide);
            ListaHerramentales.Add(idealExitGuide);

            Herramental idealCenterGuide = new Herramental();
            DataManager.GetCOIL_CENTER_GUIDE(widthMateriaPrima, thicknessMateriaPrima, out idealCenterGuide);
            ListaHerramentales.Add(idealCenterGuide);
            
            TextoHerramienta = Module.GetTextoListaHerramentales(ListaHerramentales);
        }

        /// <summary>
        /// Método en el cual se calculan los tiempos estandar.
        /// </summary>
        public void CalcularTiemposEstandar()
        {
            try
            {
                //Declaramos un objeto del tipo CentroTrabajo495.
                CentroTrabajo495 objTiempo = new CentroTrabajo495();

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
                AlertasOperacion.Add("Error en cálculo de tiempos estándar. \n" + er.StackTrace);
            }
        }

        public void InicializarDatosGenerales()
        {
            //Asignamos los valores por default a las propiedades.
            NombreOperacion = "COIL (RINGS)";
            CentroCostos = "32012674";
            CentroTrabajo = "495";
            ControlKey = "MA42";

            ListaHerramentales = new ObservableCollection<Herramental>();
            ListaMateriaPrima = new ObservableCollection<MateriaPrima>();
            ListaPropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            AlertasOperacion = new ObservableCollection<string>();
            NotasOperacion = new ObservableCollection<string>();
        }
        #endregion
        #endregion

        #region Constructors
        public CoilRings(Anillo plano)
        {
            InicializarDatosGenerales();
            elPlano = plano;
        }

        public CoilRings()
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
