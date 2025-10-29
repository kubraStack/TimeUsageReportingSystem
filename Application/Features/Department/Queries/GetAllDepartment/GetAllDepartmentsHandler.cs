using Application.Features.Department.Models;
using Application.Repositories.DepartmentRepo;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Department.Queries.GetAllDepartment
{
    public class GetAllDepartmentsHandler : IRequestHandler<GetAllDepartmentsQuery, DepartmentListDto>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetAllDepartmentsHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<DepartmentListDto> Handle(GetAllDepartmentsQuery request, CancellationToken cancellationToken)
        {
            var departmentPageable = await _departmentRepository.GetListAsync(
                predicate: d =>d.IsDeleted == false,
                orderBy: q => q.OrderBy(d => d.Id),
                index: request.PageRequest.Page,
                size: request.PageRequest.PageSize,
                cancellationToken: cancellationToken
            );

            var departmentList = departmentPageable.Items.Select(
                department => new DepartmentDetailDto
                {
                    Id = department.Id,
                    Name = department.Name,
                    Description = department.Description,
                    CreatedDate = department.CreatedDate
                }

            ).ToList();

            DepartmentListDto response = new DepartmentListDto
            {
                Items = departmentList,
                Size = departmentPageable.Size,
                Index = departmentPageable.Index,
                Count = departmentPageable.Count,
                Pages = departmentPageable.Pages
            };
            return response;
        }
    }
}
