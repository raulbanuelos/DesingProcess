﻿using DataAccess.SQLServer;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    public class SO_Documento
    {
        #region attributes
        private string SP_CIT_GET_DOCUMENTOS_LIBERADOS = "SP_CIT_GET_DOCUMENTOS_LIBERADOS";
        #endregion

        /// <summary>
        /// Método para obtener todos los registro de la tabla.
        /// </summary>
        /// <returns></returns>
        public IList GetDocumento()
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 join e in Conexion.TBL_ESTATUS_DOCUMENTO on d.ID_ESTATUS_DOCUMENTO equals e.ID_ESTATUS_DOCUMENTO
                                 join u in Conexion.Usuarios on d.ID_USUARIO equals u.Usuario
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     t.ID_TIPO_DOCUMENTO,
                                     d.NOMBRE,
                                     // d.DESCRIPCION,
                                     //d.VERSION_ACTUAL,
                                     d.FECHA_EMISION,
                                     d.FECHA_CREACION,
                                     d.FECHA_ACTUALIZACION,
                                     e.ID_ESTATUS_DOCUMENTO,
                                     u.Usuario
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
        /// Método para obtener todos los registro de la tabla.
        /// </summary>
        /// <returns></returns>
        public IList GetDocumento(string nombre)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta y se guardan en una lista, para retornar el resultado.
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 join e in Conexion.TBL_ESTATUS_DOCUMENTO on d.ID_ESTATUS_DOCUMENTO equals e.ID_ESTATUS_DOCUMENTO
                                 join u in Conexion.Usuarios on d.ID_USUARIO equals u.Usuario
                                 where d.NOMBRE == nombre
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     t.ID_TIPO_DOCUMENTO,
                                     d.NOMBRE,
                                     // d.DESCRIPCION,
                                     //d.VERSION_ACTUAL,
                                     d.FECHA_EMISION,
                                     d.FECHA_CREACION,
                                     d.FECHA_ACTUALIZACION,
                                     e.ID_ESTATUS_DOCUMENTO,
                                     d.ID_DEPARTAMENTO,
                                     u.Usuario
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
        /// metodo que obtiene los documentos de un usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public IList GetDocumentosUsuario(string id_usuario)
        {
            try
            {
                using (var conexion = new EntitiesControlDocumentos())
                {
                    var Lista = (from d in conexion.TBL_DOCUMENTO
                                 where d.ID_USUARIO==id_usuario
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.ID_USUARIO
                                 }).ToList();
                    //se retorna la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Método para insertar un registro en la tabla TBL_Documento.
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="version_actual"></param>
        /// <param name="fecha_creacion"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <param name="fecha_emision"></param>
        /// <returns></returns>
        public int SetDocumento( int id_tipo_documento,int id_dep, string nombre,int id_estatus, string id_usuario, DateTime fecha_creacion)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo documento, el cual se va agregar a la tabla
                    TBL_DOCUMENTO obj = new TBL_DOCUMENTO();

                    //Se asiganan los valores.
                    obj.ID_TIPO_DOCUMENTO = id_tipo_documento;
                    obj.ID_DEPARTAMENTO = id_dep;
                    obj.NOMBRE = nombre;
                    //obj.DESCRIPCION = descripcion;
                    obj.FECHA_CREACION = fecha_creacion;
                    obj.ID_ESTATUS_DOCUMENTO = id_estatus;
                    obj.ID_USUARIO = id_usuario;

                    //Agrega el objeto a la tabla.
                    Conexion.TBL_DOCUMENTO.Add(obj);
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del documento insertado
                    return obj.ID_DOCUMENTO;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        /// <summary>
        /// Método para actualizar un registro de la tabla TBL_documento
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="id_tipo_documento"></param>
        /// <param name="id_dep"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <param name="id_estatus"></param>
        /// <param name="fecha_emision"></param>
        /// <param name="idUsuario">id del usuario que lo creó</param>
        /// <returns></returns>
        public int UpdateDocumento(int id_documento, int id_tipo_documento,int id_dep, DateTime fecha_actualizacion,int id_estatus,DateTime fecha_emision,string idUsuario)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_DOCUMENTO obj = Conexion.TBL_DOCUMENTO.Where(x => x.ID_DOCUMENTO == id_documento).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    obj.ID_TIPO_DOCUMENTO = id_tipo_documento;
                    obj.ID_DEPARTAMENTO = id_dep;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    obj.FECHA_EMISION = fecha_emision;
                    obj.ID_ESTATUS_DOCUMENTO = id_estatus;
                    obj.ID_USUARIO = idUsuario;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si encuentra error devuelve cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que modifica la fecha de actualización de un documento
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="version_actual"></param>
        /// <returns></returns>
        public int UpdateFecha_Actualizacion(int id_documento, DateTime fecha_actualizacion)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion= new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_DOCUMENTO obj = Conexion.TBL_DOCUMENTO.Where(x => x.ID_DOCUMENTO == id_documento).FirstOrDefault();

                    //Se modifica el id de la version con la original
                    obj.FECHA_ACTUALIZACION= fecha_actualizacion;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si encuentra error devuelve cero.
                return 0;

            }
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla TBL_documento.
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public int DeleteDocumento(int id_documento)
        {
            try
            {
                // Se inicializa la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se obtiene el objeto que se va a eliminar.
                    TBL_DOCUMENTO obj = Conexion.TBL_DOCUMENTO.Where(x => x.ID_DOCUMENTO == id_documento).FirstOrDefault();

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
        /// Método que busca el documento de acuerdo al nombre o descripción
        /// </summary>
        /// <param name="search"></param>
        /// <returns></returns>
        public IList SearchDocumento(string keyword)
        {
            try
            {
                //Incializamos la conexión a través de EntityControlDocumentos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta para obtener todos los registros,donde el nombre o la descripción  del documento debe de contener la palabra recibida.
                    var Lista = Conexion.TBL_DOCUMENTO.Where(d => d.NOMBRE.Contains(keyword)).ToList();

                    //Renornamos el resultado de la consulta.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si se generó algún error, retornamos un nulo.
                return null;
            }

        }

        /// <summary>
        /// Método para obtener los registros por tipo de documento para el llenado de la tabla ControlDocumento.
        /// </summary>
        /// <returns></returns>
        public IList GetDataGrid(int idTipoDocumento,string textoBusqueda)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    if (string.IsNullOrEmpty(textoBusqueda))
                    {
                        //Realizamos la consulta, para llenar la tabla de control documento
                        var lista = (from d in Conexion.TBL_DOCUMENTO
                                     join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                     join a in Conexion.TBL_ARCHIVO on v.ID_VERSION equals a.ID_VERSION
                                     join b in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals b.ID_DEPARTAMENTO
                                     join u in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals u.Usuario
                                     join uu in Conexion.Usuarios on v.ID_USUARIO_AUTORIZO equals uu.Usuario
                                     where d.ID_TIPO_DOCUMENTO == idTipoDocumento && d.ID_ESTATUS_DOCUMENTO == 5 && v.ID_ESTATUS_VERSION == 1
                                     select new
                                     {
                                         d.ID_DOCUMENTO,
                                         d.NOMBRE,
                                         FECHA_ACTUALIZACION = v.FECHA_VERSION,
                                         d.ID_DEPARTAMENTO,
                                         v.No_VERSION,
                                         v.ID_VERSION,
                                         v.NO_COPIAS,
                                         DESCRIPCION = v.DESCRIPCION,
                                         b.NOMBRE_DEPARTAMENTO,
                                         d.FECHA_EMISION,
                                         USUARIO_ELABORO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                         USUARIO_AUTORIZO = uu.Nombre + " " + uu.APaterno + " " + uu.AMaterno
                                     }).OrderBy(x => x.ID_DOCUMENTO).Distinct().ToList();
                        return lista;
                    }
                    else
                    {
                        //Realizamos la consulta, para llenar la tabla de control documento
                        var lista = (from d in Conexion.TBL_DOCUMENTO
                                     join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                     join a in Conexion.TBL_ARCHIVO on v.ID_VERSION equals a.ID_VERSION
                                     join b in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals b.ID_DEPARTAMENTO
                                     join u in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals u.Usuario
                                     join uu in Conexion.Usuarios on v.ID_USUARIO_AUTORIZO equals uu.Usuario
                                     where d.ID_TIPO_DOCUMENTO == idTipoDocumento && d.ID_ESTATUS_DOCUMENTO == 5 && v.ID_ESTATUS_VERSION == 1  && (d.NOMBRE.Contains(textoBusqueda) || v.DESCRIPCION.Contains(textoBusqueda))
                                     select new
                                     {
                                         d.ID_DOCUMENTO,
                                         d.NOMBRE,
                                         FECHA_ACTUALIZACION = v.FECHA_VERSION,
                                         d.ID_DEPARTAMENTO,
                                         v.No_VERSION,
                                         v.ID_VERSION,
                                         v.NO_COPIAS,
                                         DESCRIPCION = v.DESCRIPCION,
                                         b.NOMBRE_DEPARTAMENTO,
                                         d.FECHA_EMISION,
                                         USUARIO_ELABORO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                         USUARIO_AUTORIZO = uu.Nombre + " " + uu.APaterno + " " + uu.AMaterno
                                     }).OrderBy(x => x.ID_DOCUMENTO).Distinct().ToList();
                        return lista;
                    }
                }
            }
            catch (Exception)
            {
                //Si existe algún error, se regresa nulo.
                return null;
            }
        }

        /// <summary>
        /// Método para obtener todos los documentos que están aprobados
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public IList GetGridDocumentos(string texto)
        {
            try
            {
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Si el texto es nulo, realiza la consulta de todos los documentos aprobados
                    if (string.IsNullOrEmpty(texto))
                    {
                        var Lista= (from d in Conexion.TBL_DOCUMENTO
                                    join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                    join u in Conexion.Usuarios on v.ID_USUARIO_AUTORIZO equals u.Usuario
                                    join us in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals us.Usuario
                                    join a in Conexion.TBL_ARCHIVO on v.ID_VERSION equals a.ID_VERSION
                                    join b in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals b.ID_DEPARTAMENTO
                                    join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                    where  d.ID_ESTATUS_DOCUMENTO == 5 && v.ID_ESTATUS_VERSION == 1
                                    select new
                                    {
                                        d.ID_DOCUMENTO,
                                        d.NOMBRE,
                                        v.FECHA_VERSION,
                                        d.ID_DEPARTAMENTO,
                                        v.No_VERSION,
                                        v.ID_VERSION,
                                        v.NO_COPIAS,
                                        DESCRIPCION = v.DESCRIPCION.ToUpper(),
                                        b.NOMBRE_DEPARTAMENTO,
                                        d.FECHA_EMISION,
                                        t.TIPO_DOCUMENTO,
                                        ID_TIPO_DOCUMENTO = t.ID_TIPO_DOCUMENTO,
                                        USUARIO_AUTORIZO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                        USUARIO_ELABORO =us.Nombre + " " + us.APaterno + " " + us.AMaterno,
                                        NOMBRE_AUTORIZO = u.Nombre,
                                        APELLIDO_AUTORIZO = u.APaterno,
                                        NOMBRE_ELABORO = us.Nombre,
                                        APELLIDO_ELABORO = us.APaterno
                                    }).OrderBy(x => x.ID_DOCUMENTO).Distinct().ToList();

                        return Lista;
                    }
                    else
                    {
                        var Lista = (from d in Conexion.TBL_DOCUMENTO
                                     join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                     join u in Conexion.Usuarios on v.ID_USUARIO_AUTORIZO equals u.Usuario
                                     join us in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals us.Usuario
                                     join a in Conexion.TBL_ARCHIVO on v.ID_VERSION equals a.ID_VERSION
                                     join b in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals b.ID_DEPARTAMENTO
                                     join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                     where d.ID_ESTATUS_DOCUMENTO == 5 && v.ID_ESTATUS_VERSION == 1 && (d.NOMBRE.Contains(texto) || v.DESCRIPCION.Contains(texto))
                                     select new
                                     {
                                         d.ID_DOCUMENTO,
                                         d.NOMBRE,
                                         FECHA_ACTUALIZACION = v.FECHA_VERSION,
                                         d.ID_DEPARTAMENTO,
                                         v.No_VERSION,
                                         v.ID_VERSION,
                                         v.NO_COPIAS,
                                         DESCRIPCION = v.DESCRIPCION.ToUpper(),
                                         b.NOMBRE_DEPARTAMENTO,
                                         d.FECHA_EMISION,
                                         t.TIPO_DOCUMENTO,
                                         USUARIO_AUTORIZO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                         USUARIO_ELABORO = us.Nombre + " " + us.APaterno + " " + us.AMaterno,
                                         v.FECHA_VERSION,
                                         NOMBRE_AUTORIZO = u.Nombre,
                                         APELLIDO_AUTORIZO = u.APaterno,
                                         NOMBRE_ELABORO = us.Nombre,
                                         APELLIDO_ELABORO = us.APaterno
                                     }).OrderBy(x => x.ID_DOCUMENTO).Distinct().ToList();

                        return Lista;
                    }
                }

            }catch(Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Métdo para obtener el tipo de documento y el archivo.
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public IList GetTipo(int id_documento,int id_version)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta para obtener el archivo y el tipo de acuerdo al documento requerido.
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 join dep in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals dep.ID_DEPARTAMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 join a in Conexion.TBL_ARCHIVO on v.ID_VERSION equals a.ID_VERSION
                                 where d.ID_DOCUMENTO == id_documento && v.ID_VERSION == id_version
                                 select new
                                 {
                                     d.ID_TIPO_DOCUMENTO,
                                     a.NOMBRE_ARCHIVO,
                                     t.TIPO_DOCUMENTO,
                                     a.ID_ARCHIVO,
                                     a.ARCHIVO,
                                     a.EXT,
                                     dep.NOMBRE_DEPARTAMENTO
                                 }).ToList();
                    //se retorna la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error, retorna nulo.
                return null;
            }
        }

        /// <summary>
        /// Método para generar número al documento.
        /// </summary>
        /// <param name="nombre"></param>
        /// <returns></returns>
        public string GetNumero(string nombre, int id_tipo,int id_deptartamento)
        {
            string lastNumber;
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta para obetener el último número de un documento.
                    string numero = (from d in Conexion.TBL_DOCUMENTO
                                     where d.NOMBRE.Contains(nombre) && d.ID_DEPARTAMENTO == id_deptartamento && d.ID_TIPO_DOCUMENTO == id_tipo
                                     orderby d.ID_DOCUMENTO descending
                                     select d.NOMBRE).ToList().FirstOrDefault();

                    //se asigna a la variable lastNumber
                    lastNumber = numero;

                    //si no encontró ningun registro, se crea un nuevo número
                    if (string.IsNullOrEmpty(lastNumber))
                    {
                        //se le asigna al nombre el primer número.
                        lastNumber = string.Concat(nombre, "-0001");

                        //Validamos que el número no sea repetido. Si es así, asignamos el siguiente hasta que no se repita.
                        while (ExistDocumento(lastNumber))
                        {
                            string resultString = string.Empty;
                            string[] vec = lastNumber.Split('-');
                            int c = vec.Length;
                            string num = vec[c - 1];
                            resultString = num;
                            int number = Int32.Parse(resultString);
                            number++;

                            //Concatenamos el nombre con el nuevo número
                            lastNumber = string.Concat(nombre, "-", number.ToString("D4"));
                        }
                    }
                    else
                    {
                        //si encontro algún número
                        //Extraemos el número del registro que se obtuvo
                        string resultString = string.Empty;
                        if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014)
                        {
                            string a = lastNumber.Substring(lastNumber.Length - 7, 7);
                            resultString = Regex.Match(a, @"\d+").Value;
                        }

                        else
                        {
                            //resultString = Regex.Match(lastNumber, @"\d+").Value;

                            string[] vec = lastNumber.Split('-');
                            int c = vec.Length;
                            string num = vec[c - 1];
                            resultString = num;
                        }

                        //Sumamos uno al número
                        int number = Int32.Parse(resultString);
                       number++;

                        //Concatenamos el nombre con el nuevo número
                        lastNumber = string.Concat(nombre,"-",number.ToString("D4"));
                    }
                    //Si el documento es de tipo procedimiento o formato
                    if (id_tipo == 1003 || id_tipo == 1005 || id_tipo == 1006 || id_tipo == 1012 || id_tipo == 1013 || id_tipo == 1014)
                    {
                        //Si agrega -es al final del número generado anteriror
                        lastNumber = string.Concat(lastNumber, "-es");
                    }

                }
            }
            catch (Exception)
            {
                //Si hay algún error, se retorna un nulo.
                return null;
            }
            //retornamos el número generado.
            return lastNumber;
        }

        /// <summary>
        /// Método para obtener los documentos pendientes de un usuario
        /// </summary>
        /// <param name="id_usuario"></param>
        public IList GetDocumento_Version(string id_usuario)
        {
            try
            {
                //Establecemos la conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta para obtener los documentos que se encuentran pendientes del usuario
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 join dep in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals dep.ID_DEPARTAMENTO
                                 where d.ID_ESTATUS_DOCUMENTO == 1 & d.ID_USUARIO== id_usuario
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.ID_TIPO_DOCUMENTO,
                                     d.ID_DEPARTAMENTO,
                                     d.NOMBRE,
                                     t.TIPO_DOCUMENTO,
                                     dep.NOMBRE_DEPARTAMENTO,
                                     d.ID_ESTATUS_DOCUMENTO
                                 }).OrderBy(x => x.ID_DOCUMENTO).ToList();

                    //Se retorna la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay algun error, retorna nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene el nombre de un documento
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public IList GetNombre(int id_documento)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta para obtener el archivo y el tipo de acuerdo al documento requerido.
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 where d.ID_DOCUMENTO == id_documento
                                 select new
                                 {
                                     d.NOMBRE
                                 }).ToList();
                    //se retorna la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error, retorna nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene las versiones que esten pendientes por corregir por el administrador
        /// y los va filtrando con el parametro textobuscar
        /// </summary>
        /// <returns></returns>
        public IList GetDocumentosValidar(string textobuscar)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta para obtener todas las versiones que de deben aprobar
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 join u in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals u.Usuario
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 where v.ID_ESTATUS_VERSION == 3 && d.NOMBRE.Contains(textobuscar)
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.NOMBRE,
                                     NOMBRE_USUARIO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                     t.TIPO_DOCUMENTO,
                                     v.No_VERSION
                                 }).ToList();
                    //retorna la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error, regresa nulo
                return null;
            }
        }

        /// <summary>
        /// Método para obtener el registro de documento a partir de un idVersion.
        /// </summary>
        /// <param name="idVersion"></param>
        /// <returns></returns>
        public IList GetDocumentoByVersion(int idVersion)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta
                    var lista = (from v in Conexion.TBL_VERSION
                                 join d in Conexion.TBL_DOCUMENTO on v.ID_DOCUMENTO equals d.ID_DOCUMENTO
                                 where v.ID_VERSION == idVersion
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.ID_TIPO_DOCUMENTO,
                                     d.ID_USUARIO,
                                     d.ID_DEPARTAMENTO,
                                     d.ID_ESTATUS_DOCUMENTO,
                                     d.NOMBRE,
                                     d.FECHA_EMISION,
                                     d.FECHA_CREACION,
                                     d.FECHA_ACTUALIZACION,
                                 }).ToList();

                    //Retornamos el resutado.
                    return lista;
                }
            }
            catch (Exception)
            {
                //Si hay error, se regresa nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene toda la información de un documento y su versión actual
        /// </summary>
        /// <param name="idDocumento"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public IList GetDocumentoVersion(int idDocumento, string version)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta
                    var lista = (from d in Conexion.TBL_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 join dep in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals dep.ID_DEPARTAMENTO
                                 where v.No_VERSION == version && d.ID_DOCUMENTO == idDocumento
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.ID_TIPO_DOCUMENTO,
                                     d.ID_USUARIO,
                                     d.ID_DEPARTAMENTO,
                                     d.ID_ESTATUS_DOCUMENTO,
                                     d.NOMBRE,
                                     v.DESCRIPCION,
                                     d.FECHA_EMISION,
                                     d.FECHA_CREACION,
                                     d.FECHA_ACTUALIZACION,
                                     v.ID_VERSION,
                                     v.ID_ESTATUS_VERSION,
                                     v.ID_USUARIO_ELABORO,
                                     v.ID_USUARIO_AUTORIZO,
                                     v.No_VERSION,
                                     v.FECHA_VERSION,
                                     v.NO_COPIAS,
                                     DEPARTAMENTO=dep.NOMBRE_DEPARTAMENTO
                                 }).ToList();

                    //Se retorna la lista
                    return lista;
                }
            }
            catch (Exception)
            {
                //Si hay error, se regresa nulo
                return null;
            }
        }

        /// <summary>
        /// Método que modifica el estatus del documento
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="id_estatus"></param>
        /// <returns></returns>
        public int UpdateEstatus_Documento(int id_documento, int id_estatus)
        {
            try
            {
                //Se establece la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //creación del objeto tipo TBL_archivo.
                    TBL_DOCUMENTO obj = Conexion.TBL_DOCUMENTO.Where(x => x.ID_DOCUMENTO == id_documento).FirstOrDefault();

                    //Asignamos los  parámetros recibidos a cada uno de los valores de los objetos.
                    obj.ID_ESTATUS_DOCUMENTO = id_estatus;

                    //Se cambia el estado de registro a modificado.
                    Conexion.Entry(obj).State = EntityState.Modified;

                    //Se guardan los cambios y se retorna el número de registros afectados.
                    return Conexion.SaveChanges();
                }
            }
            catch (Exception)
            {
                //Si encuentra error devuelve cero.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todos los documentos pendientes por corregir
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public IList GetDocumentosPendientes(string usuario,string textobuscar)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta para obtener los documentos pendientes por corregir de un usuario
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 join u in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals u.Usuario
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 where v.ID_ESTATUS_VERSION == 4 & v.ID_USUARIO_ELABORO.Equals(usuario) && d.NOMBRE.Contains(textobuscar)
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.NOMBRE,
                                     NOMBRE_USUARIO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                     t.TIPO_DOCUMENTO,
                                     v.No_VERSION,
                                     v.FECHA_VERSION
                                 }).ToList();

                    //Retorna la lista de los documentos
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error, regresa nulo
                return null;
            }
        }

        /// <summary>
        /// Metodo que retorna todos los documentos que estan en Pendiente por corregir
        /// </summary>
        /// <param name="textoBuscar"></param>
        /// <returns></returns>
        public IList GetAllDocumentosPendientes(string textoBuscar)
        {
            try
            {
                // Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta para obtener los documentos pendientes por corregir de un usuario
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 join u in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals u.Usuario
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 where v.ID_ESTATUS_VERSION == 4 && (d.NOMBRE.Contains(textoBuscar) || u.Nombre.Contains(textoBuscar))
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.NOMBRE,
                                     NOMBRE_USUARIO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                     t.TIPO_DOCUMENTO,
                                     v.No_VERSION,
                                     v.FECHA_VERSION
                                 }).ToList();

                    //Retorna la lista de documentos.
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error, se retorna un nulo.
                return null;
            }
        }

        /// <summary>
        /// Método para obtener todos los documentos aprobados pendientes por liberar
        /// si recibe un parametro lo filtra
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public IList GetDocumentosAprobados(string textobuscar)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 join u in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals u.Usuario
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 where v.ID_ESTATUS_VERSION == 5 && d.NOMBRE.Contains(textobuscar)
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.NOMBRE,
                                     NOMBRE_USUARIO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                     t.TIPO_DOCUMENTO,
                                     v.No_VERSION,
                                     v.FECHA_VERSION,
                                     v.ID_USUARIO_ELABORO,
                                     ID_VERSION = v.ID_VERSION,
                                     ID_USUARIO_AUTORIZO = v.ID_USUARIO_AUTORIZO,
                                     ID_TIPO_DOCUMENTO = d.ID_TIPO_DOCUMENTO,
                                     CODE_VALIDATION = v.CODE_VALIDATION,
                                     DESCRIPCION_VERSION = v.DESCRIPCION
                                 }).ToList();

                    //Retorna la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Retorna nulo, si hay error
                return null;
            }
        }

        public IList GetDocumentosAprobadosBuscadosCodigoValidacion(string CodigoValidacion)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 join u in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals u.Usuario
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 where v.ID_ESTATUS_VERSION == 5 && v.CODE_VALIDATION == CodigoValidacion
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.NOMBRE,
                                     NOMBRE_USUARIO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                     t.TIPO_DOCUMENTO,
                                     v.No_VERSION,
                                     v.FECHA_VERSION,
                                     v.ID_USUARIO_ELABORO,
                                     ID_VERSION = v.ID_VERSION,
                                     ID_USUARIO_AUTORIZO = v.ID_USUARIO_AUTORIZO,
                                     ID_TIPO_DOCUMENTO = d.ID_TIPO_DOCUMENTO,
                                     CODE_VALIDATION = v.CODE_VALIDATION,
                                     DESCRIPCION_VERSION = v.DESCRIPCION
                                 }).ToList();

                    //Retorna la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Retorna nulo, si hay error
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene todos los documentos pendientes por liberar de un determinado usuarip
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public IList GetDocumentos_PendientesLiberar(string usuario)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta y se asigna a una variebla local.
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 join u in Conexion.Usuarios on v.ID_USUARIO_ELABORO equals u.Usuario
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 where v.ID_ESTATUS_VERSION == 5 & v.ID_USUARIO_ELABORO.Equals(usuario)
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.NOMBRE,
                                     NOMBRE_USUARIO = u.Nombre + " " + u.APaterno + " " + u.AMaterno,
                                     t.TIPO_DOCUMENTO,
                                     v.No_VERSION,
                                     v.ID_VERSION
                                 }).ToList();

                    //Regresa la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //Si hay error, regresa nulo
                return null;
            }
        }

        /// <summary>
        /// Método para insertar documentos desde un archivo excel
        /// </summary>
        /// <param name="id_tipo_documento"></param>
        /// <param name="id_dep"></param>
        /// <param name="nombre"></param>
        /// <param name="descripcion"></param>
        /// <param name="fecha_emision"></param>
        /// <param name="id_estatus"></param>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public int InsertDocumentos(int id_tipo_documento, int id_dep, string nombre, DateTime fecha_emision, DateTime fecha_actualizacion, int id_estatus, string id_usuario)
        {
            try
            {
                //Se establece conexión a la BD.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se  crea un objeto de tipo usuarios, el cual se va agregar a la tabla
                    TBL_DOCUMENTO obj = new TBL_DOCUMENTO();

                    //Se asiganan los valores.
                    obj.ID_TIPO_DOCUMENTO = id_tipo_documento;
                    obj.ID_DEPARTAMENTO = id_dep;
                    obj.NOMBRE = nombre;
                    obj.FECHA_CREACION = fecha_emision;
                    obj.FECHA_ACTUALIZACION = fecha_actualizacion;
                    obj.FECHA_EMISION = fecha_emision;
                    obj.ID_ESTATUS_DOCUMENTO = id_estatus;
                    obj.ID_USUARIO = id_usuario;

                    //Agrega el objeto a la tabla.
                    Conexion.TBL_DOCUMENTO.Add(obj);
                    //Se guardan los cambios
                    Conexion.SaveChanges();

                    //Retorna el código del documento insertado
                    return obj.ID_DOCUMENTO;
                }
            }
            catch (Exception)
            {
                //Si hay error regresa una cadena vacía.
                return 0;
            }
        }

        /// <summary>
        /// Método que obtiene todos los documentos liberados de un tipo de documento y departamente en específico
        /// Obtiene los documentos diferentes al documento recibido
        /// </summary>
        /// <param name="id_tipo_doc"></param>
        /// <param name="id_dep"></param>
        /// <param name="id_doc"></param>
        /// <returns></returns>
        public IList ValidateDescripcion(int id_tipo_doc, int id_dep, int id_doc)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta para obtener los documentos liberados
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 where d.ID_DEPARTAMENTO == id_dep && d.ID_TIPO_DOCUMENTO == id_tipo_doc && v.ID_ESTATUS_VERSION == 1 && d.ID_ESTATUS_DOCUMENTO == 5 && d.ID_DOCUMENTO != id_doc
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.NOMBRE,
                                     v.DESCRIPCION,
                                     v.ID_VERSION,
                                     v.No_VERSION,
                                     v.FECHA_VERSION
                                 }).ToList();
                    //retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error retornamos nulo
                return null;
            }
        }

        /// <summary>
        /// Método que verifica si una descripción se repite.
        /// </summary>
        /// <param name="id_tipo_doc"></param>
        /// <param name="id_dep"></param>
        /// <param name="id_doc"></param>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public IList ValidateIgualDescripcion(int id_tipo_doc, int id_dep, int id_doc,string descripcion)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta para obtener los documentos liberados
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 where d.ID_DEPARTAMENTO == id_dep && d.ID_TIPO_DOCUMENTO == id_tipo_doc && v.ID_ESTATUS_VERSION == 1 && d.ID_ESTATUS_DOCUMENTO == 5 && d.ID_DOCUMENTO != id_doc && v.DESCRIPCION.Equals(descripcion)
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.NOMBRE,
                                     v.DESCRIPCION,
                                     v.ID_VERSION,
                                     v.No_VERSION,
                                     v.FECHA_VERSION
                                 }).ToList();
                    //retornamos la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error retornamos nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene los documentos de un usuario que el año de actualización sea mayor al período de actualizacion
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public IList GetDocumentos_Vencidos(string usuario, DateTime fecha_sistema)
        {
            try
            {
                //Se inician los servicios de Entity Control Documento
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Se realiza la consulta
                    var Lista = (from d in Conexion.TBL_DOCUMENTO
                                 join v in Conexion.TBL_VERSION on d.ID_DOCUMENTO equals v.ID_DOCUMENTO
                                 join u in Conexion.Usuarios on d.ID_USUARIO equals u.Usuario
                                 join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                 join dep in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals dep.ID_DEPARTAMENTO
                                 join c in Conexion.TBL_CONF_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals c.ID_TIPO_DOCUMENTO
                                 where d.ID_USUARIO.Equals(usuario) && d.ID_ESTATUS_DOCUMENTO ==5 && v.ID_ESTATUS_VERSION ==1 &&((fecha_sistema.Year - d.FECHA_ACTUALIZACION.Value.Year) >= c.PERIODO_ANIOS_ACTUALIZACION)
                                 select new
                                 {
                                     d.ID_DOCUMENTO,
                                     d.ID_TIPO_DOCUMENTO,
                                     d.ID_DEPARTAMENTO,
                                     d.NOMBRE,
                                     d.ID_USUARIO,
                                     d.FECHA_EMISION,
                                     d.FECHA_ACTUALIZACION,
                                     v.No_VERSION,
                                     v.ID_VERSION,
                                     v.DESCRIPCION,
                                     t.TIPO_DOCUMENTO,
                                     dep.NOMBRE_DEPARTAMENTO,
                                     c.PERIODO_ANIOS_ACTUALIZACION
                                 }).ToList();
                    //Retorna la lista
                    return Lista;
                }
            }
            catch (Exception)
            {
                //si hay error regresa nulo
                return null;
            }
        }

        /// <summary>
        /// Método que devuelve la fecha del servidor
        /// </summary>
        /// <returns></returns>
        public DateTime Get_DateTime()
        {
            try
            {
                DataSet data = null;
                //Se crea conexion a la BD.
                Desing_SQL conexion = new Desing_SQL();

                //Se inicializa un dictionario que contiene propiedades de tipo string y un objeto.
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                //se ejecuta el procedimiento que regresa la fecha del servidor
                data = conexion.EjecutarStoredProcedure("GET_DATETIME", parametros);

                DateTime dt = Convert.ToDateTime(data.Tables[0].Rows[0][0].ToString());
                //data.Tables[0].Rows[0].ItemArray[0].ToString();

                return dt;
            }
            catch (Exception)
            {
                return DateTime.Now;
            }
        }

        /// <summary>
        /// Método que obtiene el historial y la información de acuerdo a los parámetros recibidos.
        /// </summary>
        /// <param name="fecha_inicio"></param>
        /// <param name="fecha_fin"></param>
        /// <param name="descripcion"></param>
        /// <param name="id_departamento"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public IList GetHistorial_Documentos(DateTime fecha_inicio, DateTime fecha_fin, string descripcion, int id_departamento, int id_tipo)
        {
            try
            {
                //Se inicializa los servicios de EntityControlDocumetnos.
                using (var Conexion= new EntitiesControlDocumentos())
                {
                    //Si el id_tipo y el id_departamento son cero, sólo se va a filtrar por estatus.
                    if (id_departamento == 0 && id_tipo == 0)
                    {
                        //Se realiza la consulta y se asigna a una variable.
                        var Lista = (from h in Conexion.TBL_HISTORIAL_VERSION
                                     join d in Conexion.TBL_DOCUMENTO on h.NOMBRE_DOCUMENTO equals d.NOMBRE
                                     join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                     join dep in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals dep.ID_DEPARTAMENTO
                                     where DbFunctions.TruncateTime(h.FECHA) >= fecha_inicio.Date && DbFunctions.TruncateTime(h.FECHA) <= fecha_fin.Date && h.DESCRIPCION.Contains(descripcion)
                                     select new
                                     {
                                         d.NOMBRE,
                                         h.NO_VERSION,
                                         t.TIPO_DOCUMENTO,
                                         dep.NOMBRE_DEPARTAMENTO,
                                         FechaHistorial = h.FECHA,
                                         h.DESCRIPCION
                                     }).OrderBy(x => x.FechaHistorial).ToList();

                        //retornamos la lista.
                        return Lista;
                    }
                    //Si el id del tipo y el departamentos es diferente de cero.
                    else if (id_tipo!=0 & id_departamento!=0)
                    {
                        //Se realiza la consulta y se asigna a una variable.
                        var Lista = (from h in Conexion.TBL_HISTORIAL_VERSION
                                     join d in Conexion.TBL_DOCUMENTO on h.NOMBRE_DOCUMENTO equals d.NOMBRE
                                     join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                     join dep in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals dep.ID_DEPARTAMENTO
                                     where DbFunctions.TruncateTime(h.FECHA) >= fecha_inicio && DbFunctions.TruncateTime(h.FECHA) <= fecha_fin &&
                                     h.DESCRIPCION.Contains(descripcion) && t.ID_TIPO_DOCUMENTO == id_tipo && dep.ID_DEPARTAMENTO == id_departamento
                                     select new
                                     {
                                         d.NOMBRE,
                                         h.NO_VERSION,
                                         t.TIPO_DOCUMENTO,
                                         dep.NOMBRE_DEPARTAMENTO,
                                         FechaHistorial = h.FECHA,
                                         h.DESCRIPCION
                                     }).OrderBy(x => x.FechaHistorial).ToList();
                        //Retornamos la lista.
                        return Lista;
                    }
                    //Si el id_tipo es cero, sólo se buscara por estatus y departamento.
                     else if (id_tipo == 0)
                    {
                            //Se realiza la consulta y se le asigna a una variable.
                            var Lista = (from h in Conexion.TBL_HISTORIAL_VERSION
                                         join d in Conexion.TBL_DOCUMENTO on h.NOMBRE_DOCUMENTO equals d.NOMBRE
                                         join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                         join dep in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals dep.ID_DEPARTAMENTO
                                         where DbFunctions.TruncateTime(h.FECHA) >= fecha_inicio && DbFunctions.TruncateTime(h.FECHA) <= fecha_fin && h.DESCRIPCION.Contains(descripcion) && dep.ID_DEPARTAMENTO == id_departamento
                                         select new
                                         {
                                             d.NOMBRE,
                                             h.NO_VERSION,
                                             t.TIPO_DOCUMENTO,
                                             dep.NOMBRE_DEPARTAMENTO,
                                             FechaHistorial = h.FECHA,
                                             h.DESCRIPCION
                                         }).OrderBy(x => x.FechaHistorial).ToList();
                        //Retornamos la lista.
                            return Lista;
                    }
                    //Si el id_tipo no es cero, se busca por tipo y estatus.
                    else
                    {
                        //Se realiza la consulta y se asigna a una variable.
                        var Lista = (from h in Conexion.TBL_HISTORIAL_VERSION
                                        join d in Conexion.TBL_DOCUMENTO on h.NOMBRE_DOCUMENTO equals d.NOMBRE
                                        join t in Conexion.TBL_TIPO_DOCUMENTO on d.ID_TIPO_DOCUMENTO equals t.ID_TIPO_DOCUMENTO
                                        join dep in Conexion.TBL_DEPARTAMENTO on d.ID_DEPARTAMENTO equals dep.ID_DEPARTAMENTO
                                        where DbFunctions.TruncateTime(h.FECHA) >= fecha_inicio && DbFunctions.TruncateTime(h.FECHA) <= fecha_fin && h.DESCRIPCION.Contains(descripcion) && t.ID_TIPO_DOCUMENTO == id_tipo
                                        select new
                                        {
                                            d.NOMBRE,
                                            h.NO_VERSION,
                                            t.TIPO_DOCUMENTO,
                                            dep.NOMBRE_DEPARTAMENTO,
                                            FechaHistorial = h.FECHA,
                                            h.DESCRIPCION
                                        }).OrderBy(x => x.FechaHistorial).ToList();
                        //Retornamos la lista.
                        return Lista;
                    }
                }
            }
            catch (Exception)
            {
                //Si hay error retorna nulo.
                return null;
            }
        }

        /// <summary>
        /// Método que retorna la cantidad de documentos dependiendo de los parámetros recibido.
        /// </summary>
        /// <param name="fecha_inicio"></param>
        /// <param name="fecha_fin"></param>
        /// <param name="descripcion"></param>
        /// <param name="id_departamento"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public DataSet GetCountHistorial(DateTime fecha_inicio, DateTime fecha_fin, string descripcion, int id_departamento, int id_tipo)
        {
            DataSet datos = new DataSet();
            try
            {
                //Se crea conexion a la BD.
                Desing_SQL conexion = new Desing_SQL();

                //Se inicializa un dictionario que contiene propiedades de tipo string y un objeto.
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                string fInicio = fecha_inicio.Year + "-" + fecha_inicio.Month + "-" + fecha_inicio.Day;
                string fFinal = fecha_fin.Year + "-" + fecha_fin.Month + "-" + fecha_fin.Day;

                //se agregan el nombre y el objeto de los parámetros.
                parametros.Add("fecha_inicio", fInicio);
                parametros.Add("fecha_fin", fFinal);
                parametros.Add("estatus", descripcion);
                parametros.Add("id_dep", id_departamento);
                parametros.Add("id_tipo", id_tipo);

                //se ejecuta el procedimiento y se mandan los parámetros añadidos anteriormente.
                datos = conexion.EjecutarStoredProcedure("SP_CIT_Get_HistorialCantidad", parametros);
            }
            catch (Exception)
            {
                //si hay error, retorna la tabla vacía.
                return datos;
            }
            //Retorna el número de elementos en la tabla.
            return datos;
        }

        /// <summary>
        /// Metodo que obtiene de un determinado mes anterior los documentos liberados dependiedo si fueron por proceso manual(Procedimientos, MIE, Formatos) o automático (Ayudas visuales, HII,HOE,JES)
        /// </summary>
        /// <param name="proceso"></param>
        /// <param name="mes"></param>
        /// <returns></returns>
        public DataSet GetDocumentosProceso(string proceso, int mes)
        {
            DataSet datos = new DataSet();
            try
            {
                //Se crea conexion a la BD.
                Desing_SQL conexion = new Desing_SQL();

                //Se inicializa un dictionario que contiene propiedades de tipo string y un objeto.
                Dictionary<string, object> parametros = new Dictionary<string, object>();

                //se agregan el nombre y el objeto de los parámetros.
                parametros.Add("proceso", proceso);
                parametros.Add("mes", mes);

                //se ejecuta el procedimiento y se mandan los parámetros añadidos anteriormente.
                datos = conexion.EjecutarStoredProcedure(SP_CIT_GET_DOCUMENTOS_LIBERADOS, parametros);

                return datos;
            }
            catch (Exception)
            {
                //Retorna el número de elementos en la tabla.
                return datos;
            }
        }

        /// <summary>
        /// Método el cual indica si un número de archivo existe.
        /// </summary>
        /// <param name="numeroDocumento"></param>
        /// <returns></returns>
        public bool ExistDocumento(string numeroDocumento)
        {
            try
            {
                //Realizamos la conexión a la base de datos.
                using (var Conexion = new EntitiesControlDocumentos())
                {
                    //Realizamos la consulta para contar el número de documentos que tengan el mismo número.
                    return Conexion.TBL_DOCUMENTO.Where(x => x.NOMBRE.Equals(numeroDocumento)).ToList().Count > 0 ? true : false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Método que obtiene el estatus de los documentos de un usuario
        /// </summary>
        /// <param name="IdUsuario"></param>
        /// <returns></returns>
        public IList GetDocumentoEstatus(string IdUsuario,string DocumentoBuscar)
        {
            try
            {
                using (var conexion = new EntitiesControlDocumentos())
                {
                    var lista = (from a in conexion.TBL_DOCUMENTO
                                 join b in conexion.TBL_VERSION on a.ID_DOCUMENTO equals b.ID_DOCUMENTO
                                 join d in conexion.Usuarios on b.ID_USUARIO_ELABORO equals d.Usuario
                                 join c in conexion.TBL_ESTATUS_VERSION on b.ID_ESTATUS_VERSION equals c.ID_ESTATUS_VERSION
                                 where b.ID_USUARIO_ELABORO == IdUsuario &&
                                 a.NOMBRE.Contains(DocumentoBuscar) &&
                                 b.ID_ESTATUS_VERSION != 1 &&
                                 b.ID_ESTATUS_VERSION != 2
                                 orderby c.ESTATUS_VERSION descending
                                 select new
                                 {
                                     a.ID_DOCUMENTO,
                                     a.NOMBRE,
                                     b.No_VERSION,
                                     b.FECHA_VERSION,
                                     c.ESTATUS_VERSION,
                                     c.ID_ESTATUS_VERSION
                                 }).ToList();

                    return lista;
                }
            }
            catch (Exception)
            {
                //si hay error retornamos nulo
                return null;
            }
        }

        /// <summary>
        /// Método que obtiene los documentos obsoletos para poder liberar espacio en la base de datos
        /// </summary>
        /// <param name="TextoBuscar"></param>
        /// <returns></returns>
        public IList GetDocumentosObsoletos(int IdVersion)
        {
            try
            {
                using (var conexion = new EntitiesControlDocumentos())
                {
                    var lista = (from a in conexion.TBL_DOCUMENTO
                                 join b in conexion.TBL_VERSION on a.ID_DOCUMENTO equals b.ID_DOCUMENTO
                                 join c in conexion.TBL_ARCHIVO on b.ID_VERSION equals c.ID_VERSION
                                 where b.ID_ESTATUS_VERSION == 2 && b.ID_VERSION == IdVersion
                                 orderby a.NOMBRE
                                 select new
                                 {
                                     a.NOMBRE,
                                     a.ID_TIPO_DOCUMENTO,
                                     b.No_VERSION,
                                     c.ARCHIVO,
                                     c.EXT,
                                     c.ID_ARCHIVO

                                 }
                                 ).Take(100).ToList();

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