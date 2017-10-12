using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.Operaciones.Gasolina.Maquinado
{
    public class CamTurn : IOperacion, IObserverDiametro, IObserverThickness
    {
        #region Constructor
        public CamTurn(Anillo plano)
        {
            //Asignamos los valores por default a las propiedades.
            NombreOperacion = "ROUGH CAM TURN";
            CentroCostos = "32012526";
            CentroTrabajo = "230";
            ControlKey = "MA42";
            elPlano = plano;
            ListaHerramentales = new ObservableCollection<Herramental>();
            ListaMateriaPrima = new ObservableCollection<MateriaPrima>();
            ListaPropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            NotasOperacion = new ObservableCollection<string>();
        }

        public CamTurn()
        {

        }
        #endregion

        #region Properties

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
        public string TextoSyteline { get; set; }

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

        public bool RemueveGap
        {
            get;

            set;
        }
        #endregion

        #region Properties of IObserverThickness
        public double Thickness
        {
            get;

            set;
        }

        public double MatRemoverThickness
        {
            get;

            set;
        }

        public bool TrabajaOD
        {
            get;

            set;
        } 
        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// Método que obtiene el calculo del small OD de la operación.
        /// </summary>
        /// <returns></returns>
        private double GetSmallOD()
        {
            double small_od = 0;
            double rise = GetRise();
            double valor_pc = Module.GetValorPropiedad("Piece", anilloProcesado.PropiedadesAdquiridasProceso);
            double diaFinishMill = GetDiaFinishMill();
            double diaBK = GetDiaBK();
            small_od = (((((rise * 0.478) - valor_pc) / -3.1416) + elPlano.D1.Valor) - rise + (diaFinishMill - diaBK)) + 0.002;
            return Math.Round(small_od, 4);
        }

        /// <summary>
        /// Método que obtiene el valor del diámetro de la operación Finish Mill primer paso.
        /// </summary>
        /// <returns></returns>
        private double GetDiaBK()
        {
            return Module.GetDiametroOperacion("FINISH MILL", 1, elPlano.Operaciones);
        }

        /// <summary>
        /// Método que obtiene el valor del diámetro de la operación B&K primer paso.
        /// </summary>
        /// <returns></returns>
        private double GetDiaFinishMill()
        {
            return Module.GetDiametroOperacion("AUTO. FINISH TURN",1,elPlano.Operaciones);
        }

        /// <summary>
        /// Método que calcula y retorna el valor de rise de la operación.
        /// </summary>
        /// <returns></returns>
        private double GetRise()
        {
            double rise = 0;
            double valor_pc = Module.GetValorPropiedad("Piece", anilloProcesado.PropiedadesAdquiridasProceso);
            double valor_pin_gage = DataManager.GetCamTurnConstant(elPlano, Module.GetValorPropiedadString("RingShape", elPlano.PerfilOD.PropiedadesCadena));
            rise = valor_pc * 64 * valor_pin_gage * Math.Pow(10, -4);
            return Math.Round(rise, 3);
        }

        #region Methods of IOperacion
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
            double small_od = GetSmallOD();

            TextoProceso = "*RGH CAM TURN \n";
            TextoProceso += "SMALL O.D " + small_od + " +- 0.0010 \n";
            TextoProceso += "RISE " + GetRise() + " +- 0.0010    MIN TH. " + Thickness + "\n";
            TextoProceso += "*RGH. MILL \n";
            TextoProceso += "" + Diameter + "  GA. " + Gap + " +- .0075 \n";

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
                AlertasOperacion.Add("Error en cálculo de tiempos estándar. \n" + er.StackTrace);
            }
        }
        #endregion

        #region Methods of IObserverDiametro
        public void UpdateState(ISubjectDiametro sender, double MaterialRemoverAfterOperacion, double DiametroAfterOperacion, double GapAfterOperacion, bool RemueveGap)
        {
            throw new NotImplementedException();
        }
        #endregion

        #region Methods of IOBserverThickness
        public void UpdateState(ISubjectThickness sender, double MaterialRemoverAfterOperacion, double ThicknessAfterOperacion)
        {
            throw new NotImplementedException();
        } 
        #endregion

        #endregion


    }
}
