using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1
{
    public interface IMailSender
    {
        void Send(string toAddress, string subject);
    }
}
