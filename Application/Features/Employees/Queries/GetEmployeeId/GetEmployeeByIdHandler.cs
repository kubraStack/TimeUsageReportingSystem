using Application.Features.Employees.Models;
using Application.Repositories.EmployeeRepo;
using Core.CrossCuttingConcerns.Exceptions.Types;
using Core.Utilities.Encryption;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetEmployeeId
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDetailDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EncryptionHelper _encryptionHelper;
        private readonly IHttpContextAccessor _contextAccessor;
        public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository, EncryptionHelper encryptionHelper, IHttpContextAccessor contextAccessor)
        {
            _employeeRepository = employeeRepository;
            _encryptionHelper = encryptionHelper;
            _contextAccessor = contextAccessor;
        }

        public async Task<EmployeeDetailDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var userIdClaim = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null)
            {
                throw new BusinessException("JWT Token'da geçerli bir kullanıcı kimliği (ID) bulunamadı.");
            }

            var userIdString = userIdClaim.Value;
            if (!int.TryParse(userIdString, out int currentUserId))
            {
                // Kullanıcı giriş yapmış, ama token'da ID yoksa (Çok nadir)
                throw new BusinessException("JWT Token'da geçerli bir kullanıcı kimliği (ID) bulunamadı.");
            }
            var employee = await _employeeRepository.GetAsync(
                predicate: e => e.Id == currentUserId,
                ignoreQueryFilters: true,
                cancellationToken: cancellationToken
            );

            if (employee == null)
            {
                throw new BusinessException("Çalışan  bulunamadı !");
            }

            //Şifre çözümleme
            string decryptedFirstName = _encryptionHelper.DecryptString(employee.EncryptedFirstName);
            string decryptedLastName = _encryptionHelper.DecryptString(employee.EncryptedLastName);

            return new EmployeeDetailDto
            {
                Id = employee.Id,
                FirstName = decryptedFirstName,
                LastName = decryptedLastName,
                Email = employee.Email,
                Role = employee.Role,
                DepartmentId = employee.DepartmentId,
            };
        }
    }
    
}
