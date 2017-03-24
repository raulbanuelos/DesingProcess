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
        #endregion

        #region Constructores

        public Pattern()
        {
            codigo = new PropiedadCadena();
            medida = new Propiedad("DPlacaModelo","Diámetro","Diámetro de la placa modelo","Distance");
            diametro = new Propiedad("WPlacaModelo","Width","Width de la placa modelo","Distance");
            customer = new Cliente();
            mounting = new Propiedad("MoutingPlacaModelo","Mouting","Número de impresiones de la placa modelo","Cantidad");
            on_14_rd_gate = new PropiedadCadena();
            button = new PropiedadCadena();
            cone = new PropiedadCadena();
            M_Circle = new PropiedadCadena();
            ring_w_min = new Propiedad("RingWidthMinPlacaModelo", "Ring Width Min", "Width mínimo del anillo", "Distance");
            ring_w_max = new Propiedad("RingWidthMaxPlacaModelo", "Ring Width Max", "Width máximo del anillo", "Distance");
            date_ordered = new PropiedadCadena();
            B_Dia = new Propiedad("BDiaPlacaModelo","B Dia","Diámetro B de la placa modelo","Distance");
            fin_Dia = new Propiedad("FinDiaPlacaModelo","Fin Dia","","Distance");
            turn_allow = new Propiedad("TurnAllowPlacaModelo","Turn allow","Material permitido a remover en el diámetro exterior","Distance");
            cstg_sm_od = new Propiedad("CstgSModPlacaModelo", "cstg sm od","","Distance");
            shrink_allow = new Propiedad("ShrinkAllowPlacaModelo", "Shrink Allow","","Distance");
            patt_sm_od = new Propiedad("PattSMODPlacaModelo","Patt SM OD","","Distance");
            piece_in_patt = new Propiedad("PieceInPattPlacaModelo","Piece in patt","","Distance");
            bore_allow = new Propiedad("BoreAllowPlacaModelo","Bore Allow","Material permitido a remover en el diámetro interior","Distance");
            patt_sm_id = new Propiedad("PattSMIDPlacaModelo","Patt SM ID","","Distance");
            patt_thickness = new Propiedad("PattThicknessPlacaModelo","Patt thickness","","Distance");
            joint = new PropiedadCadena();
            nick = new PropiedadCadena();
            nick_draf = new PropiedadCadena();
            nick_depth = new PropiedadCadena();
            side_relief = new PropiedadCadena();
            cam = new Propiedad("CamPlacaModelo","Cam","","Distance");
            cam_roll = new Propiedad("CamRollPlacaModelo","Cam Roll","","Distance");
            rise = new Propiedad("RisePlacaModelo","Rise","","Distance");
            OD = new Propiedad("ODPlacaModelo","OD","Diámetro exterior de la placa modelo","Distance");
            ID = new Propiedad("IDPlacaModelo","ID","Diámetro interior de la placa modelo","Distance");
            diff = new Propiedad("DiffPlacaModelo","Diff","Diferencia entre OD e ID","Distance");
            tipo = new Propiedad();
            mounted = new PropiedadCadena();
            ordered = new PropiedadCadena();
            Checked = new PropiedadCadena();
            date_checked = new PropiedadCadena();
            esp_inst = new PropiedadCadena();
            factor_k = new Propiedad("FactorKPlacaModelo","Factor K","","Distance");
            rise_built = new Propiedad("RiseBuiltPlacaModelo","Rise built","","Distance");
            ring_th_min = new Propiedad("RingThicknessMinPlacaModelo","Ring Th Min","Thickness mínimo del anillo","Distance");
            ring_th_max = new Propiedad("RingThicknessMaxPlacaModelo","Ring Th Max","Thickness máximo del anillo", "Distance");
            estado = new PropiedadBool();
            plato = new Propiedad("PlatoPlacaModelo","Plato", "Dimensión del plato de la placa modelo","Distance");
            detalle = new PropiedadCadena();
            diseno = new PropiedadBool();

        }
         
        #endregion

    }

}
