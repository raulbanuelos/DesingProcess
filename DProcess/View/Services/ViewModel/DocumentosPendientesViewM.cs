﻿using Model;
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
using View.Forms.ControlDocumentos;
using View.Resources;

namespace View.Services.ViewModel
{
    public class DocumentosPendientesViewM : INotifyPropertyChanged
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

        private string _titulo;
        public string Titulo {
            get
            {
                return _titulo;
            }
            set
            {
                _titulo = value;
                NotifyChange("Titulo");
            }
        }
        #endregion

        #region Constructor
        public DocumentosPendientesViewM(Usuario user,string _estatus)
        {
            usuario = user;
            Estatus = _estatus;
            inicializa(Estatus);
        }
        #endregion

        #region Commands

        /// <summary>
        /// Comando para moestrar el documento seleccionado
        /// </summary>
        public ICommand AbrirDocumento
        {
            get
            {
                return new RelayCommand(o => modificarDocumento());
            }
        }

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

        #region Métodos
        /// <summary>
        /// método que obtiene el documento
        /// segun sea el estatus del documento se ejecuta el metodo correspondiente
        /// </summary>
        /// <param name="txt_buscar"></param>
        /// 
        private void GetDocument(string txt_buscar)
        {
            if (Estatus.Contains("pendiente"))
            {
                ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentos_PendientesCorregir(usuario.NombreUsuario, txt_buscar);
            }
            else
            {
                if (Estatus.Contains("todosPendientes"))
                {
                    ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentos_PendientesCorregir(txt_buscar);
                }
                else
                {
                    ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentos_PendientesLiberar(txt_buscar);
                }
                
            }                     
        }

        /// <summary>
        /// Método que abre la ventana donde se mostrara la información del documento y la versión
        /// Dependiendo del estatus la ventana se muestra con diferentes características
        /// </summary>
        private void modificarDocumento()
        {
            //Si fue seleccionado un documento
            if (selectedDocumento != null)
            {
                //Se ejecuta el método que obtiene la información del documento seleccionado
                documento = DataManagerControlDocumentos.GetDocumento(SelectedDocumento.id_documento, SelectedDocumento.version.no_version);

                //Si el estatus es pendiente por corregir, se muestra la ventana para modificar el documento
                if (Estatus.Contains("pendiente") || Estatus.Contains("todosPendientes"))
                {
                    DocumentoViewModel viewM = new DocumentoViewModel(documento, false,usuario);
                    FrmDocumento frm = new FrmDocumento();

                    frm.DataContext = viewM;
                    frm.ShowDialog();

                    inicializa(Estatus);
                }
                //Si el estatus es aprobado pendiente por liberar, se muestra la pantalla para liberar el documento seleccionado
                else if (Estatus.Contains("aprobados"))
                {
                    DocumentoViewModel viewM = new DocumentoViewModel(documento,usuario);
                    FrmDocumento frm = new FrmDocumento();

                    frm.DataContext = viewM;
                    frm.ShowDialog();

                    inicializa(Estatus);
                }
            }
        }

        /// <summary>
        /// Método que inicializa la lista de documentos que se mostaran en la tabla
        /// Dependiendo del estatus se ejecutarán diferentes métodos
        /// </summary>
        /// <param name="status"></param>
        public void inicializa(string status)
        {
            //Si el estatus es pendiente por corregir
            if (status.Contains("pendiente"))
            {
                //Se ejecuta el método que obtiene los documentos pendientes por corregir de un usuario
                ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentos_PendientesCorregir(usuario.NombreUsuario, "");

                _titulo = StringResources.msgDocumentosCorregir;

            }
            //Si es estatus aprobado pendiente por liberar
            else if (status.Contains("aprobados"))
            {
                //Se ejecuta el método que obtiene los documentos pendientes por liberar
                ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentos_PendientesLiberar("");
                _titulo = StringResources.msgDocumentosLiberar;
            }
            //Si es estatus todosPendientes, se muestran todos los documentos que están en pendientes por corregir(Solo para el perfil de administrador)
            else if (status.Contains("todosPendientes"))
            {
                //Se ejecuta el método que obtiene los documentos pendientes por corregir de un usuario
                ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentos_PendientesCorregir(string.Empty);

                _titulo = StringResources.msgDocumentosCorregir;
            }
        }
        #endregion
    }
}