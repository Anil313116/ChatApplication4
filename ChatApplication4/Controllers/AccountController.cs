using ChatApplication4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace ChatApplication4.Controllers
{
    public class AccountController : Controller
    {

        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AccountController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(CustomUser model, IFormFile profilePicture)
        {
            //if (ModelState.IsValid)
            //{

                if (profilePicture != null && profilePicture.Length > 0)
                {
                var webRootPath = _webHostEnvironment.WebRootPath;
                var imagePath = Path.Combine(webRootPath, "Images"); // Assumes "Images" is a folder in wwwroot
                
                                    // Save the profile picture to a location or database, and set the file path in the model.
                                   // var imagePath = "~/Images"; // Update this with the actual path
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + profilePicture.FileName;
                    var filePath = Path.Combine(imagePath, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        profilePicture.CopyTo(stream);
                    }

                    model.ProfilePicture = filePath; // Set the file path in the model
                }

                // Perform user registration and database insertion here
                _context.CustomUsers.Add(model);
                _context.SaveChanges();

                return RedirectToAction("Login");
            //}
           // return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            const string sessionKey = "FirstSeen";
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

                 HttpContext.Session.SetString(sessionKey, user.Username.ToString());
                // Access the value and store it in ViewData
                //string sessionData = HttpContext.Session.GetString("Id");

                // ViewData["SessionData"] = sessionData;

                //HttpContext.Session.SetString("UserRole", user.Role);

                // Fetch the user's profile picture file path
                var profilePicturePath = user.ProfilePicture;

                // Pass the profile picture path to the view
                //ViewBag.ProfilePicturePath = profilePicturePath;


                if (user != null)
                {
                    HttpContext.Session.SetString("Username", user.Username); // Set the session data
                                                                              // Set notification message for successful login
                    var notificationMessage =   user.Username;
                    var profilepicture = profilePicturePath;
                    TempData["NotificationMessage"] = notificationMessage;
                    TempData["PicPath"] = profilepicture;

                    // Redirect to the user's dashboard based on their role
                    if (user.Role == "Police")
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
