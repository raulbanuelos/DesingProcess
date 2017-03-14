using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface ISubjectThickness
    {
        void Notify(int index);

        void Subscribe(IObserverThickness observer);

        void Subscribe(IObserverThickness observer, double ThicknessFinal);

        void Unsubscribe(IObserverThickness observer);
    }
}
