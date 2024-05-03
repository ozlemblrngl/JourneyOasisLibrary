using Application.Features.TranslatorBooks.Commands.Create;
using Application.Features.TranslatorBooks.Commands.Delete;
using Application.Features.TranslatorBooks.Commands.Update;
using Application.Features.TranslatorBooks.Queries.GetById;
using Application.Features.TranslatorBooks.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.TranslatorBooks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<TranslatorBook, CreateTranslatorBookCommand>().ReverseMap();
        CreateMap<TranslatorBook, CreatedTranslatorBookResponse>().ReverseMap();
        CreateMap<TranslatorBook, UpdateTranslatorBookCommand>().ReverseMap();
        CreateMap<TranslatorBook, UpdatedTranslatorBookResponse>().ReverseMap();
        CreateMap<TranslatorBook, DeleteTranslatorBookCommand>().ReverseMap();
        CreateMap<TranslatorBook, DeletedTranslatorBookResponse>().ReverseMap();
        CreateMap<TranslatorBook, GetByIdTranslatorBookResponse>().ReverseMap();
        CreateMap<TranslatorBook, GetListTranslatorBookListItemDto>().ReverseMap();
        CreateMap<IPaginate<TranslatorBook>, GetListResponse<GetListTranslatorBookListItemDto>>().ReverseMap();
    }
}