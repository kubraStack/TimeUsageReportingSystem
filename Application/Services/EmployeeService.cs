using Application.Repositories.EmployeeRepo;
using Core.Dtos;
using Core.Utilities.Encryption;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmployeeService
    {
        private readonly Repositories.EmployeeRepo.IEmployeeRepository _employeeRepository;
        private readonly EncryptionHelper _encryptionHelper;

        public EmployeeService(IEmployeeRepository employeeRepository, EncryptionHelper encryptionHelper)
        {
            _employeeRepository = employeeRepository;
            _encryptionHelper = encryptionHelper;

        }

        public async Task<EmployeeDto> GetEmployee(int id)
        {
            var employeeEntity = await _employeeRepository.GetByIdAsync(id);

            if (employeeEntity == null)
            {
                // Çalışan bulunamazsa null döndür.
                return null;
            }

            var employeeDto = new EmployeeDto
            {
                Email = employeeEntity.Email,
                Role = employeeEntity.Role,

                FirstName = _encryptionHelper.DecryptString(employeeEntity.EncryptedFirstName),
                LastName = _encryptionHelper.DecryptString(employeeEntity.EncryptedLastName)

            };
            return employeeDto;
        }


    }
}
