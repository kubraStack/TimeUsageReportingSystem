using Application.Features.Employees.Command.ChangePasword;
using Application.Features.Employees.Command.Create;
using Application.Features.Employees.Command.Delete;
using Application.Features.Employees.Command.Update;
using Application.Features.Employees.Models;
using Application.Features.Employees.Queries.GetAllEmployee;
using Application.Features.Employees.Queries.GetEmployeeId;
using Application.Features.Employees.Queries.ReportQuery;
using Application.Features.Employees.Queries.SearchEmployee;
using Application.Models.Reports;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public EmployeeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //Ekleme
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateEmployeeCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        //Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateEmployeeCommand command)
        {
            UpdateEmployeeResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        //Silme softdelete
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeleteEmployeeCommand command = new DeleteEmployeeCommand { Id = id};
            DeleteEmployeeResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        //Şifre Güncelleme
        [HttpPut("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            ChangePasswordResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var query = new GetEmployeeByIdQuery { Id = id };
            EmployeeDetailDto employeeDetail = await _mediator.Send(query);
            return Ok(employeeDetail);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetEmployeeListQuery query)
        {
            EmployeeListDto employeeList = await _mediator.Send(query);
            return Ok(employeeList);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchEmployeesQuery query)
        {
           EmployeeListDto employeeList = await _mediator.Send(query);
           return Ok(employeeList);
        }

        [HttpGet("reports/monthly")]
        public async Task<IActionResult> GetMonthlyReport([FromQuery] GetEmployeeMonthlyReportQuery query)
        {
            List<ReportResultDto> reportData = await _mediator.Send(query);
            return Ok(reportData);
        }
    }
}
