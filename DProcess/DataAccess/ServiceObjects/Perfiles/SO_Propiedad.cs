using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_Propiedad
    {
        /// <summary>
        /// Método que inserta un registro en la tabla CAT_PROPIEDAD.
        /// </summary>
        /// <param name="nombre"></param>
        /// <param name="descripcionCorta"></param>
        /// <param name="descripcionLarga"></param>
        /// <param name="imagen"></param>
        /// <param name="tipoDato"></param>
        /// <returns></returns>
        public int Insert(string nombre, string descripcionCorta, string descripcionLarga, byte[] imagen, string tipoDato)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    CAT_PROPIEDAD propiedad = new CAT_PROPIEDAD();
                    
                    propiedad.NOMBRE = nombre;
                    propiedad.DESCRIPCION_CORTA = descripcionCorta;
                    propiedad.DESCRIPCION_LARGA = descripcionLarga;
                    propiedad.IMAGEN = imagen;
                    propiedad.TIPO_DATO = tipoDato;
                    
                    propiedad.FECHA_ACTUALIZACION = DateTime.Now;
                    propiedad.FECHA_CREACION = DateTime.Now;
                    propiedad.ID_USUARIO_ACTUALIZACION = 1;
                    propiedad.ID_USUARIO_CREACION = 1;

                    Conexion.CAT_PROPIEDAD.Add(propiedad);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que actualiza un registro en la tabla CAT_PROPIEDAD.
        /// </summary>
        /// <param name="idPropiedad"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcionCorta"></param>
        /// <param name="descripcionLarga"></param>
        /// <param name="imagen"></param>
        /// <param name="tipoDato"></param>
        /// <returns></returns>
        public int Update(int idPropiedad,string nombre, string descripcionCorta, string descripcionLarga, byte[] imagen, string tipoDato)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    CAT_PROPIEDAD propiedad = Conexion.CAT_PROPIEDAD.Where(x => x.ID_PROPIEDAD == idPropiedad).FirstOrDefault();

                    propiedad.NOMBRE = nombre;
                    propiedad.DESCRIPCION_CORTA = descripcionCorta;
                    propiedad.DESCRIPCION_LARGA = descripcionLarga;
                    propiedad.IMAGEN = imagen;
                    propiedad.TIPO_DATO = tipoDato;

                    propiedad.FECHA_ACTUALIZACION = DateTime.Now;
                    propiedad.FECHA_CREACION = DateTime.Now;
                    propiedad.ID_USUARIO_ACTUALIZACION = 1;
                    propiedad.ID_USUARIO_CREACION = 1;

                    Conexion.Entry(propiedad).State = EntityState.Modified;

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que elimina un registro en la tabla CAT_PROPIEDAD.
        /// </summary>
        /// <param name="idPropiedad"></param>
        /// <returns></returns>
        public int Delete(int idPropiedad)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    CAT_PROPIEDAD propiedad = Conexion.CAT_PROPIEDAD.Where(x => x.ID_PROPIEDAD == idPropiedad).FirstOrDefault();

                    Conexion.Entry(propiedad).State = EntityState.Deleted;

                    return Conexion.SaveChanges();
                }
                

            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Método que retorna todas las propiedades.
        /// </summary>
        /// <returns></returns>
        public IList GetAllPropiedades()
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework
                using (var Conexion = new EntitiesPerfiles())
                {
                    //Realizamos la consulta y el resultado lo guardamos en una lista anónima.
                    var lista = (from a in Conexion.CAT_PROPIEDAD
                                 select a).ToList();

                    //Retornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception er)
            {
                //Si ocurre algún error, retornamos un null.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene las propiedades a partir de un perfil.
        /// </summary>
        /// <param name="idPerfil">Entero que representa el id del perfil que se requiere.</param>
        /// <returns></returns>
        public IList GetPropiedadesByPerfil(int idPerfil)
        {
            try
            {
                //Establecemos la conexión a través de Entity Framework
                using (var Conexion = new EntitiesPerfiles())
                {
                    //Realizamos la consulta y el resultado lo asignamos a una variable anónima.
                    var lista = (from a in Conexion.CAT_PROPIEDAD
                                 join b in Conexion.TR_PROPIEDAD_PERFIL on a.ID_PROPIEDAD equals b.ID_PROPIEDAD
                                 join c in Conexion.CAT_PERFIL on b.ID_PERFIL equals c.ID_PERFIL
                                 where c.ID_PERFIL == idPerfil
                                 select a).ToList();

                    //Retornamos el resultado de la consulta.
                    return lista;
                }
            }
            catch (Exception)
            {
                //Si ocurre algún error retornamos un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todas las propiedades cadena a partir de un perfil.
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <returns></returns>
        public IList GetPropiedadesCadenaByPerfil(int idPerfil)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var Lista = (from a in Conexion.CAT_PROPIEDAD_CADENA
                                 join b in Conexion.TR_PROPIEDAD_CADENA_PERFIL on a.ID_PROPIEDAD_CADENA equals b.ID_PROPIEDAD_CADENA
                                 join c in Conexion.CAT_PERFIL on b.ID_PERFIL equals c.ID_PERFIL
                                 where c.ID_PERFIL == idPerfil
                                 select a).ToList();

                    return Lista;
                                 
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todas las propiedades boolean a partir de un perfil.
        /// </summary>
        /// <param name="idPerfil"></param>
        /// <returns></returns>
        public IList GetPropiedadesBoolByPerfil(int idPerfil)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var Lista = (from a in Conexion.CAT_PROPIEDAD_BOOL
                                 join b in Conexion.TR_PROPIEDAD_BOOL_PERFIL on a.ID_PROPIEDAD_BOOL equals b.ID_PROPIEDAD_BOOL
                                 join c in Conexion.CAT_PERFIL on b.ID_PERFIL equals c.ID_PERFIL
                                 where c.ID_PERFIL == idPerfil
                                 select a).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int InsertArquetipoPropiedades(string codigo, int idPropiedad, string unidad, double valor)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TBL_ARQUETIPO_PROPIEDADES obj = new TBL_ARQUETIPO_PROPIEDADES();
                    obj.CODIGO = codigo;
                    obj.ID_PROPIEDAD = idPropiedad;
                    obj.UNIDAD = unidad;
                    obj.VALOR = valor;

                    Conexion.TBL_ARQUETIPO_PROPIEDADES.Add(obj);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateArquetipoPropiedades(string codigo, int idPropiedad, string unidad, double valor)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TBL_ARQUETIPO_PROPIEDADES obj = Conexion.TBL_ARQUETIPO_PROPIEDADES.Where(x => x.CODIGO == codigo && x.ID_PROPIEDAD == idPropiedad).FirstOrDefault();

                    obj.UNIDAD = unidad;
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

        public int InsertArquetipoPropiedadesCadena(string codigo, int idPropiedad, string valor)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TBL_ARQUETIPO_PROPIEDADES_CADENA obj = new TBL_ARQUETIPO_PROPIEDADES_CADENA();
                    obj.CODIGO = codigo;
                    obj.ID_PROPIEDAD_CADENA = idPropiedad;
                    obj.VALOR = valor;

                    Conexion.TBL_ARQUETIPO_PROPIEDADES_CADENA.Add(obj);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int UpdateArquetipoPropiedadesCadena(string codigo, int idPropiedad, string valor)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TBL_ARQUETIPO_PROPIEDADES_CADENA obj = Conexion.TBL_ARQUETIPO_PROPIEDADES_CADENA.Where(x => x.CODIGO == codigo && x.ID_PROPIEDAD_CADENA == idPropiedad).FirstOrDefault();

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

        public int InsertArquetipoPropiedadesBool(string codigo, int idPropiedad, bool valor)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TBL_ARQUETIPO_PROPIEDADES_BOOL obj = new TBL_ARQUETIPO_PROPIEDADES_BOOL();
                    obj.CODIGO = codigo;
                    obj.ID_PROPIEDAD_BOOL = idPropiedad;
                    obj.VALOR = valor;

                    Conexion.TBL_ARQUETIPO_PROPIEDADES_BOOL.Add(obj);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        } 

        public int UpdateArquetipoPropiedadesBool(string codigo, int idPropiedad, bool valor)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TBL_ARQUETIPO_PROPIEDADES_BOOL obj = Conexion.TBL_ARQUETIPO_PROPIEDADES_BOOL.Where(x => x.CODIGO == codigo && x.ID_PROPIEDAD_BOOL == idPropiedad).FirstOrDefault();

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
        
        /// <summary>
        /// Método que obtiene una propiedad en específico.
        /// </summary>
        /// <param name="idPropiedad"></param>
        /// <returns></returns>
        public IList GetPropiedadById(int idPropiedad)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var Lista = (from a in Conexion.CAT_PROPIEDAD
                                 where a.ID_PROPIEDAD == idPropiedad
                                 select a).ToList();

                    return Lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public IList GetPropiedadByNombre(string nombre)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.CAT_PROPIEDAD
                                 where a.NOMBRE == nombre
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
        /// Método que obtiene una propiedad que está guardada.
        /// </summary>
        /// <param name="idPropiedad"></param>
        /// <param name="codigo"></param>
        /// <returns></returns>
        public IList GetPropiedadSaved(string codigo)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.TBL_ARQUETIPO_PROPIEDADES
                                 join b in Conexion.CAT_PROPIEDAD on a.ID_PROPIEDAD equals b.ID_PROPIEDAD
                                 join c in Conexion.TR_PROPIEDAD_PERFIL on b.ID_PROPIEDAD equals c.ID_PROPIEDAD
                                 join d in Conexion.CAT_PERFIL on c.ID_PERFIL equals d.ID_PERFIL
                                 join e in Conexion.CAT_TIPO_PERFIL on d.ID_TIPO_PERFIL equals e.ID_TIPO_PERFIL
                                 join f in Conexion.TR_PERFIL_ARQUETIPO on d.ID_PERFIL equals f.ID_PERFIL
                                 where a.CODIGO == codigo && f.CODIGO == codigo
                                 select new
                                 {
                                     b.ID_PROPIEDAD,
                                     b.NOMBRE,
                                     b.DESCRIPCION_LARGA,
                                     b.DESCRIPCION_CORTA,
                                     b.TIPO_DATO,
                                     b.IMAGEN,
                                     a.UNIDAD,
                                     a.VALOR,
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

        public IList GetPropiedadCadenaSaved(string codigo)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.TBL_ARQUETIPO_PROPIEDADES_CADENA
                                 join b in Conexion.CAT_PROPIEDAD_CADENA on a.ID_PROPIEDAD_CADENA equals b.ID_PROPIEDAD_CADENA
                                 join c in Conexion.TR_PROPIEDAD_CADENA_PERFIL on b.ID_PROPIEDAD_CADENA equals c.ID_PROPIEDAD_CADENA
                                 join d in Conexion.CAT_PERFIL on c.ID_PERFIL equals d.ID_PERFIL
                                 join e in Conexion.CAT_TIPO_PERFIL on d.ID_TIPO_PERFIL equals e.ID_TIPO_PERFIL
                                 join f in Conexion.TR_PERFIL_ARQUETIPO on d.ID_PERFIL equals f.ID_PERFIL
                                 where a.CODIGO == codigo && f.CODIGO == codigo
                                 select new
                                 {
                                     ID_PROPIEDAD = b.ID_PROPIEDAD_CADENA,
                                     b.NOMBRE,
                                     b.DESCRIPCION_LARGA,
                                     b.DESCRIPCION_CORTA,
                                     b.IMAGEN,
                                     a.VALOR,
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

        public IList GetPropiedadBoolSaved(string codigo)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    var lista = (from a in Conexion.TBL_ARQUETIPO_PROPIEDADES_BOOL
                                 join b in Conexion.CAT_PROPIEDAD_BOOL on a.ID_PROPIEDAD_BOOL equals b.ID_PROPIEDAD_BOOL
                                 join c in Conexion.TR_PROPIEDAD_BOOL_PERFIL on b.ID_PROPIEDAD_BOOL equals c.ID_PROPIEDAD_BOOL
                                 join d in Conexion.CAT_PERFIL on c.ID_PERFIL equals d.ID_PERFIL
                                 join e in Conexion.CAT_TIPO_PERFIL on d.ID_TIPO_PERFIL equals e.ID_TIPO_PERFIL
                                 join f in Conexion.TR_PERFIL_ARQUETIPO on d.ID_PERFIL equals f.ID_PERFIL
                                 where a.CODIGO == codigo && f.CODIGO == codigo
                                 select new
                                 {
                                     ID_PROPIEDAD = b.ID_PROPIEDAD_BOOL,
                                     b.NOMBRE,
                                     b.DESCRIPCION_LARGA,
                                     b.DESCRIPCION_CORTA,
                                     b.IMAGEN,
                                     a.VALOR,
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

        /// <summary>
        /// Método que inserta un registro en la tabla TR_PROPIEDAD_PERFIL
        /// </summary>
        /// <param name="idPropiedad"></param>
        /// <param name="idPerfil"></param>
        /// <returns></returns>
        public int InsertPropiedadPerfil(int idPropiedad, int idPerfil)
        {
            try
            {
                using (EntitiesPerfiles Conexion = new EntitiesPerfiles())
                {
                    TR_PROPIEDAD_PERFIL tr = new TR_PROPIEDAD_PERFIL();

                    tr.ID_PROPIEDAD = idPropiedad;
                    tr.ID_PERFIL = idPerfil;

                    Conexion.TR_PROPIEDAD_PERFIL.Add(tr);

                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Método que elimina un registro de la tabla TR_PROPIEDAD_PERFIL
        /// </summary>
        /// <param name="idPropiedad"></param>
        /// <param name="idPerfil"></param>
        /// <returns></returns>
        public int DeletePropiedadPerfil(int idPropiedad, int idPerfil)
        {
            try
            {
                using (var Conexion = new EntitiesPerfiles())
                {
                    TR_PROPIEDAD_PERFIL tr = Conexion.TR_PROPIEDAD_PERFIL.Where(x => x.ID_PROPIEDAD == idPropiedad && x.ID_PERFIL == idPerfil).FirstOrDefault();

                    Conexion.Entry(tr).State = EntityState.Deleted;

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