using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.ViewModel
{
    public class ComparativoDocumentosViewModel : INotifyPropertyChanged
    {
        #region Properties

        #region PropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        public ObservableCollection<string> ListaErrores = new ObservableCollection<string>();
        public ObservableCollection<string> ListaOks = new ObservableCollection<string>();

        #endregion

        #region Methods

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion 

        public void CompararArchivos()
        {
            //ObservableCollection<Documento> procedimientosEspecificos = GetAllDocumentoByTipoDocumento(1005);
            //ObservableCollection<Documento> formatosEspecificos = GetAllDocumentoByTipoDocumento(1012);
            //ObservableCollection<Documento> procedimientosISO = GetAllDocumentoByTipoDocumento(1006);
            //ObservableCollection<Documento> formatosISO = GetAllDocumentoByTipoDocumento(1014);
            //ObservableCollection<Documento> procedimientosOHSAS = GetAllDocumentoByTipoDocumento(1003);
            //ObservableCollection<Documento> formatosOHSAS = GetAllDocumentoByTipoDocumento(1013);
            //ObservableCollection<Documento> formatosMIE = GetAllDocumentoByTipoDocumento(1011);
            
            ObservableCollection<Documento> documentosMatriz = GetAllDocumentoByTipoDocumento(1011);
            ObservableCollection<Documento> documentosFrames = DataManagerControlDocumentos.GetAllDocumentosFrames();

            foreach (Documento documento in documentosMatriz)
            {
                documento.nombre = documento.nombre.ToUpper();
            }
            foreach (Documento documento in documentosFrames)
            {
                documento.nombre = documento.nombre.ToUpper();
            }

            if (documentosFrames.Count != documentosMatriz.Count)
            {
                ListaErrores.Add("Existe una direfencia en numero de documentos\n En matriz hay :" + documentosMatriz.Count + "\n En frames hay: " + documentosFrames.Count);

                if (documentosFrames.Count > documentosMatriz.Count)
                {
                    foreach (Documento documento in documentosFrames)
                    {
                        if (documentosMatriz.Where(x => x.nombre == documento.nombre).ToList().Count == 0)
                        {
                            ListaErrores.Add("El documento " + documento.nombre + " no está en la matriz");
                        }
                    }
                }
                else
                {
                    foreach (Documento documento in documentosMatriz)
                    {
                        if (documentosFrames.Where(x =>x.nombre == documento.nombre).ToList().Count == 0)
                        {
                            ListaErrores.Add("El documento " + documento.nombre + " no está en frames");
                        }
                    }
                }
            }
                
            else
                ListaOks.Add("El número de documentos coincide");


            foreach (Documento documentoFrames in documentosFrames)
            {

                Documento documentoMatriz = documentosMatriz.Where(x => x.nombre == documentoFrames.nombre).FirstOrDefault();

                if (documentosMatriz.Where(x => x.nombre == documentoFrames.nombre).ToList().Count == 0)
                {
                    ListaErrores.Add("El documento " + documentoFrames.nombre + " no esta en la matriz");
                }

                if (documentoMatriz != null)
                {
                    if (!string.IsNullOrEmpty(documentoMatriz.nombre))
                    {
                        if (documentoMatriz.version.no_version != documentoFrames.version.no_version)
                        {
                            ListaErrores.Add("El documento " + documentoMatriz.nombre + " tiene una versión distinta.");
                        }

                        if (documentoMatriz.fecha_actualizacion != documentoFrames.version.fecha_version)
                        {
                            ListaErrores.Add("El documento " + documentoMatriz.nombre + " tiene una fecha distinta.");
                        }
                    } 
                }
            }
        }

        private ObservableCollection<Documento> GetAllDocumentoByTipoDocumento(int idTipoDocumento)
        {
            return DataManagerControlDocumentos.GetDocumentoByTipoDocumento(idTipoDocumento, "");
        }

        #endregion
    }
}
