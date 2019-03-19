using System.Collections.Generic;
using Model.Interfaces;

namespace Model
{
    public class SubjectWidth : ISubjectWidth
    {
        #region Propiedades
        /// <summary>
        /// 
        /// </summary>
        public List<IObserverWidth> Observers;
        #endregion

        #region Constructores
        /// <summary>
        /// 
        /// </summary>
        public SubjectWidth()
        {
            Observers = new List<IObserverWidth>();
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
                Observers[index].UpdateState(this, Observers[index - 1].MatRemoverWidth, Observers[index - 1].WidthOperacion);
                index += 1;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        public void Subscribe(IObserverWidth observer)
        {
            Observers.Add(observer);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        /// <param name="WidthFinal"></param>
        public void Subscribe(IObserverWidth observer, double WidthFinal)
        {
            observer.WidthOperacion = WidthFinal;
            Observers.Add(observer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        /// <param name="WidthFinal"></param>
        //public void Subscribe(IObserverWidth observer, double WidthFinal, int cortes)
        //{
        //    observer.WidthOperacion = WidthFinal;
        //    observer.CortesOPasadas = cortes;

        //    //Total de material a remover.
        //    observer.MatRemoverWidth = observer.MatRemoverWidth * cortes;

        //    Observers.Add(observer);
        //}

        //public void Subcribe(IObserverWidth observer, double WidthFinal, PasoNISSEI paso)
        //{
        //    observer.WidthOperacion = WidthFinal;
        //    observer.CortesOPasadas = paso.Cortes.Length;

        //    observer.PasoNISSEI = paso;

        //    double totalMatRemover = 0.0;
        //    for (int i = 0; i < paso.Cortes.Length; i++)
        //    {
        //        totalMatRemover += paso.Cortes[i].MatRemover;
        //    }

        //    Observers.Add(observer);
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        /// <param name="index"></param>
        public void Subscribe(IObserverWidth observer, int index)
        {
            Observers.Insert(index, observer);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="observer"></param>
        public void Unsubscribe(IObserverWidth observer)
        {
            Observers.Remove(observer);
        }

        #endregion
    }
}
