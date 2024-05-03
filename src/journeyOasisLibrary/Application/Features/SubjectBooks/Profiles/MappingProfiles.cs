using Application.Features.SubjectBooks.Commands.Create;
using Application.Features.SubjectBooks.Commands.Delete;
using Application.Features.SubjectBooks.Commands.Update;
using Application.Features.SubjectBooks.Queries.GetById;
using Application.Features.SubjectBooks.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.SubjectBooks.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<SubjectBook, CreateSubjectBookCommand>().ReverseMap();
        CreateMap<SubjectBook, CreatedSubjectBookResponse>().ReverseMap();
        CreateMap<SubjectBook, UpdateSubjectBookCommand>().ReverseMap();
        CreateMap<SubjectBook, UpdatedSubjectBookResponse>().ReverseMap();
        CreateMap<SubjectBook, DeleteSubjectBookCommand>().ReverseMap();
        CreateMap<SubjectBook, DeletedSubjectBookResponse>().ReverseMap();
        CreateMap<SubjectBook, GetByIdSubjectBookResponse>().ReverseMap();
        CreateMap<SubjectBook, GetListSubjectBookListItemDto>().ReverseMap();
        CreateMap<IPaginate<SubjectBook>, GetListResponse<GetListSubjectBookListItemDto>>().ReverseMap();
    }
}