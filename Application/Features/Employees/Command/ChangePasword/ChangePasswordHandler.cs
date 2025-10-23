using Application.Repositories.EmployeeRepo;
using Core.Utilities.Hashing;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Command.ChangePasword
{
    public class ChangePasswordHandler : IRequestHandler<ChangePasswordCommand, ChangePasswordResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        

        public ChangePasswordHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<ChangePasswordResponse> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var currentEmployee = await _employeeRepository.GetByIdAsync(request.EmployeeId);

            if (currentEmployee == null)
            {
                throw new Exception($"Çalışan ID {request.EmployeeId} bulunamadı !!");
            }

            //Mevcut şifreyi doğrulama
            bool passwordVerified = HashingHelper.VerifyPasswordHash
            (
                request.OldPassword,
                currentEmployee.PasswordHash,
                currentEmployee.PasswordSalt
            );

            if (!passwordVerified)
            {
                throw new Exception("Mevcut şifre hatalı ! Lütfen eski şifrenizi doğru giriniz. ");
            }

            //Yeni şifre oluşturma
            HashingHelper.CreatePasswordHash
            (
                request.NewPassword,
                out byte[] newPasswordHash,
                out byte[] newPasswordSalt
            );

            //Güncellme
            currentEmployee.PasswordHash = newPasswordHash;
            currentEmployee.PasswordSalt = newPasswordSalt;
            currentEmployee.UpdatedDate = DateTime.UtcNow;

            await _employeeRepository.UpdateAsync( currentEmployee );

            return new ChangePasswordResponse
            {
                EmployeeId = currentEmployee.Id,
                Success = true,
                UpdateTime = currentEmployee.UpdatedDate.Value,
                Message ="Şifre başarıyla güncellendi."
            };

        }
    }
}
