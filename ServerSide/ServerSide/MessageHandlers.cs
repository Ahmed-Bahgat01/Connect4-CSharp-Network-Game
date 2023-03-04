using MessageLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ServerSide
{

    /// <summary>
    ///     this class has contains all message handlers
    /// </summary>
    internal partial class Server 
    {
        // signup records file path
        public static readonly string AccountsFilePath = "Accounts.json";
        //public event Action<object, string> RespondToPlayer;


        // HANDLERS
        public void SignInHandler(object sender, string recievedMessage)
        {
            SignInMessageContainer SignInObj;
            SignInObj = JsonConvert.DeserializeObject<SignInMessageContainer>(recievedMessage);
            //MessageBox.Show($"from sign in handler: username={SignInObj.UserName} ,password={SignInObj.Password}");
            _players.Last()._userName = SignInObj.UserName;

            if (_playerConnectedEvent != null)                                      //fires event when player is Connect
            {
                _playerConnectedEvent(this, _players.Last()._userName);
            }

            // search if valid username and password
            // checking if file exists
            bool ValidCredential = false;
            if (File.Exists(AccountsFilePath))
            {
                SignInMessageContainer ExistingAccount;
                foreach (string line in System.IO.File.ReadLines(AccountsFilePath))
                {
                    ExistingAccount = JsonConvert.DeserializeObject<SignInMessageContainer>(line);
                    // checking creadential
                    if (ExistingAccount.UserName == SignInObj.UserName && 
                        ExistingAccount.Password == SignInObj.Password)
                    {
                        ValidCredential = true; break;
                    }
                }
            }
            SignInResponseMessageContainer response;
            if (ValidCredential)
                response = new SignInResponseMessageContainer(ResponseCode.Success, "Signed up Successfully", "Success");
            else
                response = new SignInResponseMessageContainer(ResponseCode.Failed, "Invalid Credential, try again", "Failed");
            // sending response to player
            (sender as Player)._session.SendMessage(response);
            //MessageBox.Show($"from sign in handler: username={SignInObj.UserName} ,password={SignInObj.Password}");
            

        }

        public static void SignUpHandler(object sender, string recievedMessage)
        {
            SignUpMessageContainer SignUpObj;
            SignUpObj = JsonConvert.DeserializeObject<SignUpMessageContainer>(recievedMessage);

            
            bool ExistsUserName = false;
            // search if username already exists
            // checking if file exists
            if (File.Exists(AccountsFilePath))
            {
                SignUpMessageContainer ExistingAccount;
                foreach (string line in System.IO.File.ReadLines(AccountsFilePath))
                {
                    ExistingAccount = JsonConvert.DeserializeObject<SignUpMessageContainer>(line);
                    if(ExistingAccount.UserName == SignUpObj.UserName)
                    {
                        ExistsUserName = true; break;
                    }
                }
            }
            SignUpResponseMessageContainer response;
            if (!ExistsUserName)
            {
                File.AppendAllText(AccountsFilePath, recievedMessage + Environment.NewLine);
                // sending message to player that sign up is successfull
                response = new SignUpResponseMessageContainer(ResponseCode.Success, "Signed up Successfully", "Success");
            }
            else
            {
                response = new SignUpResponseMessageContainer
                    (ResponseCode.Failed, "user name already exists try another one", "Failed To Signup");
            }
            (sender as Player)._session.SendMessage(response);

        }

        public void CreateRoomHandler(object sender, string recievedMessage)
        {
            CreateRoomMessageContainer CreateRoomObj;
            CreateRoomObj = JsonConvert.DeserializeObject<CreateRoomMessageContainer>(recievedMessage);
            int id;
            
            id = _rooms.Count() + 1;
            
            

            foreach (var player in _players)
            {
                if(player._userName== CreateRoomObj.UserName)
                {
                    Player p = player;
                    Room newRoom = new Room(p, id, CreateRoomObj.RoomName, CreateRoomObj.GameConfig);
                    _rooms.Add(newRoom);
                    newRoom._roomIsEmptyEvent += RoomIsEmptyEventHandler;
                    //newRoom._RoomCreatedEvent += RoomCreatedEventHandler;
                    if (_RoomCreatedEvent != null)          //raising RoomCreatedEvent
                    {
                        _RoomCreatedEvent(newRoom);
                    }
                    // TODO: send rooms to user
                    newRoom._RoomUpdateEvent += RoomUpdateEventHandler;
                    break;
                }
            }
        }

        public void JoinRoomHandler(object sender, string recievedMessage)
        {
            JoinRoomMessageContainer JoinRoomObj;
            JoinRoomObj = JsonConvert.DeserializeObject<JoinRoomMessageContainer>(recievedMessage);

            foreach (var player in _players)
            {
                if (player._userName == JoinRoomObj.UserName)
                {
                    Player p = player;
                    
                    foreach(var room in _rooms)
                    {
                        if(room._ID == JoinRoomObj.RoomID)
                        {
                            room.AddPlayer(p);
                            break;
                        }
                    }
                    
                    break;
                }
            }
        }

        public void SpectateRoomHandler(object sender, string recievedMessage)
        {
            SpectateRoomMessageContainer SpecRoomObj;
            SpecRoomObj = JsonConvert.DeserializeObject<SpectateRoomMessageContainer>(recievedMessage);

            foreach (var player in _players)
            {
                if (player._userName == SpecRoomObj.UserName)
                {
                    Player p = player;
                    foreach (var room in _rooms)
                    {
                        if (room._ID == SpecRoomObj.RoomID)
                        {
                            room.AddSpectator(p);
                            break;
                        }
                    }

                    break;
                }
            }
        }

        public void DisFromRoomHandler(object sender, string recievedMessage)
        {
            SpectateRoomMessageContainer SpecRoomObj;
            SpecRoomObj = JsonConvert.DeserializeObject<SpectateRoomMessageContainer>(recievedMessage);

            foreach (var player in _players)
            {
                if (player._userName == SpecRoomObj.UserName)
                {
                    Player p = player;
                    foreach (var room in _rooms)
                    {
                        if (room._ID == SpecRoomObj.RoomID)
                        {
                            room.RemovePlayer(p);
                            break;
                        }
                    }

                    break;
                }
            }
        }
    }
}
