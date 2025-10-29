using Application.Repositories.DepartmentRepo;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Command.Update
{
    public class UpdateDepartmentHandler : IRequestHandler<UpdateDepartmentCommand, UpdateDepartmentResponse>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public UpdateDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<UpdateDepartmentResponse> Handle(UpdateDepartmentCommand request, CancellationToken cancellationToken)
        {
            //Mevcut departmanı al
            Domain.Entities.Department? currentDepartment = await _departmentRepository.GetAsync(
                    predicate: d => d.Id == request.Id && d.IsDeleted == false,
                    cancellationToken: cancellationToken
            );

            //İş kuralı/kayıt bulunamadıysa
            if (currentDepartment == null)
            {
                throw new Exception($"ID'si {request.Id} olan aktif departman bulunamadı.");
            }

            currentDepartment.Name = request.Name;
            currentDepartment.Description = request.Description;

            await _departmentRepository.UpdateAsync(currentDepartment);

            UpdateDepartmentResponse response = new UpdateDepartmentResponse
            {
                Id = currentDepartment.Id,
                Name = currentDepartment.Name,
                UpdatedDate = currentDepartment.UpdatedDate ?? DateTime.Now
            };
            return response;
        }
    }
}
