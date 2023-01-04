using System;
using System.Collections.Generic;
using System.Text;
using eShopSolution.ViewModels.System.Users;
using FluentValidation;

namespace eShopSolution.ViewModels.FluentValidations.Users
{
    public class RegisterRequestFlunetValidation : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestFlunetValidation()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .WithMessage("UserName không được trống!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Password không được trống!");

            RuleFor(x => x.ComfirmPassword)
                .NotEmpty()
                .WithMessage("ComfirmPassword không được trống!");

            RuleFor(x => x).Custom((request, context) =>
            {
                if(request.Password != request.ComfirmPassword)
                {
                    context.AddFailure("ComfirmPassword phải giống Password!");
                }
            });

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .WithMessage("FirstName không được trống!");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .WithMessage("LastName không được trống!");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email không được trống!")
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
                .WithMessage("Email không đúng định dạng");

            RuleFor(x => x.Dob)
                .NotEmpty()
                .WithMessage("Ngày sinh không được trống!")
                .Must(x => DateTime.TryParse(x.ToString(), out DateTime val))
                .WithMessage("Định dạng ngày tháng năm không đúng!")
                .GreaterThan(DateTime.Now.AddYears(-100))
                .WithMessage("Ngày sinh không vượt quá 100 năm trước!");

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .WithMessage("PhoneNumber không được trống!")
                .Matches(@"^(0?)(3[2-9]|5[6|8|9]|7[0|6-9]|8[0-6|8|9]|9[0-4|6-9])[0-9]{7}$")
                .WithMessage("Số điện thoại không đúng định dạng vùng miền VIỆT NAM!");
        }
    }
}
