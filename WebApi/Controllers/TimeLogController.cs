using Application.Features.TimeLog.Commands.CheckIn;
using Application.Features.TimeLog.Commands.CheckOut;
using Application.Features.TimeLog.Commands.Update;
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

        //İşe Giriş(check-in)
        [HttpPost("checkin")]
        public async Task<IActionResult> CheckIn([FromBody] CheckInCommand checkInCommand)
        {
            CheckInCommandResponse response = await _mediator.Send(checkInCommand);
            return Created("", response);

        }

        //İşe Çıkış(check-out)
        [HttpPost("checkout")]
        public async Task<IActionResult> CheckOut([FromBody] CheckOutCommand checkOutCommand)
        {
            CheckOutResponse response = await _mediator.Send(checkOutCommand);
            return Ok(response);
        }

        //Güncelleme
        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateTimeLogCommand updateTimeLogCommand)
        {
            UpdateTimeLogResponse response = await _mediator.Send(updateTimeLogCommand);
            return Ok(response);
        }
    }
}
