using Application.Features.Employees.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Employees.Queries.GetEmployeeId
{
    public class GetEmployeeByIdQuery : IRequest<EmployeeDetailDto>
    {
        public int Id { get; set; }
    }
}
