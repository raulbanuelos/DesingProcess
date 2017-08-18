using DataAccess.ServiceObjects.ControlDocumentos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
        public static void GetFile(int id_archivo)
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

            // Se ejecuta el método y retorna número de registros eliminados.
            return ServiceDepartamento.ValidateDepartamento(departamento.nombre_dep, departamento.Abreviatura);

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
                                                    documento.id_estatus, documento.usuario);
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
                                                 documento.fecha_actualizacion, documento.id_estatus, documento.fecha_emision);
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
        public static ObservableCollection<Documento> GetDataGrid(int idTipoDocumento, string textoBusqueda)
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
                    obj.fecha_actualizacion = (DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
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

            // Se ejecuta el método y retorna los registros que se modificaron.
            return ServiceDocumento.UpdateFecha_Actualizacion(id_doc);
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
            return ServiceDocumento.GetNumero(numero, tipoDocumento.id_tipo);
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
                    obj.descripcion = (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
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
        /// Método para obtener las coincidencias de una descripción
        /// </summary>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> ValidateDocumentosSimilares(Documento doc)
        {
            //Se inicializa los servicios de documento
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            string descripcion = DeleteAccents(doc.descripcion);

            //obtenemos un vector sin espacios de la descripción
            string[] aux = descripcion.Split(' ');

            // obtenemos todo de la BD.
            IList ObjDocumento = ServiceDocumento.ValidateDescripcion(doc.id_tipo_documento, doc.id_dep, doc.id_documento);

            if (ObjDocumento != null)
            {
                //Iteramos la lista que se obtuvo
                foreach (var item in ObjDocumento)
                {
                    //Obtenemos el tipo.
                    System.Type tipo = item.GetType();

                    //Declaramos el objeto 
                    Documento obj = new Documento();
                    int cont = 0;

                    //Asigamos los valores
                    obj.id_documento = (int)tipo.GetProperty("ID_DOCUMENTO").GetValue(item, null);
                    obj.nombre = (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.version.id_version = (int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.version.descripcion_v = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                    obj.version.no_version = (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.fecha_version = (DateTime)tipo.GetProperty("FECHA_VERSION").GetValue(item, null);

                    //Se manda a llamar a la función para eliminar acentos
                    string descVersion = DeleteAccents(obj.version.descripcion_v);

                    //Se obtiene un vector sin espacios, de la descripcion de la versión
                    string[] vec = descVersion.Split(' ');

                    //Recorremos el vector de la descripción que se va a dar de alta
                    for (int i = 0; i < aux.Length; i++)
                    {
                        //Recorremos el vector de la descripción de la versión que se obtuvo
                        for (int k = 0; k < vec.Length; k++)
                        {
                            //Comparamos si son iguales las palabras ignorando las mayúsculas
                            if (aux[i].Equals(vec[k], StringComparison.InvariantCultureIgnoreCase) || Compara(aux[i],vec[k]))
                            {
                                //Incrementamos el contador
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
                    int porcentaje = (cont * 100)/ vec.Length;

                    //si el porcentaje es mayor a 80% y si tiene una diferencia mayor a 4 palabras, la descripción se parece y agregamos el objeto a la lista
                    if (porcentaje >= 80 && resta <=4)
                    {
                        //Se agrega el objeto a la lista
                        Lista.Add(obj);
                    }
                   
                }
            }
            return Lista;
        }

        /// <summary>
        /// Función que compara dos palabras
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
                    if(w1[i].ToString().Equals(w2[j].ToString(),StringComparison.InvariantCultureIgnoreCase))
                     {
                        //si son iguales el caractér en la posición i y en la posición j
                        //suma el contador y rompe el ciclo
                        contador++;
                        break;
                     }

                }
            }

            if (w1.Length == 0)
                return false;
            
            //calculamos el porcentaje de coincidencia
            int porciento = (contador * 100) / w1.Length;

            //si el porcentaje es mayor a 80%, la palabra es parecida, retorna verdadero
            if (porciento >= 80)
                return true;
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
            return ServiceTipo.SetTipo(tipo.id_tipo, tipo.tipo_documento, tipo.abreviatura, tipo.fecha_creacion, tipo.fecha_actualizacion);
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
        public static int ValidateTipo(TipoDocumento tipo)
        {
            SO_TipoDocumento ServiceTipo = new SO_TipoDocumento();

            // Se ejecuta el método y retorna los registros que se eliminaron.
            return ServiceTipo.ValidateTipo(tipo.tipo_documento, tipo.abreviatura);
        }
        #endregion  

        #region Usuarios

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

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista.
            return Lista;
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
                                              usuarios.estado, usuarios.usql, usuarios.psql, usuarios.bloqueado);

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
                                              usuarios.estado, usuarios.usql, usuarios.psql, usuarios.bloqueado);

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

            //Se ejecuta el método y retorna número de registros eliminados.
            return ServiceUsuarios.DeleteUsuario(usuarios.usuario);
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
        #endregion

        #region version

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
        /// Método para insertar un registto a la Bd.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static int SetVersion(Version version)
        {
            //Inicializamos los servicios
            SO_Version ServiceVersion = new SO_Version();

            //Se ejecuta el método y regresa el id de la versión
            return ServiceVersion.SetVersion(version.id_usuario, version.id_usuario_autorizo, version.id_documento, version.no_version,
                                             version.fecha_version, version.no_copias, version.id_estatus_version, version.descripcion_v);
        }

        /// <summary>
        /// Método para modificar un regitro de la BD.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static int UpdateVersion(Version version)
        {
            //Inicializamos los servicios
            SO_Version ServiceVersion = new SO_Version();

            // Se ejecuta el método y retorna los registros que se modificarion
            return ServiceVersion.UpdateVersion(version.id_version, version.id_usuario, version.id_usuario_autorizo, version.id_documento, 
                                                version.no_version, version.fecha_version, version.no_copias, version.id_estatus_version, version.descripcion_v);
        }

        /// <summary>
        /// Método para eliminar un registro de la BD.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static int DeleteVersion(Version version)
        {
            //Inicializamos los servicios
            SO_Version ServiceVersion = new SO_Version();

            // Se ejecuta el método y retorna los registros que se eliminaron.
            return ServiceVersion.DeleteVersion(version.id_version);
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
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();

            //Declaramos un entero el cual será el que contenga la última versión.
            int version = 0;

            //Obtenemos la última versión.
            string ultimaVersión = ServiceVersion.GetLastVersion(id_documento);

            //Verificamos que la última versión sea numérica, si es así la obtenemos. Si no es numérica, se generará la versión 1.
            if (IsNumeric(ultimaVersión))
            {
                version = Convert.ToInt32(ultimaVersión);
            }

            //se agrega uno.
            version++;

            //etorna el nuevo valor de la versión, convertido a string.
            return Convert.ToString(version);
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

            return ServiceVersion.GetLastVersion_Id(id_documento, id_version);
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
                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método que obtiene las versiones que están pendientes por aprobar de todos los usuarios
        /// </summary>
        /// <param name="nombreUsuario"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumentosValidar(string nombreUsuario)
        {
            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Documento> ListaResultante = new ObservableCollection<Documento>();

            //Inicializamos los servicios de Documento.
            SO_Documento ServicioDocumento = new SO_Documento();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServicioDocumento.GetDocumentosValidar(nombreUsuario);

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
        public static ObservableCollection<Documento> GetDocumentos_PendientesCorregir(string usuario)
        {
            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //Inicializamos los servicios de documento.
            SO_Documento ServicioDocumento = new SO_Documento();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServicioDocumento.GetDocumentosPendientes(usuario);

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
        /// </summary>
        /// <param name="usuario"></param>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumentos_PendientesLiberar()
        {
            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //Inicializamos los servicios de documento.
            SO_Documento ServicioDocumento = new SO_Documento();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList informacionBD = ServicioDocumento.GetDocumentosAprobados();

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

                    //Agregamos a la lista resultante
                    Lista.Add(obj);
                }
            }
            //Retornamos la lista
            return Lista;
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
        public static int Update_EstatusVersion(Version obj)
        {
            //Inicializamos los servicios de version.
            SO_Version ServiceVersion = new SO_Version();

            //Ejecutamos el método y retornamos el resultado
            return ServiceVersion.UpdateEstatus_Version(obj.id_version, obj.id_estatus_version);
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
            return ServiceValidacion.SetValidacion(obj.validacion_documento, obj.validacion_descripcion);
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
        public static int GetIDValidacion(string descripcion) {
            //Se inicializan los servicios de Validacion.
            SO_Validacion ServiceValidacion = new SO_Validacion();

            //Ejecutamos el método
            return ServiceValidacion.GetID_Validacion(descripcion);
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

            int id= ServiceBloqueo.GetID_Bloqueo(fecha_final);

            if (id !=0) {

                ServiceBloqueo.UpdateEstado(id);
            }
            //Ejecutamos el método que obtiene el id del registro activo
         
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

    }
}
