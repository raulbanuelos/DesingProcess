using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Data;
using System.Threading.Tasks;
using DataAccess.ServiceObjects;
using DataAccess.ServiceObjects.Operaciones.Premaquinado;
using DataAccess.ServiceObjects.Herramentales;
using DataAccess.ServiceObjects.MateriasPrimas;
using DataAccess.ServiceObjects.Usuario;

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
                        }
                    }
                }

                //Retornamos el usuario.
                return usuario;
            });

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
        #endregion
    }
}
