using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface ISubjectWidth
    {
        void Notify(int index);

        void Subscribe(IObserverWidth observer);

        void Subscribe(IObserverWidth observer, double WidthFinal);

        void Subscribe(IObserverWidth observer, int index);

        void Unsubscribe(IObserverWidth observer);
    }
}
