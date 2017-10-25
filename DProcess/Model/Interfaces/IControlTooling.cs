using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
   public interface IControlTooling
    {
        int Guardar(string codigo);
        bool ValidaError();
        void Inicializa();
        bool ValidaRangos();
        void InicializaCampos(string codigoHerramental);
        int Update();
        int Delete();
    }
}
