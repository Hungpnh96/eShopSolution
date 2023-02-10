using eShopSolution.ViewModels.FluentValidations.Users;
using ServiceStack.FluentValidation.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace eShopSolution.ViewModels.System.Users
{
    [Validator(typeof(RegisterRequestFlunetValidation))]
    public class RegisterRequest
    {
        public string FullName { get; set; }
        public DateTime Dob { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ComfirmPassword { get; set; }
    }
}
