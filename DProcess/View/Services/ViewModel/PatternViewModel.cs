using System.ComponentModel;
using Model;

namespace View.Services.ViewModel
{
    public class PatternViewModel : INotifyPropertyChanged
    {
        #region Propiedades Pattern
        private PropiedadCadena _codigo;
        public PropiedadCadena codigo
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

        private Propiedad _medida;
        public Propiedad medida
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

        private Propiedad _diametro;
        public Propiedad diametro
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

        private Propiedad _mountig;
        public Propiedad mounting
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

        private PropiedadCadena _ON_14_RD_GATE;
        public PropiedadCadena on_14_rd_gate
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

        private PropiedadCadena _button;
        public PropiedadCadena button
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

        private PropiedadCadena _cone;
        public PropiedadCadena cone
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

        private PropiedadCadena _Mcircle;
        public PropiedadCadena M_Circle
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

        private Propiedad _ring_min;
        public Propiedad ring_w_min
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

        private Propiedad _ring_max;
        public Propiedad ring_w_max
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

        private PropiedadCadena _date_ordered;
        public PropiedadCadena date_ordered
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
        private Propiedad _b_dia;
        public Propiedad B_Dia
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
        private Propiedad _fin_dia;
        public Propiedad fin_Dia
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
        private Propiedad _turn_allow;
        public Propiedad turn_allow
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
        private Propiedad _cstg_SM_OD;
        public Propiedad cstg_sm_od
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
        private Propiedad _shrink_allow;
        public Propiedad shrink_allow
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
        private Propiedad _patt_SM_OD;
        public Propiedad patt_sm_od
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

        private Propiedad _piece_in_patt;
        public Propiedad piece_in_patt
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

        private Propiedad _bore_allow;
        public Propiedad bore_allow
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
        private Propiedad _patt_sm_id;
        public Propiedad patt_sm_id
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

        private Propiedad _patt_thickness;
        public Propiedad patt_thickness
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

        private PropiedadCadena _joint;
        public PropiedadCadena joint
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

        private PropiedadCadena _nick;
        public PropiedadCadena nick
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

        private PropiedadCadena _Nick_draf;
        public PropiedadCadena nick_draf
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

        private PropiedadCadena _Nick_depth;
        public PropiedadCadena nick_depth
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

        private PropiedadCadena _side_relief;
        public PropiedadCadena side_relief
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

        private Propiedad _cam;
        public Propiedad cam
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

        private Propiedad _cam_roll;
        public Propiedad cam_roll
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

        private Propiedad _rise;
        public Propiedad rise
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

        private Propiedad _OD;
        public Propiedad OD
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

        private Propiedad _ID;
        public Propiedad ID
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

        private Propiedad _diff;
        public Propiedad diff
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

        private PropiedadCadena _tipo;
        public PropiedadCadena tipo
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

        private PropiedadCadena _mounted;
        public PropiedadCadena mounted
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

        private PropiedadCadena _ordered;
        public PropiedadCadena ordered
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

        private PropiedadCadena _checked;
        public PropiedadCadena Checked
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

        private PropiedadCadena _date_checked;
        public PropiedadCadena date_checked
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

        private PropiedadCadena _esp_inst;
        public PropiedadCadena esp_inst
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
        private Propiedad _factor_K;
        public Propiedad factor_k
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

        private Propiedad _rise_built;
        public Propiedad rise_built
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

        private Propiedad _ring_TH_MIN;
        public Propiedad ring_th_min
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

        private Propiedad _ring_TH_max;
        public Propiedad ring_th_max
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
        private PropiedadBool _estado;
        public PropiedadBool estado
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

        private Propiedad _plato;
        public Propiedad plato
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

        private PropiedadCadena _detalle;
        public PropiedadCadena detalle
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

        private PropiedadBool _diseno;
        public PropiedadBool diseno
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
    }
}

