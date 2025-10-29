using Application.Repositories.DepartmentRepo;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Command.Create
{
    public class CreateDepartmentHandler : IRequestHandler<CreateDepartmentCommand, CreateDepartmentResponse>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public CreateDepartmentHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<CreateDepartmentResponse> Handle(CreateDepartmentCommand request, CancellationToken cancellationToken)
        {
            Domain.Entities.Department department = new Domain.Entities.Department
            {
                Name = request.Name,
                Description = request.Description,
                CreatedDate = DateTime.UtcNow
            };

            Domain.Entities.Department createdDepartment = await _departmentRepository.AddAsync(department);
            CreateDepartmentResponse response = new CreateDepartmentResponse
            {
                Id = createdDepartment.Id,
                Name = createdDepartment.Name,
                CreatedDate = createdDepartment.CreatedDate
            };

            return response;
        }
    }
}
