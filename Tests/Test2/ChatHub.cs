using System;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using Test2.Pocos;
using System.Collections.Concurrent;

namespace Test2
{
    public class ChatHub : Hub
    {
        private const string QsUserNameKey = "userName";

        // repository for users. obviously, in the real world\, we would use a more durable data store.
        private static ConcurrentDictionary<string, ChatUser> _users = new ConcurrentDictionary<string, ChatUser>();

        private void SendMessage(ChatMessage message, bool excludeCaller, bool prependUserName)
        {
            if (excludeCaller) {
                Clients.AllExcept(Context.ConnectionId).BroadcastMessage(message, prependUserName);
            } else {
                Clients.All.BroadcastMessage(message, prependUserName);
            }
        }

        /// <summary>
        ///  server method to be invoked by clients to send a message
        /// </summary>
        /// <param name="message"></param>
        /// <param name="excludeCaller"></param>
        /// <param name="prependUserName"></param>
        public void SendChatMessage(string message, bool excludeCaller, bool prependUserName)
        {
            ChatUser user;
            _users.TryGetValue(Context.ConnectionId, out user);
            var ChatMessage = new ChatMessage
            {
                SessionID = user.SessionId,
                UserName = user.UserName,
                Message = message
            };
            SendMessage(ChatMessage, excludeCaller, prependUserName);            
        }

 
        public override Task OnConnected()
        {
            var userName = Context.QueryString[QsUserNameKey];
            _users.TryAdd(Context.ConnectionId, CreateUser());
            SendChatMessage($"Online Host: {userName} has entered", false, false);
            return base.OnConnected();
        }

        public override Task OnReconnected()
        {
            var userName = Context.QueryString[QsUserNameKey];
            ChatUser user;
            
            // recreate user if not in the collection (necessary if the server has been recompiled).
            if (!_users.TryGetValue(Context.ConnectionId, out user)) { 
                _users.TryAdd(Context.ConnectionId, CreateUser());
            }
            return base.OnReconnected();
        }

        /// <summary>
        /// creates new POCO instance for user info
        /// </summary>
        /// <returns></returns>
        private ChatUser CreateUser()
        {
            return new ChatUser { UserName = Context.QueryString[QsUserNameKey], SessionId = Context.ConnectionId };
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            ChatUser removedUser;
            var userName = Context.QueryString[QsUserNameKey];            
            SendChatMessage($"Online Host: {userName} has left", false, false);
            _users.TryRemove(Context.ConnectionId, out removedUser);
            return base.OnDisconnected(stopCalled);
        }

    }
}