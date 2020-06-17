using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Milio.API.Data;
using Milio.API.Dtos;
using Milio.API.Helpers;

namespace Milio.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersRepository _repo;
        private readonly IMapper _mapper;

        public UsersController(IUsersRepository repo, IMapper mapper)
        {
            this._mapper = mapper;
            this._repo = repo;
        }

        [Authorize(Policy = "RequireClientRole")]
        [HttpGet("getCarer/{id}", Name="GetCarer")]
        public async Task<IActionResult> GetCarer(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<CarerForDetailedtDto>(user);
            
            return Ok(userToReturn);
        }

        [Authorize(Policy = "RequireClientRole")]
        [HttpGet("getClient/{id}", Name="GetClient")]
        public async Task<IActionResult> GetClient(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<ClientForDetailedtDto>(user);
            
            return Ok(userToReturn);
        }

        [HttpGet("carers")]
        public async Task<IActionResult> GetCarers([FromQuery]UserParams userParams)
        {
            var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var userFromRepo = await _repo.GetUser(currentUserId);

            userParams.UserId = currentUserId;

            var users = await _repo.GetCarers(userParams);
    
            var usersToReturn = _mapper.Map<IEnumerable<CarerForListDto>>(users);

            //esto esta en el extensions
            Response.AddPagination(users.CurrentPage, users.PageSize, users.TotalCount, users.TotalPages);

            return Ok(usersToReturn);
        }
        
        [HttpPut("updateClient/{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody]ClientForUpdateDto clientForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(clientForUpdateDto, userFromRepo);

            if ( await _repo.SaveAll())
                return NoContent();
            
            throw new Exception("$Updating client {id} failed on save");
        }

        [HttpPut("updateCarer/{id}")]
        public async Task<IActionResult> UpdateCarer(int id, [FromBody]CarerForUpdateDto carerForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(carerForUpdateDto, userFromRepo);

            if ( await _repo.SaveAll())
                return NoContent();
            
            throw new Exception("$Updating carer {id} failed on save");
        }
    }

}