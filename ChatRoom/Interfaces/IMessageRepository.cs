using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ChatRoom.Models;

namespace ChatRoom.Interfaces
{
    public interface IMessageRepository
    {
        Task AddMessage(MessageModel model);

        IEnumerable<MessageModel> GetMessagesSince(Guid? since = null);
    }
}
