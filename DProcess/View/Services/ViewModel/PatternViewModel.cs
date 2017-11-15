using System.ComponentModel;
using Model;
using System.Windows.Input;
using System;
using Model.ControlDocumentos;
using DataAccess.ServiceObjects.ControlDocumentos;
using System.IO;

namespace View.Services.ViewModel
{
    public class PatternViewModel : INotifyPropertyChanged
    {
        #region Atributos
        private Pattern model;
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
            model.codigo = new PropiedadCadena();
            model.codigo.Valor = "Nuevo Codigo";

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