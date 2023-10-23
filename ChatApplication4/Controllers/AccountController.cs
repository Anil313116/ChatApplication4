using ChatApplication4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace ChatApplication4.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _context;

        public AccountController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CustomUser model)
        {
            if (ModelState.IsValid)
            {
                // Perform user registration and database insertion here
                _context.CustomUsers.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Login");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (ModelState.IsValid)
            {
               

                // Authenticate the user. If authentication is successful, set the session.
                //var user = _context.CustomUsers.SingleOrDefault(u => u.Username == username && u.Password == password);

                // Authenticate the user. If authentication is successful, set the session.
                var user = _context.CustomUsers.SingleOrDefault(u => u.Username == username && u.Password == password);

                // Check username and password against the database
                //var user = _context.CustomUsers.SingleOrDefault(u => u.Username == username && u.Password == password);
                // Authenticate the user. If authentication is successful, set the session.
                // Implement user session management here, you can use the built-in session.

                 HttpContext.Session.SetString("Id",user.Username.ToString());
                // Access the value and store it in ViewData
                string sessionData = HttpContext.Session.GetString("Id");

                ViewData["SessionData"] = sessionData;

                //HttpContext.Session.SetString("UserRole", user.Role);

                if (user != null)
                {
                    HttpContext.Session.SetString("Id", user.Username); // Set the session data


                    // Redirect to the user's dashboard based on their role
                    if (user.Role == "Doctor")
                    {
                        return RedirectToAction("Index", "Chat");
                    }
                    else if (user.Role == "Engineer")
                    {
                       

                        return RedirectToAction("Index", "Chat");
                    }
                    else if (user.Role == "Politician")
                    {
                        return RedirectToAction("Index", "Chat");
                    }
                }
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                
            }
            return View();
        }

    }

    public class UserController : Controller
    {
        public IActionResult Profile()
        {
            // Display user profile data
            return View();
        }
    }


}
