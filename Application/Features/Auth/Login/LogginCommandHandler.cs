using Application.Features.Auth.Dtos;
using Application.Repositories.EmployeeRepo;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Entities;
using Core.Utilities.Hashing;
using Core.Utilities.JWT;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Login
{
    public class LogginCommandHandler : IRequestHandler<LogginCommand, LoggedResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ITokenHelper _tokenHelper;

        public LogginCommandHandler(IEmployeeRepository employeeRepository, ITokenHelper tokenHelper)
        {
            _employeeRepository = employeeRepository;
            _tokenHelper = tokenHelper;
        }

        public async Task<LoggedResponse> Handle(LogginCommand request, CancellationToken cancellationToken)
        {
            //Kullanıcının varlığını kontrol edelim
            var employee = await _employeeRepository.GetByEmailAsync(request.Email);
            if ( employee == null)
            {
                throw new BusinessException("Kullanıcı adı veya şifre hatalı !");
            }

            //Şifre Hash Doğrulama
            bool passwordHash = HashingHelper.VerifyPasswordHash(
                request.Password,
                employee.PasswordHash,
                employee.PasswordSalt
            );

            if (!passwordHash)
            {
                throw new BusinessException("Kullanıcı adı veya şifre hatalı !");
            }

            //Employee'i baseUser'a dönüştürüyoruz
            BaseUser? baseUser = employee as BaseUser;
            if (baseUser == null)
            {
                throw new Exception("Kullanıcı(Employee), BaseUser tipine dönüştürülemedi.");
            }

            //Role Enum'unu OperationClaim Listesine Çevirelim
            List<OperationClaim> claims = new List<OperationClaim>();
            string roleString = employee.Role.ToString();

            if (!string.IsNullOrEmpty(roleString)) {
                claims.Add(new OperationClaim { Name = roleString });
            }

            //Jwt Token oluşturma
            AccessToken accessToken = _tokenHelper.CreateToken(
                baseUser,
                claims
            );

            return new LoggedResponse
            {
                AccessToken = accessToken,
            };

        }
    }
}
