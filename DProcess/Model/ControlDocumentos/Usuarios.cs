﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class objUsuario : Arquetipo
    {
        #region Propiedades

        public string usuario { get; set; }
        public string password { get; set; }
        public string nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public int estado { get; set; }
        public string usql { get; set; }
        public string psql { get; set; }
        public bool bloqueado { get; set; }
        public string Correo { get; set; }
        public string Pathnsf { get; set; }
        public bool IsSelected { get; set; }
        public string roles { get; set; }
        public string extencion { get; set; }
        public string NombreCompleto {
            get
            {
                return nombre + " " + APaterno + " " + AMaterno;
            }
        }
        public string NombreCorto
        {
            get
            {
                return nombre + " " + APaterno;
            }
        }
        public string NombreAbreviado
        {
            get
            {
                return nombre.Substring(0, 1) + "." + APaterno;
            }
        }

        public UserDetails Details { get; set; }


        //string usuario,string password,string nombre,string APaterno,string AMaterno,
        // int estado,string usql,string psql,bool bloqueado, int id_departartemento
        #endregion

        #region Constructores

        #endregion
    }
}
