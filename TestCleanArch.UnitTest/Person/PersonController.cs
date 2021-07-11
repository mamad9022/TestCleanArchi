using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using TestCleanArch.Application.Common.Exceptions;
using TestCleanArch.Application.Persons.Command.DeletePerson;
using TestCleanArch.Application.Persons.Command.UpdatePerson;
using TestCleanArch.Application.Persons.Dtos;
using TestCleanArch.Application.Persons.Queries;
using Xunit;

namespace TestCleanArch.UnitTest.Person
{
   public class PersonController: BaseConfiguration
    {
        [Fact]
        public async Task WhenPersonListCall_ReturnOkResult()
        {
            using var controller = new BaseConfiguration().BuildPersonsController();

            var result = await controller.GetList(It.IsAny<CancellationToken>());

            var objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact]
        public async Task WhenInvalidIdSend_returnNotFound()
        {
            var mockData = new Mock<IMediator>();
            mockData.Setup(x => x.Send(It.IsAny<GetPersonDetailQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((PersonDto)null);

            using var controller =
                new BaseConfiguration().WithMediatorService(mockData.Object).BuildPersonsController();

            var result = await controller.Get(It.IsAny<Guid>(), It.IsAny<CancellationToken>());

            Assert.Equal(StatusCodes.Status404NotFound, StatusCodes.Status404NotFound);
        }

        [Fact]
        public async Task WhenValidIdSend_RemovePersonSuccessfully()
        {
            var mockData = new Mock<IMediator>();
            Guid id = Guid.NewGuid();

            mockData.Setup(x => x.Send(It.IsAny<DeletePersonCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(id);

            using var controller =
                new BaseConfiguration().WithMediatorService(mockData.Object).BuildPersonsController();

            var result = await controller.Delete(It.IsAny<DeletePersonCommand>(), It.IsAny<CancellationToken>());

            var objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }

        [Fact]
        public async Task WhenValidDataSendForUpdate_UpdatePersonSuccessfully()
        {
            var mockData = new Mock<IMediator>();
            Guid id = Guid.NewGuid();
            mockData.Setup(x => x.Send(It.IsAny<UpdatePersonCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(id);

            using var controller =
                new BaseConfiguration().WithMediatorService(mockData.Object).BuildPersonsController();

            var result = await controller.Update(It.IsAny<UpdatePersonCommand>(), It.IsAny<CancellationToken>());

            var objectResult = Assert.IsType<OkObjectResult>(result);

            Assert.Equal(StatusCodes.Status200OK, objectResult.StatusCode);
        }
    }
}
