﻿using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using DataAccess.ServiceObjects;
using DataAccess.ServiceObjects.Tooling.Operaciones.Premaquinado;
using DataAccess.ServiceObjects.MateriasPrimas;
using DataAccess.ServiceObjects.Usuario;
using DataAccess.ServiceObjects.Unidades;
using DataAccess.ServiceObjects.ControlDocumentos;
using DataAccess.ServiceObjects.Perfiles;
using DataAccess.ServiceObjects.Tooling;
using DataAccess.ServiceObjects.Tooling.Operaciones.Maquinado;

namespace Model
{
    public static class DataManager
    {
        #region Centros de trabajo
        public static double GetTimeSetup(string centroDeTrabajo)
        {
            SO_CentroTrabajo ServicesCentroTrabajo = new SO_CentroTrabajo();

            return ServicesCentroTrabajo.GetTimeLabor(centroDeTrabajo);
        }
        #endregion

        #region Material
        /// <summary>
        /// Método que obtiene el tipo de material a partir de una especificación.
        /// </summary>
        /// <param name="EspecificacionMaterial">Cadena que representa la especificación de material(SPR-128, MF012-S, ETC.)</param>
        /// <returns>Tipo de material de la especificación(HIERRO GRIS, HIERRO DUCTIL, ETC.)</returns>
        public static string GetTipoMaterial(string EspecificacionMaterial)
        {
            string tipoMaterial = string.Empty;

            SO_Material ServiceMaterial = new SO_Material();

            DataSet InformacionBD = ServiceMaterial.GetTipoMaterial(EspecificacionMaterial);

            //Verificamos que el objeto recibido sea distinto de vacío.
            if (InformacionBD != null)
            {
                if (InformacionBD.Tables.Count > 0 && InformacionBD.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow element in InformacionBD.Tables[0].Rows)
                    {
                        tipoMaterial = element["tipo_material"].ToString();
                    }
                }
            }

            return tipoMaterial;
        }

        /// <summary>
        /// Método que obtiene el valor de compensado del Piece.
        /// </summary>
        /// <param name="anillo"></param>
        /// <param name="piece"></param>
        /// <returns></returns>
        public static double GetCompensacionPiece(Anillo anillo, double piece)
        {
            //Inicializamos los servicios de SO_CompesacionPiece.
            SO_CompesacionPiece ServicioCompensacion = new SO_CompesacionPiece();

            //Obtenemos los valores necesarios que se requieren.
            string idMaterial = GetIdMaterial(anillo.MaterialBase.Especificacion.Valor);
            int idTipoAnillo = GetIdTipoAnillo(anillo.TipoAnillo);
            
            //Declaramos una variable la cual será la que retornemos en el método.
            double compensacion = 0;

            //Ejecutamos el método para obtener el valor de compensación.
            IList inforamcionBD =  ServicioCompensacion.GetCompensacion(idMaterial, idTipoAnillo);

            //Verificamos que la información obtenida sea diferente de nulo.
            if (inforamcionBD != null)
            {
                //Iteramos la lista obtenida.
                foreach (var item in inforamcionBD)
                {
                    //Obtenemos el typo del item iterado.
                    System.Type tipo = item.GetType();

                    //Obtenemos el valor.
                    compensacion = (double)tipo.GetProperty("Compensacion").GetValue(item, null);
                }
            }

            //Realizamos la compensación y retornamos el resultado.
            return Math.Round(piece / compensacion, 3);
        }

        /// <summary>
        /// Método que obtiene el ID de una especificación de material. El ID es la especificación MAHLE. Ejemplo SPR-128, su ID es MF012-S
        /// </summary>
        /// <param name="especMaterial"></param>
        /// <returns></returns>
        public static string GetIdMaterial(string especMaterial)
        {
            //Declaramos una variable la cual será la que retornemos en el método.
            string idMaterial = string.Empty;

            //Inicializamos los servicios de SO_Material.
            SO_Material ServicioMaterial = new SO_Material();

            //Ejecutamos el método para obtener la información.
            IList informacionBD =  ServicioMaterial.GetIdEspecficacionMaterial(especMaterial);

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos cada elemento de la información obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Obtenemos el valor.
                    idMaterial = (string)tipo.GetProperty("IdMaterial").GetValue(item,null);
                }
            }

            //Retornamos el valor.
            return idMaterial;
        }

        /// <summary>
        /// Método que obtiene el id del tipo de anillo.
        /// </summary>
        /// <param name="tipoAnillo"></param>
        /// <returns></returns>
        public static int GetIdTipoAnillo(string tipoAnillo)
        {
            //Incializamos los serivicios de SO_TipoAnillo.
            SO_TipoAnillo ServicioTipoAnillo = new SO_TipoAnillo();

            //Ejecutamos el método para obtener la inforamación de base de datos.
            IList informacionBD = ServicioTipoAnillo.GetTipoAnillo(tipoAnillo);

            //Declaramos un entero el cual será el que retornemos en el método.
            int idTipoAnillo = 0;

            //Verificamos que la información de base de datos sea direferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista obtenida de la consulta.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Obtenemos el valor.
                    idTipoAnillo = (int)tipo.GetProperty("IdTipoAnillo").GetValue(item,null);
                }
            }

            //Retornamos el valor.
            return idTipoAnillo;
        }

        /// <summary>
        /// Método que obtiene todos los registros de tipo de anillo
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Anillo> GetTipoAnillo()
        {
            SO_TipoAnillo ServiceMaterial = new SO_TipoAnillo();
            //Ejecutamos el método para obtener la inforamación de base de datos.
            IList informacionBD = ServiceMaterial.GetAllTipoAnillo();

            ObservableCollection<Anillo> ListaR = new ObservableCollection<Anillo>();
            //Declaramos un entero el cual será el que retornemos en el método.

            //Verificamos que la información de base de datos sea direferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista obtenida de la consulta.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    Anillo obj = new Anillo();

                    //Obtenemos el valor.
                    obj.TipoAnillo = (string)tipo.GetProperty("Tipo").GetValue(item, null);
                  
                    ListaR.Add(obj);
                }
            }
            return ListaR;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Material> GetMaterial()
        {
            SO_Material ServiceMaterial = new SO_Material();
            //Ejecutamos el método para obtener la inforamación de base de datos.
            IList informacionBD = ServiceMaterial.GetMaterial();

            ObservableCollection<Material> ListaR = new ObservableCollection<Material>();
            //Declaramos un entero el cual será el que retornemos en el método.
           
            //Verificamos que la información de base de datos sea direferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista obtenida de la consulta.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    Material obj = new Material();

                    //Obtenemos el valor.
                    obj.id_material = (string)tipo.GetProperty("id").GetValue(item, null);
                    obj.descripcion=(string)tipo.GetProperty("descripcion").GetValue(item, null);
                    obj.recomendado= (bool)tipo.GetProperty("Recomendado").GetValue(item, null);

                    ListaR.Add(obj);
                }
            }
            return ListaR;
        }
        #endregion

        #region  Operaciones

        #region First Rough Grind

        /// <summary>
        /// Método que obtiene el valor del width de la operación.
        /// </summary>
        /// <param name="proceso">Proceso por el cual el usuario elige se va a procesar el anillo(Doble, Sencillo, Cuadruple).</param>
        /// <param name="H1">Double que representa el width nominal del anillo.</param>
        /// <returns>Double que representa el width de la operación.</returns>
        public static double? GetWidthFirstRoughGrind(string proceso, double H1)
        {
            //Inicializamos los servicios de SO_FirstRoughGrind
            SO_FirstRoughGrind ServiceFirstRoughGrind = new SO_FirstRoughGrind();

            //Ejecutamos el método para obtener el width de la operación y retornamos el resultado.
            return ServiceFirstRoughGrind.GetWidthOperacion(proceso, H1);
        }

        /// <summary>
        /// Método que obtiene el valor de width de la operación.
        /// </summary>
        /// <param name="proceso">Proceso por el cual el usuario elige se va a procesar el anillo(Doble,Sencillo,Cuadruple).</param>
        /// <param name="H1">Double que representa el width nominal del anillo.</param>
        /// <returns>Double que representa el width de la splitter.</returns>
        public static double GetWidthSplitterCasting(string proceso, double H1)
        {
            //Inicializamos los servicios de SO_FirstRoughGrind
            SO_SplitterCasting ServiceSplitterCasting = new SO_SplitterCasting();

            //Ejecutamos el método para obtener el width de la operación y retornamos el resultado.
            return ServiceSplitterCasting.GetWidthSplitterCastings(H1, proceso);
        }
        
        /// <summary>
        /// Método que obtiene el herramental Barra Guia de la operación First Rough Grind.
        /// </summary>
        /// <param name="a">Dimención "A" de la barra guia que se requiere obtener.</param>
        /// <returns>Herramental barra guia.</returns>
        public static Herramental GetGuideBarFirstRoughGrind(double a)
        {
            //Inicializamos los servicios de la operación First Rough Grind.
            SO_FirstRoughGrind ServiceFirstRoughGrind = new SO_FirstRoughGrind();

            //Ejecutamos el método para obtener el herramental.
            IList InformacionBD = ServiceFirstRoughGrind.GetGuideBarFirstRoughGrind(a);

            Herramental herramental = new Herramental();

            //Verificamos si la información obtenida es diferente de nulo. Se encontró herramental.
            if (InformacionBD != null)
            {

                //Iteramos la lista resultante de la consulta.
                foreach (var elemento in InformacionBD)
                {
                    System.Type tipo = elemento.GetType();

                    //Ejecutamos el método para obtener la información del herramental.
                    herramental = ReadInformacionHerramentalEncontrado(InformacionBD);

                    //Asignamos los valores restantes a las propiedades.
                    herramental.DescripcionRuta = "GUIDE BAR  " + (double)tipo.GetProperty("DimA").GetValue(elemento,null);
                }
            }
            else
            { //Si no se encontró el herramental.

            }

            return herramental;
        }
        
        /// <summary>
        /// Método que inserta un registro de Guide Bar First Rough Grind.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetFirstRG(Herramental obj)
        {
            //Inicializamos los servicios de la operación Guide Bar First Rough Grind.
            SO_FirstRoughGrind ServiceFirstRoughGrind = new SO_FirstRoughGrind();

           return ServiceFirstRoughGrind.SetFirstRG(obj.Codigo, obj.Propiedades[0].Valor);
        }
        #endregion

        #region Second Rough Grind

        /// <summary>
        /// Método el cual obtiene el herramental ideal de Guide bar
        /// </summary>
        /// <param name="widthOperacion"></param>
        /// <returns></returns>
        public static Herramental GetGuideBarSecondRoughGrind(double widthOperacion)
        {
            //Inicializamos los servicios de SO_SecondRoughGrind.
            SO_SecondRoughGrind ServiceSecondRoughGrind = new SO_SecondRoughGrind();

            //Ejecutamos el método para obtener la información de base de datos.
            IList informacionBD = ServiceSecondRoughGrind.GetGuideBar(widthOperacion);

            //Declaramos un objeto de tipo Herramental el cual será el que retornemos en el método.
            Herramental herramental = new Herramental();

            //Verificamos que ls informacion resultante sea diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista resultante.
                foreach (var elemento in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = elemento.GetType();

                    //Ejecutamos el método para convertir la información en un objeto de tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    //Agregamos la descripción para la hoja de ruta.
                    herramental.DescripcionRuta = "GUIDE BAR   " + (double)tipo.GetProperty("EspesorBarraGuia").GetValue(elemento,null);
                }
            }
            else
            {
                //Si no se encontro herramental.
            }

            //Retornamos el herramental.
            return herramental;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetSecondtRG(Herramental obj)
        {
            //Inicializamos los servicios de SO_SecondRoughGrind.
            SO_SecondRoughGrind ServiceSecondRoughGrind = new SO_SecondRoughGrind();

            //Método que inserta un resitro a la tabla GuideBarSecondRoughGrind
            return ServiceSecondRoughGrind.SetSecondRG(obj.Codigo, obj.Propiedades[0].Valor, obj.Propiedades[1].Valor, obj.Propiedades[2].Valor);
        }

        #endregion

        #region Splitter Casting

        /// <summary>
        /// Método que obtiene el tiempo ciclo de la operación de splitter casting.
        /// </summary>
        /// <param name="especificacionMaterial"></param>
        /// <returns></returns>
        public static double GetCycleTimeSplitter(string especificacionMaterial)
        {
            double tiempoCiclo = 0;

            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Ejecutamos el método para obtener el tiempo ciclo. El resultado lo guardamos en un DataSet
            DataSet informacionBD = ServiceSplitter.GetCycleTime(especificacionMaterial);

            //Verificamos que la información obtenida no sea nula.
            if (informacionBD != null)
            {

                //Verificamos que contenga al menos una tabla y que esa tabla contenga al menos un registro.
                if (informacionBD.Tables.Count > 0 && informacionBD.Tables[0].Rows.Count > 0)
                {
                    //Itermamos la información obtenida.
                    foreach (DataRow item in informacionBD.Tables[0].Rows)
                    {
                        tiempoCiclo = Convert.ToDouble(item["TiempoCiclo"]);
                    }
                }
            }

            //Retornamos la información obtenida.
            return tiempoCiclo;

        }

        /// <summary>
        /// Método que obtiene el valor de PATT_SM_ID de la tabla de Placas modelo.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static double GetIDSplitterCasting(string codigo)
        {
            //Declaramos una variable la cual será la que retornemos en el método.
            double id = 0;

            //Inicializamos los servicios de SO_Material.
            SO_Material ServicioMaterial = new SO_Material();

            //Ejecutamos el método para obtener la inforamción.
            IList informacionBD = ServicioMaterial.GetPATTSMID(codigo);

            //Verficamos que el resultado de la busqueda sea diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista resultante.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Obtenermos el valor de la columna PATT_SM_ID y lo asignamos a la variable.
                    id = (double)tipo.GetProperty("PATT_SM_ID").GetValue(item, null);
                }
            }

            //Realizamos la formula.
            id = Math.Round((id) - (Convert.ToDouble(id) * 0.015), 3);

            //Retornamos el valor.
            return id;
        }

        /// <summary>
        /// Método que calcula el valor OD de la placa modelo.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static double GetODSplitterCasting(string codigo)
        {
            //Declaramos una variable la cual será la que retornemos en el método.
            double od = 0;
            
            //Inicializamos los servicios de SO_Material.
            SO_Material ServicioMaterial = new SO_Material();

            //Ejecutamos el método y el resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioMaterial.GetCSTGSMOD(codigo);

            //Verificamos que el resultado sea diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos cada elemento de la lista resultante.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();
                    
                    //Obtenemos los valores necesarios de cada columna.
                    double cstg_sm_od = (double)tipo.GetProperty("CSTG_SM_OD").GetValue(item, null);
                    double rise = (double)tipo.GetProperty("RISE").GetValue(item, null);

                    //Calculamos el OD.
                    od = Math.Round(cstg_sm_od + ((rise - 0.005) * 2), 3);
                }
            }

            //Retornamos el valor calculado.
            return od;
        }

        //Cutter Spacer Splitter

        /// <summary>
        /// Método el cual obtiene la lista de herramentales de Spacer.
        /// </summary>
        /// <param name="proceso"></param>
        /// <param name="h1"></param>
        /// <returns></returns>
        public static List<Herramental> GetSpacerSplitterCastings(string proceso, double h1)
        {
            //Incializamos los servicios de SO_SplitterCasting.
            SO_SplitterCasting ServicioSplitter = new SO_SplitterCasting();

            //Declaramos una lista de herramentales la cual será la que retornemos en el método.
            List<Herramental> ListaResultante = new List<Herramental>();

            //Declaramos los valores requeridos.
            double spacer, spacerMin, spacerMax, criMinSpacer, criMaxSpacer;
            int noSpacer, noSpacer2;

            //Obtenemos la medida del spacer.
            spacer = GetMedidaSpacerSplitter(proceso, h1);
            noSpacer = GetCantidadSpacerSplitterCasting(proceso,h1);

            //Obtenemos los criterios mínimo y máximo para el spacer.
            criMinSpacer = GetCriterio("SpacerMin");
            criMaxSpacer = GetCriterio("SpacerMax");

            //Calculoamos los valores mínimo y máximo.
            spacerMin = spacer - criMinSpacer;
            spacerMax = spacer + criMaxSpacer;

            //Ejecutamos el método para obtener el o los spacer.
            IList informacionBD =  ServicioSplitter.GetSpacer(spacerMin, spacerMax);

            //Verificamos que la información obtenida sea direfente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la inforamción obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Leemos la lista para convertirla en lisa de herramentales.
                    Herramental herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    herramental.DescripcionRuta = "SPACER " + spacer + " No. SPACERS " + noSpacer;
                    ListaResultante.Add(herramental);
                    if (proceso != "Doble")
                    {
                        double spacer2 = GetMedidaSpacerSplitter2(proceso, h1);
                        noSpacer2 = GetCantidadSpacerSplitterCasting(proceso, h1);
                        Herramental herramental2 = GetSpacer2SplitterCasting(spacer2);

                        herramental2.DescripcionRuta = "SPACER " + spacer2 + " No. SPACERS " + noSpacer2;
                        ListaResultante.Add(herramental2);
                    }
                    break;
                }
            }
            else
            {
                //Si no se encontró herramental.
            }
            //Retornamos la lista.
            return ListaResultante;
        }

        /// <summary>
        /// Método que obtiene el mejor herramental para cutter Spacer
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBestSpacer(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            return DataR;
        }

        /// <summary>
        /// Método que obtiene la cantidad de espaciadores de la operación splitter.
        /// </summary>
        /// <param name="proceso"></param>
        /// <param name="h1"></param>
        /// <returns></returns>
        public static int GetCantidadSpacerSplitterCasting(string proceso, double h1)
        {
            //Declaramos un entero el cual será el que retornemos en el método.
            int cantidad = 0;

            //Inicializamos los servicios de splitter.
            SO_SplitterCasting ServicioSplitter = new SO_SplitterCasting();

            //Declaramos una lista anónima la cual contendra la información de la base de datos.
            IList informacionBD;

            //Verificamos si el proceso es double.
            if (proceso == "Doble")
                informacionBD = ServicioSplitter.GetCantidadSpacerDoble(proceso, h1);
            else
                informacionBD = ServicioSplitter.GetCantidadSpacer(proceso, h1);

            //Validamos si la información obtenida es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Obtenemos el valor y lo asignamos a la variable local.
                    cantidad = (int)tipo.GetProperty("CantidadSpacer").GetValue(item, null);
                }
            }

            //Retornamos el resultado.
            return cantidad;
        }

        /// <summary>
        /// Método que obtiene el 2do herramental spacer, esto cuendo el proseso es distinto de doble.
        /// </summary>
        /// <param name="spacer"></param>
        /// <returns></returns>
        public static Herramental GetSpacer2SplitterCasting(double spacer)
        {
            Herramental herramental = new Herramental();

            SO_SplitterCasting ServicioSplitter = new SO_SplitterCasting();

            double spacerMin, spacerMax;
            //Obtenemos el creterio
            spacerMin = GetCriterio("SpacerMin");
            spacerMax = GetCriterio("SpacerMax");

            spacerMin = spacer - spacerMin;
            spacerMax = spacer + spacerMax;

            //Se obtiene la información de la base de datos.
            IList informacionBD = ServicioSplitter.GetSpacer(spacerMin, spacerMax);

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    System.Type tipo = item.GetType();

                    herramental = new Herramental();
                    //Obtenemos el herramental a partir de la lista.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                }
            }

            return herramental;
        }

        /// <summary>
        /// Método el cual obtiene la médida del Spacer de la operación splitter.
        /// </summary>
        /// <param name="proceso"></param>
        /// <param name="h1"></param>
        /// <returns></returns>
        public static double GetMedidaSpacerSplitter2(string proceso, double h1)
        {
            //Declaramos una variable la cual será la que almacene la medida del spacer.
            double medidaSpacer = 0;

            //Inicializamos los servicios de SO_SplitterCasting.
            SO_SplitterCasting ServicioSplitter = new SO_SplitterCasting();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServicioSplitter.GetMedidaSpacer2(proceso, h1);

            //Verificamos que la información obtenida
            if (informacionBD != null)
            {
                //Itermamos la información obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Obtenemos el valor de la propiedad.
                    medidaSpacer = (double)tipo.GetProperty("Cutter_Spacer").GetValue(item, null);
                }
            }
            
            //Retornamos el valor obtenido.
            return medidaSpacer;
        }

        /// <summary>
        /// Método el cual obtiene la médida del Spacer de la operación splitter.
        /// </summary>
        /// <param name="proceso"></param>
        /// <param name="h1"></param>
        /// <returns></returns>
        public static double GetMedidaSpacerSplitter(string proceso, double h1)
        {
            //Declaramos una variable la cual será la que retornemos en el método.
            double medidaSpacer = 0;

            //Inicializamos los servicios de SO_SplitterCasting.
            SO_SplitterCasting ServicioSplitter = new SO_SplitterCasting();

            //Ejecutamos el método para obtener la información de la base datos.
            IList informacionBD = ServicioSplitter.GetMedidaSpacer(proceso, h1);

            //Verificamos que la información resultante sea direfente de nulo.
            if (informacionBD != null)
            {
                //Iteramos cada elemento de la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Obtenemos el valor.
                    medidaSpacer = (double)tipo.GetProperty("Cutter_Spacer").GetValue(item,null);
                }
            }

            //Retornamos el valor.
            return medidaSpacer;
        }

        /// <summary>
        /// Método que inserta un registro de cutter spacer splitter
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetCutterSpacerS(Herramental obj)
        {
            //Inicializamos los servicios de SO_SplitterCasting.
            SO_SplitterCasting ServicioSplitter = new SO_SplitterCasting();
            //Ejecutamos el método, devolvemos el resultado
            return ServicioSplitter.SetCutterSpacerS(obj.Codigo,obj.Propiedades[0].Valor,obj.Propiedades[1].Valor,obj.Plano);
            
        }

        /// <summary>
        /// Método que modifica un registro de cutter Spacer Splitter
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateCutterSpacerS(Herramental obj)
        {
            //Inicializamos los servicios de SO_SplitterCasting.
            SO_SplitterCasting ServicioSplitter = new SO_SplitterCasting();

            //Ejecutamos el método, devolvemos el resultado
            return ServicioSplitter.UpdateCutterSpacerS(obj.idHerramental,obj.Codigo, obj.Propiedades[0].Valor, obj.Propiedades[1].Valor, obj.Plano);
        }

        /// <summary>
        ///  Método que elimina un registro de cutter Spacer Splitter
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteCutterSpacerS(int id)
        {
            //Inicializamos los servicios de SO_SplitterCasting.
            SO_SplitterCasting ServicioSplitter = new SO_SplitterCasting();
            //Ejecutamos el método, devolvemos el resultado
            return ServicioSplitter.DeleteCutterSpacerS(id);
        }

        /// <summary>
        /// Método que obtiene todos los registros de Cutter Spacer Splitter
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllCutterSpacer(string texto)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            ObservableCollection<Herramental> Lista = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales de Cutter a partir del texto de búsqueda. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServiceSplitter.GetAllCutterSpacerS(texto);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                   // herramental.Encontrado = (bool)tipo.GetProperty("Activo").GetValue(item, null);

                    //Dim A
                    Propiedad dimA = new Propiedad();
                    dimA.Valor = (double)tipo.GetProperty("A").GetValue(item, null);
                    dimA.Unidad = "Milimeters (mm)";
                    dimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(dimA);

                    //DimB
                    Propiedad dimB = new Propiedad();
                    dimB.Valor= (double)tipo.GetProperty("B").GetValue(item, null);
                    dimB.Unidad = "Milimeters (mm)";
                    dimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(dimB);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(Lista, "CutterSpacerSplitter");
        }

        /// <summary>
        /// Obtiene la información de Spacer Splitter 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoCutterSpacer(string codigo)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            Herramental herramental = new Herramental();

            //Ejecutamos el método que busca los herramentales de Cutter a partir del texto de búsqueda. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServiceSplitter.GetInfoSpacer(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

             
                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_SPACER_SPLITTER").GetValue(item, null);

                    //Dim A
                    Propiedad dimA = new Propiedad();
                    dimA.Valor = (double)tipo.GetProperty("A").GetValue(item, null);
                    dimA.Unidad = "Milimeters (mm)";
                    dimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(dimA);

                    //DimB
                    Propiedad dimB = new Propiedad();
                    dimB.Valor = (double)tipo.GetProperty("B").GetValue(item, null);
                    dimB.Unidad = "Milimeters (mm)";
                    dimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(dimB);
                  
                }
            }
            //Retornamos el herramnetal.
            return herramental;
        }

        //Cutter Splitter

        /// <summary>
        /// Método que obtiene el herramental cutter de la operacion splitter casting
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Herramental GetCutterSplitterCasting(double v)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Declaramos un objeto de tipo herramental el cual será el que retornemos en el método.
            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServiceSplitter.GetCutter(v);

            //Comparamos si la inforamción obtenida es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos cada registro.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    //Agregamos la descripción que va en la hoja de ruta.
                    herramental.DescripcionRuta = "CUTTERS " + (double)tipo.GetProperty("Diametro").GetValue(item,null);
                }
            }

            //Retornamos el objeto construido.
            return herramental;
        }

        /*ChuckSplitter
         * 
         * 
        */

        /// <summary>
        /// Método que obtiene el herramental Chuck de la operación Splitter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Herramental GetChuckSplitter(double id)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Declaramos un objeto de tipo herramental el cual será el que retornemos en el método.
            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServiceSplitter.GetChuck(id);

            //Comparamos si la inforamción obtenida es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos cada registro.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    //Agregamos la descripción que va en la hoja de ruta.
                    herramental.DescripcionRuta = "CHUCK DET. " + (string)tipo.GetProperty("TipoEnsamble").GetValue(item,null);

                }
            }

            //Retornamos el objeto construido.
            return herramental;
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla ChuckSplitter
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllChuckSplitter(string texto)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            ObservableCollection<Herramental> ListaR = new ObservableCollection<Herramental>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServiceSplitter.GetAllChuck(texto);

            if (informacionBD !=null)
            {
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();
                    Herramental herramental = new Herramental();

                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    Propiedad diaMin = new Propiedad();
                    diaMin.Valor = (double)tipo.GetProperty("DiaMin").GetValue(item, null);
                    diaMin.Unidad = "Milimeters (mm)";
                    diaMin.DescripcionCorta = "Diametro Min";
                    herramental.Propiedades.Add(diaMin);

                    Propiedad diaMax = new Propiedad();
                    diaMax.Valor= (double)tipo.GetProperty("DiaMax").GetValue(item, null);
                    diaMax.Unidad= "Milimeters (mm)";
                    diaMax.DescripcionCorta = "Diametro Max";
                    herramental.Propiedades.Add(diaMax);

                    PropiedadCadena ensamble = new PropiedadCadena();
                    ensamble.Valor= (string)tipo.GetProperty("TipoEnsamble").GetValue(item, null);
                    ensamble.DescripcionCorta = "Tipo Ensamble";
                    herramental.PropiedadesCadena.Add(ensamble);

                    ListaR.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaR, "ChuckSplitter");
        }

        /// <summary>
        ///  Obtiene la información de Chuck Splitter a partir del código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoChuckSplitter(string codigo)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            Herramental herramental = new Herramental();

            //Ejecutamos el método que busca los herramentales de Cutter a partir del texto de búsqueda. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServiceSplitter.GetInfoChuckS(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();


                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_Chuck").GetValue(item, null);

                    Propiedad diaMin = new Propiedad();
                    diaMin.Valor = (double)tipo.GetProperty("DiaMin").GetValue(item, null);
                    diaMin.Unidad = "Milimeters (mm)";
                    diaMin.DescripcionCorta = "Diametro Min";
                    herramental.Propiedades.Add(diaMin);

                    Propiedad diaMax = new Propiedad();
                    diaMax.Valor = (double)tipo.GetProperty("DiaMax").GetValue(item, null);
                    diaMax.Unidad = "Milimeters (mm)";
                    diaMax.DescripcionCorta = "Diametro Max";
                    herramental.Propiedades.Add(diaMax);

                    PropiedadCadena ensamble = new PropiedadCadena();
                    ensamble.Valor = (string)tipo.GetProperty("TipoEnsamble").GetValue(item, null);
                    ensamble.DescripcionCorta = "Tipo Ensamble";
                    herramental.PropiedadesCadena.Add(ensamble);

                }
            }
            //Retornamos el objeto.
            return herramental;
        }


        /// <summary>
        /// Método que guarda un registro de herramental ChuckSplitter
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetChuckSplitter(Herramental obj)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Ejecutamos el método
            return ServiceSplitter.SetChuck(obj.Codigo, obj.Propiedades[0].Valor, obj.Propiedades[1].Valor,obj.PropiedadesCadena[0].Valor);
        }

        /// <summary>
        /// Método que modifica un registro de la tabla chuck Splitter
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateChuckSplitter(Herramental obj)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Ejecutamos el método
            return ServiceSplitter.UpdateChuck(obj.idHerramental,obj.Codigo, obj.Propiedades[0].Valor, obj.Propiedades[1].Valor, obj.PropiedadesCadena[0].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de la tabla Chuck Splitter
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static int DeleteChuckSplitter(int id)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Ejecutamos el método
            return ServiceSplitter.DeleteChuck(id);
        }

        //Uretano Splitter

        /// <summary>
        /// Método que indica si el componente debe de llevar uretano.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool GetHasUretanoSplitter(double id)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Declaramos una bandera la cual será la que retornemos en el método.
            bool respuesta = false;

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServiceSplitter.GetHasUretano(id);

            //Comparamos si el resultado es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos los resultados obtenidos.
                foreach (var item in informacionBD)
                {
                    //Asignamos un valor true a la bandera, lo cual significa que si existe un valor.
                    respuesta = true;
                }
            }

            //Retornamos el valor obtenido.
            return respuesta;
        }

        /// <summary>
        /// Método que obtiene el Herramental Uretano de la operación Splitter Casting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Herramental GetUretanoSplitter(double id)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServicioSplitter = new SO_SplitterCasting();

            //Declaramos un objeto de tipo herramental el cual será el que retornemos en el método.
            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServicioSplitter.GetUretano(id);

            //Comparamos si la inforamción obtenida es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos cada registro.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    //Obtenemos los valores de Detalle y color del herramental.
                    string detalle, color;
                    detalle = (string)tipo.GetProperty("Detalle").GetValue(item, null);
                    color = (string)tipo.GetProperty("Color").GetValue(item, null);

                    //Agregamos la descripción que va en la hoja de ruta.
                    herramental.DescripcionRuta = "URETANOS DET. " + detalle + " " + color;
                }
            }

            //Retornamos el objeto construido.
            return herramental;
        }

        /// <summary>
        /// Método que obtiene todos los registros de Uretano o las coincidencias con el texto de búsqueda.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllUretano(string texto)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            ObservableCollection<Herramental> ListaR = new ObservableCollection<Herramental>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServiceSplitter.GetAllUretano(texto);

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();
                    Herramental herramental = new Herramental();

                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    Propiedad diaMin = new Propiedad();
                    diaMin.Valor = (double)tipo.GetProperty("DiaMin").GetValue(item, null);
                    diaMin.Unidad = "Milimeters (mm)";
                    diaMin.DescripcionCorta = "Diametro Min";
                    herramental.Propiedades.Add(diaMin);

                    Propiedad diaMax = new Propiedad();
                    diaMax.Valor = (double)tipo.GetProperty("DiaMax").GetValue(item, null);
                    diaMax.Unidad = "Milimeters (mm)";
                    diaMax.DescripcionCorta = "Diametro Max";
                    herramental.Propiedades.Add(diaMax);

                    PropiedadCadena ensamble = new PropiedadCadena();
                    ensamble.Valor = (string)tipo.GetProperty("Medidas").GetValue(item, null);
                    ensamble.DescripcionCorta = "Medidas";
                    herramental.PropiedadesCadena.Add(ensamble);

                    ListaR.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaR, "UretanoSplitter");
        }

        /// <summary>
        ///  Obtiene la información de Uretano Splitter a partir del código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoUretanoSplitter(string codigo)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            Herramental herramental = new Herramental();

            //Ejecutamos el método que busca los herramentales de Cutter a partir del texto de búsqueda. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServiceSplitter.GetInfoUretanoS(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_URETANO_SPLITTER").GetValue(item, null);

                    Propiedad diaMin = new Propiedad();
                    diaMin.Valor = (double)tipo.GetProperty("DiaMin").GetValue(item, null);
                    diaMin.Unidad = "Milimeters (mm)";
                    diaMin.DescripcionCorta = "Diametro Min";
                    herramental.Propiedades.Add(diaMin);

                    Propiedad diaMax = new Propiedad();
                    diaMax.Valor = (double)tipo.GetProperty("DiaMax").GetValue(item, null);
                    diaMax.Unidad = "Milimeters (mm)";
                    diaMax.DescripcionCorta = "Diametro Max";
                    herramental.Propiedades.Add(diaMax);

                    PropiedadCadena ensamble = new PropiedadCadena();
                    ensamble.Valor = (string)tipo.GetProperty("Medidas").GetValue(item, null);
                    ensamble.DescripcionCorta = "Medidas";
                    herramental.PropiedadesCadena.Add(ensamble);

                    PropiedadCadena color = new PropiedadCadena();
                    color.Valor = (string)tipo.GetProperty("Color").GetValue(item, null);
                    color.DescripcionCorta = "Medidas";
                    herramental.PropiedadesCadena.Add(color);

                    PropiedadCadena detalle = new PropiedadCadena();
                    detalle.Valor = (string)tipo.GetProperty("Detalle").GetValue(item, null);
                    detalle.DescripcionCorta = "Detalle";
                    herramental.PropiedadesCadena.Add(detalle);

                }
            }
            //Retornamos el objeto.
            return herramental;
        }

        /// <summary>
        /// Método que guarda un registro de Uretano Splitter.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetUretanoSplitter(Herramental obj)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Ejecutamos el método
            return ServiceSplitter.SetUretano(obj.Codigo,obj.PropiedadesCadena[0].Valor, obj.PropiedadesCadena[1].Valor, obj.Propiedades[0].Valor, obj.Propiedades[1].Valor, obj.PropiedadesCadena[2].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de Uretano Splitter.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateUretanoSplitter(Herramental obj)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Ejecutamos el método
            return ServiceSplitter.UpdateUretano(obj.idHerramental,obj.Codigo, obj.PropiedadesCadena[0].Valor, obj.PropiedadesCadena[1].Valor, obj.Propiedades[0].Valor, obj.Propiedades[1].Valor, obj.PropiedadesCadena[2].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de Uretano Splitter.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteUretanoSplitter(int id)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Ejecutamos el método
            return ServiceSplitter.DeleteUretano(id);
        }

        /*CutterSplitter
         * 
         * 
         */

        /// <summary>
        /// Método que obtiene todos los registros de Cutter Splitter
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllCutterSplitter(string texto)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            ObservableCollection<Herramental> Lista = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales de Cutter a partir del texto de búsqueda. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServiceSplitter.GetAllCutter(texto);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    //herramental.Encontrado = (bool)tipo.GetProperty("Activo").GetValue(item, null);

                    //Diametro
                    Propiedad diametro = new Propiedad();
                    diametro.Valor= (double)tipo.GetProperty("Diametro").GetValue(item, null);
                    diametro.Unidad = "Milimeters (mm)";
                    diametro.DescripcionCorta = "Diametro";
                    herramental.Propiedades.Add(diametro);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(Lista, "CutterSplitter");
        }

        /// <summary>
        /// Obtiene la información de Cutter Splitter .
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoCutterSplitter(string codigo)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            Herramental herramental = new Herramental();

            //Ejecutamos el método que busca los herramentales de Cutter a partir del texto de búsqueda. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServiceSplitter.GetInfoCutter(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();


                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_CUTTER_SPLITTER").GetValue(item, null);

                    //Diametro
                    Propiedad diametro = new Propiedad();
                    diametro.Valor = (double)tipo.GetProperty("Diametro").GetValue(item, null);
                    diametro.Unidad = "Milimeters (mm)";
                    diametro.DescripcionCorta = "Diametro";
                    herramental.Propiedades.Add(diametro);

                }
            }
            return herramental;
        }


        /// <summary>
        /// Método que agrega un resgitro de  herramental cutter de la operacion splitter casting
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetCutterSplitterCasting(Herramental obj)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Ejecutamos el método
            return ServiceSplitter.SetCutter(obj.Codigo, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que actualiza un resgitro de  herramental cutter de la operacion splitter casting
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateCutterSplitterCasting(Herramental obj)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Ejecutamos el método 
            return ServiceSplitter.UpdateCutter(obj.idHerramental,obj.Codigo, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que elimina un resgitro de  herramental cutter de la operacion splitter casting
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteCutterSplitterCasting(int id)
        {
            //Inicializamos los servicios de Splitter.
            SO_SplitterCasting ServiceSplitter = new SO_SplitterCasting();

            //Ejecutamos el método
            return ServiceSplitter.DeleteCutter(id);
        }
        #endregion

        #region Finish Grid

        /// <summary>
        /// Método que busca el herramental Guide Bar de la operación Finish Grind
        /// </summary>
        /// <param name="widthOperacion"></param>
        /// <returns></returns>
        public static Herramental GetGuideBarFinishGrind(double widthOperacion)
        {
            //Inicializamos los servicios de Finish Grind.
            SO_FinishGrind ServicioFinishGrind = new SO_FinishGrind();

            //Declaramos un objeto de tipo Herramental el cual será el que retornemos en el método.
            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información de la base de datos. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioFinishGrind.GetGuideBar(widthOperacion);

            //Verificamos que el resultado de la busqueda sea diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    //Obtenemos la propiedad EspesorBarraGuia.
                    double espesorBarraGuia = (double)tipo.GetProperty("EspesorBarraGuia").GetValue(item, null);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "GUIDE BAR   " + espesorBarraGuia;
                }
            }            
            //Retornamos el herramental.
            return herramental;
        }

        #endregion

        #region Cam Turn

        /// <summary>
        /// Método que obtiene el time de la operación Cam Turn
        /// </summary>
        /// <param name="v"></param>
        /// <param name="especificacion"></param>
        /// <returns></returns>
        public static string GetTimeCamTurn(string v, PropiedadCadena especificacion)
        {
            //Incializamos los servicios de CamTurn
            SO_CamTurn ServicioCam = new SO_CamTurn();

            //Declaramos una cadena la cual será la que retornemos en el método.
            string respuesta = string.Empty;

            //Ejecutamos el método para obtener los datos de la base de datos. El resultado lo guardamos en un dataset.
            DataSet datos = ServicioCam.GetTimeCamTurn(v, especificacion.Valor);

            //Comparamos que el data set sea diferente de nulo.
            if (datos != null)
            {
                //Comparamos que el dataset contenga al menos una tabla, y que la tabla cero contenga al menos un registro.
                if (datos.Tables.Count > 0 && datos.Tables[0].Rows.Count > 0)
                {
                    //Iteramos el dataset.
                    foreach (DataRow item in datos.Tables[0].Rows)
                    {
                        //Obtenemos el valor y lo asignamos a la variable local.
                        respuesta = item["CAM_TURN"].ToString();

                    }
                }
            }

            //Retornamos la cadena.
            return respuesta;
        }

        /// <summary>
        /// Método que obtiene el cutter angle de la operación Cam Turn
        /// </summary>
        /// <param name="cutterAngle"></param>
        /// <returns></returns>
        public static string GetCutterAngleCamTurn(double cutterAngle)
        {
            //Declaramos una variable la cual será la que retornemos en el método.
            string respuesta = string.Empty;

            //Inicializamos los servicios de CutterAngle.
            SO_CutterAngle ServiceCutter = new SO_CutterAngle();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCutter.GetCutterAngle(cutterAngle);

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Obtenemos los valores del elemento y los mapeamos a variables locales.
                    double grados = (double)tipo.GetProperty("GRADOS").GetValue(item, null);
                    double minutos = (double)tipo.GetProperty("MINUTOS").GetValue(item, null);

                    //Concatenamos los elementos y los asiganmos a la variable que retornaremos.
                    respuesta = "CUTTER ANGLE DEG. " + grados + " MIN. " + minutos;
                }
            }

            //Retornamos el valor.
            return respuesta;
        }

        /// <summary>
        /// Método que obtiene todos los registros de CollarSpacer
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllCollarSpacer(string texto)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCam.GetAllCollarSpacer(texto);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.Plano= (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDimE = new Propiedad();
                    propiedadDimE.Unidad = "";
                    propiedadDimE.Valor = (double)tipo.GetProperty("DimE").GetValue(item, null);
                    propiedadDimE.DescripcionCorta = "Dim E";
                    herramental.Propiedades.Add(propiedadDimE);

                    Propiedad propiedadDimF = new Propiedad();
                    propiedadDimF.Unidad = "";
                    propiedadDimF.Valor = (double)tipo.GetProperty("DimF").GetValue(item, null);
                    propiedadDimF.DescripcionCorta = "Dim F";
                    herramental.Propiedades.Add(propiedadDimF);

                    PropiedadCadena propiedadMN = new PropiedadCadena();
                    propiedadMN.DescripcionCorta = "Medida Nominal";
                    propiedadMN.Valor = (string)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propiedadMN);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "CollarSpacer");
        }

        /// <summary>
        /// Obtiene la información de Spacer Splitter 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoCollarSpacer(string codigo)
        {
           
            Herramental herramental = new Herramental();

            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCam.GetInfoCollarSpacer(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_CollarSpacer").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDimE = new Propiedad();
                    propiedadDimE.Unidad = "";
                    propiedadDimE.Valor = (double)tipo.GetProperty("DimE").GetValue(item, null);
                    propiedadDimE.DescripcionCorta = "Dim E";
                    herramental.Propiedades.Add(propiedadDimE);

                    Propiedad propiedadDimF = new Propiedad();
                    propiedadDimF.Unidad = "";
                    propiedadDimF.Valor = (double)tipo.GetProperty("DimF").GetValue(item, null);
                    propiedadDimF.DescripcionCorta = "Dim F";
                    herramental.Propiedades.Add(propiedadDimF);

                    PropiedadCadena propiedadMN = new PropiedadCadena();
                    propiedadMN.DescripcionCorta = "Medida Nominal";
                    propiedadMN.Valor = (string)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propiedadMN);

                    PropiedadCadena descripcion = new PropiedadCadena();
                    descripcion.Valor = (string)tipo.GetProperty("DESCRIPCIONCT").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(descripcion);
                }
            }
            return herramental;
        }


        /// <summary>
        /// Método que obtiene los mejores herramentales Collar a partir de small y pc.
        /// </summary>
        /// <param name="small_od"></param>
        /// <param name="pc"></param>
        /// <returns></returns>
        public static ObservableCollection<Herramental> GetCollarSpacer(double small_od, double pc)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            double dimE_min,dimeE_max,dimF_min,dimF_max;

            dimE_min = small_od - GetCriterio("CTCollarDimEMin");
            dimeE_max = small_od + GetCriterio("CTCollarDimEMax");
            dimF_min = pc - GetCriterio("CTCollarDimFMin");
            dimF_max = pc + GetCriterio("CTCollarDimFMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCam.GetCollarSpacer(dimE_min, dimeE_max, dimF_min, dimF_max);

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    herramental.DescripcionMedidasBusqueda = (string)tipo.GetProperty("DESCRIPCIONCT").GetValue(item, null);

                    Propiedad propiedadDimE = new Propiedad();
                    propiedadDimE.Unidad = "";
                    propiedadDimE.Valor = (double)tipo.GetProperty("DimE").GetValue(item, null);
                    propiedadDimE.DescripcionCorta = "Dim E";
                    herramental.Propiedades.Add(propiedadDimE);

                    Propiedad propiedadDimF = new Propiedad();
                    propiedadDimF.Unidad = "";
                    propiedadDimF.Valor = (double)tipo.GetProperty("DimF").GetValue(item, null);
                    propiedadDimF.DescripcionCorta = "Dim F";
                    herramental.Propiedades.Add(propiedadDimF);

                    herramental.DescripcionRuta = "COLLAR SPACER ";
                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            
            return ListaResultante;
        }

        /// <summary>
        /// Obtiene los dos primeros herramentales  para CollarSpacer CamTurn.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBestCollarSpacer(ObservableCollection<Herramental> lista)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Si la lista es diferente de nulo.
            if (lista !=null)
            {
                //Recorremos la lista, para buscar el collar.
                foreach (Herramental item in lista)
                {
                    //La primera coincidencia con descripción collar lo agrega a la nueva lista.
                    if (item.DescripcionMedidasBusqueda == "COLLAR")
                    {
                        ListaResultante.Add(item);
                        //Sale del ciclo.
                        break;
                    }
                }

                //Recorremos la lista, para buscar el spacer.
                foreach (Herramental objeto in lista)
                {
                    //La primera coincidencia con descripción spacer lo agrega a la nueva lista.
                    if (objeto.DescripcionMedidasBusqueda == "SPACER")
                    {
                        ListaResultante.Add(objeto);
                        //Sale del ciclo.
                        break;
                    }
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "CollarSpacer");
        }

        /// <summary>
        /// Método que inserta un registro en la tabla CollarSpacer
        /// </summary>
        /// <param name="herramental"></param>
        /// <returns></returns>
        public static int SetCollarSpacer(Herramental herramental)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            return ServiceCam.SetCollarSpacer(herramental.Codigo, herramental.Plano, herramental.PropiedadesCadena[0].Valor, herramental.PropiedadesCadena[1].Valor, herramental.Propiedades[0].Valor, herramental.Propiedades[1].Valor);
        }

        /// <summary>
        /// Método que modifica un registro en la tabla CollarSpacer
        /// </summary>
        /// <param name="herramental"></param>
        /// <returns></returns>
        public static int UpdateCollarSpacer(Herramental herramental)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();
             
            //Ejecutamos el método, retornamos el resultado.
            return ServiceCam.UpdateCollarSpcaer(herramental.idHerramental,herramental.Codigo, herramental.Plano, herramental.PropiedadesCadena[0].Valor, herramental.PropiedadesCadena[1].Valor, herramental.Propiedades[0].Valor, herramental.Propiedades[1].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de Collar Spcaer.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteCollarSpacer(int id)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            //Ejecutamos el método, retornamos el resultado.
            return ServiceCam.DeleteCollarSpacer(id);
        }

        /// <summary>
        /// Método que obtiene todos los registros de WorkCam
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllWorkCam(string texto)
        {
            //Inicializamos los servicios de CamTurn.
           SO_CamTurn ServiceCam = new SO_CamTurn();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCam.GetAllWorkCam(texto);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    PropiedadCadena propiedadMN = new PropiedadCadena();
                    propiedadMN.DescripcionCorta = "Medida Nominal";
                    propiedadMN.Valor = (string)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propiedadMN);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "WorkCam");
        }
      
        /// <summary>
        /// Método que obtiene los herramentales óptimos para WorkCam.
        /// </summary>
        /// <param name="material"></param>
        /// <param name="tipoAnillo"></param>
        /// <param name="pingGage"></param>
        public static DataTable GetWorkCam(string material,string tipoAnillo,string pingGage)
        {
            string cam_detail = string.Empty;

            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCamT = new SO_CamTurn();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Obtenemos la propiedad de Cam_Detail.
            DataSet InformacionBD = ServiceCamT.GetCam_Detail(material, tipoAnillo, pingGage);

            //Verificamos que el objeto recibido sea distinto de vacío.
            if (InformacionBD != null)
            {
                //Si la lista tiene información.
                if (InformacionBD.Tables.Count > 0 && InformacionBD.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow element in InformacionBD.Tables[0].Rows)
                    {
                        cam_detail = element["cam_detail"].ToString();
                    }
                }

                //Si la propiedad es diferente de vacío.
                if (!string.IsNullOrEmpty(cam_detail))
                {
                    //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
                    IList ListaBD = ServiceCamT.GetWorkCam(cam_detail);                  

                    //Verificamos que la información obtenida sea diferente de nulo.
                    if (ListaBD != null)
                    {
                        //Itermos la lista obtenida.
                        foreach (var item in ListaBD)
                        {
                            //Obtenemos el tipo del elemento iterado.
                            System.Type tipo = item.GetType();

                            //Declaramos un objeto de tipo Herramental.
                            Herramental herramental = new Herramental();
                            //Convertimos la información a tipo Herramental.
                            herramental = ReadInformacionHerramentalEncontrado(ListaBD);

                            PropiedadCadena propiedadMN = new PropiedadCadena();
                            propiedadMN.DescripcionCorta = "Medida Nominal";
                            propiedadMN.Valor = (string)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                            herramental.PropiedadesCadena.Add(propiedadMN);

                            //Mapeamos el valor a DescipcionRuta.
                            herramental.DescripcionRuta = "WORK CAM MEDIDA NOMINAL "+ propiedadMN.Valor;

                            //Agregamos el objeto a la lista resultante.
                            ListaResultante.Add(herramental);
                        }
                    }
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "WorkCam");
        }

        /// <summary>
        /// Obtiene la información de un herramental de WokCam.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoWorkCam(string codigo)
        {

            Herramental herramental = new Herramental();

            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCam.GetInfoWorkCam(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_WorkCam").GetValue(item, null);

                    PropiedadCadena propiedadMN = new PropiedadCadena();
                    propiedadMN.DescripcionCorta = "Medida Nominal";
                    propiedadMN.Valor = (string)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propiedadMN);

                    PropiedadCadena desc = new PropiedadCadena();
                    desc.Valor = (string)tipo.GetProperty("DESCRIPCIONWC").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(desc);
                }
            }
            //Retornamos el objeto.
            return herramental;
        }

        /// <summary>
        /// Método que obtiene el mejor herramental para WorkCam.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectWorkCam(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Medida Nominal");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Medida Nominal"] = row["Medida Nominal"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            //REtornamos la tabla.
            return DataR;
        }

        /// <summary>
        /// Método que inserta un registro en la tabla WorkCam.
        /// </summary>
        /// <param name="herramental"></param>
        /// <returns></returns>
        public static int SetWorkCam(Herramental herramental)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            return ServiceCam.SetWorkCam(herramental.Codigo,  herramental.PropiedadesCadena[0].Valor, herramental.PropiedadesCadena[1].Valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="herramental"></param>
        /// <returns></returns>
        public static int UpdateWorkCam(Herramental herramental)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            return ServiceCam.UpdateWorkCam(herramental.idHerramental,herramental.Codigo, herramental.PropiedadesCadena[0].Valor, herramental.PropiedadesCadena[1].Valor);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteWorkCam(int id)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            return ServiceCam.DeleteWorkCam(id);
        }


        /// <summary>
        /// Método que obtiene todos los registros de CamTurn.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllCutterCamTurn(string texto)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCam.GetAllCutterCamTurn(texto);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.Plano= (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDim = new Propiedad();
                    propiedadDim.Unidad = "";
                    propiedadDim.Valor = (double)tipo.GetProperty("Dimencion").GetValue(item, null);
                    propiedadDim.DescripcionCorta = "Dimensión";
                    herramental.Propiedades.Add(propiedadDim);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "CutterCamTurn");
        }

        /// <summary>
        /// Obtiene los herramentales óptimos para Cutter Cam Turn
        /// </summary>
        /// <param name="material"></param>
        /// <param name="width"></param>
        /// <returns></returns>
        public static DataTable GetCutterCamTurn(string material, double width)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();            

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            string tipoMaterial = GetTipoMaterial(material);

            IList informacionBD = ServiceCam.GetCutterCam(tipoMaterial, width);

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();
                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    Propiedad propiedadDim = new Propiedad();
                    propiedadDim.Unidad = "";
                    propiedadDim.Valor = (double)tipo.GetProperty("Dimencion").GetValue(item, null);
                    propiedadDim.DescripcionCorta = "Dimensión";
                    herramental.Propiedades.Add(propiedadDim);

                    herramental.DescripcionRuta = "CUTTER CAM TURN DIMENSIÓN " + propiedadDim.Valor;
                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "CutterCamTurn");
        }



        /// <summary>
        /// Obtiene la información de un herramental de Cutter Cam Turn.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoCutterCam(string codigo)
        {

            Herramental herramental = new Herramental();

            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCam.GetInfoCutterCam(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_CutterCamTurn").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDim = new Propiedad();
                    propiedadDim.Unidad = "";
                    propiedadDim.Valor = (double)tipo.GetProperty("Dimencion").GetValue(item, null);
                    propiedadDim.DescripcionCorta = "Dimensión";
                    herramental.Propiedades.Add(propiedadDim);

                    PropiedadCadena desc = new PropiedadCadena();
                    desc.Valor= (string)tipo.GetProperty("DESCRIPCIONCM").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(desc);
                }
            }
            return herramental;
        }


        /// <summary>
        /// Método que obtiene el mejor herramental para CutterCamTurn.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBestCutterCT(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Dimensión");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Dimensión"] = row["Dimensión"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            return DataR;
        }

        /// <summary>
        /// Método que inserta un registro en la tabla de CutterCamTurn.
        /// </summary>
        /// <param name="herramental"></param>
        /// <returns></returns>
        public static int SetCutterCamTurn(Herramental herramental)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            //Ejecutamos el método.
            return ServiceCam.SetCutterCamTurn(herramental.Codigo, herramental.PropiedadesCadena[0].Valor, herramental.Propiedades[0].Valor, herramental.Plano);
        }

        /// <summary>
        /// étodo que modifica un registro en la tabla de CutterCamTurn.
        /// </summary>
        /// <param name="herramental"></param>
        /// <returns></returns>
        public static int UpdateCutterCamTurn(Herramental herramental)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            //Ejecutamos el método.
            return ServiceCam.UpdateCutterCamTurn(herramental.idHerramental,herramental.Codigo, herramental.PropiedadesCadena[0].Valor, herramental.Propiedades[0].Valor, herramental.Plano);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idH"></param>
        /// <returns></returns>
        public static int DeleteCutterCamTurn(int idH)
        {
            //Inicializamos los servicios de CamTurn.
            SO_CamTurn ServiceCam = new SO_CamTurn();

            //Ejecutamos el método.
            return ServiceCam.DeleteCutterCamTurn(idH);
        }
        #endregion

        #region Auto Finish Turn
        /// <summary>
        /// Método que obtiene los collars de Auto Fin Turn a partir de los parámetros recibidos.
        /// </summary>
        /// <param name="maxA"></param>
        /// <param name="minB"></param>
        /// <returns></returns>
        public static DataTable GetCollarBK(double maxA, double minB)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioBk.GetCollar(maxA, minB);

            //Verificamos que la lista sea diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = (string)tipo.GetProperty("DIM_A_UNIDAD").GetValue(item, null);
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIM_A").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = (string)tipo.GetProperty("DIM_B_UNIDAD").GetValue(item, null);
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIM_B").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    PropiedadCadena propiedadParte = new PropiedadCadena();
                    propiedadParte.DescripcionCorta = "Parte";
                    propiedadParte.Valor = (string)tipo.GetProperty("PARTE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propiedadParte);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "CollarBK");
        }

        /// <summary>
        /// Método que obtiene los collars de Auto Fin Turn a partir de los parámetros recibidos.
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        public static DataTable GetCollarBK(string busqueda)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioBk.GetAllCollar(busqueda);

            //Verificamos que la lista sea diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);

                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = (string)tipo.GetProperty("DIM_A_UNIDAD").GetValue(item, null);
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIM_A").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = (string)tipo.GetProperty("DIM_B_UNIDAD").GetValue(item, null);
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIM_B").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    PropiedadCadena propiedadParte = new PropiedadCadena();
                    propiedadParte.DescripcionCorta = "Parte";
                    propiedadParte.Valor = (string)tipo.GetProperty("PARTE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propiedadParte);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "CollarBK");
        }

        /// <summary>
        /// Obtiene la información del herramental de Closing Sleeve BK.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoCollarBK(string codigo)
        {
            Herramental herramental = new Herramental();

            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioBk.GetInfoCollarBK(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_COLLAR_BK").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = (string)tipo.GetProperty("DimA_Unidad").GetValue(item, null);
                    propiedadDimA.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = (string)tipo.GetProperty("DimB_Unidad").GetValue(item, null);
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    PropiedadCadena propiedadParte = new PropiedadCadena();
                    propiedadParte.DescripcionCorta = "Parte";
                    propiedadParte.Valor = (string)tipo.GetProperty("Parte").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propiedadParte);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que selecciona los mejores collar´s, a partir de una tabla recibida en el parámetro.
        /// </summary>
        /// <param name="datatable"></param>
        /// <returns></returns>
        public static DataTable SelectBestCollar(DataTable datatable)
        {
            //Incializamos los servicios de BK.
            SO_BK ServicioBK = new SO_BK();

            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable dtResultante = new DataTable();

            //Agregamos las columnas de code y descripction a la tabla.
            dtResultante.Columns.Add("Code");
            dtResultante.Columns.Add("Description");

            //Iteramos los registros del data table. (Este for solo será de un ciclo.)
            foreach (DataRow row in datatable.Rows)
            {
                //Obtenemos los valores del item iterado.
                string codigo = row["CODE"].ToString();
                string descripcion = row["DESCRIPTION"].ToString();
                double dima = Convert.ToDouble(row["Dim A"].ToString());
                double dimb = Convert.ToDouble(row["Dim B"].ToString());
                string parte = row["Parte"].ToString();

                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = dtResultante.NewRow();
                dr["Code"] = codigo;
                dr["Description"] = descripcion;

                //Agregamnos el datarow al datatable resultante.
                dtResultante.Rows.Add(dr);

                //Ejecutamos el método para buscar la otra parte del collarin. El resultado lo guardamos en una lista anónima.
                IList informacionBD = ServicioBK.GetCollar(dima, dimb, parte);

                //Verificamos que la lista sea diferente de nulo.
                if (informacionBD != null)
                {
                    //Iteramos la lista.(Este for solo será de un ciclo.)
                    foreach (var item in informacionBD)
                    {
                        //Obtenemos el tipo del elemento iterado.
                        System.Type tipo = item.GetType();

                        //Creacion un DataRow en la datatable resultante.
                        DataRow dr1 = dtResultante.NewRow();

                        //Mapeamos los valores correspondientes de codigo y descripión.
                        dr1["Code"] = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                        dr1["Description"] = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);

                        //Agregamos el datarow a la datatable resultante.
                        dtResultante.Rows.Add(dr1);

                        //Salimos del ciclo-
                        break;
                    }
                }
                //Salimos del ciclo.
                break;
            }

            //Retornamos el datatable resultante.
            return dtResultante;
        }

        /// <summary>
        /// Método que retorna una cadena de texto la cual contiene las medidas de fabricación para un collarin de bk a partir de las medidas maxA y MinB
        /// </summary>
        /// <param name="maxA"></param>
        /// <param name="minB"></param>
        /// <returns></returns>
        public static string GetCotasFabricacionCollarBK(double maxA, double minB)
        {
            string medidasFabricacion = string.Empty;

            medidasFabricacion = "DIM \"A\"= " + maxA + "\n";
            medidasFabricacion += "DIM \"B\"= " + minB + "\n";

            double dimC, dimD, dimE = 0;

            if (maxA >= 2.187 && maxA <= 2.999) 
            {
                /*
                 * Cálculo para el siguiente plano
                 * PT. 744 RL40-283
                */
                dimC = maxA + 0.187;
                dimD = maxA + 0.3750;

                medidasFabricacion += dimC >= 2.750 ? "DIM \"C\"= " + 2.750 + "\n" : "DIM \"C\"= " + dimC + "\n";
                medidasFabricacion += dimD >= 2.938 ? "DIM \"D\"= " + 2.938 + "\n" : "DIM \"D\"= " + dimD + "\n";

            }
            else if(maxA >= 2.938 && maxA <= 3.499)
            {
                /*
                 * Cálculo para el siguiente plano
                 * PT 746 RL40-284
                 */
                dimC = maxA + 0.062;
                dimD = maxA + 0.375;
                dimE = minB - 0.125;

                medidasFabricacion += "DIM \"C\"=  " + dimC + "\n";
                medidasFabricacion += "DIM \"D\"=  " + dimD + "\n";
                medidasFabricacion += "DIM \"E\"=  " + dimE + "\n";
                medidasFabricacion += "NOTA: DIMENSIÓN \"E\" MINIMO DEBE SER E=2.770";

            }
            else if (maxA >= 3.380 && maxA <= 6.783)
            {
                medidasFabricacion = "Falta calculo";
            }
            else
                medidasFabricacion = "No se obtubo el calculo para obtener las medidas de fabricación.";

            return medidasFabricacion;
        }

        /// <summary>
        /// Método que inserta un registro collarBK
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetCollarBK(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Ejecutamos el método
            return ServicioBk.SetCollar(obj.Codigo, obj.Plano, obj.PropiedadesCadena[0].Valor, obj.Propiedades[0].Valor, obj.Propiedades[0].Unidad, obj.Propiedades[1].Valor, obj.Propiedades[1].Unidad);
        }

        /// <summary>
        /// Método que modifica un registro de la tabla Collar BK.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateCollarBK(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Ejecutamos el método
            return ServicioBk.UpdateCollar(obj.idHerramental,obj.Codigo, obj.Plano, obj.PropiedadesCadena[0].Valor, obj.Propiedades[0].Valor, obj.Propiedades[0].Unidad, obj.Propiedades[1].Valor, obj.Propiedades[1].Unidad);
        }

        /// <summary>
        /// Método que elimina un registro de Collar BK.
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public static int DeleteCollarBK(int Id)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Ejecutamos el método.
            return ServicioBk.DeleteCollar(Id);
        }

        /// <summary>
        /// Método que obtiene un registro a partir de los parámetros recibidos
        /// </summary>
        /// <param name="diafinish"></param>
        /// <param name="gapfinish"></param>
        public static DataTable GetClosingSleeve(double diafinish, double gapfinish)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServiceBK = new SO_BK();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Obtenemos el valor de Sleeve
            //double sleeve=Math.Round(diafinish-((gapfinish - .004) / 3.1416),3);
            //double sleeve = Math.Round(diafinish - ((gapfinish) / 3.1416), 3);
            double sleeve = Math.Round(diafinish,3);

            //Obtenemos el minimo y maximo
            //double sleeveMin = sleeve + 0.006;
            //double sleeveMax = sleeve + 0.013;

            double sleeveMin = sleeve + 0.002;
            double sleeveMax = sleeve + 0.008;

            //Se obtiene la informacion de la base de datos
            IList informacionBD = ServiceBK.GetClosingSleeveBK(sleeveMin, sleeveMax);
            

            //Si la informacion es diferente de nulo
            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD,(string)tipo.GetProperty("Codigo").GetValue(item,null));
                   
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "CLOSING SLEEVE BK  " + propiedadDimB.Valor;
                    //Agrega el objeto a la lista
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "ClosingSleeve");
        }

        /// <summary>
        /// Obtiene el mejor registro de best closing BK
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBestClosingBK(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Dim B");
            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Dim B"] = row["Dim B"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            return DataR;
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla ClosingSleeve
        /// </summary>
        /// <param name="texto_busqueda"></param>
        /// <returns></returns>
        public static DataTable GetAllClosingSleeve(string texto_busqueda)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioBk.GetAllClosingSleeveBK(texto_busqueda);

            //Verificamos que la lista sea diferente de nulo.
            if (informacionBD !=null)
            {
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                     System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    Propiedad dim = new Propiedad();
                    dim.Unidad = "Milimeters (mm)";
                    dim.Valor = (double)tipo.GetProperty("DimB").GetValue(item, null);
                    dim.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(dim);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "ClosingSleeve");
        }

        /// <summary>
        /// Obtiene la información del herramental de Closing Sleeve BK.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoClosingSleeveBK(string codigo)
        {
            Herramental herramental = new Herramental();

            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioBk.GetInfoClosingSleeve(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_CLOSINGSLEEVE_BK").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad dim = new Propiedad();
                    dim.Unidad = "Milimeters (mm)";
                    dim.Valor = (double)tipo.GetProperty("DimB").GetValue(item, null);
                    dim.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(dim);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que guarda un registro en  la tabla
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetClosingSleeveBK(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServiceBk = new SO_BK();

            //Ejecutamos el método
            return ServiceBk.SetClosingSleeveBK(obj.Codigo, obj.Propiedades[0].Valor, obj.Plano);
        }

        /// <summary>
        /// Método que actualiza un registro.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateClosingSleeveBK(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServiceBk = new SO_BK();

            //Ejecutamos el método
            return ServiceBk.UpdateClosingSleeveBK(obj.idHerramental,obj.Codigo, obj.Propiedades[0].Valor, obj.Plano);
        }

        /// <summary>
        /// Método que elimina un registro de la tabla Closing Sleeve BK
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteClosingSleeveBK(int id)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServiceBk = new SO_BK();

            //Ejecutamos el método, retornamos el resultado.
            return ServiceBk.DeleteClosingSleeveBK(id);
        }

        //GuidePLate
        /// <summary>
        /// Método que obtiene los registros de GuidePlate de acuerdo al texto de búsqueda.
        /// </summary>
        /// <param name="texto"></param>
          public static DataTable GetAllGuidePlate(string texto)
            {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioBk.GetAllGuidePlate(texto);

            //Si la información es diferente de nulo.
            if (informacionBD !=null)
            {
                //Iteramos la lista.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();                    

                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    PropiedadCadena medida = new PropiedadCadena();
                    medida.Valor= (string)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                    medida.DescripcionCorta = "Medida Nominal";
                    herramental.PropiedadesCadena.Add(medida);

                    PropiedadCadena width = new PropiedadCadena();
                    width.Valor= (string)tipo.GetProperty("Width").GetValue(item, null);
                    width.DescripcionCorta = "Width";
                    herramental.PropiedadesCadena.Add(width);

                    PropiedadCadena sobreM = new PropiedadCadena();
                    sobreM.Valor=(string)tipo.GetProperty("SobreMedida").GetValue(item, null);
                    sobreM.DescripcionCorta = "Sobre Medida";
                    herramental.PropiedadesCadena.Add(sobreM);

                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "GuidePlate");
        }

        /// <summary>
        /// Obtiene la información de un herramental de Guide Plate BK.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoGuidePlateBK(string codigo)
        {
            Herramental herramental = new Herramental();

            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioBk.GetInfoGuidePlate(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_GuidePlate").GetValue(item, null);

                    PropiedadCadena medida = new PropiedadCadena();
                    medida.Valor = (string)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                    medida.DescripcionCorta = "Medida Nominal";
                    herramental.PropiedadesCadena.Add(medida);

                    PropiedadCadena width = new PropiedadCadena();
                    width.Valor = (string)tipo.GetProperty("Width").GetValue(item, null);
                    width.DescripcionCorta = "Width";
                    herramental.PropiedadesCadena.Add(width);

                    PropiedadCadena sobreM = new PropiedadCadena();
                    sobreM.Valor = (string)tipo.GetProperty("SobreMedida").GetValue(item, null);
                    sobreM.DescripcionCorta = "Sobre Medida";
                    herramental.PropiedadesCadena.Add(sobreM);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que obtiene los herramentales óptimos para Guide Plate BK.
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="h1"></param>
        /// <returns></returns>
        public static DataTable GetGuidePlate(double d1, double h1)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            string medidaN, sobreMedida;
            //Obtenemos el width.
            string width = ServicioBk.GetWidthGuillotina(h1);

            //Obtenemos la medida nominal y sobremedida.
            IList informacionBD = ServicioBk.GetMedidaGuillotina(d1);

            //Si la informacion es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Asiganmos los valores.
                    medidaN = (string)tipo.GetProperty("MEDIDANOMINAL").GetValue(item, null);
                    sobreMedida = (string)tipo.GetProperty("SOBREMEDIDA").GetValue(item, null);

                    //Con los campos obtenidos, se obtiene el herramental.
                    IList ListaGuillotina = ServicioBk.GetGuidePlate(width, medidaN, sobreMedida);

                    //Si la lista es diferente de nulo.
                    if (ListaGuillotina != null)
                    {
                        //Iteramos la lista de herramnetal.
                        foreach (var itemH in ListaGuillotina)
                        {
                            System.Type tipo2 = itemH.GetType();

                            //Declaramos un objeto de tipo Herramental.
                            Herramental herramental = new Herramental();
                            //Convertimos la información a tipo Herramental.
                            herramental = ReadInformacionHerramentalEncontrado(ListaGuillotina);

                            PropiedadCadena medida = new PropiedadCadena();
                            medida.Valor = (string)tipo2.GetProperty("MedidaNominal").GetValue(itemH, null);
                            medida.DescripcionCorta = "Medida Nominal";
                            herramental.PropiedadesCadena.Add(medida);

                            PropiedadCadena Pwidth = new PropiedadCadena();
                            Pwidth.Valor = (string)tipo2.GetProperty("Width").GetValue(itemH, null);
                            Pwidth.DescripcionCorta = "Width";
                            herramental.PropiedadesCadena.Add(Pwidth);

                            PropiedadCadena sobreM = new PropiedadCadena();
                            sobreM.Valor = (string)tipo2.GetProperty("SobreMedida").GetValue(itemH, null);
                            sobreM.DescripcionCorta = "Sobre Medida";
                            herramental.PropiedadesCadena.Add(sobreM);

                            //Mapeamos el valor a DescipcionRuta.
                            herramental.DescripcionRuta = "GUIDE PLATE  " + Pwidth.Valor;

                            //Agregamos el objeto a la lista.
                            ListaResultante.Add(herramental);
                        }
                    }
                }
            }
            //Convertimos la lista resultante en dataTable.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "GuillotinaBK");
        }

        /// <summary>
        /// Método que guarda un registro en la tabla GuidePlate.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetGuidePlate(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Retorna el resultado.
            return ServicioBk.SetGuidePlate(obj.Codigo, obj.PropiedadesCadena[0].Valor, obj.PropiedadesCadena[1].Valor, obj.PropiedadesCadena[2].Valor);
        }

        /// <summary>
        /// Método que modifica un registro de Guide Plate.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateGuidePlate(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Retorna el resultado.
            return ServicioBk.UpdateGuidePlate(obj.idHerramental,obj.Codigo, obj.PropiedadesCadena[0].Valor, obj.PropiedadesCadena[1].Valor, obj.PropiedadesCadena[2].Valor);
        }

        /// <summary>
        ///  Método que elimina un registro de la tabla Guide Plate BK
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteGuidePlate(int id)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Ejecutamos el método y retornamos el resultado.
            return ServicioBk.DeleteGuidePlate(id);
        }

        //Guillotina BK
        /// <summary>
        /// Método que obtiene los registros de GuidePlate de acuerdo al texto de búsqueda.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllGuillotinaBK(string texto)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioBk.GetAllGuillotinaBK(texto);

            //Si la información es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    PropiedadCadena medida = new PropiedadCadena();
                    medida.Valor = (string)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                    medida.DescripcionCorta = "Medida Nominal";
                    herramental.PropiedadesCadena.Add(medida);

                    PropiedadCadena width = new PropiedadCadena();
                    width.Valor = (string)tipo.GetProperty("Width").GetValue(item, null);
                    width.DescripcionCorta = "Width";
                    herramental.PropiedadesCadena.Add(width);

                    PropiedadCadena sobreM = new PropiedadCadena();
                    sobreM.Valor = (string)tipo.GetProperty("SobreMedida").GetValue(item, null);
                    sobreM.DescripcionCorta = "Sobre Medida";
                    herramental.PropiedadesCadena.Add(sobreM);

                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "GuillotinaBK");
        }

        /// <summary>
        /// Método que obtiene un herramnetal de GuillotinaBK respecto al diametro y width del anullo.
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="h1"></param>
        public static DataTable GetGuillotina(double d1, double h1)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            string medidaN, sobreMedida;
            //Obtenemos el width
            string width = ServicioBk.GetWidthGuillotina(h1);

            //Obtenemos la medida nominal y sobremedida
            IList informacionBD = ServicioBk.GetMedidaGuillotina(d1);

            //Si la informacion es diferente de nulo
            if (informacionBD!=null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Asiganmos los valores.
                    medidaN= (string)tipo.GetProperty("MEDIDANOMINAL").GetValue(item, null);
                    sobreMedida= (string)tipo.GetProperty("SOBREMEDIDA").GetValue(item, null);

                    //Con los campos obtenidos, se obtiene el herramental.
                    IList ListaGuillotina = ServicioBk.GetGuillotinaBK(width, medidaN, sobreMedida);

                    //Si la lista es diferente de nulo.
                    if (ListaGuillotina !=null)
                    {
                        //Iteramos la lista de herramnetal.
                        foreach (var itemH in ListaGuillotina)
                        {
                            System.Type tipo2 = itemH.GetType();

                            //Declaramos un objeto de tipo Herramental.
                            Herramental herramental = new Herramental();

                            //Convertimos la información a tipo Herramental.
                            herramental = ReadInformacionHerramentalEncontrado(ListaGuillotina);

                            PropiedadCadena medida = new PropiedadCadena();
                            medida.Valor = (string)tipo2.GetProperty("MedidaNominal").GetValue(itemH, null);
                            medida.DescripcionCorta = "Medida Nominal";
                            herramental.PropiedadesCadena.Add(medida);

                            PropiedadCadena Pwidth = new PropiedadCadena();
                            Pwidth.Valor = (string)tipo2.GetProperty("Width").GetValue(itemH, null);
                            Pwidth.DescripcionCorta = "Width";
                            herramental.PropiedadesCadena.Add(Pwidth);

                            PropiedadCadena sobreM = new PropiedadCadena();
                            sobreM.Valor = (string)tipo2.GetProperty("SobreMedida").GetValue(itemH, null);
                            sobreM.DescripcionCorta = "Sobre Medida";
                            herramental.PropiedadesCadena.Add(sobreM);

                            //Mapeamos el valor a DescipcionRuta.
                            herramental.DescripcionRuta = "GUILLOTINA BK  " + Pwidth.Valor;

                            //Agregamos el objeto a la lista.
                            ListaResultante.Add(herramental);
                        }
                    }
                }
            }
            //Convertimos la lista resultante en dataTable.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "GuillotinaBK");
        }

        /// <summary>
        /// Obtiene la información de un herramental de Guide Plate BK.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoGuillotinaBK(string codigo)
        {
            Herramental herramental = new Herramental();
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioBk.GetInfoGuillotina(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_GuillotinaBK").GetValue(item, null);

                    PropiedadCadena medida = new PropiedadCadena();
                    medida.Valor = (string)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                    medida.DescripcionCorta = "Medida Nominal";
                    herramental.PropiedadesCadena.Add(medida);

                    PropiedadCadena width = new PropiedadCadena();
                    width.Valor = (string)tipo.GetProperty("Width").GetValue(item, null);
                    width.DescripcionCorta = "Width";
                    herramental.PropiedadesCadena.Add(width);

                    PropiedadCadena sobreM = new PropiedadCadena();
                    sobreM.Valor = (string)tipo.GetProperty("SobreMedida").GetValue(item, null);
                    sobreM.DescripcionCorta = "Sobre Medida";
                    herramental.PropiedadesCadena.Add(sobreM);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que guarda un registro en la tabla GuidePlate.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetGuillotinaBK(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Retorna el resultado.
            return ServicioBk.SetGuillotinaBK(obj.Codigo, obj.PropiedadesCadena[0].Valor, obj.PropiedadesCadena[1].Valor, obj.PropiedadesCadena[2].Valor);
        }

        /// <summary>
        /// Método que modifica un registro de Guillotina BK.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateGuillotinaBK(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Retorna el resultado.
            return ServicioBk.UpdateGuillotinaBK(obj.idHerramental,obj.Codigo, obj.PropiedadesCadena[0].Valor, obj.PropiedadesCadena[1].Valor, obj.PropiedadesCadena[2].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de Guillotina BK.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteGuillotinaBK(int id)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Ejecutamos la función
            return ServicioBk.DeleteGuillotinaBK(id);
        }

        /// <summary>
        /// Método que obtiene los mejpres herramnetales de GuidePlate y Guillotina BK.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBestBK(DataTable dt )
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Medida Nominal");
            DataR.Columns.Add("Width");
            DataR.Columns.Add("Sobre Medida");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["DESCRIPTION"].ToString();
                dr["Medida Nominal"] = row["Medida Nominal"].ToString();
                dr["Width"] = row["Width"].ToString();
                dr["Sobre Medida"] = row["Sobre Medida"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            return DataR;
        }

        //CamBK
        /// <summary>
        /// Método que obtiene todos los registros de Cam BK.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllCamBK(string texto)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioBk.GetAllCamBK(texto);

            //Si la información es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Asignamos los valores.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    Propiedad dimA = new Propiedad();
                    dimA.Valor = (double)tipo.GetProperty("A").GetValue(item, null);
                    dimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(dimA);

                    Propiedad dimB = new Propiedad();
                    dimB.Valor = (double)tipo.GetProperty("B").GetValue(item, null);
                    dimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(dimB);
                    //Agregamos el objeto a la lista.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "CamBK");
        }

        /// <summary>
        /// Método que obtiene la información de un herramental Cam Bk. 
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoCamBK(string codigo)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();
            //Declaramos un objeto de tipo Herramental.
            Herramental herramental = new Herramental();

            //Ejecutamos el método que busca los herramental. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioBk.GetInfoCamBK(codigo);

            //Si la información es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();                  

                    //Asignamos los valores.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental=(int)tipo.GetProperty("Id_CamBK").GetValue(item, null);

                    Propiedad dimA = new Propiedad();
                    dimA.Valor = (double)tipo.GetProperty("A").GetValue(item, null);
                    dimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(dimA);

                    Propiedad dimB = new Propiedad();
                    dimB.Valor = (double)tipo.GetProperty("B").GetValue(item, null);
                    dimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(dimB);

                    PropiedadCadena detalle = new PropiedadCadena();
                    detalle.Valor= (string)tipo.GetProperty("Detalle").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(detalle);

                }
            }
            //Retornamos el objeto.
            return herramental;
        }

        /// <summary>
        /// Método que guarda un registro de CamBK.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetCamBK(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();
            //Ejecutamos el método y retornamos el resultado.
            return ServicioBk.SetCamBK(obj.Codigo, obj.PropiedadesCadena[0].Valor, obj.Propiedades[0].Valor, obj.Propiedades[1].Valor);
        }

        /// <summary>
        /// Método que actualiza un herramental de Cam BK.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateCamBK(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Ejecutamos el método y retornamos el resultado.
            return ServicioBk.UpdateCamBK(obj.idHerramental,obj.Codigo, obj.PropiedadesCadena[0].Valor, obj.Propiedades[0].Valor, obj.Propiedades[1].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de herramental Cam BK.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteCamBK(int id)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();
            //Ejecutamos el método y retornamos el resultado.
            return ServicioBk.DeleteCamBK(id);
        }

        /// <summary>
        /// Métodoq ue obtiene toda la información de shield Bk.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllShieldBK(string texto)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioBk.GetAllShieldBK(texto);

            //Si la información es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Asignamos los valores.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    Propiedad fMin = new Propiedad();
                    fMin.Valor = (double)tipo.GetProperty("FractionalMin").GetValue(item, null);
                    fMin.DescripcionCorta = "Fractional Min";
                    herramental.Propiedades.Add(fMin);

                    Propiedad fMax = new Propiedad();
                    fMax.Valor = (double)tipo.GetProperty("FractionalMax").GetValue(item, null);
                    fMax.DescripcionCorta = "Fractional Max";
                    herramental.Propiedades.Add(fMax);

                    PropiedadCadena fraccMin = new PropiedadCadena();
                    fraccMin.Valor= (string)tipo.GetProperty("FracMin").GetValue(item, null);
                    fraccMin.DescripcionCorta = "Fracc Min";
                    herramental.PropiedadesCadena.Add(fraccMin);

                    PropiedadCadena fraccMax = new PropiedadCadena();
                    fraccMax.Valor = (string)tipo.GetProperty("FracMax").GetValue(item, null);
                    fraccMax.DescripcionCorta = "Fracc Max";
                    herramental.PropiedadesCadena.Add(fraccMax);
                    //Agregamos el objeyto a la lista.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "ShieldBK");
        }

        /// <summary>
        /// Método que obtiene los herramentales óptimos para Shield BK.
        /// </summary>
        /// <param name="dim"></param>
        /// <returns></returns>
        public static DataTable GetShieldBK(double dim)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioBk.GetShieldBK(dim);

            //Si la información es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    Propiedad fMin = new Propiedad();
                    fMin.Valor = (double)tipo.GetProperty("FractionalMin").GetValue(item, null);
                    fMin.DescripcionCorta = "Fractional Min";
                    herramental.Propiedades.Add(fMin);

                    Propiedad fMax = new Propiedad();
                    fMax.Valor = (double)tipo.GetProperty("FractionalMax").GetValue(item, null);
                    fMax.DescripcionCorta = "Fractional Max";
                    herramental.Propiedades.Add(fMax);

                    PropiedadCadena fraccMin = new PropiedadCadena();
                    fraccMin.Valor = (string)tipo.GetProperty("FracMin").GetValue(item, null);
                    fraccMin.DescripcionCorta = "Fracc Min";
                    herramental.PropiedadesCadena.Add(fraccMin);

                    PropiedadCadena fraccMax = new PropiedadCadena();
                    fraccMax.Valor = (string)tipo.GetProperty("FracMax").GetValue(item, null);
                    fraccMax.DescripcionCorta = "Fracc Max";
                    herramental.PropiedadesCadena.Add(fraccMax);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "SHIELD BK MIN " + fMin.Valor +" MAX "+ fMax.Valor;
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "ShieldBK");
        }

        /// <summary>
        /// Método que obtiene la información de un herramental Shield BK.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoShielBK(string codigo)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();

            //Declaramos un objeto de tipo Herramental.
            Herramental herramental = new Herramental();

            //Ejecutamos el método que busca los herramental. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioBk.GetInfoShieldBK(codigo);

            //Si la información es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_ShieldBK").GetValue(item, null);

                    Propiedad fMin = new Propiedad();
                    fMin.Valor = (double)tipo.GetProperty("FractionalMin").GetValue(item, null);
                    fMin.DescripcionCorta = "Fractional Min";
                    herramental.Propiedades.Add(fMin);

                    Propiedad fMax = new Propiedad();
                    fMax.Valor = (double)tipo.GetProperty("FractionalMax").GetValue(item, null);
                    fMax.DescripcionCorta = "Fractional Max";
                    herramental.Propiedades.Add(fMax);

                    PropiedadCadena fraccMin = new PropiedadCadena();
                    fraccMin.Valor = (string)tipo.GetProperty("FracMin").GetValue(item, null);
                    fraccMin.DescripcionCorta = "Fracc Min";
                    herramental.PropiedadesCadena.Add(fraccMin);

                    PropiedadCadena fraccMax = new PropiedadCadena();
                    fraccMax.Valor = (string)tipo.GetProperty("FracMax").GetValue(item, null);
                    fraccMax.DescripcionCorta = "Fracc Max";
                    herramental.PropiedadesCadena.Add(fraccMax);

                    PropiedadCadena detalle = new PropiedadCadena();
                    detalle.Valor = (string)tipo.GetProperty("Detalle").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(detalle);

                }
            }
            //Retornamos el objeto.
            return herramental;
        }

        /// <summary>
        /// Método que guarda un registro de ShieldBK.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetShieldBK(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();
            //Ejecutamos el métood y retornamos el resultado
            return ServicioBk.SetShieldBK(obj.Codigo, obj.PropiedadesCadena[0].Valor, obj.Propiedades[0].Valor, obj.Propiedades[0].Valor, obj.PropiedadesCadena[1].Valor, obj.PropiedadesCadena[2].Valor);
        }

        /// <summary>
        /// Método que actualiza un herramental de Shield BK.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateShieldBK(Herramental obj)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();
            //Ejecutamos el métood y retornamos el resultado
            return ServicioBk.UpdateShieldBK(obj.idHerramental,obj.Codigo, obj.PropiedadesCadena[0].Valor, obj.Propiedades[0].Valor, obj.Propiedades[0].Valor, obj.PropiedadesCadena[1].Valor, obj.PropiedadesCadena[2].Valor);
        }

        /// <summary>
        /// Método que elimina un herramental de Shield BK.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteShieldBK(int id)
        {
            //Inicializamos los servicios de BK.
            SO_BK ServicioBk = new SO_BK();
            //Ejecutamos el métood y retornamos el resultado
            return ServicioBk.DeleteShieldBK(id);
        }

        /// <summary>
        /// Obtiene el mejor herramental para shield BK.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBestShieldBK(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Fractional Min");
            DataR.Columns.Add("Fractional Max");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Fractional Min"] = row["Fractional Min"].ToString();
                dr["Fractional Max"] = row["Fractional Max"].ToString();
                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            return DataR;
        }
        #endregion

        #region BatesBore

        /// <summary>
        /// Obtiene todos los registros de Bushing, o las coincidencias con el texto de búsqueda.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllBushingBB(string texto)
        {
            //Inicializamos los servicios de CamTurn.
            SO_BatesBore ServiceBatesBore = new SO_BatesBore();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceBatesBore.GetAllBushing(texto);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadMedidaN = new Propiedad();
                    propiedadMedidaN.Unidad = "";
                    propiedadMedidaN.Valor = (double)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                    propiedadMedidaN.DescripcionCorta = "Medida Nominal";
                    herramental.Propiedades.Add(propiedadMedidaN);

                    PropiedadCadena propiedadDimB = new PropiedadCadena();
                    propiedadDimB.DescripcionCorta = "Dim B";
                    propiedadDimB.Valor = (string)tipo.GetProperty("DimB").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propiedadDimB);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "BushingBB");
        }

        /// <summary>
        /// Método obtiene los herramentales óptimos para Bushing Bates Bore.
        /// </summary>
        /// <param name="diaBO"></param>
        /// <returns></returns>
        public static DataTable GetBushing(double diaBO)
        {
            //Inicializamos los servicios de CamTurn.
            SO_BatesBore ServiceBatesBore = new SO_BatesBore();

            double cri_min = diaBO + GetCriterio("BoreBushingMin");
            double cri_max = diaBO + GetCriterio("BoreBushingMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceBatesBore.GetBushingBB(cri_min, cri_max);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    Propiedad propiedadMedidaN = new Propiedad();
                    propiedadMedidaN.Unidad = "";
                    propiedadMedidaN.Valor = (double)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                    propiedadMedidaN.DescripcionCorta = "Medida Nominal";
                    herramental.Propiedades.Add(propiedadMedidaN);

                    PropiedadCadena propiedadDimB = new PropiedadCadena();
                    propiedadDimB.DescripcionCorta = "Dim B";
                    propiedadDimB.Valor = (string)tipo.GetProperty("DimB").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propiedadDimB);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "BUSHING BATES BORE " + propiedadDimB.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "BushingBB");
        }

        /// <summary>
        /// Obtiene la información de un herramental de Bushing Bates Bore.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoBushing_BatesBore(string codigo)
        {
            Herramental herramental = new Herramental();

            //Inicializamos los servicios de CamTurn.
            SO_BatesBore ServiceBatesBore = new SO_BatesBore();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceBatesBore.GetInfoBushing(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_Bushing").GetValue(item, null);
                    herramental.Plano= (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadMedidaN = new Propiedad();
                    propiedadMedidaN.Unidad = "";
                    propiedadMedidaN.Valor = (double)tipo.GetProperty("MedidaNominal").GetValue(item, null);
                    propiedadMedidaN.DescripcionCorta = "Medida Nominal";
                    herramental.Propiedades.Add(propiedadMedidaN);

                    PropiedadCadena propiedadDimB = new PropiedadCadena();
                    propiedadDimB.DescripcionCorta = "Dim B";
                    propiedadDimB.Valor = (string)tipo.GetProperty("DimB").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propiedadDimB);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que obtiene el mejor herramental.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBestBushing(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Medida Nominal");
            DataR.Columns.Add("Dim B");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Medida Nominal"] = row["Medida Nominal"].ToString();
                dr["Dim B"] = row["Dim B"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            return DataR;
        }

        /// <summary>
        /// Método para guardar un registro.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetBushing(Herramental obj)
        {
            //Inicializamos los servicios de CamTurn.
            SO_BatesBore ServiceBatesBore = new SO_BatesBore();

            //Ejecutamos el método y regresa el resultado.
            return ServiceBatesBore.SetBushing(obj.Codigo, obj.Plano, obj.Propiedades[0].Valor, obj.PropiedadesCadena[0].Valor);
        }

        /// <summary>
        /// Método que actualiza un registro de Bushing Bates Bore.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateBushingBB(Herramental obj)
        {
            //Inicializamos los servicios de CamTurn.
            SO_BatesBore ServiceBatesBore = new SO_BatesBore();

            //Ejecutamos el método y regresa el resultado.
            return ServiceBatesBore.UpdateBushing(obj.idHerramental,obj.Codigo, obj.Plano, obj.Propiedades[0].Valor, obj.PropiedadesCadena[0].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de Bushing Bates Bore.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteBushingBB(int id)
        {
            // Inicializamos los servicios de CamTurn.
            SO_BatesBore ServiceBatesBore = new SO_BatesBore();

            //Ejecutamos el método y regresa el resultado.
            return ServiceBatesBore.DeleteBushing(id);
        }
        #endregion

        #region FinishMill

        /// <summary>
        /// Método que obtiene todos los registros de Bushing Finish Mill.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllBushingFM(string texto)
        {
            //Inicializamos los servicios de Finish Mill.
            SO_FinishMill ServiceFinishMill = new SO_FinishMill();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceFinishMill.GetAllBushingFM(texto);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);


                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim C";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimC").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "BushingFinishMill");
        }

        /// <summary>
        /// Método que obtiene los herramentales óptimos para Finish Mill.
        /// </summary>
        /// <param name="diaBO"></param>
        /// <returns></returns>
        public static DataTable GetBushingFM(double diaBO)
        {
            //Inicializamos los servicios de CamTurn.
            SO_FinishMill ServiceFinishMill = new SO_FinishMill();

            double cri_min = diaBO + GetCriterio("FinMillBushignMin");
            double cri_max = diaBO + GetCriterio("FinMillBushignMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceFinishMill.GetBushingFM(cri_min, cri_max);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();
                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.DescripcionCorta = "Dim C";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DimC").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimC);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "BUSHING FINISH MILL DIM C " + propiedadDimC.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "BushingFinishMill");
        }

        /// <summary>
        /// Obtiene la información de un herramental de Bushing Finish Mill.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoBushing_FinishMill(string codigo)
        {
            Herramental herramental = new Herramental();
            //Inicializamos los servicios de CamTurn.
            SO_FinishMill ServiceFinishMill = new SO_FinishMill();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceFinishMill.GetInfoBushingFM(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_BushingFM").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim C";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimC").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);
                }
            }
            //Retornamos el objeto.
            return herramental;
        }

        /// <summary>
        /// Obtiene el mejor herramental para Bushing Finish Mill.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBest_BushingFinishMill(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Dim C");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Dim C"] = row["Dim C"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            return DataR;
        }

        /// <summary>
        /// Método que guarda un registro de Bushing Finish Mill.
        /// </summary>
        /// <param name="herramental"></param>
        /// <returns></returns>
        public static int SetBushingFM(Herramental herramental)
        {
            //Inicializamos los servicios de Finish Mill.
            SO_FinishMill ServiceFinishMill = new SO_FinishMill();

            //Ejecutamos el método t retornamos el resultado.
            return ServiceFinishMill.SetBushingFM(herramental.Codigo, herramental.Plano, herramental.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que actualiza un registro de la tabla Bushing Finish Mill.
        /// </summary>
        /// <param name="herramental"></param>
        /// <returns></returns>
         public static int UpdateBushingFM(Herramental herramental)
        {
            //Inicializamos los servicios de Finish Mill.
            SO_FinishMill ServiceFinishMill = new SO_FinishMill();

            //Ejecutamos el método t retornamos el resultado.
            return ServiceFinishMill.UpdateBushingFM(herramental.idHerramental,herramental.Codigo, herramental.Plano, herramental.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de la tabla Bushing Finish Mill.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteBushingFM(int id)
        {
            //Inicializamos los servicios de Finish Mill.
            SO_FinishMill ServiceFinishMill = new SO_FinishMill();

            //Ejecutamos el método t retornamos el resultado.
            return ServiceFinishMill.DeleteBushingFM(id);
        }
        #endregion

        #region Cromo
        /// <summary>
        /// Método que obtiene todos los registros de Bushing Cromo.
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllBushingCromo(string texto)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCromo.GetAllBushingCromo(texto);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDimD = new Propiedad();
                    propiedadDimD.DescripcionCorta = "Dim D";
                    propiedadDimD.Valor = (double)tipo.GetProperty("DimD").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimD);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "BushingCromo");
        }

        /// <summary>
        /// Método que obtiene los herramentales óptimos de Bushing Cromo.
        /// </summary>
        /// <param name="diamBO"></param>
        /// <returns></returns>
        public static DataTable GetBushingCromo(double diamBO)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            double dimMin = diamBO - GetCriterio("CromoBushingMin");
            double dimMax = diamBO + GetCriterio("CromoBushingMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCromo.GetBushingCromo(dimMin, dimMax);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    Propiedad propiedadDimD = new Propiedad();
                    propiedadDimD.DescripcionCorta = "Dim D";
                    propiedadDimD.Valor = (double)tipo.GetProperty("DimD").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimD);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "Bushing Cromo   " + propiedadDimD.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "BushingCromo");
        }

        /// <summary>
        /// Método que obtiene la información de un  herramental  Bushing Cromo.
        /// </summary>
        /// <param name="codigoHerramental"></param>
        /// <returns></returns>
        public static Herramental GetInfoBushingCromo(string codigoHerramental)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            //Declaramos un objeto de tipo Herramental.
            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCromo.GetInfoBushingCromo(codigoHerramental);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_BushingCromo").GetValue(item, null);

                    Propiedad propiedadDimD = new Propiedad();
                    propiedadDimD.DescripcionCorta = "Dim D";
                    propiedadDimD.Valor = (double)tipo.GetProperty("DimD").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimD);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el objeto.
            return herramental;
        }

        /// <summary>
        /// Método que obtiene el mejor herramental para  Bushing Cromo.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBest_BushingCromo(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Dim D");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Dim D"] = row["Dim D"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            return DataR;
        }

        /// <summary>
        /// Método que guarda un regsitro de BushingCromo.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetBushingCromo(Herramental obj)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            //Ejecutamos el método y retornamos el resultado.
            return ServiceCromo.SetBushingCromo(obj.Codigo, obj.Propiedades[0].Valor, obj.Plano);
        }

        /// <summary>
        /// Método que modifica un registro de la tabla Busging Cromo.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateBushingCromo(Herramental obj)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            //Ejecutamos el método y retornamos el resultado.
            return ServiceCromo.UpdateBushingCromo(obj.idHerramental,obj.Codigo, obj.Propiedades[0].Valor, obj.Plano);
        }

        /// <summary>
        /// Método que elimina un registro de la tabla Busging Cromo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteBusgingCromo(int id)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            //Ejecutamos el método y retornamos el resultado.
            return ServiceCromo.DeleteBushingCromo(id);
        }

        /* Collar
         *  * 
         * */

            /// <summary>
            /// Método que obtiene todos los registros de Collar Cromo.
            /// </summary>
            /// <param name="texto"></param>
            /// <returns></returns>
        public static DataTable GetAllCollarCromo(string texto)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCromo.GetAllCollarsCromo(texto);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDimD = new Propiedad();
                    propiedadDimD.DescripcionCorta = "Dim A";
                    propiedadDimD.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimD);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "CollarCromo");
        }

        /// <summary>
        /// Método que obtiene la información de un herramental Collar Cromo.
        /// </summary>
        /// <param name="codigoHerramental"></param>
        /// <returns></returns>
        public static Herramental GetInfoCollarCromo(string codigoHerramental)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCromo.GetInfoCollarsCromo(codigoHerramental);

            //Declaramos un objeto de tipo Herramental.
            Herramental herramental = new Herramental();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();                 

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_Collar").GetValue(item, null);

                    Propiedad propiedadDimD = new Propiedad();
                    propiedadDimD.DescripcionCorta = "Dim A";
                    propiedadDimD.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimD);                   
                }
            }
            //Regresamos el objeto.
            return herramental;
        }

        /// <summary>
        /// Método que obtiene el mejor herramental Collar Cromo.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBest_CollarCromo(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Dim A");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Dim A"] = row["Dim A"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            //Retornamos la tabla resultante.
            return DataR;
        }

        /// <summary>
        /// Mpétodo que obtiene los herramentales óptimos de acuerdo al diámetro.
        /// </summary>
        /// <param name="diamBush"></param>
        /// <returns></returns>
        public static DataTable GetCollarCromo(double diamBush)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            double dimMin = diamBush - GetCriterio("CromoCollarMin");
            double dimMax = diamBush + GetCriterio("CromoCollarMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceCromo.GetCollarsCromo(dimMin, dimMax);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.DescripcionCorta = "Dim A";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimA);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "COLLARS CROMO  " + propiedadDimA.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "CollarCromo");
        }

        /// <summary>
        /// Método que guarda un registro a la tabla CollarsCromo.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetCollarCromo(Herramental obj)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            //Ejecutamos el método y retornamos el resultado.
            return ServiceCromo.SetCollarsCromo(obj.Codigo, obj.Propiedades[0].Valor, obj.Plano);
        }

        /// <summary>
        /// Método que modifica un registro de CollarsCromo.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateCollarCromo(Herramental obj)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            //Ejecutamos el método y retornamos el resultado.
            return ServiceCromo.UpdateCollarsCromo(obj.idHerramental,obj.Codigo, obj.Propiedades[0].Valor, obj.Plano);
        }

        /// <summary>
        /// Métdo que elimina un registro de CollarsCromo.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteCollarCromo(int id)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Cromo ServiceCromo = new SO_Cromo();

            //Ejecutamos el método y retornamos el resultado.
            return ServiceCromo.DeleteCollarsCromo(id);
        }

        #endregion

        #region Sim
        /// <summary>
        /// Método que obtiene todos los registros de Bushing Sim.
        /// </summary>
        /// <param name="textoBusqueda"></param>
        /// <returns></returns>
        public static DataTable GetAllBushingSim(string textoBusqueda)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Sim ServiceSim = new SO_Sim();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceSim.GetAllBushingSim(textoBusqueda);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    
                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim B";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimB").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "BushingSim");
        }

        /// <summary>
        /// Método que obtiene la información de un herramental Bushing Sim.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoBushingSim(string codigo)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Sim ServiceSim = new SO_Sim();

            //Declaramos un objeto de tipo Herramental.
            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceSim.GetInfoBushingSim(codigo);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental=(int)tipo.GetProperty("Id_Bushing").GetValue(item, null);

                    //Agreamos las propiedades.
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim B";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimB").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    PropiedadCadena notas = new PropiedadCadena();
                    notas.DescripcionCorta = "Notas";
                    notas.Valor= (string )tipo.GetProperty("Notas").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(notas);
                }
            }
           //Retorna el objeto.
            return herramental;
        }

        /// <summary>
        /// Método que obtiene los herramentales óptimos deBushing Sim de acuerdo con el Diametro del anillo.
        /// </summary>
        /// <param name="d1"></param>
        /// <returns></returns>
        public static DataTable GetBushingSim(double d1)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();

            double cri_min = d1 + GetCriterio("SIMBushingMin");
            double cri_max = d1 + GetCriterio("SIMBushingMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceSim.GetBushingSim(cri_min, cri_max);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim B";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimB").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "Bushing Sim  " + propiedadDimB.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "BushingSim");
        }

        /// <summary>
        /// Método que obtiene el mejor herramental para Bushing Sim.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBest_BushingSim(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Dim B");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Dim B"] = row["Dim B"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            //Retorna la tabla resultante.
            return DataR;
        }

        /// <summary>
        /// Método que guarda un registro de la tabla Bushing Sim.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="dimB"></param>
        /// <param name="notas"></param>
        /// <returns></returns>
        public static int SetBushingSim(Herramental obj)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();
            //Ejecutamos el métdo y retorna el resultado.
            return ServiceSim.SetBushingSim(obj.Codigo, obj.Propiedades[0].Valor, obj.PropiedadesCadena[0].Valor);
        }

        /// <summary>
        /// Método que modifica un registro de la tabla Bushing Sim.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateBushingSim(Herramental obj)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();
            //Ejecutamos el métdo y retorna el resultado.
            return ServiceSim.UpdateBushingSim(obj.idHerramental, obj.Propiedades[0].Valor, obj.PropiedadesCadena[0].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de la tabla Bushing Sim.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteBushingSim(int id)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();
            //Ejecutamos el métdo y retorna el resultado.
            return ServiceSim.DeleteBushingSim(id);
        }

        /// <summary>
        /// Método que obtiene todos los registros de Pusher Sim.
        /// </summary>
        /// <param name="textoBusqueda"></param>
        /// <returns></returns>
        public static DataTable GetAllPusherSim(string textoBusqueda)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Sim ServiceSim = new SO_Sim();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceSim.GetAllPusherSim(textoBusqueda);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    Propiedad propiedadDimD = new Propiedad();
                    propiedadDimD.DescripcionCorta = "Dim D";
                    propiedadDimD.Valor = (double)tipo.GetProperty("DimD").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimD);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "PusherSim");
        }

        /// <summary>
        /// Método que obtiene la ifnormación de un herramental Pusher sim.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoPusherSim(string codigo)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Sim ServiceSim = new SO_Sim();

            //Declaramos un objeto de tipo Herramental.
            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceSim.GetInfoPusher(codigo);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_Pushing").GetValue(item, null);

                    Propiedad propiedadDimD = new Propiedad();
                    propiedadDimD.DescripcionCorta = "Dim D";
                    propiedadDimD.Valor = (double)tipo.GetProperty("DimD").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimD);
                }
            }
           //Retornamos el objeto
            return herramental;
        }

        /// <summary>
        /// Método que obtiene los registros óptimos para Pusher Sim a partir del diametro Bushing.
        /// </summary>
        /// <param name="d1"></param>
        /// <returns></returns>
        public static DataTable GetPusherSim(double diamBush)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();

            double pusher_min = diamBush - GetCriterio("SIMPusherMin");
            double pusher_max = diamBush - GetCriterio("SIMPusherMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceSim.GetPusher(pusher_min, pusher_max);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    Propiedad propiedadDimD = new Propiedad();
                    propiedadDimD.DescripcionCorta = "Dim D";
                    propiedadDimD.Valor = (double)tipo.GetProperty("DimD").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimD);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "Pusher Sim" + propiedadDimD.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Pusher Sim");
        }

        /// <summary>
        /// Método que obtiene el mejor herramenatl Pusher Sim.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBest_PusherSim(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Dim D");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Dim D"] = row["Dim D"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            return DataR;
        }

        /// <summary>
        /// Método que guarda un registro de la tabla Pusher Sim.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetPusherSim(Herramental obj)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();
            //Ejecutamos el método y retornamos el resultado.
            return ServiceSim.SetPusher(obj.Codigo, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que modifica un registro de Bushing Sim.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdatePusherSim(Herramental obj)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();
            //Ejecutamos el método y retornamos el resultado.
            return ServiceSim.UpdatePusher(obj.idHerramental, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de la tabla Bushing Sim.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeletePusherSim(int id)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();
            //Ejecutamos el método y retornamos el resultado.
            return ServiceSim.DeletePusher(id);
        }

        /// <summary>
        /// Método que obtiene todos los registros de Guillotina Sim.
        /// </summary>
        /// <param name="textoBusqueda"></param>
        /// <returns></returns>
        public static DataTable GetAllGuillotinaSim(string textoBusqueda)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Sim ServiceSim = new SO_Sim();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceSim.GetAllGuillotinaSim(textoBusqueda);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    //Agregamos las propiedades.
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.DescripcionCorta = "Dim A";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadMin = new Propiedad();
                    propiedadMin.DescripcionCorta = "Width Min";
                    propiedadMin.Valor = (double)tipo.GetProperty("WidthMin").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadMin);

                    Propiedad propiedadMax = new Propiedad();
                    propiedadMax.DescripcionCorta = "Width Max";
                    propiedadMax.Valor = (double)tipo.GetProperty("WidthMax").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadMax);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "GuillotinaSim");
        }

        /// <summary>
        /// Método que obtiene la información de un herramental Guillotina sim.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoGuillotinaSim(string codigo)
        {
            //Inicializamos los servicios de Cromo OD.
            SO_Sim ServiceSim = new SO_Sim();

            //Declaramos un objeto de tipo Herramental.
            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceSim.GetInfoGuillotinaSim(codigo);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_Guillotina").GetValue(item, null);

                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.DescripcionCorta = "Dim A";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadMin = new Propiedad();
                    propiedadMin.DescripcionCorta = "Width Min";
                    propiedadMin.Valor = (double)tipo.GetProperty("WidthMin").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadMin);

                    Propiedad propiedadMax = new Propiedad();
                    propiedadMax.DescripcionCorta = "Width Max";
                    propiedadMax.Valor = (double)tipo.GetProperty("WidthMax").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadMax);

                    PropiedadCadena anillos = new PropiedadCadena();
                    anillos.Valor = Convert.ToString((int)tipo.GetProperty("CantidadAnillos").GetValue(item, null));
                    herramental.PropiedadesCadena.Add(anillos);
                }
            }
           //Retornamos el objeto.
            return herramental;
        }

        /// <summary>
        /// Método que obtiene los herramentales óptimos para Guillotina Sim.
        /// </summary>
        /// <param name="h1"></param>
        /// <returns></returns>
        public static DataTable GetGuillotinaSim(double h1)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceSim.GetguillotinaSim(h1);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.DescripcionCorta = "Dim A";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadMin = new Propiedad();
                    propiedadMin.DescripcionCorta = "Width Min";
                    propiedadMin.Valor = (double)tipo.GetProperty("WidthMin").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadMin);

                    Propiedad propiedadMax = new Propiedad();
                    propiedadMax.DescripcionCorta = "Width Max";
                    propiedadMax.Valor = (double)tipo.GetProperty("WidthMax").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadMax);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "Guillotina Sim " + propiedadDimA.Valor + " Width Min "+ propiedadMin.Valor + " Width Max "+ propiedadMax.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Guillotina Sim");
        }

        /// <summary>
        /// Método que obtiene el mejor herramental para Guillotina Sim.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBest_GuillotinaSim(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Dim A");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Dim A"] = row["Dim A"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            //Retornamos la tabla resultante.
            return DataR;
        }

        /// <summary>
        /// Método que guarda un registro en la tabla Guillotina Sim.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetGuillotinaSim(Herramental obj)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();
            //Ejecutamos el método, retornamos el resxultado.
            return ServiceSim.SetGuillotinaSim(obj.Codigo, obj.Propiedades[0].Valor, obj.Propiedades[1].Valor, obj.Propiedades[2].Valor, Convert.ToInt32(obj.PropiedadesCadena[0].Valor));
        }

        /// <summary>
        /// Método que modifica un registro en la tabla Guillotina Sim.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateGuillotinaSim(Herramental obj)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();
            //Ejecutamos el método, retornamos el resxultado.
            return ServiceSim.UpdateGuillotinaSim(obj.idHerramental, obj.Propiedades[0].Valor, obj.Propiedades[1].Valor, obj.Propiedades[2].Valor, Convert.ToInt32(obj.PropiedadesCadena[0].Valor));
        }

        /// <summary>
        /// Método que elimina un registro en la tabla Guillotina Sim.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteGuillotinaSim(int id)
        {
            //Inicializamos los servicios de Sim.
            SO_Sim ServiceSim = new SO_Sim();
            //Ejecutamos el método, retornamos el resxultado.
            return ServiceSim.DeleteGuillotinaSim(id);
        }
        #endregion

        #region Moly

        /// <summary>
        /// Método que obtiene todos los registros de Camisa Moly.
        /// </summary>
        /// <param name="textoBusqueda"></param>
        /// <returns></returns>
        public static DataTable GetAllCamisaMoly(string textoBusqueda)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetAllCamisaMoly(textoBusqueda);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "CamisaMoly");
        }

        /// <summary>
        /// Método que obtiene la información de un herramental Camisa Moly.
        /// </summary>
        /// <param name="textoBusqueda"></param>
        /// <returns></returns>
        public static Herramental GetInfoCamisaMoly(string codigo)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetInfoCamisaMoly(codigo);
       
            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_CamisaMoly").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                }
            }
            return herramental;
        }

        /// <summary>
        ///  Método que obtiene los herramentales óptimos para Camisa Moly a partir de diametro de operacion anterior.
        /// </summary>
        /// <param name="d1"></param>
        /// <returns></returns>
        public static DataTable GetCamisaMoly(double dBO)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //dbo=Diametro de operacion anterior.
            double cri_min = dBO + GetCriterio("MolyCamisaMin");
            double cri_max = dBO + GetCriterio("MolyCamisaMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetCamisaMoly(cri_min, cri_max);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    herramental.Plano= (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "Camisa Moly Dim A " + propiedadDimB.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Camisa_Moly");
        }


        /// <summary>
        /// Método que guarda un registro de Camisa Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetCamisaMoly(Herramental obj)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.SetCamisaMoly(obj.Codigo, obj.Plano, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que modifica un registro de Camisa Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateCamisaMoly(Herramental obj)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.UpdateCamisaMoly(obj.idHerramental, obj.Plano, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que elimna un registro de Camisa Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteCamisaMoly(int id)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.DeleteCamisaMoly(id);
        }

        /// <summary>
        /// Método que obtiene todos los registros de Collar Moly.
        /// </summary>
        /// <param name="textoBusqueda"></param>
        /// <returns></returns>
        public static DataTable GetAllCollarMoly(string textoBusqueda)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetAllCollarMoly(textoBusqueda);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "CollarMoly");
        }

        /// <summary>
        /// Método que obtiene los herramentales óptimos para Collar Moly a partir de la medida de camisa.
        /// </summary>
        /// <param name="dBO"></param>
        /// <returns></returns>
        public static DataTable GetCollarMoly(double D)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //D=Medida de camisa.

            double cri_min = D - GetCriterio("MolyCollarMin");
            double cri_max = D - GetCriterio("MolyCollarMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetCollarMoly(cri_min, cri_max);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "Collar Moly Dim A " + propiedadDimB.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Collar_Moly");
        }

        /// <summary>
        /// Método que obtiene la información de un herramental Collar Moly.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoCollarMoly(string codigo)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetInfoCollarMoly(codigo);

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_CollarMoly").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que guarda un registro de Collar Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetCollarMoly(Herramental obj)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.SetCollarMoly(obj.Codigo, obj.Plano, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que modifica un registro de Collar Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateCollarMoly(Herramental obj)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.UpdateCollarMoly(obj.idHerramental, obj.Plano, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de Collar Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteCollarMoly(int id)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.DeleteCollarMoly(id);
        }

        /// <summary>
        /// Método que obtiene todos los registros de Protector Inferior Moly.
        /// </summary>
        /// <param name="textoBusqueda"></param>
        /// <returns></returns>
        public static DataTable GetAllProtectorInfMoly(string textoBusqueda)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetAllProtectorInferior(textoBusqueda);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Protector_Inf_Moly");
        }

        /// <summary>
        /// Método que obtiene la información de un herramental Protector Inf Moly.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoProtectorInfMoly(string codigo)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetInfoProtectorInferior(codigo);

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_PIM").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);
                }
            }
            return herramental;
        }

        /// <summary>
        ///  Método que obtiene los herramentales óptimos para Protector Inferior Moly a partir de medida de collar.
        /// </summary>
        /// <param name="D"></param>
        /// <returns></returns>
        public static DataTable GetProtectorInfMoly(double D)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //D=Medida de collar.

            double cri_min = D + GetCriterio("MolyProteSupMin");
            double cri_max = D + GetCriterio("MolyProteSupMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetProtectorInferior(cri_min, cri_max);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "Protector Inf Moly Dim A " + propiedadDimB.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Protector_InfMoly");
        }


        /// <summary>
        /// Método que guarda un registro de Protector Inferior Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetProtectorInfMoly(Herramental obj)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.SetProtectoInferior(obj.Codigo, obj.Plano, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que modifica un registro de Protector Inferior Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateProtectorInfMoly(Herramental obj)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.UpdateProtectorInferior(obj.idHerramental, obj.Plano, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de Protector Inferior Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteProtectorInfMoly(int id)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.DeleteProtectorInferior(id);
        }

        /// <summary>
        /// Método que obtiene todos los registros de Protector Inferior Moly.
        /// </summary>
        /// <param name="textoBusqueda"></param>
        /// <returns></returns>
        public static DataTable GetAllProtectorSupMoly(string textoBusqueda)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetAllProtectorSuperior(textoBusqueda);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Protector_Sup_Moly");
        }

        /// <summary>
        /// Método que obtiene la información de un herramental PRotector Superior.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoProtectorSupMoly(string codigo)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            Herramental herramental = new Herramental();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetInfoProtectorSuperior(codigo);

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("Id_PSM").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);
                }
            }
            return herramental;
        }

        /// <summary>
        ///  Método que obtiene los herramentales óptimos para Protector Superior a partir de diametro de operacion anterior.
        /// </summary>
        /// <param name="D"></param>
        /// <returns></returns>
        public static DataTable GetProtectorSupMoly(double D)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //D=Medida de collar.

            double cri_min = D + GetCriterio("MolyProteSupMin");
            double cri_max = D + GetCriterio("MolyProteSupMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceMoly.GetProtectorSuperior(cri_min, cri_max);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    herramental.Plano = (string)tipo.GetProperty("Plano").GetValue(item, null);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim A";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimA").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "Protector Superior Moly Dim A " + propiedadDimB.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Protector_SupMoly");
        }


        /// <summary>
        /// Método que guarda un registro de Protector Superior Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetProtectorSupMoly(Herramental obj)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.SetProtectoSuperior(obj.Codigo, obj.Plano, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que modifica un registro de Protector Superior Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateProtectorSupMoly(Herramental obj)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.UpdateProtectorSuperior(obj.idHerramental, obj.Plano, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de Protector Superior Moly.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteProtectorSupMoly(int id)
        {
            //Inicializamos los servicios de Moly.
            SO_Moly ServiceMoly = new SO_Moly();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceMoly.DeleteProtectorSuperior(id);
        }

        /// <summary>
        /// Método que obtiene el mejor herramental para Moly.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBest_Moly(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Dim A");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Dim A"] = row["Dim A"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            //Retorna la tabla resultante.
            return DataR;
        }
        #endregion

        #region Scotchbrite

        /// <summary>
        /// Método que obtiene todos los registros de Collar Scotchbrite.
        /// </summary>
        /// <param name="textoBusqueda"></param>
        /// <returns></returns>
        public static DataTable GetAllCollarScotch(string textoBusqueda)
        {
            //Inicializamos los servicios de Scotchbrite.
            SO_Scotchbrite ServiceScotch = new SO_Scotchbrite();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceScotch.GetAllCollarS(textoBusqueda);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);

                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim F";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimF").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Collar Scotch");
        }

        /// <summary>
        /// método que obtiene la información de un herramental Collar Scothch a partir del código.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoCollarScotch(string codigo)
        {
            //Inicializamos los servicios de Scotchbrite.
            SO_Scotchbrite ServiceScotch = new SO_Scotchbrite();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceScotch.GetInfoCollarScotch(codigo);

            //Declaramos un objeto de tipo Herramental.
            Herramental herramental = new Herramental();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental=(int)tipo.GetProperty("Id").GetValue(item, null);
                    herramental.Plano = (string)tipo.GetProperty("plano").GetValue(item, null);

                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim F";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimF").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);                 
                }
            }
            //Retornamos el objeto.
            return herramental;
        }

       /// <summary>
       /// Método que obtiene los herramentales optimos a partir de díametro del anillo.
       /// </summary>
       /// <param name="D1"></param>
       /// <returns></returns>
        public static DataTable GetCollarScotchbrite(double D1)
        {
            //Inicializamos los servicios de Scotchbrite.
            SO_Scotchbrite ServiceScotch = new SO_Scotchbrite();

            double cri_min = D1 - GetCriterio("ScotchbriteCollarMin");
            double cri_max = D1 + GetCriterio("ScotchbriteCollarMax");

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServiceScotch.GetCollarScotch(cri_min, cri_max);

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Verificamos que la información obtenida sea diferente de nulo.
            if (informacionBD != null)
            {
                //Itermos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo Herramental.
                    Herramental herramental = new Herramental();

                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    //Agregamos las propiedades
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.DescripcionCorta = "Dim F";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DimF").GetValue(item, null);
                    herramental.Propiedades.Add(propiedadDimB);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Collar Scotch");
        }

        /// <summary>
        /// Método que guarda un registro de Collar Scotchbrite.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetCollarScotchBrite(Herramental obj)
        {
            //Inicializamos los servicios de Scotchbrite.
            SO_Scotchbrite ServiceScotch = new SO_Scotchbrite();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceScotch.SetCollarScotch(obj.Codigo, obj.Plano, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que modifica un registro de Collar Scotchbrite.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateCollarScotchBrite(Herramental obj)
        {
            //Inicializamos los servicios de Scotchbrite.
            SO_Scotchbrite ServiceScotch = new SO_Scotchbrite();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceScotch.UpdateCollarScotchbrite(obj.idHerramental, obj.Plano, obj.Propiedades[0].Valor);
        }

        /// <summary>
        /// Método que elimina un registro de Collar Scotchbrite.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static int DeleteCollarScotchBrite(int id)
        {
            //Inicializamos los servicios de Scotchbrite.
            SO_Scotchbrite ServiceScotch = new SO_Scotchbrite();

            //ejeuctamos el método y retornamos el resultado.
            return ServiceScotch.DeleteCollarScotch(id);
        }

        /// <summary>
        /// Méotod que obtiene el mejor herramental Collar ScoctchBrite.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBest_CollarScotch(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");
            DataR.Columns.Add("Dim F");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["Description"].ToString();
                dr["Dim F"] = row["Dim F"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            //Retorna la tabla resultante.
            return DataR;
        }
        #endregion
        #endregion

        #region Herramentales

        /// <summary>
        /// Método que obtiene todos los registros de la tabla ClasificacionHerramental.
        /// </summary>
        /// <returns>Lista observable con todos los registros, si se genera algún error retorna el objeto vacío.</returns>
        public static ObservableCollection<ClasificacionHerramental> GetClasificacionHerramental(string texto)
        {
            //Inicializamos los servicios de clasificación.
            SO_ClasificacionHerramental ServiceClasificacion = new SO_ClasificacionHerramental();

            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<ClasificacionHerramental> ListaResultante = new ObservableCollection<ClasificacionHerramental>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList InformacionBD = ServiceClasificacion.GetClasificacionHerramental(texto);

            //Comparamos que la información de la base de datos no sea nulo.
            if (InformacionBD != null)
            {
                //Iteramos la información recibida.
                foreach (var item in InformacionBD)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos on objeto de tipo ClasificacionHerramental que contendrá la información de un registro.
                    ClasificacionHerramental obj = new ClasificacionHerramental();

                    //Asignamos los valores correspondientes.
                    obj.CantidadUtilizar = (int)tipo.GetProperty("CantidadUtilizar").GetValue(item, null);
                    obj.Costo = (double)tipo.GetProperty("Costo").GetValue(item, null);
                    obj.Descripcion = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    obj.IdClasificacion = (int)tipo.GetProperty("idClasificacion").GetValue(item, null);
                    obj.UnidadMedida = (string)tipo.GetProperty("UnidadMedida").GetValue(item, null);
                    obj.VerificacionAnual = (bool)tipo.GetProperty("VerificacionAnual").GetValue(item, null);
                    obj.VidaUtil = (int)tipo.GetProperty("VidaUtil").GetValue(item, null);
                    obj.objetoXML = (string)tipo.GetProperty("ObjetoXML").GetValue(item, null);

                    //Obtenemos el valor de la columna ListaCotasRevisar y la asignamos a una cadena.
                    string cotasRevisar = (string)tipo.GetProperty("ListaCotasRevisar").GetValue(item, null);

                    //Convertimos la cadena a un vector separado por comas.
                    string[] vector = cotasRevisar.Split(',');

                    //Creamos un objeto de tipo ObservableCollection a partir de un vector, lo asignamos a la propiedad correspondiente.
                    obj.ListaCotasRevizar = new ObservableCollection<string>(vector);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(obj);
                }
            }

            //Retornamos la lista.
            return ListaResultante;
        }

       

        #region MaestroHerramentales
        /// <summary>
        /// Método que obtiene el maestro de herramentales a partir de un criterio de busqueda.
        /// </summary>
        /// <param name="busqueda"></param>
        /// <returns></returns>
        public static ObservableCollection<Herramental> GetMaestroHerramental(string busqueda)
        {
            //Inicializamos los servicios de SO_MaestroHerramental.
            SO_MaestroHerramental ServiceHerramental = new SO_MaestroHerramental();

            //Declaramos una lista la cual será la que retornemos en el método.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método para obtener la información.Si la variable que recibimos es igual a nulo enviamos una cadena vacía. El resultado lo guardamos en un DataSet.
            DataSet informacionBD = ServiceHerramental.GetMaestroHerramentales(busqueda == null ? string.Empty : busqueda);

            //Verificamos que el resultado sea diferente de nulo.
            if (informacionBD != null)
            {
                //Comparamos si la información obtenida contiene al menos una tabla y esa tabla contiene al menos un registro.
                if (informacionBD.Tables.Count > 0 && informacionBD.Tables[0].Rows.Count > 0)
                {
                    //Itermamos los registro de la tabla cero.
                    foreach (DataRow element in informacionBD.Tables[0].Rows)
                    {
                        //Declaramos un objeto de tipo Herramental.
                        Herramental herramental = new Herramental();

                        //Mapeamos los valores del elemento iterado a las propiedades correspondientes del objeto.
                        herramental.Codigo = Convert.ToString(element["Codigo"]);
                        herramental.DescripcionGeneral = Convert.ToString(element["Descripcion"]);
                        herramental.clasificacionHerramental.Descripcion = Convert.ToString(element["DescripcionClasificacion"]);
                        herramental.Plano = Convert.ToString(element["NO_PLANO"]);

                        //Agregamos el objeto a la lista resultante.
                        ListaResultante.Add(herramental);
                    }
                }
            }

            //Retornamos la lista.
            return ListaResultante;
        }

        /// <summary>
        /// Método para insertar un registro a la tabla MAESTROHERRAMENTALES
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SetMaestroHerramentales(MaestroHerramental obj)
        {
            //Inicializamos los servicios de SO_MaestroHerramental.
            SO_MaestroHerramental ServiceHerramental = new SO_MaestroHerramental();
            //Ejecutamos el metodo y retornamos el valor
            return ServiceHerramental.SetMaestroHerramentales(obj.descripcion, obj.fecha_creacion, obj.fecha_cambio, obj.usuario_creacion, obj.usuario_creacion, obj.activo, obj.id_clasificacion, obj.id_plano, obj.Codigo);
        }

        /// <summary>
        /// Método para modificar un registro de la tabla MAESTROHERRAMENTALES
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateMaestroHerramental(MaestroHerramental obj)
        {
            //Inicializamos los servicios de SO_MaestroHerramental.
            SO_MaestroHerramental ServiceHerramental = new SO_MaestroHerramental();
            //Ejecutamos el metodo y retornamos el valor
            return ServiceHerramental.UpdateMaestroHerramentales(obj.Codigo,obj.descripcion, obj.fecha_cambio, obj.usuario_cambio, obj.activo, obj.id_clasificacion, obj.id_plano);
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla MAESTROHERRAMENTALES
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteMaestroHerramental(MaestroHerramental obj)
        {
            //Inicializamos los servicios de SO_MaestroHerramental.
            SO_MaestroHerramental ServiceHerramental = new SO_MaestroHerramental();
            //Ejecutamos el metodo y retornamos el valor
            return ServiceHerramental.DeleteMaestroHerramentales(obj.Codigo);

        }

        /// <summary>
        /// Método que verifica si un código ya existe
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static string GetCodigoMaestro(string codigo)
        {
            //Inicializamos los servicios de SO_MaestroHerramental.
            SO_MaestroHerramental ServiceHerramental = new SO_MaestroHerramental();
            //Ejecutamos el metodo y retornamos el valor
            return ServiceHerramental.GetCodigoMaestro(codigo);
        }

        /// <summary>
        /// Método que obtiene todas las propiedades de un maestro herrametal
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static MaestroHerramental GetPropiedadesHerramental(string codigo)
        {
            //Inicializamos los servicios de SO_MaestroHerramental.
            SO_MaestroHerramental ServiceHerramental = new SO_MaestroHerramental();
            //Declaramos un objeto de tipo maestro herramental
            MaestroHerramental obj = new MaestroHerramental();
            //Ejecutamos el método para obtener la información de la base de datos.
            IList InformacionBD = ServiceHerramental.GetPropiedadesHerramental(codigo);

            //Si la lista no es nula
            if (InformacionBD !=null)
            {
                //iteramos la lista
                foreach (var item in InformacionBD)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();                 
                    //Mapeamos los valores
                    obj.id_clasificacion = (int)tipo.GetProperty("idClasificacionHerramental").GetValue(item, null);
                    obj.id_plano = 0; //(int)tipo.GetProperty("idPlano").GetValue(item, null);
                    obj.activo= (bool)tipo.GetProperty("Activo").GetValue(item, null);
                    obj.objetoXML=(string)tipo.GetProperty("ObjetoXML").GetValue(item, null);
                }
            }
            //Retornamos el objeto
            return obj;
        }
        #endregion

        /// <summary>
        /// Método que convierte una lista de tipo ObservableCollection a un DataSet
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="nameTable"></param>
        /// <returns></returns>
        public static DataTable ConverToObservableCollectionHerramental_DataSet(ObservableCollection<Herramental> lista,string nameTable)
        {
            //Declaramos un datatable que será el que retornemos en el método.
            DataTable dataTableResultante = new DataTable();

            //Asignamos las primeras dos colomnas.
            dataTableResultante.Columns.Add("Code");
            dataTableResultante.Columns.Add("Description");

            //Verificamos si la lista contiene al menos un registro.
            if (lista.Count > 0)
            {
                //Iteramos las propiedades del primer elemento, esto para saber cuantas columnas se tiene que crear.
                foreach (var item in lista[0].Propiedades)
                {
                    //Agregamos la columna al datatable.
                    dataTableResultante.Columns.Add(item.DescripcionCorta);
                }

                //Iteramos las propiedades cadena del primer elemento, esto para saber cuantas columnas se tiene que crear.
                foreach (var item in lista[0].PropiedadesCadena)
                {
                    //Agregamos la columna al datatable.
                    dataTableResultante.Columns.Add(item.DescripcionCorta);
                }

                //Iteramos la lista de herramentales.
                foreach (Herramental herramental in lista)
                {
                    DataRow dr = dataTableResultante.NewRow();
                    dr[0] = herramental.Codigo;
                    dr[1] = herramental.DescripcionGeneral;

                    int c = 2;
                    foreach (Propiedad propiedad in herramental.Propiedades)
                    {
                        dr[c] = propiedad.Valor;
                        c += 1;
                    }

                    c = 2 + herramental.Propiedades.Count;
                    foreach (PropiedadCadena propiedadCadena in herramental.PropiedadesCadena)
                    {
                        dr[c] = propiedadCadena.Valor;
                        c += 1;
                    }

                    dataTableResultante.Rows.Add(dr);
                }
            }

           

            return dataTableResultante;
        }

        /// <summary>
        /// Método que obtiene el criterio a partir de un nombre recibido en el parámetro.
        /// </summary>
        /// <param name="NombreCriterio"></param>
        /// <returns></returns>
        public static double GetCriterio(string NombreCriterio)
        {
            //Inicializamos los servicios de SO_CriteriosAnillos.
            SO_CriteriosAnillos ServicioCriterios = new SO_CriteriosAnillos();

            //Ejecutamos el método para obtener los criterios. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCriterios.GetCriterio(NombreCriterio);

            //Declaramos una variable la cual será la que retornemos en el método.
            double criterio = 0;

            //Verificamos si el objeto resultante de la consulta es diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Mapeamos el valor de la columna buscada en la variable local.
                    criterio = (double)tipo.GetProperty(NombreCriterio).GetValue(item, null);
                }
            }

            //Retornamos el criterio.
            return criterio;
        }

        #region Coil
        /// <summary>
        /// Método que inserta un registro en la tabla de tbl_coil_feed_roller
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetCOIL_FEED_ROLLER(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.SetCOIL_FEED_ROLLER(obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC, obj.dimD, obj.wire_width_min, obj.wire_width_max);
        }

        /// <summary>
        /// Método que modifica un registro en la tabla tbl_coil_feed_roller
        /// </summary>
        /// <param name="obj"></param>
        public static int UpdateCOIL_FEED_ROLLER(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.UpdateCOIL_FEED_ROLLER(obj.ID,obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC, obj.dimD, obj.wire_width_min, obj.wire_width_max);
        }

        /// <summary>
        /// Método para eliminar un registro de la tabls tbl_coil_feed_roller
        /// </summary>
        /// <param name="idH"></param>
        /// <returns></returns>
        public static int DeleteCOIL_FEED_ROLLER(int idH)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.DeleteCOIL_FEED_ROLLER(idH);
        }

        /// <summary>
        /// Obtiene las propiedades de un registro de la tabla COIL_FEED_ROLLER, de acuerdo al width
        /// </summary>
        /// <param name="widthAlambre">Milimetros</param>
        /// <returns></returns>
        public static DataTable GetCOIL_Feed_Roller(double widthAlambre)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetCOIL_FEED_ROLLER(widthAlambre);
            //Si la lista es diferente de nulo
            if (informacionBD  != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta  = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor= (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    Propiedad propiedadDimD = new Propiedad();
                    propiedadDimD.Unidad = "Milimeters (mm)";
                    propiedadDimD.Valor = (double)tipo.GetProperty("DIMD").GetValue(item, null);
                    propiedadDimD.DescripcionCorta = "Dim D";
                    herramental.Propiedades.Add(propiedadDimD);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "COIL FEED ROLLER ";

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);

                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Feed_Roller");
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla COIL_FEED_ROLLER de acuerdo al texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllCOIL_Feed_Roller(string texto)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir del texto de búsqueda. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetAllCOIL_FEED_ROLLER(texto);
            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    Propiedad propiedadDimD = new Propiedad();
                    propiedadDimD.Unidad = "Milimeters (mm)";
                    propiedadDimD.Valor = (double)tipo.GetProperty("DIMD").GetValue(item, null);
                    propiedadDimD.DescripcionCorta = "Dim D";
                    herramental.Propiedades.Add(propiedadDimD);

                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor= (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);

                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Feed_Roller");
        }

        /// <summary>
        /// Obtiene la información del herramental de Coil Feed Roller.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoCOIL_Feed_Roller(string codigo)
        {

            Herramental herramental = new Herramental();

            SO_COIL ServicioCoil = new SO_COIL();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioCoil.GetInfoCoil_Feed_roller(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();
                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental=(int)tipo.GetProperty("ID_COIL_FEED_ROLLER").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    Propiedad propiedadDimD = new Propiedad();
                    propiedadDimD.Unidad = "Milimeters (mm)";
                    propiedadDimD.Valor = (double)tipo.GetProperty("DIMD").GetValue(item, null);
                    propiedadDimD.DescripcionCorta = "Dim D";
                    herramental.Propiedades.Add(propiedadDimD);

                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);
                }
            }
            return herramental;
        }


        /// <summary>
        /// Método que inserta un registro a la tabla TBL_COIL_CENTER_GUIDE
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetCOIL_CENTER_GUIDE(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.SetCOIL_CENTER_GUIDE(obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC, obj.wire_width_min, obj.wire_width_max, obj.radial_wire_min, obj.radial_wire_max);
        }

        /// <summary>
        /// Método que modifica un registro de la tabla TBL_COIL_CENTER_GUIDE
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateCOIL_CENTER_GUIDE(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.UpdateCOIL_CENTER_GUIDE(obj.ID,obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC, obj.wire_width_min, obj.wire_width_max, obj.radial_wire_min, obj.radial_wire_max);
        }

        /// <summary>
        /// Método que elimina un registro de la tabla TBL_COIL_CENTER_GUIDE
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteCOIL_CENTER_GUIDE(int id)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.DeleteCOIL_CENTER_GUIDE(id);
        }

        /// <summary>
        /// Obtiene las propiedades de un registro de la tabla COIL_CENTER_GUIDE, de acuerdo al width y radial
        /// </summary>
        /// <param name="widthAlambre"></param>
        /// <param name="radial">Milímetros</param>
        /// <returns></returns>
        public static DataTable GetCOIL_CENTER_GUIDE(double widthAlambre,double radial)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetCOIL_CENTER_GUIDE(widthAlambre,radial);
            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();
                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimesiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "COIL CENTER GUIDE";

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Center_Guide");
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla  COIL_CENTER_GUIDE de acuerdo al texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetALLCOIL_CENTER_GUIDE(string texto)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetAllCOIL_CENTER_GUIDE(texto);
            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimesiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);

                    Propiedad propR_Min = new Propiedad();
                    propR_Min.Unidad = "Milimeters (mm)";
                    propR_Min.Valor = (double)tipo.GetProperty("RADIAL_WIRE_MIN").GetValue(item, null);
                    propR_Min.DescripcionCorta = "Radial wire min";
                    herramental.Propiedades.Add(propR_Min);

                    Propiedad propR_MAX = new Propiedad();
                    propR_MAX.Unidad = "Milimeters (mm)";
                    propR_MAX.Valor = (double)tipo.GetProperty("RADIAL_WIRE_MAX").GetValue(item, null);
                    propR_MAX.DescripcionCorta = "Radial wire max";
                    herramental.Propiedades.Add(propR_MAX);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);

                }
            }
            ///Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Center_Guide");
        }

        /// <summary>
        /// Método que obtiene la información de Coil Center Guide.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoCOIL_Center_Guide(string codigo)
        {

            Herramental herramental = new Herramental();

            SO_COIL ServicioCoil = new SO_COIL();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioCoil.GetInfoCoil_Center_G(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();
                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_COIL_CENTER_GUIDE").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimesiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);

                    Propiedad propR_Min = new Propiedad();
                    propR_Min.Unidad = "Milimeters (mm)";
                    propR_Min.Valor = (double)tipo.GetProperty("RADIAL_WIRE_MIN").GetValue(item, null);
                    propR_Min.DescripcionCorta = "Radial wire min";
                    herramental.Propiedades.Add(propR_Min);

                    Propiedad propR_MAX = new Propiedad();
                    propR_MAX.Unidad = "Milimeters (mm)";
                    propR_MAX.Valor = (double)tipo.GetProperty("RADIAL_WIRE_MAX").GetValue(item, null);
                    propR_MAX.DescripcionCorta = "Radial wire max";
                    herramental.Propiedades.Add(propR_MAX);
                }
            }
            return herramental;
        }


        /// <summary>
        /// Método que inserta un registro a la tabla TBL_EXIT_GUIDE
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetExit_GUIDE(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.SetExit_GUIDE(obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC, obj.wire_width_min, obj.wire_width_max, obj.radial_wire_min, obj.radial_wire_max);
        }
        /// <summary>
        /// Método que modifica un registro de la tabla TBL_EXIT_GUIDE
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateExit_GUIDE(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.UpdateCOIL_CENTER_GUIDE(obj.ID,obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC, obj.wire_width_min, obj.wire_width_max, obj.radial_wire_min, obj.radial_wire_max);
        }

        /// <summary>
        /// Método que elimina un registro de la tabla TBL_EXIT_GUIDE
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteExit_GUIDE(int id)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.DeleteExit_GUIDE(id);
        }

        /// <summary>
        /// Obtiene las propiedades de un registro de la tabla EXIT_GUIDE, de acuerdo al width y radial
        /// </summary>
        /// <param name="widthAlambre"></param>
        /// <param name="radial">Milímetros</param>
        /// <returns></returns>
        public static DataTable GetEXIT_GUIDE(double widthAlambre, double radial)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetEXIT_GUIDE(widthAlambre, radial);
            //si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramo sla lista 
                foreach (var item in informacionBD)
                {
                    //Obetenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD, (string)tipo.GetProperty("Codigo").GetValue(item, null));

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);
                    
                    //Dimensiones 
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "EXIT GUIDE ";

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Exit_Guide");
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla EXIT_GUIDE de acuerdo al texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllEXIT_GUIDE(string texto)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetAllEXIT_GUIDE(texto);
            //si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramo sla lista 
                foreach (var item in informacionBD)
                {
                    //Obetenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones 
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);

                    Propiedad propR_Min = new Propiedad();
                    propR_Min.Unidad = "Milimeters (mm)";
                    propR_Min.Valor = (double)tipo.GetProperty("RADIAL_WIRE_MIN").GetValue(item, null);
                    propR_Min.DescripcionCorta = "Radial wire min";
                    herramental.Propiedades.Add(propR_Min);

                    Propiedad propR_MAX = new Propiedad();
                    propR_MAX.Unidad = "Milimeters (mm)";
                    propR_MAX.Valor = (double)tipo.GetProperty("RADIAL_WIRE_MAX").GetValue(item, null);
                    propR_MAX.DescripcionCorta = "Radial wire max";
                    herramental.Propiedades.Add(propR_MAX);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);

                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Exit_Guide");
        }

        /// <summary>
        /// Método que obtiene la información de Exit Guide.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoExit_Guide(string codigo)
        {

            Herramental herramental = new Herramental();

            SO_COIL ServicioCoil = new SO_COIL();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioCoil.GetInfoexitG(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();
                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_EXIT_GUIDE").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones 
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMax);

                    Propiedad propR_Min = new Propiedad();
                    propR_Min.Unidad = "Milimeters (mm)";
                    propR_Min.Valor = (double)tipo.GetProperty("RADIAL_WIRE_MIN").GetValue(item, null);
                    propR_Min.DescripcionCorta = "Radial wire min";
                    herramental.Propiedades.Add(propR_Min);

                    Propiedad propR_MAX = new Propiedad();
                    propR_MAX.Unidad = "Milimeters (mm)";
                    propR_MAX.Valor = (double)tipo.GetProperty("RADIAL_WIRE_MAX").GetValue(item, null);
                    propR_MAX.DescripcionCorta = "Radial wire max";
                    herramental.Propiedades.Add(propR_MAX);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que inserta un registro a la tabla TBL_EXTERNAL_GUIDE_ROLLER_1PIECE
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetExternal_GR_1P(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.SetExternal_GR_1P(obj.codigo, obj.code, obj.dimB, obj.wire_width_min, obj.wire_width_max);
        }
        /// <summary>
        ///  Método que modifica un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_1PIECE
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateExternal_GR_1P(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.UpdateExternal_GR_1P(obj.ID,obj.codigo, obj.code, obj.dimB, obj.wire_width_min, obj.wire_width_max);
        }

        /// <summary>
        /// Método que elimina un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_1PIECE
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteExternal_GR_1P(int id)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.DeleteExternal_GR_1P(id);
        }

        /// <summary>
        /// Obtiene las propiedades de un registro de la tabla EXTERNAL_GR_1P, de acuerdo al width
        /// </summary>
        /// <param name="widthAlambre">Milímetros</param>
        /// <returns></returns>
        public static DataTable GetEXTERNAL_GR_1P(double widthAlambre)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetEXTERNAL_GR_1P(widthAlambre);
            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();
                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);
                    
                    //Dimensiones
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "EXTERNAL GUIDE ROLLER 1 PIECE DIM "+ propiedadDimB.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "External_GR_1P");
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla EXTERNAL_GR_1PIECE de acuerdo al texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllEXTERNAL_GR_1P(string texto)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetAllEXTERNAL_GR_1P(texto);
            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);                  

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);

                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "External_GR_1P");
        }

        /// <summary>
        /// Método que obtiene la información de External Guide Roller 1 Piece.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoExternal_GR1P(string codigo)
        {

            Herramental herramental = new Herramental();

            SO_COIL ServicioCoil = new SO_COIL();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioCoil.GetInfoExternal_GR1P(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();
                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_EGR_1P").GetValue(item, null);


                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que inserta un registro a la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetExternal_GR_3P_1(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.SetExternal_GR_3P_1(obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC,obj.wire_width_min,obj.wire_width_max);
        }
        /// <summary>
        /// Método que modifica un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateExternal_GR_3P_1(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.UpdateExternal_GR_3P_1(obj.ID,obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC, obj.wire_width_min, obj.wire_width_max);
        }
        /// <summary>
        ///  Método que elimina un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_1
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteExternal_GR_3P_1(int id)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.DeleteExternal_GR_3P_1(id);
        }

        /// <summary>
        /// Obtiene las propiedades de un registro de la tabla EXTERNAL_GR_3P_1, de acuerdo al width del alambre
        /// </summary>
        /// <param name="widthAlambre">Milímetros</param>
        /// <returns></returns>
        public static DataTable GetEXTERNAL_GR_3P_1(double widthAlambre)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetEXTERNAL_GR_3P_1(widthAlambre);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();
                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "EXTERNAL GUIDE ROLLER 3 PIECES";

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "External_GR_3P_1");
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla EXTERNAL_GR_3P_1 de acuerdo al texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllEXTERNAL_GR_3P_1(string texto)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetAllEXTERNAL_GR_3P_1(texto);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);

                }
            }

            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "External_GR_3P_1");
        }

        /// <summary>
        /// Método que obtiene la información de External Guide Roller 3 Piece 1.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoExternal_GR3P_1(string codigo)
        {

            Herramental herramental = new Herramental();

            SO_COIL ServicioCoil = new SO_COIL();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioCoil.GetInfoExternal_GR3P_1(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();
                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_EGR_3P_1").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que inserta un registro a la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetExternal_GR_3P_2(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.SetExternal_GR_3P_2(obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC, obj.wire_width_min,obj.wire_width_max);
        }
        /// <summary>
        /// Método que modifica un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateExternal_GR_3P_2(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.UpdateExternal_GR_3P_2(obj.ID, obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC, obj.wire_width_min, obj.wire_width_max);
        }
        /// <summary>
        /// Método que elimina un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_2
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteExternal_GR_3P_2(int id)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.DeleteExternal_GR_3P_2(id);
        }

        /// <summary>
        /// Obtiene las propiedades de un registro de la tabla EXTERNAL_GR_3P_2, de acuerdo al width del alambre
        /// </summary>
        /// <param name="widthAlambre">Milímetros</param>
        /// <returns></returns>
        public static DataTable GetEXTERNAL_GR_3P_2(double widthAlambre)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetEXTERNAL_GR_3P_2(widthAlambre);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();
                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);
                    
                    //Dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "EXTERNAL GUIDE ROLLER 3 PIECES  ";

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "External_GR_3P_2");
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla EXTERNAL_GR_3P_2 de acuerdo al texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllEXTERNAL_GR_3P_2(string texto)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetAllEXTERNAL_GR_3P_2(texto);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);

                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "External_GR_3P_2");
        }

        /// <summary>
        /// Método que obtiene la información de External Guide Roller 3 Piece 2.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoExternal_GR3P_2(string codigo)
        {

            Herramental herramental = new Herramental();

            SO_COIL ServicioCoil = new SO_COIL();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioCoil.GetInfoExternal_GR3P_2(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();
                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_EGR_3P_2").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que inserta un registro a la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetExternal_GR_3P_3(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.SetExternal_GR_3P_3(obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC, obj.wire_width_min, obj.wire_width_max);
        }
        /// <summary>
        /// Método que modifica un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateExternal_GR_3P_3(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.UpdateExternal_GR_3P_3(obj.ID, obj.codigo, obj.code, obj.dimA, obj.dimB, obj.dimC, obj.wire_width_min, obj.wire_width_max);
        }
        /// <summary>
        /// Método que elimina un registro de la tabla TBL_EXTERNAL_GUIDE_ROLLER_3PIECES_3
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteExternal_GR_3P_3(int id)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.DeleteExternal_GR_3P_3(id);
        }

        /// <summary>
        /// Obtiene las propiedades de un registro de la tabla EXTERNAL_GR_3P_3, de acuerdo al width del alambre 
        /// </summary>
        /// <param name="widthAlambre">Milímetros</param>
        /// <returns></returns>
        public static DataTable GetEXTERNAL_GR_3P_3(double widthAlambre)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetEXTERNAL_GR_3P_3(widthAlambre);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obetenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();
                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "EXTERNAL GUIDE ROLLER 3 PIECES ";

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "External_GR_3P_3");
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla EXTERNAL_GR_3P_3 de acuerdo al texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllEXTERNAL_GR_3P_3(string texto)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetAllEXTERNAL_GR_3P_3(texto);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obetenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);

                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "External_GR_3P_3");
        }

        /// <summary>
        /// Método que obtiene la información de External Guide Roller 3 Piece 3.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoExternal_GR3P_3(string codigo)
        {

            Herramental herramental = new Herramental();

            SO_COIL ServicioCoil = new SO_COIL();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioCoil.GetInfoExternal_GR3P_3(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();
                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_EGR_3P_3").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //dimensiones
                    Propiedad propiedadDimA = new Propiedad();
                    propiedadDimA.Unidad = "Milimeters (mm)";
                    propiedadDimA.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimA.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimA);

                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMB").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim B";
                    herramental.Propiedades.Add(propiedadDimB);

                    Propiedad propiedadDimC = new Propiedad();
                    propiedadDimC.Unidad = "Milimeters (mm)";
                    propiedadDimC.Valor = (double)tipo.GetProperty("DIMC").GetValue(item, null);
                    propiedadDimC.DescripcionCorta = "Dim C";
                    herramental.Propiedades.Add(propiedadDimC);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que inserta un registro a la tabla TBL_SHIM_OF_THE_CUT_SYSTEM
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetSHIM_OF_THE_CUT_SYSTEM(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.SetSHIM_OF_THE_CUT_SYSTEM(obj.codigo, obj.code, obj.dimA, obj.wire_width_min, obj.wire_width_max);
        }
        /// <summary>
        /// Método que modifica un registro de la tabla TBL_SHIM_OF_THE_CUT_SYSTEM
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateSHIM_OF_THE_CUT_SYSTEM(Coil obj)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.UpdateSHIM_OF_THE_CUT_SYSTEM(obj.ID,obj.codigo, obj.code, obj.dimA, obj.wire_width_min, obj.wire_width_max);
        }
        /// <summary>
        /// Método que elimina un registro de la tabla TBL_SHIM_OF_THE_CUT_SYSTEM
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteSHIM_OF_THE_CUT_SYSTEM(int id)
        {
            //Inicializamos los servicios de coil 
            SO_COIL ServiceCoil = new SO_COIL();
            //Eejcutamos el método y retornamos el resultado
            return ServiceCoil.DeleteSHIM_OF_THE_CUT_SYSTEM(id);
        }

        /// <summary>
        /// Obtiene las propiedades de un registro de la tabla SHIM_OF_THE_CUT_SYSTEM, de acuerdo al width del alambre
        /// </summary>
        /// <param name="widthAlambre">Milímetros</param>
        /// <returns></returns>
        public static DataTable GetSHIM_OF_THE_CSYSTEM(double widthAlambre)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetSHIM_CSYSTEM(widthAlambre);

            //Si la lista que se obtuvo es diferente de nullo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();
                    //Convertimos la información a tipo Herramental.
                    herramental = ReadInformacionHerramentalEncontrado(informacionBD);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimB);

                    //Mapeamos el valor a DescipcionRuta.
                    herramental.DescripcionRuta = "SHIM OF THE CUT SYSTEM  " + propiedadDimB.Valor;

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);
                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Shim_CSystem");
        }

        /// <summary>
        /// Método que obtiene todos los registros de la tabla SHIM_OF_THE_CSYSTEM de acuerdo al texto de búsqueda
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static DataTable GetAllSHIM_OF_THE_CSYSTEM(string texto)
        {
            SO_COIL ServicioCoil = new SO_COIL();

            //Declaramos una ObservableCollection la cual almacenará la información de los herramentales.
            ObservableCollection<Herramental> ListaResultante = new ObservableCollection<Herramental>();

            //Ejecutamos el método que busca los herramentales a partir de un maxA y minB. El resultado lo guardamos en una lista anónima.
            IList informacionBD = ServicioCoil.GetAllSHIM_CUT_SYSTEM(texto);

            //Si la lista que se obtuvo es diferente de nullo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();

                    Herramental herramental = new Herramental();

                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimB);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);

                    //Agregamos el objeto a la lista resultante.
                    ListaResultante.Add(herramental);

                }
            }
            //Retornamos el resultado de ejecutar el método ConverToObservableCollectionHerramental_DataSet, enviandole como parámetro la lista resultante.
            return ConverToObservableCollectionHerramental_DataSet(ListaResultante, "Shim_CSystem");
        }

        /// <summary>
        /// Método que obtiene la información de Shim of the Cut System.
        /// </summary>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public static Herramental GetInfoShim_OTCS(string codigo)
        {

            Herramental herramental = new Herramental();

            SO_COIL ServicioCoil = new SO_COIL();

            //Ejecutamos el método para obtener la información, el resultado lo guardamos en una variable anónima.
            IList informacionBD = ServicioCoil.GetInfoShimOTCS(codigo);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo
                    Type tipo = item.GetType();
                    //Mapeamos los elementos necesarios en cada una de las propiedades del objeto.
                    herramental.Codigo = (string)tipo.GetProperty("CODIGO").GetValue(item, null);
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(item, null);
                    herramental.idHerramental = (int)tipo.GetProperty("ID_SHIM_OTCS").GetValue(item, null);

                    //Code
                    PropiedadCadena propCode = new PropiedadCadena();
                    propCode.DescripcionCorta = "Detalle";
                    propCode.Valor = (string)tipo.GetProperty("DETALLE").GetValue(item, null);
                    herramental.PropiedadesCadena.Add(propCode);

                    //Dimensiones
                    Propiedad propiedadDimB = new Propiedad();
                    propiedadDimB.Unidad = "Milimeters (mm)";
                    propiedadDimB.Valor = (double)tipo.GetProperty("DIMA").GetValue(item, null);
                    propiedadDimB.DescripcionCorta = "Dim A";
                    herramental.Propiedades.Add(propiedadDimB);

                    //Tamaño
                    Propiedad propWMin = new Propiedad();
                    propWMin.Unidad = "Milimeters (mm)";
                    propWMin.Valor = (double)tipo.GetProperty("WIRE_WIDTH_MIN").GetValue(item, null);
                    propWMin.DescripcionCorta = "Wire width min";
                    herramental.Propiedades.Add(propWMin);

                    Propiedad propWMax = new Propiedad();
                    propWMax.Unidad = "Milimeters (mm)";
                    propWMax.Valor = (double)tipo.GetProperty("WIDE_WIDTH_MAX").GetValue(item, null);
                    propWMax.DescripcionCorta = "Wire width max";
                    herramental.Propiedades.Add(propWMax);
                }
            }
            return herramental;
        }

        /// <summary>
        /// Método que obtiene el mejor herramental para coil
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static DataTable SelectBestCoil(DataTable dt)
        {
            //Declaramos un objeto de tipo de DataTable que será el que retornemos en el método.
            DataTable DataR = new DataTable();

            //Agregamos las columnas de code y description a la tabla.
            DataR.Columns.Add("Code");
            DataR.Columns.Add("Description");

            //Sólo se hace la iteración una vez
            foreach (DataRow row in dt.Rows)
            {
                //Mapeamos los valores de código y descripción en un datarow.
                DataRow dr = DataR.NewRow();
                dr["Code"] = row["Code"].ToString();
                dr["Description"] = row["DESCRIPTION"].ToString();

                //Agregamnos el datarow al datatable resultante.
                DataR.Rows.Add(dr);
                break;
            }
            return DataR;
        }

        #endregion

        #region Plano
        /// <summary>
        /// Método que obtiene todos los registros del plano
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Plano> GetPlano_Herramental()
        {
            //Inicializamos los servicios de clasificación.
            SO_Plano ServicePlano = new SO_Plano();

            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Plano> ListaResultante = new ObservableCollection<Plano>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList InformacionBD = ServicePlano.GetPlanoHerramental();

            //si la lista es diferente de nulo
            if (InformacionBD !=null)
            {
                //iteramos la lista
                foreach (var item in InformacionBD)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    Plano obj = new Plano();

                    //Asignamos los valores
                    obj.idPlano = (int)tipo.GetProperty("ID_PLANO").GetValue(item, null);
                    obj.NoPlano = (string)tipo.GetProperty("NO_PLANO").GetValue(item, null);
                    obj.FechaActualizacion = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
                    obj.FechaCreacion = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    obj.UsuarioActualizacion=(string)tipo.GetProperty("USUARIO_ACTUALIZACION").GetValue(item, null);
                    obj.UsuarioCreacion= (string)tipo.GetProperty("USUARIO_CREACION").GetValue(item, null);

                    //Agregamos el objeto a la lista
                    ListaResultante.Add(obj);
                }
            }
            //Retornamos la lista
            return ListaResultante;
        }
        #endregion
        #endregion

        #region Métodos Genéricos

        /// <summary>
        /// Método que transforma un objeto de tipo IList a List<Herramental>.
        /// </summary>
        /// <param name="Informacion"></param>
        /// <param name="p">Solo para cuando se requiera obtener en formato de lista</param>
        /// <returns></returns>
        public static List<Herramental> ReadInformacionHerramentalEncontrado(IList Informacion, bool p = true)
        {
            //Declaramos un objeto de tipo Herramental, que será el que retornemos en el método.
            List<Herramental> ListaHerramental = new List<Herramental>();

            //Verificamos que el valor del parámetro recibido sea diferente de nulo.
            if (Informacion != null)
            {
                //Iteramos la lista recibida.
                foreach (var elemento in Informacion)
                {

                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = elemento.GetType();

                    //Incializamos el objeto herramental.
                    Herramental herramental = new Herramental();

                    //Asingamos los valores correspondientes a cada propiedad del objeto herramental.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(elemento, null);
                    herramental.Encontrado = true;
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(elemento, null);
                    herramental.Activo = (bool)tipo.GetProperty("Activo").GetValue(elemento, null);
                    herramental.clasificacionHerramental.CantidadUtilizar = (int)tipo.GetProperty("CantidadUtilizar").GetValue(elemento, null);
                    herramental.clasificacionHerramental.Costo = (double)tipo.GetProperty("Costo").GetValue(elemento, null);
                    herramental.clasificacionHerramental.Descripcion = (string)tipo.GetProperty("Clasificacion").GetValue(elemento, null);
                    herramental.clasificacionHerramental.IdClasificacion = (int)tipo.GetProperty("idClasificacion").GetValue(elemento, null);
                    herramental.clasificacionHerramental.ListaCotasRevizar = new ObservableCollection<string>(tipo.GetProperty("ListaCotasRevisar").GetValue(elemento, null).ToString().Split(','));
                    herramental.clasificacionHerramental.UnidadMedida = (string)tipo.GetProperty("UnidadMedida").GetValue(elemento, null);
                    herramental.clasificacionHerramental.VerificacionAnual = (bool)tipo.GetProperty("VerificacionAnual").GetValue(elemento, null);
                    herramental.clasificacionHerramental.VidaUtil = (int)tipo.GetProperty("VidaUtil").GetValue(elemento, null);

                    //Falta agregar la columna plano.
                    herramental.Plano = string.Empty;
                    herramental.Propiedades = new ObservableCollection<Propiedad>();

                    ListaHerramental.Add(herramental);
                }
            }

            //Retornamos el objeto herramental.
            return ListaHerramental;
        }

        public static Herramental ReadInformacionHerramentalEncontrado(IList Informacion,string codigo)
        {
            //Declaramos un objeto de tipo Herramental, que será el que retornemos en el método.
            Herramental herramental = new Herramental();

            //Verificamos que el valor del parámetro recibido sea diferente de nulo.
            if (Informacion != null)
            {
                //Iteramos la lista recibida.
                foreach (var elemento in Informacion)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = elemento.GetType();

                    if (codigo == (string)tipo.GetProperty("Codigo").GetValue(elemento,null))
                    {
                        //Incializamos el objeto herramental.
                        herramental = new Herramental();

                        //Asingamos los valores correspondientes a cada propiedad del objeto herramental.
                        herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(elemento, null);
                        herramental.Encontrado = true;
                        herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(elemento, null);
                        herramental.Activo = (bool)tipo.GetProperty("Activo").GetValue(elemento, null);
                        herramental.clasificacionHerramental.CantidadUtilizar = (int)tipo.GetProperty("CantidadUtilizar").GetValue(elemento, null);
                        herramental.clasificacionHerramental.Costo = (double)tipo.GetProperty("Costo").GetValue(elemento, null);
                        herramental.clasificacionHerramental.Descripcion = (string)tipo.GetProperty("Clasificacion").GetValue(elemento, null);
                        herramental.clasificacionHerramental.IdClasificacion = (int)tipo.GetProperty("idClasificacion").GetValue(elemento, null);
                        herramental.clasificacionHerramental.ListaCotasRevizar = new ObservableCollection<string>(tipo.GetProperty("ListaCotasRevisar").GetValue(elemento, null).ToString().Split(','));
                        herramental.clasificacionHerramental.UnidadMedida = (string)tipo.GetProperty("UnidadMedida").GetValue(elemento, null);
                        herramental.clasificacionHerramental.VerificacionAnual = (bool)tipo.GetProperty("VerificacionAnual").GetValue(elemento, null);
                        herramental.clasificacionHerramental.VidaUtil = (int)tipo.GetProperty("VidaUtil").GetValue(elemento, null);

                        //Falta agregar la columna plano.
                        herramental.Plano = string.Empty;
                        herramental.Propiedades = new ObservableCollection<Propiedad>();
                    }

                    
                }
            }
            //Retornamos el objeto herramental.
            return herramental;
        }

        /// <summary>
        /// Método que transforma un objeto de tipo IList a Herramental.
        /// </summary>
        /// <param name="Informacion"></param>
        /// <returns></returns>
        public static Herramental ReadInformacionHerramentalEncontrado(IList Informacion)
        {
            //Declaramos un objeto de tipo Herramental, que será el que retornemos en el método.
            Herramental herramental = new Herramental();

            //Verificamos que el valor del parámetro recibido sea diferente de nulo.
            if (Informacion != null)
            {
                //Iteramos la lista recibida.
                foreach (var elemento in Informacion)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = elemento.GetType();

                    //Incializamos el objeto herramental.
                    herramental = new Herramental();

                    //Asingamos los valores correspondientes a cada propiedad del objeto herramental.
                    herramental.Codigo = (string)tipo.GetProperty("Codigo").GetValue(elemento, null);
                    herramental.Encontrado = true;
                    herramental.DescripcionGeneral = (string)tipo.GetProperty("Descripcion").GetValue(elemento, null);
                    herramental.Activo = (bool)tipo.GetProperty("Activo").GetValue(elemento, null);
                    herramental.clasificacionHerramental.CantidadUtilizar = (int)tipo.GetProperty("CantidadUtilizar").GetValue(elemento, null);
                    herramental.clasificacionHerramental.Costo = (double)tipo.GetProperty("Costo").GetValue(elemento, null);
                    herramental.clasificacionHerramental.Descripcion = (string)tipo.GetProperty("Clasificacion").GetValue(elemento, null);
                    herramental.clasificacionHerramental.IdClasificacion = (int)tipo.GetProperty("idClasificacion").GetValue(elemento, null);
                    herramental.clasificacionHerramental.ListaCotasRevizar = new ObservableCollection<string>(tipo.GetProperty("ListaCotasRevisar").GetValue(elemento, null).ToString().Split(','));
                    herramental.clasificacionHerramental.UnidadMedida = (string)tipo.GetProperty("UnidadMedida").GetValue(elemento, null);
                    herramental.clasificacionHerramental.VerificacionAnual = (bool)tipo.GetProperty("VerificacionAnual").GetValue(elemento, null);
                    herramental.clasificacionHerramental.VidaUtil = (int)tipo.GetProperty("VidaUtil").GetValue(elemento, null);
                    
                    //Falta agregar la columna plano.
                    herramental.Plano = string.Empty;
                    herramental.Propiedades = new ObservableCollection<Propiedad>();
                }
            }
            //Retornamos el objeto herramental.
            return herramental;
        }
        
        public static ObservableCollection<string> GetAllTreatment()
        {
            ObservableCollection<string> ListaResultante = new ObservableCollection<string>();
            return ListaResultante;
        }

        /// <summary>
        /// Método que convierte una Lista de datos de cadena a una Observable collection.
        /// </summary>
        /// <param name="lista"></param>
        /// <returns></returns>
        public static ObservableCollection<string> ConvertTo(List<string> lista)
        {
            //Declaramos una colección de strigns, que será la que retornamos en el método.
            ObservableCollection<string> ListaResultante = new ObservableCollection<string>();

            //Iteramos cada elemento de la lista.
            foreach (string item in lista)
            {
                //Agregamos el elemento iterado a la colección.
                ListaResultante.Add(item);
            }

            //Retornamos la colección generada.
            return ListaResultante;
        }

        /// <summary>
        /// Elimina los valores duplicados de una lista y deja solo un valor.
        /// </summary>
        /// <param name="inputList">Lista que se requiere evaluar.</param>
        /// <returns></returns>
        public static List<string> removeDuplicates(List<string> inputList)
        {
            Dictionary<string, int> uniqueStore = new Dictionary<string, int>();
            List<string> finalList = new List<string>();
            foreach (string currValue in inputList)
            {
                if (!uniqueStore.ContainsKey(currValue))
                {
                    uniqueStore.Add(currValue, 0);
                    finalList.Add(currValue);
                }
            }
            return finalList;
        }

        #endregion

        #region MateriasPrimas

        #region Pattern
        /// <summary>
        /// Método que obtiene todos los registros de la tabla Pattern2.
        /// </summary>
        /// <returns></returns>Lista obaservable con todos los datos de la  tabla Pattern2.
        public static ObservableCollection<Pattern> GetPattern()
        {
            //Inicializamos los servicios de Pattern 
            SO_Pattern ServicePattern = new SO_Pattern();

            //Se declara una lista de tipo ObservableCollection, la cúal se va a retornar.
            ObservableCollection<Pattern> Lista = new ObservableCollection<Pattern>();

            //Se obtiene las placas modelo de la BD;
            IList PatternBD = ServicePattern.GetAllPattern();

            //Verifcamos que la información de la base de datos no se encuentre vacía.
            if (PatternBD != null)
            {
                //Iteración de la información recibida.
                foreach (var item in PatternBD)
                {
                    //Se obtiene el tipo
                    System.Type tipo = item.GetType();

                    //Declaración del objeto de tipo Pattern que contendrá la información de un registro.
                    Pattern obj = new Pattern();

                    //Se asignan los valores.
                    obj.codigo.Valor = (string)tipo.GetProperty("codigo").GetValue(item, null);
                    obj.medida.Valor = (double)tipo.GetProperty("DIAMETRO").GetValue(item, null);
                    obj.diametro.Valor = (double)tipo.GetProperty("WIDTH").GetValue(item, null);
                    //obj.customer = (Cliente)tipo.GetProperty("CUSTOMER").GetValue(item, null);
                    obj.mounting.Valor = (int)tipo.GetProperty("MOUNTING").GetValue(item, null);
                    obj.on_14_rd_gate.Valor = (string)tipo.GetProperty("ON_14_RD_GATE").GetValue(item, null);
                    obj.button.Valor = (string)tipo.GetProperty("BUTTON").GetValue(item, null);
                    obj.cone.Valor = (string)tipo.GetProperty("CONE").GetValue(item, null);
                    obj.M_Circle.Valor = (string)tipo.GetProperty("CUSTOMER").GetValue(item, null);
                    obj.ring_w_min.Valor = (double)tipo.GetProperty("RING_WTH_min").GetValue(item, null);
                    obj.ring_w_max.Valor = (double)tipo.GetProperty("RING_WTH_max").GetValue(item, null);
                    obj.date_ordered.Valor = (string)tipo.GetProperty("DATE_ORDERED").GetValue(item, null);
                    obj.B_Dia.Valor = (double)tipo.GetProperty("B_DIA").GetValue(item, null);
                    obj.fin_Dia.Valor = (double)tipo.GetProperty("FIN_DIA").GetValue(item, null);
                    obj.turn_allow.Valor = (double)tipo.GetProperty("TURN_ALLOW").GetValue(item, null);
                    obj.cstg_sm_od.Valor = (double)tipo.GetProperty("CSTG_SM_OD").GetValue(item, null);
                    obj.shrink_allow.Valor = (double)tipo.GetProperty("SHRINK_ALLOW").GetValue(item, null);
                    obj.patt_sm_od.Valor = (double)tipo.GetProperty("PATT_SM_OD").GetValue(item, null);
                    obj.piece_in_patt.Valor = (double)tipo.GetProperty("PIECE_IN_PATT").GetValue(item, null);
                    obj.bore_allow.Valor = (double)tipo.GetProperty("BORE_ALLOW").GetValue(item, null);
                    obj.patt_sm_id.Valor = (double)tipo.GetProperty("PATT_SM_ID").GetValue(item, null);
                    obj.patt_thickness.Valor = (double)tipo.GetProperty("PATT_THICKNESS").GetValue(item, null);
                    obj.joint.Valor = (string)tipo.GetProperty("JOINT").GetValue(item, null);
                    obj.nick.Valor = (string)tipo.GetProperty("NICK").GetValue(item, null);
                    obj.nick_draf.Valor = (string)tipo.GetProperty("NICK_DRAF").GetValue(item, null);
                    obj.nick_depth.Valor = (string)tipo.GetProperty("NICK_DEPTH").GetValue(item, null);
                    obj.side_relief.Valor = (string)tipo.GetProperty("SIDE_RELIEF").GetValue(item, null);
                    obj.cam.Valor = (int)tipo.GetProperty("CAM").GetValue(item, null);
                    obj.cam_roll.Valor = (double)tipo.GetProperty("CAM_ROLL").GetValue(item, null);
                    obj.rise.Valor = (double)tipo.GetProperty("RISE").GetValue(item, null);
                    obj.OD.Valor = (double)tipo.GetProperty("OD").GetValue(item, null);
                    obj.ID.Valor = (double)tipo.GetProperty("ID").GetValue(item, null);
                    obj.diff.Valor = (double)tipo.GetProperty("DIFF").GetValue(item, null);
                    obj.tipo.Valor = (double)tipo.GetProperty("TIPO").GetValue(item, null);
                    obj.mounted.Valor = (string)tipo.GetProperty("mounted").GetValue(item, null);
                    obj.ordered.Valor = (string)tipo.GetProperty("ordered").GetValue(item, null);
                    obj.Checked.Valor = (string)tipo.GetProperty("checked").GetValue(item, null);
                    obj.date_checked.Valor = (string)tipo.GetProperty("date_checked").GetValue(item, null);
                    obj.esp_inst.Valor = (string)tipo.GetProperty("esp_inst").GetValue(item, null);
                    obj.factor_k.Valor = (double)tipo.GetProperty("factor_k").GetValue(item, null);
                    obj.rise_built.Valor = (double)tipo.GetProperty("rise_built").GetValue(item, null);
                    obj.ring_th_min.Valor = (double)tipo.GetProperty("ring_th_min").GetValue(item, null);
                    obj.ring_th_min.Valor = (double)tipo.GetProperty("ring_th_max").GetValue(item, null);
                    obj.estado.Valor = (bool)tipo.GetProperty("estado").GetValue(item, null);
                    obj.plato.Valor = (double)tipo.GetProperty("Plato").GetValue(item, null);
                    obj.detalle.Valor = (string)tipo.GetProperty("Detalle").GetValue(item, null);
                    obj.diseno.Valor = (bool)tipo.GetProperty("Diseno").GetValue(item, null);

                    //Agregamos el objeto tipo Pattern a la lista.
                    Lista.Add(obj);
                }
            }
            //Devolvemos la lista 
            return Lista;

        }

        /// <summary>
        /// Método que inserta un registro en la tabla de Pattern2
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string SetPattern(Pattern pattern)
        {
            //Inicializamos los servicios de SO_Pattern.
            SO_Pattern ServicePatter = new SO_Pattern();

            //Ejecutamos el método para insertar el registro, Retornamos el nuevo código de placa modelo.
            return ServicePatter.SetPattern(pattern.codigo.Valor, pattern.medida.Valor, pattern.diametro.Valor, pattern.customer.IdCliente, Convert.ToString(pattern.mounting.Valor), pattern.on_14_rd_gate.Valor, pattern.button.Valor, pattern.cone.Valor, pattern.M_Circle.Valor, pattern.ring_w_min.Valor, pattern.ring_w_max.Valor, pattern.date_ordered.Valor, pattern.B_Dia.Valor, pattern.fin_Dia.Valor, pattern.turn_allow.Valor, pattern.cstg_sm_od.Valor, pattern.shrink_allow.Valor, pattern.patt_sm_od.Valor, pattern.piece_in_patt.Valor, pattern.bore_allow.Valor, pattern.patt_sm_id.Valor, pattern.patt_thickness.Valor, pattern.joint.Valor, pattern.nick.Valor, pattern.nick_draf.Valor, pattern.nick_depth.Valor, pattern.side_relief.Valor, pattern.cam.Valor, pattern.cam_roll.Valor, pattern.rise.Valor, pattern.OD.Valor, pattern.ID.Valor, pattern.diff.Valor, Convert.ToInt32(pattern.tipo.Valor), pattern.mounted.Valor, pattern.ordered.Valor, pattern.Checked.Valor, pattern.date_checked.Valor, pattern.esp_inst.Valor, pattern.factor_k.Valor, pattern.rise_built.Valor, pattern.ring_th_min.Valor, pattern.ring_th_max.Valor, pattern.estado.Valor, pattern.plato.Valor, pattern.detalle.Valor, pattern.diseno.Valor);
        }
    
        /// <summary>
        /// Método que modifica un registro de la tabla Pattern2.
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static int UpdatePattern(Pattern pattern)
        {
            //Se inicializan los servicios de SO_Pattern.
            SO_Pattern ServicePattern = new SO_Pattern();

            //Ejecutamos el método para modificiar el registro,se retorna el número de registros afectados.
            return ServicePattern.UpdatePattern(pattern.codigo.Valor, pattern.medida.Valor, pattern.diametro.Valor, pattern.customer.IdCliente, Convert.ToString(pattern.mounting.Valor), 
                                                pattern.on_14_rd_gate.Valor, pattern.button.Valor, pattern.cone.Valor, pattern.M_Circle.Valor, pattern.ring_w_min.Valor, pattern.ring_w_max.Valor,
                                                pattern.date_ordered.Valor, pattern.B_Dia.Valor, pattern.fin_Dia.Valor, pattern.turn_allow.Valor, pattern.cstg_sm_od.Valor, pattern.shrink_allow.Valor,
                                                pattern.patt_sm_od.Valor, pattern.piece_in_patt.Valor, pattern.bore_allow.Valor, pattern.patt_sm_id.Valor, pattern.patt_thickness.Valor, pattern.joint.Valor,
                                                pattern.nick.Valor, pattern.nick_draf.Valor, pattern.nick_depth.Valor, pattern.side_relief.Valor, pattern.cam.Valor, pattern.cam_roll.Valor, pattern.rise.Valor,
                                                pattern.OD.Valor, pattern.ID.Valor, pattern.diff.Valor, Convert.ToInt32(pattern.tipo.Valor), pattern.mounted.Valor, pattern.ordered.Valor, pattern.Checked.Valor,
                                                pattern.date_checked.Valor, pattern.esp_inst.Valor, pattern.factor_k.Valor, pattern.rise_built.Valor, pattern.ring_th_min.Valor, pattern.ring_th_max.Valor, 
                                                pattern.estado.Valor, pattern.plato.Valor, pattern.detalle.Valor, pattern.diseno.Valor);
        }

        /// <summary>
        /// Método que elimina un registro de la tabla Pattern2
        /// </summary>
        /// <param name="pattern">Cadena que representa el código de placa modelo que se requiere eliminar.</param>
        /// <returns></returns>
        public static int DeletePattern(Pattern pattern)
        {
            //Inicializamos los servicios de SO_Pattern.
            SO_Pattern ServicePattern = new SO_Pattern();

            //Ejecutamos el método para insertar el registro, Retornamos la cantidad de registros eliminados.
            return ServicePattern.DeletePattern(pattern.codigo.Valor);
        }

        /// <summary>
        /// Método que agrega un nuevo registro,en base a otro registro existente
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static string CopyPattern(Pattern pattern)
        {
            //Declaramos un variable de tipo string, la cúal se le va asignar el último código agregado
            string code;
            //Se inicializan los servicios de SO_pattern
            SO_Pattern ServicePattern = new SO_Pattern();

            //Se manda a llamar a la función GetLasCode que retorna el último código agregado a la tabla, se manda como parámetro a la función add,
            //se le suma uno al código y se asigna a la variable code.
            code = GetNextCodePattern(ServicePattern.GetLastCode());
                
            //Se ejecuta el método para insertar un nuevo registro.
            return ServicePattern.SetPattern(code, pattern.medida.Valor, pattern.diametro.Valor, pattern.customer.IdCliente, Convert.ToString(pattern.mounting.Valor),
                                              pattern.on_14_rd_gate.Valor, pattern.button.Valor, pattern.cone.Valor, pattern.M_Circle.Valor, pattern.ring_w_min.Valor, pattern.ring_w_max.Valor,
                                              pattern.date_ordered.Valor, pattern.B_Dia.Valor, pattern.fin_Dia.Valor, pattern.turn_allow.Valor, pattern.cstg_sm_od.Valor, pattern.shrink_allow.Valor,
                                              pattern.patt_sm_od.Valor, pattern.piece_in_patt.Valor, pattern.bore_allow.Valor, pattern.patt_sm_id.Valor, pattern.patt_thickness.Valor, pattern.joint.Valor,
                                              pattern.nick.Valor, pattern.nick_draf.Valor, pattern.nick_depth.Valor, pattern.side_relief.Valor, pattern.cam.Valor, pattern.cam_roll.Valor, pattern.rise.Valor,
                                              pattern.OD.Valor, pattern.ID.Valor, pattern.diff.Valor, Convert.ToInt32(pattern.tipo.Valor), pattern.mounted.Valor, pattern.ordered.Valor, pattern.Checked.Valor,
                                              pattern.date_checked.Valor, pattern.esp_inst.Valor, pattern.factor_k.Valor, pattern.rise_built.Valor, pattern.ring_th_min.Valor, pattern.ring_th_max.Valor,
                                              pattern.estado.Valor, pattern.plato.Valor, pattern.detalle.Valor, pattern.diseno.Valor);
        }

        /// <summary>
        /// Método que recibe el último código agregado y le suma uno.
        /// </summary>
        /// <param name="LastCode"></param>
        /// <returns></returns>
        public static string GetNextCodePattern(string LastCode)
        {
            //Declaración de la variable code, la cúal se va a retornar ya con nuevo valor del código.
            string code;
            //Declaración de la variable número, se le va asignar el número de la cadena recibida.
            int number;
            //Se recupera una cadena de la variable recibida, comienza en la posición 3 y tiene la longitud de LastCode menos 3
            //Se convierte a tipo int
            number=Int32.Parse(LastCode.Substring(3,LastCode.Length-3));
            //Al número de la cadena se le suma uno.
            number += 1;
            //retorna el nuevo string, concatenado con el número.
            return code = string.Concat("BC-", number.ToString());
        }

        /// <summary>
        /// Méto el cual obtiene el valor de CamTurnCosntant.
        /// </summary>
        /// <param name="elAnillo"></param>
        /// <param name="RingShape"></param>
        /// <returns></returns>
        public static double GetCamTurnConstant(Anillo elAnillo, string RingShape)
        {
            //Declaramos una variable la cual será la que retornemos en el método.
            double valor = 0;

            //Incializamos los servicios de SO_Material.
            SO_Material ServicioMaterial = new SO_Material();

            //Ejecutamos el método para obtener el valor, el resultado lo guardamos en un DataSet.
            DataSet informacionBD = ServicioMaterial.GetCamTurnConstant(elAnillo.MaterialBase.Especificacion.Valor, elAnillo.TipoAnillo, RingShape);

            //Verificamos que el dataset sea diferente de nulo.
            if (informacionBD != null)
            {
                //Verificamos que el dataset contenga al menos una tabla y que la tabla 0 contenga al menos un registro.
                if (informacionBD.Tables.Count > 0 && informacionBD.Tables[0].Rows.Count > 0)
                {
                    //Iteramos los elementos de la tabla 0.
                    foreach (DataRow element in informacionBD.Tables[0].Rows)
                    {
                        //Obtenemos el valor y lo asignamos a la variable local.
                        valor = Convert.ToDouble(element["valor"].ToString());
                    }
                }
            }

            //Retornamos el valor.
            return valor;
        }

        /// <summary>
        /// Método que obtiene un listado de códigos de placas modelos probables.
        /// </summary>
        /// <param name="diameter"></param>
        /// <returns></returns>
        public static List<string> GetProbablesPlacas(double diameter)
        {
            //Inicializamos los servicios de SO_Pattern.
            SO_Pattern ServicePattern = new SO_Pattern();

            //Declaramos una lista de string la cual será la que retornemos en el método.
            List<string> listaPattern = new List<string>();

            //Ejecutamos el método para obtener la inforamción de la base de datos. El resutlado lo asigamos a una lista anónima.
            IList informacionBD = ServicePattern.GetProbablyPattern(diameter);

            //Verificamos que la lista sea diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos cada elemento de la lista.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos una variable en la cual guadaremos el código de la placa modelo.
                    string pattern = "";

                    //Obtenemos el código de la placa modelo.
                    pattern = (string)tipo.GetProperty("Codigo").GetValue(item, null);

                    //Agregamos el código de la placa modelo a la lista.
                    listaPattern.Add(pattern);
                }
            }

            //Retornamos la lista.
            return listaPattern;
        }

        /// <summary>
        /// Método el cual aprueba la placa modelo para ver si puede ser utilizada para el desarrollo de un componente.
        /// </summary>
        /// <param name="codigoPlaca"></param>
        /// <param name="diameter"></param>
        /// <param name="piece_"></param>
        /// <param name="ts"></param>
        /// <param name="bs"></param>
        /// <param name="stock_thick"></param>
        /// <param name="min_piece"></param>
        /// <param name="max_piece"></param>
        /// <param name="a4"></param>
        /// <param name="widthAnillo"></param>
        /// <param name="Proceso"></param>
        /// <returns></returns>
        public static bool aprobarPlacaModelo(string codigoPlaca, double diameter, double piece_, double ts, double bs, double stock_thick, double min_piece, double max_piece, double a4, double widthAnillo, string Proceso)
        {
            SO_Pattern ServicioPattern = new SO_Pattern();

            Pattern2 pattern = ServicioPattern.GetPattern(codigoPlaca);

            double path_width, q;
            double size1 = 0;
            double piece1 = 0;
            double rise1 = 0;
            double turn1 = 0;
            double pattern1 = 0;
            double pattern_width = 0;
            double fin_dia1 = 0;

            if (pattern != null)
            {
                pattern_width = Convert.ToDouble(pattern.DIAMETRO);
                path_width = Convert.ToDouble(pattern.DIAMETRO);
                q = Convert.ToDouble(pattern.DIAMETRO);
                size1 = Convert.ToDouble(pattern.MEDIDA);
                turn1 = Convert.ToDouble(pattern.TURN_ALLOW);
                piece1 = Convert.ToDouble(pattern.PIECE_IN_PATT);
                rise1 = Convert.ToDouble(pattern.RISE);
                fin_dia1 = Convert.ToDouble(pattern.CSTG_SM_OD);
                pattern1 = Convert.ToDouble(pattern.PATT_THICKNESS);

                double size2, size3, size4, size5, size6, size7;
                double piece2, piece3, piece4, piece5, piece6, piece7;
                double rise2, rise3, rise4, rise5, rise6, rise7;
                double turn2, turn3, turn4, turn5, turn6, turn7;
                double pattern2, pattern3, pattern4, pattern5, pattern6;


                size3 = Math.Round(diameter * 1, 3);
                size2 = Math.Round(size3 - size1, 3);
                size4 = 0;
                size5 = (1 * size4) + size3;
                size6 = 0;
                size7 = size5 + (1 * size6);

                if (size3 > size1)
                {
                    piece2 = Math.Round(Math.Abs((size2 * 1)) * Math.PI * (-1), 4);
                }
                else
                {
                    piece2 = Math.Round(Math.Abs((size2 * 1)) * Math.PI, 4);
                }

                piece3 = Math.Round(piece2 + (1 * piece1), 3);
                piece4 = Math.Round(piece_ - piece3, 3);
                piece5 = Math.Round((1 * piece3) + piece4, 3);
                piece6 = 0;
                piece7 = piece6 + piece5 * 1;
                //---Calculos de rise
                rise2 = 0;
                rise3 = rise2 * 1 + rise1;
                rise4 = 0;
                rise5 = rise4 * 1 + rise3;
                rise6 = 0;
                rise7 = rise6 * 1 + rise5;

                //---- Calculo de turn
                turn2 = 0;
                turn3 = turn2 + (turn1 * 1);
                if (piece5 > piece3)
                {
                    turn4 = Math.Round((-1) * Math.Abs(piece4 / Math.PI), 3);
                }
                else
                {
                    turn4 = Math.Round(Math.Abs(piece4 / Math.PI), 3);
                }
                turn5 = Math.Round(1 * turn4 + turn3, 3);
                turn6 = 0;
                turn7 = 1 * turn6 + turn5;
                ts = turn7;

                //--- Calculo de pattern
                pattern2 = Math.Round(stock_thick * (-1), 3);
                pattern3 = Math.Round(pattern1 + (pattern2 * 1), 3);
                pattern4 = Math.Round(pattern3 * 2, 3);
                pattern5 = Math.Round(turn7 * (-1), 3);
                pattern6 = Math.Round(pattern5 + pattern4 * 1, 3);
                bs = pattern6;

                //---- Calculos de fin dia
                double fin_dia2, fin_dia3, fin_dia4, fin_dia5, fin_dia6, fin_dia7;
                fin_dia2 = 0;
                fin_dia3 = fin_dia2 + fin_dia1 * 1;
                //piece
                if (piece4 > piece3)
                {
                    fin_dia4 = Math.Round(Math.Abs(piece4 / Math.PI) * (-1), 3);
                }
                else
                {
                    fin_dia4 = Math.Round(Math.Abs(piece4 / Math.PI), 3);
                }
                fin_dia5 = Math.Round(fin_dia4 + fin_dia3 * 1, 3);
                fin_dia6 = 0;
                fin_dia7 = Math.Round(fin_dia6 + fin_dia5 * 1, 3);

                //---------validacion, criterios q se deben de cumplir para poder seleccionar la materia prima
                bool size_b, turn_b, piece_b, pattern_b;
                if (size3 == size7)
                {
                    size_b = true;
                }
                else
                {
                    size_b = false;
                }

                double ts_min, ts_max, bs_min, bs_max;
                ts_min = 0.055;
                ts_max = 0.09;
                bs_min = 0.055;
                bs_max = 0.1;

                if ((turn7 >= ts_min) && (turn7 <= ts_max))
                {
                    turn_b = true;
                }
                else
                {
                    turn_b = false;
                }

                if ((piece7 >= min_piece) && (piece7 <= max_piece) && (piece4 >= (-1) * a4) && (piece4 <= a4))
                {
                    piece_b = true;
                }
                else
                {
                    piece_b = false;
                }

                if ((pattern6 >= bs_min) && (pattern6 <= bs_max))
                {
                    pattern_b = true;
                }
                else
                {
                    pattern_b = false;
                }

                bool mp_aprobado = false;
                if ((size_b == true) && (turn_b == true) && (piece_b == true) && (pattern_b == true))
                {
                    mp_aprobado = true;
                }
                else
                {
                    mp_aprobado = false;
                }

                bool prueba_width = false;

                prueba_width = aprobarWidthPlacaModelo(pattern_width, widthAnillo, Proceso);
                return (mp_aprobado && prueba_width);
            }
            else
            {
                return false;
            }
            
        }

        /// <summary>
        /// Método el cual aprueba el width de la placa modelo.
        /// </summary>
        /// <param name="pattern_width"></param>
        /// <param name="widthAnillo"></param>
        /// <param name="proceso"></param>
        /// <returns></returns>
        private static bool aprobarWidthPlacaModelo(double pattern_width, double widthAnillo, string proceso)
        {
            SO_Pattern ServicePattern = new SO_Pattern();

            IList informacionBD = ServicePattern.GetIdealWidthPlacaModelo(widthAnillo, proceso);

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    System.Type tipo = item.GetType();

                    double p_min = (double)tipo.GetProperty("Minimum_casting_width").GetValue(item,null);

                    double p_ideal = (double)tipo.GetProperty("ideal_casting_Width").GetValue(item,null);

                    if ((p_min <= pattern_width) && (p_ideal >= pattern_width))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        #endregion Pattern

        #region Cuffs
        /// <summary>
        /// Método que obtiene todos los registros de la tabla Cuffs.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Cuffs> GetCuffs()
        {
            //Inicializamos los servicios de Cuffs.
            SO_Cuffs ServiceCuffs = new SO_Cuffs();

            //Se declara una lista de tipo ObservableCollection, la cúal se va a retornar.
            ObservableCollection<Cuffs> Lista = new ObservableCollection<Cuffs>();

            //Se obtienen los registros de la BD.
            IList ObjCuffs = ServiceCuffs.GetCuff();

            //Se verifica que la información de la base de datos no se encuentre vacía
            if (ObjCuffs!=null)
            {
                //Iteración de la información recibida
                foreach (var item in ObjCuffs)
                {
                    //Se obtiene el tipo
                    System.Type tipo = item.GetType();

                    //Declaración del objeto de tipo Cuffs que contendrá la información de un registro.
                    Cuffs obj = new Cuffs();

                    //Se asignan los valores 
                    obj.no_cuff.Valor = (string)tipo.GetProperty("no_cuff").GetValue(item,null);
                    obj.dia_ext.Valor = (double)tipo.GetProperty("dia_ext").GetValue(item, null);
                    obj.dia_int.Valor = (double)tipo.GetProperty("dia_int").GetValue(item, null);
                    obj.largo.Valor = (double)tipo.GetProperty("largo").GetValue(item, null);
                    obj.peso.Valor = (double)tipo.GetProperty("peso").GetValue(item, null);

                    //Agregamos el objeto a la lista
                    Lista.Add(obj);
                }
            }
            //Se retorna la lista 
            return Lista;
        }

        /// <summary>
        /// Método para insertar un nuevo registro en la tabla Cuffs.
        /// </summary>
        /// <param name="cuffs"></param>
        /// <returns></returns>
        public static string SetCuffs(Cuffs cuffs)
        {
            //Se inicializa los servicios de SO_Cuffs.
            SO_Cuffs ServiceCuffs = new SO_Cuffs();

            //Se ejecuta el método para insertar el registro, se retorna el código del cuff insertado.
            return ServiceCuffs.SetCuff(cuffs.no_cuff.Valor,cuffs.dia_ext.Valor,cuffs.dia_int.Valor,cuffs.largo.Valor,cuffs.peso.Valor);
        }

        /// <summary>
        /// Método para modificar un registro en la tabla Cuffs.
        /// </summary>
        /// <param name="cuff"></param>
        /// <returns></returns>
        public static int UpdateCuffs(Cuffs cuff)
        {
            //Se inicializa los servicios de SO_Cuffs.
            SO_Cuffs ServiceCuffs = new SO_Cuffs();

            //Se ejectuta el método para actualizar los datos del registro, retorna la cantidad de registros actualizados.
            return ServiceCuffs.UpdateCuffs(cuff.no_cuff.Valor, cuff.dia_ext.Valor, cuff.dia_int.Valor, cuff.largo.Valor, cuff.peso.Valor);
        }

        /// <summary>
        /// Método para eliminar un registro en la tabla Cuffs.
        /// </summary>
        /// <param name="cuff"></param>
        /// <returns></returns>
        public static int DeleteCuff(Cuffs cuff)
        {
            //Se inicializa los servicios de SO_Cuffs.
            SO_Cuffs ServiceCuffs = new SO_Cuffs();

            //Se ejectuta el método de eliminar, se retorna la cantidad de registros eliminados.
            return ServiceCuffs.DeleteCuffs(cuff.no_cuff.Valor);

        }
        #endregion

        #region TubosCL

        /// <summary>
        ///  Método que obtiene todos los registros de la tabla Cuffs.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Tubos_CL> GetTubosCL()
        {
            //Se inician los servicios de TubosCL.
            SO_TubosCL ServiceTubosCL = new SO_TubosCL();

            //Se declara una lista de tipo ObservableCollection, la cúal se va a retornar.
            ObservableCollection<Tubos_CL> Lista = new ObservableCollection<Tubos_CL>();

            //Se obtienen los registros de la BD.
            IList objTubosCL = ServiceTubosCL.GetTubosCL();

            //Se verifica que la información de la base de datos no se encuentre vacía
            if (objTubosCL != null)
            {
                //Iteración de la información recibida
                foreach (var item in objTubosCL)
                {
                    //Se obtiene el tipo
                    System.Type tipo = item.GetType();

                    //Declaración del objeto de tipo TubosCL que contendrá la información de un registro.
                    Tubos_CL obj = new Tubos_CL();
                    //Se asignan los valores 
                    obj.Tubo.Valor = (string)tipo.GetProperty("Tubo").GetValue(item, null);
                    obj.DiaExt.Valor = (double)tipo.GetProperty("DiaExt").GetValue(item, null);
                    obj.DiaInt.Valor = (double)tipo.GetProperty("DiaInt").GetValue(item, null);
                    obj.Thickness.Valor = (double)tipo.GetProperty("Thickness").GetValue(item, null);
                    obj.Largo.Valor = (int)tipo.GetProperty("Largo").GetValue(item, null);

                    //Se agrega el objeto a la lista
                    Lista.Add(obj);
                }
            }
            //Se retorna la lista
            return Lista;
        }

        /// <summary>
        /// Método para insertar un registro a la tabla TubosCL
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SetTubosCL(Tubos_CL obj)
        {

            //Se inician los servicios de TubosCL.
            SO_TubosCL ServiceTubosCL = new SO_TubosCL();

            //Se ejecuta el método y retorna el código del tubo que fue insertado.
            return ServiceTubosCL.SetTubosCL(obj.Tubo.Valor,obj.DiaExt.Valor,obj.DiaInt.Valor,obj.Thickness.Valor,obj.Largo.Valor);
        }

        /// <summary>
        /// Método para actualizat un registro de la tabla TubosCL
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateTubosCL(Tubos_CL obj)
        {
            //Se inician los servicios de TubosCL.
            SO_TubosCL ServiceTubosCL = new SO_TubosCL();

            //Se ejecuta el método retorna los registros que fueron modificados.
            return ServiceTubosCL.UpdateTubosCL(obj.Tubo.Valor, obj.DiaExt.Valor, obj.DiaInt.Valor, obj.Thickness.Valor, obj.Largo.Valor);
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla tubosCL.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteTubosCL(Tubos_CL obj)
        {
            // Se inician los servicios de TubosCL.
             SO_TubosCL ServiceTubosCL = new SO_TubosCL();

            //Se ejecuta el método y retorna el número de registros que fueron afectados.
            return ServiceTubosCL.DeleteTubosCL(obj.Tubo.Valor);
        }

        #endregion

        #region TubosHD

        /// <summary>
        /// Método que obtiene todos los registros de la tabla TubosHD.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Tubos_HD> GetTubosHD()
        {
            //Se inician los servicios de TubosHD.
            SO_TubosHD ServiceTubosHD = new SO_TubosHD();

            //Se declara una lista de tipo ObservableCollection, la cúal se va a retornar.
            ObservableCollection<Tubos_HD> Lista = new ObservableCollection<Tubos_HD>();

            //Se obtienen los registros de la BD.
            IList objTubosHD = ServiceTubosHD.GetTubosHD();

            //Se verifica que la información de la base de datos no se encuentre vacía
            if (objTubosHD != null)
            {
                //Iteración de la información recibida
                foreach (var item in objTubosHD)
                {
                    //Se obtiene el tipo
                    System.Type tipo = item.GetType();

                    //Declaración del objeto de tipo TubosHD que contendrá la información de un registro.
                    Tubos_HD obj = new Tubos_HD();
                    //Se asignan los valores 
                    obj.Tubo.Valor = (string)tipo.GetProperty("Tubo").GetValue(item, null);
                    obj.DiaExt.Valor = (double)tipo.GetProperty("DiaExt").GetValue(item, null);
                    obj.DiaInt.Valor = (double)tipo.GetProperty("DiaInt").GetValue(item, null);
                    obj.Thickness.Valor = (double)tipo.GetProperty("Thickness").GetValue(item, null);
                    obj.Largo.Valor = (double)tipo.GetProperty("Largo").GetValue(item, null);
                    obj.Molde.Valor= (string)tipo.GetProperty("Molde").GetValue(item, null);
                    obj.RPM.Valor = (int)tipo.GetProperty("RPM").GetValue(item, null);

                    //Se agrega el objeto a la lista
                    Lista.Add(obj);
                }
            }
            //Se retorna la lista
            return Lista;
        }

        /// <summary>
        /// Método para insertar registros a la tabla TubosHD.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string SetTubosHD(Tubos_HD obj)
        {

            //Se inician los servicios de TubosCL.
            SO_TubosHD ServiceTubosHD = new SO_TubosHD();

            //Se ejecuta el método y retorna el código del tubo que fue insertado.
            return ServiceTubosHD.SetTubosHD(obj.Tubo.Valor, obj.DiaExt.Valor, obj.DiaInt.Valor, obj.Thickness.Valor, obj.Largo.Valor,obj.Molde.Valor,Convert.ToInt32( obj.RPM.Valor));
        }

        /// <summary>
        /// Método para modificar un registro de la tabla.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateTubosHD(Tubos_HD obj)
        {

            //Se inician los servicios de TubosCL.
            SO_TubosHD ServiceTubosHD = new SO_TubosHD();

            //Se ejecuta el método y retorna el código del tubo que fue insertado.
            return ServiceTubosHD.UpdateTubosHD(obj.Tubo.Valor, obj.DiaExt.Valor, obj.DiaInt.Valor, obj.Thickness.Valor, obj.Largo.Valor, obj.Molde.Valor, Convert.ToInt32(obj.RPM.Valor));
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla TubosHd.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int DeleteTubosHD(Tubos_HD obj)
        {
            // Se inician los servicios de TubosHD.
            SO_TubosHD ServiceTubosHD = new SO_TubosHD();

            //Se ejecuta el método y retorna el número de registros que fueron afectados.
            return ServiceTubosHD.DeleteTubosHD(obj.Tubo.Valor);
        }
        #endregion

        #region Especificaciones

        /// <summary>
        /// Método el cual obtiene todas las especificaciones de materia prima.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<string> GetAllEspecificacionesMateriaPrima()
        {
            SO_Especificaciones ServicioEspecificaciones = new SO_Especificaciones();

            List<string> ListaResultante = new List<string>();

            IList InformacionBD = ServicioEspecificaciones.GetAllEspecificaciones();

            if (InformacionBD != null)
            {
                foreach (var registro in InformacionBD)
                {
                    System.Type tipo = registro.GetType();

                    string dato = "";
                    dato = (string)tipo.GetProperty("id_material").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Odl_Mahle").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref1").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref2").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref3").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref4").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref5").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref6").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref7").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref8").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref9").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref10").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref11").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }

                    dato = (string)tipo.GetProperty("Ref12").GetValue(registro, null);
                    if (!string.IsNullOrEmpty(dato.Trim()))
                    {
                        ListaResultante.Add(dato);
                    }
                }
            }

            ListaResultante.Sort();

            ListaResultante = removeDuplicates(ListaResultante);

            return ConvertTo(ListaResultante);
        }

        #endregion

        #endregion

        #region Unidades

        /// <summary>
        /// Método que obtiene las unidades correspondientes al tipo de dato recibido.
        /// </summary>
        /// <param name="TipoDato"></param>
        /// <returns></returns>
        public static ObservableCollection<string> GetUnidades(string TipoDato)
        {
            //Inicializamos el servicio de Unidades.
            SO_Unidades ServiceUnidades = new SO_Unidades();

            //Declaramos un lista Observable de tipo cadena. Esta lista será la que retornaremos en el método.
            ObservableCollection<string> ListaResultante = new ObservableCollection<string>();

            //Declaramos una lista anónima en la que almacenaremos la información de la base de datos.
            IList InformacionBD;

            //Verificamos de que tipo tendremos que obtener la información.
            if (TipoDato == "Distance")
            {
                InformacionBD = ServiceUnidades.GetUnidadesDistancia();
            }
            else {
                if (TipoDato == "Force")
                {
                    InformacionBD = ServiceUnidades.GetUnidadesForce();
                }
                else {
                    if (TipoDato == "Mass")
                    {
                        InformacionBD = ServiceUnidades.GetUnidadesMas();
                    }
                    else {
                        if (TipoDato == "Presion")
                        {
                            InformacionBD = ServiceUnidades.GetUnidadesPresion();
                        }
                        else {
                            if (TipoDato == "Tiempo")
                            {
                                InformacionBD = ServiceUnidades.GetUnidadesTiempo();
                            }
                            else {
                                if (TipoDato == "Cantidad")
                                {
                                    InformacionBD = ServiceUnidades.GetUnidadesCantidad();
                                }
                                else {
                                    if (TipoDato == "Angle")
                                    {
                                        InformacionBD = ServiceUnidades.GetUnidadesAngle();
                                    }
                                    else {
                                        if (TipoDato == "Dureza")
                                        {
                                            InformacionBD = ServiceUnidades.GetUnidadesDureza();
                                        }
                                        else {
                                            InformacionBD = null;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Verificamos que el objeto sea diferente de nulo.
            if (InformacionBD != null)
            {
                //Iteramos la información.
                foreach (var item in InformacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramemos una cadena en la cual guardaremos la unidad.
                    string elemento;

                    //Obtenemos el valor de la unidad del elemento iterado.
                    elemento = (string)tipo.GetProperty("UNIDAD").GetValue(item, null);

                    //Agregamos el elemento a la lista.
                    ListaResultante.Add(elemento);
                }
            }

            //Retornamos la lista resultante.
            return ListaResultante;

        }

        #endregion

        #region Clientes

        /// <summary>
        /// Método el cual obtiene una lista de todos los clientes.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Cliente> GetAllClientes()
        {
            SO_Cliente ServicioCliente = new SO_Cliente();

            ObservableCollection<Cliente> ListaResultante = new ObservableCollection<Cliente>();

            IList InformacionBD = ServicioCliente.GetAllClientes();

            if (InformacionBD != null)
            {
                foreach (var item in InformacionBD)
                {
                    System.Type tipo = item.GetType();

                    Cliente registro = new Cliente();

                    registro.IdCliente = (int)tipo.GetProperty("id_cliente").GetValue(item, null);
                    registro.NombreCliente = (string)tipo.GetProperty("Cliente1").GetValue(item, null);

                    ListaResultante.Add(registro);
                }
            }

            

            return ListaResultante;
        }

        #endregion

        #region Usuario
        /// <summary>
        /// Método que obtiene la información del usuario a partir de un usuario y una contraseña.
        /// </summary>
        /// <param name="Usuario">Cadena que representa el usuario que se requiere obtner.</param>
        /// <param name="Contrasena">Cadena que representa la contraseña del usuario.</param>
        /// <returns>Usuario que contiene la información del usuario con las credenciales enviadas.</returns>
        public static Task<Usuario> GetLogin(string Usuario, string Contrasena)
        {
            return Task.Run(() =>
            {
                //Declaramos un objeto de tipo Usuario que será el que retornemos en el método.
                Usuario usuario = null;

                //Inicializamos los servicios de usuario.
                SO_Usuario ServiceUsuario = new SO_Usuario();

                SO_Usuarios ServiceUsuarios = new SO_Usuarios();

                //Ejecutamos el método del login y la información resultante la asignamos a un objeto local.
                DataSet InformacionBD = ServiceUsuario.GetLogin(Usuario, Contrasena);

                //Comparamos si la información obtenida de la consulta es diferente de nulo.
                if (InformacionBD != null)
                {

                    //Comparamos si la información obtenida contiene al menos una tabla y esa tabla contiene al menos un registro.
                    if (InformacionBD.Tables.Count > 0 && InformacionBD.Tables[0].Rows.Count > 0)
                    {

                        //Itermamos los registro de la tabla cero.
                        foreach (DataRow element in InformacionBD.Tables[0].Rows)
                        {

                            //Inicializamos el objeto Usuario.
                            usuario = new Usuario();

                            //Asignamos los valores de la información a las propiedades del usuario correspondientes.
                            usuario.Block = Convert.ToBoolean(element["Bloqueado"]);
                            usuario.ApellidoMaterno = Convert.ToString(element["AMaterno"]);
                            usuario.ApellidoPaterno = Convert.ToString(element["APaterno"]);
                            usuario.Nombre = Convert.ToString(element["Nombre"]);
                            usuario.NombreUsuario = Convert.ToString(element["Usuario"]);

                            usuario.PerfilRGP = Convert.ToBoolean(element["PERFIL_RGP"]);
                            usuario.PerfilTooling = Convert.ToBoolean(element["PERFIL_TOOLING"]);
                            usuario.PerfilRawMaterial = Convert.ToBoolean(element["PERFIL_RAW_MATERIAL"]);
                            usuario.PerfilStandarTime = Convert.ToBoolean(element["PERFIL_STANDAR_TIME"]);
                            usuario.PerfilQuotes = Convert.ToBoolean(element["PERFIL_QUOTES"]);
                            usuario.PerfilCIT = Convert.ToBoolean(element["PERFIL_CIT"]);
                            usuario.PerfilData = Convert.ToBoolean(element["PERFIL_DATA"]);
                            usuario.PerfilUserProfile = Convert.ToBoolean(element["PERFIL_USER_PROFILE"]);
                            usuario.PerfilHelp = Convert.ToBoolean(element["PERFIL_HELP"]);

                            usuario.PrivilegioRGP = Convert.ToBoolean(element["PRIVIL_HELP"]);
                            usuario.PrivilegioTooling = Convert.ToBoolean(element["PRIVIL_HELP"]);
                            usuario.PrivilegioRawMaterial = Convert.ToBoolean(element["PRIVIL_HELP"]);
                            usuario.PrivilegioStandarTime = Convert.ToBoolean(element["PRIVIL_HELP"]);
                            usuario.PrivilegioQuotes = Convert.ToBoolean(element["PRIVIL_HELP"]);
                            usuario.PrivilegioCIT = Convert.ToBoolean(element["PRIVIL_HELP"]);
                            usuario.PrivilegioData = Convert.ToBoolean(element["PRIVIL_HELP"]);
                            usuario.PrivilegioUserProfile = Convert.ToBoolean(element["PRIVIL_HELP"]);
                            usuario.PrivilegioHelp = Convert.ToBoolean(element["PRIVIL_HELP"]);

                            IList informacionRolesBD = ServiceUsuarios.GetRolesUsuario(usuario.NombreUsuario);
                            
                            if (informacionRolesBD != null)
                            {
                                usuario.Roles = new List<Rol>();
                                foreach (var item in informacionRolesBD)
                                {
                                    System.Type tipo = item.GetType();
                                    Rol rol = new Rol();
                                    rol.idRol = (int)tipo.GetProperty("ID_ROL").GetValue(item, null);
                                    rol.NombreRol = (string)tipo.GetProperty("NOMBRE_ROL").GetValue(item, null);
                                    usuario.Roles.Add(rol);
                                }
                            }
                        }
                    }
                }

                //Retornamos el usuario.
                return usuario;
            });

        }

        /// <summary>
        /// Método que obtiene los roles de usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static IList GetRoles(string usuario)
        {
            SO_Usuarios ServiceUsuario = new SO_Usuarios();

            return ServiceUsuario.GetRolesUsuario(usuario);
        }

        /// <summary>
        /// Método que añade los privilegios de un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static int Set_PrivilegiosUsuario(Usuario usuario)
        {
            SO_Usuario ServiceUsuario = new SO_Usuario();

            return ServiceUsuario.Privilegio_Usuario(usuario.NombreUsuario, usuario.PerfilRGP, usuario.PerfilTooling, usuario.PerfilRawMaterial, usuario.PerfilStandarTime, usuario.PerfilQuotes, usuario.PerfilCIT, usuario.PerfilData, usuario.PerfilUserProfile, usuario.PerfilHelp);
        }

        /// <summary>
        /// Método que añade el perfil de cada usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static int Set_PerfilUsuario(Usuario usuario)
        {
            SO_Usuario ServiceUsuario = new SO_Usuario();

            return ServiceUsuario.Perfil_Usuario(usuario.NombreUsuario, usuario.PerfilRGP, usuario.PerfilTooling, usuario.PerfilRawMaterial, usuario.PerfilStandarTime, usuario.PerfilQuotes, usuario.PerfilCIT, usuario.PerfilData, usuario.PerfilUserProfile, usuario.PerfilHelp);
        }
        #endregion

        #region TipoPerfil

        /// <summary>
        /// Método el cual obtiene todos los tipos de perfil y lo retorna como una ObservableCollection
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<TipoPerfil> GetAllTipoPerfil()
        {
            //Inicializamos los servicios de SO_TipoPerfil.
            SO_TipoPerfil ServiceTipoPerfil = new SO_TipoPerfil();

            //Declaramos una colección observable la cual será la que retornemos en el método.
            ObservableCollection<TipoPerfil> ListaResultante = new ObservableCollection<TipoPerfil>();

            //Ejecutamos el método para obtener la información de tipo de perfiles, el resultado lo asignamos a una lista anónima.
            IList InformacionBD = ServiceTipoPerfil.GetAllTipoPerfil();

            //Verificamos que el resultado sea direfente de nulo.
            if (InformacionBD != null)
            {
                //Iteramos el resultado.
                foreach (var item in InformacionBD)
                {
                    //Obtenemos el tipo de cada item iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo TipoPerfil el cual será el que agregumos a la lista resultante.
                    TipoPerfil tipoperfil = new TipoPerfil();

                    //Mapeamos los valores en cada propiedad correspondiente.
                    tipoperfil.IdTipoPerfil = (int)tipo.GetProperty("ID_TIPO_PERFIL").GetValue(item, null);
                    tipoperfil.Perfil = (string)tipo.GetProperty("PERFIL").GetValue(item, null);

                    //Agregamos el objeto a la lista.
                    ListaResultante.Add(tipoperfil);
                }
            }

            //Retornamos la lista resultante.
            return ListaResultante;
        }

        #endregion

    }
}