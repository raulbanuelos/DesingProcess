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
    public class PVDCoatingWash : GenericOperation, IOperacion
    {
        #region Attributes
        double medidaManga = 0; 
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
            //Asignamos el valor del anillor procesado al anillo de la operación.
            anilloProcesado = ElAnilloProcesado;

            //Agregamos el texto con las instrucciones de la operación.
            TextoProceso = "WASHING (NOVATEC)" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "UTILIZAR RECETA:   PROGRAMA 3" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "DISTRIBUCIÓN DE BOBINAS:" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "BOBINA/RIEL:        1 BOBINA" + Environment.NewLine;
            TextoProceso += "RIELES/CANASTILLA:  6 MAXIMO" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "DESPÚES DE DESENGRASE UTILIZAR GUANTES DE LATEX O NITRILO PARA MANIPULAR EL MATERIAL" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "**********************************************" + Environment.NewLine;
            TextoProceso += "ENSAMBLE DE HERRAMENTAL Y BOBINAS" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            
            double d1mm = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), elPlano.D1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Milimeter), elPlano.D1.Valor);
            Propiedad a1Min = Module.GetPropiedad("a1 Min", elPlano.PerfilID.Propiedades);
            Propiedad a1Max = Module.GetPropiedad("a1 Max", elPlano.PerfilID.Propiedades);

            double a1Minmm = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), a1Min.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Milimeter), a1Min.Valor);
            double a1Maxmm = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), a1Max.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Milimeter), a1Max.Valor);
            
            double a1 = (a1Minmm + a1Maxmm) / 2;

            if (elPlano.MaterialBase.TipoDeMaterial == "ACERO INOXIDABLE")
            {
                //Actualizamos la tabla preparando los valores.
                DataManager.UpdateRecordsMangaPVDInoxidable(a1, d1mm);

                double n = a1 / d1mm;

                medidaManga = DataManager.GetMangaPVDWashAceroInoxidable(n);
            }
            else
            {
                //Actualizamos la tabla preparando los valores.
                DataManager.UpdaterecorsMangaPVDCarbon(a1, d1mm);

                double n = a1 / d1mm;

                medidaManga = DataManager.GetMangaPVDWashAceroCarbon(n);
            }
            
            TextoProceso += "MANGA " + medidaManga + " mm" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "ENSAMBLE MANUAL Y MEDIR CON FLEXÓMETRO LA LONGITUD TOTAL DE BOBINAS  IGUAL A 637 mm" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "NOTA:" + Environment.NewLine;
            TextoProceso += "APRIETE DE COLLARINES CON LA FUERZA DE LA MANO SIN APRETAR EXCESIVAMENTE." + Environment.NewLine;

            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el méotodo para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {
            ListaHerramentales.Add(DataManager.GetMangaPVDWash(medidaManga));

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
            NombreOperacion = "PVD/DLC COATING WASH";
            CentroCostos = "";
            CentroTrabajo = "730";
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
        public PVDCoatingWash(Anillo plano)
        {
            InicializarDatosGenerales();
            elPlano = plano;
        }

        public PVDCoatingWash()
        {
            InicializarDatosGenerales();
        }
        #endregion
    }
}
