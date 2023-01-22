using ProjectPathfinder.Models;
using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectPathfinder.Areas.Member.ViewModels
{
    public class AccountResultStatus
    {
        public DateTime RegisteredDate { get; set; }
        public DateTime SubmittedTestDate { get; set; }
        public DateTime ResultsObtainedDate { get; set; }
    }

    public class AccountPersonalDetails
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
        [DataType(DataType.EmailAddress)]
        public string ConfirmEmail { get; set; }

        [DataType(DataType.Password)]
        public string ExistingPassword { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }
    }

    public class AccountInvoice
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string ContactNo { get; set; }
        public string Email { get; set; }

        public string OrderNo { get; set; }
        public string InvoiceDate { get; set; }

        public string ItemName { get; set; }
        public string ItemPrice { get; set; }
        public string ItemDiscount { get; set; }
        public string ItemTotal { get; set; }

        public string FinalTotal { get; set; }
    }
}