using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example1
{
    public class OutlookMailcs : IMailSender
    {
        public void Send(string toAddress, string subject)
        {
            Console.WriteLine("Outlook mail to [{0}] with subject [{1}]", toAddress, subject);
        }
    }
}
