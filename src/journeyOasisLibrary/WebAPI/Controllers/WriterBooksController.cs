using Application.Features.WriterBooks.Commands.Create;
using Application.Features.WriterBooks.Commands.Delete;
using Application.Features.WriterBooks.Commands.Update;
using Application.Features.WriterBooks.Queries.GetById;
using Application.Features.WriterBooks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WriterBooksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateWriterBookCommand createWriterBookCommand)
    {
        CreatedWriterBookResponse response = await Mediator.Send(createWriterBookCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateWriterBookCommand updateWriterBookCommand)
    {
        UpdatedWriterBookResponse response = await Mediator.Send(updateWriterBookCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedWriterBookResponse response = await Mediator.Send(new DeleteWriterBookCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdWriterBookResponse response = await Mediator.Send(new GetByIdWriterBookQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListWriterBookQuery getListWriterBookQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListWriterBookListItemDto> response = await Mediator.Send(getListWriterBookQuery);
        return Ok(response);
    }
}