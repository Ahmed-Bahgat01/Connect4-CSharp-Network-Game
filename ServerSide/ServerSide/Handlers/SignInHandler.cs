using ServerSide.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerSide.Handlers
{
    internal class SignInHandler:IMessageHandler
    {
        public object Msg { get; set; }
        //SignInHandler() { }
        public SignInHandler(object message)
        {
            Msg = message;
        }
        public void Handle(object message)
        {
            var definition = new { UserName = "", Password = "" };
            //Resources.SignInMsg;
            
        }
    }
}
