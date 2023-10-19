using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Multiplex.Business.Interfaces
{
    public interface IEmailSender
    {
        void SendEmail(string to, string subject, string body);
    }
}
