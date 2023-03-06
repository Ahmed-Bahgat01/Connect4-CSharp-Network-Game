using MessageLib;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
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
        public static readonly string ResultsFilePath = "Results.txt";
        //public event Action<object, string> RespondToPlayer;


        // HANDLERS
        public void SignInHandler(object sender, string recievedMessage)
        {
            SignInMessageContainer SignInObj;
            SignInObj = JsonConvert.DeserializeObject<SignInMessageContainer>(recievedMessage);
            //MessageBox.Show($"from sign in handler: username={SignInObj.UserName} ,password={SignInObj.Password}");
            _players.Last()._userName = SignInObj.UserName;

            

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
            // send signin response to player
            SignInResponseMessageContainer response;
            if (ValidCredential)
            {
                if (PlayerSuccessfullSignInEvent != null)                                      //fires event when player is Connect
                {
                    PlayerSuccessfullSignInEvent(this, _players.Last()._userName);
                }
                response = new SignInResponseMessageContainer(ResponseCode.Success, "Signed in Successfully", "Success");
            }
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
                response = new SignUpResponseMessageContainer(ResponseCode.Success, "Signed Up Successfully", "Success");
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

            foreach (var player in _players)                        //search for the caller in players list
            {
                if(player._userName== CreateRoomObj.UserName)       //when found
                {
                    Player p = player;
                    Room newRoom = new Room(p, id, CreateRoomObj.RoomName, CreateRoomObj.GameConfig);
                    _rooms.Add(newRoom);
                    newRoom._roomIsEmptyEvent += RoomIsEmptyEventHandler;

                    
                    
                    if (_RoomCreatedEvent != null)          //raising RoomCreatedEvent to append in the form
                    {
                        _RoomCreatedEvent(newRoom);
                    }
                    
                    CreateRoomV2MessageContainer msg = new CreateRoomV2MessageContainer(newRoom._ID, newRoom._name, newRoom._players[0]._id, newRoom._players[0]._userName);
                    Broadcast(msg);                                 //open the room form for the player

                    // TODO: resend all rooms to user
                    
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
                if (player._userName == JoinRoomObj.PlayerName)
                {
                    Player p = player;
                    
                    foreach(var room in _rooms)
                    {
                        if(room._ID == JoinRoomObj.RoomID)
                        {
                            room.AddPlayer(p);

                            ////open the room form to player
                            OpenRoomForJoinedPlayerMessageContainer msg = new OpenRoomForJoinedPlayerMessageContainer(room._ID, room._name, room._players[0]._userName, p._userName);
                            p.SendMsg(msg);

                            ////add the player to all opened room form
                            

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

        public void SendReadyHandler(object sender, string recievedMessage)
        {
            SendReadyContainer SpecRoomObj;
            SpecRoomObj = JsonConvert.DeserializeObject<SendReadyContainer>(recievedMessage);

            foreach (var room in _rooms)
            {
                if (room._ID == SpecRoomObj.RoomID)
                {
                    room._readyList.Add(true);
                   // MessageBox.Show(room._readyList[0].ToString());
                    if(room._readyList.Count == 2)
                    {
                        //send start game
                        //MessageBox.Show("start game");

                        Color creatorPlayerColor = room._gameConfig._boardColor;
                        Color joinedPlayerColor = room.GetSecondPlayerColor();
                        StartGameContainer msg = new StartGameContainer(room._gameConfig._boardSize, creatorPlayerColor);
                        room._players[0].SendMsg(msg);
                        msg = new StartGameContainer(room._gameConfig._boardSize, joinedPlayerColor);
                        room._players[1].SendMsg(msg);
                        //foreach (var p in room._players)
                        //{
                        //    //MessageBox.Show("start game");
                        //    StartGameContainer msg = new StartGameContainer(room._gameConfig._boardSize, room._gameConfig._boardColor);
                        //    p.SendMsg(msg);
                        //    //MessageBox.Show(msg.size.ToString());

                        //}

                    }
                    break;
                }
            }



        }
        public void PlayerMoveHandler(object sender, string recievedMessage)
        {
            Player senderPlayer = sender as Player;

            // search for room
            Room targetedRoom = null;
            foreach (Room room in _rooms)
            {
                foreach (Player roomPlayer in room._players)
                {
                    if(senderPlayer._id == roomPlayer._id) // found room and player
                    {
                        targetedRoom = room;
                    }
                    break;
                }
                if (targetedRoom != null)
                    break;
            }

            // send move to other player on room 
            if (targetedRoom != null)
            {
                // deserialize message
                OtherPlayerMoveMessageContainer recievedObj;
                recievedObj = JsonConvert.DeserializeObject<OtherPlayerMoveMessageContainer>(recievedMessage);
                OtherPlayerMoveMessageContainer msg = new OtherPlayerMoveMessageContainer(recievedObj.ColNum, recievedObj.IsWinningMove);

                // TODO: BEFORE SENDING TO OTHER PLAYERS CHECK IF WINNING MOVE (RECORD RESULT HERE)
                if (recievedObj.IsWinningMove)
                {
                    senderPlayer._score += 1;
                    // formated date
                    DateTime localDate = DateTime.Now;
                    string dateText = localDate.ToString(new CultureInfo("en-GB"));
                    Player player1 = targetedRoom._players[0];
                    Player player2 = targetedRoom._players[1];
                    string resultRecord = $"{player1._userName} \"{player1._score}\", {player2._userName} \"{player2._score}\" ,{dateText}";
                    File.AppendAllText(ResultsFilePath, resultRecord + Environment.NewLine);
                    // save game score

                    //DateTime LocalDate;
                    //string DesktopPath;
                    //string Pth;
                    //string CurrentDate;

                    //private static string getFormattedCurrentDate()
                    //{
                    //    LocalDate = DateTime.Now;
                    //    return LocalDate.ToString(new CultureInfo("en-GB"));
                    //}

                    //private static void SaveGameScore(Player HostPlayer, Player GuestPlayer, Room CurrentRoom)
                    //{
                    //    DesktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    //    Pth = Path.Combine(DesktopPath, "GameScore.txt");
                    //    string currentDate = getFormattedCurrentDate();
                    //    string gameScore = $"{HostPlayer.Name}: {CurrentRoom.RoomPlayers[0].Score}\t{GuestPlayer.Name}: {CurrentRoom.RoomPlayers[1].Score}\t Date: {CurrentDate} \n";
                    //    File.AppendAllText(Pth, gameScore);
                    //}
                }


                // send move to players and watchers(player in spectate status)
                foreach (Player roomPlayer in targetedRoom._players)
                {
                    if(roomPlayer._userName != senderPlayer._userName)
                        roomPlayer.SendMsg(msg);
                }
            }
        }
    }
}
