using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Interfaces
{
    public interface ISubjectDiametro
    {
        void Notify(int index);

        void Subscribe(IObserverDiametro observer);

        void Subscribe(IObserverDiametro observer, double DiametroFinal);

        void Unsubscribe(IObserverDiametro observer);
    }
}
