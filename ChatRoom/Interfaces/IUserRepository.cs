using System.Threading.Tasks;
using ChatRoom.Models;

namespace ChatRoom.Interfaces
{
    public interface IUserRepository
    {
        Task AddUser(UserModel model);

        bool Contains(UserModel model);
    }
}
