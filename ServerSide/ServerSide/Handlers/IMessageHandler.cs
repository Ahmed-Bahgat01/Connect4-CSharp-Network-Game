using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Handlers
{
    internal interface IMessageHandler
    {
        void Handle(object message);
    }
}
