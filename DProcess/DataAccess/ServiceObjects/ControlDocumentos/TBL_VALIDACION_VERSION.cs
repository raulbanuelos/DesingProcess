//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.ControlDocumentos
{
    using System;
    using System.Collections.Generic;
    
    public partial class TBL_VALIDACION_VERSION
    {
        public int ID_VALIDACION_VERSION { get; set; }
        public int ID_VERSION { get; set; }
        public int ID_VALIDACION_DOCUMENTO { get; set; }
        public bool CORRECTO { get; set; }
        public System.DateTime FECHA_CREACION { get; set; }
        public Nullable<System.DateTime> FECHA_ACTUALIZACION { get; set; }
    
        public virtual TBL_VALIDACION_DOCUMENTO TBL_VALIDACION_DOCUMENTO { get; set; }
        public virtual TBL_VERSION TBL_VERSION { get; set; }
    }
}