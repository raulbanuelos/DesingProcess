using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IObserverThickness
    {
        /// <summary>
		/// Double que representa la medida del thickness del anillo.
		/// </summary>
		double Thickness { get; set; }

        /// <summary>
        /// Double que representa el material a remover en la operación.
        /// Si se agrega material (por ejemplo en cromo) el valor será negativo.
        /// </summary>
        double MatRemoverThickness { get; set; }

        /// <summary>
        /// Booleano que representa si la operación trabaja en diámetro exterior. Si es false trabaja en diámetro interior.
        /// </summary>
        bool TrabajaOD { get; set; }

        /// <summary>
        /// Método que calcula el thickness de la operación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="MaterialRemoverAfterOperacion"></param>
        /// <param name="ThicknessAfterOperacion"></param>
        void UpdateState(ISubjectThickness sender, double MaterialRemoverAfterOperacion, double ThicknessAfterOperacion);
    }
}
