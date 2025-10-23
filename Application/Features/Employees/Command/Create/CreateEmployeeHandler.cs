using Application.Repositories.EmployeeRepo;
using AutoMapper;
using Core.Utilities.Encryption;
using Core.Utilities.Hashing;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Command.Create
{
    public class CreateEmployeeHandler : IRequestHandler<CreateEmployeeCommand, CreateEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EncryptionHelper _encryptionHelper;

        public CreateEmployeeHandler(IEmployeeRepository employeeRepository, EncryptionHelper encryptionHelper)
        {
            _employeeRepository = employeeRepository;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<CreateEmployeeResponse> Handle(CreateEmployeeCommand request, CancellationToken cancellationToken)
        {
            // Şifreleme & hash işlemleri
            HashingHelper.CreatePasswordHash(request.Password, out var passwordHash, out var passwordSalt);
            string encryptedFirstName = _encryptionHelper.EncryptString(request.FirstName);
            string encryptedLastName = _encryptionHelper.EncryptString(request.LastName);

            // Entity oluşturma
            var employee = new Employee
            {
                DepartmentId = request.DepartmentId,
                EncryptedFirstName = encryptedFirstName,
                EncryptedLastName = encryptedLastName,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = request.Role,
                CreatedDate = DateTime.UtcNow,
            };

            var createdEmployee = await _employeeRepository.AddAsync(employee);

            // Response oluşturma
            var response = new CreateEmployeeResponse
            {
                Id = createdEmployee.Id,
                Message = "Employee created successfully.",
                CreatedAt = createdEmployee.CreatedDate
            };

            return response;
        }
    }

}
