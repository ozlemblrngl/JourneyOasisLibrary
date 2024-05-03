using Application.Features.Subjects.Commands.Create;
using Application.Features.Subjects.Commands.Delete;
using Application.Features.Subjects.Commands.Update;
using Application.Features.Subjects.Queries.GetById;
using Application.Features.Subjects.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SubjectsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateSubjectCommand createSubjectCommand)
    {
        CreatedSubjectResponse response = await Mediator.Send(createSubjectCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateSubjectCommand updateSubjectCommand)
    {
        UpdatedSubjectResponse response = await Mediator.Send(updateSubjectCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedSubjectResponse response = await Mediator.Send(new DeleteSubjectCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdSubjectResponse response = await Mediator.Send(new GetByIdSubjectQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListSubjectQuery getListSubjectQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListSubjectListItemDto> response = await Mediator.Send(getListSubjectQuery);
        return Ok(response);
    }
}