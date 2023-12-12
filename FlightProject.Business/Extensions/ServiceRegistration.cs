using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Business.Abstract;
using FlightProject.Business.Concrete;
using FlightProject.Core.Utilities.Security.JWT;
using FlightServiceReference;
using Microsoft.Extensions.DependencyInjection;

namespace FlightProject.Business.Extensions
{
    public static class ServiceRegistration
    {
        public static void AddBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IFlightService, FlightManager>();
            services.AddScoped<IUserService, UserManager>();
            services.AddSingleton<IAirSearch, AirSearchClient>();
            services.AddScoped<ITokenHelper, JwtHelper>();
            services.AddScoped<IAuthService, AuthManager>();
        }
    }
}
