using Application.Features.Auth.Dtos;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Login
{
    public class LoginCommand : IRequest<LoggedResponse>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
