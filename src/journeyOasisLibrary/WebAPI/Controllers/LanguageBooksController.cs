using Application.Features.LanguageBooks.Commands.Create;
using Application.Features.LanguageBooks.Commands.Delete;
using Application.Features.LanguageBooks.Commands.Update;
using Application.Features.LanguageBooks.Queries.GetById;
using Application.Features.LanguageBooks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LanguageBooksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateLanguageBookCommand createLanguageBookCommand)
    {
        CreatedLanguageBookResponse response = await Mediator.Send(createLanguageBookCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateLanguageBookCommand updateLanguageBookCommand)
    {
        UpdatedLanguageBookResponse response = await Mediator.Send(updateLanguageBookCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedLanguageBookResponse response = await Mediator.Send(new DeleteLanguageBookCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdLanguageBookResponse response = await Mediator.Send(new GetByIdLanguageBookQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListLanguageBookQuery getListLanguageBookQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListLanguageBookListItemDto> response = await Mediator.Send(getListLanguageBookQuery);
        return Ok(response);
    }
}