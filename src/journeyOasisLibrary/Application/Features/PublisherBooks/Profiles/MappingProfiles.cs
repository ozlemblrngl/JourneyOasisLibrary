using Application.Features.PublisherBooks.Commands.Create;
using Application.Features.PublisherBooks.Commands.Delete;
using Application.Features.PublisherBooks.Commands.Update;
using Application.Features.PublisherBooks.Queries.GetById;
using Application.Features.PublisherBooks.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.PublisherBooks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<PublisherBook, CreatePublisherBookCommand>().ReverseMap();
        CreateMap<PublisherBook, CreatedPublisherBookResponse>().ReverseMap();
        CreateMap<PublisherBook, UpdatePublisherBookCommand>().ReverseMap();
        CreateMap<PublisherBook, UpdatedPublisherBookResponse>().ReverseMap();
        CreateMap<PublisherBook, DeletePublisherBookCommand>().ReverseMap();
        CreateMap<PublisherBook, DeletedPublisherBookResponse>().ReverseMap();
        CreateMap<PublisherBook, GetByIdPublisherBookResponse>().ReverseMap();
        CreateMap<PublisherBook, GetListPublisherBookListItemDto>().ReverseMap();
        CreateMap<IPaginate<PublisherBook>, GetListResponse<GetListPublisherBookListItemDto>>().ReverseMap();
    }
}