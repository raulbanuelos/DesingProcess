using Model;
using Model.Interfaces;
using System;
using System.Collections.ObjectModel;
using View.Services.TiempoEstandar.Gasolina.Maquinado;

namespace View.Services.Operaciones.Gasolina.Maquinado
{
    public class CamTurn : GenericOperation,IOperacion, IObserverDiametro, IObserverThickness
    {

        #region Attributes
        private double small_od;
        private double valor_pc;
        string cam_detail;
        #endregion

        #region Constructor
        public CamTurn(Anillo plano)
        {
            InicializarDatosGenerales();
            elPlano = plano;
        }

        public CamTurn()
        {
            InicializarDatosGenerales();
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
            return Module.GetDiametroOperacion("AUTO. FINISH TURN", 1, elPlano.Operaciones);
        }

        /// <summary>
        /// Método que obtiene el valor del diámetro de la operación B&K primer paso.
        /// </summary>
        /// <returns></returns>
        private double GetDiaFinishMill()
        {
            return Module.GetDiametroOperacion("FINISH MILL", 1,elPlano.Operaciones);
        }

        /// <summary>
        /// Método que calcula y retorna el valor de rise de la operación.
        /// </summary>
        /// <returns></returns>
        private double GetRise()
        {
            double rise = 0;
            double valor_pc = Module.GetValorPropiedad("Piece", anilloProcesado.PropiedadesAdquiridasProceso);
            double valor_pin_gage = DataManager.GetCamTurnConstant(elPlano, Module.GetValorPropiedadString("RingShape", elPlano.PerfilOD.PropiedadesCadena),out cam_detail);
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
            small_od = GetSmallOD();

            TextoProceso = "*RGH CAM TURN \n";
            TextoProceso += "SMALL O.D " + small_od + " +- 0.0010 \n";
            TextoProceso += "RISE " + GetRise() + " +- 0.0010    MIN TH. " + Thickness + "\n";
            TextoProceso += "*RGH. MILL \n";
            TextoProceso += "" + Diameter + "  GA. " + Gap + " +- .0075 \n";

            valor_pc = Module.GetValorPropiedad("Piece", anilloProcesado.PropiedadesAdquiridasProceso);
            double cutterAngle = Math.Round(((valor_pc / elPlano.D1.Valor) + 0.0095), 2);

            TextoProceso += DataManager.GetCutterAngleCamTurn(cutterAngle) + "\n";

            string camTurn1 = DataManager.GetTimeCamTurn("CamTurn1", elPlano.MaterialBase.Especificacion) + " \n";
            TextoProceso += !camTurn1.Equals(string.Empty) ? camTurn1 : ". \n";

            string camturn2 = DataManager.GetTimeCamTurn("CamTurn2", elPlano.MaterialBase.Especificacion) + " \n";
            TextoProceso += !camturn2.Equals(string.Empty) ? camturn2 : ". \n";

            string camturn3 = DataManager.GetTimeCamTurn("CamTurn3", elPlano.MaterialBase.Especificacion) + " \n";
            TextoProceso += !camturn3.Equals(string.Empty) ? camturn3 : ". \n";

            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el método para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {
            //Buscamos el herramental Collar Spacer.
            foreach (Herramental herramental in DataManager.GetCollarSpacer(small_od, valor_pc))
            {
                ListaHerramentales.Add(herramental);
            }

            //Buscamos el herramental CAM
            ListaHerramentales.Add(DataManager.GetWorkCam(cam_detail));

            //Porta inserto.
            Herramental herramentalPortaInserto;
            herramentalPortaInserto = DataManager.GetHerramental("1005389");
            herramentalPortaInserto.DescripcionRuta = herramentalPortaInserto.Encontrado && herramentalPortaInserto.Activo ? "PORTA INSERTO FNH-100704-TH" : "";
            ListaHerramentales.Add(herramentalPortaInserto);

            //Inserto
            string tipoMaterial = DataManager.GetTipoMaterial(elPlano.MaterialBase.Especificacion);
            Herramental inserto;
            if (tipoMaterial.Equals("HIERRO GRIS ALTO MODULO"))
            {
                inserto = DataManager.GetHerramental("1005389");
                inserto.DescripcionRuta = inserto.Encontrado && inserto.Activo ? "INSERTO RCMT0803MO H13A" : "";
            }
            else
            {
                inserto = DataManager.GetHerramental("1078275");
                inserto.DescripcionRuta = inserto.Encontrado && inserto.Activo ? "INSERTO RCGT-0803" : "";
            }
            ListaHerramentales.Add(inserto);
            
            //Queda pendiente el calculo de la busqueda de cutter debido a que se va a consultar con Mú la selección. 23 FEB 2018
            //Herramental cutter = DataManager.GetCutterCamTurn(elPlano.H1.Valor, elPlano.MaterialBase.Especificacion.Valor);
            //ListaHerramentales.Add(cutter);

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
                //Declaramos un objeto del tipo CentroTrabajo110.
                CentroTrabajo230 objTiempos = new CentroTrabajo230();

                //Ejecutamos el método para calcular los tiempos.
                objTiempos.Calcular(anilloProcesado);

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

        public void InicializarDatosGenerales()
        {
            //Asignamos los valores por default a las propiedades.
            NombreOperacion = "ROUGH CAM TURN";
            CentroCostos = "32012526";
            CentroTrabajo = "230";
            ControlKey = "MA42";

            ListaHerramentales = new ObservableCollection<Herramental>();
            ListaMateriaPrima = new ObservableCollection<MateriaPrima>();
            ListaPropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            NotasOperacion = new ObservableCollection<string>();
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

        #region Methods of IObserverThickness
        public void UpdateState(ISubjectThickness sender, double MaterialRemoverAfterOperacion, double ThicknessAfterOperacion)
        {
            Thickness = ThicknessAfterOperacion + MaterialRemoverAfterOperacion;
        }

        /// <summary>
        /// Método que establece la cantidad de material a remover/agregar en la operación.
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