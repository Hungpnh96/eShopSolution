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
                .WithMessage("Tài khoản không được trống!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Mật khẩu không được trống!")
                .MinimumLength(8)
                .WithMessage("Mật khẩu phải ít nhất 8 kí tự!")
                .Matches("[A-Z]").WithMessage("Mật khẩu phải có chữ hoa!")
                .Matches("[a-z]").WithMessage("Mật khẩu phải có chữ thường!")
                .Matches(@"\d").WithMessage("Mật khẩu phải có chữ số!")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Mật khẩu phải có kí tự đặc biệt!")
                .Matches("^[^£# “”]*$").WithMessage("Mật khẩu không được chứa kí tự £ # “” hoặc khoảng trắng!");

            RuleFor(x => x.ComfirmPassword)
                .NotEmpty()
                .WithMessage("Xác nhận mật khẩu không được trống!")
                .MinimumLength(8)
                .WithMessage("Xác nhận mật khẩu phải ít nhất 8 kí tự!")
                .Matches("[A-Z]").WithMessage("Xác nhận mật khẩu phải có chữ hoa!")
                .Matches("[a-z]").WithMessage("Xác nhận mật khẩu phải có chữ thường!")
                .Matches(@"\d").WithMessage("Xác nhận mật khẩu phải có chữ số!")
                .Matches(@"[][""!@$%^&*(){}:;<>,.?/+_=|'~\\-]").WithMessage("Xác nhận mật khẩu phải có kí tự đặc biệt!")
                .Matches("^[^£# “”]*$").WithMessage("Xác nhận mật khẩu không được chứa kí tự £ # “” hoặc khoảng trắng!");

            RuleFor(x => x).Custom((request, context) =>
            {
                if(request.Password != request.ComfirmPassword)
                {
                    context.AddFailure("Xác thực mật khẩu không khớp!");
                }
            });

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Họ và tên không được trống!");


            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Địa chỉ email không được trống!")
                .Matches(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$")
                .WithMessage("Địa chỉ email không đúng định dạng!");

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
