using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface IObserverDiametro
    {
        /// <summary>
		/// Double que representa la medida del diámetro del anillo.
		/// </summary>
		double Diameter { get; set; }

        /// <summary>
        /// Double que representa el material a remover en el diámetro del anillo en la operación.
        /// </summary>
        double MatRemoverDiametro { get; set; }

        /// <summary>
        /// Double que representa el gap del anillo en la operación.
        /// </summary>
        double Gap { get; set; }

        /// <summary>
        /// Booleano que indica si la operación remueve material en Gap.
        /// </summary>
        bool RemueveGap { get; set; }

        /// <summary>
        /// Metodo que calcula el diámetro de la operación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="MaterialRemoverAfterOperacion"></param>
        /// <param name="DiametroAfterOperacion"></param>
        /// <param name="GapAfterOperacion"></param>
        /// <param name="RemueveGap"></param>
        void UpdateState(ISubjectDiametro sender, double MaterialRemoverAfterOperacion, double DiametroAfterOperacion, double GapAfterOperacion, bool RemueveGap);
    }
}
