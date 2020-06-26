using System;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Milio.API.Dtos;
using Microsoft.AspNetCore.Mvc;
using Milio.API.Data;
using Milio.API.Models;
using Microsoft.AspNetCore.Authorization;
using Milio.API.Helpers;
using System.Collections.Generic;

namespace Milio.API.Controllers
{
    
    [Route("api/contracts/{userId}/[controller]")]
    [Authorize(Policy = "RequireUserRole")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentRepository _appointmentRepo;
        private readonly IUsersRepository _userRepo;
        private readonly IMapper _mapper;
        public AppointmentsController(IAppointmentRepository appointmentRepo, IUsersRepository userRepo, IMapper mapper)
        {
            _mapper = mapper;
            _appointmentRepo = appointmentRepo;
            _userRepo = userRepo;
        }

        [HttpGet("{id}", Name="GetAppointment")]
        public async Task<IActionResult> GetAppointment(int userId, int id)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var appointmentFromRepo = await _appointmentRepo.GetAppointment(id);

            if (appointmentFromRepo == null)
                return NotFound();

            return Ok(appointmentFromRepo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAppointment(int userId, [FromBody]AppointmentToCreateDto appointmentToCreateDto)
        {
            var client = await _userRepo.GetUser(userId);

            if (client.Id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            appointmentToCreateDto.ClientId = client.Id;

            var carer = await _userRepo.GetCarer(appointmentToCreateDto.CarerId);

            if (carer == null)
               return BadRequest("No se puedo encontrar al usuario");

            //diferencia de las fechas para asignar el cost
            TimeSpan difference  = appointmentToCreateDto.End - appointmentToCreateDto.Start;

            if(difference.Hours<= 0)
               return BadRequest("El intervalo de fechas es incorrecto");

            //seleccionar el costo
            appointmentToCreateDto.Cost = carer.FareForHour * difference.Hours;

            //mapear appoinment
            var appointment = _mapper.Map<Appointment>(appointmentToCreateDto);

            _appointmentRepo.Add(appointment);

            if (await _appointmentRepo.SaveAll())
            {
                var appointmentToReturn = _mapper.Map<AppointmentToReturnDto>(appointment);
                return CreatedAtRoute("GetAppointment", new {userId, id = appointment.Id}, appointmentToReturn);
            }

            throw new Exception("No se pudo crear el mensaje al guardar");

        }

        [Authorize(Policy = "RequireBabysitterRole")]
        [HttpPut("confirm/{id}")]
        public async Task<IActionResult> ConfirmAppointment(int userId, int id, [FromBody]AppointmentToCreateDto appointmentToCreateDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var appointmentFromRepo = await _appointmentRepo.GetAppointment(id);

            _mapper.Map(appointmentToCreateDto, appointmentFromRepo);

            if ( await _appointmentRepo.SaveAll())
                return NoContent();
            
            throw new Exception("$Updating appointment {id} failed on save");
        }


        [HttpGet]
        public async Task<IActionResult> GetAppointments(int userId)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var appointmentsFromRepo = await _appointmentRepo.GetAppointments(userId);

            if (appointmentsFromRepo == null)
                return NotFound();
            
            var myAppointments = _mapper.Map<IEnumerable<AppointmentToReturnDto>>(appointmentsFromRepo);

    
            return Ok(myAppointments);
        }


    }
}