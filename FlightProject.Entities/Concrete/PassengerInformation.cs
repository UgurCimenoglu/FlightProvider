using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlightProject.Core.Entities;

namespace FlightProject.Entities.Concrete
{
    public class PassengerInformation : BaseEntity
    {
        public Guid FlightId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string? Description { get; set; }
        public Flight Flight { get; set; }
    }
}
