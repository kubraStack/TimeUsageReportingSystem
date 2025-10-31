using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Register
{
    public class RegisterCommandValidator :AbstractValidator<RegisterCommand>
    {
        public RegisterCommandValidator()
        {
            //E-posta doğrulaması
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("E-posta boş olamaz.")
                .MaximumLength(100).WithMessage("E-posta en fazla 100 karakter olabilir.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");
            //Parola doğrulaması
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Parola boş olamaz.")
                .MinimumLength(6).WithMessage("Parola en az 6 karakter olmalıdır.")
                .MaximumLength(50).WithMessage("Parola en fazla 50 karakter olabilir.");

            //İsim doğrulaması
            RuleFor(r => r.FirstName)
                .NotEmpty().WithMessage("Ad alanı zorunludur.");
            RuleFor(r => r.LastName)
                .NotEmpty().WithMessage("Soyad alanı zorunludur.");

            RuleFor(r => r.DepartmentId)
                .NotEmpty().WithMessage("Departman ID alanı zorunludur.")
                .GreaterThan(0).WithMessage("Departman ID geçerli bir pozitif sayı olmalıdır.");
        }
    }
}
