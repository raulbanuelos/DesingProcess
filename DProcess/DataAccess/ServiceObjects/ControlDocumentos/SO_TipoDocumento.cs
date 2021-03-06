﻿using DataAccess.ServiceObjects.ControlDocumentos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_TipoDocumento
    {
        /// <summary>
        /// Método para obtener los registros de la tabla.
        /// </summary>
        /// <returns>Si encuentra un error, retorna cero.</returns>
        public IList GetTipo()
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from t in Conexion.TBL_TIPO_DOCUMENTO
                                 orderby t.TIPO_DOCUMENTO ascending
                                 select new
                                 {
                                     t.ID_TIPO_DOCUMENTO,
                                     t.TIPO_DOCUMENTO,
                                     t.ABREBIATURA,
                                     t.FECHA_CREACION,
                                     t.FECHA_ACTUALIZACION,
                                     t.NUMERO_MATRIZ
                                 }).ToList();
                    //se retorna la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay algún error, se retorna un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método para insertar un registro a la tabla tipo.
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <param name="tipo_documento"></param>
        /// <param name="abreviatura"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <returns>Si encuentra un error, retorna cero.</returns>
        public int SetTipo(int id_tipo, string tipo_documento, string abreviatura, DateTime fecha_creacion, DateTime fecha_actualizacion, string num_matriz)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    TBL_TIPO_DOCUMENTO obj = new TBL_TIPO_DOCUMENTO();

                    //Se asiganan los valores.
                    obj.ID_TIPO_DOCUMENTO = id_tipo;
                    obj.TIPO_DOCUMENTO = tipo_documento;
                    obj.ABREBIATURA = abreviatura;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    obj.FECHA_CREACION = fecha_creacion;
                    obj.NUMERO_MATRIZ = num_matriz;

                    //Agrega el objeto a la tabla.
                    Conexion.TBL_TIPO_DOCUMENTO.Add(obj);
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del usuario insertado
                    return obj.ID_TIPO_DOCUMENTO;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        /// <summary>
        /// Método para modificar un registro de la tabla TBL_tipo
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <param name="tipo_documento"></param>
        /// <param name="abreviatura"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <returns>si hay error, retorna cero,</returns>
        public int UpdateTipo(int id_tipo, string tipo_documento, string abreviatura, DateTime fecha_creacion, DateTime fecha_actualizacion)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla 
                    TBL_TIPO_DOCUMENTO obj = Conexion.TBL_TIPO_DOCUMENTO.Where(x => x.ID_TIPO_DOCUMENTO == id_tipo).FirstOrDefault();

                    //Se asiganan los valores.

                    obj.TIPO_DOCUMENTO = tipo_documento;
                    obj.ABREBIATURA = abreviatura;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    obj.FECHA_CREACION = fecha_creacion;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges(); ;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla TBL_Tipo
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public int DeleteTipo(int id_tipo)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TBL_TIPO_DOCUMENTO obj = Conexion.TBL_TIPO_DOCUMENTO.Where(x => x.ID_TIPO_DOCUMENTO == id_tipo).FirstOrDefault();

                    //Se estable el estado de registro a eliminado.
                    Conexion.Entry(obj).State = EntityState.Deleted;

                    //Se guardan los cambios y retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si hay error, se regresa 0.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene el Id de tipo de documento a partir de un idDocumento
        /// </summary>
        /// <param name="idDocumento">Entero que representa el id del documento.</param>
        /// <returns></returns>
        public int GetTipoDocumento(int idDocumento)
        {
            try
            {
                //Relizamos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y e resultado lo guardamos en una variable local.
                    int IdTipoDocumento = (from a in Conexion.TBL_DOCUMENTO
                                           where a.ID_DOCUMENTO == idDocumento
                                           select a.ID_TIPO_DOCUMENTO).ToList().FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return IdTipoDocumento;
                }
            }
            catch (Exception)
            {
                //Si se genera un error retornamos un cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene el Id de tipo de documento a partir de un idVersion
        /// </summary>
        /// <param name="idVersion"></param>
        /// <returns></returns>
        public int GetTipoDocumentoByIdVersion(int idVersion)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    int idTipoDocumento = (from v in Conexion.TBL_VERSION
                                           join d in Conexion.TBL_DOCUMENTO on v.ID_DOCUMENTO equals d.ID_DOCUMENTO
                                           where v.ID_VERSION == idVersion
                                           select d.ID_TIPO_DOCUMENTO).ToList().FirstOrDefault();

                    return idTipoDocumento;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Retorna el nombre del tipo de acuerdo al id
        /// </summary>
        /// <param name="id_tipoDoc"></param>
        /// <returns></returns>
        public string GetNombreTipo(int id_tipoDoc)
        {
            try
            {
                //Relizamos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y e resultado lo guardamos en una variable local.
                    string TipoDocumento = (from t in Conexion.TBL_TIPO_DOCUMENTO
                                           where t.ID_TIPO_DOCUMENTO == id_tipoDoc
                                           select t.TIPO_DOCUMENTO).ToList().FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return TipoDocumento;
                }
            }
            catch (Exception)
            {
                //Si se genera un error retornamos un cero.
                return null;
            }
        }

        /// <summary>
        /// Método para validar si existe el tipo.
        /// </summary>
        /// <param name="tipo_documento"></param>
        /// <returns></returns>
        public int ValidateTipo(string tipo_documento, string abrev)
        {
            try
            {
                //Relizamos la conexión a través de Entity Framework.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y e resultado lo guardamos en una variable local.
                    int IdTipoDocumento = (from t in Conexion.TBL_TIPO_DOCUMENTO
                                           where t.TIPO_DOCUMENTO.Contains(tipo_documento) || t.ABREBIATURA.Equals(abrev)
                                           select t.ID_TIPO_DOCUMENTO).ToList().FirstOrDefault();

                    //Retornamos el resultado de la consulta.
                    return IdTipoDocumento;
                }
            }
            catch (Exception)
            {
                //Si se genera un error retornamos un cero.
                return 0;
            }
        }
    }     
}
