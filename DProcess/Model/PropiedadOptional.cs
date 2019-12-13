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
        public ObservableCollection<FO_Item> ListaOpcional { get; set; }

        public string lblTitle { get; set; }

        public FO_Item ElementSelected { get; set; }
        
        public string TipoPerfil { get; set; }

        public PropiedadOptional()
        {
            ListaOpcional = new ObservableCollection<FO_Item>();
        }
    }
}
