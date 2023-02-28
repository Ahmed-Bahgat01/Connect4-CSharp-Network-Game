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

    public abstract class MessageContainer
    {
        public MessageTag Tag { get; set; }

        private MessageContainer() { }
        public MessageContainer(MessageTag tag)
        {
            Tag = tag;
        }

        public string ToJSON()
        {
            string jsonString = JsonConvert.SerializeObject(this);
            return jsonString;
        }


        public static MessageContainer? GetObject(string jsonMessage)
        {
            MessageContainer? ReturnMsg = JsonConvert.DeserializeObject<MessageContainer>(jsonMessage);
            return ReturnMsg;
        }
       
    }

    public class SignInMessageContainer : MessageContainer
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public SignInMessageContainer(MessageTag tag,string userName, string password):base(tag)
        {
            UserName = userName;
            Password = password;
        }
    }
}