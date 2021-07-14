using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using TestCleanArch.Application.Persons.Command.CreatePesron;
using TestCleanArch.Application.Persons.Command.DeletePerson;
using TestCleanArch.Application.Persons.Command.UpdatePerson;
using TestCleanArch.Application.Persons.Dtos;
using TestCleanArch.Application.Persons.Queries;
using TestCleanArch.Common.Helper.Messages;

namespace TestCleanArch.Api.Controllers
{
    public class PersonController : BaseController
    {
        protected readonly IMediator _mediator;
        public PersonController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// list of petson
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>

        [ProducesResponseType(typeof(List<PersonDto>), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("list")]
        public async Task<IActionResult> GetList(CancellationToken cancellationToken) =>
             Ok(
               await _mediator.Send(new GetPersonListQuery
               {
               }, cancellationToken));


        /// <summary>
        /// get person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PersonDto), 200)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpGet("{id}", Name = "GetPersonInfo")]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetPersonDetailQuery { Id = id }, cancellationToken);
            if (result.Success == false)
                return result.ApiResult;
            return Ok(result);
        }


        /// <summary>
        /// add person
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(typeof(PersonDto), 201)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPost]
        public async Task<IActionResult> Create(CreatePersonCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(Get), new { result.Data.Id }, result.Data);
        }


        /// <summary>
        /// edit person
        /// </summary>
        /// <param name="command"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpPut]
        public async Task<IActionResult> Update(UpdatePersonCommand command, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(command, cancellationToken);
            if (result.Success == false)
                return result.ApiResult;
            return NoContent();
        }



        /// <summary>
        /// delete person
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [ProducesResponseType(204)]
        [ProducesResponseType(typeof(ApiMessage), 400)]
        [ProducesResponseType(typeof(ApiMessage), 500)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeletePersonCommand { Id = id }, cancellationToken);
            if (result.Success == false)
                return result.ApiResult;
            return NoContent();
        }
    }
}
