using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Core.DataAccess;
using FlightProject.DataAccess.Abstract;
using FlightProject.DataAccess.Context;
using Microsoft.EntityFrameworkCore;

namespace FlightProject.DataAccess.Concrete
{
    public class FlightDal : EfEntityRepositoryBase<Entities.Concrete.Flight>, IFlightDal
    {
        public FlightDal(FlightDbContext context) : base(context)
        {
        }
    }
}
