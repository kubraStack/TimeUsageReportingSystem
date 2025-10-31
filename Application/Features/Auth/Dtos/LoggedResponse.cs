using Core.Utilities.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Dtos
{
    public class LoggedResponse
    {
        public AccessToken AccessToken { get; set; }
        
    }
}
