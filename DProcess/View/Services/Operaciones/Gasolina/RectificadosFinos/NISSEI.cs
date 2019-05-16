using Model;
using Model.Interfaces;
using System;
using System.Collections.ObjectModel;
using View.Services.TiempoEstandar.Gasolina.RectificadosFinos;

namespace View.Services.Operaciones.Gasolina.RectificadosFinos
{
    public class NISSEI : GenericOperation, IOperacion, IObserverWidth
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

        /// <summary>
        /// Double que representa el número de cortes en la operación.
        /// </summary>
        public double CortesOPasadas { get; set; }

        /// <summary>
        /// Clase que representa las características de los cortes que están en la operación.
        /// </summary>
        public PasoNISSEI PasoNISSEI { get; set; }

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

            double widthDecimal = Math.Round(WidthOperacion * 25.4, 3);

            TextoProceso = "*FIN GRIND \n";
            TextoProceso += "mm\n";
            TextoProceso += GetMedidadasCorte2(true);
            TextoProceso += "\n";
            TextoProceso += "REF.BLOCK PATRON:\n";
            
            TextoProceso += GetMedidadasCorte2(false);

            TextoProceso += "ROUGHNESS 25 Ra MAX.\n";
            TextoProceso += "NÚMERO DE CORTES: " + CortesOPasadas + "\n"; 

            anilloProcesado.H1.Valor = WidthOperacion;

            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            anilloProcesado.PropiedadesAdquiridasProceso.Add(new Propiedad { Nombre = "NUM_PASADAS_180", DescripcionCorta = "Num. Cortes NISSEI", DescripcionLarga = "Representa el número de cortes en la operación NISSEI", Valor = PasoNISSEI.Cortes.Length});

            //Ejecutamos el méotodo para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }


        private string GetMedidadasCorte2(bool isDecimal)
        {
            double widthCalculado = WidthOperacion;
            double[] vec = new double[PasoNISSEI.Cortes.Length];
            vec[0] = widthCalculado;

            int c = 1;
            for (int i = PasoNISSEI.Cortes.Length - 1; i > 0; i--)
            {
                vec[c] = widthCalculado + PasoNISSEI.Cortes[i].MatRemover;
                widthCalculado = vec[c];
                c++;
            }

            string text = string.Empty;
            int j = 1;
            for (int i = vec.Length - 1; i >= 0; i--)
            {
                if (isDecimal)
                    text += "(" + j + ")" + "(" + Math.Round(vec[i] * 25.4,3) + ")\n";
                else
                    text += "(" + j + ")" + "(" + vec[i] + ")\n";
                
                j++;
            }
            return text;
        }

        //private string GetMedidasCortes()
        //{
        //    double[] vMedidas = new double[Convert.ToInt32(CortesOPasadas)];

        //    int c = vMedidas.Length - 1;

        //    vMedidas[c] = WidthOperacion;
        //    c--;

        //    double aux = WidthOperacion;
        //    double MatRemoverPorCorte = MatRemoverWidth / CortesOPasadas;

        //    while (c >= 0)
        //    {
        //        vMedidas[c] = aux + MatRemoverPorCorte;
        //        aux = vMedidas[c];
        //        c--;
        //    }

        //    string texto = "in ";

        //    foreach (double medida in vMedidas)
        //    {
        //        texto += "(" + medida + ")";
        //    }

        //    texto += "\n";

        //    return texto;

        //}

        public void BuscarHerramentales()
        {
            
            TextoHerramienta = Module.GetTextoListaHerramentales(ListaHerramentales);

        }

        /// <summary>
        /// Método en el cual se calculan los tiempos estandar.
        /// </summary>
        public void CalcularTiemposEstandar()
        {
            try
            {
                CentroTrabajo180 objTiempo = new CentroTrabajo180();

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
                AlertasOperacion.Add("Error en cálculo de tiempos estándar. \n" + er.StackTrace);
            }
        }

        public void InicializarDatosGenerales()
        {
            //Asignamos los valores por default a las propiedades.
            NombreOperacion = "FINISH GRIND (NISSEI)";
            CentroCostos = "32012529";
            CentroTrabajo = "180";
            ControlKey = "MA42";
            
            ListaHerramentales = new ObservableCollection<Herramental>();
            ListaMateriaPrima = new ObservableCollection<MateriaPrima>();
            ListaPropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();
            AlertasOperacion = new ObservableCollection<string>();
            NotasOperacion = new ObservableCollection<string>();
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

        /// <summary>
        /// Método que establece que cantidad de material a remover va tener la operación.
        /// </summary>
        /// <param name="operaciones"></param>
        /// <param name="posOperacion"></param>
        public void setMaterialRemover(ObservableCollection<IOperacion> operaciones, int posOperacion, Anillo plano_)
        {
            //Por corte.
            MatRemoverWidth = 0.0005;
            
            if (elPlano == null)
                elPlano = plano_;
        }

        #endregion

        #region Methods override
        public override string ToString()
        {
            return NombreOperacion;
        }
        #endregion

        #endregion

        #region Contructors
        public NISSEI(Anillo plano)
        {
            InicializarDatosGenerales();
            elPlano = plano;
        }

        public NISSEI()
        {
            InicializarDatosGenerales();
        }
        #endregion
    }
}