using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace OnePlugin
{
    class Message
    {
        JObject messageObject;

        public Message(string message)
        {
            messageObject = new JObject
            {
                { "message", message }
            };
        }

        public string GetBody()
        {
            return messageObject.SelectToken("message").ToObject<string>();
        }
    }
}
