//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.Perfiles
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAT_TABLA_PROPIEDAD_OPCIONAL
    {
        public int ID_TABLA_PROPIEDAD_OPCIONAL { get; set; }
        public Nullable<int> ID_PROPIEDAD_OPCIONAL { get; set; }
        public string NOMBRE_TABLA { get; set; }
        public string CAMPO_ID { get; set; }
        public string CAMPO_MOSTRAR { get; set; }
    
        public virtual CAT_PROPIEDAD_OPCIONAL CAT_PROPIEDAD_OPCIONAL { get; set; }
    }
}