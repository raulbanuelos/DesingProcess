using iTextSharp.text;
using iTextSharp.text.pdf;
using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Input;
using View.Resources;

namespace View.Services.ViewModel
{
    public class DocumentosLiberarQR : INotifyPropertyChanged
    {

        #region Attributes
        public Usuario User;
        #endregion

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region Properties
        private ObservableCollection<Documento> _ListaDocumentos;
        public ObservableCollection<Documento> ListaDocumentosValidar
        {
            get
            {
                return _ListaDocumentos;
            }
            set
            {
                _ListaDocumentos = value;
                NotifyChange("ListaDocumentosValidar");
            }
        }
        private string _TextoBuscar;
        public string TextoBuscar
        {
            get
            {
                return _TextoBuscar;
            }
            set
            {
                _TextoBuscar = value;
                NotifyChange("TextoBuscar");
            }
        }
        public int contador = 1;

        #endregion

        #region Constructors
        public DocumentosLiberarQR(Usuario ModelUsuario)
        {

            User = ModelUsuario;
            inicampo(string.Empty);
        }
        #endregion

        #region Commands
        /// <summary>
        ///Comando que busca el documento
        ///segun se vaya escribiendo en el textbox
        ///cada letra que se escriba la va mandando como parametro
        ///</summary>
        public ICommand BuscarDocumento
        {
            get
            {
                return new RelayCommand(param => GetDocument((string)param));
            }
        }
        #endregion

        #region Methods
        private async void GetDocument(string param)
        {
            
            DialogService dialog = new DialogService();


            string desc = string.Empty; 
            string[] vec = new string[] { };

                try
                {
                    desc = Seguridad.DesEncriptar(param);

                    vec = desc.Split('*');

                if (contador < 2)
                {
                   if (vec.Length == 2)
                    {
                        contador++;

                        string codigoValidacion = vec[0];


                        ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentos_PendientesXLiberar(codigoValidacion);

                        if (ListaDocumentosValidar.Count == 1)
                        {
                            Documento doc = ListaDocumentosValidar[0];
                            if (doc.version.CodeValidation == codigoValidacion)
                            {
                                List<Archivo> ListArchivo = DataManagerControlDocumentos.GetArchivoFiltrado(doc.version.CodeValidation);

                                if (ListArchivo.Count > 0)
                                {
                                    verArchivo(ListArchivo[0]);

                                    //Liberamos el documento.
                                    liberarDocumento(doc);
                                }
                                else
                                {
                                    //Mensaje de no se encontró ningún archivo
                                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgArchivoNoEncontrado);
                                    inicampo(string.Empty);
                                }
                            }
                            else
                            {
                                //Mensaje de codigo de validación esta mal.
                                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgQRNoCorresponde);
                                inicampo(string.Empty);
                            }
                        }
                        else
                        {
                            //Mensaje de no se encontro el documento. Aquí probablemente se pueda indicar al usuario el estatus del documento.
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgDocumentoNoEstaPendiente);
                            inicampo(string.Empty);
                        }
                    }
                }
                    
                }
                catch (Exception)
                {

                }
        }

        private async void liberarDocumento(Documento documento)
        {
            DialogService dialog = new DialogService();

            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = StringResources.lblYes;
            setting.NegativeButtonText = StringResources.lblNo;

            //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
            MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgLiberarDocumento, setting, MessageDialogStyle.AffirmativeAndNegative);

            if (result == MessageDialogResult.Affirmative)
            {
                int numeroCopias = 0;
                //Ejecutamos el método para obtener el id de la versión anterior
                int last_version = DataManagerControlDocumentos.GetID_LastVersion(documento.id_documento, documento.version.id_version);

                //si el documento sólo tiene una versión, se modifica el estatus del documento y la versión, se cambia el estatus a liberado
                if (last_version == 0)
                {
                    //Establacemos el estatus de documento el cual es LIBERADO.
                    documento.id_estatus = 5;

                    //Ejecutamos el método para actualizar el estatus del documento.
                    int update_documento = DataManagerControlDocumentos.Update_EstatusDocumento(documento);

                    //Si se actualizó correctamente.
                    if (update_documento != 0)
                    {
                        documento.version.id_estatus_version = 1;
                        documento.version.no_copias = numeroCopias;

                        //Ejecutamos el método para guardar la versión. El resultado lo guardamos en una variable local.
                        int update_version = DataManagerControlDocumentos.UpdateVersion(documento.version, User, documento.nombre);

                        //Si la versión se actualizó correctamente.
                        if (update_version != 0)
                        {
                            //Insertamos el sello electrónico a los archivos que apliquen.
                            bool res = SetElectronicStamp(documento.version);

                            string confirmacionCorreo = string.Empty;

                            if (NotificarDocumentoDisponibleConSello(documento))
                                confirmacionCorreo = StringResources.msgNotificacionCorreo;
                            else
                                confirmacionCorreo = StringResources.msgNotificacionCorreoFallida;

                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgMatrizActualizada + "\n" + confirmacionCorreo);
                        }
                        else
                        {
                            //Mensaje de no se pudo actualizar la tabla TBL_VERSION
                            documento.id_estatus = 2;
                            update_documento = DataManagerControlDocumentos.Update_EstatusDocumento(documento);
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusVersionDocumento);
                        }

                    }
                    else
                    {
                        //Mensaje de no se pudo actualizar la tabla TBL_DOCUMENTO
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusDocumento);
                    }
                    // Actualizar lista de documentos pendientes por liberara cuando selecciona si
                    inicampo(string.Empty);
                }
                else
                {
                    //si el documento tiene más de un versión, sólo se modifica el estatus de la versión a liberado
                    //la versión anterior se modifica el estatus a obsoleto
                    int fecha_actualizacion = DataManagerControlDocumentos.UpdateFecha_actualizacion(documento.id_documento);

                    documento.version.id_estatus_version = 1;
                    documento.version.no_copias = numeroCopias;

                    //Ejecutamos el método para modificar el estatus de la versión. El resultado lo guardamos en una variable local.
                    int update_version = DataManagerControlDocumentos.UpdateVersion(documento.version, User, documento.nombre);

                    if (update_version != 0)
                    {
                        //Insertamos el sello electrónico a los archivos que apliquen.
                        bool rest = SetElectronicStamp(documento.version);

                        //obetemos el id de la versión anterior
                        int last_id = DataManagerControlDocumentos.GetID_LastVersion(documento.id_documento, documento.version.id_version);

                        //Creamos un objeto para la versión anterior 
                        Model.ControlDocumentos.Version lastVersion = new Model.ControlDocumentos.Version();

                        //asigamos el id y el estatus obsoleto
                        lastVersion.id_version = last_id;
                        lastVersion.id_estatus_version = 2;

                        //Se obtienen el número de versión de la version anterior
                        lastVersion.no_version = DataManagerControlDocumentos.GetNum_Version(last_id);

                        //Ejecutamos el método para actualizar el estatus de la versión(liberamos el documento).
                        int update = DataManagerControlDocumentos.Update_EstatusVersion(lastVersion, User, documento.nombre);

                        if (update != 0)
                        {
                            _LiberarEspacioBD(last_id);
                            string confirmacionCorreo = string.Empty;

                            if (NotificarDocumentoDisponibleConSello(documento))
                                confirmacionCorreo = StringResources.msgNotificacionCorreo;
                            else
                                confirmacionCorreo = StringResources.msgNotificacionCorreoFallida;

                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgMatrizActualizada + "\n" + confirmacionCorreo);
                        }
                    }
                    else
                    {
                        //Mensaje de no se pudo actualizar TBL_VERSION
                        //Si hubo error al actualizar la última versión
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorActualizarVersion);
                    }
                }
                // Se actualiza lista de documentos a liberar cuando la version es diferente a 1
                inicampo(string.Empty);
            }
            else
            {
                // Vuelve a cargar la lista cuando no se quiere liberar documento
                inicampo(string.Empty);
            }
        }

        /// <summary>
        /// Metodo que notifica vía Correo que un documento ya esta disponible para descarga con sello electónico.
        /// </summary>
        /// <returns></returns>
        private bool NotificarDocumentoDisponibleConSello(Documento documento)
        {
            ServiceEmail serviceMail = new ServiceEmail();

            string[] correos = new string[2];
            correos[0] = DataManager.GetUsuario(documento.version.id_usuario).Correo;
            correos[1] = DataManager.GetUsuario(documento.version.id_usuario_autorizo).Correo;

            string path = User.Pathnsf;
            string title = "Documento sellado y disponible - " + documento.nombre;
            string body = string.Empty;
            string tipo_documento = string.Empty;

            switch (documento.id_tipo_documento)
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
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que " + tipo_documento + " con el número <b> " + documento.nombre + "</b> versión <b> " + documento.version.no_version + ".0" + " </b> ya se encuentra disponible en el sistema <b> Diseño del proceso </b> con el sello correspondiente. </font> </li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Adicionalmente informo que se actualizo la matríz.</font></li>";
            body += "<br/>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Número : <b>" + documento.nombre + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Descripción : <b>" + documento.version.descripcion_v + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + documento.version.no_version + ".0" + "</b></font></li>";
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

            bool respuesta = serviceMail.SendEmailLotusCustom(path, correos, title, body, "CONTROL_DOCUMENTOS", documento.version.id_version);

            return respuesta;

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
        /// Método que agrega una marca de agua a los documentos que no son formatos ni ayudas visuales.
        /// </summary>
        /// <param name="version"></param>
        private bool SetElectronicStamp(Model.ControlDocumentos.Version version)
        {
            bool res = false;

            BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);
            ObservableCollection<Archivo> archivos = DataManagerControlDocumentos.GetArchivos(version.id_version);

            DateTime fecha_sello = DataManagerControlDocumentos.Get_DateTime();

            string dia = fecha_sello.Day.ToString().Length == 1 ? "0" + fecha_sello.Day : fecha_sello.Day.ToString();
            string anio = fecha_sello.Year.ToString();
            string mes = fecha_sello.Month.ToString().Length == 1 ? "0" + fecha_sello.Month : fecha_sello.Month.ToString();

            string fecha = dia + "/" + mes + "/" + anio;


            foreach (Archivo item in archivos)
            {

                string waterMarkText = "MAHLE CONTROL DE DOCUMENTOS / DOCUMENTO LIBERADO ELECTRÓNICAMENTE Y TIENE VELIDEZ SIN FIRMA." + " DISPOSICIÓN: " + fecha;
                string waterMarkText2 = "ÚNICAMENTE TIENE VALIDEZ EL DOCUMENTO DISPONIBLE EN INTRANET.";
                string waterMarkText3 = "LAS COPIAS NO ESTÁN SUJETAS A NINGÚN SERVICIO DE ACTUALIZACIÓN";

                byte[] newarchivo = AddWatermark(item.archivo, bfTimes, waterMarkText, waterMarkText2, waterMarkText3);

                item.archivo = newarchivo;

                int r = DataManagerControlDocumentos.UpdateArchivo(item);

                res = r == 0 ? false : true;
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
        private static byte[] AddWatermark(byte[] bytes, BaseFont baseFont, string watermarkText, string waterMarkText2, string waterMarkText3)
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

                        AddWaterMarkText2(dc, watermarkText, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 6), Convert.ToInt32(realPageSize.Bottom + 245));
                        AddWaterMarkText2(dc, waterMarkText2, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 12), Convert.ToInt32(realPageSize.Bottom + 160));
                        AddWaterMarkText2(dc, waterMarkText3, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 18), Convert.ToInt32(realPageSize.Bottom + 160));

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
        /// Método para ver el contenido de los archivos
        /// </summary>
        /// <param name="item"></param>
        private async void verArchivo(Archivo item)
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();
            if (item != null)
            {
                try
                {
                    //se asigna el nombre del archivo temporal, se concatena el nombre del archivo, la posicion de la lista y la extensión.
                    string filename = GetPathTempFile(item);

                    //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                    File.WriteAllBytes(filename, item.archivo);
                    //Se inicializa el programa para visualizar el archivo.
                    Process.Start(filename);
                }
                catch (Exception)
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorAbrir);
                }
            }
        }

        /// <summary>
        /// Método que genera una cadena para cargar un archivo en la carpeta temporal del sistema.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private string GetPathTempFile(Archivo item)
        {
            //Se guarda la ruta del directorio temporal.
            var tempFolder = Path.GetTempPath();
            string filename = string.Empty;
            //Realiza la acción hasta que el archivo se haya abierto
            do
            {
                //Genera un número aleatorio
                string aleatorio = Module.GetRandomString(5);
                //Crea la ruta temporal con el nombre del archivo y el número generado, y la extensión
                filename = Path.Combine(tempFolder, item.nombre + item.numero + "_" + aleatorio + item.ext);
            } while (File.Exists(filename));

            //retornamos el nombre que se generó
            return filename;
        }

        public void inicampo (string texto)
        {
            TextoBuscar = string.Empty;
            ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentos_PendientesLiberar(texto);
            contador = 1;
        }
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
