using Newtonsoft.Json;
using ServerSide;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;

namespace MessageLib
{

    /// <summary>
    ///     this enum defines all message types
    ///     if you want to construct a new message you need to define its tag in this enum
    /// </summary>
    public enum MessageTag
    {
        None = 0,
        SignIn,
        SignUp,
        SignUpResponse,
        SignInResponse,
        CreateRoom,
        JoinRoom,
        StartGame,
        OpenRoomForJoinedPlayer,
        SpectateRoom,
        OtherPlayerMove,

        DisFromRoom,
        RoomStatusUpdate,
        LeaveRoom,
        SendReady
        // define your new message tag here
    }
    public enum ResponseCode
    {
        Success = 200,
        Failed = 500,
    }

    /// <summary>
    ///     this class is the parent class of all message containers 
    /// </summary>
    public class MessageContainer
    {
        public MessageTag Tag { get; set; }

        public MessageContainer(MessageTag tag)
        {
            Tag = tag;
        }

        /// <summary>
        ///     this method serializes the object of MessageContainer type (converts object to json string)
        ///     json string needs to be deserialized in the target to construct the object again
        /// </summary>
        /// <returns>
        ///     json string of the object
        /// </returns>
        public string ToJSON()
        {
            string jsonString = JsonConvert.SerializeObject(this);
            return jsonString;
        }

    }


    // CHILDEREN
    public class SignInMessageContainer : MessageContainer
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public SignInMessageContainer(string userName, string password):base(MessageTag.SignIn)
        {
            UserName = userName;
            Password = password;
        }

    }

    public class SignUpMessageContainer : MessageContainer
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public SignUpMessageContainer(string userName, string password):base(MessageTag.SignUp)
        {
            UserName = userName;
            Password = password;
        }
    }

    public class SignUpResponseMessageContainer : MessageContainer
    {
        public ResponseCode SignUpResponseCode { get; set; }
        public string ToPlayerResponseMessage { get; set; }
        public string ToPlayerMsgBoxTitle { get; set; }

        public SignUpResponseMessageContainer
            (ResponseCode signUpResponseCode, 
            string toPlayerResponseMessage, 
            string toPlayerMsgBoxTitle
            ): base(MessageTag.SignUpResponse)
        {
            SignUpResponseCode = signUpResponseCode;
            ToPlayerResponseMessage = toPlayerResponseMessage;
            ToPlayerMsgBoxTitle = toPlayerMsgBoxTitle;
        }
    }
    
    public class SignInResponseMessageContainer : MessageContainer
    {
        public ResponseCode SignInResponseCode { get; set; }
        public string ToPlayerResponseMessage { get; set; }
        public string ToPlayerMsgBoxTitle { get; set; }

        public SignInResponseMessageContainer
            (ResponseCode signInResponseCode, 
            string toPlayerResponseMessage, 
            string toPlayerMsgBoxTitle) : base(MessageTag.SignInResponse)
        {
            SignInResponseCode = signInResponseCode;
            ToPlayerResponseMessage = toPlayerResponseMessage;
            ToPlayerMsgBoxTitle = toPlayerMsgBoxTitle;
        }
    }

    /// <summary>
    ///     sent from client to server when client create new room (has room configuration)
    /// </summary>
    internal class CreateRoomMessageContainer : MessageContainer
    {
        public string UserName { get; set; }
        public string RoomName { get; set; }
        public GameConfiguration GameConfig { get; set; }
        public CreateRoomMessageContainer(string userName, string roomName, GameConfiguration gameConfig) : base(MessageTag.CreateRoom)
        {
            UserName = userName;
            RoomName = roomName;
            GameConfig = gameConfig;
        }
    }

    public class JoinRoomMessageContainer : MessageContainer
    {
        //public int PlayerId { get; set; }
        public string PlayerName { get; set; }
        public int RoomID { get; set; }

        public JoinRoomMessageContainer(string playerName, int roomID) : base(MessageTag.JoinRoom)
        {
            //PlayerId = playerId;
            PlayerName = playerName;
            RoomID = roomID;
        }
    }

    public class LeaveRoomMessageContainer : MessageContainer
    {
        public string PlayerName { get; set; }
        public int RoomID { get; set; }

        public LeaveRoomMessageContainer(string playerName, int roomID) : base(MessageTag.LeaveRoom)
        {
            PlayerName = playerName;
            RoomID = roomID;
        }
    }

    public class SpectateRoomMessageContainer : MessageContainer
    {
        public string UserName { get; set; }
        public int RoomID { get; set; }

        public SpectateRoomMessageContainer(string userName, int roomID) : base(MessageTag.SpectateRoom)
        {
            UserName = userName;
            RoomID = roomID;
        }
    }

    public class DisFromRoomMessageContainer : MessageContainer
    {
        public string UserName { get; set; }
        public int RoomID { get; set; }

        public DisFromRoomMessageContainer(string userName, int roomID) : base(MessageTag.SpectateRoom)
        {
            UserName = userName;
            RoomID = roomID;
        }
    }

    public class OpenRoomForJoinedPlayerMessageContainer : MessageContainer
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Player1Id { get; set; }
        public string Player1Name { get; set; }
        public int? Player2Id { get; set; }
        public string Player2Name { get; set; }

        public OpenRoomForJoinedPlayerMessageContainer
            (int roomId,
            string roomName,
            string player1Name,
            string player2Name = null
            ) : base(MessageTag.OpenRoomForJoinedPlayer)
        {
            RoomId = roomId;
            RoomName = roomName;
            Player1Name = player1Name;
            Player2Name = player2Name;
        }
    }

    public class CreateRoomV2MessageContainer : MessageContainer
    {
        public int RoomId { get; set; }
        public string RoomName { get; set; }
        public int Player1Id { get; set; }
        public string Player1Name { get; set; }
        public int? Player2Id { get; set; }
        public string Player2Name { get; set; }


        // one player constructor
        public CreateRoomV2MessageContainer
            (int roomId,
            string roomName,
            int player1Id,
            string player1Name
            ) : base(MessageTag.RoomStatusUpdate)
        {
            RoomId = roomId;
            RoomName = roomName;
            Player1Id = player1Id;
            Player1Name = player1Name;
            Player2Id = null;
            Player2Name = null;
        }

        // 2 players constructor
        /*public CreateRoomV2MessageContainer
            (int roomId,
            string roomName,
            //int player1Id,
            string player1Name,
            //int player2Id,
            string player2Name
            ) : base(MessageTag.RoomStatusUpdate)
        {
            RoomId = roomId;
            RoomName = roomName;
            //Player1Id = player1Id;
            Player1Name = player1Name;
            //Player2Id = player2Id;
            Player2Name = player2Name;
        }*/

    }

    public class OtherPlayerMoveMessageContainer : MessageContainer
    {           
        public int ColNum { get; set; }
        public OtherPlayerMoveMessageContainer(int colNum) : base(MessageTag.OtherPlayerMove)
        {
            ColNum = colNum;
        }
    }

    public class SendReadyContainer : MessageContainer
    {

        public int RoomID;

        public SendReadyContainer(int roomId) : base(MessageTag.SendReady)
        {
            RoomID = roomId;
        }

    }
    public class StartGameContainer : MessageContainer
    {
        //public GameConfiguration GameConfig { get; set; }
        public int size;
        public Color color;
        public StartGameContainer(int s,Color c) : base(MessageTag.StartGame)
        {
            size= s;
            color= c;
        }
    }

}