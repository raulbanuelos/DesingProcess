﻿using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.Operaciones.Generica
{
    public class OperacionGenericaGAP : GenericOperation, IOperacion, IObserverDiametro
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
        /// Cadena que representa el id XML de la operación.
        /// </summary>
        public string IdXML { get; set; }

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

        public bool GapFijo
        {
            get;

            set;
        }

        private bool _RemueveGap = true;
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
            TextoProceso = String.Format("{0:0.00000}", Diameter) + "   GA. " + String.Format("{0:0.000}", Gap) + " \n";

            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el méotodo para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {
            ObservableCollection<Herramental> ListaBushing = new ObservableCollection<Herramental>();
            Herramental bushing = new Herramental();
            Herramental pusher = new Herramental();
            Herramental guillotina = new Herramental();

            DataManager.GetBushingSim(elPlano.D1.Valor, out ListaBushing);

            if (ListaBushing.Count > 0)
                bushing = ListaBushing[0];

            if (bushing.Encontrado)
            {
                double dimBBushing;
                dimBBushing = Module.GetValorPropiedad("DimB", bushing.Propiedades);
                ObservableCollection<Herramental> ListaPusher = new ObservableCollection<Herramental>();
                DataManager.GetPusherSim(dimBBushing, out ListaPusher);

                if (ListaPusher.Count > 0)
                    pusher = ListaPusher[0];
            }

            ObservableCollection<Herramental> ListaGuillotinas = new ObservableCollection<Herramental>();
            DataManager.GetGuillotinaSim(elPlano.H1.Valor, out ListaGuillotinas);

            if (ListaGuillotinas.Count > 0)
                guillotina = ListaGuillotinas[0];

            ListaHerramentales.Add(bushing);
            ListaHerramentales.Add(pusher);
            ListaHerramentales.Add(guillotina);

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
            NombreOperacion = "";
            CentroCostos = "";
            CentroTrabajo = "";
            ControlKey = "MA42";
            IdXML = "IDOperacionGap";

            ListaHerramentales = new ObservableCollection<Herramental>();
            ListaMateriaPrima = new ObservableCollection<MateriaPrima>();
            ListaPropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            AlertasOperacion = new ObservableCollection<string>();
            NotasOperacion = new ObservableCollection<string>();
            MatRemoverDiametro = 0.0125; // <--Significan .004 totales
        }
        #endregion

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

        #region Methods override
        public override string ToString()
        {
            return NombreOperacion;
        }
        #endregion
        #endregion

        #region Constructors
        public OperacionGenericaGAP()
        {
            NombreOperacion = "OPERACIÓN GAP";
        } 
        #endregion
    }
}
