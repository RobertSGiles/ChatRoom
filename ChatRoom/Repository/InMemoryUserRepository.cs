using System.Collections.Generic;
using System.Threading.Tasks;
using ChatRoom.Interfaces;
using ChatRoom.Models;

namespace ChatRoom.Repository
{
    public class InMemoryUserRepository : IUserRepository
    {
        private Dictionary<string, UserModel> dictionaryOfUsers = new Dictionary<string, UserModel>();

        public async Task AddUser(UserModel model)
        {
            dictionaryOfUsers.Add(model.UserName, model);
        }

        public bool Contains(UserModel model)
        {
            return dictionaryOfUsers.ContainsKey(model.UserName);
        }
    }
}
