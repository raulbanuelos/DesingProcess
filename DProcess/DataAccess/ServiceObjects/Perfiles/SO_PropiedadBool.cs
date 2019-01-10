using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_PropiedadBool
    {
        #region CAT_PROPIEDAD_BOOL

        /// <summary>
        /// Método que obtiene todos los datos de la base de datos
        /// </summary>
        /// <returns></returns>
        public IList GetAllPropiedades()
        {
            try
            {
                //declaramos la conexion a la base de datos
                using (var Conexion = new EntitiesPerfiles() )
                {
                    //seleccionamos todos los registros de la base de datos
                    var lista = (from a in Conexion.CAT_PROPIEDAD_BOOL
                                 select a).ToList();

                    //regresamos la lista con los valores seleccionados
                    return lista;
                }
            }
            catch (Exception)
            {
                //si hay error regresa un valor nulo
                return null;
            }
        }

        /// <summary>
        /// Método para insertar una nueva propiedad bool a la base de datos
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="DescripcionLarga"></param>
        /// <param name="DescripcionCorta"></param>
        /// <param name="Imagen"></param>
        /// <param name="IdUsuarioCreacion"></param>
        /// <param name="FechaCreacion"></param>
        /// <param name="IdUsuarioActualizo"></param>
        /// <param name="FechaActualizacion"></param>
        /// <returns></returns>
        public int InsertNewPropiedad(string Nombre, string DescripcionLarga, string DescripcionCorta, byte[] Imagen)
        {
            try
            {
                //Declaramos la conexión a la base de datos
                using (var Conexion = new EntitiesPerfiles())
                {
                    CAT_PROPIEDAD_BOOL DatosPropiedad = new CAT_PROPIEDAD_BOOL();

                    //Asignamos los datos
                    DatosPropiedad.NOMBRE = Nombre;
                    DatosPropiedad.DESCRIPCION_LARGA = DescripcionLarga;
                    DatosPropiedad.DESCRIPCION_CORTA = DescripcionCorta;
                    DatosPropiedad.IMAGEN = Imagen;
                    DatosPropiedad.ID_USUARIO_CREACION = 1;
                    DatosPropiedad.FECHA_CREACION = DateTime.Now;
                    DatosPropiedad.ID_USUARIO_ACTUALIZACION = 1;
                    DatosPropiedad.FECHA_ACTUALIZACION = DateTime.Now;

                    //Insertamos los datos en la base de datos
                    Conexion.CAT_PROPIEDAD_BOOL.Add(DatosPropiedad);

                    //Guardamos cambios
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar los datos de una propiedad seleccionada
        /// </summary>
        /// <param name="idPropiedad"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcionCorta"></param>
        /// <param name="descripcionLarga"></param>
        /// <param name="imagen"></param>
        /// <returns></returns>
        public int UpdatePropiedad(int idPropiedad, string nombre, string descripcionCorta, string descripcionLarga, byte[] imagen)
        {
            try
            {
                using (var conexion = new EntitiesPerfiles())
                {
                    CAT_PROPIEDAD_BOOL data = conexion.CAT_PROPIEDAD_BOOL.Where(a => a.ID_PROPIEDAD_BOOL == idPropiedad).FirstOrDefault();

                    data.NOMBRE = nombre;
                    data.DESCRIPCION_CORTA = descripcionCorta;
                    data.DESCRIPCION_LARGA = descripcionLarga;
                    data.IMAGEN = imagen;

                    data.FECHA_ACTUALIZACION = DateTime.Now;
                    data.FECHA_CREACION = DateTime.Now;
                    data.ID_USUARIO_ACTUALIZACION = 1;
                    data.ID_USUARIO_CREACION = 1;

                    conexion.Entry(data).State = System.Data.Entity.EntityState.Modified;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro
        /// </summary>
        /// <param name="IdPropiedad"></param>
        /// <returns></returns>
        public int DeletePropiedad(int IdPropiedad)
        {
            try
            {
                using (var conexion = new EntitiesPerfiles())
                {
                    CAT_PROPIEDAD_BOOL data = conexion.CAT_PROPIEDAD_BOOL.Where(a => a.ID_PROPIEDAD_BOOL == IdPropiedad).FirstOrDefault();

                    conexion.Entry(data).State = System.Data.Entity.EntityState.Deleted;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene un registro a partir de un ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetPropiedadByID(int id)
        {
            try
            {
                //declaramos la conexion a la base de datos
                using (var Conexion = new EntitiesPerfiles())
                {
                    //seleccionamos todos los registros de la base de datos
                    var lista = (from a in Conexion.CAT_PROPIEDAD_BOOL
                                 where a.ID_PROPIEDAD_BOOL == id
                                 select a).ToList();

                    //regresamos la lista con los valores seleccionados
                    return lista;
                }
            }
            catch (Exception)
            {
                //si hay error regresa un valor nulo
                return null;
            }
        }

        #endregion

        #region TR_PROPIEDAD_BOOL_PERFIL

        /// <summary>
        /// Método que inserta un registro nuevo
        /// </summary>
        /// <param name="IdPropiedadBool"></param>
        /// <param name="IdPerfil"></param>
        /// <returns></returns>
        public int InsertNewPropiedadBoolPerfil(int IdPropiedadBool, int IdPerfil)
        {
            try
            {
                using (EntitiesPerfiles conexion = new EntitiesPerfiles())
                {
                    TR_PROPIEDAD_BOOL_PERFIL data = new TR_PROPIEDAD_BOOL_PERFIL();

                    data.ID_PROPIEDAD_BOOL = IdPropiedadBool;
                    data.ID_PERFIL = IdPerfil;

                    //Agregamos los registros a la base de datos
                    conexion.TR_PROPIEDAD_BOOL_PERFIL.Add(data);

                    //Guardamos los cambios
                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro
        /// </summary>
        /// <param name="IdPropiedadBoolPerfil"></param>
        /// <param name="IdPropiedadBool"></param>
        /// <returns></returns>
        public int DeletePropiedadBoolPerfil(int IdPropiedadBool, int IdPerfil)
        {
            try
            {
                using (EntitiesPerfiles conexion = new EntitiesPerfiles())
                {
                    TR_PROPIEDAD_BOOL_PERFIL data = conexion.TR_PROPIEDAD_BOOL_PERFIL.Where(a => a.ID_PROPIEDAD_BOOL == IdPropiedadBool && a.ID_PERFIL == IdPerfil).FirstOrDefault();

                    conexion.Entry(data).State = System.Data.Entity.EntityState.Deleted;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        #endregion
    }
}
