using System;
using System.Collections.Generic;
using Test2Client.Pocos;
using Microsoft.AspNet.SignalR.Client;
using System.Threading;
using System.Configuration;
using System.ComponentModel;

namespace Test2Client
{
    class Program
    {
        private static HubConnection _connection;
        private static  IHubProxy _chatHub;  
        private static string _userName;
        private static string chatMessage;
        private static Random _random;
        private static BackgroundWorker _worker;

        static void Main(string[] args)
        {
            _worker = new BackgroundWorker();
            _random = new Random();

            NamePrompt();

           ConnectToServer();

           MessagePrompt();
        }

        /// <summary>
        /// SignalR client method to be invoked by the chat hub
        /// </summary>
        /// <param name="message"></param>
        public static void BroadcastMessage(ChatMessage message, bool prependUserName)
        {
            var userNameToPrepend = prependUserName ? $"{message.UserName}:" : "";
            Console.WriteLine($"{userNameToPrepend}{message.Message}");            
        }

        #region backgroundworker handlers
        /// <summary>
        /// event handler for the background worker to ping the chat hub at a random interval
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (_connection.State == ConnectionState.Connected)
            {
                int arg = (int)e.Argument;
                Thread.Sleep(_random.Next(15, 21) * 1000);
                PingServer((BackgroundWorker)sender);
                e.Result = 0;
            }
        }

        /// <summary>
        /// event handler for the background worker to kick off another ping to the hub
        /// after the current ping has completed - thus creating an infinite loop
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private static void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            _worker.RunWorkerAsync(1);
            MessagePrompt();
        }
        #endregion

        #region private methods
        /// <summary>
        /// sends a chat message and recycles the messsage prompt
        /// </summary>
        /// <param name="message"></param>
        /// <param name="excludeSelf"></param>
        private static void SendChatMessageWithMessagePrompt(string message)
        {
            SendChatMessage(chatMessage, true);
            MessagePrompt();
        }

        /// <summary>
        /// recycles the chat message without recycling the message prompt.
        /// this was needed because the message prompt was causing the background worker
        /// to wait indefinitely.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="excludeSelf"></param>
        private static void SendChatMessage(string message, bool excludeSelf)
        {
            _chatHub.Invoke("SendChatMessage", message, excludeSelf, true);
        }

        private static void ConnectToServer()
        {
            try
            {
                // send user name to server
                var queryString = new Dictionary<string, string>();
                queryString.Add("userName", _userName);

                _connection = new HubConnection(ConfigurationManager.AppSettings["ChatServerUrl"], queryString);
                _chatHub = _connection.CreateHubProxy("ChatHub");

                //wire up message broadcasting code to proxy
                _chatHub.On<ChatMessage, bool>("broadcastMessage", (message, prependUserName) => BroadcastMessage(message, prependUserName));

                _connection.Start().Wait();
                if (_connection.State == ConnectionState.Connected)
                {
                    // wire up hub connection event handlers
                    _connection.Closed += () => { Console.WriteLine("Online host: Chat could not reconnect"); NamePromptWithConnectToServer(); };
                    _connection.Reconnected += () => { Console.WriteLine("Online host: Chat reconnected"); };
                    _connection.Reconnecting += () => { Console.WriteLine("Online host: Chat disconnected"); };

                    // wire up background worker event handlers
                    _worker.DoWork += new DoWorkEventHandler(BackgroundWorker_DoWork);
                    _worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(BackgroundWorker_RunWorkerCompleted);

                    // begin background worker processing
                    _worker.RunWorkerAsync(1);
                    return;
                }
                Console.WriteLine("connection could not be opened");
                NamePromptWithConnectToServer();
            }
            catch (Exception)
            {
                Console.WriteLine("error occurred while opening connection");
                NamePromptWithConnectToServer();
            }
        }

        private static void NamePromptWithConnectToServer()
        {
            NamePrompt();
            ConnectToServer();
        }
        /// <summary>
        /// used by the background worker to ping the server
        /// </summary>
        /// <param name="worker"></param>
        /// <returns></returns>
        private static int PingServer(BackgroundWorker worker)
        {
            SendChatMessage($"Hello at {DateTime.Now}", false);
            return 0;
        }

        /// <summary>
        /// prompt for user name until an entry is made
        /// </summary>
        private static void NamePrompt()
        {
            _userName = null;
            do
            {
                Console.WriteLine("Please Enter Your Name");
                _userName = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(_userName))
                {
                    break;
                }
            } while (true);
        }

        /// <summary>
        /// prompts the user to enter a chat message and sends it
        /// </summary>
        private static void MessagePrompt()
        {
            do
            {
                chatMessage = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(chatMessage))
                {
                    SendChatMessageWithMessagePrompt(chatMessage);
                    chatMessage = null;
                    break;
                }
            } while (true);
        } 
        #endregion

        ~Program()
        {
            // not the usual place to dispose but it seemd the most expeditious, given the situation.
            // these classes may dispose from their own finalizers, but just in case they don't.....
            _worker.Dispose();
            _connection.Dispose();
        }
    }
}
