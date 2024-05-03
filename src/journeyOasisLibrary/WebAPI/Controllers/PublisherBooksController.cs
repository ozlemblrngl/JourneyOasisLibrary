using Application.Features.PublisherBooks.Commands.Create;
using Application.Features.PublisherBooks.Commands.Delete;
using Application.Features.PublisherBooks.Commands.Update;
using Application.Features.PublisherBooks.Queries.GetById;
using Application.Features.PublisherBooks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublisherBooksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreatePublisherBookCommand createPublisherBookCommand)
    {
        CreatedPublisherBookResponse response = await Mediator.Send(createPublisherBookCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdatePublisherBookCommand updatePublisherBookCommand)
    {
        UpdatedPublisherBookResponse response = await Mediator.Send(updatePublisherBookCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedPublisherBookResponse response = await Mediator.Send(new DeletePublisherBookCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdPublisherBookResponse response = await Mediator.Send(new GetByIdPublisherBookQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListPublisherBookQuery getListPublisherBookQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListPublisherBookListItemDto> response = await Mediator.Send(getListPublisherBookQuery);
        return Ok(response);
    }
}