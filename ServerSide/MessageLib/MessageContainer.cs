using Newtonsoft.Json;
using System.Collections.Generic;


namespace MessageLib
{
    public enum MessageTag
    {
        None = 0,
        SignIn,
        SignUp,
        Error,
        //EnterRoom,
        //LeaveRoom,
        //PlayGame,
    }

    public class MessageContainer
    {
        public MessageTag Tag { get; set; }

        protected MessageContainer() { }
        public MessageContainer(MessageTag tag)
        {
            Tag = tag;
        }

        public string ToJSON()
        {
            string jsonString = JsonConvert.SerializeObject(this);
            return jsonString;
        }


        //public static MessageContainer? GetObject(string jsonMessage)
        //{
        //    MessageContainer? ReturnMsg = JsonConvert.DeserializeObject<MessageContainer>(jsonMessage);
        //    return ReturnMsg;
        //}


        //public static T? GetObject<T>(string jsonMessage, T? retObject)
        //{
        //    retObject = JsonConvert.DeserializeObject<T>(jsonMessage);
        //    return retObject;
        //}

    }

    public class SignInMessageContainer : MessageContainer
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        //public string JsonMessage { get; set; }

        public SignInMessageContainer(string userName, string password):base(MessageTag.SignIn)
        {
            UserName = userName;
            Password = password;
        }

    }
}