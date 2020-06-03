namespace Milio.API.Models
{
    public class Client : User
    {
        //los atirbutos de ID, UserName, PasswordSalt y PasswordHash ya estan heredados de IdentityUser
        public int? NumberOfChildren { get; set; }
    }

}