using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatRoom.Interfaces;
using ChatRoom.Models;

namespace ChatRoom.Repository
{
    public class InMemoryMessageRepository : IMessageRepository
    {
        private List<MessageModel> messages = new List<MessageModel>();
        private Dictionary<Guid, int> positionHashMap = new Dictionary<Guid, int>();
        private readonly int _pageSize = 3;

        public async Task AddMessage(MessageModel model)
        {
            messages.Add(model);
            positionHashMap[model.Id] = messages.Count - 1;
        }

        public IEnumerable<MessageModel> GetMessagesSince(Guid? since = null)
        {
            var currentPageSize = _pageSize;
            if (messages.Count <= _pageSize)
            {
                currentPageSize = messages.Count;
            }

            if (messages.Count == 0)
            {
                return new List<MessageModel>();
            }

            if (since == null)
            {
                return messages.Skip(Math.Max(0, messages.Count() - currentPageSize));
            }

            var position = positionHashMap[since.Value];

            return messages.Skip(position + 1).Take(Math.Min(currentPageSize, messages.Count() - position));
        }
    }
}