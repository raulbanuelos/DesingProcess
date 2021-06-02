using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Usuario
{
    public class SO_Archivo_Lecciones
    {
        /// <summary>
        /// Método que devuelve todos los archivos de una leccion
        /// </summary>
        /// <returns></returns>
        public IList GetArchivoLecciones(int id_leccion)
        {
            try
            {
                using (var conexion = new EntitiesUsuario())
                {
                    var lista = (from a in conexion.TBL_ARCHIVO_LECCIONES
                                 join b in conexion.TBL_LECCIONES_APRENDIDAS on a.ID_LECCIONES_APRENDIDAS equals b.ID_LECCIONES_APRENDIDAS
                                 where a.ID_LECCIONES_APRENDIDAS == id_leccion
                                 select a).ToList();
                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList GetArchivoLecciones()
        {
            try
            {
                using (var conexion = new EntitiesUsuario())
                {
                    var lista = (from a in conexion.TBL_ARCHIVO_LECCIONES
                                 select a).ToList();
                    return lista;
                }
            }
            catch (Exception er)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que inserta un archivo a la base de datos
        /// se insertan mediante un procedimiento almacenado.
        /// </summary>
        /// <returns></returns>
        public int InsertarArchivoLecciones(byte[] archivo, string ext, string nombre,int id_leccion)
        {
            try
            {
                DataSet datos = null;
                //Se crea conexion a la BD.
                Desing_SQL conexion = new Desing_SQL();

                //Se inicializa un diccionario que contiene propiedades de tipo string y un objeto.
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                //se agregan el nombre y el objeto de los parámetros.
                parametros.Add("id_lecciones_aprendidas", id_leccion);
                parametros.Add("archivo", archivo);
                parametros.Add("ext", ext);
                parametros.Add("nombre", nombre);


                //se ejecuta el procedimiento y se mandan los parámetros añadidos anteriormente.
                datos = conexion.EjecutarStoredProcedure("SP_GRAL_Set_Archivo", parametros);

                //Retorna el número de elementos en la tabla.
                return 1;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un archivo
        /// </summary>
        /// <param name="id_archivo"></param>
        /// <returns></returns>
        public int DeleteArchivoLeccion(int id_leccion)
        {
            try
            {
                using (var conexion = new EntitiesUsuario())
                {
                    var ListaArchivos = (from a in conexion.TBL_ARCHIVO_LECCIONES
                                          where a.ID_LECCIONES_APRENDIDAS == id_leccion
                                          select a).ToList();

                    foreach (var item in ListaArchivos)
                    {
                        conexion.Entry(item).State = EntityState.Deleted;
                    }
                    conexion.SaveChanges();

                    return 1;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}
