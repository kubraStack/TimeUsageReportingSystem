using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Login
{
    public class LoginCommandValidator : AbstractValidator<LoginCommand>
    {
        public LoginCommandValidator()
        {
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("E-posta alanı zorunludur.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Şifre alanı zorunludur.");
        }
    }
}
