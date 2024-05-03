using Application.Features.Writers.Commands.Create;
using Application.Features.Writers.Commands.Delete;
using Application.Features.Writers.Commands.Update;
using Application.Features.Writers.Queries.GetById;
using Application.Features.Writers.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WritersController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateWriterCommand createWriterCommand)
    {
        CreatedWriterResponse response = await Mediator.Send(createWriterCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateWriterCommand updateWriterCommand)
    {
        UpdatedWriterResponse response = await Mediator.Send(updateWriterCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedWriterResponse response = await Mediator.Send(new DeleteWriterCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdWriterResponse response = await Mediator.Send(new GetByIdWriterQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListWriterQuery getListWriterQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListWriterListItemDto> response = await Mediator.Send(getListWriterQuery);
        return Ok(response);
    }
}