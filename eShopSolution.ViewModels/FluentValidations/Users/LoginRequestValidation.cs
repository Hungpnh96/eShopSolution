using System;
using System.Collections.Generic;
using System.Text;
using eShopSolution.ViewModels.System.Users;
using FluentValidation;

namespace eShopSolution.ViewModels.FluentValidations.Users
{
    public class LoginRequestValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("UserName không được trống!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password không được trống!")
                .MinimumLength(6)
                .WithMessage("Password phải nhiều hơn 6 kí tự!");
        }
    }
}
