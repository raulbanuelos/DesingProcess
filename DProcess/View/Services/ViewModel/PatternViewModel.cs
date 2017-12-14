using System.ComponentModel;
using Model;
using System.Windows.Input;
using System;
using System.IO;
using System.Collections.Generic;
using View.Forms.Shared;

namespace View.Services.ViewModel
{
    public class PatternViewModel : INotifyPropertyChanged
    {
        #region Atributos
        private Pattern model;
        private bool calculoOk;
        #endregion

        #region Propiedades Pattern

        public PropiedadCadena codigo
        {
            get
            {
                return model.codigo;
            }
            set
            {
                model.codigo = value;
                NotifyChange("codigo");
            }
        }

        public Cliente customer
        {
            get
            {
                return model.customer;
            }
            set
            {
                model.customer = value;
                NotifyChange("customer");
            }
        }

        public Propiedad medida {
            get
            {
                return model.medida;
            }
            set
            {
                model.medida = value;
                NotifyChange("medida");
            }
        }

        public Propiedad diametro {
            get
            {
                return model.diametro;
            }
            set
            {
                model.diametro = value;
                NotifyChange("diametro");
            }
        }

        public Propiedad mounting {
            get { return model.mounting; }
            set { model.mounting = value; NotifyChange("mounting"); }
        }

        public PropiedadCadena on_14_rd_gate {
            get { return model.on_14_rd_gate; }
            set { model.on_14_rd_gate = value; NotifyChange("on_14_rd_gate"); }
        }

        public PropiedadCadena button {
            get { return model.button; }
            set { model.button = value; NotifyChange("button"); }
        }

        public PropiedadCadena cone {
            get { return model.cone; }
            set { model.cone = value; NotifyChange("cone"); }
        }

        public PropiedadCadena M_Circle {
            get { return model.M_Circle; }
            set { model.M_Circle = value; NotifyChange("M_Circle"); }
        }

        public Propiedad ring_w_min {
            get { return model.ring_w_min; }
            set { model.ring_w_min = value; NotifyChange("ring_w_min"); }
        }

        public Propiedad ring_w_max {
            get { return model.ring_w_max; }
            set { model.ring_w_max = value; NotifyChange("ring_w_max"); }
        }

        public PropiedadCadena date_ordered {
            get { return model.date_ordered; }
            set { model.date_ordered = value; NotifyChange("date_ordered"); }
        }

        public Propiedad B_Dia {
            get { return model.B_Dia; }
            set { model.B_Dia = value; NotifyChange("B_Dia"); }
        }

        public Propiedad fin_Dia
        {
            get { return model.fin_Dia; }
            set { model.fin_Dia = value; NotifyChange("fin_Dia"); }
        }

        public Propiedad turn_allow
        {
            get { return model.turn_allow; }
            set { model.turn_allow = value; NotifyChange("turn_allow"); }
        }

        public Propiedad cstg_sm_od
        {
            get { return model.cstg_sm_od; }
            set { model.cstg_sm_od = value; NotifyChange("cstg_sm_od"); }
        }

        public Propiedad shrink_allow
        {
            get { return model.shrink_allow; }
            set { model.shrink_allow = value; NotifyChange("shrink_allow"); }
        }

        public Propiedad patt_sm_od
        {
            get { return model.patt_sm_od; }
            set { model.patt_sm_od = value; NotifyChange("patt_sm_od"); }
        }

        public Propiedad piece_in_patt
        {
            get { return model.piece_in_patt; }
            set { model.piece_in_patt = value; NotifyChange("piece_in_patt"); }
        }

        public Propiedad bore_allow
        {
            get { return model.bore_allow; }
            set { model.bore_allow = value; NotifyChange("bore_allow"); }
        }

        public Propiedad patt_sm_id
        {
            get { return model.patt_sm_id; }
            set { model.patt_sm_id = value; NotifyChange("patt_sm_id"); }
        }
   
        public Propiedad patt_thickness {
            get { return model.patt_thickness; }
            set { model.patt_thickness = value; NotifyChange("patt_thickness"); }
        }

        public PropiedadCadena joint {
            get { return model.joint; }
            set { model.joint = value; NotifyChange("joint"); }
        }

        public PropiedadCadena nick {
            get { return model.nick; }
            set { model.nick = value; NotifyChange("nick"); }
        }

        public PropiedadCadena nick_draf {
            get { return model.nick_draf; }
            set { model.nick_draf = value; NotifyChange("nick_draf"); }
        }

        public PropiedadCadena nick_depth
        {
            get { return model.nick_depth; }
            set { model.nick_depth = value; NotifyChange("nick_depth"); }
        }
       
        public PropiedadCadena side_relief {
            get { return model.side_relief; }
            set { model.side_relief = value; NotifyChange("side_relief"); }
        }

        public Propiedad cam {
            get { return model.cam; }
            set { model.cam = value; NotifyChange("cam"); }
        }

        public Propiedad cam_roll
        {
            get { return model.cam_roll; }
            set { model.cam_roll = value; NotifyChange("cam_roll"); }
        }
        
        public Propiedad rise {
            get { return model.rise; }
            set { model.rise = value; NotifyChange("rise"); }
        }

        public Propiedad OD
        {
            get { return model.OD; }
            set { model.OD = value; NotifyChange("OD"); }
        }

        public Propiedad ID
        {
            get { return model.ID; }
            set { model.ID = value; NotifyChange("ID"); }
        }

        public Propiedad diff
        {
            get { return model.diff; }
            set { model.diff = value; NotifyChange("diff"); }
        }

        public PropiedadCadena Tipo
        {
            get { return model.TipoAnillo; }
            set { model.TipoAnillo = value; NotifyChange("Tipo"); }
        }

        public PropiedadCadena mounted
        {
            get { return model.mounted; }
            set { model.mounted = value; NotifyChange("mounted"); }
        }

        public PropiedadCadena ordered
        {
            get { return model.ordered; }
            set { model.ordered = value; NotifyChange("ordered"); }
        }

        public PropiedadCadena Checked
        {
            get { return model.Checked; }
            set { model.Checked = value; NotifyChange("Checked"); }
        }

        public PropiedadCadena date_checked
        {
            get { return model.date_checked; }
            set { model.date_checked = value; NotifyChange("date_checked"); }
        }

        public PropiedadCadena esp_inst
        {
            get { return model.esp_inst; }
            set { model.esp_inst = value; NotifyChange("esp_inst"); }
        }
       
        public Propiedad factor_k {
            get { return model.factor_k; }
            set { model.factor_k = value; NotifyChange("factor_k"); }
        }

        public Propiedad rise_built
        {
            get { return model.rise_built; }
            set { model.rise_built = value; NotifyChange("rise_built"); }
        }

        public Propiedad ring_th_min
        {
            get { return model.ring_th_min; }
            set { model.ring_th_min = value; NotifyChange("ring_th_min"); }
        }

        public Propiedad ring_th_max
        {
            get { return model.ring_th_max; }
            set { model.ring_th_max = value; NotifyChange("ring_th_max"); }
        }
 
        public PropiedadBool estado {
            get { return model.estado; }
            set { model.estado = value; NotifyChange("estado"); }
        }

        public Propiedad plato
        {
            get { return model.plato; }
            set { model.plato = value; NotifyChange("plato"); }
        }

        public PropiedadCadena detalle
        {
            get { return model.detalle; }
            set { model.detalle = value; NotifyChange("detalle"); }
        }

        public Propiedad peso_cstg
        {
            get { return model.peso_cstg; }
            set { model.peso_cstg = value; NotifyChange("peso_cstg"); }
        }

        public Propiedad cam_lever
        {
            get { return model.cam_lever; }
            set { model.cam_lever = value; NotifyChange("cam_lever"); }
        }

        public Propiedad patt_width
        {
            get { return model.patt_width; }
            set { model.patt_width = value; NotifyChange("patt_width"); }
        }

        public PropiedadCadena Hardness {
            get
            {
                return model.Hardness;
            }
            set
            {
                model.Hardness = value;
                NotifyChange("Hardness");
            }
        }

        public Propiedad HardnessMin {
            get
            {
                return model.HardnessMin;
            }
            set
            {
                model.HardnessMin = value;
                NotifyChange("HardnessMin");
            }
        }

        public Propiedad HardnessMax {
            get
            {
                return model.HardnessMax;
            }
            set
            {
                model.HardnessMax = value;
                NotifyChange("HardnessMax");
            }
        }

        public PropiedadCadena Proceso {
            get
            {
                return model.Proceso;
            }
            set
            {
                model.Proceso = value;
                NotifyChange("Proceso");
            }
        }

        public PropiedadCadena EspecMaterialAnillo {
            get
            {
                return model.EspecMaterialAnillo;
            }
            set
            {
                model.EspecMaterialAnillo = value;
                NotifyChange("EspecMaterialAnillo");
            }
        }

        public PropiedadCadena TipoMaterial {
            get
            {
                return model.TipoMaterial;
            }
            set
            {
                model.TipoMaterial = value;
                NotifyChange("TipoMaterial");
            }
        }

        public PropiedadCadena TipoAnillo {
            get
            {
                return model.TipoAnillo;
            }
            set
            {
                model.TipoAnillo = value;
                NotifyChange("TipoAnillo");
            }
        }

        public PropiedadBool diseno
        {
            get
            {
                return model.diseno;
            }
            set
            {
                model.diseno = value;
                NotifyChange("diseno");
            }
        }

        #endregion

        #region Propierties
        private bool _ReadOnlyFactorK;
        public bool ReadOnlyFactorK
        {
            get { return _ReadOnlyFactorK; }
            set { _ReadOnlyFactorK = value; NotifyChange("ReadOnlyFactorK"); }
        }

        private bool _ReadOnlyCamLever;
        public bool ReadOnlyCamLever
        {
            get { return _ReadOnlyCamLever; }
            set { _ReadOnlyCamLever = value; NotifyChange("ReadOnlyCamLever"); }
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

        #region Constructores

        public PatternViewModel(Pattern modelPattern)
        {
            model = modelPattern;
        }

        public PatternViewModel()
        {
            model = new Pattern();
            codigo = new PropiedadCadena();
            codigo.Valor = "";
            medida = new Propiedad();
            medida.Valor = 0;
            diametro = new Propiedad();
            diametro.Valor = 0;
            detalle = new PropiedadCadena();
            detalle.Valor = "";
            customer = new Cliente();
            customer.NombreCliente = "";
            mounting = new Propiedad();
            mounting.Valor = 0;
            plato = new Propiedad();
            plato.Valor = 0;
            on_14_rd_gate = new PropiedadCadena();
            on_14_rd_gate.Valor = "";
            button = new PropiedadCadena();
            button.Valor = "";
            M_Circle = new PropiedadCadena();
            M_Circle.Valor = "";
            cone = new PropiedadCadena();
            cone.Valor = "";
            ring_th_min = new Propiedad();
            ring_th_min.Valor = 0;
            ring_th_max = new Propiedad();
            ring_th_max.Valor = 0;
            ring_w_min = new Propiedad();
            ring_w_min.Valor = 0;
            ring_w_max = new Propiedad();
            ring_w_max.Valor = 0;
            date_ordered = new PropiedadCadena();
            date_ordered.Valor = "";
            mounted = new PropiedadCadena();
            mounted.Valor = "";
            ordered = new PropiedadCadena();
            ordered.Valor = "";
            Checked = new PropiedadCadena();
            Checked.Valor = "";
            factor_k = new Propiedad();
            factor_k.Valor = 0;
            OD = new Propiedad();
            OD.Valor = 0;
            ID = new Propiedad();
            ID.Valor = 0;
            diff = new Propiedad();
            diff.Valor = 0;
            B_Dia = new Propiedad();
            B_Dia.Valor = 0;
            fin_Dia = new Propiedad();
            fin_Dia.Valor = 0;
            turn_allow = new Propiedad();
            turn_allow.Valor = 0;
            cstg_sm_od = new Propiedad();
            cstg_sm_od.Valor = 0;
            shrink_allow = new Propiedad();
            shrink_allow.Valor = 0;
            patt_sm_od = new Propiedad();
            patt_sm_od.Valor = 0;
            piece_in_patt = new Propiedad();
            piece_in_patt.Valor = 0;
            bore_allow = new Propiedad();
            bore_allow.Valor = 0;
            patt_thickness = new Propiedad();
            patt_thickness.Valor = 0;
            patt_sm_id = new Propiedad();
            patt_sm_id.Valor = 0;
            joint = new PropiedadCadena();
            joint.Valor = "";
            nick = new PropiedadCadena();
            nick.Valor = "";
            nick_draf = new PropiedadCadena();
            nick_draf.Valor = "";
            nick_depth = new PropiedadCadena();
            nick_depth.Valor = "";
            side_relief = new PropiedadCadena();
            side_relief.Valor = "";
            cam = new Propiedad();
            cam.Valor = 0;
            cam_roll = new Propiedad();
            cam_roll.Valor = 0;
            rise_built = new Propiedad();
            rise_built.Valor = 0;
            cam_lever = new Propiedad();
            cam_lever.Valor = 0;
            patt_width = new Propiedad();
            patt_width.Valor = 0;
            peso_cstg = new Propiedad();
            peso_cstg.Valor = 0;
            TipoAnillo = new PropiedadCadena();
            TipoAnillo.Valor = "";
            diseno = new PropiedadBool { Valor = true};
            


            //medida = new PropiedadViewModel("DPlacaModelo", "Diámetro", "Diámetro de la placa modelo", "Distance");
            //diametro = new PropiedadViewModel("WPlacaModelo", "Width", "Width de la placa modelo", "Distance");
            //customer = new Cliente();
            //mounting = new PropiedadViewModel("MoutingPlacaModelo", "Mouting", "Número de impresiones de la placa modelo", "Cantidad");
            //on_14_rd_gate = new PropiedadCadenaViewModel();
            //button = new PropiedadCadenaViewModel();
            //cone = new PropiedadCadenaViewModel();
            //M_Circle = new PropiedadCadenaViewModel();
            //ring_w_min = new PropiedadViewModel("RingWidthMinPlacaModelo", "Ring Width Min", "Width mínimo del anillo", "Distance");
            //ring_w_max = new PropiedadViewModel("RingWidthMaxPlacaModelo", "Ring Width Max", "Width máximo del anillo", "Distance");
            //date_ordered = new PropiedadCadenaViewModel();
            //B_Dia = new PropiedadViewModel("BDiaPlacaModelo", "B Dia", "Diámetro B de la placa modelo", "Distance");
            //fin_Dia = new PropiedadViewModel("FinDiaPlacaModelo", "Fin Dia", "", "Distance");
            //turn_allow = new PropiedadViewModel("TurnAllowPlacaModelo", "Turn allow", "Material permitido a remover en el diámetro exterior", "Distance");
            //cstg_sm_od = new PropiedadViewModel("CstgSModPlacaModelo", "cstg sm od", "", "Distance");
            //shrink_allow = new PropiedadViewModel("ShrinkAllowPlacaModelo", "Shrink Allow", "", "Distance");
            //patt_sm_od = new PropiedadViewModel("PattSMODPlacaModelo", "Patt SM OD", "", "Distance");
            //piece_in_patt = new PropiedadViewModel("PieceInPattPlacaModelo", "Piece in patt", "", "Distance");
            //bore_allow = new PropiedadViewModel("BoreAllowPlacaModelo", "Bore Allow", "Material permitido a remover en el diámetro interior", "Distance");
            //patt_sm_id = new PropiedadViewModel("PattSMIDPlacaModelo", "Patt SM ID", "", "Distance");
            //patt_thickness = new PropiedadViewModel("PattThicknessPlacaModelo", "Patt thickness", "", "Distance");
            //joint = new PropiedadCadenaViewModel();
            //nick = new PropiedadCadenaViewModel();
            //nick_draf = new PropiedadCadenaViewModel();
            //nick_depth = new PropiedadCadenaViewModel();
            //side_relief = new PropiedadCadenaViewModel();
            //cam = new PropiedadViewModel("CamPlacaModelo", "Cam", "", "Distance");
            //cam_roll = new PropiedadViewModel("CamRollPlacaModelo", "Cam Roll", "", "Distance");
            //rise = new PropiedadViewModel("RisePlacaModelo", "Rise", "", "Distance");
            //OD = new PropiedadViewModel("ODPlacaModelo", "OD", "Diámetro exterior de la placa modelo", "Distance");
            //ID = new PropiedadViewModel("IDPlacaModelo", "ID", "Diámetro interior de la placa modelo", "Distance");
            //diff = new PropiedadViewModel("DiffPlacaModelo", "Diff", "Diferencia entre OD e ID", "Distance");
            ////tipo = new PropiedadViewModel();
            //mounted = new PropiedadCadenaViewModel();
            //ordered = new PropiedadCadenaViewModel();
            //Checked = new PropiedadCadenaViewModel();
            //date_checked = new PropiedadCadenaViewModel();
            //esp_inst = new PropiedadCadenaViewModel();
            //factor_k = new PropiedadViewModel("FactorKPlacaModelo", "Factor K", "", "Distance");
            //rise_built = new PropiedadViewModel("RiseBuiltPlacaModelo", "Rise built", "", "Distance");
            //ring_th_min = new PropiedadViewModel("RingThicknessMinPlacaModelo", "Ring Th Min", "Thickness mínimo del anillo", "Distance");
            //ring_th_max = new PropiedadViewModel("RingThicknessMaxPlacaModelo", "Ring Th Max", "Thickness máximo del anillo", "Distance");
            //estado = new PropiedadBoolViewModel();
            //plato = new PropiedadViewModel("PlatoPlacaModelo", "Plato", "Dimensión del plato de la placa modelo", "Distance");
            //detalle = new PropiedadCadenaViewModel();
            //diseno = new PropiedadBoolViewModel();
        }
        #endregion

        #region Commands

        /// <summary>
        /// Comando que responde a la petición de guardar una placa modelo.
        /// </summary>
        public ICommand GuardarPattern {
            get {
                return new RelayCommand(o => guardarPattern());
            }
        }

        public ICommand Calcular
        {
            get{
                return new RelayCommand(o => calcularPlaca());
            }
        }

        #endregion

        #region Methods

        private void calcularPlaca()
        {
            //Constante
            joint.Valor = "BUTT";
            nick.Valor = "RAD.";
            nick_draf.Valor = "4°";
            nick_depth.Valor = ".045 - .050";
            side_relief.Valor = "-----";
            cam.Valor = 8;
            cam_roll.Valor = 1.622;
            cam_lever.Valor = 0.074;
            rise_built.Valor = 0.005;
            
            if (diseno.Valor)
            {
                double[] resultsTurnBore = DataManager.Get_TurnBoreAllow(TipoAnillo.Valor, EspecMaterialAnillo.Valor);
                turn_allow.Valor = resultsTurnBore[0];
                bore_allow.Valor = resultsTurnBore[1];
            }
            else
            {
                turn_allow.Valor = 0.1;
                bore_allow.Valor = 0.105;
            }

            if (TipoMaterial.Valor.Equals("GASOLINA") || TipoMaterial.Valor.Equals("SPR-212"))
                patt_width.Valor = DataManager.GetIdealCastingWidth(medida.Valor, Proceso.Valor);
            else
                patt_width.Valor = ring_w_max.Valor * 1 + 0.021;

            medida = patt_width;


            if (!factor_k.Valor.Equals(0))
            {
                codigo.Valor = DataManager.GetNextCodePattern(DataManager.GetLastCodePattern());

                //Comparamos si el tipo de material es gasolina.
                if (TipoMaterial.Valor.Equals("GASOLINA"))
                {
                    //Comparamos si el diseño es normal.
                    if (diseno.Valor)
                    {
                        cam_lever.Valor = Math.Round((piece_in_patt.Valor * factor_k.Valor * 64) + 0.005, 3);
                        ReadOnlyFactorK = false;
                        ReadOnlyCamLever = true;
                    }
                    else //Si el diseño es redondo
                    {
                        ReadOnlyFactorK = false;
                        ReadOnlyCamLever = false;
                        cam_lever.Valor = Math.Round((piece_in_patt.Valor * factor_k.Valor * 64) - 0.005, 3);
                    }
                    fin_Dia.Valor = Math.Round((((((cam_lever.Valor - 0.005) * 0.478) - piece_in_patt.Valor) / -3.1416) + diametro.Valor) - (cam_lever.Valor - 0.005), 3);

                }
                else if(TipoMaterial.Valor.Equals("SPR-212")) //Si el tipo de material es 
                {
                    cam_lever.Valor = Math.Round((piece_in_patt.Valor * factor_k.Valor * 64) + 0.015, 3);
                    fin_Dia.Valor = Math.Round((((((cam_lever.Valor - 0.015) * 0.478) - piece_in_patt.Valor) / -3.1416) + diametro.Valor) - (cam_lever.Valor - 0.015), 3);
                }
                else if(TipoMaterial.Valor.Equals("SUPER DUTY"))
                {
                    cam_lever.Valor = Math.Round((piece_in_patt.Valor * factor_k.Valor * 64), 3);
                    fin_Dia.Valor = Math.Round((((((cam_lever.Valor - 0.005) * 0.478) - piece_in_patt.Valor) / -3.1416) + diametro.Valor) - (cam_lever.Valor - 0.005), 3);
                }
                else
                {
                    //NOTIFICAR QUE ESTE MATERIAL NO EXISTE!!!
                    return;
                }

                cstg_sm_od.Valor = Math.Round(fin_Dia.Valor + (1 * turn_allow.Valor), 3);

                if (TipoMaterial.Valor.Equals("GASOLINA"))
                {
                    shrink_allow.Valor = Math.Round(cstg_sm_od.Valor * 0.0104, 3);
                }
                else if (TipoMaterial.Valor.Equals("SPR-212"))
                {
                    shrink_allow.Valor = 0.02;
                }
                else if(TipoMaterial.Valor.Equals("SUPER DUTY"))
                {
                    shrink_allow.Valor = Math.Round(cstg_sm_od.Valor * 0.0094, 3);
                }

                patt_sm_od.Valor = Math.Round(cstg_sm_od.Valor + (1 * shrink_allow.Valor), 3);
                patt_thickness.Valor = Math.Round((turn_allow.Valor + (1 * bore_allow.Valor)) / 2 + (ring_th_min.Valor + (1 * ring_th_max.Valor)) / 2, 3);
                patt_sm_id.Valor = Math.Round(patt_sm_od.Valor - (patt_thickness.Valor * 2), 3);
                OD.Valor = Math.Round(cstg_sm_od.Valor + ((cam_lever.Valor - rise_built.Valor) * 2), 3);
                ID.Valor = Math.Round(patt_sm_id.Valor - (patt_sm_id.Valor * 0.015), 3);
                diff.Valor = Math.Round(OD.Valor - ID.Valor, 3);
                peso_cstg.Valor = Math.Round((((3.1416 / 4) * (Convert.ToDouble(Math.Pow(patt_sm_od.Valor , 2)) - Convert.ToDouble(Math.Pow(patt_sm_id.Valor,2)))) * medida.Valor * 16.387 * 7.2) / 0.95, 3);
                B_Dia.Valor = Math.Round(patt_sm_od.Valor + (2 * cam_lever.Valor), 4);

                definirPlato();
                if (calculoOk)
                {
                    detalle.Valor = DataManager.GetDetalleMoutingWidth(medida.Valor);
                    string[] valoresMoutingDia = DataManager.GetMoutingDia(B_Dia.Valor, plato.Valor);
                    if (valoresMoutingDia.Length.Equals(5) )
                    {
                        mounting.Valor = Convert.ToDouble(valoresMoutingDia[0]);
                        on_14_rd_gate.Valor = valoresMoutingDia[1];
                        M_Circle.Valor = valoresMoutingDia[2].Equals("Aplica") ? Convert.ToString(Math.Round((patt_sm_id.Valor - .06) / 2, 3)) : M_Circle.Valor = "No Aplica";
                        button.Valor = valoresMoutingDia[3];
                        cone.Valor = valoresMoutingDia[4];
                    }else
                    {
                        //Notificar que no se encontraron valores de la tabla MoutingDia.
                    }

                }else
                {
                    //Notificar al usuario que no se completo el calculo.
                }
                
            }

            actualizarValores();
        }

        private void definirPlato()
        {
            List<double> ListaPlato = DataManager.GetPlatoMoutingDia(B_Dia.Valor);

            ListItemViewModel context = new ListItemViewModel(ListaPlato);
            WOptionList fmr = new WOptionList();
            fmr.DataContext = context;
            if (fmr.ShowDialog().Equals(true))
            {
                plato.Valor = Convert.ToDouble(context.SelectedItem);
                calculoOk = true;
            }
        }

        /// <summary>
        /// Método el cual actualiza los valores. (Esto para que se vean reflejados los valores en pantalla.
        /// </summary>
        private void actualizarValores()
        {
            turn_allow = turn_allow;
            bore_allow = bore_allow;
            joint = joint;
            nick = nick;
            nick_draf = nick_draf;
            nick_depth = nick_depth;
            side_relief = side_relief;
            cam = cam;
            cam_roll = cam_roll;
            cam_lever = cam_lever;
            rise_built = rise_built;
            diseno = diseno;
            patt_width = patt_width;
            codigo = codigo;
            fin_Dia = fin_Dia;
            cstg_sm_od = cstg_sm_od;
            shrink_allow = shrink_allow;
            patt_sm_od = patt_sm_od;
            patt_thickness = patt_thickness;
            patt_sm_id = patt_sm_id;
            OD = OD;
            ID = ID;
            diff = diff;
            peso_cstg = peso_cstg;
            B_Dia = B_Dia;
            plato = plato;
            detalle = detalle;
            mounting = mounting;
            on_14_rd_gate = on_14_rd_gate;
            M_Circle = M_Circle;
            button = button;
            cone = cone;
        }

        public byte[] FileToByteArray(string fileName)
        {
            try
            {
                return File.ReadAllBytes(fileName);
            }
            catch (Exception e)
            {
                return null;
            }
        }
        /// <summary>
        /// Método que guarda una placa modelo.
        /// </summary>
        private async void guardarPattern()
        {
            ////Declaramos un objeto de tipo DialogService.
            //DialogService dialog = new DialogService();

            ////Ejecutamos el método para insertar el pattern.
            //string codigoNuevo = DataManager.SetPattern(new Pattern{ Codigo = codigo.Valor});

            ////Comparamos si es distinto de nulo o vacío, si es así indica que se guardó con exito la placa modelo.
            //if (!string.IsNullOrEmpty(codigoNuevo))

            //    //Mostramos el mensaje de confirmación con el nuevo código registrado.
            //    await dialog.SendMessage("RGP: Confirmación", "Placa modelo registrada con el código: " + codigoNuevo);
            //else

            //    //Mostramos
            //    await dialog.SendMessage("RGP: Alerta", "Oh, Oh, parece ser que algo salió mal.");

            DialogService dialog = new DialogService();
            int test = 1;
            if (test != 0)
            {
                await dialog.SendMessage("RGP: Confirmación", "Placa modelo registrada con el código: " + test);
            }
            else
            {
                await dialog.SendMessage("RGP: Alerta", "Oh, Oh, parece ser que algo salió mal.");
            }
        } 
        #endregion
    }
}