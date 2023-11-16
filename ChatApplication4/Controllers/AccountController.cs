using ChatApplication4.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public async Task<IActionResult> Login(string username, string password)
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

                // Get user's location based on their IP address
                // Get user's location based on their IP address
                var userIpAddress = GetUserIpAddress();

                var locationInfo = await GetUserLocationInfo(userIpAddress);



                // Pass the location information to the view
                //ViewData["Country"] = locationInfo.Country;
                //ViewData["Region"] = locationInfo.Region;
                //ViewData["City"] = locationInfo.City;

                // ... rest of your code





                // Pass the profile picture path to the view
                //ViewBag.ProfilePicturePath = profilePicturePath;


                if (user != null)
                {
                    HttpContext.Session.SetString("Username", user.Username); // Set the session data
                                                                              // Set notification message for successful login
                    var notificationMessage = user.Username;
                    var profilepicture = profilePicturePath;
                    TempData["NotificationMessage"] = notificationMessage;
                    TempData["PicPath"] = profilepicture;
                    TempData["Country"] = locationInfo.Country;
                    TempData["Region"] = locationInfo.Region;
                    TempData["City"] = locationInfo.City;

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

        private async Task<UserLocationInfo> GetUserLocationInfo(string ipAddress)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = $"http://ip-api.com/json/{ipAddress}";
                var response = await httpClient.GetStringAsync(apiUrl);

                try
                {
                    dynamic jsonResponse = JsonConvert.DeserializeObject(response);

                    // Check if the JSON contains the "country" field
                    if (jsonResponse != null && jsonResponse.country != null)
                    {
                        var locationInfo = new UserLocationInfo
                        {
                            Country = jsonResponse.country,
                            Region = jsonResponse.regionName,
                            City = jsonResponse.city
                        };
                        return locationInfo;
                    }
                    else
                    {
                        Console.WriteLine("Country information not found in the JSON response.");
                    }
                }
                catch (Exception)
                {
                    // Handle JSON parsing or other exceptions as needed
                }
            }

            return null; // Return null if no location information is found
        }

        private string GetUserIpAddress()
        {
            string ipAddress = "203.122.21.210";
            //string ipAddress = HttpContext.Connection.RemoteIpAddress.ToString();
            //if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            //{
            //    ipAddress = HttpContext.Request.Headers["X-Forwarded-For"];
            //}
            //else if (HttpContext.Connection.RemoteIpAddress.IsIPv4MappedToIPv6)
            //{
            //    ipAddress = HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            //}
            return ipAddress;


        }

        public class UserLocationInfo
        {
            public string Country { get; set; }
            public string Region { get; set; }
            public string City { get; set; }
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
}
