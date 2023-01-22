using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectPathfinder.ViewModels
{
    public class AboutUs
    {
        // Name
        [Required]
        public string Name { get; set; }

        // Email
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        // Name
        [Required]
        public string Message { get; set; }

    }
}