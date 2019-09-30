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
    public class Bobinado : GenericOperation, IOperacion
    {
        #region Attributes
        double thicknessMin, thicknessMax;
        #endregion

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
            //Agregamos la materia prima.
            ListaMateriaPrima.Add(elPlano.MaterialBase);

            //Asignamos el valor del anillor procesado al anillo de la operación.
            anilloProcesado = ElAnilloProcesado;

            thicknessMin = Module.GetValorPropiedadMin("a1", elPlano.PerfilID.Propiedades, true);
            thicknessMax = Module.GetValorPropiedadMax("a1", elPlano.PerfilID.Propiedades, true);
            
            double diaBobinado = elPlano.D1.Valor;
            double pesoAlambre = Math.Round((elPlano.D1.Valor)*(Math.PI) * (elPlano.H1.Valor) * (((thicknessMin + thicknessMax) / 2)) * (128.5),2);

            //Obtenemos las medidas de la materia prima.
            double espesorAxialMP = 0;
            double espesorRadialMP = 0;
            
            espesorAxialMP = Module.GetValorPropiedad("espesorAxialMP", elPlano.MaterialBase.Propiedades);
            espesorRadialMP = Module.GetValorPropiedad("espesorRadialMP", elPlano.MaterialBase.Propiedades);
            
            //Agregamos el texto con las instrucciones de la operación.
            TextoProceso = "*BOBINADO" + Environment.NewLine;
            TextoProceso += "LONG DE LA BOBINA 6.330-.000+.072" + Environment.NewLine;
            TextoProceso += "PULL - BACK 8%" + Environment.NewLine;
            TextoProceso += "VELOCIDAD 250 RPM +/- 25 RPM" + Environment.NewLine;
            TextoProceso += "PRESION DEL ROLADOR SUPERIOR 15 - 45PSI" + Environment.NewLine;
            TextoProceso += "DIA DE LA BOBINA  " + diaBobinado + " +- .004" + Environment.NewLine;
            TextoProceso += "PESO ALAMBRE  " + pesoAlambre + Environment.NewLine;
            TextoProceso += "ESPESOR 0.0169MAX. DEBIDO AL PROCESO" + Environment.NewLine;
            TextoProceso += "MEDIDA DEL ALAMBRE REF." + Environment.NewLine;
            TextoProceso += "" + espesorAxialMP + "	+/- 0.0003		"+ espesorRadialMP +"	+/-0.0017" + Environment.NewLine;
            TextoProceso += "CODIGO ALMACEN: " + elPlano.MaterialBase.Codigo + Environment.NewLine;
            
            if (Module.IsNormaSelected(elPlano.ListaNormas, "ES-349-1"))
            {
                double separador = espesorAxialMP + .002;
                TextoProceso += "HELICE: HACER PRUEBA DE PLACAS PARALELAS" + Environment.NewLine;
                TextoProceso += "CON UN SEPARADOR DE  " + separador + Environment.NewLine;
            }

            if (Module.IsNormaSelected(elPlano.ListaNormas, "ES-349-2"))
            {
                double gra = (Math.PI / 180) * 1;
                double senogrados = Math.Sin(gra);
                double dish = Math.Round(senogrados * diaBobinado, 4);
                TextoProceso += "DISH: " + dish + " MAX" + Environment.NewLine;
            }
            
            TextoProceso += "APARENCIA: SIN GOLPES EN EL DIAMETRO" + Environment.NewLine;
            TextoProceso += "EXTERIOR" + Environment.NewLine;
            
            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el méotodo para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {
            thicknessMin = Module.GetValorPropiedadMin("a1", elPlano.PerfilID.Propiedades, true);
            thicknessMax = Module.GetValorPropiedadMax("a1", elPlano.PerfilID.Propiedades, true);
            double a1 = Math.Round( (thicknessMin + thicknessMax) / 2,4);

            double d1Inch = Module.ConvertTo(elPlano.D1.TipoDato, elPlano.D1.Unidad,EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), elPlano.D1.Valor);
            double h1Inch = Module.ConvertTo(elPlano.H1.TipoDato, elPlano.H1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), elPlano.H1.Valor);
            
            ListaHerramentales.Add(DataManager.GetLowerRollBobinadoSegmentos(a1, d1Inch));
            ListaHerramentales.Add(DataManager.GetUpperRollBobinadoSegmentos(a1, d1Inch));
            ListaHerramentales.Add(DataManager.GetTargetRollBobinadoSegmentos(h1Inch, d1Inch));
            ListaHerramentales.Add(DataManager.GetCenterWaferBobinadoSegmentos(h1Inch, d1Inch));

            TextoHerramienta = Module.GetTextoListaHerramentales(ListaHerramentales);
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
            NombreOperacion = "BOBINADO SEGMENTOS";
            CentroCostos = "32012536";
            CentroTrabajo = "510";
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
        public Bobinado(Anillo plano)
        {
            InicializarDatosGenerales();
            elPlano = plano;
        }

        public Bobinado()
        {
            InicializarDatosGenerales();
        }
        #endregion
    }
}
