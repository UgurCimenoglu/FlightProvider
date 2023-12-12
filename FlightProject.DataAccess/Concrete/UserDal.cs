using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Core.Entities;
using FlightProject.DataAccess.Abstract;
using FlightProject.DataAccess.Context;
using FlightProject.Entities.Concrete;

namespace FlightProject.DataAccess.Concrete
{
    public class UserDal : EfEntityRepositoryBase<User>, IUserDal
    {
        public UserDal(FlightDbContext context) : base(context)
        {
        }
    }
}
