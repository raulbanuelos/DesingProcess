using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Ninject.Modules;
using Ninject;

namespace Example1
{
    public class Bindings : NinjectModule
    {
        public readonly string fromSend;
        public Bindings(string fromSend)
        {
            this.fromSend = fromSend;
        }

        public override void Load()
        {
            if (fromSend == "Mock")
            {
                Bind<IMailSender>().To<MockMailSender>();
            }else if(fromSend == "Outlook"){
                Bind<IMailSender>().To<OutlookMailcs>();
            }
            else
            {
                Bind<IMailSender>().To<MockMailSender>();
            }
        }
    }
}
