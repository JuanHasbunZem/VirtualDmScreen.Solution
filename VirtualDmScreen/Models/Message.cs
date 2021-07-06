using System;

namespace VirtualDmScreen.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public int PlayerId { get; set; }
        public string Text { get; set; }
        public DateTime DateTimeStamp { get; set; }
        public virtual Player Player { get; set; } 
    }
}