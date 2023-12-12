using System;
using System.Collections.Generic;
using System.Text;

namespace FlightProject.Entities.Dtos
{
    public class UserDetailDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}
