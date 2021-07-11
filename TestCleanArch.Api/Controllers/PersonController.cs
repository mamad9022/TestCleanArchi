using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Microsoft.AspNetCore.Cors;
using TestCleanArch.Application.Authorize;
using TestCleanArch.Application.Persons.Command.CreatePesron;
using TestCleanArch.Application.Persons.Command.DeletePerson;
using TestCleanArch.Application.Persons.Command.UpdatePerson;
using TestCleanArch.Application.Persons.Dtos;
using TestCleanArch.Application.Persons.Queries;

namespace TestCleanArch.Api.Controllers
{
    public class PersonController : Controller
    {
        protected readonly IMediator _mediator;
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [ProducesResponseType(typeof(List<PersonDto>), 200)]
        [EnableCors]
        [Authorize]
        [HttpGet("person/list")]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPersonListQuery {}, cancellationToken);

            return Ok(result);
        }

        [ProducesResponseType(typeof(PersonDto), 200)]
        [EnableCors]
        [Authorize]
        [HttpGet("person/{id}")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPersonDetailQuery { Id = id }, cancellationToken);

            return Ok(result);
        }

        [ProducesResponseType(typeof(PersonDto), 200)]
        [EnableCors]
        [Authorize]
        [HttpPost("person")]
        public async Task<IActionResult> Post(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }


        [ProducesResponseType(typeof(Guid), 200)]
        [EnableCors]
        [Authorize]
        [HttpPut("person")]
        public async Task<IActionResult> Update(UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }


        [ProducesResponseType(typeof(Guid), 200)]
        [EnableCors]
        [AuthorizeAttribute]
        [HttpDelete("person/{id}")]
        public async Task<IActionResult> Delete(DeletePersonCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return Ok(result);
        }
    }
}
