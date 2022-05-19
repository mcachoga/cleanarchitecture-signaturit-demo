using Microsoft.AspNetCore.Mvc;
using Signaturit.API.Controllers;
using Signaturit.Application.Features.Trials.Commands.Create;
using Signaturit.Application.Features.Trials.Commands.Delete;
using Signaturit.Application.Features.Trials.Commands.Update;
using Signaturit.Application.Features.Trials.Queries.GetAllCached;
using Signaturit.Application.Features.Trials.Queries.GetById;
using System.Threading.Tasks;

namespace Signaturit.Api.Controllers.v1
{
    public class TrialController : BaseApiController<TrialController>
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trials = await _mediator.Send(new GetAllTrialsCachedQuery());
            return Ok(trials);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var trial = await _mediator.Send(new GetTrialByIdQuery() { Id = id });
            return Ok(trial);
        }

        [HttpPost]
        public async Task<IActionResult> Post(CreateTrialCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, UpdateTrialCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return Ok(await _mediator.Send(new DeleteTrialCommand { Id = id }));
        }
    }
}