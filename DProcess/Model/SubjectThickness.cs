using System.Collections.Generic;
using Model.Interfaces;

namespace Model
{
    public class SubjectThickness : ISubjectThickness
    {
        #region Propiedades
        /// <summary>
        /// 
        /// </summary>
        public List<IObserverThickness> Observers;
        #endregion

        #region Constructores

        /// <summary>
        /// 
        /// </summary>
        public SubjectThickness()
        {
            Observers = new List<IObserverThickness>();
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
                Observers[index].UpdateState(this, Observers[index - 1].MatRemoverThickness, Observers[index - 1].Thickness);
                index += 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        public void Unsubscribe(IObserverThickness observer)
        {
            Observers.Remove(observer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        public void Subscribe(IObserverThickness observer)
        {
            Observers.Add(observer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        /// <param name="ThicknessFinal"></param>
        public void Subscribe(IObserverThickness observer, double ThicknessFinal)
        {
            observer.Thickness = ThicknessFinal;
            Observers.Add(observer);
        }
        #endregion
    }
}
