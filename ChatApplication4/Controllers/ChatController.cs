using Microsoft.AspNetCore.Mvc;
 
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ChatApplication4.Models;

namespace ChatApplication4.Controllers
{
    public class ChatController : Controller
    {
        private readonly AppDbContext _context;
        public ChatController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
             

            //string username  = HttpContext.Session.GetString("Username");
            ViewBag.NotificationMessage = TempData["NotificationMessage"] as string;
            ViewBag.PicPath = TempData["PicPath"] as string;
            ViewBag.Country = TempData["Country"] as string;
            ViewBag.Region = TempData["Region"] as string;
            ViewBag.City = TempData["City"] as string;

            //var allUsers = _context.CustomUsers.ToList();


            //var chatViewModel = new ChatViewModel();

            return _context.CustomUsers != null ?
                       View(await _context.CustomUsers.ToListAsync()) :
                       Problem("Entity set 'AppDbContext.CustomUsers'  is null.");


            //chatViewModel.CustomUser = allUsers.Select(x => x.Username);

            //ViewData["CustomUser"] = chatViewModel.CustomUser;


            
        }
        public IActionResult VideoChat()
        {


            return View();

        }

        public IActionResult ChatList()
        {

            return View();
        }
    }
}
