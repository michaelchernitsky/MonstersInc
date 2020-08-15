using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AuthServer.Models
{
    public class RegisterInputModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Please make sure your passwords match")]
				[Display(Name = "Confirm password")]
        public string ConfirmPassword { get; set; }

        [Required]
        [StringLength(200)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(200)]
        public string LastName { get; set; }
        [Required]
        [Phone]
        public string PhoneNumber { get; set; }
        public int? TentaclesNumber { get; set; }
        [Required]
        public DateTime ScareStartDate { get; set; }


        //public string ReturnUrl { get; set; }
    }
}