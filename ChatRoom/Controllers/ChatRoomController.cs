using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using ChatRoom.Interfaces;
using ChatRoom.Models;
using Microsoft.AspNetCore.Http;
using System;

namespace ChatRoom.Controllers
{
    public class ChatRoomController : Controller
    {
        private readonly ILogger<ChatRoomController> _logger;
        private readonly IMessageRepository _messageRepository;

        public ChatRoomController(ILogger<ChatRoomController> logger, IMessageRepository messageRepository)
        {
            _logger = logger;
            _messageRepository = messageRepository;
        }

        [HttpPost]
        [Route("store-message")]
        public IActionResult StoreMessage(MessageModel model)
        {
            var updatedModel = AddUserAndDateTime(model);
            _messageRepository.AddMessage(updatedModel);
            return Ok();
        }

        [HttpGet]
        [Route("print-messages")]
        public IEnumerable<MessageModel> PrintMessages([FromQuery]string messageId)
        {
            if (Guid.TryParse(messageId, out Guid result))
            {
                return _messageRepository.GetMessagesSince(result);
            }
            return _messageRepository.GetMessagesSince();
        }

        private MessageModel AddUserAndDateTime(MessageModel model)
        {
            model.Username = HttpContext.Session.GetString("username");
            model.DateTimeSent = DateTime.UtcNow.AddHours(1).ToString("dd'/'MM'/'yyyy HH:mm:ss");
            return model;


        }
    }
}
