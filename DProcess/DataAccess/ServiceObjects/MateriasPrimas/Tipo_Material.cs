namespace DataAccess.ServiceObjects.MateriasPrimas
{
    using System;
    using System.Collections.Generic;
    public class Tipo_Material
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Tipo_Material()
        {
            this.CAT_MATERIA_PRIMA_ROLADO = new HashSet<CAT_MATERIA_PRIMA_ROLADO>();
            this.Compensacion_Piece = new HashSet<Compensacion_Piece>();
            this.CAT_MATERIA_PRIMA_ACEROS = new HashSet<CAT_MATERIA_PRIMA_ACEROS>();
        }

        public int id_tipo_material { get; set; }
        public string tipo_material { get; set; }

        public virtual Esp_MP_Anillos Esp_MP_Anillos { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAT_MATERIA_PRIMA_ROLADO> CAT_MATERIA_PRIMA_ROLADO { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Compensacion_Piece> Compensacion_Piece { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CAT_MATERIA_PRIMA_ACEROS> CAT_MATERIA_PRIMA_ACEROS { get; set; }
    }
}