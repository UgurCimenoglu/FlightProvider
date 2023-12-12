using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Core.DataAccess;
using FlightProject.Entities.Concrete;

namespace FlightProject.DataAccess.Abstract
{
    public interface IFlightDal : IEntityRepository<Flight>
    {
    }
}
