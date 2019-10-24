using System;
using Encriptar;
using System.Collections;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.ServiceObjects.ControlDocumentos;
using System.Data;
using DataAccess.SQLServer;

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
                                               p.COMPONENTE.Contains(TextoBuscar) ||
                                               p.REPORTADO_POR.Contains(TextoBuscar)
                                               orderby p.FECHA_ACTUALIZACION descending
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
        /// Método que obtiene la lista de las lecciones aprendidas filtradas por texto y fecha.
        /// </summary>
        /// <param name="TextoBuscar"></param>
        /// <param name="fechaInicial"></param>
        /// <param name="fechaFinal"></param>
        /// <returns></returns>
        public IList GetLeccionesAprendidas(string TextoBuscar, DateTime fechaInicial, DateTime fechaFinal)
        {
            Encriptacion encript = new Encriptacion();
            TextoBuscar = string.IsNullOrEmpty(TextoBuscar) ? string.Empty : TextoBuscar;
            string user = encript.encript(TextoBuscar);
            try
            {
                //declaramos la conexion a la BD
                using (EntitiesUsuario conexion = new EntitiesUsuario())
                {
                    //Obtenemos todas las lecciones aprendidas registradas por un usuario
                    var LeccionesAprendidas = (from p in conexion.TBL_LECCIONES_APRENDIDAS
                                               where (p.ID_USUARIO.Contains(user) ||
                                               p.DESCRIPCION_PROBLEMA.Contains(TextoBuscar) ||
                                               p.COMPONENTE.Contains(TextoBuscar) ||
                                               p.REPORTADO_POR.Contains(TextoBuscar)) && 
                                               p.FECHA_ACTUALIZACION >= fechaInicial &&
                                               p.FECHA_ACTUALIZACION <= fechaFinal
                                               orderby p.FECHA_ACTUALIZACION descending
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
        /// Método que obtiene todos los datos de los componentes similares
        /// </summary>
        /// <param name="NombreComponente"></param>
        /// <returns></returns>
        public IList GetComponentesSimilares(string NombreComponente)
        {
            try
            {
                //Declaramos la conexíon a la BD
                using (EntitiesUsuario Conexion = new EntitiesUsuario())
                {
                    //Obtenemos todos los datos de los componentes similares
                    var ComponentesSimilares = (from a in Conexion.TBL_LECCIONES_APRENDIDAS
                                                where a.COMPONENTE.Contains(NombreComponente)
                                                select a).ToList();

                    //Retornamos la lista con todos los componentes similares
                    return ComponentesSimilares;
                }
            }
            catch (Exception)
            {
                //si hay error regresamos un valor nulo
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
        public int SetLeccion(string Componente, string DescripcionProblema, DateTime FechaUltimoCambio, DateTime FechaActualizacion,
            string ReportadoPor, string SolicitudTrabajoIngenieria, string IdUsuario)
        {
            try
            {
                //declaramos la conexion a la BD
                using (EntitiesUsuario conexion = new EntitiesUsuario())
                {
                    //creacion del objeto TBL_LECCIONES_APRENDIDAS
                    TBL_LECCIONES_APRENDIDAS obj = new TBL_LECCIONES_APRENDIDAS();

                    //insertamos los valores
                    obj.ID_USUARIO = IdUsuario;
                    obj.COMPONENTE = Componente;
                    obj.DESCRIPCION_PROBLEMA = DescripcionProblema;
                    obj.FECHA_ACTUALIZACION = FechaActualizacion;
                    obj.FECHA_ULTIMO_CAMBIO = FechaUltimoCambio;
                    obj.REPORTADO_POR = ReportadoPor;
                    obj.SOLICITUD_DE_TRABAJO_INGENIERIA = SolicitudTrabajoIngenieria;


                    //insertamos los valores de la nueva leccion aprendida a la BD
                    conexion.TBL_LECCIONES_APRENDIDAS.Add(obj);

                    conexion.SaveChanges();

                    //guaramos los cambios
                    return obj.ID_LECCIONES_APRENDIDAS;
                }
            }
            catch (Exception e)
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

                    //Obtenemos los centros de trabajo que contenga la leccion aprendida
                    var CentrosTrabajo = (from a in conexion.TR_LECCIONES_CENTROSTRABAJO
                                          where a.ID_LECCIONESAPRENDIDAS == id_leccion
                                          select a).ToList();

                    //Eliminamos cada centro de trabajo correspondiente
                    foreach (var item in CentrosTrabajo)
                    {
                        conexion.Entry(item).State = EntityState.Deleted;
                    }
                    //guardamos los cambios
                    conexion.SaveChanges();

                    //Obtenemos los niveles de cambio que contenga la leccion aprendida
                    var NivelesDeCambio = (from a in conexion.TR_LECCIONES_TIPOCAMBIO
                                             where a.ID_LECCIONAPRENDIDA == id_leccion
                                             select a).ToList();

                    //Eliminamos cada nivel de cambio
                    foreach (var item in NivelesDeCambio)
                    {
                        conexion.Entry(item).State = EntityState.Deleted;
                    }
                    //Guardamos cambio
                    conexion.SaveChanges();

                    //despues de haber eliminado cada archivo,centro de trabajo y nivel de cambio relacionados, borramos la leccion aprendida
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
                                 string nivel_de_cambio,
                                 string centro_de_trabajo,
                                 string operacion,
                                 string descripcion_problema,
                                 DateTime fecha_ultimo_cambio,
                                 DateTime fecha_actualizacion,
                                 string reportado_por,
                                 string solicitud_trabajo_ingenieria)
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
                    obj.DESCRIPCION_PROBLEMA = descripcion_problema;
                    obj.REPORTADO_POR = reportado_por;
                    obj.SOLICITUD_DE_TRABAJO_INGENIERIA = solicitud_trabajo_ingenieria ;
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

       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Componente"></param>
        /// <returns></returns>
        public IList FechaUltimoCamio(string Componente)
        {
            try
            {
                using (EntitiesUsuario conexion = new EntitiesUsuario())
                {
                    var FechaUltimoCambio = (from a in conexion.TBL_LECCIONES_APRENDIDAS
                                             where a.COMPONENTE == Componente orderby a.FECHA_ULTIMO_CAMBIO descending
                                             select a).ToList();

                    return FechaUltimoCambio;
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
        /// <param name="COMPONENTE"></param>
        /// <returns></returns>
        public DataSet GetUltimosCambiosComponentesSimilares(string COMPONENTE)
        {
            try
            {
                DataSet Lista = null;

                Desing_SQL conexion = new Desing_SQL();
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                parametros.Add("COMPONENTE", COMPONENTE);

                Lista = conexion.EjecutarStoredProcedure("SP_LA_GET_COMPONENTES_SIMILARES", parametros);

                return Lista;

            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion
    }
}