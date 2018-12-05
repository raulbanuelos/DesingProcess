using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_PropiedadCadena
    {
        /// <summary>
        /// Método que muestra todos los registros de la tabla
        /// </summary>
        /// <returns></returns>
        public IList GetAllPropiedadesCadena()
        {
            try
            {
                using (EntitiesPerfiles conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in conexion.CAT_PROPIEDAD_CADENA
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
        /// Método que actualiza un registro seleccionado
        /// </summary>
        /// <param name="Nombre"></param>
        /// <param name="DescripcionLarga"></param>
        /// <param name="DescripcionCorta"></param>
        /// <param name="Imagen"></param>
        /// <returns></returns>
        public int InsertNewPropiedadCadena(string Nombre, string DescripcionLarga, string DescripcionCorta, byte[] Imagen)
        {
            try
            {
                using (EntitiesPerfiles conexion = new EntitiesPerfiles())
                {
                    CAT_PROPIEDAD_CADENA data = new CAT_PROPIEDAD_CADENA();

                    data.NOMBRE = Nombre;
                    data.DESCRIPCION_LARGA = DescripcionLarga;
                    data.DESCRIPCION_CORTA = DescripcionCorta;
                    data.IMAGEN = Imagen;

                    data.FECHA_ACTUALIZACION = DateTime.Now;
                    data.FECHA_CREACION = DateTime.Now;
                    data.ID_USUARIO_ACTUALIZACION = 1;
                    data.ID_USUARIO_CREACION = 1;

                    conexion.CAT_PROPIEDAD_CADENA.Add(data);

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método para actualizar la informacion de una propiedad
        /// </summary>
        /// <param name="IdPropiedadCadena"></param>
        /// <param name="Nombre"></param>
        /// <param name="DescripcionLarga"></param>
        /// <param name="DescripcionCorta"></param>
        /// <param name="Imagen"></param>
        /// <returns></returns>
        public int UpdatePropiedadCadena(int IdPropiedadCadena, string Nombre, string DescripcionLarga, string DescripcionCorta, byte[] Imagen)
        {
            try
            {
                using (EntitiesPerfiles conexion = new EntitiesPerfiles())
                {
                    CAT_PROPIEDAD_CADENA data = conexion.CAT_PROPIEDAD_CADENA.Where(a => a.ID_PROPIEDAD_CADENA == IdPropiedadCadena).FirstOrDefault();

                    data.NOMBRE = Nombre;
                    data.DESCRIPCION_LARGA = DescripcionLarga;
                    data.DESCRIPCION_CORTA = DescripcionCorta;
                    data.IMAGEN = Imagen;

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
        /// Método para eliminar un registro de la propiedad cadena
        /// </summary>
        /// <param name="IdPropiedadCadena"></param>
        /// <returns></returns>
        public int DeletePropiedadCadena(int IdPropiedadCadena)
        {
            try
            {
                using (EntitiesPerfiles conexion = new EntitiesPerfiles())
                {
                    CAT_PROPIEDAD_CADENA data = conexion.CAT_PROPIEDAD_CADENA.Where(a => a.ID_PROPIEDAD_CADENA == IdPropiedadCadena).FirstOrDefault();

                    conexion.Entry(data).State = System.Data.Entity.EntityState.Deleted;

                    return conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que obtiene un registro a partir del ID de la tabla CAT_PROPIEDAD_CADENA
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IList GetPropiedadCadenaByID(int id)
        {
            try
            {
                using (EntitiesPerfiles conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in conexion.CAT_PROPIEDAD_CADENA
                                 where a.ID_PROPIEDAD_CADENA == id
                                 select a).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
