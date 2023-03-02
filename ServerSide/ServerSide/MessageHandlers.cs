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
    internal class MessageHandlers
    {
        public static readonly string AccountsFilePath = "Accounts.json";

        // HANDLERS
        public static void SignInHandler(object sender, string recievedMessage)
        {
            SignInMessageContainer SignInObj;
            SignInObj = JsonConvert.DeserializeObject<SignInMessageContainer>(recievedMessage);
            MessageBox.Show($"from sign in handler: username={SignInObj.UserName} ,password={SignInObj.Password}");
        }

        public static void SignUpHandler(object sender, string recievedMessage)
        {
            SignUpMessageContainer SignUpObj;
            SignUpObj = JsonConvert.DeserializeObject<SignUpMessageContainer>(recievedMessage);

            // search if username already exists
            // checking if file exists
            bool ExistsUserName = false; 
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

            if (! ExistsUserName)
                File.AppendAllText(AccountsFilePath, recievedMessage);
            else
            {
                // TODO: SEND MESSAGE TO PLAYER THAT USERNAME IS NOT VALID
                // raise event of invalid signUp and make server sends message to user of invalid signup
            }

        }
    }
}
