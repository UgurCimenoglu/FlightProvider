using System;
using System.Collections.Generic;
using System.Text;
using FlightProject.Core.Entities;

namespace FlightProject.Core.Utilities.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user);
    }
}
