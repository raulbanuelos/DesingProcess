using System.Collections.ObjectModel;

namespace Model.Interfaces
{
    public interface IObserverWidth
    {
        /// <summary>
		/// Double que representa la medida del width del anillo en la operación.
		/// </summary>
		double WidthOperacion { get; set; }

        /// <summary>
        /// Double que representa el material a remover en la operación.
        /// Si en la operación se agrega material(por ejemplo cromo lateral) el valor será negativo.
        /// </summary>
        double MatRemoverWidth { get; set; }

        /// <summary>
        /// Método que calcula el width de la operación.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="MaterialRemoverAfterOperacion"></param>
        /// <param name="WidthAfterOperacion"></param>
        void UpdateState(ISubjectWidth sender, double MaterialRemoverAfterOperacion, double WidthAfterOperacion);

        /// <summary>
        /// Método que establece la cantidad de material a remover/agregar en la operación.
        /// </summary>
        /// <param name="operaciones"></param>
        /// <param name="posOperacion"></param>
        void setMaterialRemover(ObservableCollection<IOperacion> operaciones, int posOperacion, Anillo planoAnillo);
    }
}
