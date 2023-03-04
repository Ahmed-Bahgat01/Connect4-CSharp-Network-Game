using Newtonsoft.Json;
using System.Collections.Generic;


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
        JoinRoom,
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

    public class JoinRoomMessageContainer : MessageContainer
    {
        public ResponseCode JoinResponseCode { get; set; }
        //public string ToPlayerResponseMessage { get; set; }
        //public string ToPlayerMsgBoxTitle { get; set; }
        public string RequestJoinMessage { get; set; }

        public JoinRoomMessageContainer
            (ResponseCode joinResponseCode, string requestJoinMessage) : base(MessageTag.JoinRoom)
        {
            JoinResponseCode = joinResponseCode;
            //ToPlayerResponseMessage = toPlayerResponseMessage;
            //ToPlayerMsgBoxTitle = toPlayerMsgBoxTitle;
            RequestJoinMessage = requestJoinMessage;
        }
    }

}