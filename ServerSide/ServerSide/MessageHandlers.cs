using MessageLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide
{
    internal class MessageHandlers
    {
        public static void SignInHandler(object sender, string recievedMessage)
        {
            SignInMessageContainer SignInObj;
            SignInObj = JsonConvert.DeserializeObject<SignInMessageContainer>(recievedMessage);
            MessageBox.Show($"from sign in handler: username={SignInObj.UserName} ,password={SignInObj.Password}");
        }
    }
}
