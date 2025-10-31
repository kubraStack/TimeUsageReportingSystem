using Application.Features.Auth.Dtos;
using Application.Repositories.EmployeeRepo;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Entities;
using Core.Utilities.Encryption;
using Core.Utilities.Hashing;
using Core.Utilities.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, LoggedResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITokenHelper _tokenHelper;
        private readonly EncryptionHelper _encryptionHelper;

        public RegisterCommandHandler(IEmployeeRepository employeeRepository, ITokenHelper tokenHelper, EncryptionHelper encryptionHelper)
        {
            _employeeRepository = employeeRepository;
            _tokenHelper = tokenHelper;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<LoggedResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            //Kullanıcının e-postasının zaten kayıtlı olup olmadığını kontrol et
            var userExists = await _employeeRepository.GetByEmailAsync(request.Email);
            if (userExists != null )
            {
                throw new BusinessException("Bu e-posta adresi zaten kayıtlı.");
            }

            //Hashleme
            HashingHelper.CreatePasswordHash(
                request.Password,
                out byte[] passwordHash,
                out byte[] passwordSalt
            );
            string encryptedFirstName = _encryptionHelper.EncryptString(request.FirstName);
            string encryptedLastName = _encryptionHelper.EncryptString(request.LastName);


            var newEmployee = new Domain.Entities.Employee
            {
               Email = request.Email,
               EncryptedFirstName = encryptedFirstName,
               EncryptedLastName = encryptedLastName,
               PasswordHash = passwordHash,
               PasswordSalt = passwordSalt,
               DepartmentId = request.DepartmentId,
               Role = UserType.Employee,
               IsDeleted = false,

            };
            await _employeeRepository.AddAsync(newEmployee);

            BaseUser? baseUser = newEmployee as BaseUser;
            if (baseUser == null)
            {
                // Eğer dönüşüm başarısızsa 
                throw new System.Exception("Employee nesnesi, BaseUser tipine dönüştürülemedi. Token Helper'ı kullanmak için Employee'nin BaseUser'dan türemesi gerekir.");
            }

            List<OperationClaim> claims = new List<OperationClaim>();
            string roleString = newEmployee.Role.ToString();//Tip Dönüşümü: Enum'u string'e çeviriyoruz
            if (!string.IsNullOrEmpty(roleString))
            {
                claims.Add(new OperationClaim { Name = roleString });
            }

            AccessToken accessToken = _tokenHelper.CreateToken(
                baseUser, 
                claims    
            );
            return new LoggedResponse
            {
                AccessToken = accessToken
            };
        }
    }
}
