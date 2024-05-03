using Application.Features.AnalogueBooks.Commands.Create;
using Application.Features.AnalogueBooks.Commands.Delete;
using Application.Features.AnalogueBooks.Commands.Update;
using Application.Features.AnalogueBooks.Queries.GetById;
using Application.Features.AnalogueBooks.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.AnalogueBooks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<AnalogueBook, CreateAnalogueBookCommand>().ReverseMap();
        CreateMap<AnalogueBook, CreatedAnalogueBookResponse>().ReverseMap();
        CreateMap<AnalogueBook, UpdateAnalogueBookCommand>().ReverseMap();
        CreateMap<AnalogueBook, UpdatedAnalogueBookResponse>().ReverseMap();
        CreateMap<AnalogueBook, DeleteAnalogueBookCommand>().ReverseMap();
        CreateMap<AnalogueBook, DeletedAnalogueBookResponse>().ReverseMap();
        CreateMap<AnalogueBook, GetByIdAnalogueBookResponse>().ReverseMap();
        CreateMap<AnalogueBook, GetListAnalogueBookListItemDto>().ReverseMap();
        CreateMap<IPaginate<AnalogueBook>, GetListResponse<GetListAnalogueBookListItemDto>>().ReverseMap();
    }
}