using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Forms.ControlDocumentos;
using View.Resources;

namespace View.Services.ViewModel
{
    public class NotificarAViewModel : INotifyPropertyChanged
    {
        public Usuario User;

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

        #region Commands

        /// <summary>
        /// Comando para ir a la ventana de crear grupos
        /// </summary>
        public ICommand IrCrearGrupo
        {
            get
            {
                return new RelayCommand(a => ircreargrupo());
            }
        }

        /// <summary>
        /// Comando para abrir grupo y ver usuarios integrantes
        /// </summary>
        public ICommand AbrirGrupo
        {
            get
            {
                return new RelayCommand(a => abrirgrupo());
            }
        }

        /// <summary>
        /// Comando para eliminar grupo
        /// </summary>
        public ICommand EliminarGrupo
        {
            get
            {
                return new RelayCommand(a => eliminargrupo());
            }
        }

        public ICommand AbrirCerrarFlyout
        {
            get
            {
                return new RelayCommand(a => abrirCerrarFlyout());
            }
        }

        public ICommand EnviarCorreo
        {
            get
            {
                return new RelayCommand(a => enviarCorreo());
            }
        }

        public ICommand AgregarArchivo
        {
            get
            {
                return new RelayCommand(x => agregarArchivo());
            }
        }

        #endregion

        #region Porpiedades

        private ObservableCollection<DO_Grupos> _ListaGrupos;
        public ObservableCollection<DO_Grupos> ListaGrupos
        {
            get
            {
                return _ListaGrupos;
            }
            set
            {
                _ListaGrupos = value;
                NotifyChange("ListaGrupos");
            }
        }

        private DO_Grupos _GrupoSeleccionado;
        public DO_Grupos GrupoSeleccionado
        {
            get
            {
                return _GrupoSeleccionado;
            }
            set
            {
                _GrupoSeleccionado = value;
                NotifyChange("GrupoSeleccionado");
            }
        }

        private ObservableCollection<Usuarios> _ListaUsuarios;
        public ObservableCollection<Usuarios> ListaUsuarios
        {
            get
            {
                return _ListaUsuarios;
            }
            set
            {
                _ListaUsuarios = value;
                NotifyChange("ListaUsuarios");
            }
        }

        public ObservableCollection<Usuarios> _ListaUsuarioANotificar;
        public ObservableCollection<Usuarios> ListaUsuarioANotificar
        {
            get
            {
                return _ListaUsuarioANotificar;
            }
            set
            {
                _ListaUsuarioANotificar = value;
                NotifyChange("ListaUsuarioANotificar");
            }
        }

        private string _BodyEmail;
        public string BodyEmail
        {
            get { return _BodyEmail; }
            set { _BodyEmail = value; NotifyChange("BodyEmail"); }
        }

        private bool _IsOpen;
        public bool IsOpen
        {
            get { return _IsOpen; }
            set
            {
                _IsOpen = value;
                NotifyChange("IsOpen");
                if (!_IsOpen)
                {
                    //Obtenemos los usuarios seleccionados y los asignamos a una lista temporal.
                    List<Usuarios> listaAux = ListaUsuarios.Where(x => x.IsSelected).ToList();

                    //Limpiamos la lista.
                    ListaUsuarioANotificar.Clear();

                    //Recorremos la lista temporal para asignar cada usuario a la lista de correos a notificar.
                    foreach (Usuarios usuario in listaAux)
                        ListaUsuarioANotificar.Add(usuario);

                    List<DO_Grupos> listaGrupoAux = ListaGrupos.Where(x => x.IsSelected).ToList();

                    foreach (DO_Grupos grupo in listaGrupoAux)
                    {

                        ObservableCollection<DO_INTEGRANTES_GRUPO> listaUsuariosGrupo = DataManagerControlDocumentos.GetAllIntegrantesGrupo(grupo.idgrupo);
                        foreach (DO_INTEGRANTES_GRUPO integrate in listaUsuariosGrupo)
                        {
                            Usuario usuario = DataManager.GetUsuario(integrate.idusuariointegrante);
                            Usuarios user = new Usuarios();

                            user.Details = usuario.Details;
                            user.nombre = usuario.Nombre;
                            user.APaterno = usuario.ApellidoPaterno;
                            user.AMaterno = usuario.ApellidoMaterno;
                            user.Pathnsf = usuario.Pathnsf;
                            user.IsSelected = true;
                            user.usuario = usuario.IdUsuario;

                            ListaUsuarioANotificar.Add(user);
                        }

                    }


                }
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { _title = value; NotifyChange("Title"); }
        }

        private ObservableCollection<Archivo> _listaArchivos;
        public ObservableCollection<Archivo> ListaArchivos
        {
            get { return _listaArchivos; }
            set { _listaArchivos = value; NotifyChange("ListaArchivos"); }
        }

        private bool _IsEnableEditor;

        public bool IsEnableEditor
        {
            get { return _IsEnableEditor; }
            set { _IsEnableEditor = value; NotifyChange("IsEnableEditor"); }
        }


        #endregion

        #region Constructor

        public NotificarAViewModel(Usuario ModelUsuario, string body, ObservableCollection<Archivo> archivos, List<Usuarios> listaANotificar, string title)
        {
            Title = title;
            IsEnableEditor = true;
            User = ModelUsuario;
            ListaArchivos = archivos;
            BodyEmail = "<BR>" + definirSaludo() + "<BR><BR>" + body + "<br><br>" + definirPieDeCorreo();
            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();
            ListaGrupos = DataManagerControlDocumentos.GetAllGrupos(User.NombreUsuario);
            ListaUsuarioANotificar = new ObservableCollection<Usuarios>();

            foreach (var item in listaANotificar)
                ListaUsuarioANotificar.Add(item);

            

            #region Prueba Correo
            //string body = "<HTML>";
            //body += "<head>";
            //body += "<meta http-equiv=\"Content - Type\" content=\"text / html; charset = utf - 8\"/>";
            //body += "</head>";
            //body += "<body text=\"white\">";
            //body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">" + "Buenas tardes" + "</font> </p>";
            //body += "<ul>";
            //body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Para notificar que " + "Formato especifico" + " con el número <b> " + "W-3571-454AS54AS" + "</b> versión <b> " + "1" + ".0" + " </b> ya se encuentra disponible en el sistema </font> <a href=\"http://sealed/frames.htm\">frames</a> </li>";
            //body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Adicionalmente informo que se actualizo la matríz.</font></li>";
            //body += "<br/>";
            //body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Número : <b>" + "4-3571-ASDASDASD" + "</b></font></li>";
            //body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Descripción : <b>" + "DESCRIPCIÓN" + "</b></font></li>";
            //body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Versión : <b>" + "1" + ".0" + "</b></font></li>";
            ////body += "<li><font font=\"verdana\" size=\"3\" color=\"black\">Área del Frames en donde se inserto : <b>" + AreaFrames + "</b></font></li>";
            //body += "</ul>";
            //body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Cualquier duda quedo a sus órdenes</font> </p>";
            //body += "<br/>";
            //body += "<p><font font=\"verdana\" size=\"3\" color=\"black\">Este correo se ha generado automáticamente, por favor no responda.</font> </p>";
            //body += "<br/>";
            //body += "<p><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Saludos / Kind regards</font> </p>";
            //body += "<ul>";
            //body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + "Edgar Raúl " + " " + "Bañuelos" + "</font> </li>";
            //body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">MAHLE Componentes de Motor de México, S. de R.L. de C.V.</font></li>";
            //body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Engineering (ENG)</font> </li>";
            //body += "<li></li>";
            //body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico</font> </li>";
            //body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">Teléfono: +52 449 910 8200-82 90, Fax: +52 449 910 8200 - 267</font> </li>";
            //body += "<li><font font=\"default Sans Serif\" size=\"3\" color=\"black\">" + "raul.banuelos@mx.mahle.com" + ",</font> <a href=\"http://www.mx.mahle.com\">http://www.mx.mahle.com</a>  </li>";
            //body += "</ul>";
            //body += "</body>";
            //body += "</HTML>";
            //BodyEmail = body; 
            #endregion
        }

        #endregion

        #region Métodos

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

        private async Task<bool> validar()
        {
            DialogService dialogService = new DialogService();
            if (ListaUsuarioANotificar.Count == 0)
            {
                IsEnableEditor = false;
                await dialogService.SendMessage("Por favor valide la información", "Por favor elija al menos un destinatario");
                IsEnableEditor = true;
                return false;
            }

            if (string.IsNullOrEmpty(Title))
            {
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;
                IsEnableEditor = false;
                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialogService.SendMessage(StringResources.ttlAlerta, "Su correo no contiene asunto, desea enviarlo sin asunto.", setting, MessageDialogStyle.AffirmativeAndNegative);
                IsEnableEditor = true;
                return result == MessageDialogResult.Affirmative ? true : false;
            }
            return true;

        }

        private string definirPieDeCorreo()
        {
            string pie = "<FONT size=2 face=Helv>";
            pie += "<FONT size=2 face=Helv>";
            pie += "<P>Saludos / Kind regards<BR>";
            pie += "<BR>" + User.Nombre + " " + User.ApellidoPaterno + " " + User.ApellidoMaterno;
            pie += "<BR>MAHLE Componentes de Motor de México, S. de R.L. de C.V.";
            pie += "<BR>  <BR>";
            pie += "<BR>Km. 0.3 Carr. Maravillas-Jesús María , 20900 Aguascalientes, Mexico";
            pie += "<BR>Teléfono: +52 449 910 82 00, Fax: +52 449 910 8200 ext. 26";
            pie += "<BR>" + User.Correo + ", http://www.mx.mahle.com</P>";
            pie += "</FONT>";
            pie += "</FONT>";

            return pie;
        }

        private void agregarArchivo()
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;

            bool? respuesta = fileDialog.ShowDialog();

            if (respuesta == true)
            {
                foreach (string archivo in fileDialog.FileNames)
                {
                    //ListaArchivos.Add(archivo);
                    Archivo objArchivo = new Archivo();
                    objArchivo.ext = System.IO.Path.GetExtension(archivo);
                    objArchivo.nombre = System.IO.Path.GetFileName(archivo);
                    objArchivo.ruta = archivo;

                    ListaArchivos.Add(objArchivo);
                }
            }
        }

        private async void enviarCorreo()
        {
            if (await validar())
            {
                ServiceEmail SO_Email = new ServiceEmail();

                int l = ListaUsuarioANotificar.Count;
                string[] usuarios = new string[l];
                int c = 0;
                foreach (Usuarios usuario in ListaUsuarioANotificar)
                {
                    usuarios[c] = usuario.Correo;
                    c++;
                }

                string[] archivos = new string[ListaArchivos.Count];
                int i = 0;

                foreach (var item in ListaArchivos)
                {
                    archivos[i] = item.ruta;
                    i++;
                }

                bool respuesta = SO_Email.SendEmailWithAttachment(User.Pathnsf, usuarios, Title, BodyEmail, archivos);
                DialogService dialogService = new DialogService();
                IsEnableEditor = false;
                if (respuesta)
                {
                    await dialogService.SendMessage("Aviso", "Correo enviado exitosamente");
                }
                else
                {
                    await dialogService.SendMessage("Aviso", "Ocurrio un error al enviar el correo");
                }
                IsEnableEditor = true;
            }
        }

        /// <summary>
        /// Método para abrir o cerrar el Flyout
        /// </summary>
        private void abrirCerrarFlyout()
        {
            IsOpen = !IsOpen;
        }

        /// <summary>
        /// Método para abrir ventana cuando se quiere crear nuevo grupo
        /// </summary>
        public void ircreargrupo()
        {
            FrmCrearGrupo wcreargrupo = new FrmCrearGrupo();
            GruposViewModel vw = new GruposViewModel(User);

            wcreargrupo.DataContext = vw;
            wcreargrupo.ShowDialog();

            // Cargamos de nuevo la lista de grupos, para que se actualice al momento de crear nuevo o eliminar grupo
            ListaGrupos = DataManagerControlDocumentos.GetAllGrupos(User.NombreUsuario);
            ListaGrupos = ListaGrupos;
            NotifyChange("ListaGrupos");
        }

        /// <summary>
        /// Método para abrir o ver registros de un grupo.
        /// </summary>
        public void abrirgrupo()
        {
            if (GrupoSeleccionado.idgrupo != 0)
            {
                FrmVerIntegrantesGrupo Form = new FrmVerIntegrantesGrupo();
                GruposViewModel Data = new GruposViewModel(GrupoSeleccionado.idgrupo, User);

                Form.DataContext = Data;
                Form.ShowDialog();
            }
        }

        /// <summary>
        /// Método para eliminar un grupo.
        /// </summary>
        public async void eliminargrupo()
        {
            // Recorremos lista grupos
            foreach (var grupo in ListaGrupos)
            {
                // Si el grupo está seleccionado...
                if (grupo.IsSelected)
                {
                    // Inicializamos servicios
                    DialogService dialog = new DialogService();
                    MetroDialogSettings settings = new MetroDialogSettings();

                    settings.AffirmativeButtonText = StringResources.lblYes;
                    settings.NegativeButtonText = StringResources.lblNo;

                    // Leemos la respuesta
                    MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEliminarRegistro, settings, MessageDialogStyle.AffirmativeAndNegative);

                    // Se asegura que el grupo es existente
                    if (grupo.idgrupo != 0)
                    {
                        // Si la respuesta fue si
                        if (MessageDialogResult.Affirmative == result)
                        {
                            // Generamos lista con integrantes del grupo a eliminar
                            ObservableCollection<DO_INTEGRANTES_GRUPO> ListaIntegrantes = DataManagerControlDocumentos.GetAllIntegrantesGrupo(grupo.idgrupo);

                            // Recorremos y eliminamos integrantes del grupo
                            foreach (var usuariointegrante in ListaIntegrantes)
                            {
                                DataManagerControlDocumentos.eliminarintegrantes(grupo.idgrupo, usuariointegrante.idusuariointegrante);
                            }

                            // Eliminamos grupo ya vacío
                            DataManagerControlDocumentos.eliminarGrupos(grupo.idgrupo);

                            // Cargamos lista de grupos para que se actualice al momento de eliminar alguno
                            ListaGrupos = DataManagerControlDocumentos.GetAllGrupos(User.NombreUsuario);

                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.ttlDone);

                            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                            var window = System.Windows.Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();
                        }
                    }
                    else
                    {
                        //Si hay error mandamos mensaje
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                    }
                }
            }
        }

        #endregion

    }
}
