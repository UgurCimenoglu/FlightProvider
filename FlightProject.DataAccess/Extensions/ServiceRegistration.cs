using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.DataAccess.Abstract;
using FlightProject.DataAccess.Concrete;
using FlightProject.DataAccess.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FlightProject.DataAccess.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IFlightDal, FlightDal>();
            services.AddScoped<IUserDal, UserDal>();
        }
    }
}
