using System;
using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Models
{
    public class MessageModel
    {
        [Required]
        public string Content { get; set; }

        public string Username { get; set; }

        public string DateTimeSent { get; set; }

        public Guid Id { get; set; } = Guid.NewGuid();
    }
}
