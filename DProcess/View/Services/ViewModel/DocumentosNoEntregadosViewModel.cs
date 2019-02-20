using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Resources;
using MahApps.Metro.Controls;

namespace View.Services.ViewModel
{
    public class DocumentosNoEntregadosViewModel : INotifyPropertyChanged
    {

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

        #region Propiedades
        public Usuario usuario;
        public bool IsSelected;

        private ObservableCollection<DO_DocumentosRechazados> _ListaDocumentos;
        public ObservableCollection<DO_DocumentosRechazados> ListaDocumentos
        {
            get
            {
                return _ListaDocumentos;
            }
            set
            {
                _ListaDocumentos = value;
                NotifyChange("ListaDocumentos");
            }
        }

        private string _Titulo = "Deseleccionar Todos";
        public string Titulo
        {
            get
            {
                return _Titulo;
            }
            set
            {
                _Titulo = value;
                NotifyChange("Titulo");
            }
        }
        #endregion

        #region Constructor
        public DocumentosNoEntregadosViewModel(Usuario ModelUsuario)
        {
            ListaDocumentos = DataManagerControlDocumentos.GetDocumentosAprobadosNoRecibidos(true);
            usuario = new Usuario();
            usuario = ModelUsuario;
        }
        #endregion
        
        #region Comandos

        /// <summary>
        /// Comando para rechazar todos los documentos que no fueron entregados
        /// </summary>
        public ICommand RechazarDocumentos
        {
            get
            {
                return new RelayCommand(a => RechazarDocumentosNoEntregados());
            }
        }

        public ICommand SelecDeselec
        {
            get
            {
                return new RelayCommand(a => _SelecDeselec());
            }
        }
        #endregion

        #region Métodos

        /// <summary>
        /// Método que rechaza todos los documentos que no fueron entregados
        /// </summary>
        public async void RechazarDocumentosNoEntregados()
        {
            if (ListaDocumentos.Count > 0)
            {
                //Incializamos los servicios de dialog.
                DialogService dialog = new DialogService();

                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;

                bool Resultado = false;

                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, "¿Deseas rechazar los documentos que tienen mas de dos dias sin entregar? \n Si rechazas los documentos se notificará al dueño del documento vía correo. ", setting, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    //Rechazar el documento.
                    foreach (DO_DocumentosRechazados documentoRezadado in ListaDocumentos)
                    {
                        //Verificamos que el documento este seleccionado para poder rechazarlo
                        if (documentoRezadado.IsSelected == true)
                        {
                            //Obtenemos el ID de la version.
                            int idVersion = DataManagerControlDocumentos.GetIdVersion(documentoRezadado.NombreDocumento, documentoRezadado.NoVersion);

                            ////Rechazamos el documento
                            DataManagerControlDocumentos.SetRechazarVersion(idVersion);

                            DataManagerControlDocumentos.InsertHistorialVersion(idVersion, usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno, documentoRezadado.NombreDocumento, documentoRezadado.NoVersion, "Se cambia el estatus a: PENDIENTE POR CORREGIR");

                            ServiceEmail serviceEmail = new ServiceEmail();

                            string[] correos = new string[2];

                            correos[0] = documentoRezadado.Correo;

                            //CAMBIAR AL USUARIO ACTIVO.
                            correos[1] = usuario.Correo;

                            //Construimos en mensaje.
                            string body = "<HTML>";
                            body += "<head>";
                            body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
                            body += "</head>";
                            body += "<body text=\"white\">";
                            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + UsuarioViewModel.definirSaludo() + "</font> </p>";
                            body += "<ul>";
                            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que el documento con el número <b> " + documentoRezadado.NombreDocumento + "</b> versión <b> " + documentoRezadado.NoVersion + ".0" + " </b> fué rechazado y puesto en estatus Pendiente por corregir debido a que no se entrego a tiempo</font> </li>";
                            body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Adicionalmente informo que se actualizó la fecha de la versión.</font></li>";
                            body += "<br/>";
                            body += "</ul>";
                            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Cualquier duda quedo a sus órdenes</font> </p>";
                            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\"></font> </p>";
                            body += "<br/>";
                            body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor no responda.</font> </p>";
                            body += "<br/>";
                            body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
                            body += "<ul>";
                            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + usuario.Nombre + " " + usuario.ApellidoPaterno + "</font> </li>";
                            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
                            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
                            body += "<li></li>";
                            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
                            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
                            body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + usuario.Correo + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </li>";
                            body += "</ul>";
                            body += "</body>";
                            body += "</HTML>";

                            Resultado = serviceEmail.SendEmailLotusCustom(usuario.Pathnsf, correos, "Documento rechazado - " + documentoRezadado.NombreDocumento, body);
                            
                        }
                    }

                    if (Resultado)
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgNotificacionCorreo);

                    }
                    else
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgNotificacionCorreoFallida);
                    }

                    ListaDocumentos = DataManagerControlDocumentos.GetDocumentosAprobadosNoRecibidos(true);
                }
            }
        }

        /// <summary>
        /// Método que selecciona todos los archivos
        /// </summary>
        public void _SeleccionarTodos()
        {
            ObservableCollection<DO_DocumentosRechazados> Aux = new ObservableCollection<DO_DocumentosRechazados>();

            foreach (var item in ListaDocumentos)
            {
                item.IsSelected = true;
                Aux.Add(item);
            }
            ListaDocumentos.Clear();
            foreach (var item in Aux)
            {
                ListaDocumentos.Add(item);
            }
        }

        /// <summary>
        /// Método que deselecciona todos los archivos
        /// </summary>
        public void _DeseleccionarTodos()
        {
            ObservableCollection<DO_DocumentosRechazados> Aux = new ObservableCollection<DO_DocumentosRechazados>();

            foreach (var item in ListaDocumentos)
            {
                item.IsSelected = false;
                Aux.Add(item);
            }
            ListaDocumentos.Clear();
            foreach (var item in Aux)
            {
                ListaDocumentos.Add(item);
            }
        }

        /// <summary>
        /// Método que selecciona o deselecciona segun sea el caso
        /// </summary>
        public void _SelecDeselec()
        {
            if (IsSelected)
            {
                Titulo = "Deseleccionar Todos";
                _SeleccionarTodos();
                IsSelected = false;

            }
            else
            {
                Titulo = "Seleccionar Todos";
                _DeseleccionarTodos();
                IsSelected = true;
            }
        }
        #endregion

    }
}
