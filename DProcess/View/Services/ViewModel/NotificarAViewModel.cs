using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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

        #endregion

        #region Constructor

        public NotificarAViewModel(Usuario ModelUsuario)
        {
            User = ModelUsuario;
            ListaUsuarios = DataManagerControlDocumentos.GetUsuarios();
            ListaGrupos = DataManagerControlDocumentos.GetAllGrupos(User.NombreUsuario);
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método para abrir ventana cuando se quiere crear nuevo grupo
        /// </summary>
        public void ircreargrupo()
        {
            FrmCrearGrupo wcreargrupo = new FrmCrearGrupo();
            GruposViewModel vw = new GruposViewModel(User);

            wcreargrupo.DataContext = vw;
            wcreargrupo.ShowDialog();

            // Cargamos de nuevo la lista de grupos, para que se actualice al momento de crear nuevo grupo
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
            foreach (var grupo in ListaGrupos)
            {
                if (grupo.IsSelected)
                {
                    DialogService dialog = new DialogService();
                    MetroDialogSettings settings = new MetroDialogSettings();

                    settings.AffirmativeButtonText = StringResources.lblYes;
                    settings.NegativeButtonText = StringResources.lblNo;

                    MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEliminarRegistro, settings, MessageDialogStyle.AffirmativeAndNegative);

                    // Se asegura que el grupo es existente
                    if (grupo.idgrupo != 0)
                    {
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
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                    }
                }
            }
        }
        #endregion

    }
}
