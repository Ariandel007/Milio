using System;
using System.Collections.Generic;

namespace Milio.API.Dtos
{
    public class AppointmentToCreateDto
    {
        public DateTime Start { get; set; }
        public DateTime End { get; set; } 
        // public bool Acepted { get; set; }
        public float? Cost { get; set; }
        public int CarerId  { get; set; }
        public int ClientId  { get; set; }

    }
}