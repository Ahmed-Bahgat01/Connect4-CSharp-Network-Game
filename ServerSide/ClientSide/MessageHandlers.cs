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

            if (ResponseObj.SignUpResponseCode == ResponseCode.Success)
            {
                // TODO: SHOW FORM OF AVAILABLE PLAYERS AND ROOMS HERE
            }
            else if(ResponseObj.SignUpResponseCode == ResponseCode.Failed)
            {
                string msg = ResponseObj.ToPlayerResponseMessage;
                string title = ResponseObj.ToPlayerMsgBoxTitle;
                MessageBox.Show(msg, title,MessageBoxButtons.OK,MessageBoxIcon.Information);
            }
        }
    }
}
