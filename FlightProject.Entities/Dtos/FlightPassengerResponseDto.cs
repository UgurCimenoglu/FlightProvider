using FlightProject.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightProject.Entities.Dtos
{
    public class FlightPassengerResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
    }
}
