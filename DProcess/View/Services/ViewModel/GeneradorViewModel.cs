﻿using MahApps.Metro.Controls;
using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using View.Forms.ControlDocumentos;
using View.Resources;

namespace View.Services.ViewModel
{
    public class GeneradorViewModel : INotifyPropertyChanged
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
        public Usuario ModelUsuario;
       
        public string NombreUsuario
        {
            get
            {
                return ModelUsuario.NombreUsuario;
            }
            set
            {
                ModelUsuario.NombreUsuario = value;
                NotifyChange("NombreUsuario");
            }
        }
        public string Nombre
        {
            get
            {
                return ModelUsuario.Nombre;
            }
            set
            {
                ModelUsuario.Nombre = value;
                NotifyChange("Nombre");
            }
        }

        private ObservableCollection<TipoDocumento> _ListaTipoDocumento = DataManagerControlDocumentos.GetTipo();
        public ObservableCollection<TipoDocumento> ListaTipoDocumento
        {
            get
            {
                return _ListaTipoDocumento;
            }
            set
            {
                _ListaTipoDocumento = value;
                NotifyChange("ListaTipoDocumento");
            }
        }

        private ObservableCollection<Departamento> _ListaDepartamento = DataManagerControlDocumentos.GetDepartamento();
        public ObservableCollection<Departamento> ListaDepartamento
        {
            get
            {
                return _ListaDepartamento;
            }
            set
            {
                _ListaDepartamento = value;
                NotifyChange("ListaDepartamento");
            }
        }

        private TipoDocumento selectedTipoDocumento;
        public TipoDocumento SelectedTipoDocumento
        {
            get
            {
                return selectedTipoDocumento;
            }
            set
            {
                selectedTipoDocumento = value;
                NotifyChange("SelectedTipoDocumento");
            }
        }

        private Departamento selectedDepartamento;
        public Departamento SelectedDepartamento {
            get
            {
                return selectedDepartamento;
            }
            set
            {
                selectedDepartamento = value;
                NotifyChange("SelectedDepartamento");
            }
        }

        private string nombreDocumento;
        public string NombreDocumento
        {
            get { return nombreDocumento; }
            set { nombreDocumento = value; NotifyChange("NombreDocumento"); }
        }

        #endregion

        #region Commands
        /// <summary>
        /// 
        /// </summary>
        public ICommand Crear {
            get
            {
                return new RelayCommand(o => crearNumeroDocumento());
            }
        }

        /// <summary>
        /// Comando para generar un nuevo número
        /// </summary>
        public ICommand Generar
        {
            get
            {
             return new RelayCommand(o => generarNumero());
            }
        }
        
        #endregion

        #region Comandos

        /// <summary>
        /// 
        /// </summary>
        private async void crearNumeroDocumento()
        {
            DialogService dialog = new DialogService();

            if (SelectedTipoDocumento != null && selectedDepartamento != null && !string.IsNullOrEmpty(NombreDocumento))
            {
                if (!DataManagerControlDocumentos.ExistDocumento(NombreDocumento))
                {
                    Documento documento = new Documento();

                    documento.nombre = NombreDocumento;
                    documento.id_tipo_documento = selectedTipoDocumento.id_tipo;
                    documento.id_dep = selectedDepartamento.id_dep;
                    documento.usuario = NombreUsuario;
                    documento.id_estatus = 1;
                    documento.fecha_creacion = DataManagerControlDocumentos.Get_DateTime();

                    //Ejecutamos el método para registrar un nuevo documento
                    int id_doc = DataManagerControlDocumentos.SetDocumento(documento);

                    if (id_doc != 0)
                    {

                        //Copiamos el número generado al portapapeles.
                        Clipboard.SetText(NombreDocumento);

                        //Muestra mensaje con el número que se generó.
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgGenerarNumero +" " + NombreDocumento + "\n\n" +" "+ StringResources.msgPortapapeles);

                        //Muestra la ventana para crear un nuevo documento
                        FrmDocumento frm = new FrmDocumento();
                        DocumentoViewModel context = new DocumentoViewModel(ModelUsuario);
                        frm.DataContext = context;
                        frm.ShowDialog();

                        //Obtememos la ventana actual
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
                        //No se pudo dar de alta el documento
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorRegistrarDocumento);
                    }
                }
                else
                {
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgNumeroRepetido);
                }
            }
            else
            {
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgFillFlields);
            }
        }

        /// <summary>
        /// Método que genera un nuevo número y crea un nuevo documento con el número generado
        /// </summary>
        [STAThread]
        private async void generarNumero()
        {
            //Incializamos los servicios de dialog.
            DialogService dialog = new DialogService();

            if (selectedTipoDocumento != null & selectedDepartamento != null)
            {
                //Ejecutamos el método para generar el número
                string numero = DataManagerControlDocumentos.GetNumero(selectedTipoDocumento, selectedDepartamento);

                //si se generó correctamente
                if (numero != null)
                {
                    //Realizamos una última verificación de que el número no este duplicado.
                    if (!DataManagerControlDocumentos.ExistDocumento(numero))
                    {
                        //inicializamos un objeto de documento
                        Documento objDocumento = new Documento();

                        //Mapeamos los valores
                        objDocumento.nombre = numero;
                        objDocumento.id_tipo_documento = selectedTipoDocumento.id_tipo;
                        objDocumento.id_dep = selectedDepartamento.id_dep;
                        objDocumento.usuario = NombreUsuario;
                        objDocumento.id_estatus = 1;
                        objDocumento.fecha_creacion = DataManagerControlDocumentos.Get_DateTime();

                        //Ejecutamos el método para registrar un nuevo documento
                        int id_doc = DataManagerControlDocumentos.SetDocumento(objDocumento);

                        if (id_doc != 0)
                        {

                            //Copiamos el número generado al portapapeles.
                            Clipboard.SetText(numero);

                            //Muestra mensaje con el número que se generó.
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgGenerarNumero +" "+ numero + "\n\n" +" "+ StringResources.msgPortapapeles);

                            //Muestra la ventana para crear un nuevo documento
                            FrmDocumento frm = new FrmDocumento();
                            DocumentoViewModel context = new DocumentoViewModel(ModelUsuario);
                            frm.DataContext = context;
                            frm.ShowDialog();

                            //Obtememos la ventana actual
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
                            //No se pudo dar de alta el documento
                            await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorGuardarDocumento);
                        }
                    }
                    else
                    {
                        //Se generó un número que ya existe.
                        await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorGenerarNumeroExistente);
                    }

                }
                else
                {
                    //Si hubo error al generar el número
                    await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgErrorGenerarNumero);
                }
            }
            else
            {
                //Si no éscogió ninguna opción
                await dialog.SendMessage(StringResources.ttlAlerta, StringResources.msgTipoDepartamento);
            }

        }
        #endregion
    }
}
