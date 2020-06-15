using System.Collections.Generic;

namespace Milio.API.Models
{
    public class Carer : User
    {
        //los atirbutos de ID, UserName, PasswordSalt y PasswordHash ya estan heredados de IdentityUser
        public float FareForHour { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }
        public virtual Document Document { get; set; }
    }

}