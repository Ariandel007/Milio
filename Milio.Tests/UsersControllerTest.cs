using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Milio.API.Controllers;
using Milio.API.Data;
using Milio.API.Dtos;
using Milio.API.Models;
using Moq;
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
    }
}
