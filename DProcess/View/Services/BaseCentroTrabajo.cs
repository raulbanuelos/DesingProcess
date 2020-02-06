using Model;
using Model.ControlDocumentos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace View.Services
{
    public class BaseCentroTrabajo
    {
        public string GetNombre(string CentroTrabajo)
        {
            string nombre;
            nombre = DataManagerControlDocumentos.GetNombreOperacion(CentroTrabajo).ToString();
            return nombre;
        }
    }
}
