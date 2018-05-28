using System;
using Encriptar;
using System.Collections;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.ServiceObjects.ControlDocumentos;

namespace DataAccess.ServiceObjects.Usuario
{
    public class SO_Lecciones
    {
        #region Métodos
        /// <summary>
        /// Método que obtiene la lista de las lecciones aprendidas
        /// </summary>
        /// <returns></returns>
        public IList GetLeccionesAprendidas(string TextoBuscar)
        {
            Encriptacion encript = new Encriptacion();
            string user = encript.encript(TextoBuscar);
            try
            {
                //declaramos la conexion a la BD
                using (EntitiesUsuario conexion = new EntitiesUsuario())
                {
                    //Obtenemos todas las lecciones aprendidas registradas por un usuario
                    var LeccionesAprendidas = (from p in conexion.TBL_LECCIONES_APRENDIDAS
                                               where p.ID_USUARIO.Contains(user) ||
                                               p.DESCRIPCION_PROBLEMA.Contains(TextoBuscar) ||
                                               p.COMPONENTE.Contains(TextoBuscar)||
                                               p.REPORTADO_POR.Contains(TextoBuscar)||
                                               p.CENTRO_DE_TRABAJO.Contains(TextoBuscar)

                                               select p).ToList();
                    //retornamos la lista
                    return LeccionesAprendidas;
                }
            }
            catch (Exception)
            {
                //si existe error retornamos nulo
                return null;
            }
        }

        /// <summary>
        /// Método que inserta una nueva leccion aprendida
        /// </summary>
        /// <param name="idleccion"></param>
        /// <param name="idusuario"></param>
        /// <param name="componente"></param>
        /// <param name="nivel_cambio"></param>
        /// <param name="c_trabajo"></param>
        /// <param name="operacion"></param>
        /// <param name="desc_probl"></param>
        /// <param name="reportado_por"></param>
        /// <param name="solicitud_Tingenieria"></param>
        /// <param name="criterio_1"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <returns></returns>
        public int SetLeccion(string idusuario, string componente,
            string nivel_cambio, string c_trabajo, string operacion,
            string desc_probl, DateTime fecha_ultimo_cambio , DateTime fecha_actualizacion, 
            string reportado_por, string solicitud_Tingenieria,
            string criterio_1, string cambio_requerido)
        {
            try
            {
                //declaramos la conexion a la BD
                using (EntitiesUsuario conexion = new EntitiesUsuario())
                {
                    //creacion del objeto TBL_LECCIONES_APRENDIDAS
                    TBL_LECCIONES_APRENDIDAS obj = new TBL_LECCIONES_APRENDIDAS();

                    //insertamos los valores
                    obj.ID_USUARIO = idusuario;
                    obj.COMPONENTE = componente;
                    obj.NIVEL_DE_CAMBIO = nivel_cambio;
                    obj.CENTRO_DE_TRABAJO = c_trabajo;
                    obj.OPERACION = operacion;
                    obj.DESCRIPCION_PROBLEMA = desc_probl;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    obj.FECHA_ULTIMO_CAMBIO = fecha_ultimo_cambio;
                    obj.REPORTADO_POR = reportado_por;
                    obj.SOLICITUD_DE_TRABAJO_INGENIERIA = solicitud_Tingenieria;
                    obj.CRITERIO_1 = criterio_1;
                    obj.CAMBIO_REQUERIDO = cambio_requerido;


                    //insertamos los valores de la nueva leccion aprendida a la BD
                    conexion.TBL_LECCIONES_APRENDIDAS.Add(obj);

                    conexion.SaveChanges();

                    //guaramos los cambios
                    return obj.ID_LECCIONES_APRENDIDAS;
                }
            }
            catch (Exception)
            {
                //si existe eroror regresemos 0
                return 0;
            }
        }

        /// <summary>
        /// Metodo que elimina una leccion aprendida con sus respectivos archivos.
        /// </summary>
        /// <returns></returns>
        public int DeleteLeccion(int id_leccion)
        {
            try
            {
                //declaramos la conexion a la BD
                using (EntitiesUsuario conexion = new EntitiesUsuario())
                {
                    //obtenemos los archivos que esten relacionados a la leccion aprendida que vamos a eliminar y tambien los eliminamos de la base de datos.
                    var archivos = (from a in conexion.TBL_ARCHIVO_LECCIONES
                                    where a.ID_LECCIONES_APRENDIDAS == id_leccion
                                    select a).ToList();

                    //recorremos la lista y eliminamos cada uno
                    foreach (var item in archivos)
                    {
                        conexion.Entry(item).State = EntityState.Deleted;
                    }
                    //guardamos los cambios.
                    conexion.SaveChanges();

                    //despues de haber eliminado cada archivo relacionado, borramos la leccion aprendida
                    TBL_LECCIONES_APRENDIDAS datos = (from o in conexion.TBL_LECCIONES_APRENDIDAS
                                 where o.ID_LECCIONES_APRENDIDAS == id_leccion
                                 select o).FirstOrDefault();

                    conexion.Entry(datos).State = EntityState.Deleted;
                    //guardamos los cambios
                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //si existe error regresamos 0
                return 0;
            }
        }

        /// <summary>
        /// Método que actualiza los datos de una leccion aprendida
        /// </summary>
        /// <returns></returns>
        public int UpdateLeccion(int id_leccion,
                                 string id_usuario,
                                 string componente,
                                 string cambio_requerido,
                                 string nivel_de_cambio,
                                 string centro_de_trabajo,
                                 string operacion,
                                 string descripcion_problema,
                                 DateTime fecha_ultimo_cambio,
                                 DateTime fecha_actualizacion,
                                 string reportado_por,
                                 string solicitud_trabajo_ingenieria,
                                 string criterio_1)
        {
            try
            {
                //declaramos la conexion
                using (EntitiesUsuario conexion = new EntitiesUsuario())
                {
                    //declaramos el objeto del tipo TBL_LECCIONES_APRENDIDAS
                    TBL_LECCIONES_APRENDIDAS obj = conexion.TBL_LECCIONES_APRENDIDAS.Where(x => x.ID_LECCIONES_APRENDIDAS == id_leccion).FirstOrDefault();

                    //insertamos los nuevos valores de los campos
                    obj.ID_USUARIO = id_usuario;
                    obj.COMPONENTE = componente;
                    obj.CAMBIO_REQUERIDO = cambio_requerido;
                    obj.NIVEL_DE_CAMBIO = nivel_de_cambio;
                    obj.CENTRO_DE_TRABAJO = centro_de_trabajo;
                    obj.OPERACION = operacion;
                    obj.DESCRIPCION_PROBLEMA = descripcion_problema;
                    obj.REPORTADO_POR = reportado_por;
                    obj.SOLICITUD_DE_TRABAJO_INGENIERIA = solicitud_trabajo_ingenieria ;
                    obj.CRITERIO_1 = criterio_1;
                    obj.FECHA_ULTIMO_CAMBIO = fecha_ultimo_cambio ;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;

                    //modificamos los campos
                    conexion.Entry(obj).State = EntityState.Modified;

                    //guardamos cambios
                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //si existe error regresamos 0
                return 0;
            }
        }
        #endregion
    }
}
