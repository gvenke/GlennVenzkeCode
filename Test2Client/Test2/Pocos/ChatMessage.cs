using System;

namespace Test2Client.Pocos
{
    /// <summary>
    /// copy of the POCO by the same name in the server solution. The original plan was to
    /// import the server POCO via project reference. However, the code to wire up "broadcastMessage"
    /// to the hub proxy was preventing the VS debbugger from loading the type. Given time, I probably
    /// could've solved that problem. Because of the ticking clock, I came up with a more "hacky"
    /// solution of re-creating the class on the client. Since it gets serialized into json, 
    /// neither server nor client will know the difference
    /// </summary>
    public class ChatMessage
    {
        public string UserName { get; set; }
        public string SessionID { get; set; }
        public string Message { get; set; }
    }
}
