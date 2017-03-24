using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using DataAccess.ServiceObjects;
using DataAccess.ServiceObjects.Operaciones.Premaquinado;
using DataAccess.ServiceObjects.Herramentales;
using DataAccess.ServiceObjects.MateriasPrimas;

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
            SO_Material ServiceMaterial = new SO_Material("");

            DataSet InformacionBD = ServiceMaterial.GetTipoMaterial(EspecificacionMaterial);

            //Verificamos que el objeto recibido sea distinto de vacío.
            if (InformacionBD != null)
            {
                //Leer el dataset para obtener el registro.
            }

            return string.Empty;
        }
        #endregion

        #region Operaciones

        #region First Rough Grind

        /// <summary>
        /// Método que obtiene el valor del width de la operación.
        /// </summary>
        /// <param name="proceso">Proceso por el cual el usuario elige se va a procesar el anillo(Doble, Sencillo, Cuadruple).</param>
        /// <param name="H1">Double que representa el width nominal del anillo.</param>
        /// <returns>Double que representa el width de la operación.</returns>
        public static double GetWidthFirstRoughGrind(string proceso, double H1)
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
                    //Ejecutamos el método para obtener la información del herramental.
                    herramental = ReadInformacionHerramentalEncontrado(InformacionBD);

                    //Asignamos los valores restantes a las propiedades.
                    herramental.DescripcionRuta = "";
                }
            }
            else
            { //Si no se encontró el herramental.

            }

            return herramental;
        }
        #endregion

        #endregion

        #region Herramentales

        /// <summary>
        /// Método que obtiene todos los registros de la tabla ClasificacionHerramental.
        /// </summary>
        /// <returns>Lista observable con todos los registros, si se genera algún error retorna el objeto vacío.</returns>
        public static ObservableCollection<ClasificacionHerramental> GetClasificacionHerramental()
        {
            //Inicializamos los servicios de clasificación.
            SO_ClasificacionHerramental ServiceClasificacion = new SO_ClasificacionHerramental();

            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<ClasificacionHerramental> ListaResultante = new ObservableCollection<ClasificacionHerramental>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList InformacionBD = ServiceClasificacion.GetClasificacionHerramental();

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
        #endregion

        #region Métodos Generales

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
                        }
                    }
                }

                //Retornamos el usuario.
                return usuario;
            });

        }
        #endregion

        #region MateriasPrimas
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
            if (PatternBD!=null)
            {
                //Iteración de la información recibida.
                foreach (var item in PatternBD)
                {
                    //Se obtiene el tipo
                    System.Type tipo = item.GetType();

                    //Declaración del objeto de tipo Pattern que contendrá la información de un registro.
                    Pattern obj = new Pattern();

                    //Se asignan los valores.
                    obj.codigo = (string)tipo.GetProperty("codigo").GetValue(item, null);
                    obj.medida = (double)tipo.GetProperty("DIAMETRO").GetValue(item, null);
                    obj.diametro = (double)tipo.GetProperty("WIDTH").GetValue(item, null);
                    Cliente cliente = new Cliente();
                    cliente.IdCliente = (int)tipo.GetProperty("id_cliente").GetValue(item, null);
                    cliente.NombreCliente=(string)tipo.GetProperty("Cliente1").GetValue(item, null);
                    obj.customer = cliente;
                    obj.mounting=(string) tipo.GetProperty("MOUNTING").GetValue(item, null);
                    obj.on_14_rd_gate= (string)tipo.GetProperty("ON_14_RD_GATE").GetValue(item, null);
                    obj.button=(string) tipo.GetProperty("BUTTON").GetValue(item, null);
                    obj.cone= (string)tipo.GetProperty("CONE").GetValue(item, null);
                    obj.M_Circle=(string) tipo.GetProperty("M_CIRCLE").GetValue(item, null);
                    obj.ring_w_min= (double)tipo.GetProperty("RING_WTH_min").GetValue(item, null);
                    obj.ring_w_max= (double)tipo.GetProperty("RING_WTH_max").GetValue(item, null);
                    obj.date_ordered= (string)tipo.GetProperty("DATE_ORDERED").GetValue(item, null);
                    obj.B_Dia= (double)tipo.GetProperty("B_DIA").GetValue(item, null);
                    obj.fin_Dia=(double) tipo.GetProperty("FIN_DIA").GetValue(item, null);
                    obj.turn_allow=(double) tipo.GetProperty("TURN_ALLOW").GetValue(item, null);
                    obj.cstg_sm_od= (double) tipo.GetProperty("CSTG_SM_OD").GetValue(item, null);
                    obj.shrink_allow= (double) tipo.GetProperty("SHRINK_ALLOW").GetValue(item, null);
                    obj.patt_sm_od= (double) tipo.GetProperty("PATT_SM_OD").GetValue(item, null);
                    obj.piece_in_patt= (double) tipo.GetProperty("PIECE_IN_PATT").GetValue(item, null);
                    obj.bore_allow= (double) tipo.GetProperty("BORE_ALLOW").GetValue(item, null);
                    obj.patt_sm_id= (double) tipo.GetProperty("PATT_SM_ID").GetValue(item, null);
                    obj.patt_thickness= (double) tipo.GetProperty("PATT_THICKNESS").GetValue(item, null);
                    obj.joint= (string)tipo.GetProperty("JOINT").GetValue(item, null);
                    obj.nick= (string)tipo.GetProperty("NICK").GetValue(item, null);
                    obj.nick_draf= (string)tipo.GetProperty("NICK_DRAF").GetValue(item, null);
                    obj.nick_depth= (string)tipo.GetProperty("NICK_DEPTH").GetValue(item, null);
                    obj.side_relief= (string)tipo.GetProperty("SIDE_RELIEF").GetValue(item, null);
                    obj.cam= (double)tipo.GetProperty("CAM").GetValue(item, null);
                    obj.cam_roll= (double)tipo.GetProperty("CAM_ROLL").GetValue(item, null);
                    obj.rise= (double)tipo.GetProperty("RISE").GetValue(item, null);
                    obj.OD= (double)tipo.GetProperty("OD").GetValue(item, null);
                    obj.ID= (double)tipo.GetProperty("ID").GetValue(item, null);
                    obj.diff= (double)tipo.GetProperty("DIFF").GetValue(item, null);
                    obj.tipo= (int)tipo.GetProperty("id_tipo_mp").GetValue(item, null);
                    obj.mounted= (string)tipo.GetProperty("mounted").GetValue(item, null);
                    obj.ordered= (string)tipo.GetProperty("ordered").GetValue(item, null);
                    obj.Checked= (string)tipo.GetProperty("checked").GetValue(item, null);
                    obj.date_checked= (string)tipo.GetProperty("date_checked").GetValue(item, null);
                    obj.esp_inst= (string)tipo.GetProperty("esp_inst").GetValue(item, null);
                    obj.factor_k= (double)tipo.GetProperty("factor_k").GetValue(item, null);
                    obj.rise_built= (double)tipo.GetProperty("rise_built").GetValue(item, null);
                    obj.ring_th_min=(double)tipo.GetProperty("ring_th_min").GetValue(item, null);
                    obj.ring_th_min= (double)tipo.GetProperty("ring_th_max").GetValue(item, null);
                    obj.estado= (bool)tipo.GetProperty("estado").GetValue(item, null);
                    obj.plato= (double)tipo.GetProperty("Plato").GetValue(item, null);
                    obj.detalle= (string)tipo.GetProperty("Detalle").GetValue(item, null);
                    obj.diseno= (bool)tipo.GetProperty("Diseno").GetValue(item, null);

                    //Agregamos el objeto tipo Pattern a la lista.
                    Lista.Add(obj);
                }
            }
            //Devolvemos la lista 
            return Lista;

        }
      
        #endregion
    }
}
