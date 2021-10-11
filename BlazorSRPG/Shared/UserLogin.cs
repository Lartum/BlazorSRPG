using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSRPG.Shared
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Please Enter an email address")]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
