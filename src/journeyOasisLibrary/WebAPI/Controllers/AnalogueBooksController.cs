using Application.Features.AnalogueBooks.Commands.Create;
using Application.Features.AnalogueBooks.Commands.Delete;
using Application.Features.AnalogueBooks.Commands.Update;
using Application.Features.AnalogueBooks.Queries.GetById;
using Application.Features.AnalogueBooks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnalogueBooksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateAnalogueBookCommand createAnalogueBookCommand)
    {
        CreatedAnalogueBookResponse response = await Mediator.Send(createAnalogueBookCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateAnalogueBookCommand updateAnalogueBookCommand)
    {
        UpdatedAnalogueBookResponse response = await Mediator.Send(updateAnalogueBookCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedAnalogueBookResponse response = await Mediator.Send(new DeleteAnalogueBookCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdAnalogueBookResponse response = await Mediator.Send(new GetByIdAnalogueBookQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListAnalogueBookQuery getListAnalogueBookQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListAnalogueBookListItemDto> response = await Mediator.Send(getListAnalogueBookQuery);
        return Ok(response);
    }
}