using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Milio.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Milio.API.Data;
using Milio.API.Models;
using Microsoft.AspNetCore.Authorization;

namespace Milio.API.Controllers
{
    
    [Route("api/chat/{userId}/[controller]")]
    [Authorize(Policy = "RequireUserRole")]
    [ApiController]
    public class MessagesController : ControllerBase
    {
        private readonly IMessagesRepository _messageRepo;
        private readonly IUsersRepository _userRepo;
        private readonly IMapper _mapper;
        public MessagesController(IMessagesRepository messageRepo, IUsersRepository userRepo, IMapper mapper)
        {
            _mapper = mapper;
            _messageRepo = messageRepo;
            _userRepo = userRepo;
        }

        [HttpGet("{id}", Name="GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var messageFromRepo = await _messageRepo.GetMessage(id);

            if (messageFromRepo == null)
                return NotFound();

            return Ok(messageFromRepo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreationDto messageForCreationDto)
        {
            //obetener usuario que envio el mensaje
            var sender = await _userRepo.GetUser(userId);

            if (sender.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            messageForCreationDto.SenderId = userId;

            var recipient = await _userRepo.GetUser(messageForCreationDto.RecipientId);

            if (recipient == null)
               return BadRequest("No se puedo encontrar al usuario");

            var message = _mapper.Map<Message>(messageForCreationDto);

            _messageRepo.Add(message);

            if (await _messageRepo.SaveAll())
            {
                var messageToReturn = _mapper.Map<MessageToReturnDto>(message);
                return CreatedAtRoute("GetMessage", new {userId, id = message.Id}, messageToReturn);
            }

            throw new Exception("No se pudo crear el mensaje al guardar");
        }

    }
}