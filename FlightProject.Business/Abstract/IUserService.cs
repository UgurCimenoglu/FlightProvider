using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Core.Entities;
using FlightProject.Core.Utilities.Results;

namespace FlightProject.Business.Abstract
{
    public interface IUserService
    {
        Task<IResult> AddAsync(User user);
        Task<IDataResult<User>> FindByIdAsync(string id);
        Task<IDataResult<User?>> FindByEmailAsync(string email);
    }
}
