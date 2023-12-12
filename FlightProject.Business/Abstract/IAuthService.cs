using FlightProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Core.Utilities.Results;
using FlightProject.Entities.Dtos;
using FlightProject.Core.Entities;
using FlightProject.Core.Utilities.Security.JWT;
using FlightProject.Entities.Dtos;

namespace FlightProject.Business.Abstract
{
    public interface IAuthService
    {
        Task<IResult> RegisterAsync(UserForRegisterDto userForRegisterDto, string password);
        Task<IDataResult<User>> LoginAsync(UserForLoginDto userForLoginDto);
        Task<IResult> UserExists(string email);
        IDataResult<AccessToken> CreateAccessToken(User user);
    }
}
