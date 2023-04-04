﻿using System.ComponentModel.DataAnnotations;
namespace Tour.Application.Dto
{
    public class SignInDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
    }
}
