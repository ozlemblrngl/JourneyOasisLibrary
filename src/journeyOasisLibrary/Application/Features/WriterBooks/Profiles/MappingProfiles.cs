using Application.Features.WriterBooks.Commands.Create;
using Application.Features.WriterBooks.Commands.Delete;
using Application.Features.WriterBooks.Commands.Update;
using Application.Features.WriterBooks.Queries.GetById;
using Application.Features.WriterBooks.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.WriterBooks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<WriterBook, CreateWriterBookCommand>().ReverseMap();
        CreateMap<WriterBook, CreatedWriterBookResponse>().ReverseMap();
        CreateMap<WriterBook, UpdateWriterBookCommand>().ReverseMap();
        CreateMap<WriterBook, UpdatedWriterBookResponse>().ReverseMap();
        CreateMap<WriterBook, DeleteWriterBookCommand>().ReverseMap();
        CreateMap<WriterBook, DeletedWriterBookResponse>().ReverseMap();
        CreateMap<WriterBook, GetByIdWriterBookResponse>().ReverseMap();
        CreateMap<WriterBook, GetListWriterBookListItemDto>().ReverseMap();
        CreateMap<IPaginate<WriterBook>, GetListResponse<GetListWriterBookListItemDto>>().ReverseMap();
    }
}