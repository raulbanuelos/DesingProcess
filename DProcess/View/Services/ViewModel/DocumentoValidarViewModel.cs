using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.ViewModel
{
    public class DocumentoValidarViewModel
    {
        public ObservableCollection<Documento> ListaDocumentosValidar { get; set; }

        public Usuario usuario;

        public DocumentoValidarViewModel(Usuario _usuario)
        {
            usuario = _usuario;
            ListaDocumentosValidar = DataManagerControlDocumentos.GetDocumentosValidar(usuario.NombreUsuario);

        }

    }
}
