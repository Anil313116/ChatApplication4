using Microsoft.AspNetCore.Mvc;
 
using Microsoft.AspNetCore.Http;
namespace ChatApplication4.Controllers
{
    public class ChatController : Controller
    {
        public IActionResult Index()
        {
            //string username  = HttpContext.Session.GetString("Username");
            ViewBag.NotificationMessage = TempData["NotificationMessage"] as string;
            ViewBag.PicPath = TempData["PicPath"] as string;

            //if (username != null)
            //{
            //    ViewData["Username"] = username ; // Pass the username to the view
            //}
            //else
            //{
            //    ViewData["Username"] = "User Not Logged In"; // Provide a default value or an error message
            //}

            // Retrieve session data
            //var username = HttpContext.Session.GetString("Id");

            //if (username != null)
            //{
            //    ViewData["SessionData"] = username;
            //}
            //else
            //{
            //    ViewData["SessionData"] = "User Not Logged In"; // Provide a default value or an error message
            //}

            return View("Index");
        }



    }
}
