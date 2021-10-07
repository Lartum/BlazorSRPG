using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSRPG.Shared
{
    public class UserRegister
    {
        [Required, EmailAddress]
        public string Email { get; set; }
        [Required, StringLength(100, MinimumLength = 8, ErrorMessage = "Password should be between 8-100 characters")]
        public string Password { get; set; }
        [Compare("Password", ErrorMessage = "Your Passwords do not match")]
        public string ConfirmPassword { get; set; }
        [StringLength(16, ErrorMessage ="Username cannot be longer than 16 character")]
        public string Username { get; set; }
        public string Bio { get; set; }
        public int StartUnitId { get; set; } = 1;
        [Range(0, 5000, ErrorMessage = "Pick a number between 0-5000")]
        public int Bananas { get; set; } = 100;
        public DateTime DateOfBirth { get; set; } = DateTime.Now;
        [Range(typeof(bool), "true", "true", ErrorMessage = "Please confirm your account")]
        public bool IsConfirmed { get; set; } = true;
    }
}
