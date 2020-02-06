using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class PropiedadOptional
    {
        public int idPropiedadOpcional { get; set; }

        public ObservableCollection<FO_Item> ListaOpcional { get; set; }

        public string lblTitle { get; set; }

        public FO_Item ElementSelected { get; set; }
        
        public string TipoPerfil { get; set; }

        public string Nombre { get; set; }

        public PropiedadOptional()
        {
            ListaOpcional = new ObservableCollection<FO_Item>();
        }

        /// <summary>
        /// 1: Tabla, 2:Lista opciones
        /// </summary>
        public int Source { get; set; }
    }
}
