using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Microsoft.Win32;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections;
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

        /// <summary>
        /// Comando para abrir y cerrar flyout
        /// </summary>
        public ICommand AbrirCerrarFlyout
        {
            get
            {
                return new RelayCommand(a => abrirCerrarFlyout());
            }
        }

        /// <summary>
        /// Comando para envíar correo
        /// </summary>
        public ICommand EnviarCorreo
        {
            get
            {
                return new RelayCommand(a => enviarCorreo());
            }
        }

        /// <summary>
        /// Comando para agregar archivos adjuntos
        /// </summary>
        public ICommand AgregarArchivo
        {
            get
            {
                return new RelayCommand(x => agregarArchivo());
            }
        }

        /// <summary>
        /// Método para eliminar un item de listBox
        /// </summary>
        public ICommand EliminarItem
        {
            get
            {
                return new RelayCommand(o => eliminarItem(SelectedItem));
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

        private ObservableCollection<objUsuario> _ListaUsuarios;
        public ObservableCollection<objUsuario> ListaUsuarios
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

        public ObservableCollection<objUsuario> _ListaUsuarioANotificar;
        public ObservableCollection<objUsuario> ListaUsuarioANotificar
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
                    List<objUsuario> listaAux = ListaUsuarios.Where(x => x.IsSelected).ToList();

                    //Limpiamos la lista.
                    ListaUsuarioANotificar.Clear();

                    //Recorremos la lista temporal para asignar cada usuario a la lista de correos a notificar.
                    foreach (objUsuario usuario in listaAux)
                        ListaUsuarioANotificar.Add(usuario);

                    List<DO_Grupos> listaGrupoAux = ListaGrupos.Where(x => x.IsSelected).ToList();

                    // Recorremos la lista de grupos
                    foreach (DO_Grupos grupo in listaGrupoAux)
                    {
                        ObservableCollection<DO_INTEGRANTES_GRUPO> listaUsuariosGrupo = DataManagerControlDocumentos.GetAllIntegrantesGrupo(grupo.idgrupo);

                        // Recorremos la lista de usuarios por grupo
                        foreach (DO_INTEGRANTES_GRUPO integrate in listaUsuariosGrupo)
                        {
                            Usuario usuario = DataManager.GetUsuario(integrate.idusuariointegrante);
                            objUsuario user = new objUsuario();

                            user.Details = usuario.Details;
                            user.nombre = usuario.Nombre;
                            user.APaterno = usuario.ApellidoPaterno;
                            user.AMaterno = usuario.ApellidoMaterno;
                            user.Pathnsf = usuario.Pathnsf;
                            user.IsSelected = true;
                            user.usuario = usuario.IdUsuario;
                            user.Correo = usuario.Correo;

                            // Agregamos al usuarios a la lista para notificar
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

        #endregion

        #region Constructor

        public NotificarAViewModel(Usuario UsuarioLogeado, string body, ObservableCollection<Archivo> archivos, List<objUsuario> listaANotificar, string title)
        {
            Title = title;
            IsEnableEditor = true;
            User = UsuarioLogeado;
            ListaArchivos = archivos;


            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();
            
            //Si la listaANotificar es decir los usuarios a los que se van a mandar los correos tiene 1 registro
            if (listaANotificar.Count == 1)
            {
                //Se imprime el nombre de la primera posición
                BodyEmail = "<BR>" + definirSaludo() + listaANotificar[0].nombre + ";<BR><BR>" + body + "<br><br>" + definirPieDeCorreo();
            }
            else
            {
                //Sino se imprime "Buenos días a todos / todas" cuando se tiene mas de 1 
                BodyEmail = "<BR>" + definirSaludo() + " " + "a todos / a todas;" + "<BR><BR>" + body + "<br><br>" + definirPieDeCorreo();

            }
            ListaGrupos = DataManagerControlDocumentos.GetAllGrupos(User.NombreUsuario);
            ListaUsuarioANotificar = new ObservableCollection<objUsuario>();

            // Se carga a la lista el usuario reportó
            foreach (var item in listaANotificar)
                ListaUsuarioANotificar.Add(item);

            // Iteramos lista de Usarios a notificar y usuarios para seleccionar
            foreach (var listusernotify in ListaUsuarioANotificar)
            {
                foreach (var listuser in ListaUsuarios)
                {
                    // Seleccionamos en la lista de usuarios, los usuarios a notificar ya cargados
                    if (listusernotify.NombreCorto == listuser.NombreCorto)
                    {
                        listuser.IsSelected = true;
                    }
                }
            }            
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

            return d.Hour <= 11 ? "Buenos días " : "Buenas tardes ";
        }

        /// <summary>
        /// Se valida el correo antes de enviar
        /// </summary>
        /// <returns></returns>
        private async Task<bool> validar()
        {
            DialogService dialogService = new DialogService();

            // Si no se ha seleccionado a nadie
            if (ListaUsuarioANotificar.Count == 0)
            {
                IsEnableEditor = false;
                await dialogService.SendMessage(StringResources.ttlValideInformacion, StringResources.msgElijaDestinatario);
                IsEnableEditor = true;
                return false;
            }

            // Si el título del correo está vacío
            if (string.IsNullOrEmpty(Title))
            {
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;
                IsEnableEditor = false;

                //Ejecutamos el método para mostrar el mensaje. El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialogService.SendMessage(StringResources.ttlAlerta, StringResources.msgCorreoSinAsunto, setting, MessageDialogStyle.AffirmativeAndNegative);
                IsEnableEditor = true;

                return result == MessageDialogResult.Affirmative ? true : false;
            }
            return true;
        }

        /// <summary>
        /// Se define el pie del correo
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Método para agregar archivos adjuntos al correo
        /// </summary>
        private void agregarArchivo()
        {
            // Se declara servicio
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = true;

            bool? respuesta = fileDialog.ShowDialog();

            if (respuesta == true)
            {
                foreach (string archivo in fileDialog.FileNames)
                {
                    // Objeto
                    Archivo objArchivo = new Archivo();

                    // Se le asignan propiedades
                    objArchivo.ext = System.IO.Path.GetExtension(archivo);
                    objArchivo.nombre = System.IO.Path.GetFileName(archivo);
                    objArchivo.ruta = archivo;

                    //Si el archivo tiene extensión pdf
                    if (objArchivo.ext == ".pdf")
                    {
                        // Asigna la imagen del pdf al objeto
                        objArchivo.rutaIcono = @"/Images/p.png";
                    }
                    else
                    {
                        // Si el archivo tiene extensión xlsm o xlsx
                        if (objArchivo.ext == ".xlsm" || objArchivo.ext == ".xlsx")
                        {
                            // Asigna la imagen de excel al objeto
                            objArchivo.rutaIcono = @"/Images/E.jpg";
                        }
                        else
                        {
                            if (objArchivo.ext == ".doc" || objArchivo.ext == ".docx")
                            {
                                //Si es archivo de word asigna la imagen correspondiente.
                                objArchivo.rutaIcono = @"/Images/w.png";
                            }
                            else
                            {
                                // Si archivo de otro tipo se le asigna una imagen por default
                                objArchivo.rutaIcono = @"/Images/file.png";
                            }
                        }
                    }

                    // Se agrega el objeto a la lista
                    ListaArchivos.Add(objArchivo);
                }
            }
        }

        /// <summary>
        /// Método para eliminar archivo adjuntado
        /// </summary>
        /// <param name="item"></param>
        public void eliminarItem(Archivo item)
        {
            if (item != null)
            {
                //Se elimina el item seleccionado de la listaArchivos.
                ListaArchivos.Remove(item);
            }
        }

        /// <summary>
        /// Método para enviar el correo
        /// </summary>
        private async void enviarCorreo()
        {
            if (await validar())
            {
                // Inicializamos los servicios
                ServiceEmail SO_Email = new ServiceEmail();
                DialogService dialog = new DialogService();

                //Declaramos un objeto de tipo ProgressDialogController, el cual servirá para recibir el resultado el mensaje progress.
                ProgressDialogController AsyncProgress;

                // Se declara vector de tamaño elementos ListaUsuarioANotificar + 1
                int l = ListaUsuarioANotificar.Count;
                string[] usuarios = new string[l + 1];
                int c = 0;

                // Se itera la lista y se agregan a la lista a notificar
                foreach (objUsuario usuario in ListaUsuarioANotificar)
                {
                    usuarios[c] = usuario.Correo;
                    c++;
                }

                // Se agrega al vector el usuario logueado para ser notificado                
                usuarios[c] = User.Correo;

                // Vector con archivos adjuntados
                string[] archivos = new string[ListaArchivos.Count];
                int i = 0;

                foreach (var item in ListaArchivos)
                {
                    archivos[i] = item.ruta;
                    i++;
                }
                IsEnableEditor = false;

                //Ejecutamos el método para enviar un mensaje de espera mientras se lee el archivo.
                AsyncProgress = await dialog.SendProgressAsync(StringResources.msgEnviandoCorreo, "");

                DO_PathMail respuesta = await SO_Email.SendEmailWithAttachment(User.Pathnsf, usuarios, Title, BodyEmail, archivos);

                //Ejecutamos el método para cerrar el mensaje de espera.
                await AsyncProgress.CloseAsync();

                IsEnableEditor = true;

                DialogService dialogService = new DialogService();

                // Se oculta editor de texto
                IsEnableEditor = false;

                if (respuesta.respuesta)
                {
                    await dialogService.SendMessage(StringResources.ttlAlerta, respuesta.rutamail);
                }
                else
                {
                    if (respuesta.rutamail == StringResources.msgDeseasConfigCorreo)
                    {
                        //Configuramos las opciones del mesaje de pregunta.
                        MetroDialogSettings settings = new MetroDialogSettings();
                        settings.AffirmativeButtonText = StringResources.lblYes;
                        settings.NegativeButtonText = StringResources.lblNo;

                        //Preguntamos al usuario si lo quiere configurar en estos momentos.
                        MessageDialogResult resultMSG = await dialog.SendMessage(StringResources.ttlAtencion, StringResources.msgConfiguracionCorreo, settings, MessageDialogStyle.AffirmativeAndNegative, "Servicio de Correo");

                        //Verificamos la respuesta del usuario, si es afirmativa iniciamos el proceso de configuración.
                        if (resultMSG == MessageDialogResult.Affirmative)
                        {
                            settings = new MetroDialogSettings();
                            settings.AffirmativeButtonText = StringResources.ttlOkEntiendo;

                            await dialog.SendMessage(User.Nombre + StringResources.msgParaTuInf, StringResources.msgProcesoConfiguracion);

                            ProgressDialogController AsyncProgressConfigEmail;

                            AsyncProgressConfigEmail = await dialog.SendProgressAsync(StringResources.ttlEspereUnMomento + User.Nombre + "...", StringResources.msgEstamosConfigurando);

                            ConfigEmailViewModel configEmail = new ConfigEmailViewModel(User);

                            // Se reciben valores de las 2 propiedades del objeto
                            DO_PathMail respuestaConfigEmail = await configEmail.setEmail();

                            await AsyncProgressConfigEmail.CloseAsync();

                            if (respuestaConfigEmail.respuesta)
                            {
                                // Actualizamos el path de usuario en la misma sesión
                                User.Pathnsf = respuestaConfigEmail.rutamail;

                                // M
                                await dialog.SendMessage(StringResources.msgPerfecto + User.Nombre, StringResources.msgCuentaConfigurada);

                                //enviarCorreo();
                                l = ListaUsuarioANotificar.Count;
                                usuarios = new string[l + 1];
                                c = 0;

                                // Se itera la lista y se agregan a la lista a notificar
                                foreach (objUsuario usuario in ListaUsuarioANotificar)
                                {
                                    usuarios[c] = usuario.Correo;
                                    c++;
                                }

                                // Se agrega al vector el usuario logueado para ser notificado                
                                usuarios[c] = User.Correo;

                                // Vector con archivos adjuntados
                                archivos = new string[ListaArchivos.Count];
                                i = 0;

                                foreach (var item in ListaArchivos)
                                {
                                    archivos[i] = item.ruta;
                                    i++;
                                }
                                IsEnableEditor = false;

                                //Ejecutamos el método para enviar un mensaje de espera mientras se lee el archivo.
                                AsyncProgress = await dialog.SendProgressAsync(StringResources.msgEnviandoCorreo, "");

                                respuesta = await SO_Email.SendEmailWithAttachment(User.Pathnsf, usuarios, Title, BodyEmail, archivos);

                                //Ejecutamos el método para cerrar el mensaje de espera.
                                await AsyncProgress.CloseAsync();

                                IsEnableEditor = true;

                                dialogService = new DialogService();

                                // Se oculta editor de texto
                                IsEnableEditor = false;

                                if (respuesta.respuesta)
                                {
                                    await dialogService.SendMessage(StringResources.ttlAlerta, respuesta.rutamail);
                                }
                            }
                            else
                            {
                                await dialog.SendMessage(StringResources.ttlOcurrioError, StringResources.msgErrorVincular);
                            }
                        }
                    }
                }

                // Se muestra editor de texto
                IsEnableEditor = true;

                //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                var window = System.Windows.Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                //Verificamos que la pantalla sea diferente de nulo.
                if (window != null)
                {
                    //Cerramos la pantalla
                    window.Close();
                }
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

                    //Ocultamos el editor
                    IsEnableEditor = false;

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

                    // Hacemos visible nuevamente el editor
                    IsEnableEditor = true;
                }
            }
        }

        #endregion
    }
}
