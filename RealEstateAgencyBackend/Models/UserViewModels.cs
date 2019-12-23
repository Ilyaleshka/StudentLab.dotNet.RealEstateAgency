using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RealEstateAgencyBackend.Models
{
    public class UserCreateModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string ConfirmPassword { get; set; }
    }

    public class UserLoginModel
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }
    }

    public class UserViewModel
    {
        [Required]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
    }

}