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
    internal partial class Client
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

        /// <summary>
        ///     TO BE REMOVED
        /// </summary>
        /// <param name="recievedMessage"></param>
        public static void RoomStatusUpdate(string recievedMessage)
        {
            RoomStatusUpdateMessageContainer RoomStatus;
            RoomStatus = JsonConvert.DeserializeObject<RoomStatusUpdateMessageContainer>(recievedMessage);

            // raise event of room status update
            if (RoomUpdateEvent != null)      //firing event when room update notification for ui to handle
            {
                RoomUpdateEvent(RoomStatus);
            }
        }
        

        public static void CreateRoomHandler(string recievedMessage)
        {
            CreateRoomV2MessageContainer RoomStatus;
            RoomStatus = JsonConvert.DeserializeObject<CreateRoomV2MessageContainer>(recievedMessage);

            // raise event of room creation
            if (CreateRoomEvent != null)      //firing event when room create notification for ui to handle
            {
                CreateRoomEvent(RoomStatus);
            }
        }

        





    }
}
