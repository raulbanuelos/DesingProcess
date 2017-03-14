using System.Collections.Generic;
using Model.Interfaces;

namespace Model
{
    public class SubjectDiametro : ISubjectDiametro
    {
        #region Propiedades
        public List<IObserverDiametro> Observers { get; set; }
        #endregion

        #region Constructores
        public SubjectDiametro()
        {
            Observers = new List<IObserverDiametro>();
        }
        #endregion

        #region Métodos

        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        public void Notify(int index)
        {
            while (index <= Observers.Count - 1)
            {
                Observers[index].UpdateState(this, Observers[index - 1].MatRemoverDiametro, Observers[index - 1].Diameter, Observers[index - 1].Gap, Observers[index - 1].RemueveGap);
                index += 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        public void Unsubscribe(IObserverDiametro observer)
        {
            Observers.Add(observer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        public void Subscribe(IObserverDiametro observer)
        {
            Observers.Add(observer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        /// <param name="DiametroFinal"></param>
        public void Subscribe(IObserverDiametro observer, double DiametroFinal)
        {
            observer.Diameter = DiametroFinal;
            Observers.Add(observer);
        }
        #endregion
    }
}
