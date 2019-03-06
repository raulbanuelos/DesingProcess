using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_Documento_Eliminado
    {
        /// <summary>
        /// Método que obtiene todos los registros de la tabla,o los filtra por numero de documento
        /// </summary>
        /// <param name="numeroBusqueda"></param>
        /// <returns></returns>
        public IList GetAllDocumento_Eliminado(string numeroBusqueda)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Ejecutamos el método 
                    var Lista = (from d in Conexion.TBL_DOCUMENTO_ELIMINADO
                                 where d.NUM_DOCUMENTO.Contains(numeroBusqueda)
                                 select new
                                 {
                                     d.ID_ELIMINADO,
                                     d.NUM_DOCUMENTO,
                                     d.NO_VERSION,
                                     d.FECHA_ELIMINO,
                                     d.ARCHIVO,
                                     d.EXT
                                 }).OrderBy(x => x.NUM_DOCUMENTO).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error, retornamos nulo
                return null;
            }
        }

        /// <summary>
        /// Método para insertar un registro en TBL_DOCUMENTO_ELIMINADO
        /// </summary>
        /// <param name="num_doc"></param>
        /// <param name="no_version"></param>
        /// <param name="fecha_eliminado"></param>
        /// <returns></returns>
        public int SetDocumento_Eliminado(string num_doc, string no_version, DateTime fecha_eliminado,byte[] archivo, string ext)
        {
            try
            {
                    DataSet datos = null;
                    //Se crea conexion a la BD.
                    Desing_SQL conexion = new Desing_SQL();

                    //Se inicializa un dictionario que contiene propiedades de tipo string y un objeto.
                    Dictionary<string, object> parametros = new Dictionary<string, object>();

                    //se agregan el nombre y el objeto de los parámetros.
                    parametros.Add("num_doc", num_doc);
                    parametros.Add("num_version", no_version);
                    parametros.Add("archivo", archivo);
                    parametros.Add("ext", ext);
                    parametros.Add("fecha_eliminado", fecha_eliminado);

                    //se ejecuta el procedimiento y se mandan los parámetros añadidos anteriormente.
                    datos = conexion.EjecutarStoredProcedure("SP_CIT_Set_DocumentoEliminado", parametros);

                    //Retorna el número de elementos en la tabla.
                    return datos.Tables.Count;
                               
            }
            catch (Exception er)
            {
                //si hay error, retorna cero
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene el archivo de un documento en especifico
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public IList GetArchivo(int id_documento)
        {
            try
            {
                using (var Conexion= new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y el resultado lo guardamos en una lista
                    var Lista = (from d in Conexion.TBL_DOCUMENTO_ELIMINADO
                                 where d.ID_ELIMINADO == id_documento
                                 select new
                                 {
                                     d.ARCHIVO,
                                     d.EXT,
                                     NOMBRE = d.NUM_DOCUMENTO+d.NO_VERSION,
                                     d.FECHA_ELIMINO
                                 }).ToList();
                    //retorna la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error, retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método que modifica el campo de archivo para liberar espacio en la memoria
        /// </summary>
        /// <param name="Id_Registro"></param>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public int UpdateDocumentoEliminado(int Id_Registro,byte[] archivo)
        {
            try
            {
                using (var conexion = new EntitiesControlDocumentos())
                {
                    TBL_DOCUMENTO_ELIMINADO obj = conexion.TBL_DOCUMENTO_ELIMINADO.Where(x => x.ID_ELIMINADO == Id_Registro).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    obj.ARCHIVO = archivo;

                    //Se cambia el estado de registro a modificado.
                    conexion.Entry(obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList GetDocumentoEliminar(string Nombre, string No_version)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Ejecutamos el método 
                    var Lista = (from d in Conexion.TBL_DOCUMENTO_ELIMINADO
                                 where d.NUM_DOCUMENTO.Equals(Nombre) && d.NO_VERSION.Equals(No_version)
                                 select new
                                 {
                                     d.ID_ELIMINADO,
                                     d.NUM_DOCUMENTO,
                                     d.NO_VERSION,
                                     d.FECHA_ELIMINO,
                                     d.ARCHIVO,
                                     d.EXT
                                 }).OrderBy(x => x.NUM_DOCUMENTO).ToList();
                    //Retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error, retornamos nulo
                return null;
            }
        }

    }
}
