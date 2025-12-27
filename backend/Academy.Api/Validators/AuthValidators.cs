using Academy.Api.DTOs;
using FluentValidation;

namespace Academy.Api.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.FullName).NotEmpty().WithMessage("نام و نام خانوادگی الزامی است.");
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("ایمیل معتبر وارد کنید.");
        RuleFor(x => x.Password).MinimumLength(6).WithMessage("رمز عبور حداقل ۶ کاراکتر باشد.");
    }
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("ایمیل معتبر وارد کنید.");
        RuleFor(x => x.Password).NotEmpty().WithMessage("رمز عبور الزامی است.");
    }
}
