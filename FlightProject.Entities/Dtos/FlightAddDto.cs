using FlightProject.Core.Entities;
using FlightProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Entities.Dtos
{
    public class FlightAddDto
    {
        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string FlightNumber { get; set; }
        public decimal Price { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public FlightPassengerAddDto PassengerInformation { get; set; }
    }
}
