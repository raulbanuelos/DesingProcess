﻿using Model;
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
    public class Thompson : GenericOperation, IOperacion
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
            //Asignamos el valor del anillo procesado al anillo de la operación.
            anilloProcesado = ElAnilloProcesado;

            elPlano.D1.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), elPlano.D1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), elPlano.D1.Valor);
            elPlano.D1.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

            Propiedad widthMin = Module.GetPropiedad("h11 Min", elPlano.PerfilLateral.Propiedades);
            Propiedad widthMax = Module.GetPropiedad("h11 Max", elPlano.PerfilLateral.Propiedades);

            widthMin.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), widthMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), widthMin.Valor);
            widthMax.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), widthMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), widthMax.Valor);

            Propiedad thicknessMin = Module.GetPropiedad("a1 Min", elPlano.PerfilID.Propiedades);
            Propiedad thicknessMax = Module.GetPropiedad("a1 Max", elPlano.PerfilID.Propiedades);

            thicknessMin.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), thicknessMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), thicknessMin.Valor);
            thicknessMax.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), thicknessMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), thicknessMax.Valor);

            Propiedad gapMin = Module.GetPropiedad("s1 Min", elPlano.PerfilPuntas.Propiedades);
            Propiedad gapMax = Module.GetPropiedad("s1 Max", elPlano.PerfilPuntas.Propiedades);

            gapMin.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), gapMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), gapMin.Valor);
            gapMax.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), gapMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), gapMax.Valor);
            gapMin.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);
            gapMax.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

            double gapMedia = (gapMin.Valor + gapMax.Valor) / 2;
            double gapTolerancia = gapMedia - gapMin.Valor;

            Propiedad freeGapMin = Module.GetPropiedad("freeGapMin", elPlano.PerfilPuntas.Propiedades);
            Propiedad freeGapMax = Module.GetPropiedad("freeGapMax", elPlano.PerfilPuntas.Propiedades);

            freeGapMin.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), freeGapMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), freeGapMin.Valor);
            freeGapMax.Valor = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), freeGapMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), freeGapMax.Valor);
            freeGapMin.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);
            freeGapMax.Unidad = EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch);

            //Agregamos el texto con las instrucciones de la operación.
            TextoProceso = "*CORTE DE PUNTAS" + Environment.NewLine;
            TextoProceso += "*Y CHAFLAN VIA" + Environment.NewLine;
            TextoProceso += "*THOMPSON" + Environment.NewLine;
            TextoProceso += "CALIBRADOR        " + elPlano.D1.Valor + Environment.NewLine;
            TextoProceso += "ESP. AXIAL        " + widthMin.Valor + "  -  " + widthMax.Valor + Environment.NewLine;
            TextoProceso += "ESP RADIAL        " + thicknessMin.Valor + "  -  " + thicknessMax.Valor + Environment.NewLine;
            TextoProceso += "ABERTURA          " + gapMedia + " +/- 0.003 <SC>" + Environment.NewLine;
            TextoProceso += "ABERTURA LIBRE    " + freeGapMin.Valor + "  -  " + freeGapMax.Valor + "" + Environment.NewLine;
            TextoProceso += "CHAFLÁN DIA. EXT. 0.0004  -  0.0078" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "NOTA: LIBRE DE REBABAS EN PUNTAS" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;

            int r = anilloProcesado.Operaciones.Where(x => x.NombreOperacion == "NITRURADO A GAS SEGMENTOS").ToList().Count;
            if (r > 0)
            {
                anilloProcesado.PropiedadesCadenaAdquiridasProceso.Add(new PropiedadCadena { Nombre = "recubrimiento", Valor = "Nitrurado" });
            }

            r = anilloProcesado.Operaciones.Where(x => x.NombreOperacion == "CROMO").ToList().Count;
            if (r > 0)
            {
                anilloProcesado.PropiedadesCadenaAdquiridasProceso.Add(new PropiedadCadena { Nombre = "recubrimiento", Valor = "Cromado" });
            }

            anilloProcesado.PropiedadesBoolAdquiridasProceso.Add(new PropiedadBool { Nombre = "chaflan_interno555", Valor = true });
            anilloProcesado.PropiedadesBoolAdquiridasProceso.Add(new PropiedadBool { Nombre = "rebabeoCT555", Valor = true });


            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el método para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {
            Propiedad gapMin = Module.GetPropiedad("s1 Min", elPlano.PerfilPuntas.Propiedades);
            Propiedad gapMax = Module.GetPropiedad("s1 Max", elPlano.PerfilPuntas.Propiedades);

            double gapMinInch = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), gapMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), gapMin.Valor);
            double gapMaxInch = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), gapMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), gapMax.Valor);
            double gapMedida = Math.Round((gapMinInch + gapMaxInch) / 2,3);

            double d1 = Module.ConvertTo(EnumEx.GetEnumDescription(DataManager.TipoDato.Distance), elPlano.D1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), elPlano.D1.Valor);

            List<Herramental> bushingAndDisco = DataManager.GetBushingAndDiscoThompsonSegmentos(gapMedida,d1);

            Herramental bushing = bushingAndDisco[0];
            Herramental disco = bushingAndDisco[1];

            ListaHerramentales.Add(bushing);
            ListaHerramentales.Add(disco);

            double medidaBushing = bushing.Propiedades[0].Valor;

            Herramental clampPlate = DataManager.GetClampPlateThompsonSegmentos(medidaBushing);
            Herramental backUp = DataManager.GetBackUpRingThompsonSegmentos(medidaBushing);
            double medidaBackUp = backUp.Propiedades[0].Valor;

            Herramental platoEmpujador = DataManager.GetPlatoEmpujadorThompsonSegmentos(medidaBushing);
            Herramental tuboEnrollador = DataManager.GetTuboEnrolladorThompsonSegmentos(medidaBackUp);

            ListaHerramentales.Add(clampPlate);
            ListaHerramentales.Add(backUp);
            ListaHerramentales.Add(platoEmpujador);
            ListaHerramentales.Add(tuboEnrollador);

            TextoHerramienta = Module.GetTextoListaHerramentales(ListaHerramentales);

        }

        /// <summary>
        /// Método en el cuál se calculan los tiempos estándar.
        /// </summary>
        public void CalcularTiemposEstandar()
        {
            try
            {
                CentroTrabajo555 centroTrabajo510 = new CentroTrabajo555();

                centroTrabajo510.Calcular(anilloProcesado);

                this.TiempoLabor = centroTrabajo510.TiempoLabor;
                this.TiempoMachine = centroTrabajo510.TiempoMachine;
                this.TiempoSetup = centroTrabajo510.TiempoSetup;

                if (centroTrabajo510.Alertas.Count > 0)
                {
                    AlertasOperacion.Add("Error en cálculo de tiempos estándar");
                    AlertasOperacion.CopyTo(centroTrabajo510.Alertas.ToArray(), 0);
                }
                else
                {
                    NotasOperacion.Add("Tiempos estándar calculados correctamente");
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
            NombreOperacion = "CORTE DE SEGMENTOS THOMPSON";
            CentroCostos = "32012536";
            CentroTrabajo = "555";
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
        public Thompson(Anillo plano)
        {
            InicializarDatosGenerales();
            elPlano = plano;
        }

        public Thompson()
        {
            InicializarDatosGenerales();
        }
        #endregion
    }
}
