﻿using MahApps.Metro.Controls;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Model;
using View.Resources;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace View.Services.ViewModel
{
    public class ValidarDocumentoViewM : INotifyPropertyChanged
    {
        #region Attributes
        private Usuario _usuarioLogueado;
        #endregion

        #region Propiedades
        private ObservableCollection<Archivo> _ListaArchivos = new ObservableCollection<Archivo>();
        public ObservableCollection<Archivo> ListaArchivos
        {
            get
            {
                return _ListaArchivos;
            }
            set
            {
                _ListaArchivos = value;
                NotifyChange("ListaArchivos");
            }
        }

        private Documento selectedDocumento;
        public Documento SelectedDocumento
        {
            get
            {
                return selectedDocumento;
            }
            set
            {
                selectedDocumento = value;
                NotifyChange("SelectedDocumento");
            }
        }

        private Archivo _selectedItem;
        public Archivo SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                NotifyChange("SelectedItem");
            }

        }

        private Model.ControlDocumentos.Version _usuario;
        public Model.ControlDocumentos.Version Usuario
        {
            get
            {
                return _usuario;
            }
            set
            {
                _usuario = value;
                NotifyChange("Usuario");
            }

        }

        private bool isSelected = false;
        public bool IsSelected
        {
            get
            {
                return isSelected;
            }
            set
            {
                isSelected = value;
                NotifyChange("IsSelected");
            }
        }

        private string _estatus = StringResources.lblPendientePorCorregir;
        public string Estatus
        {
            get
            {
                return _estatus;
            }
            set
            {
                _estatus = value;
                NotifyChange("Estatus");
            }
        }

        public DialogService dialog = new DialogService();

        #endregion

        #region Constructores
        /// <summary>
        /// ViewModel de ventana FrmValidarDocumento donde el administrador válida un documento
        /// Cambia el estatus Corregir o aprobado
        /// </summary>
        /// <param name="documento"></param>
        /// <param name="usuarioLogueado"></param>
        public ValidarDocumentoViewM(Documento documento, Usuario usuarioLogueado)
        {

            _usuarioLogueado = usuarioLogueado;
            //Obtenemos la información del documento y de la versión
            SelectedDocumento = DataManagerControlDocumentos.GetDocumento(documento.id_documento, documento.version.no_version);

            //Ejecutamos el método para obtener los archivos de la versión
            ObservableCollection<Documento> Lista = DataManagerControlDocumentos.GetArchivos(documento.id_documento, documento.version.id_version);

            //Ejecutamos el método para obtener el id del usuario que elaboró la versión
            Usuario = DataManagerControlDocumentos.GetIdUsuario(documento.version.id_version);

            //Iteramos la lista de dpcumentos
            foreach (var item in Lista)
            {
                SelectedDocumento.tipo.tipo_documento = item.tipo.tipo_documento;
                SelectedDocumento.Departamento = item.Departamento;
                Archivo objArchivo = new Archivo();

                objArchivo.nombre = item.nombre;
                objArchivo.id_archivo = item.version.archivo.id_archivo;
                objArchivo.archivo = item.version.archivo.archivo;
                objArchivo.ext = item.version.archivo.ext;

                if (objArchivo.ext == ".pdf")
                {
                    //asigna la imagen del pdf al objeto
                    objArchivo.ruta = @"/Images/p.png";
                }
                else
                {
                    //Si es archivo de word asigna la imagen correspondiente.
                    objArchivo.ruta = @"/Images/w.png";
                }
                ListaArchivos.Add(objArchivo);
            }
        }
        #endregion

        #region Commandos
        /// <summary>
        /// Comando para ver el archivo desde la lista de documentos.
        /// </summary>
        public ICommand VerArchivo
        {
            get
            {
                return new RelayCommand(o => verArchivo(SelectedItem));
            }
        }

        /// <summary>
        /// Comando que guarda el estatus del documento
        /// </summary>
        public ICommand Guardar
        {
            get
            {
                return new RelayCommand(g => guardarEstatus());
            }
        }

        /// <summary>
        /// Comando para cambiar el combobox
        /// </summary>
        public ICommand Checked
        {
            get
            {
                return new RelayCommand(g => check());
            }
        }

        /// <summary>
        /// Comando para cambiar estatus 
        /// </summary>
        public ICommand Unchecked
        {
            get
            {
                return new RelayCommand(g => uncheck());
            }
        }

        public ICommand VerVistaPrevia
        {
            get
            {
                return new RelayCommand(o => verVistaPrevia(SelectedItem));
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para visualizar el archivo con el sello electrónico.
        /// </summary>
        /// <param name="archivo"></param>
        private async void verVistaPrevia(Archivo archivo)
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();
            try
            {
                //Si hay un archivo seleccionado
                if (archivo != null)
                {
                    BaseFont bfTimes = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, false);

                    //se asigna el nombre del archivo temporal, se concatena el nombre del archivo, la posicion de la lista y la extensión.
                    string filename = GetPathTempFile(archivo);

                    string waterMarkText = "MAHLE CONTROL DE DOCUMENTOS / DOCUMENTO LIBERADO ELECTRÓNICAMENTE Y TIENE VELIDEZ SIN FIRMA." + " DISPOSICIÓN: " + "00/00/0000";
                    string waterMarkText2 = "ÚNICAMENTE EL SELLO ES PARA PRUEBA Y NO TIENE NINGUNA VALIDEZ.";
                    string waterMarkText3 = "LAS COPIAS CON ESTE  SELLO NO TIENEN   NINGUNA VALIDEZ OFICIAL.";

                    byte[] newarchivo = AddWatermark(archivo.archivo, bfTimes, waterMarkText, waterMarkText2, waterMarkText3);

                    //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                    File.WriteAllBytes(filename, newarchivo);

                    //Se inicializa el programa para visualizar el archivo.
                    Process.Start(filename);
                }
            }
            catch (Exception er)
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorAbrir);
            }
        }

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



                        Rectangle realPageSize = reader.GetPageSizeWithRotation(i);

                        AddWaterMarkText2(dc, watermarkText, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 6), Convert.ToInt32(realPageSize.Bottom + 245));
                        AddWaterMarkText2(dc, waterMarkText2, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 12), Convert.ToInt32(realPageSize.Bottom + 160));
                        AddWaterMarkText2(dc, waterMarkText3, baseFont, 6, 90, BaseColor.BLACK, Convert.ToInt32(realPageSize.Left + 18), Convert.ToInt32(realPageSize.Bottom + 160));

                    }
                    stamper.Close();
                }
                return ms.ToArray();
            }
        }

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
        /// Funcíón que modifica la versión
        /// </summary>
        /// <param name="objVersion"></param>
        public async void UpdateVersion(Model.ControlDocumentos.Version objVersion)
        {
            //Se llama al método para actualizar el estatus de la version
            int update_version = DataManagerControlDocumentos.Update_EstatusVersion(objVersion, _usuarioLogueado, SelectedDocumento.nombre);

            if (update_version != 0)
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusVersionActualizada);
                //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                if (IsSelected)
                {
                    DO_Notification notificacion = new DO_Notification();
                    notificacion.TITLE = StringResources.ttlDocumentoAprobado;
                    notificacion.MSG = StringResources.msgDocumento + " " + SelectedDocumento.nombre + "\n" + StringResources.lblVersion + " " + selectedDocumento.version.no_version + "\n" + StringResources.ttlEntregarDocumento;
                    notificacion.TYPE_NOTIFICATION = 1;
                    notificacion.ID_USUARIO_RECEIVER = Usuario.id_usuario;
                    notificacion.ID_USUARIO_SEND = "ADMINISTRADOR";

                    DataManagerControlDocumentos.insertNotificacion(notificacion);

                }
                else
                {
                    DO_Notification notificacion = new DO_Notification();
                    notificacion.TITLE = StringResources.ttlDocumentoRechazado;
                    notificacion.MSG = StringResources.msgDocumento + " " + SelectedDocumento.nombre + "\n" + StringResources.lblVersion + " " + selectedDocumento.version.no_version + "\n" + StringResources.ttlRechazarDocumento;
                    notificacion.TYPE_NOTIFICATION = 3;
                    notificacion.ID_USUARIO_RECEIVER = Usuario.id_usuario;
                    notificacion.ID_USUARIO_SEND = "ADMINISTRADOR";

                    DataManagerControlDocumentos.insertNotificacion(notificacion);
                }

                //Verificamos que la pantalla sea diferente de nulo.
                if (window != null)
                {
                    //Cerramos la pantalla
                    window.Close();
                }
            }
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusVersion);
            }
        }

        /// <summary>
        /// Método que cambia el estatus aprobado o pendiente por liberar
        /// Si el documento sólo tiene una versión cambia el estatus de documento y versión
        /// De lo contrario cambia estatus de versión
        /// </summary>
        private async void guardarEstatus()
        {
            //isSelected es falso, id_estatus=pendiente por corregir, verdadero estatus= aprobado pendiente por liberar
            //

            string version = SelectedDocumento.version.no_version;
            Model.ControlDocumentos.Version objVersion = new Model.ControlDocumentos.Version();
            objVersion.id_version = SelectedDocumento.version.id_version;
            objVersion.no_version = SelectedDocumento.version.no_version;

            int last_id = DataManagerControlDocumentos.GetID_LastVersion(SelectedDocumento.id_documento, SelectedDocumento.version.id_version);

            // Si el checkbox es verdadero
            if (isSelected == true)
            {
                //Si el documento no tiene otra versión
                if (last_id == 0)
                {
                    //Actualiza el estatus de la versión y del documento a pendiente por liberar
                    selectedDocumento.id_estatus = 4;
                    objVersion.id_estatus_version = 5;

                    //Se llama al método para actualizar el estatus del documento
                    int n = DataManagerControlDocumentos.Update_EstatusDocumento(SelectedDocumento);

                    //si se realizo la actualizacion
                    if (n != 0)
                    {
                        //Se llama a la función para actualizar el estatus de la versión
                        UpdateVersion(objVersion);

                        // Se manda notificación al correo
                        if (selectedDocumento.id_tipo_documento == 1003 || selectedDocumento.id_tipo_documento == 1005 || selectedDocumento.id_tipo_documento == 1006 || selectedDocumento.id_tipo_documento == 1011 || selectedDocumento.id_tipo_documento == 1012 || selectedDocumento.id_tipo_documento == 1013 || selectedDocumento.id_tipo_documento == 1014)
                        {
                            string confirmacion = string.Empty;
                            if (NotificarDocumentoAprobado())
                            {
                                confirmacion = StringResources.msgNotificacionCorreo;
                            }
                            else
                            {
                                confirmacion = StringResources.msgNotificacionCorreoFallida;

                            }
                            await dialog.SendMessage(StringResources.ttlAlerta, confirmacion);
                        }
                    }
                    else
                    {
                        //Se muestra que hubo un error al actualizar el documento
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusDocumento);
                    }
                }
                else
                {
                    //si es un documento con versión anterior liberada.
                    objVersion.id_estatus_version = 5;

                    //Se llama a la función para actualizar el estatus de la versión
                    UpdateVersion(objVersion);
                }

            }
            else
            {
                //Si el documento no tiene una versión anterior liberada
                if (last_id == 0)
                {
                    //Actualiza el estatus de la versión y del documento a pendiente por corregir
                    selectedDocumento.id_estatus = 3;
                    objVersion.id_estatus_version = 4;

                    //Se llama al método para actualizar el estatus del documento
                    int n = DataManagerControlDocumentos.Update_EstatusDocumento(SelectedDocumento);

                    //si se realizo la actualizacion
                    if (n != 0)
                    {
                        //Se llama a la función para actualizar el estatus de la versión
                        UpdateVersion(objVersion);

                        // Se manda notificación al correo
                        if (selectedDocumento.id_tipo_documento == 1003 || selectedDocumento.id_tipo_documento == 1005 || selectedDocumento.id_tipo_documento == 1006 || selectedDocumento.id_tipo_documento == 1011 || selectedDocumento.id_tipo_documento == 1012 || selectedDocumento.id_tipo_documento == 1013 || selectedDocumento.id_tipo_documento == 1014)
                        {
                            string confirmacion = string.Empty;
                            if (NotificarDocumentoAprobado())
                            {
                                confirmacion = StringResources.msgNotificacionCorreo;
                            }
                            else
                            {
                               confirmacion = StringResources.msgNotificacionCorreoFallida;

                            }
                            await dialog.SendMessage(StringResources.ttlAlerta, confirmacion);

                        }

                    }
                    else
                    {
                        //Se muestra que hubo un error al actualizar el documento
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusDocumento);
                    }
                }
                else
                {
                    //si es un documento con versión una versión anterior liberada .
                    //Estatus pendiente por corregir.
                    objVersion.id_estatus_version = 4;
                    //Se llama a la función para actualizar el estatus de la versión
                    UpdateVersion(objVersion);
                }

            }
        }

        /// <summary>
        /// Si el checkbox cambia a seleccionado falso, la etiqeuta cambia de estado
        /// </summary>
        private void uncheck()
        {
            Estatus = StringResources.lblPendientePorCorregir;
        }

        /// <summary>
        /// Método que cambia la etiqueta si el checkbox fue seleccionado
        /// </summary>
        private void check()
        {
            Estatus = StringResources.lblAprobadoPendienteLiberar;
        }

        /// <summary>
        /// Método que visualiza el archivo seleccioando de la Lista
        /// </summary>
        /// <param name="item"></param>
        private async void verArchivo(Archivo item)
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();
            try
            {
                //Si hay un archivo seleccionado
                if (item != null)
                {
                    //se asigna el nombre del archivo temporal, se concatena el nombre del archivo, la posicion de la lista y la extensión.
                    string filename = GetPathTempFile(item);

                    //Crea un archivo nuevo temporal, escribe en él los bytes extraídos de la BD.
                    File.WriteAllBytes(filename, item.archivo);

                    //Se inicializa el programa para visualizar el archivo.
                    Process.Start(filename);
                }
            }
            catch (Exception er)
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorAbrir);
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
            do
            {
                string aleatorio = Module.GetRandomString(5);

                filename = Path.Combine(tempFolder, item.nombre + item.numero + "_" + aleatorio + item.ext);
            } while (File.Exists(filename));

            return filename;
        }

        private bool NotificarDocumentoAprobado()
        {
            ServiceEmail serviceMail = new ServiceEmail();
            string CorreoUsuarioElaboro = DataManagerControlDocumentos.GetCorreoUsuario(SelectedDocumento.version.id_usuario);
            DateTime fechahoy = DataManagerControlDocumentos.Get_DateTime();
            DateTime fechaCompromisoEntrega = DataManagerControlDocumentos.AddBusinessDays(fechahoy, 2);

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

            string fechacompromiso = fechaCompromisoEntrega.Year + "-" + FechaMes + "-" + FechaDia + "  " + hora + ":" + minuto;

            string[] correos = new string[2];
            correos[0] = CorreoUsuarioElaboro;
            correos[1] = "raul.banuelos@mx.mahle.com";

            string path = _usuarioLogueado.Pathnsf;
            string title = "Documento aprobado - " + SelectedDocumento.nombre;
            string body = string.Empty;
            string tipo_documento = string.Empty;

            switch (SelectedDocumento.id_tipo_documento)
            {
                case 1012:
                    tipo_documento = "EL FORMATO ESPECÍFICO";
                    break;
                case 1013:
                    tipo_documento = "EL FORMATO OHSAS";
                    break;
                case 1014:
                    tipo_documento = "EL FORMATO ISO";
                    break;
                case 1011:
                    tipo_documento = "LA MIE";
                    break;
                case 1003:
                    tipo_documento = "EL PROCEDIMIENTO OHSAS";
                    break;
                case 1005:
                    tipo_documento = "EL PROCEDIMIENTO ESPECÍFICO";
                    break;
                case 1006:
                    tipo_documento = "EL PROCEDIMIENTO ISO";
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
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que " + tipo_documento + " con el número <b> " + SelectedDocumento.nombre + "</b> versión <b> " + SelectedDocumento.version.no_version + ".0" + " </b> ha sido aprobado y tiene hasta el día <b>  " + fechacompromiso + " </b> si no el sistema lo rechazará automáticamente. </font> </li>";
            body += "<br/>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Número : <b>" + SelectedDocumento.nombre + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Descripción : <b>" + SelectedDocumento.descripcion + "</b></font></li>";
            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + SelectedDocumento.version.no_version + ".0" + "</b></font></li>";
            body += "</ul>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Cualquier duda quedo a sus órdenes</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor no responda.</font> </p>";
            body += "<br/>";
            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
            body += "<ul>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + _usuarioLogueado.Nombre + " " + "</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
            body += "<li></li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + _usuarioLogueado.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </li>";
            body += "</ul>";
            body += "</body>";
            body += "</HTML>";

            bool respuesta = serviceMail.SendEmailLotusCustom(path, correos, title, body);

            return respuesta;

        }

        private string definirSaludo()
        {
            DateTime d = DateTime.Now;
            string saludo = string.Empty;

            return d.Hour <= 11 ? "Buenos días;" : "Buenas tardes;";
        }

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




    }
}
