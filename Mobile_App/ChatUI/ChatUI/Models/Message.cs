using System;

namespace ChatUI.Models
{
    public class Message
    {
        public string Text { get; set; }
        public string User { get; set; }

        public Message(string user = null, string text = null)
        {
            if (string.IsNullOrEmpty(user))
                user = Guid.NewGuid().ToString();

            if (string.IsNullOrEmpty(text))
                text = "BimBim BamBam";

            Text = text;
            User = user;
        }
    }
}
