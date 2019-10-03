using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.ControlDocumentos
{
    public class DO_INTEGRANTES_GRUPO
    {
        public int idintegrantegrupo { get; set; }
        public int idgrupo { get; set; }
        public string idusuariointegrante { get; set; }
        public bool IsSelected { get; set; }
        public string nombrecompleto { get; set; }
    }

    /// <summary>
    /// Clase para comparar la clase DO_INTEGRANTES_GRUPO
    /// </summary>
    public class DO_INTEGRANTES_GRUPO_EqualityComparer : IEqualityComparer<DO_INTEGRANTES_GRUPO>
    {
        public bool Equals(DO_INTEGRANTES_GRUPO x, DO_INTEGRANTES_GRUPO y)
        {
            return x.idintegrantegrupo == y.idintegrantegrupo;
        }

        public int GetHashCode(DO_INTEGRANTES_GRUPO obj)
        {
            return obj.idintegrantegrupo.GetHashCode();
        }
    }
}
