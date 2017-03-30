using System.ComponentModel;
using System.Windows.Input;
using Model;
using System.Windows.Controls;
using View.Forms.Routing;
using System;
using View.Forms.RawMaterial;

namespace View.Services.ViewModel
{
    public class UsuarioViewModel : INotifyPropertyChanged
    {
        public Usuario ModelUsuario;

        #region Propiedades de Usuario
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
        
        public string ApellidoPaterno
        {
            get
            {
                return ModelUsuario.ApellidoPaterno;
            }
            set
            {
                ModelUsuario.ApellidoPaterno = value;
                NotifyChange("ApellidoPaterno");
            }
        }
        
        public string ApellidoMaterno
        {
            get
            {
                return ModelUsuario.ApellidoMaterno;
            }
            set
            {
                ModelUsuario.ApellidoMaterno = value;
                NotifyChange("ApellidoMaterno");
            }
        }
        
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
        
        public bool Block
        {
            get
            {
                return ModelUsuario.Block;
            }
            set
            {
                ModelUsuario.Block = value;
                NotifyChange("Block");
            }
        }
        
        public bool Conectado
        {
            get
            {
                return ModelUsuario.Conectado;
            }
            set
            {
                ModelUsuario.Conectado = value;
                NotifyChange("Conectado");
            }
        }
        #endregion

        #region Propiedades de navegación
        private Page pagina;
        public Page Pagina
        {
            get { return pagina; }
            set
            {
                pagina = value;
                NotifyChange("Pagina");
            }
        }

        //Perfiles
        public bool PerfilRGP
        {
            get {
                return ModelUsuario.PerfilRGP;
            }
            set {
                ModelUsuario.PerfilRGP = value;
                NotifyChange("PerfilRGP");
            }
        }
        
        public bool PerfilTooling
        {
            get {
                return ModelUsuario.PerfilTooling;
            }
            set {
                ModelUsuario.PerfilTooling = value;
                NotifyChange("PerfilTooling");
            }
        }
        
        public bool PerfilRawMaterial
        {
            get { return ModelUsuario.PerfilRawMaterial; }
            set { ModelUsuario.PerfilRawMaterial = value; NotifyChange("PerfilRawMaterial"); }
        }
        
        public bool PerfilStandarTime
        {
            get { return ModelUsuario.PerfilStandarTime; }
            set { ModelUsuario.PerfilStandarTime = value; NotifyChange("PerfilStandarTime"); }
        }
        
        public bool PerfilQuotes
        {
            get { return ModelUsuario.PerfilQuotes; }
            set { ModelUsuario.PerfilQuotes = value; NotifyChange("PerfilQuotes"); }
        }
        
        public bool PerfilCIT
        {
            get { return ModelUsuario.PerfilCIT; }
            set { ModelUsuario.PerfilCIT = value; NotifyChange("PerfilCIT"); }
        }
        
        public bool PerfilData
        {
            get { return ModelUsuario.PerfilData; }
            set { ModelUsuario.PerfilData = value; NotifyChange("PerfilData"); }
        }
        
        public bool PerfilUserProfile
        {
            get { return ModelUsuario.PerfilUserProfile; }
            set { ModelUsuario.PerfilUserProfile = value; NotifyChange("PerfilUserProfile"); }
        }
        
        public bool PerfilHelp
        {
            get { return ModelUsuario.PerfilHelp; }
            set { ModelUsuario.PerfilHelp = value; NotifyChange("PerfilHelp"); }
        }

        //Privilegios
        private bool privilegioRGP;
        public bool PrivilegioRGP
        {
            get { return privilegioRGP; }
            set { privilegioRGP = value; }
        }

        private bool privilegioTooling;
        public bool PrivilegioTooling
        {
            get { return privilegioTooling; }
            set { privilegioTooling = value; }
        }

        private bool privilegioRawMaterial;
        public bool PrivilegioRawMaterial
        {
            get { return privilegioRawMaterial; }
            set { privilegioRawMaterial = value; }
        }

        private bool privilegioStandarTime;
        public bool PrivilegioStandarTime
        {
            get { return privilegioStandarTime; }
            set { privilegioStandarTime = value; }
        }

        private bool privilegioQuotes;
        public bool PrivilegioQuotes
        {
            get { return privilegioQuotes; }
            set { privilegioQuotes = value; }
        }

        private bool privilegioCIT;
        public bool PrivilegioCIT
        {
            get { return privilegioCIT; }
            set { privilegioCIT = value; }
        }

        private bool privilegioData;
        public bool PrivilegioData
        {
            get { return privilegioData; }
            set { privilegioData = value; }
        }

        private bool privilegioUserProfile;
        public bool PrivilegioUserProfileMyProperty
        {
            get { return privilegioUserProfile; }
            set { privilegioUserProfile = value; }
        }

        private bool privilegioHelp;
        public bool PrivilegioHelp
        {
            get { return privilegioHelp; }
            set { privilegioHelp = value; }
        }

        #endregion

        #region Constructores
        public UsuarioViewModel()
        {

        }
        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion

        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Commands

        public ICommand IrRouting
        {
            get
            {
                return new RelayCommand(o => irRouting());
            }
        }

        public ICommand IrRawMaterial {
            get {
                return new RelayCommand(o => irRawMaterial());
            }
        }

        private void irRawMaterial()
        {
            PPattern pantallaPattern = new PPattern();
            PatternViewModel context = new PatternViewModel();

            pantallaPattern.DataContext = context;
            Pagina = pantallaPattern;
        }

        #endregion

        #region Métodos
        #region Métodos de navegación
        private void irRouting()
        {
            PRouting pantallaPlano = new PRouting();
            Pagina = pantallaPlano;
            pantallaPlano.DataContext = this;
        }
        #endregion
        #endregion
    }
}
