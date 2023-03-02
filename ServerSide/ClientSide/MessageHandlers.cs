using MessageLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSide
{
    /// <summary>
    ///     this class has contains all message handlers
    /// </summary>
    internal class MessageHandlers
    {

        // HANDLERS
        public static void SignUpResponseHandler(string recievedMessage)
        {
            SignUpResponseMessageContainer ResponseObj;
            ResponseObj = JsonConvert.DeserializeObject<SignUpResponseMessageContainer>(recievedMessage);

            string msg = ResponseObj.ToPlayerResponseMessage;
            string title = ResponseObj.ToPlayerMsgBoxTitle;
            if (ResponseObj.SignUpResponseCode == ResponseCode.Success)
                MessageBox.Show(msg, title,MessageBoxButtons.OK,MessageBoxIcon.Information);
            else if(ResponseObj.SignUpResponseCode == ResponseCode.Failed)
                MessageBox.Show(msg, title,MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
        }

        public static void SignInResponseHandler(string recievedMessage)
        {
            SignInResponseMessageContainer ResponseObj;
            ResponseObj = JsonConvert.DeserializeObject<SignInResponseMessageContainer>(recievedMessage);
            string msg = ResponseObj.ToPlayerResponseMessage;
            string title = ResponseObj.ToPlayerMsgBoxTitle;
            if (ResponseObj.SignInResponseCode == ResponseCode.Success)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
            else if (ResponseObj.SignInResponseCode == ResponseCode.Failed)
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
    }
}
