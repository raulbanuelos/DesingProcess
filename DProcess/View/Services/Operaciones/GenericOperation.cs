using ControlzEx.Standard;
using Model.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services.Operaciones
{
    public class GenericOperation
    {

        /// <summary>
        /// Propiedad que indica si la clase contiene la interfaz IObservableDiametro.
        /// </summary>
        public bool IsObservableDiametro
        {
            get
            {
                return GetObservableDiameter();
            } 
        }

        /// <summary>
        /// Propiedad que indica si la clase contiene la interfaz IObservableWidth.
        /// </summary>
        public bool IsObservableWidth
        {
            get
            {
                return GetObservableWidth();
            }
        }

        /// <summary>
        /// Propiedad que indica si la clase contiene la interfaz IObservableThickness.
        /// </summary>
        public bool IsObservableThickness
        {
            get
            {
                return GetObservableThickness();
            }
        }

        public bool GetObservableDiameter()
        {
            return this is IObserverDiametro ? true : false;
        }

        public bool GetObservableWidth()
        {
            return this is IObserverWidth ? true : false;
        }

        public bool GetObservableThickness()
        {
            return this is IObserverThickness ? true : false;
        }
    }
}
