using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_PropiedadOpcional
    {
        /// <summary>
        /// Método que obtiene las propiedades que tiene asignado un perfil.
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <returns></returns>
        public IList GetPropiedadesByPerfil(int idPerfil)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.CAT_PROPIEDAD_OPCIONAL
                                 join b in Conexion.TR_PROPIEDAD_OPCIONAL_PERFIL on a.ID_PROPIEDAD_OPCIONAL equals b.ID_PROPIEDAD_OPCIONAL
                                 join c in Conexion.CAT_PERFIL on b.ID_PERFIL equals c.ID_PERFIL
                                 where c.ID_PERFIL == idPerfil
                                 select new
                                 {
                                     a.ID_PROPIEDAD_OPCIONAL,
                                     a.NOMBRE,
                                     a.DESCRIPCION_LARGA,
                                     a.DESCRIPCION_CORTA,
                                     a.IMAGEN,
                                     a.SOURCE
                                 }).ToList();

                    return lista;

                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todas las opciones que tiene una propiedad opcional.
        /// </summary>
        /// <param name="idPropiedadOpcional"></param>
        /// <returns></returns>
        public IList GetOpcionesByIdPropiedadOpcional(int idPropiedadOpcional)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.CAT_OPCION_PROPIEDAD_OPCIONAL
                                 join b in Conexion.CAT_PROPIEDAD_OPCIONAL on a.ID_PROPIEDAD_OPCIONAL equals b.ID_PROPIEDAD_OPCIONAL
                                 where b.ID_PROPIEDAD_OPCIONAL == idPropiedadOpcional
                                 select new
                                 {
                                     a.ID_OPCION_PROPIEDAD_OPCIONAL,
                                     a.VALOR
                                 }).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Metodo que obtiene la información de la tabla la cual contiene la lista de opciones de una propiedad opcional.
        /// </summary>
        /// <param name="idPropiedadOpcional"></param>
        /// <returns></returns>
        public IList GetTablaSourcePropiedadOpcional(int idPropiedadOpcional)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.CAT_TABLA_PROPIEDAD_OPCIONAL
                                 join b in Conexion.CAT_PROPIEDAD_OPCIONAL on a.ID_PROPIEDAD_OPCIONAL equals b.ID_PROPIEDAD_OPCIONAL
                                 where a.ID_PROPIEDAD_OPCIONAL == idPropiedadOpcional
                                 select new
                                 {
                                     a.NOMBRE_TABLA,
                                     a.CAMPO_ID,
                                     a.CAMPO_MOSTRAR
                                 }).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el listado de opciones de una propiedad opcional.
        /// </summary>
        /// <param name="table"></param>
        /// <param name="showField"></param>
        /// <param name="idField"></param>
        /// <returns></returns>
        public DataTable GetOpcionesFromTable(string table, string showField, string idField)
        {
            string query = "SELECT " + idField + "," + showField + " FROM " + table;

            try
            {
                Desing_SQL conexion = new Desing_SQL();

                DataTable dt = conexion.EjecutarQuery(query);

                return dt;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que inserta la propiedad y el valor seleccionado de un arquetipo.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="idPropiedadOpcional"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public int InsertArquetipoPropiedadOpcional(string codigo, int idPropiedadOpcional, string valor)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TBL_ARQUETIPO_PROPIEDADES_OPCIONAL obj = new TBL_ARQUETIPO_PROPIEDADES_OPCIONAL();

                    obj.CODIGO = codigo;
                    obj.ID_PROPIEDAD_OPCIONA = idPropiedadOpcional;
                    obj.VALOR = valor;

                    Conexion.TBL_ARQUETIPO_PROPIEDADES_OPCIONAL.Add(obj);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que actuliza un valor de una propiedad opcional anteriormente guardada.
        /// </summary>
        /// <param name="codigo"></param>
        /// <param name="idPropiedadOpciona"></param>
        /// <param name="valor"></param>
        /// <returns></returns>
        public int UpdateArquetipoPropiedadOpcional(string codigo, int idPropiedadOpciona, string valor)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TBL_ARQUETIPO_PROPIEDADES_OPCIONAL obj = Conexion.TBL_ARQUETIPO_PROPIEDADES_OPCIONAL.Where(o => o.CODIGO == codigo && o.ID_PROPIEDAD_OPCIONA == idPropiedadOpciona).FirstOrDefault();

                    obj.VALOR = valor;

                    Conexion.Entry(obj).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public IList GetPropiedadesByCodigo(string codigo)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.TBL_ARQUETIPO_PROPIEDADES_OPCIONAL
                                 join b in Conexion.CAT_PROPIEDAD_OPCIONAL on a.ID_PROPIEDAD_OPCIONA equals b.ID_PROPIEDAD_OPCIONAL
                                 join c in Conexion.TR_PROPIEDAD_OPCIONAL_PERFIL on b.ID_PROPIEDAD_OPCIONAL equals c.ID_PROPIEDAD_OPCIONAL
                                 join d in Conexion.CAT_PERFIL on c.ID_PERFIL equals d.ID_PERFIL
                                 join e in Conexion.CAT_TIPO_PERFIL on d.ID_TIPO_PERFIL equals e.ID_TIPO_PERFIL
                                 join f in Conexion.TR_PERFIL_ARQUETIPO on d.ID_PERFIL equals f.ID_PERFIL
                                 where a.CODIGO == codigo && f.CODIGO == codigo
                                 select new
                                 {
                                     b.ID_PROPIEDAD_OPCIONAL,
                                     b.NOMBRE,
                                     b.DESCRIPCION_LARGA,
                                     b.DESCRIPCION_CORTA,
                                     a.VALOR,
                                     b.IMAGEN,
                                     b.SOURCE, 
                                     TIPO_PERFIL = e.PERFIL
                                 }).ToList();

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
