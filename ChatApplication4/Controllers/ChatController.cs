using Microsoft.AspNetCore.Mvc;

namespace ChatApplication4.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            // Retrieve session data
            var username = HttpContext.Session.GetString("Id");

            if (username != null)
            {
                ViewData["SessionData"] = username;
            }
            else
            {
                ViewData["SessionData"] = "User Not Logged In"; // Provide a default value or an error message
            }

            return View();
        }



    }
}
