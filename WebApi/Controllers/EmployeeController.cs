using Application.Features.Employees.Command.Create;
using Application.Features.Employees.Command.Delete;
using Application.Features.Employees.Command.Update;
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            DeleteEmployeeCommand command = new DeleteEmployeeCommand { Id = id};
            DeleteEmployeeResponse response = await _mediator.Send(command);
            return Ok(response);
        }

    }
}
