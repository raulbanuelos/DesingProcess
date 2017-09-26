using MahApps.Metro.Controls.Dialogs;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace View.Services.ViewModel
{
    public class NuevoMaestroHerramental_VM : INotifyPropertyChanged
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

        #region Atributtes
        public Usuario usuario;
        DialogService dialog = new DialogService();
        #endregion

        #region Propiedades
        private string codigo;
        public string Codigo { get
            {
                return codigo;
            }
            set
            {
                codigo = value;
                NotifyChange("Codigo");
            }
        }

        private string descripcion;
        public string Descripcion { get
            {
                return descripcion;
            }
            set
            {
                descripcion = value;
                NotifyChange("Descripcion");
            } }

        private ObservableCollection<ClasificacionHerramental> _ListaClasificacion;
        public ObservableCollection<ClasificacionHerramental> ListaClasificacion { get
            {
                return _ListaClasificacion;
            }
            set
            {
                _ListaClasificacion = value;
                NotifyChange("ListaClasificacion");
            }
        }

        private ObservableCollection<Plano> _ListaPlano;
        public ObservableCollection<Plano> ListaPlano
        {
            get
            {
                return _ListaPlano;
            }
            set
            {
                _ListaPlano = value;
                NotifyChange("ListaPlano");
            }
        }

        private ClasificacionHerramental _SelectedClasificacion;
        public ClasificacionHerramental SelectedClasificacion { get
            {
                return _SelectedClasificacion;
            }
            set
            {
                _SelectedClasificacion = value;
                NotifyChange("SelectedClasificacion");
            }
        }

        private Plano _SelectedPlano;
        public Plano SelectedPlano { get
            {
                return _SelectedPlano;
            }
            set
            {
                _SelectedPlano = value;
                NotifyChange("SelectedPlano");
            }
        }

        private bool _IsSelected;
        public bool IsSelected { get
            {
                return _IsSelected;
            }
            set
            {
                _IsSelected = value;
                NotifyChange("IsSelected");
            }
        }
        #endregion

        #region Commands

        public ICommand GuardarMaestro
        {
            get
            {
                return new RelayCommand(o => guardar());
            }
        }
        #endregion
        #region Methods

        private async void guardar()
        {
            //Declaramos un objeto de tipo MetroDialogSettings al cual le asignamos las propiedades que contendra el mensaje modal.
            MetroDialogSettings setting = new MetroDialogSettings();
            setting.AffirmativeButtonText = "SI";
            setting.NegativeButtonText = "NO";

            if (ValidaValores())
            {
                //Ejecutamos el método para mostrar el mensaje con la información que el usuario capturó.El resultado lo asignamos a una variable local.
                MessageDialogResult result = await dialog.SendMessage("Attention", "¿Desea guardar los cambios?", setting, MessageDialogStyle.AffirmativeAndNegative);

                if (result == MessageDialogResult.Affirmative)
                {
                    MaestroHerramental obj = new MaestroHerramental();

                    if (IsSelected)
                        obj.activo = true;
                    else
                        obj.activo = false;

                    obj.Codigo = Codigo;
                    obj.descripcion = Descripcion;
                    obj.fecha_creacion = DateTime.Now.ToString();
                    obj.fecha_cambio = DateTime.Now.ToString();
                    obj.usuario_cambio = usuario.NombreUsuario;
                    obj.usuario_creacion = usuario.NombreUsuario;

                    string codigo_maestro = DataManager.SetMaestroHerramentales(obj);

                    if (codigo_maestro !=null)
                    {

                    }
                }
            }
            else
            {

            }
        }

        private bool ValidaValores()
        {
            if (!string.IsNullOrEmpty(codigo) & !string.IsNullOrWhiteSpace(codigo) & !string.IsNullOrWhiteSpace(descripcion) & !string.IsNullOrEmpty(descripcion) & SelectedClasificacion != null & SelectedPlano != null)
                return true;
            else
                return false;
        }
        #endregion
        #region Constructor
        public NuevoMaestroHerramental_VM(Usuario ModelUsuario)
        {
            usuario = ModelUsuario;
            ListaPlano = DataManager.GetPlano_Herramental();
            ListaClasificacion = DataManager.GetClasificacionHerramental();
        }
        #endregion
    }
}
