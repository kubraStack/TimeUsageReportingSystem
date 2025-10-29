using Application.Features.Department.Command.Create;
using Application.Features.Department.Command.Delete;
using Application.Features.Department.Command.Update;
using Application.Features.Department.Models;
using Application.Features.Department.Queries.GetAllDepartment;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateDepartmentCommand command)
        {
          var response = await  _mediator.Send(command);
          return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDepartmentCommand command)
        {
            UpdateDepartmentResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeleteDepartmentCommand command = new DeleteDepartmentCommand { Id = id };
            DeleteDepartmentResponse response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetList([FromQuery] GetAllDepartmentsQuery query)
        {
            DepartmentListDto departmentList = await _mediator.Send(query);
            return Ok(departmentList);
        }


    }
}
