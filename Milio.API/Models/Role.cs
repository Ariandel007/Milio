using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Milio.API.Models;

namespace Milio.API.Models
{
    public class Role : IdentityRole<int>
    {
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}