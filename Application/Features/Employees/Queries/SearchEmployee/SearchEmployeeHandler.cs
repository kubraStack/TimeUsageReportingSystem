using Application.Features.Employees.Models;
using Application.Repositories.EmployeeRepo;
using Core.Utilities.Encryption;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.SearchEmployee
{
    public class SearchEmployeeHandler : IRequestHandler<SearchEmployeesQuery, EmployeeListDto>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly EncryptionHelper _encryptionHelper;

        public SearchEmployeeHandler(IEmployeeRepository employeeRepository, EncryptionHelper encryptionHelper)
        {
            _employeeRepository = employeeRepository;
            _encryptionHelper = encryptionHelper;
        }

        public async Task<EmployeeListDto> Handle(SearchEmployeesQuery request, CancellationToken cancellationToken)
        {
            //Arama terimini küçük harfe çevirip boşlukları kaldırma
            string searchTerm = (request.SearchTerm ?? string.Empty).Trim().ToLower();

            //Repository'e gönderilecek filtreleme koşulu
            var employeePageable = await _employeeRepository.GetListAsync(
                
                predicate: e => e.IsDeleted == false &&
                    (
                       string.IsNullOrEmpty(searchTerm) ||
                       e.EncryptedFirstName.ToLower().Contains(searchTerm) ||
                       e.EncryptedLastName.ToLower().Contains(searchTerm) ||
                       e.Email.ToLower().Contains(searchTerm) 
                    ),
               //sıralama
               orderBy: e => e.OrderBy(e => e.Id),

               //sayfalama
               index: request.PageRequest.Page,
               size: request.PageRequest.PageSize,
               cancellationToken: cancellationToken
            );
            //şifre çözme ve dto'ya dönüştürme
            var employeeList = employeePageable.Items.Select(employee => new EmployeeDetailDto
                {
                    Id = employee.Id,
                    FirstName = _encryptionHelper.DecryptString(employee.EncryptedFirstName),
                    LastName = _encryptionHelper.DecryptString(employee.EncryptedLastName),
                    Email = employee.Email,
                    Role = employee.Role,
                    DepartmentId = employee.DepartmentId
                }
            ).ToList();

            EmployeeListDto response = new EmployeeListDto
            {
                Items = employeeList,
                Count = employeePageable.Count,
                Size = employeePageable.Size,
                Index = employeePageable.Index,
                Pages = (int)Math.Ceiling((double)employeePageable.Count / employeePageable.Size)
            };
            return response;
        }
    }
}
