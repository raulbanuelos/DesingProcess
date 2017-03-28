﻿using Model;
using System.ComponentModel;

namespace View.Services.ViewModel
{
    public class PropiedadCadenaViewModel : INotifyPropertyChanged
    {
        #region Atributos
        private PropiedadCadena model;
        #endregion

        #region Propiedades del modelo
        /// <summary>
        /// Cadena que representa el nombre de la propiedad.
        /// </summary>
        public string Nombre {
            get {
                return model.Nombre;
            }
            set {
                model.Nombre = value;
                NotifyChange("Nombre");
            }
        }

        /// <summary>
        /// Cadena que representa el valor de la propiedad.
        /// </summary>
        public string Valor {
            get {
                return model.Valor;
            }
            set {
                model.Valor = value;
                NotifyChange("Valor");
            }
        }

        /// <summary>
        /// Cadena que representa una descripción corta de la propiedad.
        /// </summary>
        public string DescripcionCorta {
            get {
                return model.DescripcionCorta;
            }
            set {
                model.DescripcionCorta = value;
                NotifyChange("DescripcionCorta");
            }
        }

        /// <summary>
        /// Cadena que representa una descripción larga de la propiedad.
        /// </summary>
        public string DescripcionLarga {
            get {
                return model.DescripcionLarga;
            }
            set {
                model.DescripcionLarga = value;
            }
        }

        /// <summary>
        /// Arreglo de bytes que representa la imagen de la propiedad.
        /// </summary>
        public byte[] Imagen {
            get {
                return model.Imagen;
            }
            set {
                model.Imagen = value;
                NotifyChange("Imagen");
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
