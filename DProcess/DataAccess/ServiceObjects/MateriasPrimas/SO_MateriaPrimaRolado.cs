using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    public class SO_MateriaPrimaRolado
    {

        #region Attributes
        private string SP_RGP_GET_MATERIA_PRIMA_ROLADO = "SP_RGP_GET_MATERIA_PRIMA_ROLADO"; 
        #endregion

        #region Propiedades
        //Cadena de conexión
        string StringDeConexion = string.Empty;
        #endregion

        #region Constructors
        public SO_MateriaPrimaRolado()
        {
            //Obtenemos la cadena de conexión.
            StringDeConexion = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"];
        } 
        #endregion

        public IList GetAll(string busqueda)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    var lista = (from a in Conexion.CAT_MATERIA_PRIMA_ROLADO
                                 where a.ID_MATERIA_PRIMA_ROLADO.Contains(busqueda) || a.ID_ESPECIFICACION.Contains(busqueda) || a.DESCRIPCION.Contains(busqueda)
                                 select a).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="widthCalculado">Media del Width del anillo menos(-) cantidad de material a agregar durante el proceso. Inch</param>
        /// <param name="especMaterial"></param>
        /// <param name="especPerfil"></param>
        /// <returns></returns>
        public DataSet GetMateriaPrimaRoladoIdeal(double widthCalculado, double thicknessCalculado,string especMaterial, string especPerfil, bool banThickness, double thicknessMin, double thicknessMax)
        {
            DataSet informacionBD = new DataSet();
            try
            {
                if (StringDeConexion != string.Empty)
                {
                    Desing_SQL ConexionSQL = new Desing_SQL();

                    //Declaramos un diccionario para guardar los parámetros necesarios del procedimiento.
                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    //Agregamos los paramentros al diccionario.
                    parametros.Add("withCalculado", widthCalculado);
                    parametros.Add("thicknessCalculado", thicknessCalculado);
                    parametros.Add("especPerfil", especPerfil);
                    parametros.Add("especMaterial", especMaterial);
                    parametros.Add("banThickness", banThickness);
                    parametros.Add("a1Min", thicknessMin);
                    parametros.Add("a1Max", thicknessMax);
                    
                    informacionBD = ConexionSQL.EjecutarStoredProcedure(SP_RGP_GET_MATERIA_PRIMA_ROLADO, parametros);

                }
            }
            catch (Exception er)
            {
                return informacionBD;
            }

            return informacionBD;
        }

        public int Insert(string codigoMateriaPrima, string especificacion, double thickness, double groove,string unidadMedida, double _width,string descripcion, string ubicacion, string Especificacion_Perfil)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    CAT_MATERIA_PRIMA_ROLADO materiaPrima = new CAT_MATERIA_PRIMA_ROLADO();

                    materiaPrima.ID_MATERIA_PRIMA_ROLADO = codigoMateriaPrima;
                    materiaPrima.ID_ESPECIFICACION = especificacion;
                    materiaPrima.THICKNESS = thickness;
                    materiaPrima.UM = unidadMedida;
                    materiaPrima.WIDTH = _width;
                    materiaPrima.GROOVE = groove;
                    materiaPrima.DESCRIPCION = descripcion;
                    materiaPrima.UBICACION = ubicacion;
                    materiaPrima.ESPEC_PERFIL = Especificacion_Perfil;

                    Conexion.CAT_MATERIA_PRIMA_ROLADO.Add(materiaPrima);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Update(string codigoMateriaPrima, string especificacion, double thickness, double groove, string unidadMedida, double _width, string descripcion,string ubicacion)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    CAT_MATERIA_PRIMA_ROLADO materiaPrima = Conexion.CAT_MATERIA_PRIMA_ROLADO.Where(x => x.ID_MATERIA_PRIMA_ROLADO == codigoMateriaPrima).FirstOrDefault();

                    materiaPrima.ID_ESPECIFICACION = especificacion;
                    materiaPrima.THICKNESS = thickness;
                    materiaPrima.GROOVE = groove;
                    materiaPrima.UM = unidadMedida;
                    materiaPrima.WIDTH = _width;
                    materiaPrima.DESCRIPCION = descripcion;
                    materiaPrima.UBICACION = ubicacion;

                    Conexion.Entry(materiaPrima).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Delete(string codigoMateriaPrima)
        {
            try
            {
                using (var Conexion = new EntitiesMateriaPrima())
                {
                    CAT_MATERIA_PRIMA_ROLADO materiaPrima = Conexion.CAT_MATERIA_PRIMA_ROLADO.Where(x => x.ID_MATERIA_PRIMA_ROLADO == codigoMateriaPrima).FirstOrDefault();

                    Conexion.Entry(materiaPrima).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
