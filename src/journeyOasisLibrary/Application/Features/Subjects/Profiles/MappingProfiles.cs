using Application.Features.Subjects.Commands.Create;
using Application.Features.Subjects.Commands.Delete;
using Application.Features.Subjects.Commands.Update;
using Application.Features.Subjects.Queries.GetById;
using Application.Features.Subjects.Queries.GetList;
using AutoMapper;
using NArchitecture.Core.Application.Responses;
using Domain.Entities;
using NArchitecture.Core.Persistence.Paging;

namespace Application.Features.Subjects.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Subject, CreateSubjectCommand>().ReverseMap();
        CreateMap<Subject, CreatedSubjectResponse>().ReverseMap();
        CreateMap<Subject, UpdateSubjectCommand>().ReverseMap();
        CreateMap<Subject, UpdatedSubjectResponse>().ReverseMap();
        CreateMap<Subject, DeleteSubjectCommand>().ReverseMap();
        CreateMap<Subject, DeletedSubjectResponse>().ReverseMap();
        CreateMap<Subject, GetByIdSubjectResponse>().ReverseMap();
        CreateMap<Subject, GetListSubjectListItemDto>().ReverseMap();
        CreateMap<IPaginate<Subject>, GetListResponse<GetListSubjectListItemDto>>().ReverseMap();
    }
}