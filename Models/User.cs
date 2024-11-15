﻿using System.ComponentModel.DataAnnotations;

namespace NUCCITWebApp.Models
{
    public class User
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [StringLength(255)]
        public string Password { get; set; }
    }
}
