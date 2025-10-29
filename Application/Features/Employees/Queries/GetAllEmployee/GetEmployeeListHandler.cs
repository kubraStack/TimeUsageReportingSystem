using Application.Features.Employees.Models;
using Application.Repositories.EmployeeRepo;
using Core.Utilities.Encryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetAllEmployee
{
    public class GetEmployeeListHandler : IRequestHandler<GetEmployeeListQuery, EmployeeListDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EncryptionHelper _encryptionHelper;

        public GetEmployeeListHandler(IEmployeeRepository employeeRepository, EncryptionHelper encryptionHelper)
        {
            _employeeRepository = employeeRepository;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<EmployeeListDto> Handle(GetEmployeeListQuery request, CancellationToken cancellationToken)
        {
            //Repository'den sayfalanmış verileri çek
            var employeesPageable = await _employeeRepository.GetListAsync(
                    //filtreleme
                    predicate: e => e.IsDeleted == false,
                    //sıralama
                    orderBy: q => q.OrderBy(e => e.Id),
                    //sayfalama
                    index: request.PageRequest.Page,
                    size: request.PageRequest.PageSize,
                    cancellationToken: cancellationToken
            );

            //şifre çözme ve dto'ya dönüştürme
            var employeeList = employeesPageable.Items.Select(
                employee => new EmployeeDetailDto
                {
                    Id = employee.Id,
                    FirstName =  _encryptionHelper.DecryptString(employee.EncryptedFirstName),
                    LastName = _encryptionHelper.DecryptString(employee.EncryptedLastName),
                    Email = employee.Email,
                    Role = employee.Role,
                    DepartmentId = employee.DepartmentId
                }
            ).ToList();

            //list dto'sunu oluşturma
            EmployeeListDto response = new EmployeeListDto
            {
                Items = employeeList,
                Index = employeesPageable.Index,
                Size = employeesPageable.Size,
                Count = employeesPageable.Count,
                Pages = employeesPageable.Pages
            };
            return response;
        }
    }
}
