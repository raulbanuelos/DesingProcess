using Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace View.Services.ViewModel
{
    public class MateriaPrimaViewModel : INotifyPropertyChanged
    {
        #region Attributes
        public MateriaPrima model;
        #endregion

        #region Propiedades de Materia Prima
        
        /// <summary>
        /// Propiedad que representa la especificación de la materia prima.
        /// </summary>
        /// <example>
        /// SPR-128,MF012-S,etc.
        /// </example>
        public PropiedadCadena Especificacion
        {
            get
            {
                return model.Especificacion;
            }
            set
            {
                model.Especificacion = value;
                TipoDeMaterial = DataManager.GetTipoMaterial(model.Especificacion.Valor);

                NotifyChange("Especificacion");
                NotifyChange("TipoDeMaterial");
                NotifyChange("TextoPresentacion");
            }
        }

        /// <summary>
        /// Cadena que representa un texto para identificar que tipo de material es.
        /// </summary>
        public string TextoPresentacion {
            get {
                return "MATERIAL: " + TipoDeMaterial;
            }
        }

        /// <summary>
        /// Cadena que representa el tipo de material.
        /// </summary>
        /// <example>
        /// HIERRO GRIS, ACERO AL CARBON, ETC.
        /// </example>
        public string TipoDeMaterial {
            get {
                return model.TipoDeMaterial;
            }
            set {
                model.TipoDeMaterial = value;
                NotifyChange("TipoDeMaterial");
            }
        }

        /// <summary>
        /// Lista de propiedades que contiene la materia prima.
        /// </summary>
        public ObservableCollection<Propiedad> Propiedades {
            get {
                return model.Propiedades;
            }
            set {
                model.Propiedades = value;
                NotifyChange("Propiedades");
            }
        }

        /// <summary>
        /// Double que representa la cantidad utilizable en cada operación.
        /// </summary>
        public double Cantidad {
            get {
                return model.Cantidad;
            }
            set {
                model.Cantidad = value;
                NotifyChange("Cantidad");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string Measurement {
            get {
                return model.Measurement;
            }
            set {
                model.Measurement = value;
                NotifyChange("Measurement");
            }
        }
        #endregion

        #region Events INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region INotifyPropertyChanged Métodos
        void NotifyChange(params string[] ids)
        {
            if (PropertyChanged != null)
                foreach (var id in ids)
                    PropertyChanged(this, new PropertyChangedEventArgs(id));
        }
        #endregion
    }
}
