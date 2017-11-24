using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{ 

    public class Pattern : Arquetipo
    {
        #region Propiedades 
        public PropiedadCadena codigo { get; set; }
        public Propiedad medida { get; set; }
        public Propiedad diametro { get; set; }
        public Cliente customer { get; set; }
        public Propiedad mounting { get; set; }
        public PropiedadCadena on_14_rd_gate { get; set; }
        public PropiedadCadena button { get; set; }
        public PropiedadCadena cone { get; set; }
        public PropiedadCadena M_Circle { get; set; }
        public Propiedad ring_w_min { get; set; }
        public Propiedad ring_w_max { get; set; }
        public PropiedadCadena date_ordered { get; set; }
        public Propiedad B_Dia { get; set; }
        public Propiedad fin_Dia { get; set; }
        public Propiedad turn_allow { get; set; }
        public Propiedad cstg_sm_od { get; set; }
        public Propiedad shrink_allow { get; set; }
        public Propiedad patt_sm_od { get; set; }
        public Propiedad piece_in_patt { get; set; }
        public Propiedad bore_allow { get; set; }
        public Propiedad patt_sm_id { get; set; }
        public Propiedad patt_thickness { get; set; }
        public PropiedadCadena joint { get; set; }
        public PropiedadCadena nick { get; set; }
        public PropiedadCadena nick_draf { get; set; }
        public PropiedadCadena nick_depth { get; set; }
        public PropiedadCadena side_relief { get; set; }
        public Propiedad cam { get; set; }
        public Propiedad cam_roll { get; set; }
        public Propiedad rise { get; set; }
        public Propiedad OD { get; set; }
        public Propiedad ID { get; set; }
        public Propiedad diff { get; set; }
        public Propiedad tipo { get; set; }
        public PropiedadCadena mounted { get; set; }
        public PropiedadCadena ordered { get; set; }
        public PropiedadCadena Checked { get; set; }
        public PropiedadCadena date_checked { get; set; }
        public PropiedadCadena esp_inst { get; set; }
        public Propiedad factor_k { get; set; }
        public Propiedad rise_built { get; set; }
        public Propiedad ring_th_min { get; set; }
        public Propiedad ring_th_max { get; set; }
        public PropiedadBool estado { get; set; }
        public Propiedad plato { get; set; }
        public PropiedadCadena detalle { get; set; }
        public PropiedadBool diseno { get; set; }
        public Propiedad peso_cstg { get; set; }
        public Propiedad cam_lever { get; set; }
        public Propiedad patt_width { get; set; }
        public PropiedadCadena TipoAnillo { get; set; }

        public PropiedadCadena Hardness { get; set; }
        public Propiedad HardnessMin { get; set; }
        public Propiedad HardnessMax { get; set; }
        public PropiedadCadena Proceso { get; set; }
        public PropiedadCadena EspecMaterialAnillo { get; set; }
        public PropiedadCadena TipoMaterial { get; set; }
        #endregion

        #region Constructores

        public Pattern()
        {
            date_ordered = new PropiedadCadena { DescripcionCorta = "Date Ordered", DescripcionLarga = "Date Ordered", Imagen = null, Nombre = "DateOrdered", Valor = DateTime.Now.ToString() };
            date_checked = new PropiedadCadena { DescripcionCorta = "Date Checked", DescripcionLarga = "Date Checked", Imagen = null, Nombre = "DateChecked", Valor = DateTime.Now.ToString() };

            joint = new PropiedadCadena();
            turn_allow = new Propiedad();
            bore_allow = new Propiedad();
            joint = new PropiedadCadena();
            nick = new PropiedadCadena();
            nick_draf = new PropiedadCadena();
            nick_depth = new PropiedadCadena();
            side_relief = new PropiedadCadena();
            cam = new Propiedad();
            cam_roll = new Propiedad();
            cam_lever = new Propiedad();
            rise_built = new Propiedad();
            diseno = new PropiedadBool { Valor = true };
            patt_width = new Propiedad();
            codigo = new PropiedadCadena();
        }
         
        #endregion

    }

}
