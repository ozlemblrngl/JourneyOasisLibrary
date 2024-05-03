using Application.Features.TranslatorBooks.Commands.Create;
using Application.Features.TranslatorBooks.Commands.Delete;
using Application.Features.TranslatorBooks.Commands.Update;
using Application.Features.TranslatorBooks.Queries.GetById;
using Application.Features.TranslatorBooks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TranslatorBooksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateTranslatorBookCommand createTranslatorBookCommand)
    {
        CreatedTranslatorBookResponse response = await Mediator.Send(createTranslatorBookCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateTranslatorBookCommand updateTranslatorBookCommand)
    {
        UpdatedTranslatorBookResponse response = await Mediator.Send(updateTranslatorBookCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedTranslatorBookResponse response = await Mediator.Send(new DeleteTranslatorBookCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdTranslatorBookResponse response = await Mediator.Send(new GetByIdTranslatorBookQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListTranslatorBookQuery getListTranslatorBookQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListTranslatorBookListItemDto> response = await Mediator.Send(getListTranslatorBookQuery);
        return Ok(response);
    }
}