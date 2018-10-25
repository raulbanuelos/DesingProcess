using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_Perfil
    {
        /// <summary>
        /// Método que retorna todos los registros de Perfiles
        /// </summary>
        /// <returns></returns>
        public IList GetAllPerfiles()
        {
            try
            {
                //Establecemos la conexión a la base de datos a través de Entity Framework.
                using (var Conexion = new EntitiesPerfiles())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var lista = (from a in Conexion.CAT_PERFIL
                                 select a).ToList();

                    //Retornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception er)
            {
                //Si se genero algún error, retornamos un null.
                return null;
            }
        }

        public IList GetAllPerfiles(string tipoPerfil)
        {
            try
            {
                //Establecemos la conexión a la base de datos a través de Entity Framework.
                using (var Conexion = new EntitiesPerfiles())
                {
                    //Realizamos la consulta, el resultado lo guardamos en una variable anónima.
                    var lista = (from a in Conexion.CAT_PERFIL
                                 join b in Conexion.CAT_TIPO_PERFIL on a.ID_TIPO_PERFIL equals b.ID_TIPO_PERFIL
                                 where b.PERFIL == tipoPerfil
                                 select new {
                                     a.ID_PERFIL,
                                     a.NOMBRE,
                                     a.DESCRIPCION,
                                     a.IMAGEN,
                                     b.PERFIL
                                 }).ToList();

                    //Retornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception er)
            {
                //Si se genero algún error, retornamos un null.
                return null;
            }
        }

        public Task<int> SetPerfil(int idTipoPerfil, string Nombre, string Descripcion, byte[] imagen, int idUsuarioCreacion)
        {
            return Task.Run(() =>
            {
                try
                {
                    DataSet datos = null;

                    Desing_SQL conexion = new Desing_SQL();

                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    parametros.Add("idTipoPerfil", idTipoPerfil);
                    parametros.Add("Nombre", Nombre);
                    parametros.Add("Descripcion", Descripcion);
                    parametros.Add("Imagen", imagen);
                    parametros.Add("idUsuarioCreacion", idUsuarioCreacion);

                    datos = conexion.EjecutarStoredProcedure("SP_RGP_INSERT_PERFIL", parametros);

                    //Retorna el número de elementos en la tabla.
                    return datos.Tables.Count;
                }
                catch (Exception)
                {
                    return 0;
                }
            });
        }
    }
}