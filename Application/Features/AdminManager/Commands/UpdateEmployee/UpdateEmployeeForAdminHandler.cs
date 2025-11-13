using Application.Repositories.EmployeeRepo;
using Core.CrossCuttingConcerns.Exceptions.Types;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.AdminManager.Commands.UpdateEmployee
{
    public class UpdateEmployeeForAdminHandler : IRequestHandler<UpdateEmployeeForAdminCommand, UpdateEmployeeForAdminResponse>
    {
        private readonly IEmployeeRepository _employeeRepository;

        public UpdateEmployeeForAdminHandler(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<UpdateEmployeeForAdminResponse> Handle(UpdateEmployeeForAdminCommand request, CancellationToken cancellationToken)
        {
            var employeeToUpdate = await _employeeRepository.GetAsync(e => e.Id == request.EmployeeId);
            if (employeeToUpdate == null)
            {
                throw new BusinessException("Güncellenmek istenen çalışan bulunamadı !");
                
            }

            employeeToUpdate.Role = request.NewRole;
            employeeToUpdate.DepartmentId = request.NewDepartmentId;

             await _employeeRepository.UpdateAsync(employeeToUpdate);

            return new UpdateEmployeeForAdminResponse
            {
                EmployeeId = employeeToUpdate.Id,
                DepartmentId = employeeToUpdate.DepartmentId,
                Role = employeeToUpdate.Role
            };
        }
    }
}
