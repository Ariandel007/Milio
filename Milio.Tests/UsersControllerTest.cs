using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Milio.API.Controllers;
using Milio.API.Data;
using Milio.API.Dtos;
using Milio.API.Models;
using Moq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Milio.Tests
{
    [TestClass]
    public class UsersControllerTest
    {
        [TestMethod]
        public async Task GetCarerExiste()
        {
            //preparacion
            var idCarer = 1; 
            
            var mockUserRepo = new Mock<IUsersRepository>();
            mockUserRepo.Setup(x => x.GetUser(idCarer)).Returns(Task.FromResult(default(User)));

            var mockMapper = new Mock<IMapper>();
            mockMapper.Setup(x => x.Map<CarerForDetailedtDto>(default(User))).Returns(default(CarerForDetailedtDto));

            var usersController = new UsersController(mockUserRepo.Object, mockMapper.Object);

            //prueba
            var resultado = usersController.GetCarer(idCarer);

            //verificacion
            Assert.IsInstanceOfType(resultado.Result, typeof(NotFoundResult));

        }

        //[TestMethod]
        //public async Task CreateAppointmentInvalidUser()
        //{
        //    var userTest = new User
        //    {
        //        Id = 2
        //    };

        //    //preparacion
        //    var userId = 1;
        //    //mocking claims
        //    //ClaimsPrincipal Controller.bas

        //    //mocking appointments repo
        //    var mockAppointmentUserRepo = new Mock<IAppointmentRepository>();
        //    mockAppointmentUserRepo.Setup(x=> x.SaveAll()).Returns(Task.FromResult(true));

        //    //mocking users repo
        //    var mockUserRepo = new Mock<IUsersRepository>();
        //    mockUserRepo.Setup(x => x.GetUser(userId)).Returns(Task.FromResult(userTest));

        //    //mocking messages
        //    var mockMessageRepo = new Mock<IMessagesRepository>();

        //    var mockMapper = new Mock<IMapper>();
        //    mockMapper.Setup(x => x.Map<CarerForDetailedtDto>(default(User))).Returns(default(CarerForDetailedtDto));
        //    //var appointment = _mapper.Map<Appointment>(appointmentToCreateDto);




        //    var appointmentsController = new AppointmentsController(mockAppointmentUserRepo.Object, mockUserRepo.Object, mockMapper.Object, mockMessageRepo.Object);

        //    appointmentsController.ControllerContext = new ControllerContext();
        //    appointmentsController.ControllerContext.HttpContext = new DefaultHttpContext();

        //    //prueba
        //    var resultado = appointmentsController.CreateAppointment(1, default(AppointmentToCreateDto));

        //    Assert.IsInstanceOfType(resultado.Result, typeof(UnauthorizedResult));
        //}

    }
}
