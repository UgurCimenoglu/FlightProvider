using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace FlightProject.DataAccess.Extensions
{
    static class Configuration
    {
        public static string ConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                try
                {
                    configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../FlightProject.WebAPI"));
                    configurationManager.AddJsonFile("appsettings.json");
                    return configurationManager.GetConnectionString("FlightDb");
                }
                catch
                {
                    configurationManager.AddJsonFile("appsettings.Production.json");
                }
                return configurationManager.GetConnectionString("FlightDb");
            }
        }
    }
}
