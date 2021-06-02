using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_HistorialVersion
    {
        /// <summary>
        /// Método que inserta un registro en la tabla de historial versión.
        /// </summary>
        /// <param name="idVersion"></param>
        /// <param name="fecha"></param>
        /// <param name="descripcion"></param>
        /// <param name="nombreUsuario"></param>
        /// <param name="nombreDocumento"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public int Insert(int idVersion, DateTime fecha, string descripcion, string nombreUsuario,string nombreDocumento, string version)
        {
            try
            {
                //Creamos la conexion
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Creamos un objeto el cual será el que insertemos.
                    TBL_HISTORIAL_VERSION registro = new TBL_HISTORIAL_VERSION();

                    //Mapeamos los valores a las propiedades correspondientes.
                    registro.NOMBRE_DOCUMENTO = nombreDocumento;
                    registro.NO_VERSION = version;
                    registro.FECHA = fecha;
                    registro.DESCRIPCION = descripcion;
                    registro.NOMBRE_USUARIO = nombreUsuario;

                    //Agregamos el registro.
                    Conexion.TBL_HISTORIAL_VERSION.Add(registro);

                    //Guardamos los cambios y retornamos el resultado.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si ocurre un error retornamos un cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todo los registro de tbl_hisotial, los filtra por nombre
        /// </summary>
        /// <param name="nombre_busqueda"></param>
        /// <returns></returns>
        public IList GetHistorial(string nombre_busqueda)
        {
            try
            {
                //Creamos la conexion
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //si el campo a buscar es nulo
                    if (string.IsNullOrEmpty(nombre_busqueda))
                    {
                        //Se obtiene todos los registros de la tabla
                        var Lista = (from h in Conexion.TBL_HISTORIAL_VERSION
                                     select new
                                     {
                                         // h.ID_HISTORIAL_VERSION,
                                         h.NOMBRE_DOCUMENTO,
                                         h.NO_VERSION
                                     }).OrderBy(x => x.NOMBRE_DOCUMENTO).Distinct().ToList();

                        return Lista;
                    }
                    else
                    {
                        //Si el campo no esta vacio, se busca la coicidencia con el nombre de documento
                        var Lista = (from h in Conexion.TBL_HISTORIAL_VERSION
                                     where h.NOMBRE_DOCUMENTO.Contains(nombre_busqueda)
                                     select new
                                     {
                                         h.NOMBRE_DOCUMENTO,
                                         h.NO_VERSION
                                     }).OrderBy(x => x.NOMBRE_DOCUMENTO).Distinct().ToList();
                        //Retornamos la lista
                        return Lista;
                    }
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el historial de la version de un documento
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="no_version"></param>
        /// <returns></returns>
        public IList GetHistorial_version(string nombre, string no_version)
        {
            try
            {
                //establecemos la conexion
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Ejecutamos el metodo que obtiene el historial de la version
                    var Lista = (from h in Conexion.TBL_HISTORIAL_VERSION
                                 where h.NOMBRE_DOCUMENTO.Equals(nombre) && h.NO_VERSION.Equals(no_version)
                                 select new
                                 {
                                     h.ID_HISTORIAL_VERSION,
                                     h.FECHA,
                                     h.DESCRIPCION,
                                     h.NOMBRE_USUARIO
                                 }).OrderBy(x => x.ID_HISTORIAL_VERSION).ToList();
                    //Retornamos la lita
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error, retornamos nulo
                return null;
            }
        }
    }
}
