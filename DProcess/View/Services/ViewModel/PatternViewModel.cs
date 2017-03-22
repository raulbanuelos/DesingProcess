using System.ComponentModel;
using Model;

namespace View.Services.ViewModel
{
    public class PatternViewModel : INotifyPropertyChanged
    {
        #region Propiedades Pattern
        private string _codigo;
        public string codigo
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

        private double _medida;
        public double medida
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

        private double _diametro;
        public double diametro
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

        private int _mountig;
        public int mounting
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

        private string _ON_14_RD_GATE;
        public string on_14_rd_gate
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

        private string _button;
        public string button
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

        private string _cone;
        public string cone
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

        private string _Mcircle;
        public string M_Circle
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

        private double _ring_min;
        public double ring_w_min
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

        private double _ring_max;
        public double ring_w_max
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

        private string _date_ordered;
        public string date_ordered
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
        private double _b_dia;
        public double B_Dia
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
        private double _fin_dia;
        public double fin_Dia
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
        private double _turn_allow;
        public double turn_allow
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
        private double _cstg_SM_OD;
        public double cstg_sm_od
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
        private double _shrink_allow;
        public double shrink_allow
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
        private double _patt_SM_OD;
        public double patt_sm_od
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

        private double _piece_in_patt;
        public double piece_in_patt
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

        private double _bore_allow;
        public double bore_allow
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
        private double _patt_sm_id;
        public double patt_sm_id
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

        private double _patt_thickness;
        public double patt_thickness
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

        private string _joint;
        public string joint
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

        private string _nick;
        public string nick
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

        private string _Nick_draf;
        public string nick_draf
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

        private string _Nick_depth;
        public string nick_depth
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

        private string _side_relief;
        public string side_relief
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

        private int _cam;
        public int cam
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

        private double _cam_roll;
        public double cam_roll
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

        private double _rise;
        public double rise
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

        private double _OD;
        public double OD
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

        private double _ID;
        public double ID
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

        private double _diff;
        public double diff
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

        private string _tipo;
        public string tipo
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

        private string _mounted;
        public string mounted
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

        private string _ordered;
        public string ordered
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

        private string _checked;
        public string Checked
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

        private string _date_checked;
        public string date_checked
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

        private string _esp_inst;
        public string esp_inst
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
        private double _factor_K;
        public double factor_k
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

        private double _rise_built;
        public double rise_built
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

        private double _ring_TH_MIN;
        public double ring_th_min
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

        private double _ring_TH_max;
        public double ring_th_max
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
        private bool _estado;
        public bool estado
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

        private double _plato;
        public double plato
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

        private string _detalle;
        public string detalle
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

        private bool _diseno;
        public bool diseno
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

