using System.Collections.Generic;

namespace Model
{
    public class Usuario
    {
        #region Propiedades

        public string Nombre { get; set; }

        public string ApellidoPaterno { get; set; }

        public string ApellidoMaterno { get; set; }

        public string NombreUsuario { get; set; }

        public bool Block { get; set; }

        public bool Conectado { get; set; }

        public string Correo { get; set; }

        public string Pathnsf { get; set; }

        public bool IsSelected { get; set; }

        public string IdUsuario { get; set; }

        public UserDetails Details { get; set; }
        
        //Perfiles
        public bool PerfilRGP{ get; set;  }
        
        public bool PerfilTooling { get; set;  }

        public bool PerfilRawMaterial { get; set; }

        public bool PerfilStandarTime { get; set; }

        public bool PerfilQuotes { get; set; }

        public bool PerfilCIT { get; set; }

        public bool PerfilData { get; set; }

        public bool PerfilUserProfile { get; set; }

        public bool PerfilHelp { get; set; }

        //Privilegios
        public bool PrivilegioRGP{ get; set; }

        public bool PrivilegioTooling { get; set; }

        public bool PrivilegioRawMaterial { get; set; }

        public bool PrivilegioStandarTime { get; set; }

        public bool PrivilegioQuotes { get; set; }

        public bool PrivilegioCIT { get; set; }

        public bool PrivilegioData { get; set; }

        public bool PrivilegioUserProfile { get; set; }

        public bool PrivilegioHelp { get; set; }

        //Roles
        public List<Rol> Roles { get; set; }
        #endregion

        #region Constructores
        public Usuario()
        {
        }
        #endregion

        #region Métodos

        #endregion

    }
}
