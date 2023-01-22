using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace ProjectPathfinder.ViewModels
{
    public class AccountLogin
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class AccountSignUp
    {
        // Name
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        // Birthday
        [Required]
        public string BirthdayDay { get; set; }
        [Required]
        public string BirthdayMonth { get; set; }
        [Required]
        public string BirthdayYear { get; set; }

        // Contact and School
        [Required]
        public string Grade { get; set; }

        [Required, DataType(DataType.PhoneNumber)]
        public string ContactNumber { get; set; }
        [Required]
        public string School { get; set; }

        // Login Details
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }

        // Date Signed Up
        [Required]
        public DateTime DateOfRegistration { get; set; }
    }

    public class AccountForgotPassword
    {
        [Required, DataType(DataType.EmailAddress)]
        public string Email { get; set; }
    }

    public class AccountResetPassword
    {
        public string Id { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
    }
}