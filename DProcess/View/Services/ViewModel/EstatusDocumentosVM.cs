using MahApps.Metro.Controls.Dialogs;
using Model;
using Model.ControlDocumentos;
using System;
using System.Windows;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using View.Resources;
using MahApps.Metro.Controls;

namespace View.Services.ViewModel
{
    public class EstatusDocumentosVM : INotifyPropertyChanged
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

        private ObservableCollection<Documento> _ListaDocumentosConEstatus;
        public ObservableCollection<Documento> ListaDocumentosConEstatus
        {
            get
            {
                return _ListaDocumentosConEstatus;
            }
            set
            {
                _ListaDocumentosConEstatus = value;
                NotifyChange("ListaDocumentosConEstatus");
                
            }
        }

        private ObservableCollection<Documento> _ListaDocumentosSeleccionados;
        public ObservableCollection<Documento> ListaDocumentosSeleccionados
        {
            get
            {
                return _ListaDocumentosSeleccionados;
            }
            set
            {
                _ListaDocumentosSeleccionados = value;
                NotifyChange("ListaDocumentosSeleccionados");
            }
        }

        #endregion

        #region Constructor
        public EstatusDocumentosVM(Usuario _usuario)
        {
            usuario = _usuario;
            ListaDocumentosConEstatus = DataManagerControlDocumentos.GetDocumentosEstatus(usuario.NombreUsuario,"");
            ListaDocumentosSeleccionados = new ObservableCollection<Documento>();

            foreach (Documento item in ListaDocumentosConEstatus)
            {
                switch (item.version.id_estatus_version)
                {
                    case 4:
                        item.ruta = @"\Images\TLROJO.png";
                        break;
                    case 3:
                        item.ruta = @"\Images\TLAMARILLO.png";
                        break;
                    case 5:
                        item.ruta = @"\Images\TLVERDE.png";
                        break;
                }
            }
        }

        #endregion

        #region Comandos

        /// <summary>
        /// Comando para rechazar los documentos
        /// </summary>
        public ICommand RechazarDocumentos
        {
            get
            {
                return new RelayCommand(a => _RechazarDocumentos());
            }
        }

        public ICommand BuscarEstatusDocumento
        {
            get
            {
                return new RelayCommand(a => _BuscarEstatusDocumento((string)a));
            }
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Método que rechaza los documentos
        /// </summary>
        public async void _RechazarDocumentos()
        {
            if (ListaDocumentosConEstatus.Count > 0)
            {
                int v = 0;
                //Incializamos los servicios de dialog.
                DialogService dialog = new DialogService();

                //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
                MetroDialogSettings setting = new MetroDialogSettings();
                setting.AffirmativeButtonText = StringResources.lblYes;
                setting.NegativeButtonText = StringResources.lblNo;

                MessageDialogResult result = await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblRegresarPendienteCorregir + "\n" + "Total de documentos seleccionados : " + ListaDocumentosConEstatus.Where(x=> x.IsSelected == true).ToList().Count() , setting, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    foreach (Documento doc in ListaDocumentosConEstatus)
                    {
                        if (doc.IsSelected == true && doc.version.id_estatus_version != 4)
                        {
                            //Obtenemos el ID de la version.
                            int idVersion = DataManagerControlDocumentos.GetIdVersion(doc.nombre, doc.version.no_version);

                            ////Rechazamos el documento
                            v = DataManagerControlDocumentos.SetRechazarVersion(idVersion);

                            //Registramos el cambio en la bitácora.
                            DataManagerControlDocumentos.InsertHistorialVersion(idVersion, usuario.Nombre + " " + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno, doc.nombre, doc.version.no_version, "Se cambia el estatus a: PENDIENTE POR CORREGIR");

                        }
                    }
                    int y = ListaDocumentosConEstatus.Where(x => x.version.id_estatus_version == 4 && x.IsSelected).Count();
                    if (y == ListaDocumentosConEstatus.Where(x => x.IsSelected).Count())
                    {
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.lblDocumentosPendientesCorregir);
                    }
                    else
                    {
                        if (v != 0)
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgEstatusPendienteCorregir);

                            //Obtenemos la pantalla actual, y casteamos para que se tome como tipo MetroWindow.
                            var window = Application.Current.Windows.OfType<MetroWindow>().LastOrDefault();

                            //Verificamos que la pantalla sea diferente de nulo.
                            if (window != null)
                            {
                                //Cerramos la pantalla
                                window.Close();
                            }
                        }
                        else
                        {
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgError);
                        }
                    }                  
                }
            }
        }

        /// <summary>
        /// Método para filtrar los documentos
        /// </summary>
        public void _BuscarEstatusDocumento(string Texto)
        {

            foreach (var item in ListaDocumentosConEstatus)
            {
                if (item.IsSelected)
                {
                    if (ListaDocumentosSeleccionados.Where(x=> x.id_documento == item.id_documento).ToList().Count == 0)
                    {
                        ListaDocumentosSeleccionados.Add(item);
                    }
                }
            }

            foreach (var item in ListaDocumentosConEstatus)
            {
                if (!item.IsSelected)
                {
                    if (ListaDocumentosSeleccionados.Where(x => x.id_documento == item.id_documento).ToList().Count > 0)
                    {
                        Documento ct = ListaDocumentosSeleccionados.Where(x => x.id_documento == item.id_documento).FirstOrDefault();
                        ListaDocumentosSeleccionados.Remove(ct);
                    }
                }
            }

            ListaDocumentosConEstatus = DataManagerControlDocumentos.GetDocumentosEstatus(usuario.NombreUsuario,Texto);

            foreach (var item in ListaDocumentosConEstatus)
            {
                if (ListaDocumentosSeleccionados.Where(x => x.id_documento == item.id_documento).ToList().Count > 0)
                {
                    item.IsSelected = true;
                }
            }

            foreach (Documento item in ListaDocumentosConEstatus)
            {
                switch (item.version.id_estatus_version)
                {
                    case 4:
                        item.ruta = @"\Images\TLROJO.png";
                        break;
                    case 3:
                        item.ruta = @"\Images\TLAMARILLO.png";
                        break;
                    case 5:
                        item.ruta = @"\Images\TLVERDE.png";
                        break;
                }
            }
        }

        #endregion

    }
}
