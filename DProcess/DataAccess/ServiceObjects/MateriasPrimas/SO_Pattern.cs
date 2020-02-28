using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_Pattern
    {
        #region Attributes
        private string SP_RGP_GET_PESO_ALEANTES = "SP_RGP_GET_PESO_ALEANTES";
        private string SP_RGP_GET_TURN_BORE_ALLOW = "SP_RGP_GET_TURN_BORE_ALLOW";
        #endregion

        #region Propiedades
        #endregion

        #region Constructores
        #endregion

        #region Métodos

        /// <summary>
        /// Método con el cual se obtienen  todas las placas modelo del sistema.
        /// </summary>
        /// <returns></returns>
        public IList GetAllPattern(string busqueda)
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta.
                    var Lista = (from p in Conexion.Pattern2
                                 join c in Conexion.Cliente on p.CUSTOMER equals c.id_cliente
                                 join t in Conexion.Tipo_Materia_Prima on p.TIPO equals t.id_tipo_mp
                                 where p.codigo.Contains(busqueda)
                                 select new {
                                     p.codigo,
                                     DIAMETRO = p.MEDIDA,
                                     WIDTH = p.DIAMETRO,
                                     c.Cliente1,c.id_cliente, p.MOUNTING, p.ON_14_RD_GATE, p.BUTTON, p.CONE,
                                     p.M_CIRCLE, p.RING_WTH_min, p.RING_WTH_max, p.DATE_ORDERED,
                                     p.B_DIA, p.FIN_DIA, p.TURN_ALLOW, p.CSTG_SM_OD, p.SHRINK_ALLOW, p.PATT_SM_OD, p.PIECE_IN_PATT, p.BORE_ALLOW,
                                     p.PATT_SM_ID, p.PATT_THICKNESS, p.JOINT, p.NICK, p.NICK_DRAF, p.NICK_DEPTH, p.SIDE_RELIEF, p.CAM, p.CAM_ROLL,
                                     p.RISE, p.OD, p.ID, p.DIFF, TIPO = t.materia_prima,t.id_tipo_mp, p.mounted, p.ordered, p.@checked, p.date_checked, p.esp_inst, p.factor_k, p.rise_built, p.ring_th_min, p.ring_th_max,
                                     p.estado, p.Plato, p.Detalle, p.Diseno
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Registrar el error.

                //Si se generó algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        ///Método para insertar materias primas en la tabla Pattern2.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="medida"></param>
        /// <param name="diametro"></param>
        /// <param name="customer"></param>
        /// <param name="mounting"></param>
        /// <param name="on_14_rd_gate"></param>
        /// <param name="button"></param>
        /// <param name="cone"></param>
        /// <param name="M_circle"></param>
        /// <param name="ring_w_min"></param>
        /// <param name="ring_w_max"></param>
        /// <param name="date_ordered"></param>
        /// <param name="B_dia"></param>
        /// <param name="fin_dia"></param>
        /// <param name="turn_allow"></param>
        /// <param name="cstg_sm_od"></param>
        /// <param name="shrink_allow"></param>
        /// <param name="patt_sm_od"></param>
        /// <param name="piece_in_patt"></param>
        /// <param name="bore_allow"></param>
        /// <param name="patt_sm_id"></param>
        /// <param name="patt_thickness"></param>
        /// <param name="joint"></param>
        /// <param name="nick"></param>
        /// <param name="nick_draf"></param>
        /// <param name="nick_depth"></param>
        /// <param name="side_relief"></param>
        /// <param name="cam"></param>
        /// <param name="cam_roll"></param>
        /// <param name="rise"></param>
        /// <param name="OD"></param>
        /// <param name="ID"></param>
        /// <param name="DIFF"></param>
        /// <param name="tipo"></param>
        /// <param name="mounted"></param>
        /// <param name="ordered"></param>
        /// <param name="Checked"></param>
        /// <param name="date_checked"></param>
        /// <param name="esp_inst"></param>
        /// <param name="factor_k"></param>
        /// <param name="rise_built"></param>
        /// <param name="ring_th_min"></param>
        /// <param name="ring_th_max"></param>
        /// <param name="estado"></param>
        /// <param name="plato"></param>
        /// <param name="detalle"></param>
        /// <param name="diseno"></param>
        /// <returns>Retorna una cadena vacía si hay algún error.</returns>
        public string SetPattern(string codigo, double medida, double diametro, int customer, string mounting, string on_14_rd_gate, string button, string cone,
                                string M_circle, double ring_w_min, double ring_w_max, string date_ordered, double B_dia, double fin_dia, double turn_allow,
                                double cstg_sm_od, double shrink_allow, double patt_sm_od, double piece_in_patt, double bore_allow, double patt_sm_id,
                                double patt_thickness, string joint, string nick, string nick_draf, string nick_depth, string side_relief, double cam, double cam_roll,
                                double rise, double OD, double ID, double DIFF, int tipo, string mounted, string ordered, string Checked, string date_checked,
                                string esp_inst, double factor_k, double rise_built, double ring_th_min, double ring_th_max, bool estado, double plato, string detalle, bool diseno)

        {
            try
            {   //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesMateriaPrima())
                {

                    //Se crea un objeto de tipo Pattern2, el cual será insertado.
                    Pattern2 obj = new Pattern2();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.

                    
                    obj.codigo = codigo;
                    obj.MEDIDA = medida;
                    obj.DIAMETRO = diametro;
                    obj.CUSTOMER = customer;
                    obj.MOUNTING = mounting;
                    obj.ON_14_RD_GATE = on_14_rd_gate;
                    obj.BUTTON = button;
                    obj.CONE = cone;
                    obj.M_CIRCLE = M_circle;
                    obj.RING_WTH_min = ring_w_min;
                    obj.RING_WTH_max = ring_w_max;
                    obj.DATE_ORDERED = date_ordered;
                    obj.B_DIA = B_dia;
                    obj.FIN_DIA = fin_dia;
                    obj.TURN_ALLOW = turn_allow;
                    obj.CSTG_SM_OD = cstg_sm_od;
                    obj.SHRINK_ALLOW = shrink_allow;
                    obj.PATT_SM_OD = patt_sm_od;
                    obj.PIECE_IN_PATT = piece_in_patt;
                    obj.BORE_ALLOW = bore_allow;
                    obj.PATT_SM_ID = patt_sm_id;
                    obj.PATT_THICKNESS = patt_thickness;
                    obj.JOINT = joint;
                    obj.NICK = nick;
                    obj.NICK_DRAF = nick_draf;
                    obj.NICK_DEPTH = nick_depth;
                    obj.SIDE_RELIEF = side_relief;
                    obj.CAM = cam;
                    obj.CAM_ROLL = cam_roll;
                    obj.RISE = rise;
                    obj.OD = OD;
                    obj.ID = ID;
                    obj.DIFF = DIFF;
                    obj.TIPO = tipo;
                    obj.mounted = mounted;
                    obj.ordered = ordered;
                    obj.@checked = Checked;
                    obj.date_checked = date_checked;
                    obj.esp_inst = esp_inst;
                    obj.factor_k = factor_k;
                    obj.rise_built = rise_built;
                    obj.ring_th_min = ring_th_min;
                    obj.ring_th_max = ring_th_max;
                    obj.estado = estado;
                    obj.Plato = plato;
                    obj.Detalle = detalle;
                    obj.Diseno = diseno;

                    //obj.Tipo_Materia_Prima = new Tipo_Materia_Prima { id_tipo_mp = tipo, materia_prima = "GASOLINA" };
                    
                    //Insertamos el objeto a la base de datos.
                    Conexion.Pattern2.Add(obj);

                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //se retorna el código. 
                    return obj.codigo;

                }
            }
            catch (Exception er)
            {
                //Si se encuentra un error se retorna 0.
                return string.Empty;
            }
        }

        /// <summary>
        ///  Método que actualiza los valores de un registro en la tabla Pattern2.
        ///retorna cero si existe se ecnuentra algún error.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="medida"></param>
        /// <param name="diametro"></param>
        /// <param name="customer"></param>
        /// <param name="mounting"></param>
        /// <param name="on_14_rd_gate"></param>
        /// <param name="button"></param>
        /// <param name="cone"></param>
        /// <param name="M_circle"></param>
        /// <param name="ring_w_min"></param>
        /// <param name="ring_w_max"></param>
        /// <param name="date_ordered"></param>
        /// <param name="B_dia"></param>
        /// <param name="fin_dia"></param>
        /// <param name="turn_allow"></param>
        /// <param name="cstg_sm_od"></param>
        /// <param name="shrink_allow"></param>
        /// <param name="patt_sm_od"></param>
        /// <param name="piece_in_patt"></param>
        /// <param name="bore_allow"></param>
        /// <param name="patt_sm_id"></param>
        /// <param name="patt_thickness"></param>
        /// <param name="joint"></param>
        /// <param name="nick"></param>
        /// <param name="nick_draf"></param>
        /// <param name="nick_depth"></param>
        /// <param name="side_relief"></param>
        /// <param name="cam"></param>
        /// <param name="cam_roll"></param>
        /// <param name="rise"></param>
        /// <param name="OD"></param>
        /// <param name="ID"></param>
        /// <param name="DIFF"></param>
        /// <param name="tipo"></param>
        /// <param name="mounted"></param>
        /// <param name="ordered"></param>
        /// <param name="Checked"></param>
        /// <param name="date_checked"></param>
        /// <param name="esp_inst"></param>
        /// <param name="factor_k"></param>
        /// <param name="rise_built"></param>
        /// <param name="ring_th_min"></param>
        /// <param name="ring_th_max"></param>
        /// <param name="estado"></param>
        /// <param name="plato"></param>
        /// <param name="detalle"></param>
        /// <param name="diseno"></param>
        /// <returns></returns>
        public int UpdatePattern(string codigo, double medida, double diametro, int customer, string mounting, string on_14_rd_gate, string button, string cone,
                                string M_circle, double ring_w_min, double ring_w_max, string date_ordered, double B_dia, double fin_dia, double turn_allow,
                                double cstg_sm_od, double shrink_allow, double patt_sm_od, double piece_in_patt, double bore_allow, double patt_sm_id,
                                double patt_thickness, string joint, string nick, string nick_draf, string nick_depth, string side_relief, double cam, double cam_roll,
                                double rise, double OD, double ID, double DIFF, int tipo, string mounted, string ordered, string Checked, string date_checked,
                                string esp_inst, double factor_k, double rise_built, double ring_th_min, double ring_th_max, bool estado, double plato, string detalle, bool diseno)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //creación del objeto tipo Pattern2.
                    Pattern2 pattern = Conexion.Pattern2.Where(x => x.codigo == codigo).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    pattern.MEDIDA = medida;
                    pattern.DIAMETRO = diametro;
                    pattern.CUSTOMER = customer;
                    pattern.MOUNTING = mounting;
                    pattern.ON_14_RD_GATE = on_14_rd_gate;
                    pattern.BUTTON = button;
                    pattern.CONE = cone;
                    pattern.M_CIRCLE = M_circle;
                    pattern.RING_WTH_min = ring_w_min;
                    pattern.RING_WTH_max = ring_w_max;
                    pattern.DATE_ORDERED = date_ordered;
                    pattern.B_DIA = B_dia;
                    pattern.FIN_DIA = fin_dia;
                    pattern.TURN_ALLOW = turn_allow;
                    pattern.CSTG_SM_OD = cstg_sm_od;
                    pattern.SHRINK_ALLOW = shrink_allow;
                    pattern.PATT_SM_OD = patt_sm_od;
                    pattern.PIECE_IN_PATT = piece_in_patt;
                    pattern.BORE_ALLOW = bore_allow;
                    pattern.PATT_SM_ID = patt_sm_id;
                    pattern.PATT_THICKNESS = patt_thickness;
                    pattern.JOINT = joint;
                    pattern.NICK = nick;
                    pattern.NICK_DRAF = nick_draf;
                    pattern.NICK_DEPTH = nick_depth;
                    pattern.SIDE_RELIEF = side_relief;
                    pattern.CAM = cam;
                    pattern.CAM_ROLL = cam_roll;
                    pattern.RISE = rise;
                    pattern.OD = OD;
                    pattern.ID = ID;
                    pattern.DIFF = DIFF;
                    pattern.TIPO = tipo;
                    pattern.mounted = mounted;
                    pattern.ordered = ordered;
                    pattern.@checked = Checked;
                    pattern.date_checked = date_checked;
                    pattern.esp_inst = esp_inst;
                    pattern.factor_k = factor_k;
                    pattern.rise_built = rise_built;
                    pattern.ring_th_min = ring_th_min;
                    pattern.ring_th_max = ring_th_max;
                    pattern.estado = estado;
                    pattern.Plato = plato;
                    pattern.Detalle = detalle;
                    pattern.Diseno = diseno;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(pattern).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si encuentra error devuelve cero.
                return 0;
            }
        }

        /// <summary>
        ///  Método que elimina un registro de la tabla Pattern2
        /// </summary>
        /// <param name="codigo">Código de Pattern a eliminar</param>
        /// <returns></returns>
        public int DeletePattern(string codigo)
        {
            try
            {
                //Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    Pattern2 pattern = Conexion.Pattern2.Where(x => x.codigo == codigo).FirstOrDefault();

                    //Se estable el estado de registro a eliminado.
                    Conexion.Entry(pattern).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();

              
                }
            }
            catch (Exception)
            {
                //Si hay error, se regresa 0.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene el último código de Pattern
        /// </summary>
        /// <returns>Cadena que representa el último código agregado, si la tabla esta vacía el primer código es: BC-00001, si hubo algún error el método retorna una cadena vacía.</returns>
        public string GetLastCode()
        {
            //Declaramos una variable, que retornara el último código agregado
            string LastCod;

            try
            {
                //Se establece la conexión a la BD.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Se ordena de mayor a menor el código para obtener el primer valor,
                    //en este caso es el último código agregado a la tabla.
                    var last = (from p in Conexion.Pattern2
                                orderby p.codigo descending
                                select p.codigo).First();

                    //Asignamos el resultado obtenido a la variable local.
                    LastCod = last;

                    //Verificamos que no sea vacío.
                    if (string.IsNullOrEmpty(LastCod))
                        LastCod = "BC-00001";
                }
            }
            catch (Exception)
            {
                //Si hubo algún error retornamos una cadena vacía.
                return string.Empty;
            }
            //Retornamos el valor.
            return LastCod;

        }

        /// <summary>
        /// Método que obtiene la lista de placas modelos probables a partir de la dimención diámetro.
        /// </summary>
        /// <param name="diameter"></param>
        /// <returns></returns>
        public IList GetProbablyPattern(double diameter)
        {
            try
            {
                //Establecemos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.Pattern2
                                 where a.MEDIDA <= diameter + 0.020 && a.MEDIDA >= diameter - 0.020
                                 select new
                                 {
                                     Codigo = a.codigo
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método el cual retorna la información de la placa modelo a partir del código de la placa modelo.
        /// </summary>
        /// <param name="codigoPlacaModelo"></param>
        /// <returns></returns>
        public Pattern2 GetPattern(string codigoPlacaModelo)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta y el resultado lo asignamos en una variable anónima.
                    var pattern = (from p in Conexion.Pattern2
                                 where p.codigo == codigoPlacaModelo
                                 select p).FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return pattern;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el width ideal para la placa modelo.
        /// </summary>
        /// <param name="size_w"></param>
        /// <param name="proceso"></param>
        /// <returns></returns>
        public IList GetIdealWidthPlacaModelo(double size_w,string proceso)
        {
            try
            {
                //Realizamos la conexión a través de EntityFramework.
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var Lista = (from a in Conexion.castings_widths
                                 where a.Nominal_Ring_Width_min <= size_w && a.Nominal_Ring_Width_max >= size_w && a.tipo == proceso
                                 select new
                                 {
                                     a.Minimum_casting_width,
                                     a.ideal_casting_Width
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }

        }

        /// <summary>
        /// Método que ejecuta el procedimiento [SP_RGP_GET_TURN_BORE_ALLOW].
        /// </summary>
        /// <param name="tipo"></param>
        /// <param name="material"></param>
        /// <returns></returns>
        public DataSet Get_TurnBoreAllow(string tipo, string material)
        {
            DataSet datos = new DataSet();
            try
            {
                //Se crea conexion a la BD.
                Desing_SQL conexion = new Desing_SQL();

                //Se inicializa un dictionario que contiene propiedades de tipo string y un objeto.
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                //Agregamos los parámetos.
                parametros.Add("tipo", tipo);
                parametros.Add("material", material);

                //se ejecuta el procedimiento y se mandan los parámetros añadidos anteriormente.
                datos = conexion.EjecutarStoredProcedure(SP_RGP_GET_TURN_BORE_ALLOW, parametros);
            }
            catch (Exception)
            {
                //Si hay error, retorna la tabla vacía.
                return datos;
            }
            //Retorna la tabla.
            return datos;
        }

        /// <summary>
        /// Método que obtiene la suma de los pesos de una determinada especificación de materia prima.
        /// </summary>
        /// <param name="especMaterial"></param>
        /// <returns></returns>
        public DataSet GetPesoAleantes(string especMaterial)
        {
            DataSet datos = new DataSet();
            try
            {
                //Establecemos la conexión a la base de datos.
                Desing_SQL conexion = new Desing_SQL();

                //Declaramos un diccionario el cual contrendrá los parámetros que recibe el procedimiento alamcenado.
                Dictionary<string, object> paramentros = new Dictionary<string, object>();

                //Asignamos los parámetros con su valor.
                paramentros.Add("material", especMaterial);

                //Ejecutamos el procedimiento enviando los parámetros, el resultado lo asignamos a un conjunto de datos.
                datos = conexion.EjecutarStoredProcedure(SP_RGP_GET_PESO_ALEANTES, paramentros);

                //Retornamos el resultado de la consulta.
                return datos;
            }
            catch (Exception)
            {
                //Si ocurre algún error, retornamos un nulo.
                return null;
            }
        }

        #endregion

    }
}
