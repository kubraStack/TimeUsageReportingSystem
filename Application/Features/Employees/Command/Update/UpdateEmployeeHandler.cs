using Application.Repositories.EmployeeRepo;
using Core.Utilities.Encryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Command.Update
{
    public class UpdateEmployeeHandler : IRequestHandler<UpdateEmployeeCommand, UpdateEmployeeResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EncryptionHelper _encryptionHelper;

        public UpdateEmployeeHandler(IEmployeeRepository employeeRepository, EncryptionHelper encryptionHelper)
        {
            _employeeRepository = employeeRepository;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<UpdateEmployeeResponse> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {

            var currentEmployee = await _employeeRepository.GetByIdAsync(request.Id);
            if (currentEmployee == null)
            {
                throw new Exception($"Çalışan ID {request.Id} bulunamadı !!");
            }

            string encryptedFirstName = _encryptionHelper.EncryptString(currentEmployee.EncryptedFirstName);
            string encryptedLastName = _encryptionHelper.EncryptString(currentEmployee.EncryptedLastName);

            currentEmployee.EncryptedFirstName = encryptedFirstName;
            currentEmployee.EncryptedLastName = encryptedLastName;
            currentEmployee.Email = request.Email;
            currentEmployee.DepartmentId = request.DepartmentId;
            currentEmployee.Role = request.Role;
            currentEmployee.UpdatedDate = DateTime.UtcNow;
            
             await _employeeRepository.UpdateAsync(currentEmployee);

            //response DTO'yu oluşturalım
            string decrytedFirstName = _encryptionHelper.DecryptString(currentEmployee.EncryptedFirstName);
            string decryptedLastName = _encryptionHelper.DecryptString(currentEmployee.EncryptedLastName);

            return new UpdateEmployeeResponse
            {
                Id = currentEmployee.Id,
                FirstName = decrytedFirstName,
                LastName = decryptedLastName,
                Email = currentEmployee.Email,
                UpdatedTime = currentEmployee.UpdatedDate.GetValueOrDefault(DateTime.UtcNow),
                Message = $"{decrytedFirstName} {decryptedLastName} adlı çalışan bilgileri başarıyla güncellenmiştir !"
            };
        }
    }
}
