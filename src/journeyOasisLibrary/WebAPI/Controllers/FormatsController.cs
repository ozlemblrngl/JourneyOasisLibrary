using Application.Features.Formats.Commands.Create;
using Application.Features.Formats.Commands.Delete;
using Application.Features.Formats.Commands.Update;
using Application.Features.Formats.Queries.GetById;
using Application.Features.Formats.Queries.GetList;
using NArchitecture.Core.Application.Requests;
using NArchitecture.Core.Application.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FormatsController : BaseController
{
    [HttpPost]
    public async Task<IActionResult> Add([FromBody] CreateFormatCommand createFormatCommand)
    {
        CreatedFormatResponse response = await Mediator.Send(createFormatCommand);

        return Created(uri: "", response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateFormatCommand updateFormatCommand)
    {
        UpdatedFormatResponse response = await Mediator.Send(updateFormatCommand);

        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        DeletedFormatResponse response = await Mediator.Send(new DeleteFormatCommand { Id = id });

        return Ok(response);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById([FromRoute] Guid id)
    {
        GetByIdFormatResponse response = await Mediator.Send(new GetByIdFormatQuery { Id = id });
        return Ok(response);
    }

    [HttpGet]
    public async Task<IActionResult> GetList([FromQuery] PageRequest pageRequest)
    {
        GetListFormatQuery getListFormatQuery = new() { PageRequest = pageRequest };
        GetListResponse<GetListFormatListItemDto> response = await Mediator.Send(getListFormatQuery);
        return Ok(response);
    }
}