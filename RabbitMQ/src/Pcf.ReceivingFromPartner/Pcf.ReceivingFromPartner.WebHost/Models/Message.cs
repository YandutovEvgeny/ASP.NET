using System;

namespace Pcf.ReceivingFromPartner.WebHost.Models
{
    public class Message
    {
        public Guid Id { get; set; }

        public Message(Guid id)
        {
            Id = id;
        }
    }
}