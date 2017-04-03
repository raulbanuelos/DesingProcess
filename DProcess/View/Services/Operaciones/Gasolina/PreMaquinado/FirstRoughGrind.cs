using System;
using System.Collections.ObjectModel;
using Model;
using Model.Interfaces;
using View.Services.TiempoEstandar.Gasolina.PreMaquinado;
namespace View.Services.Operaciones.Gasolina.PreMaquinado
{
    public class FirstRoughGrind
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

        #region Constructores
        public FirstRoughGrind(Anillo plano)
        {
            //Asignamos los valores por default a las propiedades.
            NombreOperacion = "FIRST ROUGH GRIND";
            CentroCostos = "32012524";
            CentroTrabajo = "110";
            ControlKey = "MA42";
            elPlano = plano;
            ListaHerramentales = new ObservableCollection<Herramental>();
            ListaMateriaPrima = new ObservableCollection<MateriaPrima>();
            ListaPropiedadesAdquiridasProceso = new ObservableCollection<Propiedad>();

            //Ejecutamos el método para calcular el width y el meterial a remover.
            CalcularWidth();
        }
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
            TextoProceso = "*1ST RGH GRIND";
            TextoProceso += "(2)(" + Convert.ToString(WidthOperacion + .010) + " +- .0005) (" + Convert.ToString(WidthOperacion) + " +- .0005)" + "\n";
            TextoProceso += "CHUCK RPM 15+-2 " + "\n";

            //Agregamos las propiedades que se obtiene el anillo durante el proceso.
            Propiedad rpm1_110 = new Propiedad { Nombre = "RPM1_110", TipoDato = "Cantidad", DescripcionLarga = "Cantidad de RPM primer corte en operación FIRST ROUGH GRIND", Imagen = null, DescripcionCorta = "RPM 1er corte (First Rough grind):", Valor = 15 };
            anilloProcesado.PropiedadesAdquiridasProceso.Add(rpm1_110);
            Propiedad rpm2_100 = new Propiedad { Nombre = "RMP2_110", TipoDato = "Cantidad", DescripcionLarga = "Cantidad de RPM segundo corte en operación FIRST ROUGH GRIND", Imagen = null, DescripcionCorta = "RPM 2do corte (First Rough grind):", Valor = 20 };
            anilloProcesado.PropiedadesAdquiridasProceso.Add(rpm2_100);

            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el méotodo para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {
            ListaHerramentales.Add(DataManager.GetGuideBarFirstRoughGrind(.125));
        }

        /// <summary>
        /// Método en el cual se calculan los tiempos estandar.
        /// </summary>
        public void CalcularTiemposEstandar()
        {
            try
            {
                //Ejecutamos el método
                TiempoSetup = DataManager.GetTimeSetup(CentroTrabajo);
                CentroTrabajo110 objTiempos = new CentroTrabajo110();
                objTiempos.Calcular(anilloProcesado);
                this.TiempoLabor = objTiempos.TiempoLabor;
                this.TiempoMachine = objTiempos.TiempoMachine;
                if (objTiempos.Alertas.Count > 0)
                {
                    AlertasOperacion.Add("Error en calculo de tiempo estándar");
                    AlertasOperacion.CopyTo(objTiempos.Alertas.ToArray(), 0);
                }
                else
                {
                    NotasOperacion.Add("Tiempos estándar calculador correctamente.");
                }
            }
            catch (Exception er)
            {
                AlertasOperacion.Add("Error en cálculo de tiempos estándar. \n" + er.StackTrace);
            }
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

        /// <summary>
        /// Método que calcula el width y el material a remover en la operación.
        /// </summary>
        private void CalcularWidth()
        {

            //Obtenemos el valor de la propiedad Proceso que ingresó el usuario.
            string proceso = Module.GetValorPropiedadString("Proceso", elPlano.PerfilOD.PropiedadesCadena);

            //Obtenemos el valor del width de la operación First Rough Grind.
            WidthOperacion = DataManager.GetWidthFirstRoughGrind(proceso, elPlano.H1.Valor);

            //Comparamos si el proceso es distindo a Sencillo.
            if (proceso != "Sencillo")
            {

                //Obtenemos el valor del width en la operación Splitter.
                double widthSplitter = DataManager.GetWidthSplitterCasting(proceso, elPlano.H1.Valor);

                //Calculamos el valor del material a remover en la operación.
                MatRemoverWidth = WidthOperacion - widthSplitter;
            }
        }

        #endregion
    }
}
