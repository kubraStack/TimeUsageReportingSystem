using Application.Features.Employees.Models;
using Application.Repositories.EmployeeRepo;
using Core.Utilities.Encryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetEmployeeId
{
    public class GetEmployeeByIdHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDetailDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EncryptionHelper _encryptionHelper;

        public GetEmployeeByIdHandler(IEmployeeRepository employeeRepository, EncryptionHelper encryptionHelper)
        {
            _employeeRepository = employeeRepository;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<EmployeeDetailDto> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetAsync(
                predicate: e => e.Id == request.Id,
                ignoreQueryFilters: true,
                cancellationToken: cancellationToken
            );

            if (employee == null)
            {
                throw new Exception($"Çalışan Id {request.Id} bulunamadı !");
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
