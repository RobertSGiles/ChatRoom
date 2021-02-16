using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChatRoom.Models;
using ChatRoom.Repository;

namespace ChatRoom.Tests
{
    [TestClass]
    public class InMemoryUserepositoryTests
    {
        private readonly InMemoryUserRepository _inMemoryUserRepository = new InMemoryUserRepository();
        private readonly UserModel user = new UserModel { UserName = "Chat" };
        private readonly UserModel differentUser = new UserModel { UserName = "Room" };

        [TestMethod]
        public void ShouldReturnTrue_WhenUserNameExists()
        {
            _inMemoryUserRepository.AddUser(user);
            Assert.IsTrue(_inMemoryUserRepository.Contains(user));
        }

        [TestMethod]
        public void ShouldReturnFalse_WhenUserNameDoesNotExists()
        {
            _inMemoryUserRepository.AddUser(user);
            Assert.IsFalse(_inMemoryUserRepository.Contains(differentUser));
        }
    }
}
