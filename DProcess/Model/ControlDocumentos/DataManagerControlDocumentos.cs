using DataAccess.ServiceObjects.ControlDocumentos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
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
            return  ServiceArchivo.SetArchivo(archivo.id_version, archivo.archivo, archivo.ext);

        }
        /// <summary>
        /// Método para modificar un registro de la tabla.
        /// </summary>
        /// <param name="archivo"></param>
        /// <returns></returns>
        public static int UpdateArchivo(Archivo archivo)
        {

            //Inicializamos los servicios de Archivo.
            SO_Archivo ServiceArchivo = new SO_Archivo();

            //Se ejecuta el método y regresa los registros modificados.
            return ServiceArchivo.UpdateArchivo(archivo.id_archivo, archivo.id_version, archivo.archivo, archivo.ext);
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
           string filename = Path.Combine(tempFolder, "temp"+obj.ext);
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
                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        /// <summary>
        /// Método para insertar un registro en la tabla.
        /// </summary>
        /// <param name="departamento"></param>
        /// <returns></returns>
        public static int SetDepartamento(Departamento departamento)
        {
            //Se inician los servicios de ]Departamento.
            SO_Departamento ServiceDepartamento = new SO_Departamento();

            //Se ejecuta el método y retorna el id del departamento que fue insertado.
            return ServiceDepartamento.SetDepartamento(departamento.id_dep, departamento.nombre_dep, departamento.fecha_creacion, departamento.fecha_actualizacion);
        }

        /// <summary>
        /// Método para modificar un registro de la tabla.
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
        /// Método para eliminar un registro de la tabla.
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
        #endregion

        #region Documento
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Documento> GetDocumento()
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
                    obj.descripcion = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                    obj.version_actual = (string)tipo.GetProperty("VERSION_ACTUAL").GetValue(item, null);
                    obj.fecha_emision = (DateTime)tipo.GetProperty("FECHA_EMISION").GetValue(item, null);
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
        /// Método para insertar un registro de la tabla TBL_Documento.
        /// </summary>
        /// <param name="documento"></param>
        /// <returns></returns>
        public static int SetDocumento(Documento documento)
        {
            //Se inician los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se ejecuta el método y retorna el id del documento que fue insertado.
            return ServiceDocumento.SetDocumento( documento.id_documento, documento.id_tipo_documento,documento.id_dep,documento.nombre, documento.descripcion,
                                                 documento.version_actual, documento.fecha_creacion, documento.fecha_actualizacion, documento.fecha_emision);
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
            return ServiceDocumento.UpdateDocumento(documento.id_documento,documento.id_tipo_documento,documento.id_dep, documento.nombre, documento.descripcion,
                                                 documento.version_actual, documento.fecha_creacion, documento.fecha_actualizacion, documento.fecha_emision);
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

        public static ObservableCollection<Documento> GetDataGrid(int idTipoDocumento,string textoBusqueda)
        {
            //Se inicializan los servicios de Documento.
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //obtenemos todo de la BD.
            IList ObjDocumento = ServiceDocumento.GetDataGrid(idTipoDocumento,textoBusqueda);

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
                    obj.fecha_actualizacion=(DateTime)tipo.GetProperty("FECHA_ACTUALIZACION").GetValue(item, null);
                    obj.version.no_version= (string)tipo.GetProperty("No_VERSION").GetValue(item, null);
                    obj.version.id_version=(int)tipo.GetProperty("ID_VERSION").GetValue(item, null);
                    obj.version.no_copias= (int)tipo.GetProperty("NO_COPIAS").GetValue(item, null);
                    obj.descripcion = (string)tipo.GetProperty("DESCRIPCION").GetValue(item, null);
                    obj.Departamento = (string)tipo.GetProperty("NOMBRE_DEPARTAMENTO").GetValue(item, null);
                    obj.fecha_emision = (DateTime)tipo.GetProperty("FECHA_EMISION").GetValue(item, null);
                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;
        }

        public static ObservableCollection<Documento> GetTipo(int id_documento)
        {
            SO_Documento ServiceDocumento = new SO_Documento();

            //Se crea una lista de tipo documento, la cual se va a retornar
            ObservableCollection<Documento> Lista = new ObservableCollection<Documento>();

            //obtenemos todo de la BD.
            IList ObjDocumento = ServiceDocumento.GetTipo(id_documento);

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
                    obj.nombre= (string)tipo.GetProperty("NOMBRE").GetValue(item, null);
                    obj.id_tipo_documento = (int)tipo.GetProperty("ID_TIPO_DOCUMENTO").GetValue(item, null);
                    obj.tipo.tipo_documento= (string)tipo.GetProperty("TIPO_DOCUMENTO").GetValue(item, null);
                    obj.version.archivo.id_archivo = (int)tipo.GetProperty("ID_ARCHIVO").GetValue(item, null);
                    obj.version.archivo.ext= (string)tipo.GetProperty("EXT").GetValue(item, null);
                    obj.version.archivo.archivo = (byte[])tipo.GetProperty("ARCHIVO").GetValue(item, null);

                    //Agregamos el objeto a la lista resultante.
                    Lista.Add(obj);
                }
            }
            //regresamos la lista.
            return Lista;

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
        #endregion  

        #region Usuarios

        /// <summary>
        /// Método para obtener todos los registros de la tabla Usuarios.
        /// </summary>
        /// <returns></returns>
        public static ObservableCollection<Usuarios> GetUsuarios(){


            //Inicializamos los servicios de usuarios.
            SO_Usuarios ServiceUsuarios = new SO_Usuarios();

            //Declaramos una lista de tipo ObservableCollection que será el que retornemos en el método.
            ObservableCollection<Usuarios> Lista = new ObservableCollection<Usuarios>();

            //Ejecutamos el método para obtener la información de la base de datos.
            IList ObjUsuarios = ServiceUsuarios.GetUsuario();

            //Comparamos que la información de la base de datos no sea nulo
            if (ObjUsuarios!=null)

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
            return ServiceUsuarios.SetUsuario(usuarios.usuario,usuarios.password, usuarios.nombre, usuarios.APaterno, usuarios.AMaterno,
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
            return ServiceVersion.SetVersion(version.id_version, version.id_usuario,version.id_documento, version.no_version, version.fecha_version, version.no_copias);
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
            return ServiceVersion.UpdateVersion(version.id_version, version.id_usuario, version.id_documento, version.no_version, version.fecha_version, version.no_copias);
        }

        /// <summary>
        /// Método para eliminar un registro de la BD.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static int DelteVersion(Version version)
        {
            //Inicializamos los servicios
            SO_Version ServiceVersion = new SO_Version();

            // Se ejecuta el método y retorna los registros que se eliminaron.
            return ServiceVersion.DeleteVersion(version.id_version);
        }
        #endregion

    }
}
