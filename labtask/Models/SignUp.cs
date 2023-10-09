using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Remoting.Lifetime;
using System.Text.RegularExpressions;
using System.Web;

namespace labtask.Models
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;

                // If the birthdate has not occurred this year, subtract one year from the age
                if (dateOfBirth > today.AddYears(-age))
                {
                    age--;
                }

                if (age < _minimumAge)
                {
                    return new ValidationResult($"You must be at least {_minimumAge} years old.");
                }
            }

            return ValidationResult.Success;
        }
    }
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class CustomPasswordValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false; // Password is required, so a null value is not valid
            }

            string password = value.ToString();

            // Password length validation
            if (password.Length < 8)
            {
                return false; // Password should be at least 8 characters long
            }

            // First 4 characters must be alphabets with at least 1 upper and 2 lower case letters
            if (!Regex.IsMatch(password.Substring(0, 4), @"^(?=.*[a-z]{2})(?=.*[A-Z]).+$"))
            {
                return false; // First 4 characters do not meet requirements
            }

            // Next 4 characters must be a combination of special characters and numbers
            if (!Regex.IsMatch(password.Substring(4, 4), @"^(?=.*\d)(?=.*[.@!#$%^&*]).+$"))
            {
                return false; // Next 4 characters do not meet requirements
            }

            return true; // Password meets all requirements
        }
    }


    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
    public sealed class CustomEmailValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value == null)
            {
                return false; // Email is required, so a null value is not valid
            }



            string email = value.ToString();



            // Define a regular expression pattern for a valid email address
            string pattern = @"^\d{2}-\d{5}-\d@student\.aiub\.edu$";



            return Regex.IsMatch(email, pattern);
        }
    }


    public class SignUp
    {
        [Required(ErrorMessage = "Provide you name")]

        [MaxLength(50)]
        [MinLength(4)]
        [RegularExpression(@"^[a-zA-Z .-]*$", ErrorMessage = "Only alphabets,Space(),dot(.)and dash (-) are not allowed")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Provide you uniqe userid")]

        [MaxLength(12)]
        [MinLength(4)]
        [RegularExpression(@"^[0-9a-zA-Z-_]*$", ErrorMessage = "Only alphabets,numbers,underscore(_)and dash (-) are allowed")]
        public string UserId { get; set; }


        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Please enter password")]
        [CustomPasswordValidation(ErrorMessage = "Invalid Password")]

        public string Password { get; set; }

        [Required(ErrorMessage = "Provide your Student ID")]
        [RegularExpression(@"^\d{2}-\d{5}-\d$", ErrorMessage = "ID must be in the format xx-xxxxx-x")]
        
        public string Id { get; set; }

        [Required(ErrorMessage = "Provide your Email")]
        [StringLength(30, ErrorMessage = "Length must be less than 30 characters")]
        [RegularExpression(@"^\d{2}-\d{5}-\d@student\.aiub\.edu$", ErrorMessage = "Email must be in the format xx-xxxxx-x@student.aiub.edu")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Provide your Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [MinimumAge(18, ErrorMessage = "You must be at least 18 years old.")]
        public DateTime DoB { get; set; }
    }

}