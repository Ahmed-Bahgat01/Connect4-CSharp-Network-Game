using MessageLib;
using Newtonsoft.Json;
using ServerSide.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide.Handlers
{
    internal class SignInHandler:IMessageHandler
    {
        private SignInMessageContainer SignInObj;
        public RecievedMessageEventData RecievedMessage { get; set; }
        public object Sender { get; set; }
        public SignInHandler(object sender, RecievedMessageEventData recievedMessage)
        {
            RecievedMessage = recievedMessage;
            Sender = sender;
            SignInObj = JsonConvert.DeserializeObject<SignInMessageContainer>(recievedMessage._msg);
        }
        public void Handle(object message)
        {

            MessageBox.Show($"from sign in handler: username={SignInObj.UserName} ,password={SignInObj.Password}");

        }
    }
}
