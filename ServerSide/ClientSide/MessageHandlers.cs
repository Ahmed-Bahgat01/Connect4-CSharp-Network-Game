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
    ///     this class contains all message handlers
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

        // TODO:

        public static void RoomStatusUpdate(string recievedMessage)
        {
            RoomStatusUpdateMessageContainer RoomStatus;
            RoomStatus = JsonConvert.DeserializeObject<RoomStatusUpdateMessageContainer>(recievedMessage);
            // check if room exists
            if (Client.RoomPanelDic.ContainsKey(RoomStatus.RoomId))
            {
                //TODO:
                // if exist update it's data

            }
            else  // if not exist create the room
            {
                
                // TODO: create UI for room
            }


        }
        
    }
}
