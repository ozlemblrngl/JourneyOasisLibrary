using Application.Features.SubjectBooks.Commands.Create;
using Application.Features.SubjectBooks.Commands.Delete;
using Application.Features.SubjectBooks.Commands.Update;
using Application.Features.SubjectBooks.Queries.GetById;
using Application.Features.SubjectBooks.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubjectBooksController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSubjectBookCommand createSubjectBookCommand)
    {
        CreatedSubjectBookResponse response = await Mediator.Send(createSubjectBookCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSubjectBookCommand updateSubjectBookCommand)
    {
        UpdatedSubjectBookResponse response = await Mediator.Send(updateSubjectBookCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedSubjectBookResponse response = await Mediator.Send(new DeleteSubjectBookCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdSubjectBookResponse response = await Mediator.Send(new GetByIdSubjectBookQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSubjectBookQuery getListSubjectBookQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSubjectBookListItemDto> response = await Mediator.Send(getListSubjectBookQuery);
        return Ok(response);
    }
}