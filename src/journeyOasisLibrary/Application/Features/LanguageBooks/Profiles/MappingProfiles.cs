using Application.Features.LanguageBooks.Commands.Create;
using Application.Features.LanguageBooks.Commands.Delete;
using Application.Features.LanguageBooks.Commands.Update;
using Application.Features.LanguageBooks.Queries.GetById;
using Application.Features.LanguageBooks.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.LanguageBooks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<LanguageBook, CreateLanguageBookCommand>().ReverseMap();
        CreateMap<LanguageBook, CreatedLanguageBookResponse>().ReverseMap();
        CreateMap<LanguageBook, UpdateLanguageBookCommand>().ReverseMap();
        CreateMap<LanguageBook, UpdatedLanguageBookResponse>().ReverseMap();
        CreateMap<LanguageBook, DeleteLanguageBookCommand>().ReverseMap();
        CreateMap<LanguageBook, DeletedLanguageBookResponse>().ReverseMap();
        CreateMap<LanguageBook, GetByIdLanguageBookResponse>().ReverseMap();
        CreateMap<LanguageBook, GetListLanguageBookListItemDto>().ReverseMap();
        CreateMap<IPaginate<LanguageBook>, GetListResponse<GetListLanguageBookListItemDto>>().ReverseMap();
    }
}