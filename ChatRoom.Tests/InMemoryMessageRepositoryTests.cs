using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChatRoom.Models;
using System;
using System.Collections.Generic;
using ChatRoom.Repository;
using System.Linq;

namespace ChatRoom.Tests
{
    [TestClass]
    public class InMemoryMessageRepositoryTests
    {
        private readonly InMemoryMessageRepository _inMemoryMessageRepository = new InMemoryMessageRepository();
        private const int pageSize = 3;
        private static readonly Guid FirstMessageId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019A");
        private static readonly Guid ForthMessageId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019D");
        private static readonly Guid LastMessageId = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019F");

        public static List<MessageModel> MessageModelList()
        {
            var messages = new List<MessageModel>();
            messages.Add(new MessageModel { Content = "first_message", Username = "username", Id = FirstMessageId, DateTimeSent = DateTime.UtcNow.AddHours(1).ToString("dd'/'MM'/'yyyy HH:mm:ss") });
            messages.Add(new MessageModel { Content = "second_message", Username = "username", Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019B"), DateTimeSent = DateTime.UtcNow.AddHours(1).ToString("dd'/'MM'/'yyyy HH:mm:ss") });
            messages.Add(new MessageModel { Content = "third_message", Username = "username", Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019C"), DateTimeSent = DateTime.UtcNow.AddHours(1).ToString("dd'/'MM'/'yyyy HH:mm:ss") });
            messages.Add(new MessageModel { Content = "forth_message", Username = "username", Id = ForthMessageId, DateTimeSent = DateTime.UtcNow.AddHours(1).ToString("dd'/'MM'/'yyyy HH:mm:ss") });
            messages.Add(new MessageModel { Content = "fifth_message", Username = "username", Id = new Guid("62FA647C-AD54-4BCC-A860-E5A2664B019E"), DateTimeSent = DateTime.UtcNow.AddHours(1).ToString("dd'/'MM'/'yyyy HH:mm:ss") });
            messages.Add(new MessageModel { Content = "sixth_message", Username = "username", Id = LastMessageId, DateTimeSent = DateTime.UtcNow.AddHours(1).ToString("dd'/'MM'/'yyyy HH:mm:ss") });
            return messages;
        }

        [TestMethod]
        public void ShouldReturnEmptyList_WhenNoMessagesSent()
        {
            var emptyList = new List<MessageModel>();
            Equals(emptyList, _inMemoryMessageRepository.GetMessagesSince(null));
        }

        [TestMethod]
        public void ShouldReturnAllMessages_WhenSizeIsLessThenPageSize()
        {
            var messages = MessageModelList();
            _inMemoryMessageRepository.AddMessage(messages.ElementAt(0));
            _inMemoryMessageRepository.AddMessage(messages.ElementAt(1));
            var result = _inMemoryMessageRepository.GetMessagesSince(null);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(FirstMessageId, result.First().Id);
        }

        [TestMethod]
        public void ShouldReturnPageSince_LastReceive()
        {
            var messages = MessageModelList();
            foreach (var item in messages)
            {
                _inMemoryMessageRepository.AddMessage(item);
            }
            var result = _inMemoryMessageRepository.GetMessagesSince(FirstMessageId);
            Assert.AreEqual(3, result.Count());
            Assert.AreEqual(ForthMessageId, result.Last().Id);
        }

        [TestMethod]
        public void ShouldReturnLessThenPageSize_IfOnlyAFewMessagesAreAdded()
        {
            var messages = MessageModelList();
            foreach (var item in messages)
            {
                _inMemoryMessageRepository.AddMessage(item);
            }
            var result = _inMemoryMessageRepository.GetMessagesSince(ForthMessageId);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual(LastMessageId, result.Last().Id);
        }

        [TestMethod]
        public void ShouldReturnLastPage_WhenNewUserJoined()
        {
            var messages = MessageModelList();
            foreach (var item in messages)
            {
                _inMemoryMessageRepository.AddMessage(item);
            }

            var result = _inMemoryMessageRepository.GetMessagesSince(null);
            Assert.AreEqual(result.Count(), pageSize);
            Assert.AreEqual(ForthMessageId, result.ElementAt(0).Id);
        }
    }
}
