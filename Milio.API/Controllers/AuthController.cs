using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Milio.API.Dtos;
using Milio.API.Models;

namespace Milio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public AuthController(IConfiguration config, IMapper mapper,
                              UserManager<User> userManager, 
                              SignInManager<User> signInManager)
        {
            this._config = config;
            this._mapper = mapper;
            this._userManager = userManager;
            this._signInManager = signInManager;
        }

        [HttpPost("registerClient")]
        public async Task<IActionResult> RegisterClient(UserForRegisterDto userForRegisterDto)
        {
            //comprobamos que sea mayor de edad
            if(!this.userIsOverEighteen(userForRegisterDto.DateOfBirth))
                return BadRequest("users should be over 18 years old");

            //si es mayor de edad mapeamos el body, lo mapeamos a la entidad carer para luego asignarle un rol en la db
            var userToCreate = _mapper.Map<Client>(userForRegisterDto);

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            var userToReturn = _mapper.Map<ClientForDetailedtDto>(userToCreate);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userToCreate, "Client");

                return CreatedAtRoute("GetClient", new {controller = "Users", id = userToCreate.Id}, userToReturn);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("registerCarer")]
        public async Task<IActionResult> RegisterCarer(UserForRegisterDto userForRegisterDto)
        {
            //comprobamos que sea mayor de edad
            if(!this.userIsOverEighteen(userForRegisterDto.DateOfBirth))
                return BadRequest("users should be over 18 years old");
            
            //si es mayor de edad mapeamos el body, lo mapeamos a la entidad carer para luego asignarle un rol en la db
            var userToCreate = _mapper.Map<Carer>(userForRegisterDto);

            var result = await _userManager.CreateAsync(userToCreate, userForRegisterDto.Password);

            var userToReturn = _mapper.Map<CarerForDetailedtDto>(userToCreate);

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(userToCreate, "Babysitter");

                return CreatedAtRoute("GetCarer", new {controller = "Users", id = userToCreate.Id}, userToReturn);
            }

            return BadRequest(result.Errors);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForLoginDto userForLoginDto)
        {
            var user = await this._userManager.FindByNameAsync(userForLoginDto.UserName);

            var result = await this._signInManager.CheckPasswordSignInAsync(user, 
            userForLoginDto.Password, false);

            if (result.Succeeded)
            {
                var appUser = await _userManager.Users.Include(p => p.Photos)
                    .FirstOrDefaultAsync(u => u.NormalizedUserName == userForLoginDto.UserName.ToUpper());

                var userToReturn = _mapper.Map<UserForListDto>(appUser);

                return Ok(new
                {
                    token = GenerateJwToken(appUser).Result,
                    user = userToReturn
                });
            }

            return Unauthorized();

        }

        private async Task<string> GenerateJwToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            var roles = await _userManager.GetRolesAsync(user);

            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this._config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDesciptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDesciptor);

            return tokenHandler.WriteToken(token);
        }

        private bool userIsOverEighteen(DateTime birthDay)
        {
            var age = DateTime.Today.Year - birthDay.Year;

            if (birthDay.AddYears(age) > DateTime.Today)
                age--;

            if(age >=18)
                return true;

            return false;
        }
        
    }
}