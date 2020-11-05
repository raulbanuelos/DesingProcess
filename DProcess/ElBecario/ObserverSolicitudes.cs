using iTextSharp.text;
using iTextSharp.text.pdf;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using TableDependency.SqlClient.Base.EventArgs;
using View.Services;

namespace ElBecario
{
    public class ObserverSolicitudes
    {
        Usuario User;
        #region Constructor
        public ObserverSolicitudes(Usuario _User)
        {
            User = _User;
            initObserver();
        }
        #endregion

        private void initObserver()
        {
            Console.WriteLine("Inicializando Observador....\n");

            SqlTableDependency<DO_Solicitud_Control_Documentos> tableDependency;
            var connectionString = System.Configuration.ConfigurationManager.AppSettings["CadenaConexion"];
            tableDependency = new SqlTableDependency<DO_Solicitud_Control_Documentos>(connectionString, "TBL_SOLICITUD_CONTROL_DOCUMENTO");

            tableDependency.OnChanged += TableDependency_OnChanged;
            tableDependency.OnError += TableDependency_OnError;
            tableDependency.Start();

            Console.WriteLine("Ya estoy al tiro para cualquier cosa que quieran....\n");
        }

        private void TableDependency_OnError(object sender, TableDependency.SqlClient.Base.EventArgs.ErrorEventArgs e)
        {
            Exception ex = e.Error;
            throw ex;
        }

        private async void TableDependency_OnChanged(object sender, RecordChangedEventArgs<DO_Solicitud_Control_Documentos> e)
        {
            if (e.ChangeType == TableDependency.SqlClient.Base.Enums.ChangeType.Insert)
            {
                var changedEntity = e.Entity;

                Console.Beep(5000, 1000);

                Console.WriteLine("Chinga! ya me pusieron a trabajar, ni pex!");
                Console.WriteLine("Fecha de inicio: " + DateTime.Now.ToString());
                //System.Threading.Thread.Sleep(3000);
                Console.WriteLine("Un wey esta solicitando lo siguiente:");
                Console.WriteLine("VERSIÓN: " + changedEntity.ID_VERSION + "\nACCIÓN SOLICITADA: " + changedEntity.ACCION + "\n");

                //System.Threading.Thread.Sleep(3000);
                Console.WriteLine("Deja empiezo a atender la solicitud, tu no te preocupes.");

                int idSolicitud = DataManagerControlDocumentos.getIdSolicitudControlDocumentos(changedEntity.ID_VERSION, changedEntity.ACCION, changedEntity.FECHA_SOLICITUD);

                if (changedEntity.ACCION == "LIBERAR")
                {
                    await iniciarLiberacion(changedEntity.ID_VERSION, idSolicitud);
                }
                else
                {
                    if (changedEntity.ACCION == "RECHAZAR")
                    {
                        iniciarRechazo(idSolicitud, changedEntity.ID_VERSION, changedEntity.COMENTARIO);
                    }
                }

            }
        }

        private bool iniciarRechazo(int idSolicitud, int idVersion, string comentario)
        {
            Documento objDocumento = DataManagerControlDocumentos.GetDocumento(idVersion);
            objDocumento.version = DataManagerControlDocumentos.GetVersion(idVersion);

            if (rechazar(objDocumento))
            {
                if (notificarRechazo(objDocumento, comentario))
                {
                    //Establecemos que esta solicitud ya fué realizada.
                    DataManagerControlDocumentos.setDoneSolicitudControlDocumentos(idSolicitud);

                    Console.WriteLine("Eliminare la solicitud: " + idSolicitud + " para que no exista registro de este pedo que estamos haciendo");
                    //Eliminamos el registro de la tabla solictudes.
                    int cc = DataManagerControlDocumentos.deleteSolicitudControlDocumentos(idSolicitud);

                    if (cc > 0)
                    {
                        Console.WriteLine("Se eliminó el registro de las solicitudes");
                    }else
                    {
                        Console.WriteLine("OOOppsss! tube un pedo para eliminar el registro de las solicitudes.");
                    }

                    Console.WriteLine("Listo Campeón!!, este documento ya está en estatus de pendientes por corregir");
                    //System.Threading.Thread.Sleep(2000);
                    Console.WriteLine("Deja voy por unas papas y una coca, cualquier cosa estoy al pendiente.");
                }
            }
            return true;
        }

        private bool notificarRechazo(Documento objDocumento, string comentario)
        {
            Console.WriteLine("Vamos a empezar a notificar por correo, ya para terminar, que ya me quiero echar otro sueño!");
            //System.Threading.Thread.Sleep(3000);

            //Declaramos una lista la cual almacenará todos los id's de los usuarios que se van a notificar.
            List<string> lUsuariosNotificar = new List<string>();

            lUsuariosNotificar.Add(objDocumento.version.id_usuario);

            string[] correos = new string[lUsuariosNotificar.Count];
            Usuario usuarioAutorizo = DataManager.GetUsuario(objDocumento.version.id_usuario_autorizo);

            int i = 0;
            //Iteramos la lista con los id´s de los usuarios para obtener el correo.
            foreach (var item in lUsuariosNotificar)
            {
                string correo = DataManagerControlDocumentos.GetCorreoUsuario(item);
                correos[i] = correo;
                i++;
            }

            ServiceEmail SO_Email = new ServiceEmail();
            string path = User.Pathnsf;
            string title = "Control de Documentos --> Documento rechazado: " + objDocumento.nombre;
            string body = string.Empty;

            body = "<HTML>";
            body += "<head>";
            body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
            body += "</head>";
            body += "<body text=\"white\">";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + definirSaludo() + "</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que el documento " + objDocumento.nombre + " fué rechazado por " + usuarioAutorizo.Nombre + " " + usuarioAutorizo.ApellidoPaterno + " </li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">El motivo de rechazo se muestra en el siguiente texto:</font></li>";
            body += "<br/>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\"><b>" + comentario + "</b></font></li>";
            body += "</ul>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Para corregir el documento, por favor ingrese a la plataforma y en la sección de pendientes por corregir, usted podrá sustituir el documento e iniciar de nuevo el proceso de alta.</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente.</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Nombre + " " + User.ApellidoPaterno + "</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
            body += "<li></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a> </li>";
            body += "</ul>";
            body += "</body>";
            body += "</HTML>";

            //Ejecutamos el método para notificar por correo
            bool respuesta = SO_Email.SendEmailLotusCustom(path, correos, title, body);

            return respuesta;

            Console.WriteLine("El correo se envió perron!!");
            //System.Threading.Thread.Sleep(3000);
            return true;
        }

        private bool rechazar(Documento objDocumento)
        {
            Console.WriteLine("Deja empiezo a rechazar el documento...");
            //System.Threading.Thread.Sleep(3000);

            int last_id = DataManagerControlDocumentos.GetID_LastVersion(objDocumento.id_documento, objDocumento.version.id_version);

            if (last_id == 0)
            {
                objDocumento.id_estatus = 3;
                int n = DataManagerControlDocumentos.Update_EstatusDocumento(objDocumento);

                if (n != 0)
                {
                    objDocumento.version.id_estatus_version = 4;
                    //Se llama al método para actualizar el estatus de la version
                    int update_version = DataManagerControlDocumentos.Update_EstatusVersion(objDocumento.version, User, objDocumento.nombre);
                    Console.WriteLine("Listo documento rechazado");
                    //System.Threading.Thread.Sleep(3000);

                    return true;
                }
                else
                {
                    Console.WriteLine("Hubo un error al querer rechazar el documento: " + objDocumento.nombre);
                    return false;
                }
            }
            else
            {
                objDocumento.version.id_estatus_version = 4;
                //Se llama al método para actualizar el estatus de la version
                int update_version = DataManagerControlDocumentos.Update_EstatusVersion(objDocumento.version, User, objDocumento.nombre);
                Console.WriteLine("Listo documento rechazado");
                //System.Threading.Thread.Sleep(3000);
                return true;
            }
        }

        private async Task<bool> iniciarLiberacion(int idVersion, int idSolicitud)
        {
            Documento objDocumento = DataManagerControlDocumentos.GetDocumento(idVersion);

            Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
            objVersion = DataManagerControlDocumentos.GetVersion(idVersion);
            objDocumento.version = objVersion;

            if(objDocumento.id_tipo_documento == 2 || objDocumento.id_tipo_documento == 1002 || objDocumento.id_tipo_documento == 1004 || objDocumento.id_tipo_documento == 1015)
            {
                Console.WriteLine("Este documento SI va sellado. id tipo documento= " + objDocumento.id_tipo_documento);
                if (await sellar(idVersion))
                {
                    if (liberar(objDocumento, objVersion))
                    {
                        if (notificarLiberacion(objDocumento))
                        {
                            //Establecemos que esta solicitud ya fué realizada.
                            DataManagerControlDocumentos.setDoneSolicitudControlDocumentos(idSolicitud);

                            //Eliminamos el registro de la tabla solictudes.
                            DataManagerControlDocumentos.deleteSolicitudControlDocumentos(idSolicitud);

                            Console.WriteLine("Listo Campeón!!, este documento fué liberado exitosamente, Juntos somos Invensibles.");
                            //System.Threading.Thread.Sleep(2000);
                            Console.WriteLine("Arriba el Ame!.");
                            Console.WriteLine("Cualquier cosa estoy al pendiente y te aviso ok. Tu sigue durmiendo.");
                            
                        }
                        else
                        {
                            Console.WriteLine("Uuhhh!! valió mauser la notificación por correo.");
                            Console.WriteLine("Pero el documento ya esta liberado, haz paro y notifica de manera manual, y después arreglame para no tener estos pedos.");

                            //Eliminamos el registro de la tabla solictudes.
                            DataManagerControlDocumentos.deleteSolicitudControlDocumentos(idSolicitud);
                            //System.Threading.Thread.Sleep(4000);
                            Console.WriteLine("Voy a seguir al pendiente por cualquier cosa que se ofrezca.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Uta! no pude liberar el documento we, porfa liberalo manualmente ya que!!!");
                        return false;
                    }
                }
            }
            else
            {
                Console.WriteLine("Este documento NO va sellado. id tipo documento= " + objDocumento.id_tipo_documento);
                if (liberar(objDocumento, objVersion))
                {
                    if (notificarLiberacion(objDocumento))
                    {
                        //Establecemos que esta solicitud ya fué realizada.
                        DataManagerControlDocumentos.setDoneSolicitudControlDocumentos(idSolicitud);

                        //Eliminamos el registro de la tabla solictudes.
                        DataManagerControlDocumentos.deleteSolicitudControlDocumentos(idSolicitud);

                        Console.WriteLine("Listo Campeón!!, este documento fué liberado exitosamente, Juntos somos Invensibles.");
                        //System.Threading.Thread.Sleep(2000);
                        Console.WriteLine("Arriba el Ame!.");
                        Console.WriteLine("Cualquier cosa estoy al pendiente y te aviso ok. Tu sigue durmiendo.");
                    }
                    else
                    {
                        Console.WriteLine("Uuhhh!! valió mauser la notificación por correo.");
                        Console.WriteLine("Pero el documento ya esta liberado, haz paro y notifica de manera manual, y después arreglame para no tener estos pedos.");

                        //Eliminamos el registro de la tabla solictudes.
                        DataManagerControlDocumentos.deleteSolicitudControlDocumentos(idSolicitud);
                        //System.Threading.Thread.Sleep(4000);
                        Console.WriteLine("Voy a seguir al pendiente por cualquier cosa que se ofrezca.");
                    }
                }
                else
                {
                    Console.WriteLine("Uta! no pude liberar el documento we, porfa liberalo manualmente ya que!!!");
                    return false;
                }
            }

            return true;
        }

        private bool notificarLiberacion(Documento objDocumento)
        {
            Console.WriteLine("Vamos a empezar a notificar por correo, ya para terminar, que ya me quiero echar otro sueño!");
            //System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Vamos verificando la gente que el dueño de documento quiere que le notifiquemos.");
            //System.Threading.Thread.Sleep(4000);

            //Declaramos una lista la cual almacenará todos los id's de los usuarios que se van a notificar.
            List<string> lUsuariosNotificar = new List<string>();

            //Asignamos por default los id´s del dueño del documento así como del usuario que aprueba el documento.
            lUsuariosNotificar.Add(objDocumento.version.id_usuario_autorizo);
            lUsuariosNotificar.Add(objDocumento.version.id_usuario);

            //Declaramos una los con los usuarios que seleccionó el dueño de documento para notifcarles.
            ObservableCollection<DO_USUARIO_NOTIFICACION_VERSION> ListaUsuariosCorreoCompleta = DataManagerControlDocumentos.GetAllUsuariosNotificacionVersion(objDocumento.version.id_version);

            //Llenamos la lista con los id de los usuarios
            foreach (var item in ListaUsuariosCorreoCompleta)
                lUsuariosNotificar.Add(item.id_usuario);

            //Declaramos una lista con los usuarios suscritos a los cambios de este documento.
            ObservableCollection<DO_UsuarioSuscrito> ListaUsuariosSuscritos = DataManagerControlDocumentos.Get_UserSuscripDoc(objDocumento.id_documento);

            //Llenamos la lista con los id de los usuarios
            foreach (var item in ListaUsuariosSuscritos)
                lUsuariosNotificar.Add(item.id_usuariosuscrito);

            //Se agrega el usuario Raúl Bañuelos. <--Por Default.
            lUsuariosNotificar.Add("¢¥®ª¯");

            //Declaramos el vector el cual guardará los correos de los usuarios.
            string[] correos = new string[lUsuariosNotificar.Count];

            int i = 0;
            //Iteramos la lista con los id´s de los usuarios para obtener el correo.
            foreach (var item in lUsuariosNotificar)
            {
                string correo = DataManagerControlDocumentos.GetCorreoUsuario(item);
                correos[i] = correo;
                i++;
            }

            //Eliminamos correos duplicados
            correos = Module.EliminarCorreosDuplicados(correos);

            Console.WriteLine("Ok, Ok, listo ya tengo la lista, deja empiezo a armar el correo...");
            //System.Threading.Thread.Sleep(3500);


            //Verificamos si son documentos Procedimientos y Formatos
            if (objDocumento.id_tipo_documento == 1003 || objDocumento.id_tipo_documento == 1005 || objDocumento.id_tipo_documento == 1006 || objDocumento.id_tipo_documento == 1012 || objDocumento.id_tipo_documento == 1013 || objDocumento.id_tipo_documento == 1014 || objDocumento.id_tipo_documento == 1011)
            {
                //Ejecutamos el método para obtener el id de la versión anterior
                int last_version = DataManagerControlDocumentos.GetID_LastVersion(objDocumento.id_documento, objDocumento.version.id_version);

                NotificarNuevaVersion(objDocumento, correos, last_version == 0 ? true : false);

            }
            else
            {
                string confirmacionCorreo = string.Empty;

                if (NotificarDocumentoDisponibleConSello(objDocumento, correos))
                    //confirmacionCorreo = StringResources.msgNotificacionCorreo;
                    confirmacionCorreo = "TODO: Mensaje de confirmación de correo";
                else
                    //confirmacionCorreo = StringResources.msgNotificacionCorreoFallida;
                    confirmacionCorreo = "TODO:Mensaje de error al enviar correo";

                //await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgMatrizActualizada + "\n" + confirmacionCorreo);
            }

            Console.WriteLine("Todo Ok, a enviar ahora si el correo....");
            //System.Threading.Thread.Sleep(3000);
            Console.WriteLine("El correo se envió perrón!!");
            //System.Threading.Thread.Sleep(3000);
            return true;
        }

        /// <summary>
        /// Metodo que notifica vía Correo que un documento ya esta disponible para descarga con sello electónico.
        /// </summary>
        /// <returns></returns>
        private bool NotificarDocumentoDisponibleConSello(Documento objDocumento, string[] correos)
        {
            ServiceEmail serviceMail = new ServiceEmail();


            string path = User.Pathnsf;
            string title = "Documento sellado y disponible - " + objDocumento.nombre;
            string body = string.Empty;
            string tipo_documento = string.Empty;

            switch (objDocumento.id_tipo_documento)
            {
                case 2:
                    tipo_documento = "la HOE";
                    break;
                case 1002:
                    tipo_documento = "la HII";
                    break;
                case 1004:
                    tipo_documento = "la ayuda visual";
                    break;
                case 1007:
                    tipo_documento = "la HMTE";
                    break;
                case 1015:
                    tipo_documento = "la JES";
                    break;
                case 1010:
                    tipo_documento = "la HVA";
                    break;
                case 1011:
                    tipo_documento = "la MIE";
                    break;
                default:
                    break;
            }

            body = "<HTML>";
            body += "<head>";
            body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
            body += "</head>";
            body += "<body text=\"white\">";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + definirSaludo() + "</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que " + tipo_documento + " con el número <b> " + objDocumento.nombre + "</b> versión <b> " + objDocumento.version.no_version + ".0" + " </b> ya se encuentra disponible en el sistema <b> Diseño del proceso </b> con el sello correspondiente. </font> </li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Adicionalmente informo que se actualizo la matríz.</font></li>";
            body += "<br/>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Número : <b>" + objDocumento.nombre + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Descripción : <b>" + objDocumento.descripcion + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + objDocumento.version.no_version + ".0" + "</b></font></li>";
            body += "</ul>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Cualquier duda quedo a sus órdenes</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor solo responda en caso de que el documento sustituya a algún otro.</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Nombre + " " + User.ApellidoPaterno + "</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
            body += "<li></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </li>";
            body += "</ul>";
            body += "</body>";
            body += "</HTML>";

            bool respuesta = serviceMail.SendEmailLotusCustom(path, correos, title, body);

            return respuesta;
        }

        /// <summary>
        /// Método que notifica vía correo el alta de un documento.
        /// </summary>
        /// <returns></returns>
        private bool NotificarNuevaVersion(Documento objDocumento, string[] correos, bool isFirstVersion)
        {
            ServiceEmail SO_Email = new ServiceEmail();

            string path = User.Pathnsf;
            string title = "Alta de documento - " + objDocumento.nombre;
            string body = string.Empty;
            string tipo_documento = string.Empty;

            switch (objDocumento.id_tipo_documento)
            {
                case 1003:
                case 1005:
                case 1006:
                    tipo_documento = "la instrucción de trabajo";
                    break;
                case 1012:
                case 1013:
                case 1014:
                    tipo_documento = "el formato";
                    break;
                default:
                    break;
            }

            body = "<HTML>";
            body += "<head>";
            body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
            body += "</head>";
            body += "<body text=\"white\">";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + definirSaludo() + "</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que " + tipo_documento + " con el número <b> " + objDocumento.nombre + "</b> versión <b> " + objDocumento.version.no_version + ".0" + " </b> ya se encuentra disponible en el sistema </font> <a href=\"http://sealed/frames.htm\">frames</a> </li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Adicionalmente informo que se actualizo la matríz.</font></li>";
            body += "<br/>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Número : <b>" + objDocumento.nombre + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Descripción : <b>" + objDocumento.version.descripcion_v + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + objDocumento.version.no_version + ".0" + "</b></font></li>";
            //body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Área del Frames en donde se inserto : <b>" + AreaFrames + "</b></font></li>";
            body += "</ul>";
            if (isFirstVersion)
            {
                body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">NOTA: Si este documento sustituye a algún otro, favor de notificarme para realizar la baja correspondiente.</font> </p>";
            }
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Cualquier duda quedo a sus órdenes</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor solo responda en caso de que el documento sustituya a algún otro.</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Nombre + " " + User.ApellidoPaterno + "</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
            body += "<li></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + User.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a> </li>";
            body += "</ul>";
            body += "</body>";
            body += "</HTML>";

            //Ejecutamos el método para notificar por correo
            bool respuesta = SO_Email.SendEmailLotusCustom(path, correos, title, body);

            return respuesta;
        }

        private bool liberar(Documento objDocumento, Model.ControlDocumentos.Version objVersion)
        {
            Console.WriteLine("Deja empiezo a liberar el documento...");
            //System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Que tiempos aquellos cuando tenias que registrar uno por uno las liberaciones!!!");
            //System.Threading.Thread.Sleep(5000);
            Console.WriteLine("Como ha cambiado el tiempo verdad?, tan fácil que es ahora.");
            //System.Threading.Thread.Sleep(4000);

            //Ejecutamos el método para obtener el id de la versión anterior
            int last_version = DataManagerControlDocumentos.GetID_LastVersion(objDocumento.id_documento, objVersion.id_version);

            //si el documento sólo tiene una versión, se modifica el estatus del documento y la versión, se cambia el estatus a liberado.
            if (last_version == 0)
            {
                //Estatus de documento liberado
                objDocumento.id_estatus = 5;

                //Ejecutamos el método para actualizar el estatus del documento.
                int update_documento = DataManagerControlDocumentos.Update_EstatusDocumento(objDocumento);

                if (update_documento != 0)
                {
                    objVersion.id_estatus_version = 1;

                    //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                    int update_version = DataManagerControlDocumentos.UpdateVersion(objVersion, User, objDocumento.nombre);

                    if (update_version != 0)
                    {
                        string file = SaveFile(objDocumento.id_tipo_documento, objDocumento.nombre, objVersion.no_version, objVersion.id_version);

                        if (file == null)
                        {

                            // Llamamos el método para eliminar los registros de la tabla TR_USUARIO_NOTIFICACION_VERSION por ID_VERSION, una vez que el documento sea liberado
                            DataManagerControlDocumentos.EliminarRegistroVersion(objVersion.id_version);
                        }
                        else
                        {
                            Console.WriteLine("Error al copiar el documento al servidor\nidVersion: " + objVersion.id_version);
                            return false;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Error al actualizar version\nidVersion : " + objVersion.id_version);
                        objDocumento.id_estatus = 2;
                        update_documento = DataManagerControlDocumentos.Update_EstatusDocumento(objDocumento);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Error al actualizar el estátus del documento\nidDocumento : " + objDocumento.id_documento);
                    objDocumento.id_estatus = 2;
                    return false;
                }
            }
            else
            {
                //si el documento tiene más de un versión, sólo se modifica el estatus de la versión a liberado
                //la versión anterior se modifica el estatus a obsoleto.
                int fecha_actualizacion = DataManagerControlDocumentos.UpdateFecha_actualizacion(objDocumento.id_documento);

                objVersion.id_estatus_version = 1;
                objVersion.no_copias = 0;


                //Ejecutamos el método para modificar el estatus de la versión. El resultado lo guardamos en una variable local.
                int update_version = DataManagerControlDocumentos.UpdateVersion(objVersion, User, objDocumento.nombre);

                if (update_version != 0)
                {
                    //obetemos el id de la versión anterior
                    int last_id = DataManagerControlDocumentos.GetID_LastVersion(objDocumento.id_documento, objVersion.id_version);

                    //Creamos un objeto para la versión anterior 
                    Model.ControlDocumentos.Version lastVersion = new Model.ControlDocumentos.Version();

                    //asigamos el id y el estatus obsoleto
                    lastVersion.id_version = last_id;
                    lastVersion.id_estatus_version = 2;

                    //Se obtienen el número de versión de la version anterior
                    lastVersion.no_version = DataManagerControlDocumentos.GetNum_Version(last_id);

                    //Ejecutamos el método para actualizar el estatus de la versión(liberamos el documento).
                    int update = DataManagerControlDocumentos.Update_EstatusVersion(lastVersion, User, objDocumento.nombre);

                    //si se actualizó correctamente
                    if (update != 0)
                    {
                        string file = SaveFile(objDocumento.id_tipo_documento, objDocumento.nombre, objVersion.no_version, objVersion.id_version);


                        //Si los registros fueron guardados exitosamente el archivo que queda como obsoleto se pasa a la carpeta de respaldo y se elimina de la base de datos
                        _LiberarEspacioBD(last_id);

                        // Llamamos el método para eliminar los registros de la tabla TR_USUARIO_NOTIFICACION_VERSION por ID_VERSION, una vez que el documento sea liberado
                        DataManagerControlDocumentos.EliminarRegistroVersion(objVersion.id_version);

                    }
                    else
                    {
                        Console.WriteLine("Error al copiar el documento al servidor\nidVersion:" + objVersion.id_version);
                        return false;
                    }
                }
                else
                {
                    Console.WriteLine("Error al actualizar version\nidVersion : " + objVersion.id_version);
                    return false;
                }

            }

            Console.WriteLine("Listo, el documento ya esta liberado");
            //System.Threading.Thread.Sleep(3000);
            Console.WriteLine("Vamos al último paso");
            //System.Threading.Thread.Sleep(3000);
            return true;
        }

        /// <summary>
        /// Método que guarda el archivo de tipo OHSAS, ESPECIFICOS, ISO14001 en sealed//documents__
        /// </summary>
        private string SaveFile(int idTipoDocumento, string nombreDocumento, string noVersion, int idVersion)
        {
            string nombre_tipo;
            try
            {   //Si es documneto de tipo especifico o formato
                if (idTipoDocumento == 1003 || idTipoDocumento == 1005 || idTipoDocumento == 1006 || idTipoDocumento == 1012 || idTipoDocumento == 1013 || idTipoDocumento == 1014 || idTipoDocumento == 1011)
                {
                    string path = @"\\MXAGSQLSRV01\documents__";
                    //Switch del tipo de documento
                    switch (idTipoDocumento)
                    {
                        //Si de tipo OHSAS
                        case 1003:
                        case 1013:
                            nombre_tipo = "OHSAS";
                            path = string.Concat(path, @"\", nombre_tipo, @"\", nombreDocumento, noVersion);
                            break;
                        //Si es de tipo específicos
                        case 1005:
                        case 1012:
                        case 1011:
                            nombre_tipo = "ESPECIFICOS";
                            path = string.Concat(path, @"\", nombre_tipo, @"\", nombreDocumento, noVersion);
                            break;
                        //Si es de tipo ISO14001
                        case 1006:
                        case 1014:
                            nombre_tipo = "ISO14001";
                            path = string.Concat(path, @"\", nombre_tipo, @"\", nombreDocumento, noVersion);
                            break;
                    }

                    ObservableCollection<Archivo> archivos = DataManagerControlDocumentos.GetArchivos(idVersion);

                    //Iteramos la lista de archivos
                    foreach (var item in archivos)
                    {

                        //Concatenamos la ruta y la extensión
                        path = string.Concat(path, item.ext);
                        //Guardamos el archivo
                        File.WriteAllBytes(path, item.archivo);
                    }
                }
                //Si no hay error se retorna nulo
                return null;
            }
            catch (Exception er)
            {
                //Si hay error se retorna el error
                return er.ToString();
            }
        }

        private async Task<bool> sellar(int idVersion)
        {

            bool respuesta = false;
            Console.WriteLine("Estoy sellando el documento....");

            Model.ControlDocumentos.Version version = DataManagerControlDocumentos.GetVersion(idVersion);
            int idTipoDocumento = DataManagerControlDocumentos.GetTipoDocumentoByIdVersion(idVersion);
            respuesta = await SetElectronicStamp(version, idTipoDocumento);
            //System.Threading.Thread.Sleep(3000);

            Console.WriteLine("Mientras lo voy sellando, recuerdo cuando sellabas el documento a mano... que fácil te estoy haciendo la vida.....");
            //System.Threading.Thread.Sleep(6000);

            Console.WriteLine("Listo, documento sellado...");
            //System.Threading.Thread.Sleep(4000);

            Console.WriteLine("Ahora vamos a lo que sigue.");
            //System.Threading.Thread.Sleep(3000);

            return respuesta;
        }

        #region Liberar

        /// <summary>
        /// Método que agrega una marca de agua a los documentos que no son formatos ni ayudas visuales.
        /// </summary>
        /// <param name="version"></param>
        private async Task<bool> SetElectronicStamp(Model.ControlDocumentos.Version version, int idTipoDocumento)
        {
            bool res = false;
            if (idTipoDocumento != 1003 && idTipoDocumento != 1005 && idTipoDocumento != 1006 && idTipoDocumento != 1012 && idTipoDocumento != 1013 && idTipoDocumento != 1014 && idTipoDocumento != 1011)
            {
                BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
                ObservableCollection<Archivo> archivos = DataManagerControlDocumentos.GetArchivos(version.id_version);

                DateTime fecha_sello = DataManagerControlDocumentos.Get_DateTime();

                string dia = fecha_sello.Day.ToString().Length == 1 ? "0" + fecha_sello.Day : fecha_sello.Day.ToString();
                string anio = fecha_sello.Year.ToString();
                string mes = fecha_sello.Month.ToString().Length == 1 ? "0" + fecha_sello.Month : fecha_sello.Month.ToString();

                string fecha = dia + "/" + mes + "/" + anio;

                Usuario personaAutorizo = DataManager.GetUsuario(version.id_usuario_autorizo);

                foreach (Archivo item in archivos)
                {
                    string waterMarkText = "MAHLE CONTROL DE DOCUMENTOS / DOCUMENTO LIBERADO ELECTRÓNICAMENTE Y TIENE VALIDEZ SIN FIRMA." + " DISPOSICIÓN: " + fecha;
                    string waterMarkText2 = "ÚNICAMENTE TIENE VALIDEZ EL DOCUMENTO DISPONIBLE EN INTRANET.";
                    string waterMarkText3 = "LAS COPIAS NO ESTÁN SUJETAS A NINGÚN SERVICIO DE ACTUALIZACIÓN.";
                    string waterMarkText4 = "DOCUMENTO APROBADO ELECTRÓNICAMNETE POR " + personaAutorizo.Nombre.ToUpper() + " " + personaAutorizo.ApellidoPaterno.ToUpper();

                    byte[] newarchivo = AddWatermark(item.archivo, bfTimes, waterMarkText, waterMarkText2, waterMarkText3,waterMarkText4, false);

                    item.archivo = newarchivo;

                    int r = DataManagerControlDocumentos.UpdateArchivo(item);

                    res = r == 0 ? false : true;
                }


            }
            return res;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="baseFont"></param>
        /// <param name="watermarkText"></param>
        /// <param name="waterMarkText2"></param>
        /// <param name="waterMarkText3"></param>
        /// <returns></returns>
        public static byte[] AddWatermark(byte[] bytes, BaseFont baseFont, string watermarkText, string waterMarkText2, string waterMarkText3, string waterMarkText4, bool banAyudaVisual)
        {
            using (var ms = new MemoryStream(10 * 1024))
            {
                using (var reader = new PdfReader(bytes))
                using (var stamper = new PdfStamper(reader, ms))
                {
                    var pages = reader.NumberOfPages;

                    for (var i = 1; i <= pages; i++)
                    {
                        var dc = stamper.GetOverContent(i);
                        //AddWaterMarkText(dc, watermarkText, baseFont, 8, 0, BaseColor.BLACK, reader.GetPageSizeWithRotation(i), 10, 315);
                        //AddWaterMarkText(dc, waterMarkText2, baseFont, 8, 0, BaseColor.BLACK, reader.GetPageSizeWithRotation(i), 20, 290);

                        iTextSharp.text.Rectangle realPageSize = reader.GetPageSizeWithRotation(i);

                        /*if (recurso != 0 && (recurso == 1054 || recurso == 1055))
                        {
                            if (recurso == 1054)
                            {
                                //Vertical
                                AddWaterMarkText2(dc, watermarkText, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 28), Convert.ToInt32(realPageSize.Bottom + 350));
                                AddWaterMarkText2(dc, waterMarkText2, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 34), Convert.ToInt32(realPageSize.Bottom + 265));
                                AddWaterMarkText2(dc, waterMarkText3, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 40), Convert.ToInt32(realPageSize.Bottom + 265));
                            }
                            else
                            {
                                //Horizontal
                                AddWaterMarkText2(dc, watermarkText, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 28), Convert.ToInt32(realPageSize.Bottom + 285));
                                AddWaterMarkText2(dc, waterMarkText2, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 34), Convert.ToInt32(realPageSize.Bottom + 210));
                                AddWaterMarkText2(dc, waterMarkText3, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 40), Convert.ToInt32(realPageSize.Bottom + 210));
                            }
                        }
                        else
                        {
                            AddWaterMarkText2(dc, watermarkText, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 6), Convert.ToInt32(realPageSize.Bottom + 245));
                            AddWaterMarkText2(dc, waterMarkText2, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 12), Convert.ToInt32(realPageSize.Bottom + 160));
                            AddWaterMarkText2(dc, waterMarkText3, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 18), Convert.ToInt32(realPageSize.Bottom + 160));
                        }*/

                        AddWaterMarkText2(dc, watermarkText, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 6), Convert.ToInt32(realPageSize.Bottom + 245));
                        AddWaterMarkText2(dc, waterMarkText2, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 12), Convert.ToInt32(realPageSize.Bottom + 160));
                        AddWaterMarkText2(dc, waterMarkText4, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 18), Convert.ToInt32(realPageSize.Bottom + 160));
                        
                    }
                    stamper.Close();
                }
                return ms.ToArray();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pdfData"></param>
        /// <param name="watermarkText"></param>
        /// <param name="font"></param>
        /// <param name="fontSize"></param>
        /// <param name="angle"></param>
        /// <param name="color"></param>
        /// <param name="pos_x"></param>
        /// <param name="pos_y"></param>
        public static void AddWaterMarkText2(PdfContentByte pdfData, string watermarkText, BaseFont font, float fontSize, float angle, BaseColor color, int pos_x, int pos_y)
        {
            var gstate = new PdfGState { FillOpacity = 1.0f, StrokeOpacity = 1.0f };

            pdfData.SaveState();
            pdfData.SetGState(gstate);
            pdfData.SetColorFill(color);
            pdfData.BeginText();
            pdfData.SetFontAndSize(font, fontSize);
            var x = pos_x;
            var y = pos_y;

            pdfData.ShowTextAligned(Element.ALIGN_CENTER, watermarkText, x, y, angle);
            pdfData.EndText();
            pdfData.RestoreState();

        }

        /// <summary>
        /// Método que define si es "Buenos días" o "Buenas tardes" dependiendo la hora.
        /// </summary>
        /// <returns></returns>
        private string definirSaludo()
        {
            DateTime d = DateTime.Now;
            string saludo = string.Empty;

            return d.Hour <= 11 ? "Buenos días;" : "Buenas tardes;";
        }

        /// <summary>
        /// Método que copia los archivos que tengan estatus obsoleto a una carpeta de respaldo
        /// </summary>
        public string _LiberarEspacioBD(int IdVersionEliminar)
        {
            try
            {
                ObservableCollection<Documento> data = DataManagerControlDocumentos.GetDocumentosObsoletos(IdVersionEliminar);

                foreach (var item in data)
                {
                    string NombreFolder = string.Empty;

                    switch (item.id_tipo_documento)
                    {
                        case 2:
                            //asignamos la ruta donde se va a crear el nuevo folder mas el nombre del folder
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\HOJA DE OPERACION ESTANDAR\" + item.nombre;
                            break;
                        case 1002:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\HOJA DE INSTRUCCION DE INSPECCION\" + item.nombre;
                            break;
                        case 1003:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\PROCEDIMIENTO OHSAS\" + item.nombre;
                            break;
                        case 1004:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\AYUDAS VISUALES\" + item.nombre;
                            break;
                        case 1005:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\PROCEDIMIENTO ESPECIFICO\" + item.nombre;
                            break;
                        case 1006:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\PROCEDIMIENTO ISO\" + item.nombre;
                            break;
                        case 1007:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\HOJA DE METODO DE TRABAJO ESTANDAR\" + item.nombre;
                            break;
                        case 1011:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\METODO DE INSPECCION ESTANDARIZADO\" + item.nombre;
                            break;
                        case 1012:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\FORMATO ESPECIFICO\" + item.nombre;
                            break;
                        case 1013:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\FORMATO OHSAS\" + item.nombre;
                            break;
                        case 1014:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\FORMATO ISO\" + item.nombre;
                            break;
                        case 1015:
                            NombreFolder = @"Z:\NUEVO SOFTWARE RUTAS\RespaldoControlDocumentos\JES\" + item.nombre;
                            break;
                    }

                    if (!System.IO.Directory.Exists(NombreFolder))
                    {
                        //creamos el folder
                        System.IO.Directory.CreateDirectory(NombreFolder);
                    }

                    //Asignamos el nombre del archivo, concatenamos el nombre y el número de la version.
                    string NombreArchivo = item.nombre + "_" + item.version.no_version + item.version.archivo.ext;

                    //Creamos la ruta donde se pondran los archivos
                    string pathString = System.IO.Path.Combine(NombreFolder, NombreArchivo);

                    //Obtenemos el arreglo de bytes que representan el archivo
                    byte[] file = item.version.archivo.archivo;

                    //Lo copiamos a la carpeta
                    System.IO.File.WriteAllBytes(pathString, file);

                }

                EliminarDocumentos(data);

                return "Ok";
            }
            catch (Exception error)
            {
                return error.Message;
            }
        }

        /// <summary>
        /// Método que elimina los archivos que esten en estatus obsoleto
        /// </summary>
        /// <param name="data"></param>
        public void EliminarDocumentos(ObservableCollection<Documento> data)
        {
            foreach (var item in data)
            {
                DataManagerControlDocumentos.DeleteArchivo(item.version.archivo);
            }
        }
        #endregion

    }
}