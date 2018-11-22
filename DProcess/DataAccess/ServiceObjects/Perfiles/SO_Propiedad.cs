﻿using System;
using System.Collections;
using System.Data.Entity;
using System.Linq;

namespace DataAccess.ServiceObjects.Perfiles
{
    public class SO_Propiedad
    {
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
                                 where a.CODIGO == codigo
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
    }
}