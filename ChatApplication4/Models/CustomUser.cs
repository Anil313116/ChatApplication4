﻿namespace ChatApplication4.Models
{
    public class CustomUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; } // Doctor, Engineer, Politician, etc.
        public string ProfilePicture { get; set; } // Add this property for profile picture file path
        public string Gender { get; set; } // Add this property for profile picture file path
        public bool IsOnline { get; set; }

    }
}
