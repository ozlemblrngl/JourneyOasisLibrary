using Application.Features.EBooks.Commands.Create;
using Application.Features.EBooks.Commands.Delete;
using Application.Features.EBooks.Commands.Update;
using Application.Features.EBooks.Queries.GetById;
using Application.Features.EBooks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EBooksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateEBookCommand createEBookCommand)
    {
        CreatedEBookResponse response = await Mediator.Send(createEBookCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateEBookCommand updateEBookCommand)
    {
        UpdatedEBookResponse response = await Mediator.Send(updateEBookCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedEBookResponse response = await Mediator.Send(new DeleteEBookCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdEBookResponse response = await Mediator.Send(new GetByIdEBookQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListEBookQuery getListEBookQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListEBookListItemDto> response = await Mediator.Send(getListEBookQuery);
        return Ok(response);
    }
}