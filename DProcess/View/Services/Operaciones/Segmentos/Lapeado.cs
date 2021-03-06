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
    public class Lapeado : GenericOperation, IOperacion
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

            //Agregamos el texto con las instrucciones de la operación.
            TextoProceso = "*LAPEADO" + Environment.NewLine;
            TextoProceso += "PROGRAMA 2" + Environment.NewLine;
            if (elPlano.PerfilOD.Propiedades.Where(x=> x.Nombre == "CaidaRadioSegMin").ToList().Count > 0)
            {
                Propiedad caidaRadioMin = Module.GetPropiedad("CaidaRadioSegMin", elPlano.PerfilOD.Propiedades);
                Propiedad caidaRadioMax = Module.GetPropiedad("CaidaRadioSegMax", elPlano.PerfilOD.Propiedades);
                Propiedad pistaLapeado = Module.GetPropiedad("PistaLapeado", elPlano.PerfilOD.Propiedades);

                caidaRadioMin.Valor = Module.ConvertTo(caidaRadioMin.TipoDato, caidaRadioMin.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), caidaRadioMin.Valor);
                caidaRadioMax.Valor = Module.ConvertTo(caidaRadioMax.TipoDato, caidaRadioMax.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), caidaRadioMax.Valor);
                pistaLapeado.Valor = Module.ConvertTo(pistaLapeado.TipoDato, pistaLapeado.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), pistaLapeado.Valor);

                TextoProceso += "USAR PASTA DE CROMO" + Environment.NewLine;
                TextoProceso += "LAPEAR HASTA OBTENER UNA CAIDA" + Environment.NewLine;
                TextoProceso += "DE RÁDIO DE " + caidaRadioMin.Valor + " - " + caidaRadioMax.Valor + " EN" + Environment.NewLine;
                TextoProceso += pistaLapeado.Valor + " AL CENTRO DEL SEGMENTO" + Environment.NewLine;
                TextoProceso += "ACABADO: EL LAPEADO DEBE SER" + Environment.NewLine;
                TextoProceso += "CONTINUO A 360° DEL SEGMENTO" + Environment.NewLine;
                TextoProceso += "HACER PRUEBA DE LUZ" + Environment.NewLine;
            }
            else
            {
                TextoProceso += "USAR PASTA DE LAPEADO GRANO 1200" + Environment.NewLine;
                TextoProceso += "CARA DE CONTACTO MAXIMA 0.010" + Environment.NewLine;
                TextoProceso += "ACABADO: CARA DE CONTACTO DE LOS" + Environment.NewLine;
                TextoProceso += "SEGMENTOS DEBE SER CONTINUO" + Environment.NewLine;
                TextoProceso += "HACER PRUEBA DE LUZ" + Environment.NewLine;
            }

            TextoProceso += " " + Environment.NewLine;
            TextoProceso += "CAPA DE DIFUSIÓN_______________________" + Environment.NewLine;
            TextoProceso += " " + Environment.NewLine;

            anilloProcesado.PropiedadesAdquiridasProceso.Add(new Propiedad { Nombre = "programa456", Valor = 2 });

            //Ejecutamos el método para calculo de Herramentales.
            BuscarHerramentales();

            //Ejecutamos el méotodo para calcular los tiempos estándar.
            CalcularTiemposEstandar();
        }

        public void BuscarHerramentales()
        {
            double d1Inch = Module.ConvertTo(elPlano.D1.TipoDato, elPlano.D1.Unidad, EnumEx.GetEnumDescription(DataManager.UnidadDistance.Inch), elPlano.D1.Valor);
            ListaHerramentales.Add(DataManager.GetMangaLapeadoSegmentos(d1Inch));

            bool hasNitrurado = elPlano.Operaciones.Where(o => o.NombreOperacion == "").ToList().Count > 0 ? true : false;

            TextoHerramienta = Module.GetTextoListaHerramentales(ListaHerramentales);
        }

        /// <summary>
        /// Método en el cual se calculan los tiempos estandar.
        /// </summary>
        public void CalcularTiemposEstandar()
        {
            try
            {
                CentroTrabajo456 centroTrabajo510 = new CentroTrabajo456();

                centroTrabajo510.Calcular(anilloProcesado);

                this.TiempoLabor = centroTrabajo510.TiempoLabor;
                this.TiempoMachine = centroTrabajo510.TiempoMachine;
                this.TiempoSetup = centroTrabajo510.TiempoSetup;

                if (centroTrabajo510.Alertas.Count > 0)
                {
                    AlertasOperacion.Add("Error en calculo de tiempos estándar");
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
            NombreOperacion = "LAPEADO";
            CentroCostos = "";
            CentroTrabajo = "456";
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
        public Lapeado(Anillo plano)
        {
            InicializarDatosGenerales();
            elPlano = plano;
        }

        public Lapeado()
        {
            InicializarDatosGenerales();
        }
        #endregion
    }
}