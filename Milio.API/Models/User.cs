using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Milio.API.Models
{
    public class User : IdentityUser<int>
    {
        //los atirbutos de ID, UserName, PasswordSalt y PasswordHash ya estan heredados de IdentityUser
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string AboutMe { get; set; }
         public string City { get; set; }
        public string Country { get; set; }
        //lo hacemos virtual para habilitar lazy loading
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<Message> MessagesSent { get; set; }
        public virtual ICollection<Message> MessagesReceived { get; set; }

        
    }

}