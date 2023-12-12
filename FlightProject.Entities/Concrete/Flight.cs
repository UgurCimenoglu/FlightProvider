using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Core.Entities;

namespace FlightProject.Entities.Concrete
{
    public class Flight : BaseEntity
    {
        public Guid UserId { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string FlightNumber { get; set; }
        public decimal Price { get; set; }
        public User User { get; set; }
        public PassengerInformation PassengerInformation { get; set; }
    }
}