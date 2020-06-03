namespace Milio.API.Models
{
    public class Carer : User
    {
        //los atirbutos de ID, UserName, PasswordSalt y PasswordHash ya estan heredados de IdentityUser
        public int? Attitude { get; set; }
    }

}