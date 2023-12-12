using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Core.Extensions
{
    public class ErrorResponse
    {
        public string? Message { get; set; }
        public bool Success { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
