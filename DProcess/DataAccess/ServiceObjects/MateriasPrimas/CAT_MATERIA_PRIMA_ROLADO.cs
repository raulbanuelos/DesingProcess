//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DataAccess.ServiceObjects.MateriasPrimas
{
    using System;
    using System.Collections.Generic;
    
    public partial class CAT_MATERIA_PRIMA_ROLADO
    {
        public string ID_MATERIA_PRIMA_ROLADO { get; set; }
        public string ID_ESPECIFICACION { get; set; }
        public string DESCRIPCION { get; set; }
        public string UM { get; set; }
        public double WIDTH { get; set; }
        public double GROOVE { get; set; }
        public double THICKNESS { get; set; }
        public string UBICACION { get; set; }
    
        public virtual material material { get; set; }
    }
}
