namespace ServerSide
{
    public class RecievedMessageEventData
    {
        public string _msg { get; set; }

        public RecievedMessageEventData(string msg)
        {
            this._msg = msg;
        }
    }
}