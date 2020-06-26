using System;
using System.Collections.Generic;

namespace Milio.API.Dtos
{
    public class AppointmentToReturnDto
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; } 
        public float Cost { get; set; }
        public int CarerId  { get; set; }
        public int ClientId  { get; set; }

    }
}