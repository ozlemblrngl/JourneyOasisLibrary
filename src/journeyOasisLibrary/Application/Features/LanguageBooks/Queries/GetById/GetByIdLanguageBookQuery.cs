using Application.Features.LanguageBooks.Constants;
using Application.Features.LanguageBooks.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using MediatR;
using static Application.Features.LanguageBooks.Constants.LanguageBooksOperationClaims;

namespace Application.Features.LanguageBooks.Queries.GetById;

public class GetByIdLanguageBookQuery : IRequest<GetByIdLanguageBookResponse>, ISecuredRequest
{
    public Guid Id { get; set; }

    public string[] Roles => [Admin, Read];

    public class GetByIdLanguageBookQueryHandler : IRequestHandler<GetByIdLanguageBookQuery, GetByIdLanguageBookResponse>
    {
        private readonly IMapper _mapper;
        private readonly ILanguageBookRepository _languageBookRepository;
        private readonly LanguageBookBusinessRules _languageBookBusinessRules;

        public GetByIdLanguageBookQueryHandler(IMapper mapper, ILanguageBookRepository languageBookRepository, LanguageBookBusinessRules languageBookBusinessRules)
        {
            _mapper = mapper;
            _languageBookRepository = languageBookRepository;
            _languageBookBusinessRules = languageBookBusinessRules;
        }

        public async Task<GetByIdLanguageBookResponse> Handle(GetByIdLanguageBookQuery request, CancellationToken cancellationToken)
        {
            LanguageBook? languageBook = await _languageBookRepository.GetAsync(predicate: lb => lb.Id == request.Id, cancellationToken: cancellationToken);
            await _languageBookBusinessRules.LanguageBookShouldExistWhenSelected(languageBook);

            GetByIdLanguageBookResponse response = _mapper.Map<GetByIdLanguageBookResponse>(languageBook);
            return response;
        }
    }
}