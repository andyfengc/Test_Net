using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Email
{
    public interface IEmailManager
    {
        string Host { get; }
        int Port { get; }
        string UserName { get; }
        string Password { get; }
        void Send(Email email);
    }
}
