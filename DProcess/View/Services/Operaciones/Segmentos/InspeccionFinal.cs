using Model;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using View.Services.TiempoEstandar.Segmentos;

namespace View.Services.Operaciones.Segmentos
{
    public class InspeccionFinal : GenericOperation, IOperacion
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

            #region Obtenemos el Width
            Propiedad widthMin = Module.GetPropiedad("h11 Min", elPlano.PerfilLateral.Propiedades);
            Propiedad widthMax = Module.GetPropiedad("h11 Max", elPlano.PerfilLateral.Propiedades);
            widthMin.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), widthMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), widthMin.Valor);
            widthMax.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), widthMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), widthMax.Valor);
            #endregion

            #region Obtenemos el Thickness
            Propiedad thicknessMin = Module.GetPropiedad("a1 Min", elPlano.PerfilID.Propiedades);
            Propiedad thicknessMax = Module.GetPropiedad("a1 Max", elPlano.PerfilID.Propiedades);
            thicknessMin.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), thicknessMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), thicknessMin.Valor);
            thicknessMax.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), thicknessMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), thicknessMax.Valor);
            #endregion

            #region Obtenemos el Gap
            Propiedad gapMin = Module.GetPropiedad("s1 Min", elPlano.PerfilPuntas.Propiedades);
            Propiedad gapMax = Module.GetPropiedad("s1 Max", elPlano.PerfilPuntas.Propiedades);
            gapMin.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), gapMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), gapMin.Valor);
            gapMax.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), gapMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), gapMax.Valor);
            #endregion

            #region Obtenemos el Free Gap
            Propiedad freeGapMin = Module.GetPropiedad("freeGapMin", elPlano.PerfilPuntas.Propiedades);
            Propiedad freeGapMax = Module.GetPropiedad("freeGapMax", elPlano.PerfilPuntas.Propiedades);
            freeGapMin.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), freeGapMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), freeGapMin.Valor);
            freeGapMax.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), freeGapMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), freeGapMax.Valor);
            #endregion

            #region Obtenemos la capa de nitrurado
            //Propiedad thicknessNitMin = Module.GetPropiedad("ODCoatingNitrideMin", elPlano.PerfilOD.Propiedades);
            //Propiedad thicknessNitMax = Module.GetPropiedad("ODCoatingNitrideMax", elPlano.PerfilOD.Propiedades);
            //thicknessNitMax.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), thicknessNitMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), thicknessNitMax.Valor);
            //thicknessNitMin.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), thicknessNitMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), thicknessNitMin.Valor);

            PropiedadOptional especNitrurado = elPlano.PerfilOD.PropiedadesOpcionales.Where(o => o.lblTitle == "ESPEC_NITRURADO").FirstOrDefault();

            DO_DataGasNitridingRails data = DataManager.GetDataGasNitriding(especNitrurado.ElementSelected.ValorCadena);
            #endregion

            double d1 = Module.ConvertTo(elPlano.D1.TipoDato, elPlano.D1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), elPlano.D1.Valor);
            double h1 = Module.ConvertTo(elPlano.H1.TipoDato, elPlano.H1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), elPlano.H1.Valor);

            PropiedadOptional propiedadEspec = elPlano.PerfilOD.PropiedadesOpcionales.Where(o => o.lblTitle == "ESPEC_PVD").FirstOrDefault();
            DO_DataPVD dataPVD = DataManager.GetDataPVD(propiedadEspec.ElementSelected.ValorCadena);
            string recetaPVD = dataPVD.NoReceta;

            //Agregamos el texto con las instrucciones de la operación.
            TextoProceso = "*INSPECCION FINAL" + Environment.NewLine;
            TextoProceso += "*AUDITORIA DIM." + Environment.NewLine;
            TextoProceso += "ABERTURA       " + gapMin.Valor + " - " + gapMax.Valor + "" + Environment.NewLine;
            TextoProceso += "ESPESOR RADIAL " + thicknessMin.Valor + " - " + thicknessMax.Valor + "" + Environment.NewLine;
            TextoProceso += "TH. NITRURADO  " + data.ThicknessMin + " - " + data.ThicknessMax + "" + Environment.NewLine;
            TextoProceso += "ABERTURA LIBRE " + freeGapMin.Valor + " - " + freeGapMin.Valor + "" + Environment.NewLine;
            TextoProceso += "ESPESOR AXIAL  " + widthMin.Valor + " - " + widthMax.Valor + "" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "ESPECIFICACION PVD " + recetaPVD + Environment.NewLine;
            TextoProceso += "ESPESOR PVD " + dataPVD.ThicknessMin + " - " + dataPVD.ThicknessMax + Environment.NewLine;
            TextoProceso += "DIMENSION PARA REGIÓN INTERNA " + "" + " -" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "ALINEAR E INSPECCIONAR" + Environment.NewLine;

            #region Pintura
            bool banPintura = false;
            TextoProceso += "*PINTURA" + Environment.NewLine;
            if (elPlano.FranjasPintura.Count > 0)
            {
                banPintura = true;
                TextoProceso += "PINTAR FRANJAS COLOR:" + Environment.NewLine;
                int c = 1;
                foreach (var franja in elPlano.FranjasPintura)
                {
                    TextoProceso += c + ")" + franja.Color + "     DE " + franja.AnchoPintura + Environment.NewLine;
                    c++;
                }

                TextoProceso += "UBICACION FRANJA:" + Environment.NewLine;
                c = 1;
                foreach (var franja in elPlano.FranjasPintura)
                {
                    TextoProceso += c + ")" + franja.UbicacionFranja.UbicacionFranjaTexto + Environment.NewLine;
                    c++;
                }

                TextoProceso += "NOTAS:" + Environment.NewLine;
                foreach (var franja in elPlano.FranjasPintura)
                {
                    TextoProceso += "." + franja.Nota + Environment.NewLine;
                }
            }
            else
            {
                TextoProceso += "PINTAR FRANJAS COLOR: N O  P I N T A R" + Environment.NewLine;
            }
            #endregion

            #region Condiciones de empaque
            TextoProceso += "*ENVOLTURA" + Environment.NewLine;
            TextoProceso += "ACEITE :" + Environment.NewLine;
            TextoProceso += "TIPO: " + elPlano.CondicionesDeEmpaque.AceiteTipo + "       CANT: " + elPlano.CondicionesDeEmpaque.CantidadPasos + " PASOS" + Environment.NewLine;
            TextoProceso += "NOTA:" + Environment.NewLine;
            TextoProceso += "" + elPlano.CondicionesDeEmpaque.Nota1 + "" + Environment.NewLine;
            TextoProceso += "" + elPlano.CondicionesDeEmpaque.Nota2 + "" + Environment.NewLine;
            TextoProceso += "" + elPlano.CondicionesDeEmpaque.PzasXRollo + " PIEZAS POR ROLLO" + Environment.NewLine;
            TextoProceso += "" + elPlano.CondicionesDeEmpaque.PapelTipo + Environment.NewLine;
            TextoProceso += "" + elPlano.CondicionesDeEmpaque.RollosXCaja + " ROLLOS POR CAJA, CJA " + elPlano.CondicionesDeEmpaque.CajaNo + "" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "NOTA:" + Environment.NewLine;
            TextoProceso += "" + elPlano.CondicionesDeEmpaque.NotaGeneral + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "*IDENTIFICACION" + Environment.NewLine;
            TextoProceso += "CLIENTE: MAHLE MORRISTOWN" + Environment.NewLine;
            TextoProceso += "PTE. CLTE. " + "" + " REV. " + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "MEDIDA " + d1 + " X " + h1 +  Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;

            bool banAceite = string.IsNullOrEmpty(elPlano.CondicionesDeEmpaque.AceiteTipo) ? false : true;
            anilloProcesado.PropiedadesBoolAdquiridasProceso.Add(new PropiedadBool { Nombre = "llevaAceite", Valor = banAceite });

            anilloProcesado.PropiedadesAdquiridasProceso.Add(new Propiedad { Nombre = "CantidadFranjas", Valor = elPlano.FranjasPintura.Count, TipoDato = "Cantidad", Unidad = "Unidades" });

            anilloProcesado.PropiedadesBoolAdquiridasProceso.Add(new PropiedadBool { Nombre = "llevapintura", Valor = banPintura });

            #endregion

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
                CentroTrabajo831 centroTrabajo831 = new CentroTrabajo831();

                centroTrabajo831.Calcular(anilloProcesado);

                this.TiempoLabor = centroTrabajo831.TiempoLabor;
                this.TiempoMachine = centroTrabajo831.TiempoMachine;
                this.TiempoSetup = centroTrabajo831.TiempoSetup;

                if (centroTrabajo831.Alertas.Count > 0)
                {
                    AlertasOperacion.Add("Error en calculo de tiempos estándar");
                    AlertasOperacion.CopyTo(centroTrabajo831.Alertas.ToArray(), 0);
                }
                else
                {
                    NotasOperacion.Add("Tiempos estándar celculados correctamente");
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
            NombreOperacion = "INSPECCION FINAL SEGMENTOS";
            CentroCostos = "32014563";
            CentroTrabajo = "831";
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
        public InspeccionFinal(Anillo plano)
        {
            InicializarDatosGenerales();
            elPlano = plano;
        }

        public InspeccionFinal()
        {
            InicializarDatosGenerales();
        }
        #endregion
    }
}
