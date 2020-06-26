using System;

namespace Milio.API.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; } 
        public bool Acepted { get; set; }
        public float Cost { get; set; }
        public virtual Carer Carer { get; set; }
        public int CarerId  { get; set; }
        public virtual Client Client { get; set; }
        public int ClientId  { get; set; } 

    }
}