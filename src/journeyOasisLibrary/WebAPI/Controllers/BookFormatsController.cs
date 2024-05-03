using Application.Features.BookFormats.Commands.Create;
using Application.Features.BookFormats.Commands.Delete;
using Application.Features.BookFormats.Commands.Update;
using Application.Features.BookFormats.Queries.GetById;
using Application.Features.BookFormats.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookFormatsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateBookFormatCommand createBookFormatCommand)
    {
        CreatedBookFormatResponse response = await Mediator.Send(createBookFormatCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateBookFormatCommand updateBookFormatCommand)
    {
        UpdatedBookFormatResponse response = await Mediator.Send(updateBookFormatCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedBookFormatResponse response = await Mediator.Send(new DeleteBookFormatCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdBookFormatResponse response = await Mediator.Send(new GetByIdBookFormatQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListBookFormatQuery getListBookFormatQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListBookFormatListItemDto> response = await Mediator.Send(getListBookFormatQuery);
        return Ok(response);
    }
}