using Application.Features.Department.Models;
using Application.Repositories.DepartmentRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Queries.GetDepartmentById
{
    public class GetDepartmentByIdHandler : IRequestHandler<GetDepartmentByIdQuery, DepartmentDetailDto>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentByIdHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentDetailDto> Handle(GetDepartmentByIdQuery request, CancellationToken cancellationToken)
        {
            //Sadece aktif olan departmanları getir
            var department = await _departmentRepository.GetAsync(
                predicate: d => d.Id == request.Id ,
                ignoreQueryFilters: true,
                cancellationToken: cancellationToken
            );

            if (department == null)
            {
                throw new Exception($"ID'si {request.Id} olan departman bulunamadı");
            }

            DepartmentDetailDto departmentDetailDto = new DepartmentDetailDto
            {
                Id = department.Id,
                Name = department.Name,
                Description = department.Description
            };
            return departmentDetailDto;
        }
    }
}
