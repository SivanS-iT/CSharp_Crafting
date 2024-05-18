﻿using System.ComponentModel.DataAnnotations;

namespace WebHotels.Web.ViewModels
{
    public class LoginVM
    {
        [Required]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        public string RememberMe { get; set; }
        public string RedirectUrl { get; set; }

    }
}