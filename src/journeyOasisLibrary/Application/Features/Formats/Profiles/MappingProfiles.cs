using Application.Features.Formats.Commands.Create;
using Application.Features.Formats.Commands.Delete;
using Application.Features.Formats.Commands.Update;
using Application.Features.Formats.Queries.GetById;
using Application.Features.Formats.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Formats.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Format, CreateFormatCommand>().ReverseMap();
        CreateMap<Format, CreatedFormatResponse>().ReverseMap();
        CreateMap<Format, UpdateFormatCommand>().ReverseMap();
        CreateMap<Format, UpdatedFormatResponse>().ReverseMap();
        CreateMap<Format, DeleteFormatCommand>().ReverseMap();
        CreateMap<Format, DeletedFormatResponse>().ReverseMap();
        CreateMap<Format, GetByIdFormatResponse>().ReverseMap();
        CreateMap<Format, GetListFormatListItemDto>().ReverseMap();
        CreateMap<IPaginate<Format>, GetListResponse<GetListFormatListItemDto>>().ReverseMap();
    }
}