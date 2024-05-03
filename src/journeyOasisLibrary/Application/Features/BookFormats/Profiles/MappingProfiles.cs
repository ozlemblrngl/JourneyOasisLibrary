using Application.Features.BookFormats.Commands.Create;
using Application.Features.BookFormats.Commands.Delete;
using Application.Features.BookFormats.Commands.Update;
using Application.Features.BookFormats.Queries.GetById;
using Application.Features.BookFormats.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.BookFormats.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<BookFormat, CreateBookFormatCommand>().ReverseMap();
        CreateMap<BookFormat, CreatedBookFormatResponse>().ReverseMap();
        CreateMap<BookFormat, UpdateBookFormatCommand>().ReverseMap();
        CreateMap<BookFormat, UpdatedBookFormatResponse>().ReverseMap();
        CreateMap<BookFormat, DeleteBookFormatCommand>().ReverseMap();
        CreateMap<BookFormat, DeletedBookFormatResponse>().ReverseMap();
        CreateMap<BookFormat, GetByIdBookFormatResponse>().ReverseMap();
        CreateMap<BookFormat, GetListBookFormatListItemDto>().ReverseMap();
        CreateMap<IPaginate<BookFormat>, GetListResponse<GetListBookFormatListItemDto>>().ReverseMap();
    }
}