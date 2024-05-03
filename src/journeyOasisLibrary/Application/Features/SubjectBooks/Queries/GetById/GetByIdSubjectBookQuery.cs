using Application.Features.SubjectBooks.Constants;
using Application.Features.SubjectBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.SubjectBooks.Constants.SubjectBooksOperationClaims;

namespace Application.Features.SubjectBooks.Queries.GetById;

public class GetByIdSubjectBookQuery : IRequest<GetByIdSubjectBookResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdSubjectBookQueryHandler : IRequestHandler<GetByIdSubjectBookQuery, GetByIdSubjectBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISubjectBookRepository _subjectBookRepository;
        private readonly SubjectBookBusinessRules _subjectBookBusinessRules;

        public GetByIdSubjectBookQueryHandler(IMapper mapper, ISubjectBookRepository subjectBookRepository, SubjectBookBusinessRules subjectBookBusinessRules)
        {
            _mapper = mapper;
            _subjectBookRepository = subjectBookRepository;
            _subjectBookBusinessRules = subjectBookBusinessRules;
        }

        public async Task<GetByIdSubjectBookResponse> Handle(GetByIdSubjectBookQuery request, CancellationToken cancellationToken)
        {
            SubjectBook? subjectBook = await _subjectBookRepository.GetAsync(predicate: sb => sb.Id == request.Id, cancellationToken: cancellationToken);
            await _subjectBookBusinessRules.SubjectBookShouldExistWhenSelected(subjectBook);

            GetByIdSubjectBookResponse response = _mapper.Map<GetByIdSubjectBookResponse>(subjectBook);
            return response;
        }
    }
}