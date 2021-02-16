using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ChatRoom.Interfaces;
using ChatRoom.Models;


namespace ChatRoom.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger, IUserRepository userRespository)
        {
            _logger = logger;
            _userRepository = userRespository;
        }

        public IActionResult Index()  
        {  
            return View();  
        }  

        [HttpPost]
        [Route("login-session")]
        public IActionResult LoginSession(UserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = Contains(model);
                if(!result)
                {
                    HttpContext.Session.SetString("username", model.UserName);
                    AddUsers(model);
                    return Ok();
                }
                return StatusCode(409);
            }
            return Ok(model);
        }
        
        [HttpGet]
        [Route("get-user-from-session")]
        public IActionResult GetUserFromSession()
        {
            var currentUser = HttpContext.Session.GetString("username");
            return string.IsNullOrEmpty(currentUser) ? NotFound() : Ok(currentUser);
        }
        
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }

        public IActionResult AddUsers(UserModel model)
        {
            _userRepository.AddUser(model);
            return Ok();
        }

        public bool Contains(UserModel model)
        {
            var contains = _userRepository.Contains(model);
            return contains;
        }
    }
}