using MediatR;
using Moq;
using TestCleanArch.Api.Controllers;

namespace TestCleanArch.UnitTest
{
    public class BaseConfiguration
    {
        private IMediator _mediator;

        public BaseConfiguration()
        {
            _mediator = new Mock<IMediator>().Object;
        }

        internal BaseConfiguration WithMediatorService(IMediator mediator)
        {
            _mediator = mediator;
            return this;
        }

        internal PersonController BuildPersonsController() => new(_mediator);

    }
}
