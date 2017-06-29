﻿using Model;
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

namespace View.Services.ViewModel
{
    class DocumentosPendientesViewM : INotifyPropertyChanged
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

        #region propiedades
        public Usuario usuario;

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


        private bool _gridUsuario;
        public bool GridUsuario
        {
            get
            {
                return _gridUsuario;
            }
            set
            {
                _gridUsuario = value;
                NotifyChange("GridUsuario");
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

        private Documento documento;
        private string Estatus;
        #endregion

        #region constructor

        public DocumentosPendientesViewM(Usuario user,string _estatus)
        {
            usuario = user;
            Estatus = _estatus;
            inicializa(Estatus);
        }
        #endregion

        #region commands
        public ICommand AbrirDocumento
        {
            get
            {
                return new RelayCommand(o => modificarDocumento());
            }
        }

        private void modificarDocumento()
        {
            //Si fue seleccionado un documento
            if (selectedDocumento != null)
            {
                //Se ejecuta el método que obtiene la información del documento seleccionado
                documento = DataManagerControlDocumentos.GetDocumento(SelectedDocumento.id_documento, SelectedDocumento.version.no_version);

                //Si el estatus es pendiente por corregir, se muestra ña ventana para modificar el documento
                if (Estatus.Contains("pendiente"))
                {
                    DocumentoViewModel viewM = new DocumentoViewModel(documento, false,usuario);
                    FrmDocumento frm = new FrmDocumento();

                    frm.DataContext = viewM;
                    frm.ShowDialog();

                    inicializa(Estatus);
                }
             // Si el estatus es aprobado pendiente por liberar, se muestra la pantalla para liberar el documento seleccionado
                else if (Estatus.Contains("aprobados"))
                {
                    DocumentoViewModel viewM = new DocumentoViewModel(documento);
                    FrmDocumento frm = new FrmDocumento();

                    frm.DataContext = viewM;
                    frm.ShowDialog();

                    inicializa(Estatus);
                }
            }
        }

        #endregion

        public void inicializa(string status)
        {
            //Si el estatus es pendiente por corregir
            if (status.Contains("pendiente")) {
                //Se ejecuta el método que obtiene los documentos pendientes por corregir de un usuario
                ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentos_PendientesCorregir(usuario.NombreUsuario);
            }//Si es estatus aprobado pendiente por liberar
            else if (status.Contains("aprobados"))
            {
                //Se ejecuta el método que obtiene los documentos pendientes por liberar
                ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentos_PendientesLiberar();
            }
        }

    }
}
