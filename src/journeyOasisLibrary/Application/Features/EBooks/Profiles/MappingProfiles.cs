using Application.Features.EBooks.Commands.Create;
using Application.Features.EBooks.Commands.Delete;
using Application.Features.EBooks.Commands.Update;
using Application.Features.EBooks.Queries.GetById;
using Application.Features.EBooks.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.EBooks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<EBook, CreateEBookCommand>().ReverseMap();
        CreateMap<EBook, CreatedEBookResponse>().ReverseMap();
        CreateMap<EBook, UpdateEBookCommand>().ReverseMap();
        CreateMap<EBook, UpdatedEBookResponse>().ReverseMap();
        CreateMap<EBook, DeleteEBookCommand>().ReverseMap();
        CreateMap<EBook, DeletedEBookResponse>().ReverseMap();
        CreateMap<EBook, GetByIdEBookResponse>().ReverseMap();
        CreateMap<EBook, GetListEBookListItemDto>().ReverseMap();
        CreateMap<IPaginate<EBook>, GetListResponse<GetListEBookListItemDto>>().ReverseMap();
    }
}