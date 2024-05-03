using Application.Features.Writers.Commands.Create;
using Application.Features.Writers.Commands.Delete;
using Application.Features.Writers.Commands.Update;
using Application.Features.Writers.Queries.GetById;
using Application.Features.Writers.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Writers.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Writer, CreateWriterCommand>().ReverseMap();
        CreateMap<Writer, CreatedWriterResponse>().ReverseMap();
        CreateMap<Writer, UpdateWriterCommand>().ReverseMap();
        CreateMap<Writer, UpdatedWriterResponse>().ReverseMap();
        CreateMap<Writer, DeleteWriterCommand>().ReverseMap();
        CreateMap<Writer, DeletedWriterResponse>().ReverseMap();
        CreateMap<Writer, GetByIdWriterResponse>().ReverseMap();
        CreateMap<Writer, GetListWriterListItemDto>().ReverseMap();
        CreateMap<IPaginate<Writer>, GetListResponse<GetListWriterListItemDto>>().ReverseMap();
    }
}