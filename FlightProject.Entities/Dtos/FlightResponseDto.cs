using FlightProject.Core.Entities;
using FlightProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Entities.Dtos
{
    public class FlightResponseDto
    {

        public Guid Id { get; set; }
        public DateTime ArrivalDateTime { get; set; }
        public DateTime DepartureDateTime { get; set; }
        public string FlightNumber { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public bool IsActive { get; set; }
        public UserDetailDto User { get; set; }
        public FlightPassengerResponseDto PassengerInformation { get; set; }
    }
}
