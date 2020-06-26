using System.Collections.Generic;

namespace Milio.API.Models
{
    public class Client : User
    {
        //los atirbutos de ID, UserName, PasswordSalt y PasswordHash ya estan heredados de IdentityUser
        public string Address { get; set; }
        public virtual ICollection<Appointment> Appointments { get; set; }

    }

}