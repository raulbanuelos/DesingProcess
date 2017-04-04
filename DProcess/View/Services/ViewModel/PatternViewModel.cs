using System.ComponentModel;
using Model;
using System.Windows.Input;
using System;
using Model.ControlDocumentos;
using DataAccess.ServiceObjects.Herramentales;
using DataAccess.ServiceObjects.ControlDocumentos;

namespace View.Services.ViewModel
{
    public class PatternViewModel : INotifyPropertyChanged
    {
        #region Atributos
        private Pattern model; 
        #endregion

        #region Propiedades Pattern

        private PropiedadCadenaViewModel _codigo;
        public PropiedadCadenaViewModel codigo
        {
            get
            {
                return _codigo;
            }
            set
            {
                _codigo = value;
                NotifyChange("codigo");
            }
        }

        PropiedadViewModel _medida;
        public PropiedadViewModel medida
        {
            get
            {
                return _medida;
            }
            set
            {
                _medida = value;
                NotifyChange("medida");
            }
        }

        private PropiedadViewModel _diametro;
        public PropiedadViewModel diametro
        {
            get
            {
                return _diametro;
            }
            set
            {
                _diametro = value;
                NotifyChange("diametro");
            }
        }

        private Cliente _customer;
        public Cliente customer
        {
            get
            {
                return _customer;
            }
            set
            {
                _customer = value;
                NotifyChange("customer");
            }
        }

        private PropiedadViewModel _mountig;
        public PropiedadViewModel mounting
        {
            get
            {
                return _mountig;
            }
            set
            {
                _mountig = value;
                NotifyChange("mounting");
            }
        }

        private PropiedadCadenaViewModel _ON_14_RD_GATE;
        public PropiedadCadenaViewModel on_14_rd_gate
        {
            get
            {
                return _ON_14_RD_GATE;
            }
            set
            {
                _ON_14_RD_GATE = value;
                NotifyChange("on_14_rd_gate");
            }
        }

        private PropiedadCadenaViewModel _button;
        public PropiedadCadenaViewModel button
        {
            get
            {
                return _button;
            }
            set
            {
                _button = value;
                NotifyChange("button");
            }
        }

        private PropiedadCadenaViewModel _cone;
        public PropiedadCadenaViewModel cone
        {
            get
            {
                return _cone;
            }
            set
            {
                _cone = value;
                NotifyChange("cone");
            }
        }

        private PropiedadCadenaViewModel _Mcircle;
        public PropiedadCadenaViewModel M_Circle
        {
            get
            {
                return _Mcircle;
            }
            set
            {
                _Mcircle = value;
                NotifyChange("M_Circle");
            }
        }

        private PropiedadViewModel _ring_min;
        public PropiedadViewModel ring_w_min
        {
            get
            {
                return _ring_min;
            }
            set
            {
                _ring_min = value;
                NotifyChange("ring_w_min");
            }
        }

        private PropiedadViewModel _ring_max;
        public PropiedadViewModel ring_w_max
        {
            get
            {
                return _ring_max;
            }
            set
            {
                _ring_max = value;
                NotifyChange("ring_w_max");
            }
        }

        private PropiedadCadenaViewModel _date_ordered;
        public PropiedadCadenaViewModel date_ordered
        {
            get
            {
                return _date_ordered;
            }
            set
            {
                _date_ordered = value;
                NotifyChange("date_ordered");
            }
        }

        private PropiedadViewModel _b_dia;
        public PropiedadViewModel B_Dia
        {
            get
            {
                return _b_dia;
            }
            set
            {
                _b_dia = value;
                NotifyChange("B_dia");
            }
        }

        private PropiedadViewModel _fin_dia;
        public PropiedadViewModel fin_Dia
        {
            get
            {
                return _fin_dia;
            }
            set
            {
                _fin_dia = value;
                NotifyChange("fin_dia");
            }
        }

        private PropiedadViewModel _turn_allow;
        public PropiedadViewModel turn_allow
        {
            get
            {
                return _turn_allow;
            }
            set
            {
                _turn_allow = value;
                NotifyChange("turn_allow");
            }
        }

        private PropiedadViewModel _cstg_SM_OD;
        public PropiedadViewModel cstg_sm_od
        {
            get
            {
                return _cstg_SM_OD;
            }
            set
            {
                _cstg_SM_OD = value;
                NotifyChange("cstg_sm_od");
            }
        }

        private PropiedadViewModel _shrink_allow;
        public PropiedadViewModel shrink_allow
        {
            get
            {
                return _shrink_allow;
            }
            set
            {
                _shrink_allow = value;
                NotifyChange("shrink_allow");
            }
        }

        private PropiedadViewModel _patt_SM_OD;
        public PropiedadViewModel patt_sm_od
        {
            get
            {
                return _patt_SM_OD;
            }
            set
            {
                _patt_SM_OD = value;
                NotifyChange("patt_sm_od");
            }
        }

        private PropiedadViewModel _piece_in_patt;
        public PropiedadViewModel piece_in_patt
        {
            get
            {
                return _piece_in_patt;
            }
            set
            {
                _piece_in_patt = value;
                NotifyChange("piece_in_patt");
            }
        }

        private PropiedadViewModel _bore_allow;
        public PropiedadViewModel bore_allow
        {
            get
            {
                return _bore_allow;
            }
            set
            {
                _bore_allow = value;
                NotifyChange("bore_allow");
            }
        }

        private PropiedadViewModel _patt_sm_id;
        public PropiedadViewModel patt_sm_id
        {
            get
            {
                return _patt_sm_id;
            }
            set
            {
                _patt_sm_id = value;
                NotifyChange("patt_sm_id");
            }
        }

        private PropiedadViewModel _patt_thickness;
        public PropiedadViewModel patt_thickness
        {
            get
            {
                return _patt_thickness;
            }
            set
            {
                _patt_thickness = value;
                NotifyChange("patt_thickness");
            }
        }

        private PropiedadCadenaViewModel _joint;
        public PropiedadCadenaViewModel joint
        {
            get
            {
                return _joint;
            }
            set
            {
                _joint = value;
                NotifyChange("joint");
            }
        }

        private PropiedadCadenaViewModel _nick;
        public PropiedadCadenaViewModel nick
        {
            get
            {
                return _nick;
            }
            set
            {
                _nick = value;
                NotifyChange("nick");
            }
        }

        private PropiedadCadenaViewModel _Nick_draf;
        public PropiedadCadenaViewModel nick_draf
        {
            get
            {
                return _Nick_draf;
            }
            set
            {
                _Nick_draf = value;
                NotifyChange("nick_draf");
            }
        }

        private PropiedadCadenaViewModel _Nick_depth;
        public PropiedadCadenaViewModel nick_depth
        {
            get
            {
                return _Nick_depth;
            }
            set
            {
                _Nick_depth = value;
                NotifyChange("nick_depth");
            }
        }

        private PropiedadCadenaViewModel _side_relief;
        public PropiedadCadenaViewModel side_relief
        {
            get
            {
                return _side_relief;
            }
            set
            {
                _side_relief = value;
                NotifyChange("side_relief");
            }
        }

        private PropiedadViewModel _cam;
        public PropiedadViewModel cam
        {
            get
            {
                return _cam;
            }
            set
            {
                _cam = value;
                NotifyChange("cam");
            }
        }

        private PropiedadViewModel _cam_roll;
        public PropiedadViewModel cam_roll
        {
            get
            {
                return _cam_roll;
            }
            set
            {
                _cam_roll = value;
                NotifyChange("cam_roll");
            }
        }

        private PropiedadViewModel _rise;
        public PropiedadViewModel rise
        {
            get
            {
                return _rise;
            }
            set
            {
                _rise = value;
                NotifyChange("rise");
            }
        }

        private PropiedadViewModel _OD;
        public PropiedadViewModel OD
        {
            get
            {
                return _OD;
            }
            set
            {
                _OD = value;
                NotifyChange("OD");
            }
        }

        private PropiedadViewModel _ID;
        public PropiedadViewModel ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
                NotifyChange("ID");
            }
        }

        private PropiedadViewModel _diff;
        public PropiedadViewModel diff
        {
            get
            {
                return _diff;
            }
            set
            {
                _diff = value;
                NotifyChange("diff");
            }
        }

        private PropiedadCadenaViewModel _tipo;
        public PropiedadCadenaViewModel tipo
        {
            get
            {
                return _tipo;
            }
            set
            {
                _tipo = value;
                NotifyChange("tipo");
            }
        }

        private PropiedadCadenaViewModel _mounted;
        public PropiedadCadenaViewModel mounted
        {
            get
            {
                return _mounted;
            }
            set
            {
                _mounted = value;
                NotifyChange("mounted");
            }
        }

        private PropiedadCadenaViewModel _ordered;
        public PropiedadCadenaViewModel ordered
        {
            get
            {
                return _ordered;
            }
            set
            {
                _ordered = value;
                NotifyChange("ordered");
            }
        }

        private PropiedadCadenaViewModel _checked;
        public PropiedadCadenaViewModel Checked
        {
            get
            {
                return _checked;
            }
            set
            {
                _checked = value;
                NotifyChange("Checked");
            }
        }

        private PropiedadCadenaViewModel _date_checked;
        public PropiedadCadenaViewModel date_checked
        {
            get
            {
                return _date_checked;
            }
            set
            {
                _date_checked = value;
                NotifyChange("date_checked");
            }
        }

        private PropiedadCadenaViewModel _esp_inst;
        public PropiedadCadenaViewModel esp_inst
        {
            get
            {
                return _esp_inst;
            }
            set
            {
                _esp_inst = value;
                NotifyChange("esp_inst");
            }
        }

        private PropiedadViewModel _factor_K;
        public PropiedadViewModel factor_k
        {
            get
            {
                return _factor_K;
            }
            set
            {
                _factor_K = value;
                NotifyChange("factor_k");
            }
        }

        private PropiedadViewModel _rise_built;
        public PropiedadViewModel rise_built
        {
            get
            {
                return _rise_built;
            }
            set
            {
                _rise_built = value;
                NotifyChange("rise_built");
            }
        }

        private PropiedadViewModel _ring_TH_MIN;
        public PropiedadViewModel ring_th_min
        {
            get
            {
                return _ring_TH_MIN;
            }
            set
            {
                _ring_TH_MIN = value;
                NotifyChange("ring_th_min");
            }
        }

        private PropiedadViewModel _ring_TH_max;
        public PropiedadViewModel ring_th_max
        {
            get
            {
                return _ring_TH_max;
            }
            set
            {
                _ring_TH_max = value;
                NotifyChange("ring_th_max");
            }
        }

        private PropiedadBoolViewModel _estado;
        public PropiedadBoolViewModel estado
        {
            get
            {
                return _estado;
            }
            set
            {
                _estado = value;
                NotifyChange("estado");
            }
        }

        private PropiedadViewModel _plato;
        public PropiedadViewModel plato
        {
            get
            {
                return _plato;
            }
            set
            {
                _plato = value;
                NotifyChange("plato");
            }
        }

        private PropiedadCadenaViewModel _detalle;
        public PropiedadCadenaViewModel detalle
        {
            get
            {
                return _detalle;
            }
            set
            {
                _detalle = value;
                NotifyChange("detalle");
            }
        }

        private PropiedadBoolViewModel _diseno;
        public PropiedadBoolViewModel diseno
        {
            get
            {
                return _diseno;
            }
            set
            {
                _diseno = value;
                NotifyChange("diseno");
            }
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
        public PatternViewModel()
        {
            codigo = new PropiedadCadenaViewModel();
            medida = new PropiedadViewModel("DPlacaModelo", "Diámetro", "Diámetro de la placa modelo", "Distance");
            diametro = new PropiedadViewModel("WPlacaModelo", "Width", "Width de la placa modelo", "Distance");
            customer = new Cliente();
            mounting = new PropiedadViewModel("MoutingPlacaModelo", "Mouting", "Número de impresiones de la placa modelo", "Cantidad");
            on_14_rd_gate = new PropiedadCadenaViewModel();
            button = new PropiedadCadenaViewModel();
            cone = new PropiedadCadenaViewModel();
            M_Circle = new PropiedadCadenaViewModel();
            ring_w_min = new PropiedadViewModel("RingWidthMinPlacaModelo", "Ring Width Min", "Width mínimo del anillo", "Distance");
            ring_w_max = new PropiedadViewModel("RingWidthMaxPlacaModelo", "Ring Width Max", "Width máximo del anillo", "Distance");
            date_ordered = new PropiedadCadenaViewModel();
            B_Dia = new PropiedadViewModel("BDiaPlacaModelo", "B Dia", "Diámetro B de la placa modelo", "Distance");
            fin_Dia = new PropiedadViewModel("FinDiaPlacaModelo", "Fin Dia", "", "Distance");
            turn_allow = new PropiedadViewModel("TurnAllowPlacaModelo", "Turn allow", "Material permitido a remover en el diámetro exterior", "Distance");
            cstg_sm_od = new PropiedadViewModel("CstgSModPlacaModelo", "cstg sm od", "", "Distance");
            shrink_allow = new PropiedadViewModel("ShrinkAllowPlacaModelo", "Shrink Allow", "", "Distance");
            patt_sm_od = new PropiedadViewModel("PattSMODPlacaModelo", "Patt SM OD", "", "Distance");
            piece_in_patt = new PropiedadViewModel("PieceInPattPlacaModelo", "Piece in patt", "", "Distance");
            bore_allow = new PropiedadViewModel("BoreAllowPlacaModelo", "Bore Allow", "Material permitido a remover en el diámetro interior", "Distance");
            patt_sm_id = new PropiedadViewModel("PattSMIDPlacaModelo", "Patt SM ID", "", "Distance");
            patt_thickness = new PropiedadViewModel("PattThicknessPlacaModelo", "Patt thickness", "", "Distance");
            joint = new PropiedadCadenaViewModel();
            nick = new PropiedadCadenaViewModel();
            nick_draf = new PropiedadCadenaViewModel();
            nick_depth = new PropiedadCadenaViewModel();
            side_relief = new PropiedadCadenaViewModel();
            cam = new PropiedadViewModel("CamPlacaModelo", "Cam", "", "Distance");
            cam_roll = new PropiedadViewModel("CamRollPlacaModelo", "Cam Roll", "", "Distance");
            rise = new PropiedadViewModel("RisePlacaModelo", "Rise", "", "Distance");
            OD = new PropiedadViewModel("ODPlacaModelo", "OD", "Diámetro exterior de la placa modelo", "Distance");
            ID = new PropiedadViewModel("IDPlacaModelo", "ID", "Diámetro interior de la placa modelo", "Distance");
            diff = new PropiedadViewModel("DiffPlacaModelo", "Diff", "Diferencia entre OD e ID", "Distance");
            //tipo = new PropiedadViewModel();
            mounted = new PropiedadCadenaViewModel();
            ordered = new PropiedadCadenaViewModel();
            Checked = new PropiedadCadenaViewModel();
            date_checked = new PropiedadCadenaViewModel();
            esp_inst = new PropiedadCadenaViewModel();
            factor_k = new PropiedadViewModel("FactorKPlacaModelo", "Factor K", "", "Distance");
            rise_built = new PropiedadViewModel("RiseBuiltPlacaModelo", "Rise built", "", "Distance");
            ring_th_min = new PropiedadViewModel("RingThicknessMinPlacaModelo", "Ring Th Min", "Thickness mínimo del anillo", "Distance");
            ring_th_max = new PropiedadViewModel("RingThicknessMaxPlacaModelo", "Ring Th Max", "Thickness máximo del anillo", "Distance");
            estado = new PropiedadBoolViewModel();
            plato = new PropiedadViewModel("PlatoPlacaModelo", "Plato", "Dimensión del plato de la placa modelo", "Distance");
            detalle = new PropiedadCadenaViewModel();
            diseno = new PropiedadBoolViewModel();
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
            TipoDocumento obj = new TipoDocumento();
            obj.id_tipo = 2;
            obj.tipo_documento = "test";
            obj.abreviatura = "tst";
            obj.fecha_actualizacion= Convert.ToDateTime("01/02/2017");
            obj.fecha_creacion= Convert.ToDateTime("01/02/2017");


            int test = DataManagerControlDocumentos.UpdateTipo(obj);

            if (test!=0)
            {
                await dialog.SendMessage("RGP: Confirmación", "Placa modelo registrada con el código: " + test);
            }else
            {
                await dialog.SendMessage("RGP: Alerta", "Oh, Oh, parece ser que algo salió mal.");
            }
        }

            
        #endregion
    }
}