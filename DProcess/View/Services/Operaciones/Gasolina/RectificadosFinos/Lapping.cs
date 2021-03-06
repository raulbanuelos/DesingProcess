﻿using Model;
using Model.Interfaces;
using System;
using System.Collections.ObjectModel;

namespace View.Services.Operaciones.Gasolina.RectificadosFinos
{
    public class Lapping : GenericOperation, IOperacion, IObserverDiametro, IObserverThickness
    {
        #region Attributes
        private int noPrograma;
        private int noStrokes; 
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
            //TextoProceso = "Diámetro: " + String.Format("{0:0.00000}", Diameter) + Environment.NewLine;
            //TextoProceso += "Thickness: " + String.Format("{0:0.0000}", Thickness);

            //Para los NNSI siempre es el programa 44.
            noPrograma = 44;

            //If Width(mm) <= 1.2 : 10, Else Width(mm) > 1.5 : 15
            noStrokes = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), EnumEx.GetEnumDescription(DataManager.UnidadDistance.Milimeter), elPlano.H1.Valor) <= 1.2 ? 10 : 15;

            TextoProceso = "PROG. \" " + noPrograma + " \" " + noStrokes + " STROKES" + Environment.NewLine;
            TextoProceso += "*LAP" + Environment.NewLine;

            //Fijo para los tipos NNSI
            TextoProceso += "LAPEAR EL DIAMETRO EXTERIOR PARA" + Environment.NewLine;
            TextoProceso += "OBTENER UNA PISTA CONTINUA EN LOS 360" + Environment.NewLine;
            TextoProceso += "GRADOS DEL ANILLO" + Environment.NewLine;
            
            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el método para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {
            Herramental barrelGrade = herramentalBarrelGrade();

            ListaHerramentales.Add(barrelGrade);

            double d1Inch = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), elPlano.D1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), elPlano.D1.Valor);

            Herramental front = DataManager.GetFrontRearCollarAnillos(d1Inch, "FRONT COLLAR ANI.", "FRONT COLLAR. ");
            Herramental rear = DataManager.GetFrontRearCollarAnillos(d1Inch, "REAR COLLAR ANI.", "REAR COLLAR. ");
            Herramental loading = DataManager.GetLoadingGuideAnillos(d1Inch);
            
            ListaHerramentales.Add(front);
            ListaHerramentales.Add(rear);
            ListaHerramentales.Add(loading);

            TextoHerramienta = Module.GetTextoListaHerramentales(ListaHerramentales);
        }

        private Herramental herramentalBarrelGrade()
        {
            ObservableCollection<Herramental> listaBarrelGrade = new ObservableCollection<Herramental>();
            DataManager.GetALLBarrelLapAnillos_("", out listaBarrelGrade);

            double diametroAnillo = elPlano.D1.Valor;
            Herramental barrelIdeal = new Herramental();
            string fracc= string.Empty;

            foreach (Herramental barrel in listaBarrelGrade)
            {
                //Module.ConvertFracToDecimal()
                fracc = barrel.PropiedadesCadena[0].Valor;
                double deci = Convert.ToDouble(Module.ConvertFracToDecimal(fracc));
                double tope = 0;
                if (deci <= diametroAnillo)
                {
                    if (deci > tope)
                    {
                        tope = deci;
                        barrelIdeal = barrel;
                        barrelIdeal.Encontrado = true;
                    }
                }
            }

            diametroAnillo = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), elPlano.D1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), elPlano.D1.Valor);

            barrelIdeal.DescripcionRuta = "BARREL " + diametroAnillo;
            return barrelIdeal;

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

        public void InicializarDatosGenerales()
        {
            //Asignamos los valores por default a las propiedades.
            NombreOperacion = "LAPPING (ANILLOS)";
            CentroCostos = "32012529";
            CentroTrabajo = "455";
            ControlKey = "MA42";
            IdXML = "IDCentroTrabajo455";

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

        #region Methods override
        public override string ToString()
        {
            return NombreOperacion;
        }
        #endregion

        #endregion

        #region Constructors
        public Lapping(Anillo plano)
        {
            InicializarDatosGenerales();
            elPlano = plano;
        }

        public Lapping()
        {
            InicializarDatosGenerales();
        }
        #endregion
    }
}
