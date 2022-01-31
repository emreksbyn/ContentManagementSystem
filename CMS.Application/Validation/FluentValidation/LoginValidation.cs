using CMS.Application.Models.DTOs;
using FluentValidation;

namespace CMS.Application.Validation.FluentValidation
{
    public class LoginValidation : AbstractValidator<LoginDTO>
    {
        public LoginValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("Kullanıcı adı giriniz.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Şifre giriniz.");
        }
    }
}
