using DataAccess.ServiceObjects.ControlDocumentos;
using DataAccess.ServiceObjects.Notificaciones;
using System;
using Encriptar;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using DataAccess.ServiceObjects.Usuario;
using DataAccess.ServiceObjects;

namespace Model.ControlDocumentos
{
    public static class DataManagerControlDocumentos
    {
        #region Archivo
        /// <summary>
        /// Método para obtener los registros de la BD.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Archivo> GetArchivo()
        {
            //Se inicializan los servicios de Archivo.
            SO_Archivo ServiceArchivo = new SO_Archivo();

            //Se crea una lista de tipo archivo, la cual se va a retornar.
            ObservableCollection<Archivo> Lista = new ObservableCollection<Archivo>();

            //obtenemos todo de la BD.
            IList ObjArchivo = ServiceArchivo.GetArchivo();

            //Verificamos que la informacion no esté vacía.
            if (ObjArchivo != null)
            {
                foreach (var item in ObjArchivo)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Archivo obj = new Archivo();

                    obj.id_archivo = (int)tipo.GetProperty("ID_ARCHIVO").GetValue(item, null);
                    obj.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    obj.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE_ARCHIVO").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que obtiene el archivo de un documento seleccionado
        /// </summary>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public static List<Archivo> GetArchivoFiltrado(int id_version)
        {

            SO_Archivo ServiceArchivo = new SO_Archivo();

            List<Archivo> documento = new List<Archivo>();
            IList ObjArchivo = ServiceArchivo.GetArchivoFiltrado(id_version);

            if (ObjArchivo != null)
            {
                foreach (var item in ObjArchivo)
                {
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Archivo obj = new Archivo();

                    obj.id_archivo = (int)tipo.GetProperty("ID_ARCHIVO").GetValue(item, null);
                    obj.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    obj.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE_ARCHIVO").GetValue(item, null);

                    documento.Add(obj);
        
                }
            }
            return documento;
        }

        public static List<Archivo> GetArchivoFiltrado(string CodigoValidacion)
        {

            SO_Archivo ServiceArchivo = new SO_Archivo();

            List<Archivo> documento = new List<Archivo>();
            IList ObjArchivo = ServiceArchivo.GetArchivoFiltrado(CodigoValidacion);

            if (ObjArchivo != null)
            {
                foreach (var item in ObjArchivo)
                {
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Archivo obj = new Archivo();

                    obj.id_archivo = (int)tipo.GetProperty("ID_ARCHIVO").GetValue(item, null);
                    obj.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    obj.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE_ARCHIVO").GetValue(item, null);

                    documento.Add(obj);

                }
            }
            return documento;
        }

        /// <summary>
        /// Método que inserta un registro a la tabla TBL_Archivo
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public static Task<int> SetArchivo(Archivo archivo)
        {
            //Inicializamos los servicios de Archivo.
            SO_Archivo ServiceArchivo = new SO_Archivo();

            //Se ejecuta el método y regresa el id del archivo insertado.
            return ServiceArchivo.SetArchivo(archivo.id_version, archivo.archivo, archivo.ext, archivo.nombre);
        }

        /// <summary>
        /// Método para modificar un registro de la tabla archvio.
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public static int UpdateArchivo(Archivo archivo)
        {

            //Inicializamos los servicios de Archivo.
            SO_Archivo ServiceArchivo = new SO_Archivo();

            //Se ejecuta el método y regresa los registros modificados.
            return ServiceArchivo.UpdateArchivo(archivo.id_archivo, archivo.id_version, archivo.archivo, archivo.ext, archivo.nombre);
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla.
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public static int DeleteArchivo(Archivo archivo)
        {
            //Inicializamos los servicios de Archivo
            SO_Archivo ServiceArchivo = new SO_Archivo();

            //Se ejecuta el método y regresamos los registros afectados.
            return ServiceArchivo.DeleteArchivo(archivo.id_archivo);

        }

        /// <summary>
        /// Método para obtener el archivo de la BD.
        /// </summary>
        /// <param name="id_archivo"></param>
        public static  void GetFile(int id_archivo)
        {
            //Se inicializan los servicios de Archivo.
            SO_Archivo ServiceArchivo = new SO_Archivo();

            //obtenemos la extensión y el archivo con el id que se recibió.
            IList ObjArchivo = ServiceArchivo.GetByte(id_archivo);

            //Creamos un objeto de tipo archivo
            Archivo obj = new Archivo();

            //Se verifica que la información extraída de la BD no esté vacía.
            if (ObjArchivo != null)
            {
                foreach (var item in ObjArchivo)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Agregamos el objeto a la lista resultante.
                    obj.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    obj.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);
                }
            }

            //Se guarda la ruta del directorio temporal.
            var tempFolder = Path.GetTempPath();
            //se asigna el nombre del archivo temporal, se concatena el nombre y la extensión.
            string filename = Path.Combine(tempFolder, "temp" + obj.ext);
            //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
            File.WriteAllBytes(filename, obj.archivo);

            //Se inicializa el programa para visualizar el archivo.
            Process.Start(filename);

        }

        #endregion

        #region Departamento

        /// <summary>
        /// Método para obtener todos los registros de la tabla TBL_departamento
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Departamento> GetDepartamento()
        {

            //Se inicializan los servicios de Departamento.
            SO_Departamento ServiceDepartamento = new SO_Departamento();

            //Se crea una lista de tipo departamento, la cual se va a retornar.
            ObservableCollection<Departamento> Lista = new ObservableCollection<Departamento>();

            //obtenemos todo de la BD.
            IList ObjDepartamento = ServiceDepartamento.GetDepartamento();

            //Verificamos que la informacion no esté vacía.
            if (ObjDepartamento != null)
            {
                foreach (var item in ObjDepartamento)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();
                    //Declaramos un objeto  que contendrá la información de un registro.
                    Departamento obj = new Departamento();

                    //Asignamos los valores correspondientes.
                    obj.id_dep = (int)tipo.GetProperty("ID_DEPARTAMENTO").GetValue(item, null);
                    obj.nombre_dep = (string)tipo.GetProperty("NOMBRE_DEPARTAMENTO").GetValue(item, null);
                    obj.fecha_creacion = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    obj.fecha_actualizacion = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
                    obj.Abreviatura = (string)tipo.GetProperty("ABREVIATURA").GetValue(item, null);
                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método para insertar un registro en la tabla deparatamento.
        /// </summary>
        /// <param name="departamento"></param>
        /// <returns></returns>
        public static int SetDepartamento(Departamento departamento)
        {
            //Se inician los servicios de ]Departamento.
            SO_Departamento ServiceDepartamento = new SO_Departamento();

            //Se ejecuta el método y retorna el id del departamento que fue insertado.
            return ServiceDepartamento.SetDepartamento(departamento.id_dep, departamento.nombre_dep, departamento.Abreviatura, departamento.fecha_creacion, departamento.fecha_actualizacion);
        }

        /// <summary>
        /// Método para modificar un registro de la tabla deparatamento.
        /// </summary>
        /// <param name="departamento"></param>
        /// <returns></returns>
        public static int UpdateDepartamento(Departamento departamento)
        {
            //inicializamos los servicios de Departamento.
            SO_Departamento ServiceDepartamento = new SO_Departamento();

            // Se ejecuta el método y retorna los registros que se modificaron.
            return ServiceDepartamento.UpdateDepartamento(departamento.id_dep, departamento.nombre_dep, departamento.fecha_creacion, departamento.fecha_actualizacion);
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla deparatamento.
        /// </summary>
        /// <param name="departamento"></param>
        /// <returns></returns>
        public static int DeleteDepartamento(Departamento departamento)
        {
            //Se inicializan los servicios.
            SO_Departamento ServiceDepartamento = new SO_Departamento();

            // Se ejecuta el método y retorna número de registros eliminados.
            return ServiceDepartamento.DeleteDepartamento(Convert.ToInt32(departamento.id_dep));

        }

        /// <summary>
        /// Método para validar si el departamento existe
        /// </summary>
        /// <param name="departamento"></param>
        /// <returns></returns>
        public static int ValidateDepartamento(Departamento departamento)
        {
            //Se inicializan los servicios.
            SO_Departamento ServiceDepartamento = new SO_Departamento();

            //obtenemos todo de la BD.
            IList ObjDepartamento = ServiceDepartamento.GetDepartamento();

            //Verificamos que la informacion no esté vacía.
            if (ObjDepartamento != null)
            {
                foreach (var item in ObjDepartamento)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();
                    //Declaramos un objeto  que contendrá la información de un registro.
                    Departamento obj = new Departamento();

                    //Asignamos los valores correspondientes.
                    obj.id_dep = (int)tipo.GetProperty("ID_DEPARTAMENTO").GetValue(item, null);
                    obj.nombre_dep = (string)tipo.GetProperty("NOMBRE_DEPARTAMENTO").GetValue(item, null);
                    obj.fecha_creacion = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    obj.fecha_actualizacion = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
                    obj.Abreviatura = (string)tipo.GetProperty("ABREVIATURA").GetValue(item, null);

                    //Quitamamos los acentos del nombre de departamento iterado
                    string nombreSinAcentos = DeleteAccents(obj.nombre_dep);

                    //Compara el objeto iterado con el objeto que se recibió
                    if (nombreSinAcentos.Contains(DeleteAccents(departamento.nombre_dep)) || obj.Abreviatura.Equals(departamento.Abreviatura))
                    {
                        //Si el nombre del deptarmento o la abreviatura son iguales, devuleve el id
                        return obj.id_dep;
                    }
                }
            }
            //regremos cero, si no encontro registros.
            return 0;

        }

        #endregion

        #region Documento
        /// <summary>
        /// Método para obtener los registros de todos los documentos
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumentos()
        {
            //Se inicializan los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //obtenemos todo de la BD.
            IList ObjDocumento = ServiceDocumento.GetDocumento();

            //Verificamos que la informacion no esté vacía.
            if (ObjDocumento != null)
            {
                foreach (var item in ObjDocumento)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.id_tipo_documento = (int)tipo.GetProperty("ID_TIPO_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    //obj.descripcion = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                    obj.fecha_emision = (DateTime)tipo.GetProperty("FECHA_EMISION").GetValue(item, null);
                    obj.fecha_creacion = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    obj.fecha_actualizacion = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
                    obj.usuario = (string)tipo.GetProperty("Usuario").GetValue(item, null);
                    obj.id_estatus = (int)tipo.GetProperty("ID_ESTATUS_DOCUMENTO").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// metodo para contar los documentos de un usario en especifico
        /// si tiene documentos no se puede eliminar el usuario, solo se eliminan los privilegios y perfiles
        /// si no tiene documentos se elimina
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static bool ContarDocumentos(string usuario)
        {
            SO_Documento ServiceDocumento = new SO_Documento();
            ObservableCollection<Documento> lista = new ObservableCollection<Documento>();

            IList objdocumento = ServiceDocumento.GetDocumentosUsuario(usuario);
            if (objdocumento.Count != 0)
            {
                //si tiene documentos
                return true;
            }else
            {
                //si no tiene documentos
                return false;
            }
        }

        /// <summary>
        /// Método para insertar un registro de la tabla TBL_Documento.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public static int SetDocumento(Documento documento)
        {
            //Se inician los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se ejecuta el método y retorna el id del documento que fue insertado.
            return ServiceDocumento.SetDocumento(documento.id_tipo_documento, documento.id_dep, documento.nombre,
                                                    documento.id_estatus, documento.usuario, documento.fecha_creacion);
        }

        /// <summary>
        /// Método para modificar un registro de la tabla TBL_Documento.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public static int UpdateDocumento(Documento documento)
        {
            //Se inician los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            // Se ejecuta el método y retorna los registros que se modificaron.
            return ServiceDocumento.UpdateDocumento(documento.id_documento, documento.id_tipo_documento, documento.id_dep,
                                                 documento.fecha_actualizacion, documento.id_estatus, documento.fecha_emision, documento.usuario);
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla TB_Documento.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public static int DeleteDocumento(Documento documento)
        {
            //Se inician los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            // Se ejecuta el método y retorna los registros que se eliminaron.
            return ServiceDocumento.DeleteDocumento(documento.id_documento);
        }

        /// <summary>
        /// Método que obtiene los documentos liberados de acuerdo al tipo de documento
        /// Llena el grid de ControlDocumento
        /// </summary>
        /// <param name="idTipoDocumento"></param>
        /// <param name="textoBusqueda"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumentoByTipoDocumento(int idTipoDocumento, string textoBusqueda)
        {
            //Se inicializan los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //obtenemos todo de la BD.
            IList ObjDocumento = ServiceDocumento.GetDataGrid(idTipoDocumento, textoBusqueda);

            //Verificamos que la informacion no esté vacía.
            if (ObjDocumento != null)
            {
                foreach (var item in ObjDocumento)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.fecha_actualizacion = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
                    obj.id_dep = (int)tipo.GetProperty("ID_DEPARTAMENTO").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.version.no_copias = (int)tipo.GetProperty("NO_COPIAS").GetValue(item, null);
                    obj.descripcion = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null).ToString().ToUpper();
                    obj.Departamento = (string)tipo.GetProperty("NOMBRE_DEPARTAMENTO").GetValue(item, null);
                    obj.fecha_emision = (DateTime)tipo.GetProperty("FECHA_EMISION").GetValue(item, null);
                    obj.version.nombre_usuario_elaboro = (string)tipo.GetProperty("USUARIO_ELABORO").GetValue(item, null);
                    obj.version.nombre_usuario_autorizo = (string)tipo.GetProperty("USUARIO_AUTORIZO").GetValue(item, null);
                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que obtiene todos los documentos liberados
        /// Llena el DataGrid del Frm_Busqueda_documentos
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetGridDocumentos(string texto)
        {
            //Se inicializan los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //obtenemos todo de la BD.
            IList ObjDocumento = ServiceDocumento.GetGridDocumentos(texto);

            //Verificamos que la informacion no esté vacía.
            if (ObjDocumento != null)
            {
                foreach (var item in ObjDocumento)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.version.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);
                    obj.id_dep = (int)tipo.GetProperty("ID_DEPARTAMENTO").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.version.no_copias = (int)tipo.GetProperty("NO_COPIAS").GetValue(item, null);
                    obj.descripcion = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                    obj.Departamento = (string)tipo.GetProperty("NOMBRE_DEPARTAMENTO").GetValue(item, null);
                    obj.fecha_emision = (DateTime)tipo.GetProperty("FECHA_EMISION").GetValue(item, null);
                    obj.tipo.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.usuario = (string)tipo.GetProperty("USUARIO_ELABORO").GetValue(item, null);
                    obj.usuario_autorizo = (string)tipo.GetProperty("USUARIO_AUTORIZO").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método para obtener el tipo y el archivo de un documento y una version.
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetArchivos(int id_documento, int id_version)
        {
            //Se inicializan los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //obtenemos todo de la BD.
            IList ObjDocumento = ServiceDocumento.GetTipo(id_documento, id_version);

            //Verificamos que la informacion no esté vacía.
            if (ObjDocumento != null)
            {
                foreach (var item in ObjDocumento)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = id_documento;
                    obj.nombre = (string)tipo.GetProperty("NOMBRE_ARCHIVO").GetValue(item, null);
                    obj.id_tipo_documento = (int)tipo.GetProperty("ID_TIPO_DOCUMENTO").GetValue(item, null);
                    obj.tipo.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.version.archivo.id_archivo = (int)tipo.GetProperty("ID_ARCHIVO").GetValue(item, null);
                    obj.version.archivo.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);
                    obj.version.archivo.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    obj.Departamento = (string)tipo.GetProperty("NOMBRE_DEPARTAMENTO").GetValue(item, null);
                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que obtiene el nombre de un documento
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetNombre_Documento(int id_documento)
        {
            //Se inicializan los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //obtenemos todo de la BD.
            IList ObjDocumento = ServiceDocumento.GetNombre(id_documento);

            //Verificamos que la informacion no esté vacía.
            if (ObjDocumento != null)
            {
                foreach (var item in ObjDocumento)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;

        }

        /// <summary>
        /// Método para actualizar la versión actual en la tbl documento.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public static int UpdateFecha_actualizacion(int id_doc)
        {
            //Se inician los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            DateTime fecha_sist = Get_DateTime();
            // Se ejecuta el método y retorna los registros que se modificaron.
            return ServiceDocumento.UpdateFecha_Actualizacion(id_doc, fecha_sist);
        }

        /// <summary>
        /// Método para obtener el último número de un documento
        /// </summary>
        /// <param name="tipoDocumento"></param>
        /// <param name="departamento"></param>
        /// <returns></returns>
        public static string GetNumero(TipoDocumento tipoDocumento, Departamento departamento)
        {
            //concatenamos la abreviatura del tipo y del departamento.
            string numero = string.Concat(tipoDocumento.abreviatura, departamento.Abreviatura);

            //Se inician los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            // Se ejecuta el método y retonamos el número generado.
            return ServiceDocumento.GetNumero(numero, tipoDocumento.id_tipo, departamento.id_dep);
        }

        /// <summary>
        /// Método que obtiene una lista de los documentos sin versión de un usuario.
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumento_SinVersion(string id_usuario)
        {
            //Se inicializan los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //obtenemos todo de la BD.
            IList ObjDocumento = ServiceDocumento.GetDocumento_Version(id_usuario);

            //Verificamos que la informacion no esté vacía.
            if (ObjDocumento != null)
            {
                foreach (var item in ObjDocumento)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.id_tipo_documento = (int)tipo.GetProperty("ID_TIPO_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.tipo.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.id_dep = (int)tipo.GetProperty("ID_DEPARTAMENTO").GetValue(item, null);
                    obj.Departamento = (string)tipo.GetProperty("NOMBRE_DEPARTAMENTO").GetValue(item, null);
                    obj.id_estatus = (int)tipo.GetProperty("ID_ESTATUS_DOCUMENTO").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que obtiene la información de la versión y de un documento en específico
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static Documento GetDocumento(int id_documento, string version)
        {
            //Se inicializan los servicios de Documento.
            SO_Documento ServicioDocumento = new SO_Documento();

            //Se crea un objeto de tipo documento
            Documento documento = new Documento();

            //Obtenemos la información de la BD.
            IList informacionBD = ServicioDocumento.GetDocumentoVersion(id_documento, version);

            //Si la información es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista que se obtuvo
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Asignamos los valores correspondientes.
                    documento.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    documento.id_tipo_documento = (int)tipo.GetProperty("ID_TIPO_DOCUMENTO").GetValue(item, null);
                    documento.id_estatus = (int)tipo.GetProperty("ID_ESTATUS_DOCUMENTO").GetValue(item, null);
                    documento.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    documento.descripcion = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                    documento.fecha_emision = (DateTime)tipo.GetProperty("FECHA_EMISION").GetValue(item, null);
                    documento.fecha_creacion = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    documento.fecha_actualizacion = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
                    documento.version.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    documento.version.id_estatus_version = (int)tipo.GetProperty("ID_ESTATUS_VERSION").GetValue(item, null);
                    documento.version.id_usuario = (string)tipo.GetProperty("ID_USUARIO_ELABORO").GetValue(item, null);
                    documento.version.id_usuario_autorizo = (string)tipo.GetProperty("ID_USUARIO_AUTORIZO").GetValue(item, null);
                    documento.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    documento.version.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);
                    documento.version.no_copias = (int)tipo.GetProperty("NO_COPIAS").GetValue(item, null);
                    documento.id_dep = (int)tipo.GetProperty("ID_DEPARTAMENTO").GetValue(item, null);
                    documento.Departamento = (string)tipo.GetProperty("DEPARTAMENTO").GetValue(item, null);

                }
            }

            //Retornamos el objeto
            return documento;
        }

        /// <summary>
        /// Método para actualizar el estado de un documento
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="id_estatus"></param>
        /// <returns></returns>
        public static int Update_EstatusDocumento(Documento obj)
        {
            //Se inicializan los servicios de Documento.
            SO_Documento ServicioDocumento = new SO_Documento();

            //Se ejecuta el método y retornamos el resultado
            return ServicioDocumento.UpdateEstatus_Documento(obj.id_documento, obj.id_estatus);
        }

        /// <summary>
        /// Método que indica si un número de documento existe.
        /// </summary>
        /// <param name="numeroDocumento"></param>
        /// <returns></returns>
        public static bool ExistDocumento(string numeroDocumento)
        {
            //Inicializamos los servicios de SO_Documento.
            SO_Documento servicioDocumento = new SO_Documento();

            //Ejecutamos el método y retornamos el valor.
            return servicioDocumento.ExistDocumento(numeroDocumento);
        }
        //excel

        /// <summary>
        /// Método que regresa el id del departamento
        /// Se usa para importar a excel
        /// </summary>
        /// <param name="nombre_dep"></param>
        /// <returns></returns>
        public static int GetID_Dep(string nombre_dep)
        {
            //Se inicializan los servicios.
            SO_Departamento ServiceDepartamento = new SO_Departamento();

            return ServiceDepartamento.GetID_Departamento(nombre_dep);

        }

        /// <summary>
        /// Método para insertar documentos a la base de datos
        /// Se usa para importar desde excel
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public static int InsertDocumentos(Documento documento)
        {
            //Se inician los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            return ServiceDocumento.InsertDocumentos(documento.id_tipo_documento, documento.id_dep, documento.nombre, documento.fecha_emision, documento.fecha_actualizacion, documento.id_estatus,
                                                documento.usuario);
        }

        /// <summary>
        /// Método que valída si una descripción ya existe.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> ValidateDescripcionIgual(Documento doc)
        {
            //Se inicializa los servicios de documento
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            // obtenemos todo de la BD.
            IList ObjDocumento = ServiceDocumento.ValidateIgualDescripcion(doc.id_tipo_documento, doc.id_dep, doc.id_documento, doc.descripcion);

            //Si la Lista de documetnos es diferente de nulo.
            if (ObjDocumento != null)
            {
                //Iteramos la lista que se obtuvo
                foreach (var item in ObjDocumento)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos el objeto 
                    Documento obj = new Documento();

                    //Asigamos los valores
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.version.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.version.descripcion_v = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);

                    Lista.Add(obj);

                }
            }
            //Retornamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método para obtener las coincidencias de una descripción de documento.
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> ValidateDocumentosSimilares(Documento doc)
        {
            //Se inicializa los servicios de documento
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //Eliminamos los acentos a la descripción recibida.
            string descripcion = DeleteAccents(doc.descripcion);

            //obtenemos un vector sin espacios de la descripción
            string[] aux = descripcion.Split(' ');

            //Ejecutamos el método para eliminar palabras de menos de 3 caracteres
            aux = (RemoveChacters(aux));

            //Obtenemos todo de la BD.
            IList ObjDocumento = ServiceDocumento.ValidateDescripcion(doc.id_tipo_documento, doc.id_dep, doc.id_documento);

            //Si la lista de Documentos es diferento de nulo.
            if (ObjDocumento != null)
            {
                //Iteramos la lista que se obtuvo
                foreach (var item in ObjDocumento)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos el objeto 
                    Documento obj = new Documento();

                    //Inicializamos el contador, de las palabar similares.
                    int cont = 0;

                    //Asigamos los valores
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.version.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.version.descripcion_v = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);

                    //Se manda a llamar a la función para eliminar acentos de la versión iterada.
                    string descVersion = DeleteAccents(obj.version.descripcion_v);

                    //Se obtiene un vector sin espacios, de la descripcion de la versión iterada.
                    string[] vec = descVersion.Split(' ');

                    //Ejecutamos el método para eliminar palabras con  menos de 3 caracteres de la descripción de la versión iterada.
                    vec = RemoveChacters(vec);

                    //Si el vector resultante es mayor a cero.
                    if (vec.Length > 0)
                    {
                        //Recorremos el vector de la descripción que se va a dar de alta
                        for (int i = 0; i < aux.Length; i++)
                        {
                            //Recorremos el vector de la descripción de la versión que se obtuvo
                            for (int k = 0; k < vec.Length; k++)
                            {
                                //Comparamos si son iguales las palabras ignorando las mayúsculas
                                if (aux[i].Equals(vec[k], StringComparison.InvariantCultureIgnoreCase) || Compara(aux[i], vec[k]))
                                {
                                    //Incrementamos el contador, rompemos el ciclo.
                                    cont++;
                                    break;
                                }
                            }
                        }

                        int resta = 0;

                        //Si la descripcion que se va a dar de alta es mayor a la descripción de la versión iterada
                        if (aux.Length > vec.Length)
                        {
                            //restamos el tamaño del vector auxiliar menos el tamaño del vector vec
                            resta = aux.Length - vec.Length;
                        }
                        //si es mayor el vector de la descripción iterada
                        else
                            resta = vec.Length - aux.Length;

                        //calculamos el porcentaje de coincidencia
                        int porcentaje = (cont * 100) / vec.Length;

                        //si el porcentaje es mayor a 80% y si tiene una diferencia mayor a 4 palabras, la descripción se parece y agregamos el objeto a la lista
                        if (porcentaje >= 80 && resta <= 4)
                        {
                            //Se agrega el objeto a la lista
                            Lista.Add(obj);
                        }
                    }
                }
            }
            return Lista;
        }

        /// <summary>
        /// Función que compara dos palabras, para verificar si dos documentos son similares.
        /// </summary>
        /// <param name="w1"></param>
        /// <param name="w2"></param>
        /// <returns></returns>
        private static bool Compara(string w1, string w2)
        {
            //inicializa el contador
            int contador = 0;

            //recorre la primera palabara
            for (int i = 0; i < w1.Length; i++)
            {
                //recorre la segunda palabra
                for (int j = 0; j < w2.Length; j++)
                {
                    //realiza la comparación ignorado mayúsculas y minúsculas
                    if (w1[i].ToString().Equals(w2[j].ToString(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        //si son iguales el caractér en la posición i y en la posición j
                        //suma el contador y rompe el ciclo
                        contador++;
                        break;
                    }
                }
            }

            //Si el caractér es un espacio
            if (w1.Length == 0)
                return false;

            int tam = 0;

            //si la primer palabra es mayor a la segunda, guarda el tamaño de la segunda palabra
            if (w1.Length > w2.Length)
                tam = w2.Length;
            else
                //Si el tamaño de la primer palabra es mas pequeño se guarda el tamaño
                tam = w1.Length;

            //calculamos el porcentaje de coincidencia
            int porciento = (contador * 100) / w1.Length;

            //si el porcentaje es mayor a 80%, la palabra es parecida
            if (porciento >= 80)
            {
                int aux = 0;
                int contador2 = 0;
                //Se agrega nueva validación, para mayor presicion de coicidencia de las palabras
                //Mientras el auxiliar sea menor al tamaño guardado
                do
                {
                    //si la letras coinciden en la misma posicion, sumamos el contador
                    if (w1[aux] == w2[aux])
                        contador2++;

                    //Sumamos el auxiliar
                    aux++;
                } while (aux < tam);

                //Calculamos de nuevo el porcentaje de coincidencia de las palabras 
                int porciento2 = (contador2 * 100) / tam;

                //si el porcentaje es mayor a 80
                if (porciento2 >= 80)
                    return true;
                else
                    return false;
            }
            else
                return false;

        }

        /// <summary>
        /// Función que devuelve una cadena sin acentos
        /// </summary>
        /// <param name="palabra"></param>
        /// <returns></returns>
        private static string DeleteAccents(string palabra)
        {
            //normalizamos la cadena
            string descripcion = palabra.Normalize(NormalizationForm.FormD);

            //Inicializamos una variable de la clase Stringbuilder
            var stringBuilder = new StringBuilder();

            //recorremos la cadena, para eliminar los acentos
            foreach (var c in descripcion)
            {
                var unicodeCategory = CharUnicodeInfo.GetUnicodeCategory(c);
                if (unicodeCategory != UnicodeCategory.NonSpacingMark)
                {
                    stringBuilder.Append(c);
                }
            }

            //se guarda el resultado de la cadena sin acentos
            return stringBuilder.ToString().Normalize(NormalizationForm.FormC);
        }

        /// <summary>
        /// Método que elimina de un vector las palabras que tengan menos de 3 caracteres
        /// </summary>
        /// <param name="vector"></param>
        /// <returns></returns>
        private static string[] RemoveChacters(string[] vector)
        {
            //recorre todo el vector
            for (int i = 0; i < vector.Length; i++)
            {
                //si el vector en la posicion i su tamaño es menor o igual a 3, se reemplaza por cadena vacia
                if (vector[i].Length <= 3)
                    vector[i] = string.Empty;
            }
            //Eliminamos las cadenas vacías del vector resultante
            vector = vector.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            //Retornamos el vector sin las palabras de menos de 3 caracteres
            return vector;
        }

        /// <summary>
        /// Obtiene los documentos vencidos de un usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumentos_Vencidos(string usuario)
        {
            //Se inicializa los servicios de documento
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            DateTime fecha_sist = Get_DateTime();
            IList ListaResul = ServiceDocumento.GetDocumentos_Vencidos(usuario, fecha_sist);

            //Si la lista es diferente de nulo
            if (ListaResul != null)
            {
                //Iteramos la lista
                foreach (var item in ListaResul)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos el objeto 
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.id_tipo_documento = (int)tipo.GetProperty("ID_TIPO_DOCUMENTO").GetValue(item, null);
                    obj.id_dep = (int)tipo.GetProperty("ID_DEPARTAMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.fecha_emision = (DateTime)tipo.GetProperty("FECHA_EMISION").GetValue(item, null);
                    obj.fecha_actualizacion = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
                    obj.version.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.version.descripcion_v = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.tipo.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.Departamento = (string)tipo.GetProperty("NOMBRE_DEPARTAMENTO").GetValue(item, null);
                    //Regresamos la lista
                    Lista.Add(obj);
                }
            }
            //Regresamos la lista
            return Lista;

        }

        /// <summary>
        /// Método que obtiene la fecha del servidor.
        /// </summary>
        /// <returns></returns>
        public static DateTime Get_DateTime()
        {
            //Se inicializa los servicios de documento
            SO_Documento ServiceDocumento = new SO_Documento();

            return ServiceDocumento.Get_DateTime();
        }

        /// <summary>
        /// Método que obtiene el historial de los documentos de acuerdo a los parámetros recibidos.
        /// </summary>
        /// <param name="objeto"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetHistorialDocumentos(DateTime fecha_inicio, DateTime fecha_fin, string estado, int id_dep, int id_tipo)
        {
            //Se inicializa los servicios de documento
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            IList ListaResul = ServiceDocumento.GetHistorial_Documentos(fecha_inicio, fecha_fin, estado, id_dep, id_tipo);

            //Si la lista es diferente de nulo
            if (ListaResul != null)
            {
                //Iteramos la lista
                foreach (var item in ListaResul)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos el objeto 
                    Documento obj = new Documento();

                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("NO_VERSION").GetValue(item, null);
                    obj.tipo.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.Departamento = (string)tipo.GetProperty("NOMBRE_DEPARTAMENTO").GetValue(item, null);
                    obj.fecha_actualizacion = (DateTime)tipo.GetProperty("FechaHistorial").GetValue(item, null);
                    obj.descripcion = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);

                    //Agregamos el objeto a la lista.
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que obtiene la fecha y cantidad de docuementos dependiendo del parámetro.
        /// </summary>
        /// <param name="fecha_inicio"></param>
        /// <param name="fecha_fin"></param>
        /// <param name="estado"></param>
        /// <param name="id_dep"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static ObservableCollection<HistorialVersion> GetCountDocumentos(DateTime fecha_inicio, DateTime fecha_fin, string estado, int id_dep, int id_tipo)
        {
            //Se inicializa los servicios de documento
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<HistorialVersion> Lista = new ObservableCollection<HistorialVersion>();

            //Obtenemos la propiedad de Cam_Detail.
            DataSet InformacionBD = ServiceDocumento.GetCountHistorial(fecha_inicio, fecha_fin, estado, id_dep, id_tipo);

            //Verificamos que el objeto recibido sea distinto de vacío.
            if (InformacionBD != null)
            {
                //Si la lista tiene información.
                if (InformacionBD.Tables.Count > 0 && InformacionBD.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow element in InformacionBD.Tables[0].Rows)
                    {
                        HistorialVersion obj = new HistorialVersion();

                        obj.fecha = Convert.ToDateTime(element["FECHA"].ToString());
                        obj.cantidad = Int32.Parse(element["CANTIDAD_LIBERADOS"].ToString());

                        Lista.Add(obj);
                    }
                }

            }
            //Retornamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que obtiene todos los documentos con su estatus correspondiente de un usuario
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumentosEstatus(string usuario, string DocumentoBuscar)
        {
            SO_Documento ServiceDocument = new SO_Documento();

            ObservableCollection<Documento> List = new ObservableCollection<Documento>();

            IList Data = ServiceDocument.GetDocumentoEstatus(usuario,DocumentoBuscar);

            if (Data != null)
            {
                foreach (var item in Data)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();
                    //Declaramos el objeto 
                    Documento obj = new Documento();

                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);
                    obj.version.estatus = (string)tipo.GetProperty("ESTATUS_VERSION").GetValue(item, null);
                    obj.version.id_estatus_version = (int)tipo.GetProperty("ID_ESTATUS_VERSION").GetValue(item, null);

                    List.Add(obj);

                }
            }
            //Regresamos la lista
            return List;
        }

        public static ObservableCollection<Documento> GetDocumentosObsoletos(int IdVersion)
        {
            SO_Documento servicio = new SO_Documento();

            ObservableCollection<Documento> list = new ObservableCollection<Documento>();

            IList Data = servicio.GetDocumentosObsoletos(IdVersion);

            if (Data != null)
            {
                foreach (var item in Data)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();
                    //Declaramos el objeto 
                    Documento obj = new Documento();

                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.archivo.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    obj.version.archivo.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);
                    obj.id_tipo_documento = (int)tipo.GetProperty("ID_TIPO_DOCUMENTO").GetValue(item, null);
                    obj.version.archivo.id_archivo = (int)tipo.GetProperty("ID_ARCHIVO").GetValue(item, null);

                    list.Add(obj);

                }
            }
            return list;
        }

        /// <summary>
        /// Método para eliminar un documento que contenga sello electronico cuando se modifique su estado a pendiente por corregir
        /// </summary>
        /// <param name="id_Version"></param>
        /// <returns></returns>
        public static int EliminarDocumentoSellado(int id_Version)
        {
            SO_Archivo Archivo = new SO_Archivo();
            return Archivo.ElimiarDocumentoSellado(id_Version);

        }

        /// <summary>
        /// Método que obtiene la fecha de la primera versión que se creo
        /// </summary>
        /// <param name="Id_Documento"></param>
        /// <returns></returns>
        public static string GetFechaPrimeraVersion(int Id_Documento)
        {
            SO_Version servicio = new SO_Version();

            return servicio.GetFechaPrimerVersion(Id_Documento);
        }

        public static ObservableCollection<TipoError> GetAllTipoError()
        {
            SO_TipoError errortipo = new SO_TipoError();

            ObservableCollection<TipoError> List = new ObservableCollection<TipoError>();

            IList Data = errortipo.GetAllTipoError();

            if (Data != null)
            {
                foreach (var item in Data)
                {
                    //Obtenemos el tipo
                    System.Type tipo = item.GetType();
                    //Declaramos el objeto 
                    TipoError obj = new TipoError();
                    obj.IsSelected = false;

                    obj.ID_NOTIFICACION_ERROR = (int)tipo.GetProperty("ID_NOTIFICACION_ERROR").GetValue(item, null);
                    obj.DESCRIPCION_ERROR = (string)tipo.GetProperty("DESCRIPCION_ERROR").GetValue(item, null);

                    List.Add(obj);
                }
            }

            //Regresamos la lista
            return List;
        }

        #endregion

        #region Rol
        /// <summary>
        /// Método para obetener los registros de la BD.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Rol> GetRol()
        {
            //Se inicializan los servicios
            SO_Rol ServiceRol = new SO_Rol();

            //Se crea una lista de tipo rol, la cual se va a retornar
            ObservableCollection<Rol> Lista = new ObservableCollection<Rol>();

            //obtenemos todo de la BD.
            IList ObjRol = ServiceRol.GetRol();

            //Verificamos que la informacion no esté vacía.
            if (ObjRol != null)
            {
                foreach (var item in ObjRol)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Rol obj = new Rol();

                    //Asignamos los valores correspondientes.
                    obj.id_rol = (int)tipo.GetProperty("ID_ROL").GetValue(item, null);
                    obj.nombre_rol = (string)tipo.GetProperty("NOMBRE_ROL").GetValue(item, null);
                    obj.fecha_creacion = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    obj.fecha_actualizacion = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método para insertar un registro a la BD.
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        public static int SetRol(Rol rol)
        {

            //Inicializamos los servicios
            SO_Rol ServiceRol = new SO_Rol();

            //Se ejecuta el método y retorna el id del rol
            return ServiceRol.SetRol(rol.id_rol, rol.nombre_rol, rol.fecha_creacion, rol.fecha_actualizacion);
        }

        /// <summary>
        /// Método para modificar un registro.
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        public static int UpdateRol(Rol rol)
        {
            //Inicializamos los servicios
            SO_Rol ServiceRol = new SO_Rol();

            // Se ejecuta el método y retorna los registros que se modificaron.
            return ServiceRol.UpdateRol(rol.id_rol, rol.nombre_rol, rol.fecha_creacion, rol.fecha_actualizacion);
        }

        /// <summary>
        /// Método para eliminar un registro.
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        public static int DeleteRol(Rol rol)
        {
            //Inicializamos los servicios
            SO_Rol ServiceRol = new SO_Rol();

            // Se ejecuta el método y retorna los registros que se eliminaron.
            return ServiceRol.DeleteRol(rol.id_rol);
        }

        /// <summary>
        /// Método que devuelve una lista de los roles que tiene un usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static ObservableCollection<Rol> GetRol_Usuario(string usuario)
        {
            //Se inicializan los servicios
            SO_Rol ServiceRol = new SO_Rol();

            //Se crea una lista de tipo rol, la cual se va a retornar
            ObservableCollection<Rol> Lista = new ObservableCollection<Rol>();

            //obtenemos todo de la BD.
            IList ObjRol = ServiceRol.GetRol_Usuario(usuario);

            //Verificamos que la informacion no esté vacía.
            if (ObjRol != null)
            {
                foreach (var item in ObjRol)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Rol obj = new Rol();

                    //Asignamos los valores correspondientes.
                    obj.id_rol = (int)tipo.GetProperty("ID_ROL").GetValue(item, null);
                    obj.nombre_rol = (string)tipo.GetProperty("NOMBRE_ROL").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que inserta los roles de cada usuario.
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>
        public static int SetRol_Usuario(Rol rol)
        {

            //Inicializamos los servicios
            SO_Rol ServiceRol = new SO_Rol();

            //Se ejecuta el método y retorna el id del rol
            return ServiceRol.SetRol_Usuario(rol.id_rol, rol.id_usuario);
        }

        /// <summary>
        /// metodo que elimina los roles de un usuario
        /// </summary>
        /// <param name="rol"></param>
        /// <returns></returns>

        public static int DeleteRol_Usuario(string IdUsuario)
        {
            SO_Rol ServiceRol = new SO_Rol();

            return ServiceRol.DeleteRolUsuario(IdUsuario);
        }
        #endregion

        #region TipoDocumento

        /// <summary>
        /// Método que obtiene el id de tipo de documento de un documento dado de alta.
        /// </summary>
        /// <param name="idDocumento">Entero que representa el id del documento.</param>
        /// <returns></returns>
        public static int GetTipoDocumento(int idDocumento)
        {
            //Inicializamos los servicios de Tipo de Documento.
            SO_TipoDocumento ServicioTipoDocumento = new SO_TipoDocumento();

            //Ejecutamos el método y el resultado lo retornamos.
            return ServicioTipoDocumento.GetTipoDocumento(idDocumento);
        }

        /// <summary>
        /// Retorna el nombre del tipo de documento
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static string GetNombretipo(int id_tipo)
        {
            //Inicializamos los servicios de Tipo de Documento.
            SO_TipoDocumento ServicioTipoDocumento = new SO_TipoDocumento();

            //Ejecutamos el método y el resultado lo retornamos.
            return ServicioTipoDocumento.GetNombreTipo(id_tipo);
        }

        /// <summary>
        /// Método para obtener los registros de la tabla.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<TipoDocumento> GetTipo()
        {
            //Se inicializan los servicios de Documento.
            SO_TipoDocumento ServiceTipo = new SO_TipoDocumento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<TipoDocumento> Lista = new ObservableCollection<TipoDocumento>();

            //obtenemos todo de la BD.
            IList ObjTipo = ServiceTipo.GetTipo();

            //Verificamos que la informacion no esté vacía.
            if (ObjTipo != null)
            {
                foreach (var item in ObjTipo)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    TipoDocumento obj = new TipoDocumento();

                    //Asignamos los valores correspondientes.
                    obj.id_tipo = (int)tipo.GetProperty("ID_TIPO_DOCUMENTO").GetValue(item, null);
                    obj.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.abreviatura = (string)tipo.GetProperty("ABREBIATURA").GetValue(item, null);
                    obj.fecha_creacion = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    obj.fecha_actualizacion = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
                    obj.num_matriz = (string)tipo.GetProperty("NUMERO_MATRIZ").GetValue(item, null);
                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que inserta un registro a la tabla TBL_Tipo.
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static int SetTipo(TipoDocumento tipo)
        {
            //Se inician los servicios de TipoDocumento.
            SO_TipoDocumento ServiceTipo = new SO_TipoDocumento();

            //Se ejecuta el método y retorna el id del tipo que fue insertado.
            return ServiceTipo.SetTipo(tipo.id_tipo, tipo.tipo_documento, tipo.abreviatura, tipo.fecha_creacion, tipo.fecha_actualizacion, tipo.num_matriz);
        }

        /// <summary>
        /// Método para modificar un registro en la tabla.
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static int UpdateTipo(TipoDocumento tipo)
        {
            //Se inician los servicios de TipoDocumento.
            SO_TipoDocumento ServiceTipo = new SO_TipoDocumento();

            //Se ejecuta el método y retorna el id del tipo que fue insertado.
            return ServiceTipo.UpdateTipo(tipo.id_tipo, tipo.tipo_documento, tipo.abreviatura, tipo.fecha_creacion, tipo.fecha_actualizacion);
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla.
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static int DeleteTipo(TipoDocumento tipo)
        {
            //Se inician los servicios de TipoDocumento.
            SO_TipoDocumento ServiceTipo = new SO_TipoDocumento();

            // Se ejecuta el método y retorna los registros que se eliminaron.
            return ServiceTipo.DeleteTipo(tipo.id_tipo);
        }

        /// <summary>
        /// Método para validar si existe el tipo de documento 
        /// </summary>
        /// <param name="tipo"></param>
        /// <returns></returns>
        public static int ValidateTipo(TipoDocumento tipoDoc)
        {
            SO_TipoDocumento ServiceTipo = new SO_TipoDocumento();

            //obtenemos todo de la BD.
            IList ObjTipo = ServiceTipo.GetTipo();

            //Verificamos que la informacion no esté vacía.
            if (ObjTipo != null)
            {
                foreach (var item in ObjTipo)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    TipoDocumento obj = new TipoDocumento();

                    //Asignamos los valores correspondientes.
                    obj.id_tipo = (int)tipo.GetProperty("ID_TIPO_DOCUMENTO").GetValue(item, null);
                    obj.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.abreviatura = (string)tipo.GetProperty("ABREBIATURA").GetValue(item, null);
                    obj.fecha_creacion = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    obj.fecha_actualizacion = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);

                    //Quita los acentos del tipo de documento
                    string tipoSinAcento = DeleteAccents(obj.tipo_documento);

                    //compara las cadenas sin acentos
                    if (tipoSinAcento.Contains(DeleteAccents(tipoDoc.tipo_documento)) || obj.abreviatura.Equals(tipoDoc.abreviatura))
                    {
                        //si el tipo de documento o la abreviatura son iguales, devuelve el id                      
                        return obj.id_tipo;
                    }
                }
            }
            //regresamos cero, no encontró ninguna coincidencia
            return 0;
        }
        #endregion  

        #region Usuarios

        /// <summary>
        /// Método para obtener todos los registros de la tabla Usuarios.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Usuarios> GetUsuarios(string Buscar)
        {


            //Inicializamos los servicios de usuarios.
            SO_Usuarios ServiceUsuarios = new SO_Usuarios();

            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Usuarios> Lista = new ObservableCollection<Usuarios>();

            Encriptacion encriptar = new Encriptacion();
            //Ejecutamos el método para obtener la información de la base de datos.
            string usuario_buscar = encriptar.encript(Buscar);
            IList ObjUsuarios = ServiceUsuarios.GetUsuario(usuario_buscar);

            //Comparamos que la información de la base de datos no sea nulo
            if (ObjUsuarios != null)

            {
                //Iteramos la información recibida.
                foreach (var item in ObjUsuarios)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos on objeto de tipo usuarios que contendrá la información de un registro.
                    Usuarios obj = new Usuarios();

                    //Asignamos los valores correspondientes.
                    obj.usuario = encriptar.desencript((string)tipo.GetProperty("Usuario").GetValue(item, null));
                    obj.password = (string)tipo.GetProperty("Password").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("Nombre").GetValue(item, null);
                    obj.APaterno = (string)tipo.GetProperty("APaterno").GetValue(item, null);
                    obj.AMaterno = (string)tipo.GetProperty("AMaterno").GetValue(item, null);
                    obj.estado = (int)tipo.GetProperty("Estado").GetValue(item, null);
                    obj.usql = (string)tipo.GetProperty("Usql").GetValue(item, null);
                    obj.psql = (string)tipo.GetProperty("Psql").GetValue(item, null);
                    obj.bloqueado = (bool)tipo.GetProperty("Bloqueado").GetValue(item, null);
                    obj.Correo = (string)tipo.GetProperty("Correo").GetValue(item, null);
                    obj.Pathnsf = (string)tipo.GetProperty("Pathnsf").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método para obtener todos los registros de la tabla Usuarios.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Usuarios> GetUsuarios() {


            //Inicializamos los servicios de usuarios.
            SO_Usuarios ServiceUsuarios = new SO_Usuarios();

            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Usuarios> Lista = new ObservableCollection<Usuarios>();
            
            //Ejecutamos el método para obtener la información de la base de datos.
            IList ObjUsuarios = ServiceUsuarios.GetUsuario();

            //Comparamos que la información de la base de datos no sea nulo
            if (ObjUsuarios != null)

            {
                //Iteramos la información recibida.
                foreach (var item in ObjUsuarios)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos on objeto de tipo usuarios que contendrá la información de un registro.
                    Usuarios obj = new Usuarios();

                    //Asignamos los valores correspondientes.
                    obj.usuario = (string)tipo.GetProperty("Usuario").GetValue(item, null);
                    obj.password = (string)tipo.GetProperty("Password").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("Nombre").GetValue(item, null);
                    obj.APaterno = (string)tipo.GetProperty("APaterno").GetValue(item, null);
                    obj.AMaterno = (string)tipo.GetProperty("AMaterno").GetValue(item, null);
                    obj.estado = (int)tipo.GetProperty("Estado").GetValue(item, null);
                    obj.usql = (string)tipo.GetProperty("Usql").GetValue(item, null);
                    obj.psql = (string)tipo.GetProperty("Psql").GetValue(item, null);
                    obj.bloqueado = (bool)tipo.GetProperty("Bloqueado").GetValue(item, null);
                    obj.Correo = (string)tipo.GetProperty("Correo").GetValue(item, null);
                    obj.Pathnsf = (string)tipo.GetProperty("Pathnsf").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista.
            return Lista;
        }
        /// <summary>
        /// metodo para buscar un usuario
        /// </summary>
        /// <param name="txt_busqueda"></param>
        /// <returns></returns>
        public static ObservableCollection<Usuarios> GetUsuario(string txt_busqueda)
        {
            SO_Usuarios ServiceUsuarios = new SO_Usuarios();

            ObservableCollection<Usuarios> lista = new ObservableCollection<Usuarios>();
            IList obj = ServiceUsuarios.BuscarUsuario(txt_busqueda);

            if (obj != null)
            {
                foreach (var item in obj)
                {
                    System.Type tipo = item.GetType();

                    Usuarios user = new Usuarios();

                    user.usuario = (string)tipo.GetProperty("Usuario").GetValue(item, null);
                    user.password = (string)tipo.GetProperty("Password").GetValue(item, null);
                    user.nombre = (string)tipo.GetProperty("Nombre").GetValue(item, null);
                    user.APaterno = (string)tipo.GetProperty("APaterno").GetValue(item, null);
                    user.AMaterno = (string)tipo.GetProperty("AMaterno").GetValue(item, null);
                    user.estado = (int)tipo.GetProperty("Estado").GetValue(item, null);
                    user.usql = (string)tipo.GetProperty("Usql").GetValue(item, null);
                    user.psql = (string)tipo.GetProperty("Psql").GetValue(item, null);
                    user.bloqueado = (bool)tipo.GetProperty("Bloqueado").GetValue(item, null);
                    user.Correo = (string)tipo.GetProperty("Correo").GetValue(item, null);
                    user.Pathnsf = (string)tipo.GetProperty("Pathnsf").GetValue(item, null);
                    lista.Add(user);
                }
            }
            return lista;
        }

        /// <summary>
        /// Método para insertar un registro en la tabla Usuarios.
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        public static string SetUsuario(Usuarios usuarios)
        {
            //Se inician los servicios de Usuarios.
            SO_Usuarios ServiceUsuarios = new SO_Usuarios();

            //Se ejecuta el método y retorna el usuario que fue insertado.
            return ServiceUsuarios.SetUsuario(usuarios.usuario, usuarios.password, usuarios.nombre, usuarios.APaterno, usuarios.AMaterno,
                                              usuarios.estado, usuarios.usql, usuarios.psql, usuarios.bloqueado,usuarios.Correo,usuarios.Pathnsf);

        }

        /// <summary>
        /// Método para modificar un registro en la tabla.
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        public static int UpdateUsuario(Usuarios usuarios)
        {
            //Se inician los servicios de Usuarios.
            SO_Usuarios ServiceUsuarios = new SO_Usuarios();

            // Se ejecuta el método y retorna los registros que se modificaron.
            return ServiceUsuarios.UpdateUsuarios(usuarios.usuario, usuarios.password, usuarios.nombre, usuarios.APaterno, usuarios.AMaterno,
                                              usuarios.estado, usuarios.usql, usuarios.psql, usuarios.bloqueado,usuarios.Correo,usuarios.Pathnsf);

        }

        /// <summary>
        /// Método para eliminar un registro de la tabla Usuarios.
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        public static int DeleteUsuarios(Usuarios usuarios)
        {
            //Se inician los servicios de Usuarios.
            SO_Usuarios ServiceUsuarios = new SO_Usuarios();
            Encriptacion encriptar = new Encriptacion();

            //encriptamos el usuario para poder buscarlo
            string Usuario = encriptar.encript(usuarios.usuario);

            //Se ejecuta el método y retorna número de registros eliminados.
            return ServiceUsuarios.DeleteUsuario(Usuario);
        }

        /// <summary>
        /// Método para validar si existe un usuario
        /// </summary>
        /// <param name="usuarios"></param>
        /// <returns></returns>
        public static string ValidateUsuario(Usuarios usuarios)
        {
            //Se inician los servicios de Usuarios.
            SO_Usuarios ServiceUsuarios = new SO_Usuarios();

            //Se ejecuta el método y retorna número de registros eliminados.
            return ServiceUsuarios.ValidateUsuarios(usuarios.nombre, usuarios.APaterno, usuarios.AMaterno, usuarios.usuario);
        }

        /// <summary>
        /// Método que obtiene la contraseña de un determinado usuario.
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <returns></returns>
        public static string GetPass(string id_usuario)
        {
            //Se inician los servicios de Usuarios.
            SO_Usuarios ServiceUsuarios = new SO_Usuarios();

            //Se ejecuta el método y retorna número de registros eliminados.
            return ServiceUsuarios.GetContraseña(id_usuario);
        }

        /// <summary>
        /// Método que modifica la contraseña de un determinado usuario.
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static int UpdatePass(string id_usuario, string pass)
        {
            //Se inician los servicios de Usuarios.
            SO_Usuarios ServiceUsuarios = new SO_Usuarios();

            //Se ejecuta el método y retorna número de registros eliminados.
            return ServiceUsuarios.UpdatePass(id_usuario, pass);
        }

        /// <summary>
        /// Método que obtiene el nombre de usuario
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static string GetNombreUsuario(string usuario)
        {
            //Se inician los servicios de Usuarios.
            SO_Usuarios ServiceUsuarios = new SO_Usuarios();

            //Se ejecuta el método y retorna número de registros eliminados.
            return ServiceUsuarios.GetNombreUsuario(usuario);
        }

        /// <summary>
        /// Método que obtiene el correo filtrado por nombre 
        /// </summary>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public static string GetCorreoUsuario(string idUsuario)
        {
            //Inicializamos los servicios del usuario.
            SO_Usuarios ServicioUsuario = new SO_Usuarios();

            return ServicioUsuario.GetCorreoUsuario(idUsuario);
        }

        public static string GetPath(string idUsuario)
        {
            //Inicializamos los servicios del usuario.
            SO_Usuarios ServicioUsuario = new SO_Usuarios();

            return ServicioUsuario.GetPathSnf(idUsuario);
        }

        #endregion

        #region Version

        /// <summary>
        /// Método para obtener los registros de la BD.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Version> GetVersion()
        {
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();

            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Version> Lista = new ObservableCollection<Version>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList ObjVersion = ServiceVersion.GetVersion();

            //Comparamos que la información de la base de datos no sea nulo.
            if (ObjVersion != null)
            {

                //Iteramos la información recibida.
                foreach (var item in ObjVersion)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos on objeto de tipo version que contendrá la información de un registro.
                    Version obj = new Version();

                    //Asignamos los valores correspondientes.
                    obj.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.id_usuario = (string)tipo.GetProperty("ID_USUARIO_ELABORO").GetValue(item, null);
                    obj.id_usuario_autorizo = (string)tipo.GetProperty("ID_USUARIO_AUTORIZO").GetValue(item, null);
                    obj.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);
                    obj.no_copias = (int)tipo.GetProperty("NO_COPIAS").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que obtiene todos los registros de las versiones de un documento en especifico
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public static ObservableCollection<Version> GetVersionesAnterioresXDocumento(int id_documento, int Numero_Tomar)
        {
            SO_Version servicio = new SO_Version();
            ObservableCollection<Version> ListaDatos = new ObservableCollection<Version>();

            IList Obj = servicio.GetVersionesXDocumento(id_documento, Numero_Tomar);

            if (Obj != null)
            {
                foreach (var item in Obj)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos on objeto de tipo version que contendrá la información de un registro.
                    Version objver = new Version();
                    //Asignamos los valores correspondientes.
                    objver.id_usuario = (string)tipo.GetProperty("USUARIO_ELABORO").GetValue(item, null);
                    objver.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    objver.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);
                    //Agregamos el objeto a la lista resultante.
                    ListaDatos.Add(objver);
                }
            }
            return ListaDatos;
        }
        /// <summary>
        /// Método que obtiene la version actual de un documento seleccionado
        /// </summary>
        /// <returns></returns>
        public static int  GetVersionDocumento(int id_documento)
        {
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();

            //Ejecutamos el método para obtener la información de la base de datos.
            return ServiceVersion.GetVersionDocumento(id_documento);

        }

        /// <summary>
        /// Método para insertar un registto a la Bd.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static int SetVersion(Version version,string nombreDocumento)
        {
            //Inicializamos los servicios
            SO_Version ServiceVersion = new SO_Version();
            SO_HistorialVersion ServiceHistorial = new SO_HistorialVersion();

            //Se ejecuta el método y regresa el id de la versión
            int i = ServiceVersion.SetVersion(version.id_usuario, version.id_usuario_autorizo, version.id_documento, version.no_version,
                                             version.fecha_version, version.no_copias, version.id_estatus_version, version.descripcion_v);
            
            //Se registra en el historial la acción.
            ServiceHistorial.Insert(i, Get_DateTime(), "Se crea la versión " + version.no_version, GetNombreUsuario(version.id_usuario),nombreDocumento,version.no_version);

            return i;
        }

        /// <summary>
        /// Método para modificar un regitro de la BD.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static int UpdateVersion(Version version,Usuario usuarioLogueado,string nombreDocumento)
        {
            //Inicializamos los servicios
            SO_Version ServiceVersion = new SO_Version();
            SO_HistorialVersion ServiceHistorial = new SO_HistorialVersion();
            SO_EstatusVersion ServiceEstatusVersion = new SO_EstatusVersion();

            // Se ejecuta el método y retorna los registros que se modificarion
            int i = ServiceVersion.UpdateVersion(version.id_version, 
                                                    version.id_usuario,
                                                    version.id_usuario_autorizo,
                                                    version.id_documento, 
                                                    version.no_version, 
                                                    version.fecha_version, 
                                                    version.no_copias, 
                                                    version.id_estatus_version, 
                                                    version.descripcion_v);

            string estatusVersion = ServiceEstatusVersion.GetEstatusVersion(version.id_estatus_version);

            ServiceHistorial.Insert(version.id_version, 
                                    Get_DateTime(),
                                    "Se cambia el estatus a: " + estatusVersion,
                                    usuarioLogueado.Nombre + " " + usuarioLogueado.ApellidoPaterno + " " + usuarioLogueado.ApellidoMaterno
                                    ,nombreDocumento,version.no_version);

            return i;
        }

        /// <summary>
        /// Método para actualizar el campo de numero de copias
        /// </summary>
        /// <param name="version"></param>
        /// <param name="num_copias"></param>
        /// <returns></returns>
        public static int UpdateNoCopias(int version, int num_copias)
        {
            SO_Version ServiceVersion = new SO_Version();//tabla donde se actualizan las copias de los documentos

            //se ejecuta el metodo para el registro de copias que se va a modificar
            int i = ServiceVersion.UpdateNumCopias(version, num_copias);
            return i;
        }

        /// <summary>
        /// metodo para actualizar el campo de Numero de copias
        /// despues de haberlo modificado(sobre la marcha)
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static int GetCopias(int version)
        {
            //Inicializamos los servicios
            SO_Version ServiceVersion = new SO_Version();

            //Ejecutamos el método y retornamos el valor
            return ServiceVersion.GetNumCopias(version);
        }

        /// <summary>
        /// Método para eliminar un registro de la BD.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static int DeleteVersion(Version version,string descrip_historial, Usuario UsuarioLog,string nombreDoc)
        {
            //Inicializamos los servicios de versión e Historial
            SO_Version ServiceVersion = new SO_Version();
            SO_HistorialVersion ServiceHistorial = new SO_HistorialVersion();

            // Se ejecuta el método y retorna los registros que se eliminaron.
            int delete= ServiceVersion.DeleteVersion(version.id_version);
            //Ejecutamos el método para insertar en el historial cuando una version se eliminó
            ServiceHistorial.Insert(version.id_version,Get_DateTime() ,descrip_historial,UsuarioLog.Nombre +" "+ UsuarioLog.ApellidoPaterno +" " +UsuarioLog.ApellidoMaterno,nombreDoc,version.no_version);

            return delete;
        }

        /// <summary>
        /// Método para obtener el usuario
        /// </summary>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public static Version GetIdUsuario(int id_version)
        {
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();

            Version obj = new Version();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList ObjVersion = ServiceVersion.GetUsuario(id_version);

            //Comparamos que la información de la base de datos no sea nulo.
            if (ObjVersion != null)
            {

                //Iteramos la información recibida.
                foreach (var item in ObjVersion)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos on objeto de tipo version que contendrá la información de un registro.


                    //Asignamos los valores correspondientes.
                    obj.id_usuario = (string)tipo.GetProperty("ID_USUARIO_ELABORO").GetValue(item, null);
                    obj.id_usuario_autorizo = (string)tipo.GetProperty("ID_USUARIO_AUTORIZO").GetValue(item, null);
                    obj.nombre_usuario_elaboro = (string)tipo.GetProperty("USUARIO_ELABORO").GetValue(item, null);
                    obj.nombre_usuario_autorizo = (string)tipo.GetProperty("USUARIO_AUTORIZO").GetValue(item, null);
                    //Agregamos el objeto a la lista resultante.

                }
            }
            return obj;
        }
        
        #region Modificaciones
        #region Raúl Bañuelos
        #region 09 AGO 2017
        //Descripción: Se agrega la funcionalidad de cuando la última versión sea una letra(A,B,C,etc) se genere la versión 1.
        //Desarrollador: Raúl Bañuelos.
        /*Código anterior
         * public static string GetLastVersion(int id_documento)
            {
                //Inicializamos los servicios de version.
                SO_Version ServiceVersion = new SO_Version();

                //convierte a entero para poder agregarle uno al valor.
                int version = Convert.ToInt32(ServiceVersion.GetLastVersion(id_documento));
                //se agrega uno.
                version++;

                //etorna el nuevo valor de la versión, convertido a string.
                return Convert.ToString(version);
            }
         */
        #endregion
        #endregion
        #endregion

        /// <summary>
        /// Método para obetener la última versión de un documento.
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public static string GetLastVersion(int id_documento)
        {
            SO_Version ServiceVersion = new SO_Version();
            IList informacionBD = ServiceVersion.GetLastVersion(id_documento);
            
            List<string> versionesDocumento = new List<string>();

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    System.Type tipo = item.GetType();

                    string version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    versionesDocumento.Add(version);
                }
            }
            
            return GetNextVersion(versionesDocumento);
        }

        /// <summary>
        /// Método que retorna la siguiente versión que debe ser utilizada en un documento.
        /// </summary>
        /// <param name="versionesDocumento"></param>
        /// <returns></returns>
        public static string GetNextVersion(List<string> versionesDocumento)
        {
            List<string> versionesLetra = new List<string>();
            List<int> versionesNumericas = new List<int>();

            foreach (string version in versionesDocumento)
            {
                if (IsNumeric(version))
                    versionesNumericas.Add(Convert.ToInt32(version));
                else
                    versionesLetra.Add(version);
            }

            if (versionesLetra.Count > 0)
            {
                return "1";
            }
            else
            {
                int i = versionesNumericas.Count;
                versionesNumericas.Sort();
                int c = versionesNumericas[i - 1];
                c += 1;
                return Convert.ToString(c);
            }
        }

        /// <summary>
        /// Método que indica si una cadena es un número.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsNumeric(string s)
        {
            float output;
            return float.TryParse(s, out output);
        }

        /// <summary>
        /// Método para obtener el id de la versión anterior de la actual
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public static int GetID_LastVersion(int id_documento, int id_version)
        {
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();

            //Ejecutamos la función que obtienen el id y la retornamos
            return ServiceVersion.GetLastVersion_Id(id_documento, id_version);
        }

        /// <summary>
        /// Método que obtiene el número de versión de un id
        /// </summary>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public static string GetNum_Version(int id_version)
        {
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();

            //Ejecutamos el método y retornamos el valor
            return ServiceVersion.GetNum_Version(id_version);
        }

        /// <summary>
        /// Método para validar la versión
        /// </summary>
        /// <param name="id_documento"></param>
        /// <param name="version"></param>
        public static int ValidateVersion(Version obj)
        {
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();

            //Retorna el id de la versión si ya existe.
            return ServiceVersion.ValidateVersion(obj.id_documento, obj.no_version);
        }

        /// <summary>
        /// Método que retorna el id de las versiones de un documento.
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public static ObservableCollection<Version> GetVersiones(int id_documento)
        {
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();

            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Version> Lista = new ObservableCollection<Version>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList ObjVersion = ServiceVersion.Versiones(id_documento);

            //Comparamos que la información de la base de datos no sea nulo.
            if (ObjVersion != null)
            {

                //Iteramos la información recibida.
                foreach (var item in ObjVersion)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos on objeto de tipo version que contendrá la información de un registro.
                    Version obj = new Version();

                    //Asignamos los valores correspondientes.
                    obj.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que retorna toda la información de las versiones de un documento
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public static ObservableCollection<Version> GetVersiones_Documento(int id_documento)
        {
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();

            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Version> Lista = new ObservableCollection<Version>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList ObjVersion = ServiceVersion.GetVersiones(id_documento);

            //Comparamos que la información de la base de datos no sea nulo.
            if (ObjVersion != null)
            {

                //Iteramos la información recibida.
                foreach (var item in ObjVersion)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos on objeto de tipo version que contendrá la información de un registro.
                    Version obj = new Version();

                    //Asignamos los valores correspondientes.
                    obj.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);
                    obj.no_copias = (int)tipo.GetProperty("NO_COPIAS").GetValue(item, null);
                    obj.nombre_usuario_autorizo = (string)tipo.GetProperty("USUARIO_AUTORIZO").GetValue(item, null);
                    obj.nombre_usuario_elaboro = (string)tipo.GetProperty("USUARIO_ELABORO").GetValue(item, null);
                    obj.descripcion_v = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista.
            return Lista;
        }
        
        /// <summary>
        /// Método para obtener los archivos de una versión
        /// </summary>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public static ObservableCollection<Archivo> GetArchivos(int id_version)
        {
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();

            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Archivo> Lista = new ObservableCollection<Archivo>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList ObjVersion = ServiceVersion.GetArchivos(id_version);

            //Comparamos que la información de la base de datos no sea nulo.
            if (ObjVersion != null)
            {

                //Iteramos la información recibida.
                foreach (var item in ObjVersion)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo archivo que contendrá la información de un registro.
                    Archivo obj = new Archivo();

                    //Asignamos los valores correspondientes.
                    obj.id_archivo = (int)tipo.GetProperty("ID_ARCHIVO").GetValue(item, null);
                    obj.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);
                    obj.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE_ARCHIVO").GetValue(item, null);
                    obj.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que obtiene las versiones que el administrador tiene pendientes por aprobar de todos los usuarios
        /// y los filtra con el parametro textobuscar
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumentosValidar(string nombreUsuario,string textobuscar)
        {
            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Documento> ListaResultante = new ObservableCollection<Documento>();

            //Inicializamos los servicios de Documento.
            SO_Documento ServicioDocumento = new SO_Documento();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServicioDocumento.GetDocumentosValidar(textobuscar);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista resultante
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo documento que contendrá la información de un registro.
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.tipo.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.usuario = (string)tipo.GetProperty("NOMBRE_USUARIO").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    //Los agregamos a la lista
                    ListaResultante.Add(obj);
                }
            }
            //Retornamos la lista resultante
            return ListaResultante;
        }

        /// <summary>
        /// Método para obtener los documentos pendientes por corregir  de un usuario.
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumentos_PendientesCorregir(string usuario, string textobuscar)
        {
            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //Inicializamos los servicios de documento.
            SO_Documento ServicioDocumento = new SO_Documento();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServicioDocumento.GetDocumentosPendientes(usuario,textobuscar);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Creamos un objeto de tipo Documento
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.tipo.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.usuario = (string)tipo.GetProperty("NOMBRE_USUARIO").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);

                    //La agregamos a la lista
                    Lista.Add(obj);

                }
            }

            //Retonamos la lista resultante
            return Lista;
        }

        /// <summary>
        /// Método que obtiene todos los documentos que estan en estatus de pendientes por corregir.
        /// </summary>
        /// <param name="textoBuscar"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumentos_PendientesCorregir(string textoBuscar)
        {
            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //Inicializamos los servicios de documento.
            SO_Documento ServicioDocumento = new SO_Documento();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServicioDocumento.GetAllDocumentosPendientes(textoBuscar);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Creamos un objeto de tipo Documento
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.tipo.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.usuario = (string)tipo.GetProperty("NOMBRE_USUARIO").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);

                    //La agregamos a la lista
                    Lista.Add(obj);
                }
            }

            //Retonamos la lista resultante
            return Lista;
        }

        /// <summary>
        /// Método para obtener los documentos pendientes por liberar
        /// filtrados por el nombre
        /// </summary>
        /// <param name="textobuscar"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumentos_PendientesLiberar(string textobuscar)
        {
            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //Inicializamos los servicios de documento.
            SO_Documento ServicioDocumento = new SO_Documento();

            //Ejecutamos el método para obtener la información de la base de datos.
            //le mandamos un parametro que servira para filtrar los datos
            IList informacionBD = ServicioDocumento.GetDocumentosAprobados(textobuscar);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Creamos un objeto de tipo Documento
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.tipo.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.id_tipo_documento = (int)tipo.GetProperty("ID_TIPO_DOCUMENTO").GetValue(item, null);
                    obj.usuario = (string)tipo.GetProperty("NOMBRE_USUARIO").GetValue(item, null);
                    
                    
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);
                    obj.version.id_usuario = (string)tipo.GetProperty("ID_USUARIO_ELABORO").GetValue(item, null);
                    obj.version.id_usuario_autorizo = (string)tipo.GetProperty("ID_USUARIO_AUTORIZO").GetValue(item, null);
                    obj.version.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.version.CodeValidation = (string)tipo.GetProperty("CODE_VALIDATION").GetValue(item, null);
                    obj.version.id_documento = obj.id_documento;
                    obj.version.descripcion_v = (string)tipo.GetProperty("DESCRIPCION_VERSION").GetValue(item, null);

                    //Agregamos a la lista resultante
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista
            return Lista;
        }

        public static ObservableCollection<Documento> GetDocumentos_PendientesXLiberar(string CodigoValidacion)
        {
            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //Inicializamos los servicios de documento.
            SO_Documento ServicioDocumento = new SO_Documento();

            //Ejecutamos el método para obtener la información de la base de datos.
            //le mandamos un parametro que servira para filtrar los datos
            IList informacionBD = ServicioDocumento.GetDocumentosAprobadosBuscadosCodigoValidacion(CodigoValidacion);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Creamos un objeto de tipo Documento
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.tipo.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.id_tipo_documento = (int)tipo.GetProperty("ID_TIPO_DOCUMENTO").GetValue(item, null);
                    obj.usuario = (string)tipo.GetProperty("NOMBRE_USUARIO").GetValue(item, null);


                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);
                    obj.version.id_usuario = (string)tipo.GetProperty("ID_USUARIO_ELABORO").GetValue(item, null);
                    obj.version.id_usuario_autorizo = (string)tipo.GetProperty("ID_USUARIO_AUTORIZO").GetValue(item, null);
                    obj.version.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.version.CodeValidation = (string)tipo.GetProperty("CODE_VALIDATION").GetValue(item, null);
                    obj.version.id_documento = obj.id_documento;
                    obj.version.descripcion_v = (string)tipo.GetProperty("DESCRIPCION_VERSION").GetValue(item, null);

                    //Agregamos a la lista resultante
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista
            return Lista;
        }

        /// <summary>
        /// Método que obtiene la lista de documentos que ya fueron aprobados y no han sido entregados desde hace dos dias.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<DO_DocumentosRechazados> GetDocumentosAprobadosNoRecibidos(bool Seleccionado)
        {
            //Declaramos una lista la cual contendrá  los documentos y será la que retornemos en el métod.
            ObservableCollection<DO_DocumentosRechazados> ListaDocumentos = new ObservableCollection<DO_DocumentosRechazados>();

            //Inicializamos los servicios del historial de documentos.
            SO_HistorialVersion ServiceHistorial = new SO_HistorialVersion();

            //Declaramos una lista la cual contendrá los documentos pendientes por Liberar. Es decir, documentos que ya fueron aprobados y se está a la espera de recibirlos.
            ObservableCollection<Documento> DocumentosPendientes = new ObservableCollection<Documento>();
            
            //Ejecutamos el método para obtener los documentos.
            DocumentosPendientes = GetDocumentos_PendientesLiberar(string.Empty);

            //Iteramos la lista de documentos.
            foreach (Documento documento in DocumentosPendientes)
            {
                //Obtenemos el historial de cambios de estatus del documento. Los guardamos en una lista.
                IList informacionBD = ServiceHistorial.GetHistorial_version(documento.nombre, documento.version.no_version);

                //Validamos que la lista de historial sea distinto de nulo.
                if (informacionBD != null)
                {
                    //Declaramos
                    DateTime fechaUltimaActualizacion = new DateTime();

                    foreach (var item in informacionBD)
                    {
                        Type tipo = item.GetType();
                        string estatus = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                        if (estatus == "Se cambia el estatus a: APROBADO, PENDIENTE POR LIBERAR")
                        {
                            fechaUltimaActualizacion = Convert.ToDateTime(tipo.GetProperty("FECHA").GetValue(item, null));
                        }
                    }

                    //Agregamos dos dias habiles a la fecha de actualización.
                    DateTime fechaCompromisoEntrega = AddBusinessDays(fechaUltimaActualizacion, 2);
                    DateTime fechaActual = Get_DateTime();

                    if (fechaCompromisoEntrega < fechaActual)
                    {
                        Usuario duenoDocumento = new Usuario();
                        duenoDocumento = DataManager.GetNameUsuario(documento.version.id_usuario);

                        DO_DocumentosRechazados documentoRechazado = new DO_DocumentosRechazados();
                        documentoRechazado.NombreDocumento = documento.nombre;
                        documentoRechazado.NoVersion = documento.version.no_version;
                        documentoRechazado.Correo = duenoDocumento.Correo;
                        documentoRechazado.DuenoDocumento = duenoDocumento.NombreUsuario;

                        string hora = fechaCompromisoEntrega.Hour.ToString();
                        if (fechaCompromisoEntrega.Hour.ToString().Length == 1)
                            hora = "0" + fechaCompromisoEntrega.Hour;

                        string minuto = fechaCompromisoEntrega.Minute.ToString();
                        if (fechaCompromisoEntrega.Minute.ToString().Length == 1)
                            minuto = "0" + fechaCompromisoEntrega.Minute;

                        string FechaMes = fechaCompromisoEntrega.Month.ToString();
                        if (fechaCompromisoEntrega.Month.ToString().Length == 1)
                            FechaMes = "0" + fechaCompromisoEntrega.Month;

                        string FechaDia = fechaCompromisoEntrega.Day.ToString();
                        if (fechaCompromisoEntrega.Day.ToString().Length == 1)
                            FechaDia = "0" + fechaCompromisoEntrega.Day;

                        documentoRechazado.Fecha = fechaCompromisoEntrega.Year + "-" + FechaMes + "-" + FechaDia + "  " + hora + ":" + minuto;
                        documentoRechazado.IsSelected = Seleccionado;
                        ListaDocumentos.Add(documentoRechazado);

                    }
                }
            }

            return ListaDocumentos;
        }

        public static int GetIdVersion(string nombreDocumento, string noVersion)
        {
            SO_Version ServiceVersion = new SO_Version();

            return ServiceVersion.GetVersionDocumento(nombreDocumento, noVersion);
        }

        public static int SetRechazarVersion(int idVersion)
        {
            SO_Version ServiceVersion = new SO_Version();

            return ServiceVersion.SetRechazarDocumento(idVersion);
        }

        /// <summary>
        /// Funcion que agrega los días indicados sin contar sabados ni domingos.
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="nDays"></param>
        /// <returns></returns>
        public static DateTime AddBusinessDays(DateTime dt, int nDays)
        {
            int weeks = nDays / 5;
            nDays %= 5;
            while (dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday)
            {
                dt = dt.AddDays(1);
            }

            while (nDays-- > 0)
            {
                dt = dt.AddDays(1);
                if (dt.DayOfWeek == DayOfWeek.Saturday)
                {
                    dt = dt.AddDays(2);
                }
            }

            return dt.AddDays(weeks * 7);
        }

        /// <summary>
        /// Método que obtiene todos los documentos pendientes por liberar de un determinado usuarip
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetPendientes_Liberar(string usuario)
        {
            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //Inicializamos los servicios de documento.
            SO_Documento ServicioDocumento = new SO_Documento();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServicioDocumento.GetDocumentos_PendientesLiberar(usuario);

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Creamos un objeto de tipo Documento
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.tipo.tipo_documento = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.usuario = (string)tipo.GetProperty("NOMBRE_USUARIO").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    //se añaden a la lista resultante
                    Lista.Add(obj);

                }
            }
            //Retornamos la lista
            return Lista;
        }

        /// <summary>
        /// Método para actualizar el estatus de una versión
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int Update_EstatusVersion(Version obj, Usuario usuario,string nombreDocumento)
        {
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();
            SO_HistorialVersion ServiceHistorial = new SO_HistorialVersion();
            SO_EstatusVersion ServiceEstatusVersion = new SO_EstatusVersion();

            //Ejecutamos el método y retornamos el resultado
            int i = ServiceVersion.UpdateEstatus_Version(obj.id_version, obj.id_estatus_version);

            string estatusVersion = ServiceEstatusVersion.GetEstatusVersion(obj.id_estatus_version);

            ServiceHistorial.Insert(obj.id_version, Get_DateTime(), "Se cambia el estatus a: " + estatusVersion, usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno,nombreDocumento, obj.no_version);

            return i;
        }

        /// <summary>
        /// Método para obtener la versión de un documento que no esté liberado u obsoleto
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public static ObservableCollection<Version> GetStatus_Version(int id_documento)
        {
            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Version> Lista = new ObservableCollection<Version>();

            //Inicializamos los servicios de version.
            SO_Version ServicioVersion = new SO_Version();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServicioVersion.GetStatus(id_documento);

            //Creamos un objeto de tipo Versión
            Version obj = new Version();

            //Si la lista es diferente de nulo
            if (informacionBD != null)
            {
                //Iteramos la lista
                foreach (var item in informacionBD)
                {
                    System.Type tipo = item.GetType();
                    //Asignamos los valores correspondientes
                    obj.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.estatus = (string)tipo.GetProperty("ESTATUS_VERSION").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante
                    Lista.Add(obj);
                }
            }
            //retornamos la lista
            return Lista;
        }

        /// <summary>
        /// Méto que indica si un CODE_VALIDATION esta repetido en la base de datos.
        /// </summary>
        /// <param name="codeValidation"></param>
        /// <returns></returns>
        public static bool ExistsCodeValidation(string codeValidation)
        {
            SO_Version ServiceVersion = new SO_Version();

            IList informacionBD = ServiceVersion.GetCodeValidation(codeValidation);

            if (informacionBD != null)
            {
                return informacionBD.Count > 0 ? true : false;
            }
            else
                return true;
        }

        /// <summary>
        /// Método que actualiza el codigo de validación.
        /// </summary>
        /// <param name="idVersion"></param>
        /// <param name="codeValidation"></param>
        /// <returns></returns>
        public static int UpdateCodeValidation(int idVersion, string codeValidation)
        {
            SO_Version ServiceVersion = new SO_Version();

            return ServiceVersion.UpdateCodeValidation(idVersion, codeValidation);
        }

        #endregion

        #region ValidacionDocumento

        /// <summary>
        /// Método que obtiene las relaciones de validación documento, dependiendo del tipo de documento
        /// </summary>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static ObservableCollection<ValidacionDocumento> GetValidacion_Documento(int id_tipo)
        {
            //Se inicializan los servicios de ValidacionDocumento
            SO_Validacion ServiceValidacion = new SO_Validacion();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<ValidacionDocumento> Lista = new ObservableCollection<ValidacionDocumento>();

            //obtenemos todo de la BD.
            IList ObjValidacion = ServiceValidacion.GetTR_Validacion(id_tipo);

            //Verificamos que la informacion no esté vacía.
            if (ObjValidacion != null)
            {
                //Iteramos la lista resultante
                foreach (var item in ObjValidacion)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    ValidacionDocumento obj = new ValidacionDocumento();

                    //Asignamos los valores correspondientes.
                    obj.id_validacion = (int)tipo.GetProperty("ID_VALIDACION_DOCUMENTO").GetValue(item, null);
                    obj.validacion_documento = (string)tipo.GetProperty("VALIDACION_DOCUMENTO").GetValue(item, null);
                    obj.validacion_descripcion = (string)tipo.GetProperty("VALIDACION_DESCRIPCION").GetValue(item, null);
                    obj.fecha_creacion = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);
                    obj.id_val_tipo = (int)tipo.GetProperty("ID_VALIDACION_TIPO_DOCUMENTO").GetValue(item, null);
                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que obtiene las relaciones de validacion tipo de documento, dependiendo del id de la validación
        /// </summary>
        /// <param name="id_val"></param>
        /// <returns></returns>
        public static ObservableCollection<ValidacionDocumento> GetR_Val_Tipo(int id_val)
        {
            //Se inicializan los servicios de ValidacionDocumento.
            SO_Validacion ServiceValidacion = new SO_Validacion();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<ValidacionDocumento> Lista = new ObservableCollection<ValidacionDocumento>();

            //obtenemos todo de la BD.
            IList ObjValidacion = ServiceValidacion.GetR_Val_Tipo(id_val);

            //Verificamos que la informacion no esté vacía.
            if (ObjValidacion != null)
            {
                //Iteramos la lista resultante
                foreach (var item in ObjValidacion)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    ValidacionDocumento obj = new ValidacionDocumento();

                    //Asignamos los valores correspondientes.
                    obj.id_val_tipo = (int)tipo.GetProperty("ID_VALIDACION_TIPO_DOCUMENTO").GetValue(item, null);
                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }
        
        /// <summary>
        /// Método que obtiene todas las validaciones de documento
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<ValidacionDocumento> GetValidaciones()
        {
            //Se inicializan los servicios de ValidacionDocumetno.
            SO_Validacion ServiceValidacion = new SO_Validacion();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<ValidacionDocumento> Lista = new ObservableCollection<ValidacionDocumento>();

            //obtenemos todo de la BD.
            IList ObjValidacion = ServiceValidacion.GetValidaciones();

            //Verificamos que la informacion no esté vacía.
            if (ObjValidacion != null)
            {
                //Iteramos la lista
                foreach (var item in ObjValidacion)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    ValidacionDocumento obj = new ValidacionDocumento();

                    //Asignamos los valores correspondientes.
                    obj.id_validacion = (int)tipo.GetProperty("ID_VALIDACION_DOCUMENTO").GetValue(item, null);
                    obj.validacion_documento = (string)tipo.GetProperty("VALIDACION_DOCUMENTO").GetValue(item, null);
                    obj.validacion_descripcion = (string)tipo.GetProperty("VALIDACION_DESCRIPCION").GetValue(item, null);
                    obj.fecha_creacion = (DateTime)tipo.GetProperty("FECHA_CREACION").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que guarda un registro en la tabla validacion documento
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetValidacion(ValidacionDocumento obj)
        {
            //Se inicializan los servicios de Validacion.
            SO_Validacion ServiceValidacion = new SO_Validacion();

            //Ejecutamos el método que guarda el registro
            return ServiceValidacion.SetValidacion(obj.validacion_documento, obj.validacion_descripcion, obj.fecha_creacion);
        }

        /// <summary>
        /// Método que verifica si existe una relación de tipo de documento con la validación
        /// </summary>
        /// <param name="id_validacion"></param>
        /// <param name="id_tipo"></param>
        /// <returns></returns>
        public static int SearchValidacion(int id_validacion, int id_tipo)
        {
            //Se inicializan los servicios de Validacion.
            SO_Validacion ServiceValidacion = new SO_Validacion();

            //Ejecutamos el método para buscar el registro
            return ServiceValidacion.SearchValidacion(id_validacion,id_tipo);
        }

        /// <summary>
        /// Método que guarda la relación entre validación y tipo de documento
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetRelacion(ValidacionDocumento obj)
        {
            //Se inicializan los servicios de Validacion.
            SO_Validacion ServiceValidacion = new SO_Validacion();

            //Ejecutamos el método para guardar el registro
            return ServiceValidacion.SetRelacion(obj.id_tipo, obj.id_validacion);
        }

        /// <summary>
        /// Método que obtiene el id la validación 
        /// </summary>
        /// <param name="descripcion"></param>
        /// <returns></returns>
        public static int GetIDValidacion(string validacion_doc) {
            //Se inicializan los servicios de Validacion.
            SO_Validacion ServiceValidacion = new SO_Validacion();

            //Ejecutamos el método
            return ServiceValidacion.GetID_Validacion(validacion_doc);
        }

        /// <summary>
        /// Método para eliminar un registro de la tabla Validacion documento
        /// </summary>
        /// <param name="id_validacion"></param>
        /// <returns></returns>
        public static int DeleteValidacion(int id_validacion)
        {
            //Se inicializan los servicios de Validacion.
            SO_Validacion ServiceValidacion = new SO_Validacion();

            //Ejecutamos el método para eliminar el registro
            return ServiceValidacion.DeleteValidacion(id_validacion);
        }

        /// <summary>
        /// Método para eliminar la relación de la tabla validación y un tipo de documento
        /// </summary>
        /// <param name="id_val_tipo"></param>
        /// <returns></returns>
        public static int DeleteTR_Validacion(int id_val_tipo)
        {
            //Se inicializan los servicios de Validacion.
            SO_Validacion ServiceValidacion = new SO_Validacion();
            //Ejecutamos el método
            return ServiceValidacion.DeleteRelacion_Validacion(id_val_tipo);
        }
        #endregion

        #region Bloqueo
        /// <summary>
        /// Método que obtiene la información del registro de bloqueo que tenga estado bloqueado
        /// o estado igual 1
        /// </summary>
        /// <returns></returns>
        public static Bloqueo GetBloqueo()
        {
            //Se inicializan los servicios del Bloqueo
            SO_Bloqueo ServiceBloqueo = new SO_Bloqueo();
            
            //Creamos un objeto de tipo bloqueo
            Bloqueo obj = new Bloqueo();

            //Obtenemos la información de la BD.
            IList Lista = ServiceBloqueo.GetBloqueo();

            //Si existe un registro con estado bloqueado
            if (Lista!= null)
            {
                //Iteramos la lista
                foreach (var item in Lista)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Asiganmos los valores correspondientes
                    obj.id_bloqueo = (int)tipo.GetProperty("ID_BLOQUEO").GetValue(item, null);
                    obj.fecha_fin = (DateTime)tipo.GetProperty("FECHA_FIN").GetValue(item, null);
                    obj.fecha_inicio = (DateTime)tipo.GetProperty("FECHA_INICIO").GetValue(item, null);
                    obj.observaciones = (string)tipo.GetProperty("OBSERVACIONES").GetValue(item, null);
                    obj.estado = (bool)tipo.GetProperty("ESTADO").GetValue(item, null);
                }
            }
            //Retornamos el objeto de tipo bloqueo
            return obj;
        }

        /// <summary>
        /// Método que desbloquea el sistema si llega a la fecha final
        /// </summary>
        /// <param name="fecha_final"></param>
        /// <returns></returns>
        public static void DesbloquearSistema(DateTime fecha_final)
        {
            //inicializamos los servicios de SO_Bloqueo
            SO_Bloqueo ServiceBloqueo = new SO_Bloqueo();

            //Obtiene el id si se encuentra el sistema bloqueado
            int id= ServiceBloqueo.GetID_Bloqueo(fecha_final);

            //Si el id es diferente de cero
            if (id !=0) {
                //Actualiza el estado a desbloqueado
                ServiceBloqueo.UpdateEstado(id);
            }
         
        }

        /// <summary>
        /// Método que inserta un registro a la tabla bloqueo
        /// </summary>
        /// <param name="fecha_i"></param>
        /// <param name="fecha_f"></param>
        /// <param name="observaciones"></param>
        /// <returns></returns>
        public static int SetBloqueo(Bloqueo obj)
        {
            //inicializamos los servicios de SO_Bloqueo
            SO_Bloqueo ServiceBloqueo = new SO_Bloqueo();

            //Ejecutamos el método para insertar el registro
           return ServiceBloqueo.SetBloqueo(obj.fecha_inicio, obj.fecha_fin, obj.observaciones);

        }

        /// <summary>
        /// Método para modificar el registro de la table bloqueo
        /// </summary>
        /// <param name="id_bloqueo"></param>
        /// <param name="fecha_i"></param>
        /// <param name="fecha_f"></param>
        /// <param name="estado"></param>
        /// <returns></returns>
        public static int UpdateBloqueo(Bloqueo obj)
        {
            //inicializamos los servicios de SO_Bloqueo
            SO_Bloqueo ServiceBloqueo = new SO_Bloqueo();

            //Ejecutamos el método para modificar un registro
            return ServiceBloqueo.UpdateBloqueo(obj.id_bloqueo, obj.fecha_inicio, obj.fecha_fin, obj.observaciones);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int UpdateEstado(Bloqueo obj)
        {
            //inicializamos los servicios de SO_Bloqueo
            SO_Bloqueo ServiceBloqueo = new SO_Bloqueo();

            //Ejecutamos el método para desbloquear un registro
            return ServiceBloqueo.UpdateEstado(obj.id_bloqueo);

        }

        #endregion

        #region Recursos Tipo de documento

        /// <summary>
        /// Método que obtiene los archivos (Recursos) de un tipo de documento.
        /// </summary>
        /// <param name="idTipoDocumento"></param>
        /// <returns></returns>
        public static ObservableCollection<Archivo> GetRecursosTipoDocumento(int idTipoDocumento)
        {
            //Inicializamos los servicios de Recursos.
            SO_RecursoTipoDocumento ServiceRecurso = new SO_RecursoTipoDocumento();

            //Declaramos una lista que será la que retornemos en el método.
            ObservableCollection<Archivo> ListaResultante = new ObservableCollection<Archivo>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServiceRecurso.GetArchivos(idTipoDocumento);

            //Verificamos que la información de la base de datos sea diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo archivo.
                    Archivo archivo = new Archivo();

                    //Asignamos los valores a las propiedades correspondientes.
                    archivo.id_archivo = (int)tipo.GetProperty("ID_RECURSO_TIPO_DOCUMENTO").GetValue(item, null);
                    archivo.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    archivo.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);
                    archivo.nombre = (string)tipo.GetProperty("NOMBRE_ARCHVO").GetValue(item, null);

                    //Agregamos el objeto a la lista.
                    ListaResultante.Add(archivo);
                    
                }
            }

            //Retornamos la lista
            return ListaResultante;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="idRecurso"></param>
        /// <returns></returns>
        public static ObservableCollection<Archivo> GetRecursoByIdRecurso(int idRecurso)
        {
            //Inicializamos los servicios de Recursos.
            SO_RecursoTipoDocumento ServiceRecurso = new SO_RecursoTipoDocumento();

            //Declaramos una lista que será la que retornemos en el método.
            ObservableCollection<Archivo> ListaResultante = new ObservableCollection<Archivo>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServiceRecurso.GetArchivosByIdRecurso(idRecurso);

            //Verificamos que la información de la base de datos sea diferente de nulo.
            if (informacionBD != null)
            {
                //Iteramos la lista obtenida.
                foreach (var item in informacionBD)
                {
                    //Obtenemos el tipo del elemento iterado.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto de tipo archivo.
                    Archivo archivo = new Archivo();

                    //Asignamos los valores a las propiedades correspondientes.
                    archivo.id_archivo = (int)tipo.GetProperty("ID_RECURSO_TIPO_DOCUMENTO").GetValue(item, null);
                    archivo.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    archivo.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);
                    archivo.nombre = (string)tipo.GetProperty("NOMBRE_ARCHVO").GetValue(item, null);

                    //Agregamos el objeto a la lista.
                    ListaResultante.Add(archivo);

                }
            }

            //Retornamos la lista
            return ListaResultante;
        }

        /// <summary>
        /// Método que inserta un recurso.
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="descripcion"></param>
        /// <param name="ext"></param>
        /// <param name="nombreArchivo"></param>
        /// <param name="idTipoDocumento"></param>
        /// <returns></returns>
        public static int InsertRecurso(byte[] archivo, string descripcion, string ext, string nombreArchivo,int idTipoDocumento)
        {
            //Inicializamos los servicios de Recursos.
            SO_RecursoTipoDocumento ServiceRecurso = new SO_RecursoTipoDocumento();

            //Ejecutamos el método para agregar el archivo y retornamos el resultado.
            return ServiceRecurso.Insert(archivo, descripcion, ext, nombreArchivo, idTipoDocumento);
        }

        /// <summary>
        /// Método que elimina un recurso.
        /// </summary>
        /// <param name="idRecurso"></param>
        /// <returns></returns>
        public static int DeleteRecurso(int idRecurso)
        {
            //Inicializamos los servicios de Recursos.
            SO_RecursoTipoDocumento ServicioRecurso = new SO_RecursoTipoDocumento();

            //Ejecutamos el método para agregar el archivo y retornamos el resultado.
            return ServicioRecurso.Delete(idRecurso);
        }
        #endregion

        #region Lecciones Aprendidas

        /// <summary>
        /// Método para insetar una nueva leccion aprendida
        /// </summary>
        /// <param name="idusuario"></param>
        /// <param name="componente"></param>
        /// <param name="cambio_requerido"></param>
        /// <param name="nivel_cambio"></param>
        /// <param name="c_trabajo"></param>
        /// <param name="operacion"></param>
        /// <param name="desc_probl"></param>
        /// <param name="reportado_por"></param>
        /// <param name="solicitud_Tingenieria"></param>
        /// <param name="criterio_1"></param>
        /// <param name="fecha_ultimo_cambio"></param>
        /// <param name="fecha_actualizacion"></param>
        /// <returns></returns>
        public static int InsertLeccion(string Componente, string DescripcionProblema, DateTime FechaUltimoCambio, DateTime FechaActualizacion,
            string ReportadoPor, string SolicitudTrabajoIngenieria, string IdUsuario)
        {
            SO_Lecciones servicio = new SO_Lecciones();

            return servicio.SetLeccion(Componente, DescripcionProblema, FechaUltimoCambio, FechaActualizacion, ReportadoPor, SolicitudTrabajoIngenieria, IdUsuario);
        }

        /// <summary>
        /// Método para eliminar una lección
        /// </summary>
        /// <param name="id_leccion"></param>
        /// <returns></returns>
        public static int Delete_Lecciones(int id_leccion)
        {
            SO_Lecciones Service = new SO_Lecciones();

            return Service.DeleteLeccion(id_leccion);
        }

        /// <summary>
        /// Método para eliminar el archivo de una leccion
        /// </summary>
        /// <param name="id_archivo_lecciones"></param>
        /// <returns></returns>
        public static int Delete_Archivo_Lecciones(int Id_Leccion)
        {
            SO_Archivo_Lecciones sercicio = new SO_Archivo_Lecciones();

            return sercicio.DeleteArchivoLeccion(Id_Leccion);
        }

        /// <summary>
        /// Método para actualizar los campos de una leccion
        /// </summary>
        /// <returns></returns>
        public static int UpdateLecccion(int id_lecciones,
                                         string id_usuario,
                                         string componente,
                                         string nivel_de_cambio,
                                         string centro_de_trabajo,
                                         string operacion,
                                         string descripcion_problema,
                                         DateTime fecha_ultimo_cambio,
                                         DateTime fecha_actualizacion,
                                         string reportado_por,
                                         string solicitud_trabajo_ingenieria
                                         )
        {
            SO_Lecciones servicio = new SO_Lecciones();

            return servicio.UpdateLeccion(id_lecciones, id_usuario, componente, nivel_de_cambio, centro_de_trabajo, operacion, descripcion_problema, fecha_ultimo_cambio, fecha_actualizacion, reportado_por, solicitud_trabajo_ingenieria);
        }
        /// <summary>
        /// Método que agrega un archivo nuevo de las lecciones aprendidas
        /// </summary>
        /// <param name="archivo"></param>
        /// <param name="ext"></param>
        /// <param name="nombre_archivo"></param>
        /// <param name="id_leccion"></param>
        /// <returns></returns>
        public static Task<int> SetArchivo_Lecciones(byte[] archivo, string ext, string nombre_archivo, int id_leccion)
        {
            SO_Archivo_Lecciones servicios = new SO_Archivo_Lecciones();

            return servicios.InsertarArchivoLecciones(archivo, ext, nombre_archivo, id_leccion);
        }

        /// <summary>
        /// Función que verifica si existe un usuario en una lista.
        /// </summary>
        /// <param name="lista"></param>
        /// <param name="idUsuario"></param>
        /// <returns></returns>
        public static bool VerificarNombre(List<DO_UsuarioNombre> lista, string idUsuario)
        {
            foreach (var item in lista)
            {
                if (item.ID_USUARIO == idUsuario)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Método que obtiene las lecciones aprendidas
        /// y las filtra
        /// </summary>
        /// <param name="TextoBuscar"></param>
        /// <returns></returns>
        public static ObservableCollection<LeccionesAprendidas> GetLec(string TextoBuscar)
        {
            SO_Lecciones ServiceLecciones = new SO_Lecciones();

            ObservableCollection<LeccionesAprendidas> ListaLecciones = new ObservableCollection<LeccionesAprendidas>();

            IList ObjLeccion = ServiceLecciones.GetLeccionesAprendidas(TextoBuscar);
            List<DO_UsuarioNombre> listausuarios = new List<DO_UsuarioNombre>(); 

            if (ObjLeccion != null)
            {
                foreach (var item in ObjLeccion)
                {
                    Encriptacion encript = new Encriptacion();

                    System.Type tipo = item.GetType();

                    LeccionesAprendidas ObjLec = new LeccionesAprendidas();

                    ObjLec.ID_LECCIONES_APRENDIDAS = (int)tipo.GetProperty("ID_LECCIONES_APRENDIDAS").GetValue(item, null);
                    ObjLec.ID_USUARIO = encript.desencript((string)tipo.GetProperty("ID_USUARIO").GetValue(item, null));
                    ObjLec.COMPONENTE = (string)tipo.GetProperty("COMPONENTE").GetValue(item, null);                    
                    ObjLec.DESCRIPCION_PROBLEMA = (string)tipo.GetProperty("DESCRIPCION_PROBLEMA").GetValue(item, null);
                    ObjLec.FECHA_ULTIMO_CAMBIO = (DateTime)tipo.GetProperty("FECHA_ULTIMO_CAMBIO").GetValue(item, null);
                    ObjLec.FECHA_ACTUALIZACION = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
                    ObjLec.REPORTADO_POR = (string)tipo.GetProperty("REPORTADO_POR").GetValue(item, null);
                    ObjLec.SOLICITUD_DE_TRABAJO = (string)tipo.GetProperty("SOLICITUD_DE_TRABAJO_INGENIERIA").GetValue(item, null);
                    if (VerificarNombre(listausuarios,ObjLec.ID_USUARIO))
                    {
                        DO_UsuarioNombre persona = listausuarios.Where(x => x.ID_USUARIO == ObjLec.ID_USUARIO).FirstOrDefault();
                        ObjLec.NombreCompleto = persona.NOMBRE_COMPLETO;
                    }
                    else
                    {
                        ObjLec.NombreCompleto = GetNombreUsuario(encript.encript(ObjLec.ID_USUARIO));
                        listausuarios.Add(new DO_UsuarioNombre { ID_USUARIO = ObjLec.ID_USUARIO, NOMBRE_COMPLETO = ObjLec.NombreCompleto });
                    }
                    
                    ListaLecciones.Add(ObjLec);
                }
            }
            return ListaLecciones;
        }

        /// <summary>
        /// Método que obtiene una lista de todos los componentes similares
        /// </summary>
        /// <param name="NombreComponente"></param>
        /// <returns></returns>
        public static ObservableCollection<LeccionesAprendidas> GetComponentesSimilares(string NombreComponente)
        {
            SO_Lecciones Servicio = new SO_Lecciones();

            ObservableCollection<LeccionesAprendidas> ListaComponentesSimilares = new ObservableCollection<LeccionesAprendidas>();

            IList ObjComponentes = Servicio.GetComponentesSimilares(NombreComponente);

            if (ObjComponentes != null)
            {
                foreach (var item in ObjComponentes)
                {
                    System.Type tipo = item.GetType();

                    LeccionesAprendidas ObjLec = new LeccionesAprendidas();

                    ObjLec.COMPONENTE = (string)tipo.GetProperty("COMPONENTE").GetValue(item, null);
                    ObjLec.DESCRIPCION_PROBLEMA = (string)tipo.GetProperty("DESCRIPCION_PROBLEMA").GetValue(item, null);

                    ListaComponentesSimilares.Add(ObjLec);
                }
            }
            return ListaComponentesSimilares;
        }

        /// <summary>
        /// Obtiene todos los centros de trabajo de una leccion aprendida
        /// </summary>
        /// <param name="Id_Leccion"></param>
        /// <returns></returns>
        public static ObservableCollection<CentrosTrabajo> GetCentrosDetrabajoLecciones(int Id_Leccion)
        {
            SO_LeccionesCentroTrabajo Servicio = new SO_LeccionesCentroTrabajo();

            ObservableCollection<CentrosTrabajo> ListaCentrosTrabajoLeccion = new ObservableCollection<CentrosTrabajo>();

            IList ObjCentrosLec = Servicio.GetLeccionesCentroTrabajo(Id_Leccion);

            if (ObjCentrosLec != null)
            {
                foreach (var item in ObjCentrosLec)
                {
                    System.Type tipo = item.GetType();

                    CentrosTrabajo ObjCent = new CentrosTrabajo();

                    ObjCent.CentroTrabajo = (string)tipo.GetProperty("CentroTrabajo1").GetValue(item, null);
                    ObjCent.NombreOperacion = (string)tipo.GetProperty("NombreOperacion").GetValue(item, null);
                    ObjCent.IsSelected = true;

                    ListaCentrosTrabajoLeccion.Add(ObjCent);
                }
            }
            return ListaCentrosTrabajoLeccion;
        }

        /// <summary>
        /// Obtiene todos los tipos de cambio de una leccion aprendida
        /// </summary>
        /// <param name="Id_Leccion"></param>
        /// <returns></returns>
        public static ObservableCollection<TIPOCAMBIO> GetTipoCambioLecciones(int Id_Leccion)
        {
            SO_LeccionesTipoCambio Servicio = new SO_LeccionesTipoCambio();

            ObservableCollection<TIPOCAMBIO> ListaTipoCambioLecciones = new ObservableCollection<TIPOCAMBIO>();

            IList ObjLec = Servicio.GetLeccionesTipoCambio(Id_Leccion);

            if (ObjLec != null)
            {
                foreach (var item in ObjLec)
                {
                    System.Type tipo = item.GetType();

                    TIPOCAMBIO ObjTipo = new TIPOCAMBIO();

                    ObjTipo.ID_TIPOCAMBIO = (int)tipo.GetProperty("ID_TIPOCAMBIO").GetValue(item, null);
                    ObjTipo.NOMBRETIPOCAMBIO = (string)tipo.GetProperty("NOMBRETIPOCAMBIO").GetValue(item, null);

                    ListaTipoCambioLecciones.Add(ObjTipo);
                }
            }
            return ListaTipoCambioLecciones;
        }

        /// <summary>
        /// Método que obtiene todos los archivos de una leccion
        /// </summary>
        /// <param name="id_leccion"></param>
        /// <returns></returns>
        public static ObservableCollection<Archivo_LeccionesAprendidas> GetArchivosLecciones(int id_leccion)
        {
            SO_Archivo_Lecciones ServicioArchivos = new SO_Archivo_Lecciones();

            ObservableCollection<Archivo_LeccionesAprendidas> ListaArchivosLecciones = new ObservableCollection<Archivo_LeccionesAprendidas>();

            IList ObjArchivo = ServicioArchivos.GetArchivoLecciones(id_leccion);

            if (ObjArchivo!=null)
            {
                foreach (var item in ObjArchivo)
                {
                    System.Type tipo = item.GetType();

                    Archivo_LeccionesAprendidas ObjArc = new Archivo_LeccionesAprendidas();

                    ObjArc.ARCHIVO = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    ObjArc.NOMBRE_ARCHIVO = (string)tipo.GetProperty("NOMBRE_ARCHIVO").GetValue(item, null);
                    ObjArc.EXT = (string)tipo.GetProperty("EXT").GetValue(item, null);
                    //ObjArc.ruta = (string)tipo.GetProperty("ruta").GetValue(item, null);
                    ObjArc.ID_ARCHIVO_LECCIONES = (int)tipo.GetProperty("ID_ARCHIVO_LECCIONES").GetValue(item, null);
                    ObjArc.ID_LECCIONES_APRENDIDAS = (int)tipo.GetProperty("ID_LECCIONES_APRENDIDAS").GetValue(item, null);

                    ListaArchivosLecciones.Add(ObjArc);
                }
            }
            return ListaArchivosLecciones;
        }

        /// <summary>
        /// Método para obtener todos los tipos de cambio para insertarlos en un combobox
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<TIPOCAMBIO> GetNivelesDeCambio()
        {
            SO_TipoCambio Servicio = new SO_TipoCambio();
            ObservableCollection<TIPOCAMBIO> ListaNivelDeCambio = new ObservableCollection<TIPOCAMBIO>();

            IList ObjCambios = Servicio.GetTiposCambios();

            if (ObjCambios!= null)
            {
                foreach (var item in ObjCambios)
                {
                    System.Type tipo = item.GetType();

                    TIPOCAMBIO ObjNIvelCambio = new TIPOCAMBIO();
                    
                    ObjNIvelCambio.ID_TIPOCAMBIO = (int)tipo.GetProperty("ID_TIPOCAMBIO").GetValue(item, null);
                    ObjNIvelCambio.NOMBRETIPOCAMBIO = (string)tipo.GetProperty("NOMBRETIPOCAMBIO").GetValue(item, null);

                    ListaNivelDeCambio.Add(ObjNIvelCambio);
                }
            }
            return ListaNivelDeCambio;
        }

        /// <summary>
        /// Método que obtiene todos los centros de trabajo para insertarlos en un combobox
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<CentrosTrabajo> GetCentrosDeTrabajo(string TextoBuscar)
        {
            SO_CentrosDeTrabajo Servicio = new SO_CentrosDeTrabajo();
            ObservableCollection<CentrosTrabajo> ListaCentrosDeTrabajo = new ObservableCollection<CentrosTrabajo>();

            IList ObjCentros = Servicio.GetCentrosTrabajo(TextoBuscar);

            if (ObjCentros != null)
            {
                foreach (var item in ObjCentros)
                {
                    System.Type tipo = item.GetType();

                    CentrosTrabajo ObjCentro = new CentrosTrabajo();

                    ObjCentro.CentroTrabajo = (string)tipo.GetProperty("CentroTrabajo1").GetValue(item, null);
                    ObjCentro.TiempoSetup = Convert.ToDouble(tipo.GetProperty("TiempoSetup").GetValue(item, null));
                    ObjCentro.NombreOperacion = (string)tipo.GetProperty("NombreOperacion").GetValue(item, null);
                    ObjCentro.ObjetoXML = (string)tipo.GetProperty("ObjetoXML").GetValue(item, null);
                    ObjCentro.ObjetoXMLVista = (string)tipo.GetProperty("ObjetoXMLVista").GetValue(item, null);
                    ObjCentro.NombreIngles = (string)tipo.GetProperty("NombreIngles").GetValue(item, null);

                    ListaCentrosDeTrabajo.Add(ObjCentro);                 

                }
            }
            return ListaCentrosDeTrabajo;
        }

        /// <summary>
        /// Método para insertar un nuevo registro en la tabla de tr_lecciones_centrostrabajo
        /// </summary>
        /// <param name="Id_CentroTrabajo"></param>
        /// <param name="Id_LeccionAprendida"></param>
        /// <returns></returns>
        public static int InsertLeccionesCentroDeTrabajo(string Id_CentroTrabajo, int Id_LeccionAprendida)
        {
            //Declaramos los servicios
            SO_CentrosDeTrabajo Servicio = new SO_CentrosDeTrabajo();

            //Mandamos llamar el método para insertar el nuevo registro
            return Servicio.InsertLeccionCentroTrabajo(Id_CentroTrabajo, Id_LeccionAprendida);
        }

        /// <summary>
        /// Método para insertar un nuevo registro en la tabla de tr_lecciones_tipo_cambio
        /// </summary>
        /// <param name="Id_TipoCambio"></param>
        /// <param name="Id_LeccionAprendida"></param>
        /// <returns></returns>
        public static int InsertLeccionesNivelCambio(int Id_TipoCambio, int Id_LeccionAprendida)
        {
            SO_LeccionesTipoCambio Servicio = new SO_LeccionesTipoCambio();

            return Servicio.InsertLeccionesTipoCambio(Id_TipoCambio, Id_LeccionAprendida);
        }

        /// <summary>
        /// Método para eliminar los centros de trabajo que tenga una leccion aprendida
        /// </summary>
        /// <param name="Id_Leccion"></param>
        /// <returns></returns>
        public static int DeleteCentrosDeTrabajoLeccion(int Id_Leccion)
        {
            SO_LeccionesCentroTrabajo Servicio = new SO_LeccionesCentroTrabajo();

            return Servicio.DeleteLeccionesCentroTrabajo(Id_Leccion);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id_leccion"></param>
        /// <returns></returns>
        public static int DeleteTiposDeCambioLeccion(int id_leccion)
        {
            SO_LeccionesTipoCambio Servicio = new SO_LeccionesTipoCambio();

            return Servicio.DeleteLeccionesTipoCambio(id_leccion);
        }

        /// <summary>
        /// Método que obtiene la fecha del ultimo cambio de componente que se vaya a ingresar nuevo
        /// </summary>
        /// <param name="Componente"></param>
        /// <returns></returns>
        public static ObservableCollection<LeccionesAprendidas> ConsultaFechaUltimoCambio(string Componente)
        {

            SO_Lecciones Servicio = new SO_Lecciones();

            ObservableCollection<LeccionesAprendidas> ComponentesSimilares = new ObservableCollection<LeccionesAprendidas>();

            DataSet Obj = Servicio.GetUltimosCambiosComponentesSimilares(Componente);

            if (Obj != null)
            {
                if (Obj.Tables.Count > 0 && Obj.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow item in Obj.Tables[0].Rows)
                    {
                        LeccionesAprendidas Lec = new LeccionesAprendidas();


                        Lec.DESCRIPCION_PROBLEMA = Convert.ToString(item["DESCRIPCION_PROBLEMA"]);
                        Lec.FECHA_ULTIMO_CAMBIO = Convert.ToDateTime(item["FECHA_ULTIMO_CAMBIO"]);
                        Lec.FECHA_ACTUALIZACION = Convert.ToDateTime(item["FECHA_ACTUALIZACION"]);


                        ComponentesSimilares.Add(Lec);
                    }
                }
            }
            return ComponentesSimilares;
        }
        #endregion

        #region Historial
        /// <summary>
        /// Método que obtiene todos los registro del historial, los filtra por numero de documento
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static ObservableCollection<HistorialVersion> GetHistorial(string texto)
        {
            //Se inicializan los servicios de Historial
            SO_HistorialVersion ServiceHistorial = new SO_HistorialVersion();

            //Lista que se va a retornar
            ObservableCollection<HistorialVersion> ListaR = new ObservableCollection<HistorialVersion>();
            //Obtenemos el resultado de la consulta
            IList Lista = ServiceHistorial.GetHistorial(texto);
            //Si la lista es diferente de nulo
            if (Lista !=null)
            {
                //Iteramos la lista
                foreach (var item in Lista)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();
                    //Declaramos el objeto de tipo historial
                    HistorialVersion obj = new HistorialVersion();

                    //Asiganmos los valores al objeto
                    obj.nombre_documento = (string)tipo.GetProperty("NOMBRE_DOCUMENTO").GetValue(item, null);
                    obj.no_version = (string)tipo.GetProperty("NO_VERSION").GetValue(item, null);

                    ListaR.Add(obj);
                }
            }
            //Retornamos la lista
            return ListaR;
        }

        /// <summary>
        /// Obtiene la información del historial de la version de un documento
        /// </summary>
        /// <param name="documento"></param>
        /// <param name="no_version"></param>
        /// <returns></returns>
        public static ObservableCollection<HistorialVersion> GetHistorial_Version(string documento,string no_version)
        {
            //Se inicializan los servicios de Historial
            SO_HistorialVersion ServiceHistorial = new SO_HistorialVersion();
            //Lista que se va a retornar
            ObservableCollection<HistorialVersion> ListaR = new ObservableCollection<HistorialVersion>();
            //Obtenemos el resultado de la consulta
            IList Lista = ServiceHistorial.GetHistorial_version(documento, no_version);
            //Si la lista es diferente de nulo
            if (Lista != null)
            {
                //Iteramos la lista
                foreach (var item in Lista)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    HistorialVersion obj = new HistorialVersion();
                    //Asiganmos los valores al objeto
                    obj.id_historial = (int)tipo.GetProperty("ID_HISTORIAL_VERSION").GetValue(item, null);
                    obj.fecha = (DateTime)tipo.GetProperty("FECHA").GetValue(item, null);
                    obj.descripcion = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                    obj.Nombre_usuario = (string)tipo.GetProperty("NOMBRE_USUARIO").GetValue(item, null);

                    ListaR.Add(obj);
                }
            }
            //Retornamos la lista
            return ListaR;
        }

        /// <summary>
        /// Método que inserta un registro en la tabla historial de versión
        /// </summary>
        /// <param name="idVersion"></param>
        /// <param name="usuario"></param>
        /// <param name="nombreDocumento"></param>
        /// <param name="version"></param>
        /// <param name="descripcionCambio"></param>
        /// <returns></returns>
        public static int InsertHistorialVersion(int idVersion,string usuario,string nombreDocumento, string version,string descripcionCambio )
        {
            //Inicializamos los servicios de historial de versión.
            SO_HistorialVersion ServiceVersion = new SO_HistorialVersion();

            //Ejecutamos el método y retornamos el resultado.
            return ServiceVersion.Insert(idVersion, DateTime.Now, descripcionCambio, usuario, nombreDocumento, version);
        }

        #endregion

        #region Documento Eliminado

        /// <summary>
        /// Método que obtiene todos los registros de la tabla 
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetAllDocumento_Eliminado(string texto)
        {
            //Inicializamos los servicios
            SO_Documento_Eliminado ServiceDoc_Eliminado = new SO_Documento_Eliminado();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //obtenemos todo de la BD.
            IList ListaResul = ServiceDoc_Eliminado.GetAllDocumento_Eliminado(texto);

            if (ListaResul !=null)
            {
                //Iteramos la lista 
                foreach (var item in ListaResul)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento= (int)tipo.GetProperty("ID_ELIMINADO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NUM_DOCUMENTO").GetValue(item, null);
                    obj.version.no_version= (string)tipo.GetProperty("NO_VERSION").GetValue(item, null);
                    obj.fecha_actualizacion= (DateTime)tipo.GetProperty("FECHA_ELIMINO").GetValue(item, null);
                    obj.version.archivo.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    obj.version.archivo.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);

                    //Agregamos el objeto a la lista.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }
        
        /// <summary>
        /// Métdo que inserta un registro a la tabla
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int SetDocumento_Eliminado(Documento obj)
        {
            //Inicializamos los servicios
            SO_Documento_Eliminado ServiceDoc_Eliminado = new SO_Documento_Eliminado();
            //Ejecutamos el método 
            return ServiceDoc_Eliminado.SetDocumento_Eliminado(obj.nombre, obj.version.no_version, Get_DateTime(), obj.version.archivo.archivo,obj.version.archivo.ext);
        }

        /// <summary>
        /// Obtiene la lista de los archivos de un documento eliminado
        /// </summary>
        /// <param name="id_documento"></param>
        /// <returns></returns>
        public static ObservableCollection<Archivo> GetArchivo_DocumentoEliminado(int id_documento)
        {
            //Inicializamos los servicios
            SO_Documento_Eliminado ServiceDoc_Eliminado = new SO_Documento_Eliminado();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Archivo> Lista = new ObservableCollection<Archivo>();

            //obtenemos todo de la BD.
            IList ListaResul = ServiceDoc_Eliminado.GetArchivo(id_documento);

            if (ListaResul !=null)
            {
                //Iteramos la lista 
                foreach (var item in ListaResul)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    Archivo obj = new Archivo();

                    obj.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    obj.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.fecha_elimino=(DateTime)tipo.GetProperty("FECHA_ELIMINO").GetValue(item, null);

                    //Agregamos a la lista
                    Lista.Add(obj);
                }           
             }
            //Retornamos la lista
            return Lista;
        }

        public static int UpdateDocumentoEliminado(int Id_registro, byte[] Archivo)
        {
            //Se inician los servicios de Documento.
            SO_Documento_Eliminado ServiceDocumento = new SO_Documento_Eliminado();

            // Se ejecuta el método y retorna los registros que se modificaron.
            return ServiceDocumento.UpdateDocumentoEliminado(Id_registro, Archivo);
        }


        /// <summary>
        /// Método que obtiene todos los registros de la tabla 
        /// </summary>
        /// <param name="texto"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumentoEliminar(string Nombre, string No_Version)
        {
            //Inicializamos los servicios
            SO_Documento_Eliminado ServiceDoc_Eliminado = new SO_Documento_Eliminado();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //obtenemos todo de la BD.
            IList ListaResul = ServiceDoc_Eliminado.GetDocumentoEliminar(Nombre, No_Version);

            if (ListaResul != null)
            {
                //Iteramos la lista 
                foreach (var item in ListaResul)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos un objeto  que contendrá la información de un registro.
                    Documento obj = new Documento();

                    //Asignamos los valores correspondientes.
                    obj.id_documento = (int)tipo.GetProperty("ID_ELIMINADO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NUM_DOCUMENTO").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("NO_VERSION").GetValue(item, null);
                    obj.fecha_actualizacion = (DateTime)tipo.GetProperty("FECHA_ELIMINO").GetValue(item, null);
                    obj.version.archivo.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);
                    obj.version.archivo.ext = (string)tipo.GetProperty("EXT").GetValue(item, null);

                    //Agregamos el objeto a la lista.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        #endregion

        #region SEALED

        public static int InsertDocumentoOSHAS(int emisor, string numero,string nombre,string cambio,string fecha,string original,int copias,string liga)
        {
            SO_Sealed service = new SO_Sealed();

            return service.InsertDocumentoOSHAS(emisor,numero,nombre,cambio,fecha,original,copias,liga);
        }

        public static int UpdateDocumentoOHSAS(int emisor, string numero, string nombre, string cambio, string fecha, string original, int copias, string liga)
        {
            SO_Sealed service = new SO_Sealed();

            return service.UpdateDocumentoOHSAS(emisor, numero, nombre, cambio, fecha, original, copias, liga);
        }

        public static int DeleteDocumentoOHSAS(string numero)
        {
            SO_Sealed service = new SO_Sealed();

            return service.DeleteDocumentoOHSAS(numero);
        }

        public static ObservableCollection<FO_Item> GetAllAreasOHSAS()
        {
            ObservableCollection<FO_Item> lista = new ObservableCollection<FO_Item>();

            SO_Sealed service = new SO_Sealed();

            DataTable informacionBD = service.GetAllAreasOHSAS();

            if (informacionBD != null)
            {
                if (informacionBD.Rows.Count > 0)
                {
                    foreach (DataRow item in informacionBD.Rows)
                    {
                        FO_Item itemF = new FO_Item();

                        itemF.Nombre = item["area"].ToString();
                        itemF.ValorCadena = item["a_descripcion"].ToString();

                        lista.Add(itemF);
                    }
                }
            }
            
            return lista;
        }

        public static string GetIdAreaOHSAS(string numero)
        {
            SO_Sealed service = new SO_Sealed();

            string id = string.Empty;

            DataTable informacionBD =service.GetIdAreaOHSAS(numero);

            if (informacionBD != null)
            {
                if (informacionBD.Rows.Count > 0)
                {
                    foreach (DataRow item in informacionBD.Rows)
                    {
                        id = item["area"].ToString();
                    }
                }
            }

            return id;
        }

        public static int InsertDocumentoEspecifico(int emisor, string numero, string nombre, string cambio, string fecha, string original, int copias, string liga)
        {
            SO_Sealed service = new SO_Sealed();

            return service.InsertDocumentoEspecifico(emisor, numero, nombre, cambio, fecha, original, copias, liga);
        }

        public static int UpdateDocumentoEspecifico(int emisor, string numero, string nombre, string cambio, string fecha, string original, int copias, string liga)
        {
            SO_Sealed service = new SO_Sealed();

            return service.UpdateDocumentoEspecifico(emisor, numero, nombre, cambio, fecha, original, copias, liga);
        }

        public static int DeleteDocumentoEspecifico(string numero)
        {
            SO_Sealed service = new SO_Sealed();

            return service.DeleteDocumentoEspecifico(numero);
        }

        public static ObservableCollection<FO_Item> GetAllAreasEspecifico()
        {
            ObservableCollection<FO_Item> lista = new ObservableCollection<FO_Item>();

            SO_Sealed service = new SO_Sealed();

            DataTable informacionBD = service.GetAllAreasEspecifico();

            if (informacionBD != null)
            {
                if (informacionBD.Rows.Count > 0)
                {
                    foreach (DataRow item in informacionBD.Rows)
                    {
                        FO_Item itemF = new FO_Item();

                        itemF.Nombre = item["area"].ToString();
                        itemF.ValorCadena = item["a_descripcion"].ToString();

                        lista.Add(itemF);
                    }
                }
            }

            return lista;
        }

        public static string GetIdAreaEspecifico(string numero)
        {
            SO_Sealed service = new SO_Sealed();

            string id = string.Empty;

            DataTable informacionBD = service.GetIdAreaEspecifico(numero);

            if (informacionBD != null)
            {
                if (informacionBD.Rows.Count > 0)
                {
                    foreach (DataRow item in informacionBD.Rows)
                    {
                        id = item["area"].ToString();
                    }
                }
            }

            return id;
        }

        public static int InsertDocumentoISO(int emisor, string numero, string nombre, string cambio, string fecha, string original, int copias, string liga)
        {
            SO_Sealed service = new SO_Sealed();

            return service.InsertDocumentoISO(emisor, numero, nombre, cambio, fecha, original, copias, liga);
        }

        public static int UpdateDocumentoISO(int emisor, string numero, string nombre, string cambio, string fecha, string original, int copias, string liga)
        {
            SO_Sealed service = new SO_Sealed();

            return service.UpdateDocumentoISO(emisor, numero, nombre, cambio, fecha, original, copias, liga);
        }

        public static int DeleteDocumentoISO(string numero)
        {
            SO_Sealed service = new SO_Sealed();

            return service.DeleteDocumentoISO(numero);
        }

        public static ObservableCollection<FO_Item> GetAllAreasISO()
        {
            ObservableCollection<FO_Item> lista = new ObservableCollection<FO_Item>();

            SO_Sealed service = new SO_Sealed();

            DataTable informacionBD = service.GetAllAreasISO();

            if (informacionBD != null)
            {
                if (informacionBD.Rows.Count > 0)
                {
                    foreach (DataRow item in informacionBD.Rows)
                    {
                        FO_Item itemF = new FO_Item();

                        itemF.Nombre = item["area"].ToString();
                        itemF.ValorCadena = item["a_descripcion"].ToString();

                        lista.Add(itemF);
                    }
                }
            }

            return lista;
        }

        public static string GetIdAreaISO(string numero)
        {
            SO_Sealed service = new SO_Sealed();

            string id = string.Empty;

            DataTable informacionBD = service.GetIdAreaISO(numero);

            if (informacionBD != null)
            {
                if (informacionBD.Rows.Count > 0)
                {
                    foreach (DataRow item in informacionBD.Rows)
                    {
                        id = item["area"].ToString();
                    }
                }
            }

            return id;
        }

        /// <summary>
        /// Método que obtiene el nombre del area frames
        /// donde se inserto un archivo.
        /// </summary>
        /// <param name="id_areasealed"></param>
        /// <returns></returns>
        public static string GetNombreAreaOHSAS(int id_areasealed)
        {
            SO_Sealed service = new SO_Sealed();
            string NombreArea = string.Empty;
            DataTable informacionBD = service.GetNombreAreaOHSAS(id_areasealed);

            if (informacionBD != null)
            {
                if (informacionBD.Rows.Count > 0)
                {
                    foreach (DataRow item in informacionBD.Rows)
                    {
                        NombreArea = item["a_descripcion"].ToString();
                    }
                }
            }

            return NombreArea;
        }

        /// <summary>
        /// Método que obtiene el nombre del area frames 
        /// donde se inserto un archivo.
        /// </summary>
        /// <param name="id_areasealed"></param>
        /// <returns></returns>
        public static string GetNombreAreaISO(int id_areasealed)
        {
            SO_Sealed service = new SO_Sealed();
            string NombreArea = string.Empty;
            DataTable informacionBD = service.GetNombreAreaISO(id_areasealed);

            if (informacionBD != null)
            {
                if (informacionBD.Rows.Count > 0)
                {
                    foreach (DataRow item in informacionBD.Rows)
                    {
                        NombreArea = item["a_descripcion"].ToString();
                    }
                }
            }

            return NombreArea;
        }

        /// <summary>
        /// Método que obtiene el nombre del area frames 
        /// donde se inserto un archivo
        /// </summary>
        /// <param name="id_areasealed"></param>
        /// <returns></returns>
        public static string GetNombreAreaESPECIFICOS(int id_areasealed)
        {
            SO_Sealed service = new SO_Sealed();
            string NombreArea = string.Empty;
            DataTable informacionBD = service.GetNombreAreasEspecificas(id_areasealed);

            if (informacionBD != null)
            {
                if (informacionBD.Rows.Count > 0)
                {
                    foreach (DataRow item in informacionBD.Rows)
                    {
                        NombreArea = item["a_descripcion"].ToString();
                    }
                }
            }

            return NombreArea;
        }

        public static ObservableCollection<Documento> GetAllDocumentosFrames()
        {
            ObservableCollection<Documento> documentos = new ObservableCollection<Documento>();

            SO_InformacionFrames ServiceObject = new SO_InformacionFrames();

            IList informacionBD = ServiceObject.GetAll();

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    Type tipo = item.GetType();

                    Documento documento = new Documento();

                    documento.nombre = (string)tipo.GetProperty("NOMBRE_DOCUMENTO").GetValue(item, null);
                    documento.version = new Version();
                    documento.version.no_version = (string)tipo.GetProperty("VERSION").GetValue(item, null);
                    documento.version.fecha_version = (DateTime)tipo.GetProperty("FECHA_LIBERACION").GetValue(item, null);
                    
                    documentos.Add(documento);
                }
            }

            return documentos;
        }
        #endregion

        #region Notificaciones

        /// <summary>
        /// Método que inserta una notificación.
        /// <param name="notificacion"></param>
        /// <returns></returns>
        /// </summary>
        public static int insertNotificacion(DO_Notification notificacion)
        {
            SO_Notificaciones ServiceNotificaciones = new SO_Notificaciones();

            return ServiceNotificaciones.InsertNotificacion(notificacion.ID_USUARIO_SEND, notificacion.ID_USUARIO_RECEIVER, notificacion.TITLE, notificacion.MSG, notificacion.TYPE_NOTIFICATION);
        }

        #endregion

        #region Grupos

        public static ObservableCollection<DO_Grupos> GetAllGrupos(string usuario)
        {
            SO_GRUPOS ServiceGrupos = new SO_GRUPOS();

            IList informacionBD = ServiceGrupos.GetAll(usuario);

            ObservableCollection<DO_Grupos> listaResultante = new ObservableCollection<DO_Grupos>();

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    DO_Grupos grupo = new DO_Grupos();

                    Type tipo = item.GetType();

                    grupo.idgrupo = (int)tipo.GetProperty("ID_GRUPO").GetValue(item, null);
                    grupo.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    grupo.idusuariodueno = (string)tipo.GetProperty("ID_USUARIO_DUENO").GetValue(item, null);
                    grupo.IsSelected = false;

                    listaResultante.Add(grupo);
                }
            }
            return listaResultante;
        }

        public static ObservableCollection<DO_INTEGRANTES_GRUPO> GetAllIntegrantesGrupo(int idGrupoSeleccionado)
        {
            SO_INTEGRANTES_GRUPO ServiceNormas = new SO_INTEGRANTES_GRUPO();

            IList informacionBD = ServiceNormas.GetAll(idGrupoSeleccionado);

            ObservableCollection<DO_INTEGRANTES_GRUPO> listaResultante = new ObservableCollection<DO_INTEGRANTES_GRUPO>();

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    DO_INTEGRANTES_GRUPO integrupo = new DO_INTEGRANTES_GRUPO();

                    Type tipo = item.GetType();

                    integrupo.idintegrantegrupo = (int)tipo.GetProperty("ID_INTEGRANTES_GRUPO").GetValue(item, null);
                    integrupo.idgrupo = (int)tipo.GetProperty("ID_GRUPO").GetValue(item, null);
                    integrupo.idusuariointegrante = (string)tipo.GetProperty("ID_USUARIO_INTEGRANTE").GetValue(item, null);
                    integrupo.nombrecompleto = (string)tipo.GetProperty("nombrecompleto").GetValue(item, null);
                    integrupo.IsSelected = false;

                    listaResultante.Add(integrupo);
                }
            }
            return listaResultante;
        }

        public static ObservableCollection<DO_USUARIO_NOTIFICACION_VERSION> GetAllUsuariosNotificacionVersion(int id_version)
        {
            SO_USUARIO_NOTIFICACION_VERSION Service = new SO_USUARIO_NOTIFICACION_VERSION();

            IList informacionBD = Service.GetAll(id_version);

            ObservableCollection<DO_USUARIO_NOTIFICACION_VERSION> ListaResultante = new ObservableCollection<DO_USUARIO_NOTIFICACION_VERSION>();

            if (informacionBD != null)
            {
                foreach (var item in informacionBD)
                {
                    DO_USUARIO_NOTIFICACION_VERSION obj = new DO_USUARIO_NOTIFICACION_VERSION();

                    Type tipo = item.GetType();

                    obj.id_usuario_notificacion_version = (int)tipo.GetProperty("ID_USUARIO_NOTIFICACION_VERSION").GetValue(item, null);
                    obj.id_usuario = (string)tipo.GetProperty("ID_USUARIO").GetValue(item, null);
                    obj.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.IsSelected = false;

                    ListaResultante.Add(obj);
                }
            }
            return ListaResultante;
        }

        /// <summary>
        /// Método para eliminar integrantes de los grupos
        /// </summary>
        /// <returns></returns>
        public static int eliminarintegrantes(int id_grupo, string id_integrante)
        {
            SO_INTEGRANTES_GRUPO serviceintegrantes = new SO_INTEGRANTES_GRUPO();

            return serviceintegrantes.DeleteIntegrantesGrupos(id_grupo, id_integrante);
        }

        /// <summary>
        /// Método para agregar nuevo integrante a cualquier grupo
        /// </summary>
        /// <returns></returns>
        public static int agregarintegrante(int id_grupo, string id_integrante_grupo)
        {
            SO_INTEGRANTES_GRUPO serviceintegrantes = new SO_INTEGRANTES_GRUPO();

            return serviceintegrantes.SetIntegrantesGrupos(id_grupo, id_integrante_grupo);
        }

        /// <summary>
        /// Método para crear un nuevo grupo
        /// </summary>
        /// <param name="id_norma"></param>
        /// <returns></returns>
        public static int CrearNuevoGrupo(string nombre_grupo, string id_usuario_dueno)
        {
            SO_GRUPOS servicegrupos = new SO_GRUPOS();

            return servicegrupos.SetGrupo(nombre_grupo, id_usuario_dueno);
        }

        /// <summary>
        /// Método para eliminar un grupo
        /// </summary>
        /// <param name="id_norma"></param>
        /// <returns></returns>
        public static int eliminarGrupos(int id_grupo)
        {
            SO_GRUPOS servicegrupos = new SO_GRUPOS();

            return servicegrupos.DeleteGrupo(id_grupo);
        }

        /// <summary>
        /// Método para insertar integrantes finales
        /// </summary>
        /// <param name="id_usuario"></param>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public static int InsertUserNotifyVersion(string id_usuario, int id_version)
        {
            SO_USUARIO_NOTIFICACION_VERSION Service = new SO_USUARIO_NOTIFICACION_VERSION();

            return Service.SetUserNotifyVersion(id_usuario, id_version);
        }

        /// <summary>
        /// Método para eliminar registros de la tabla TR_USUARIO_NOTIFICACIÓN_VERSION al momento de dar de baja algún documento
        /// </summary>
        /// <param name="id_version"></param>
        /// <returns></returns>
        public static int EliminarRegistroVersion(int id_version)
        {
            SO_USUARIO_NOTIFICACION_VERSION Service = new SO_USUARIO_NOTIFICACION_VERSION();

            return Service.DeleteRegistroVersion(id_version);
        }

        #endregion
    }
}