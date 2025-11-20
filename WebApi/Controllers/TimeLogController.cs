using Application.Features.TimeLog.Commands.CheckOut;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeLogController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TimeLogController(IMediator mediator)
        {
            _mediator = mediator;
        }

        //İşe Çıkış(check-out)
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutCommand checkOutCommand)
        {
            CheckOutResponse response = await _mediator.Send(checkOutCommand);
            return Ok(response);
        }

    }
}
