using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Models
{
    public class UserModel
    {
        [Required]
        public string UserName { get; set; }
    }
}
