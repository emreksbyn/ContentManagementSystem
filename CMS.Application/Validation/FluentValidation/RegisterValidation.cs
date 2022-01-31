using CMS.Application.Models.DTOs;
using FluentValidation;

namespace CMS.Application.Validation.FluentValidation
{
    public class RegisterValidation : AbstractValidator<RegisterDTO>
    {
        public RegisterValidation()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Bir Email adresi giriniz.")
                .EmailAddress()
                .WithMessage("Geçerli bir Email adresi giriniz!");

            RuleFor(x => x.Password)
                .NotEmpty()
                .WithMessage("Şifre giriniz.");

            RuleFor(x => x.ConfirmPassword)
                .Equal(x => x.Password)
                .WithMessage("Şifreler eşleşmiyor.");

            RuleFor(x => x.FullName)
                .NotEmpty()
                .WithMessage("Boş bırakılamaz.")
                .MinimumLength(3)
                .MaximumLength(50)
                .WithMessage("Min 3, Max 50 karakter olmalıdır.");
        }
    }
}