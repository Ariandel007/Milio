using System;
using System.ComponentModel.DataAnnotations;

namespace Milio.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(16, MinimumLength = 4, ErrorMessage = "Debes especificar una contraseña entre 4 y 16 caracteres")]
        public string Password { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string LastName { get; set; }        
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Country { get; set; }
        public DateTime Created { get; set; }
        public string Address { get; set; }
        public float FareForHour { get; set; }
        public DateTime LastActive { get; set; }

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }

    }
}